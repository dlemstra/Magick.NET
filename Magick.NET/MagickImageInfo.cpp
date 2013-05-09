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
#include "MagickImageInfo.h"

namespace ImageMagick
{
	//==============================================================================================
	void MagickImageInfo::Initialize(Magick::Image* image)
	{
		_FileName = Marshaller::Marshal(image->baseFilename());
		_ColorSpace = (ImageMagick::ColorSpace)image->colorSpace();
		_Height = Convert::ToInt32(image->size().height());
		_Width = Convert::ToInt32(image->size().width());
		_Format = EnumHelper::Parse<MagickFormat>(Marshaller::Marshal(image->magick()), MagickFormat::Unknown);
	}
	//==============================================================================================
	MagickImageInfo::MagickImageInfo(array<Byte>^ data)
	{
		Read(data);
	}
	//==============================================================================================
	MagickImageInfo::MagickImageInfo(String^ fileName)
	{
		Read(fileName);
	}
	//==============================================================================================
	MagickImageInfo::MagickImageInfo(Stream^ stream)
	{
		Read(stream);
	}
	//==============================================================================================
	void MagickImageInfo::Read(array<Byte>^ data)
	{
		Magick::Image* image = new Magick::Image();
		MagickReader::Read(image, data, true);
		Initialize(image);
		delete image;
	}
	//==============================================================================================
	void MagickImageInfo::Read(String^ fileName)
	{
		Magick::Image* image = new Magick::Image();
		MagickReader::Read(image, fileName, true);
		Initialize(image);
		delete image;
	}
	//==============================================================================================
	void MagickImageInfo::Read(Stream^ stream)
	{
		Magick::Image* image = new Magick::Image();
		MagickReader::Read(image, stream, true);
		Initialize(image);
		delete image;
	}
	//==============================================================================================
}