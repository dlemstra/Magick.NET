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
#include "ColorHSL.h"
#include "..\Quantum.h"

namespace ImageMagick
{
	//==============================================================================================
	ColorHSL::ColorHSL(MagickColor^ color)
		: ColorBase(Magick::Color::PixelType::RGBPixel)
	{
		double hue, luminosity, saturation;
		MagickCore::ConvertRGBToHSL(color->R, color->G, color->B, &hue, &luminosity, &saturation);

		Hue = hue;
		Luminosity = luminosity;
		Saturation = saturation;
	}
	//==============================================================================================
	void ColorHSL::UpdateValue()
	{
		double red, green, blue;
		MagickCore::ConvertHSLToRGB(Hue, Saturation, Luminosity, &red, &green, &blue);

		Value->R = Quantum::Convert(red);
		Value->G = Quantum::Convert(green);
		Value->B = Quantum::Convert(blue);
	}
	//==============================================================================================
	ColorHSL::ColorHSL(double hue, double saturation, double luminosity)
		: ColorBase(Magick::Color::PixelType::RGBPixel)
	{
		Hue = hue;
		Saturation = saturation;
		Luminosity = luminosity;
	}
	//==============================================================================================
	ColorHSL::operator ColorHSL^ (MagickColor^ color)
	{
		return FromMagickColor(color);
	}
	//==============================================================================================
	ColorHSL^ ColorHSL::FromMagickColor(MagickColor^ color)
	{
		if (color == nullptr)
			return nullptr;

		return gcnew ColorHSL(color);
	}
	//==============================================================================================
}