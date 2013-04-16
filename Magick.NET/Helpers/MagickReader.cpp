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
#include "MagickReader.h"

namespace ImageMagick
{
	//==============================================================================================
	String^ MagickReader::GetReadWarning(Magick::Warning warning)
	{
		return Marshaller::Marshal(warning.what());
	}
	//==============================================================================================
	void MagickReader::SetSize(Magick::Image* image, Nullable<int> width, Nullable<int> height)
	{
		if (!width.HasValue || !height.HasValue)
			return;

		Magick::Geometry geometry = Magick::Geometry(width.Value, height.Value);
		image->size(geometry);
	}
	//==============================================================================================
	String^ MagickReader::Read(Magick::Image* image, bool ping, MagickBlob^ blob, Nullable<int> width,
		Nullable<int> height, Nullable<ColorSpace> colorSpace)
	{
		Throw::IfNull("blob", blob);

		String^ readWarning = nullptr;

		try
		{
			if (ping)
			{
				image->ping(blob);
			}
			else
			{
				SetSize(image, width, height);
				image->read(blob);
			}

			if (colorSpace.HasValue)
				SetColorSpace(image, colorSpace.Value);
		}
		catch (Magick::Warning exception)
		{
			readWarning = GetReadWarning(exception);
		}
		catch (Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
		}

		return readWarning;
	}
	//==============================================================================================
	String^ MagickReader::Read(Magick::Image* image, bool ping, String^ fileName, Nullable<int> width,
		Nullable<int> height, Nullable<ColorSpace> colorSpace)
	{
		Throw::IfInvalidFileName(fileName);

		String^ readWarning = nullptr;

		try
		{
			std::string imageSpec;
			Marshaller::Marshal(fileName, imageSpec);

			if (ping)
			{
				image->ping(imageSpec);
			}
			else
			{
				SetSize(image, width, height);
				image->read(imageSpec);
			}

			if (colorSpace.HasValue)
				SetColorSpace(image, colorSpace.Value);
		}
		catch (Magick::Warning exception)
		{
			readWarning = GetReadWarning(exception);
		}
		catch (Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
		}

		return readWarning;
	}
	//==============================================================================================
	String^ MagickReader::Read(Magick::Image* image, bool ping, Stream^ stream, Nullable<int> width,
		Nullable<int> height, Nullable<ColorSpace> colorSpace)
	{
		MagickBlob^ blob = MagickBlob::Read(stream);
		return Read(image, ping, blob, width, height, colorSpace);
	}
	//==============================================================================================
	void MagickReader::SetColorSpace(Magick::Image* image, ColorSpace colorSpace)
	{
		MagickCore::ColorspaceType colorSpaceType = (MagickCore::ColorspaceType)colorSpace;

		if (image->colorSpace() == colorSpaceType)
			return;

		if (image->colorSpace() == MagickCore::ColorspaceType::CMYKColorspace &&
			(colorSpace == ColorSpace::RGB || colorSpace == ColorSpace::sRGB))
		{
			image->profile("ICM", ColorProfile::SRGB);
		}

		image->colorSpace((MagickCore::ColorspaceType)colorSpace);
	}
	//==============================================================================================
	String^ MagickReader::Read(std::list<Magick::Image>* imageList, String^ fileName,
		Nullable<ColorSpace> colorSpace)
	{
		Throw::IfInvalidFileName(fileName);

		String^ readWarning = nullptr;

		try
		{
			std::string imageSpec;
			Marshaller::Marshal(fileName, imageSpec);

			Magick::readImages(imageList, (std::string)imageSpec);

			if (colorSpace.HasValue)
				SetColorSpace(imageList, colorSpace.Value);
		}
		catch (Magick::Warning exception)
		{
			readWarning = GetReadWarning(exception);
		}
		catch (Magick::Exception& exception)
		{
			throw MagickException::Create(exception);
		}

		return readWarning;
	}
	//==============================================================================================
	void MagickReader::SetColorSpace(std::list<Magick::Image>* imageList, ColorSpace colorSpace)
	{
		std::list<Magick::Image>::iterator iter;

		for(iter = imageList->begin(); iter != imageList->end(); iter++)
		{
			SetColorSpace(&*(iter), colorSpace);
		}
	}
	//===========================================================================================
	String^ MagickReader::Read(Magick::Image* image, bool ping, MagickBlob^ blob)
	{
		return Read(image, ping, blob, Nullable<int>(), Nullable<int>(), Nullable<ColorSpace>());
	}
	//===========================================================================================
	String^ MagickReader::Read(Magick::Image* image, bool ping, MagickBlob^ blob, ColorSpace colorSpace)
	{
		return Read(image, ping, blob, Nullable<int>(), Nullable<int>(), Nullable<ColorSpace>(colorSpace));
	}
	//===========================================================================================
	String^ MagickReader::Read(Magick::Image* image, bool ping, String^ fileName)
	{
		return Read(image, ping, fileName, Nullable<int>(), Nullable<int>(), Nullable<ColorSpace>());
	}
	//===========================================================================================
	String^ MagickReader::Read(Magick::Image* image, bool ping, String^ fileName, ColorSpace colorSpace)
	{
		return Read(image, ping, fileName, Nullable<int>(), Nullable<int>(), Nullable<ColorSpace>(colorSpace));
	}
	//===========================================================================================
	String^ MagickReader::Read(Magick::Image* image, bool ping, Stream^ stream)
	{
		return Read(image, ping, stream, Nullable<int>(), Nullable<int>(), Nullable<ColorSpace>());
	}
	//===========================================================================================
	String^ MagickReader::Read(Magick::Image* image, bool ping, Stream^ stream, ColorSpace colorSpace)
	{
		return Read(image, ping, stream, Nullable<int>(), Nullable<int>(), Nullable<ColorSpace>(colorSpace));
	}
	//===========================================================================================
	String^ MagickReader::Read(Magick::Image* image, MagickBlob^ blob, int width, int height)
	{
		return Read(image, false, blob, width, height, Nullable<ColorSpace>());
	}
	//===========================================================================================
	String^ MagickReader::Read(Magick::Image* image, String^ fileName, int width, int height)
	{
		return Read(image, false, fileName, width, height, Nullable<ColorSpace>());
	}
	//===========================================================================================
	String^ MagickReader::Read(Magick::Image* image, Stream^ stream, int width, int height)
	{
		return Read(image, false, stream, width, height, Nullable<ColorSpace>());
	}
	//===========================================================================================
	String^ MagickReader::Read(std::list<Magick::Image>* imageList, String^ fileName)
	{
		return Read(imageList, fileName, Nullable<ColorSpace>());
	}
	//===========================================================================================
	String^ MagickReader::Read(std::list<Magick::Image>* imageList, String^ fileName, ColorSpace colorSpace)
	{
		return Read(imageList, fileName, Nullable<ColorSpace>(colorSpace));
	}
	//==============================================================================================
}