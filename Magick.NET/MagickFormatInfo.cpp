//=================================================================================================
// Copyright 2013 Dirk Lemstra <http://magick.codeplex.com/>
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
#include "stdafx.h"
#include "MagickFormatInfo.h"
#include "Exceptions\Base\MagickException.h"
#include "Helpers\EnumHelper.h"

namespace ImageMagick
{
	//==============================================================================================
	Collection<MagickFormatInfo^>^ MagickFormatInfo::LoadFormats()
	{
		Collection<MagickFormatInfo^>^ result = gcnew Collection<MagickFormatInfo^>();

		std::list<Magick::CoderInfo> coderList; 

		try
		{
			coderInfoList(&coderList, Magick::CoderInfo::AnyMatch, Magick::CoderInfo::AnyMatch, Magick::CoderInfo::AnyMatch);
		}
		catch(Magick::ErrorModule)
		{
		}
		catch(Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
		}

		std::list<Magick::CoderInfo>::const_iterator coder = coderList.begin(); 
		while(coder != coderList.end())
		{
			MagickFormatInfo^ formatInfo = gcnew MagickFormatInfo();

			String^ name = Marshaller::Marshal(coder->name());
			
			name = name->Replace("-", "");
			if (name == "3FR")
				name = "ThreeFR";

			formatInfo->_Format = EnumHelper::Parse<MagickFormat>(name, MagickFormat::Unknown);
			formatInfo->_Description = Marshaller::Marshal(coder->description());
			formatInfo->_IsMultiFrame = coder->isMultiFrame();
			formatInfo->_IsReadable = coder->isReadable();
			formatInfo->_IsWritable = coder->isWritable();

			result->Add(formatInfo);

			coder++;
		} 

		return result;
	}
	//==============================================================================================
	MagickFormatInfo^ MagickFormatInfo::Get(MagickFormat format)
	{
		for each (MagickFormatInfo^ formatInfo in All)
		{
			if (formatInfo->Format == format)
				return formatInfo;
		}

		return nullptr;
	}
	//==============================================================================================
}