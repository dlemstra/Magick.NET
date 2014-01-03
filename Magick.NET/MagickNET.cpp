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

using namespace System::IO;
using namespace System::Security;

namespace ImageMagick
{
	//==============================================================================================
	void MagickNET::CheckImageMagickFiles(String^ path)
	{
		for each (String^ imageMagickFile in _ImageMagickFiles)
		{
			String^ fileName = path + "\\" + imageMagickFile;
			Throw::IfFalse("path", File::Exists(fileName), "Unable to find file: " + fileName);
		}
	}
	//==============================================================================================
	String^ MagickNET::Features::get()
	{
		std::string features = std::string(MagickCore::GetMagickFeatures());

		return Marshaller::Marshal(features);
	}
	//==============================================================================================
	IEnumerable<MagickFormatInfo^>^ MagickNET::SupportedFormats::get()
	{
		return MagickFormatInfo::All;
	}
	//==============================================================================================
	bool MagickNET::ThrowWarnings::get()
	{
		return _ThrowWarnings;
	}
	//==============================================================================================
	void MagickNET::ThrowWarnings::set(bool value)
	{
		_ThrowWarnings = value;
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
		Throw::IfNullOrEmpty("path", path);

		path = Path::GetFullPath(path);
		Throw::IfFalse("path", Directory::Exists(path), "Unable to find path: " + path);

		CheckImageMagickFiles(path);

		std::string configurePath;
		_putenv_s("MAGICK_CONFIGURE_PATH", Marshaller::Marshal(path, configurePath).c_str());
	}
	//==============================================================================================
	void MagickNET::SetCacheThreshold(Magick::MagickSizeType threshold)
	{
		Magick::Image::cacheThreshold((size_t)threshold);
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
		Throw::IfNullOrEmpty("path", path);
		Throw::IfFalse("directory", Directory::Exists(path), "Unable to find directory: " + path);

		std::string tempDirectory;
		Marshaller::Marshal(path, tempDirectory);
		_putenv_s("MAGICK_TEMPORARY_PATH", tempDirectory.c_str());
	}
	//==============================================================================================
}
