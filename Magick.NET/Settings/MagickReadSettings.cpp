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

using namespace System::Globalization;

namespace ImageMagick
{
	//==============================================================================================
	String^ MagickReadSettings::Scenes::get()
	{
		if (!FrameIndex.HasValue && !FrameCount.HasValue)
			return nullptr;

		if (FrameIndex.HasValue && (!FrameCount.HasValue || FrameCount.Value == 1))
			return FrameIndex.Value.ToString(CultureInfo::InvariantCulture);

		int frame = FrameIndex.HasValue ? FrameIndex.Value : 0;
		return String::Format(CultureInfo::InvariantCulture, "{0}-{1}", frame, frame + FrameCount.Value);
	}
	//==============================================================================================
	void MagickReadSettings::Apply(Magick::Image* image)
	{
		Throw::IfFalse("settings", (!FrameCount.HasValue || FrameCount.Value == 1) , "The FrameCount can only be set to 1 when a MagickImage is being read.");

		Apply(image->imageInfo());
	}
	//==============================================================================================
	void MagickReadSettings::Apply(MagickCore::ImageInfo *imageInfo)
	{
		if (ColorSpace.HasValue)
			imageInfo->colorspace = (MagickCore::ColorspaceType)ColorSpace.Value;

		if (Density != nullptr)
		{
			if (imageInfo->density != (char*)NULL)
				imageInfo->density=MagickCore::DestroyString(imageInfo->density);

			const Magick::Geometry* geometry = Density->CreateGeometry();
			std::string geometryStr = *geometry;
			MagickCore::CloneString(&imageInfo->density, geometryStr.c_str());
			delete geometry;
		}

		if (Format.HasValue)
		{
			std::string name;
			Marshaller::Marshal(Enum::GetName(MagickFormat::typeid, Format.Value), name);
			MagickCore::CopyMagickString(imageInfo->magick, name.c_str(), MaxTextExtent - 1);
		}

		if (FrameIndex.HasValue || FrameCount.HasValue)
		{
			if (imageInfo->scenes != (char*)NULL)
				imageInfo->scenes=MagickCore::DestroyString(imageInfo->scenes);

			std::string scenes;
			Marshaller::Marshal(Scenes, scenes);
			MagickCore::CloneString(&imageInfo->scenes, scenes.c_str());
		}

		if (Width.HasValue && Height.HasValue)
		{
			if (imageInfo->size != (char*)NULL)
				imageInfo->size=MagickCore::DestroyString(imageInfo->size);

			Magick::Geometry geometry = Magick::Geometry(Width.Value, Height.Value);
			std::string geometryStr = geometry;
			MagickCore::CloneString(&imageInfo->size, geometryStr.c_str());
		}
	}
	//==============================================================================================
	MagickReadSettings::MagickReadSettings()
	{
	}
	//==============================================================================================
}