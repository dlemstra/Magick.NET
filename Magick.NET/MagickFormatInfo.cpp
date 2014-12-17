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
#include "MagickFormatInfo.h"
#include "Exceptions\Base\MagickException.h"
#include "Helpers\EnumHelper.h"

using namespace System::Globalization;

namespace ImageMagick
{
	//==============================================================================================
	void MagickFormatInfo::AddStealthCoder(std::vector<Magick::CoderInfo>* coderList, std::string name)
	{
		try
		{
			Magick::CoderInfo coderInfo(name);
			coderList->push_back(coderInfo);
		}
		catch(Magick::ErrorModule)
		{
		}
		catch(Magick::Exception& exception)
		{
			MagickException::Throw(exception);
		}
	}
	//==============================================================================================
	Collection<MagickFormatInfo^>^ MagickFormatInfo::LoadFormats()
	{
		Collection<MagickFormatInfo^>^ result = gcnew Collection<MagickFormatInfo^>();

		std::vector<Magick::CoderInfo> coderList; 

		try
		{
			coderInfoList(&coderList);
		}
		catch(Magick::Exception& exception)
		{
			MagickException::Throw(exception);
		}

		AddStealthCoder(&coderList, "DIB");
		AddStealthCoder(&coderList, "TIF");

		std::vector<Magick::CoderInfo>::const_iterator coder = coderList.begin(); 
		while(coder != coderList.end())
		{
			MagickFormatInfo^ formatInfo = gcnew MagickFormatInfo();

			String^ name = Marshaller::Marshal(coder->name());

			name = name->Replace("-", "");
			if (name == "3FR")
				name = "ThreeFr";

			formatInfo->_Format = EnumHelper::Parse<MagickFormat>(name, MagickFormat::Unknown);
			formatInfo->_Description = Marshaller::Marshal(coder->description());
			formatInfo->_MimeType = Marshaller::Marshal(coder->mimeType());
			formatInfo->_CoderInfo = new Magick::CoderInfo(*coder);

			result->Add(formatInfo);

			coder++;
		} 

		return result;
	}
	//==============================================================================================
	String^ MagickFormatInfo::Description::get()
	{
		return _Description;
	}
	//==============================================================================================
	MagickFormat MagickFormatInfo::Format::get()
	{
		return _Format;
	}
	//==============================================================================================
	bool MagickFormatInfo::IsMultiFrame::get()
	{
		return _CoderInfo->isMultiFrame();
	}
	//==============================================================================================
	bool MagickFormatInfo::IsReadable::get()
	{
		return _CoderInfo->isReadable();
	}
	//==============================================================================================
	bool MagickFormatInfo::IsWritable::get()
	{
		return _CoderInfo->isWritable();
	}
	//==============================================================================================
	String^ MagickFormatInfo::MimeType::get()
	{
		return _MimeType;
	}
	//==============================================================================================
	String^ MagickFormatInfo::ToString()
	{
		return String::Format(CultureInfo::InvariantCulture, "{0}: {1} ({2}R{3}W{4}M)", Format, Description, 
			IsReadable ? "+" : "-", IsWritable ? "+" : "-", IsMultiFrame ? "+" : "-");
	}
	//==============================================================================================
	bool MagickFormatInfo::Unregister()
	{
		return _CoderInfo->unregister();
	}
	//==============================================================================================
}