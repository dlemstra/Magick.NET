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
#include "Stdafx.h"
#include "ColorProfile.h"
#include "..\Helpers\FileHelper.h"
#include "..\IO\MagickReader.h"

namespace ImageMagick
{
	//==============================================================================================
	void ColorProfile::Initialize(String^ name, Stream^ stream)
	{
		Throw::IfNullOrEmpty("name", name);

		_Name = name;
		_Data = MagickReader::Read(stream);
	}
	//==============================================================================================
	ColorProfile::ColorProfile(String^ name, Stream^ stream)
	{
		Initialize(name, stream);
	}
	//==============================================================================================
	array<Byte>^ ColorProfile::Data::get()
	{
		return _Data;
	}
	//==============================================================================================
	ColorProfile::ColorProfile(String^ name, String^ fileName)
	{
		String^ filePath = FileHelper::CheckForBaseDirectory(fileName);
		Throw::IfInvalidFileName(filePath);

		FileStream^ stream = File::OpenRead(filePath);
		Initialize(name, stream);
		delete stream;
	}
	//==============================================================================================
	String^ ColorProfile::Name::get()
	{
		return _Name;
	}
	//==============================================================================================
}