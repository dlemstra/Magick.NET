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
#include "MagickReadSettings.h"

namespace ImageMagick
{
	//==============================================================================================
	void MagickReadSettings::Apply(Magick::Image* image)
	{
		if (ColorSpace.HasValue)
		{
			// For some reason I cannot access the methods of image->options(). This trick makes sure
			// that calling image->colorspaceType() will not raise an exception.
			Magick::Geometry size = image->size();
			image->size(Magick::Geometry(1, 1));
			image->read("xc:white");

			image->colorspaceType((MagickCore::ColorspaceType)ColorSpace.Value);
			image->size(size);
		}

		if (Density != nullptr)
		{
			const Magick::Geometry* geometry = Density->CreateGeometry();
			image->density(*geometry);
			delete geometry;
		}

		if (Format.HasValue)
		{
			std::string name;
			Marshaller::Marshal(Enum::GetName(MagickFormat::typeid, Format.Value), name);
			image->magick(name);
		}

		if (Width.HasValue && Height.HasValue)
		{
			Magick::Geometry geometry = Magick::Geometry(Width.Value, Height.Value);
			image->size(geometry);
		}
	}
	//==============================================================================================
	void MagickReadSettings::Apply(MagickCore::ImageInfo *imageInfo)
	{
		if (ColorSpace.HasValue)
			imageInfo->colorspace = (MagickCore::ColorspaceType)ColorSpace.Value;

		if (Density != nullptr)
		{
			const Magick::Geometry* geometry = Density->CreateGeometry();
			std::string geometryStr = *geometry;
			MagickCore::CloneString(&imageInfo->density, geometryStr.c_str());
			delete geometry;
		}

		if (Format.HasValue)
		{
			std::string name;
			Marshaller::Marshal(Enum::GetName(MagickFormat::typeid, Format.Value), name);
			MagickCore::CopyMagickString(imageInfo->magick, name.c_str(), MaxTextExtent);
		}

		if (Width.HasValue && Height.HasValue)
		{
			Magick::Geometry geometry = Magick::Geometry(Width.Value, Height.Value);
			std::string geometryStr = geometry;
			MagickCore::CloneString(&imageInfo->size, geometryStr.c_str());
		}
	}
	//==============================================================================================
}