//=================================================================================================
// Copyright 2013-2014 Dirk Lemstra <https://magick.codeplex.com/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in 
// compliance with the License. You may obtain a copy of the License at
//
//   http://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
// express or implied. See the License for the specific language governing permissions and
// limitations under the License.
//=================================================================================================
#include "Stdafx.h"
#include "MagickNET.h"
#include "Helpers\EnumHelper.h"
#include "Helpers\FileHelper.h"

using namespace System::IO;
using namespace System::Security;

namespace ImageMagick
{
	//==============================================================================================
	String^ MagickNET::CheckDirectory(String^ path)
	{
		Throw::IfNullOrEmpty("path", path);

		path = FileHelper::CheckForBaseDirectory(path);
		path = Path::GetFullPath(path);
		Throw::IfFalse("path", Directory::Exists(path), "Unable to find directory: {0}", path);
		return path;
	}
	//==============================================================================================
	void MagickNET::CheckImageMagickFiles(String^ path)
	{
		for each (String^ imageMagickFile in _ImageMagickFiles)
		{
			String^ fileName = path + "\\" + imageMagickFile;
			Throw::IfFalse("path", File::Exists(fileName), "Unable to find file: {0}", fileName);
		}
	}
	//==============================================================================================
	void MagickNET::OnLog(const Magick::LogEventType type, const char* text)
	{
		if (text == NULL)
			return;

		if (_LogEvent == nullptr)
			return;

		std::string message=std::string(text);
		_LogEvent->Invoke(nullptr, gcnew LogEventArgs((LogEvents)type, Marshaller::Marshal(message)));
	}
	//==============================================================================================
	void MagickNET::SetEnv(const char* name, String^ value)
	{
		std::string val;
		_putenv_s(name, Marshaller::Marshal(value, val).c_str());
	}
	//==============================================================================================
	bool MagickNET::SetUseOpenCL(bool value)
	{
		try
		{
			if (value)
				return Magick::EnableOpenCL();
			else
				Magick::DisableOpenCL();
		}
		catch(Magick::Exception &exception)
		{
			MagickException::Throw(exception);
		}

		return false;
	}
	//==============================================================================================
	String^ MagickNET::Features::get()
	{
		std::string features = std::string(MagickCore::GetMagickFeatures());

		return Marshaller::Marshal(features);
	}
	//==============================================================================================
	void MagickNET::Log::add(EventHandler<LogEventArgs^>^ handler)
	{
		_LogEvent += handler;

		if (_LogDelegate != nullptr)
			return;

		_LogDelegate = gcnew MagickLogFuncDelegate(&OnLog);
		MagickCore::SetLogMethod((MagickCore::MagickLogMethod)Marshal::GetFunctionPointerForDelegate(_LogDelegate).ToPointer());
	}
	//==============================================================================================
	void MagickNET::Log::remove(EventHandler<LogEventArgs^>^ handler)
	{
		_LogEvent -= handler;

		if (_LogEvent != nullptr)
			return;

		MagickCore::SetLogMethod((MagickCore::MagickLogMethod)NULL);
		_LogDelegate = nullptr;
	}
	//==============================================================================================
	IEnumerable<MagickFormatInfo^>^ MagickNET::SupportedFormats::get()
	{
		return MagickFormatInfo::All;
	}
	//==============================================================================================
	bool MagickNET::UseOpenCL::get()
	{
		if (!_UseOpenCL.HasValue)
			_UseOpenCL = SetUseOpenCL(true);

		return _UseOpenCL.Value;
	}
	//==============================================================================================
	void MagickNET::UseOpenCL::set(bool value)
	{
		_UseOpenCL=SetUseOpenCL(value);
	}
	//==============================================================================================
	String^ MagickNET::Version::get()
	{
		Object^ title = (MagickNET::typeid)->Assembly->GetCustomAttributes(AssemblyTitleAttribute::typeid, false)[0];
		Object^ version = (MagickNET::typeid)->Assembly->GetCustomAttributes(AssemblyFileVersionAttribute::typeid, false)[0];
		return ((AssemblyTitleAttribute^)title)->Title + " " + ((AssemblyFileVersionAttribute^)version)->Version;
	}
	//==============================================================================================
	MagickFormatInfo^ MagickNET::GetFormatInformation(MagickFormat format)
	{
		for each (MagickFormatInfo^ formatInfo in SupportedFormats)
		{
			if (formatInfo->Format == format)
				return formatInfo;
		}

		return nullptr;
	}
	//==============================================================================================
	void MagickNET::Initialize(String^ path)
	{
		path = CheckDirectory(path);

		CheckImageMagickFiles(path);

		SetEnv("MAGICK_CONFIGURE_PATH", path);
	}
	//==============================================================================================
	void MagickNET::SetLogEvents(LogEvents events)
	{
		String^ eventFlags = nullptr;

		if (events == LogEvents::All)
		{
			eventFlags = "All";
		}
		else
		{
			List<String^>^ flags = gcnew List<String^>();
			for each(LogEvents flag in EnumHelper::GetFlags(events))
			{
				if (flag != LogEvents::All)
					flags->Add(Enum::GetName(LogEvents::typeid, flag));
			}

			eventFlags = String::Join(",", flags->ToArray());
		}

		std::string logEvents;
		Marshaller::Marshal(eventFlags, logEvents);
		MagickCore::SetLogEventMask(logEvents.c_str());
	}
	//==============================================================================================
	void MagickNET::SetTempDirectory(String^ path)
	{
		SetEnv("MAGICK_TEMPORARY_PATH", CheckDirectory(path));
	}
	//==============================================================================================
	void MagickNET::SetGhostscriptDirectory(String^ path)
	{
		SetEnv("MAGICK_GHOSTSCRIPT_PATH", CheckDirectory(path));
	}
	//==============================================================================================
	void MagickNET::SetGhostscriptFontDirectory(String^ path)
	{
		SetEnv("MAGICK_GHOSTSCRIPT_FONT_PATH", CheckDirectory(path));
	}
	//==============================================================================================
}