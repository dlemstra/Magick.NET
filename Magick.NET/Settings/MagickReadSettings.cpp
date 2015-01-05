//=================================================================================================
// Copyright 2013-2015 Dirk Lemstra <https://magick.codeplex.com/>
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
	void MagickReadSettings::ApplyColorSpace(MagickCore::ImageInfo *imageInfo)
	{
		if (ColorSpace.HasValue)
			imageInfo->colorspace = (Magick::ColorspaceType)ColorSpace.Value;
	}
	//==============================================================================================
	void MagickReadSettings::ApplyDensity(MagickCore::ImageInfo *imageInfo)
	{
		if (!Density.HasValue)
			return;

		if (imageInfo->density != (char*)NULL)
			imageInfo->density=MagickCore::DestroyString(imageInfo->density);

		const Magick::Point* point = Density.Value.CreatePoint();
		std::string pointStr = *point;
		MagickCore::CloneString(&imageInfo->density, pointStr.c_str());
		delete point;
	}
	//==============================================================================================
	void MagickReadSettings::ApplyDimensions(MagickCore::ImageInfo *imageInfo)
	{
		if (!Width.HasValue || !Height.HasValue)
			return;

		if (imageInfo->size != (char*)NULL)
			imageInfo->size=MagickCore::DestroyString(imageInfo->size);

		Magick::Geometry geometry = Magick::Geometry(Width.Value, Height.Value);
		std::string geometryStr = geometry;
		MagickCore::CloneString(&imageInfo->size, geometryStr.c_str());
	}
	//==============================================================================================
	void MagickReadSettings::ApplyFormat(MagickCore::ImageInfo *imageInfo)
	{
		if (!Format.HasValue)
			return;

		std::string name;
		Marshaller::Marshal(Enum::GetName(MagickFormat::typeid, Format.Value) + ":", name);
		MagickCore::CopyMagickString(imageInfo->filename, name.c_str(), MaxTextExtent - 1);
	}
	//==============================================================================================
	void MagickReadSettings::ApplyFrame(MagickCore::ImageInfo *imageInfo)
	{
		if (!FrameIndex.HasValue && !FrameCount.HasValue)
			return;

		if (imageInfo->scenes != (char*)NULL)
			imageInfo->scenes=MagickCore::DestroyString(imageInfo->scenes);

		std::string scenes;
		Marshaller::Marshal(Scenes, scenes);
		MagickCore::CloneString(&imageInfo->scenes, scenes.c_str());

		imageInfo->scene = FrameIndex.HasValue ? FrameIndex.Value : 0;
		imageInfo->number_scenes = FrameCount.HasValue ? FrameCount.Value : 1;
	}
	//==============================================================================================
	void MagickReadSettings::ApplyDefines(MagickCore::ImageInfo *imageInfo)
	{
		if (_Defines->Count == 0)
			return;

		for each (String^ key in _Defines->Keys)
		{
			std::string option;
			Marshaller::Marshal(key, option);
			std::string value;
			Marshaller::Marshal(_Defines[key], value);
			(void) MagickCore::SetImageOption(imageInfo, option.c_str(), value.c_str());
		}
	}
	//==============================================================================================
	void MagickReadSettings::Apply(Magick::Image* image)
	{
		Throw::IfFalse("settings", (!FrameCount.HasValue || FrameCount.Value == 1) ,
			"The FrameCount can only be set to 1 when a MagickImage is being read.");

		Apply(image->imageInfo());
	}
	//==============================================================================================
	void MagickReadSettings::Apply(MagickCore::ImageInfo *imageInfo)
	{
		ApplyColorSpace(imageInfo);
		ApplyDensity(imageInfo);
		ApplyDimensions(imageInfo);
		ApplyFormat(imageInfo);
		ApplyFrame(imageInfo);
		ApplyDefines(imageInfo);
	}
	//==============================================================================================
	MagickReadSettings::MagickReadSettings()
	{
		_Defines = gcnew Dictionary<String^, String^>();
	}
	//==============================================================================================
	void MagickReadSettings::SetDefine(MagickFormat format, String^ name, String^ value)
	{
		Throw::IfNullOrEmpty("name", name);
		Throw::IfNull("value", value);

		_Defines[Enum::GetName(MagickFormat::typeid, format) + ":" + name] = value;
	}
	//==============================================================================================
}