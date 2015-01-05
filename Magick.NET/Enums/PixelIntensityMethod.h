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
#pragma once

#include "Stdafx.h"

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Pixel intensity methods.
	///</summary>
	public enum class PixelIntensityMethod
	{
		Undefined = Magick::UndefinedPixelIntensityMethod,
		Average = Magick::AveragePixelIntensityMethod,
		Brightness = Magick::BrightnessPixelIntensityMethod,
		Lightness = Magick::LightnessPixelIntensityMethod,
		Rec601Luma = Magick::Rec601LumaPixelIntensityMethod,
		Rec601Luminance = Magick::Rec601LuminancePixelIntensityMethod,
		Rec709LumaPixel = Magick::Rec709LumaPixelIntensityMethod,
		Rec709Luminance = Magick::Rec709LuminancePixelIntensityMethod,
		RMS = Magick::RMSPixelIntensityMethod,
		MS = Magick::MSPixelIntensityMethod
	};
	//==============================================================================================
}