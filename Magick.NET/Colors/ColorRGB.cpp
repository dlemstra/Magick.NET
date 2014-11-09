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
#include "ColorRGB.h"

namespace ImageMagick
{
	//==============================================================================================
	ColorRGB::ColorRGB(MagickColor^ color)
		: ColorBase(Magick::Color::PixelType::RGBPixel)
	{
		Value->B = color->B;
		Value->G = color->G;
		Value->R = color->R;
	}
	//==============================================================================================
	ColorRGB::ColorRGB(Magick::Quantum red, Magick::Quantum green, Magick::Quantum blue)
		: ColorBase(Magick::Color::PixelType::RGBPixel)
	{
		Value->B = blue;
		Value->G = green;
		Value->R = red;
	}
	//==============================================================================================
	ColorRGB::ColorRGB(Color color)
		: ColorBase(Magick::Color::PixelType::RGBPixel)
	{
		Value->Initialize(color);
	}
	//==============================================================================================
	Magick::Quantum ColorRGB::B::get()
	{
		return Value->B;
	}
	//==============================================================================================
	void ColorRGB::B::set(Magick::Quantum value)
	{
		Value->B = value;
	}
	//==============================================================================================
	Magick::Quantum ColorRGB::G::get()
	{
		return Value->G;
	}
	//==============================================================================================
	void ColorRGB::G::set(Magick::Quantum value)
	{
		Value->G = value;
	}
	//==============================================================================================
	Magick::Quantum ColorRGB::R::get()
	{
		return Value->R;
	}
	//==============================================================================================
	void ColorRGB::R::set(Magick::Quantum value)
	{
		Value->R = value;
	}
	//==============================================================================================
	ColorRGB::operator ColorRGB^ (MagickColor^ color)
	{
		return FromMagickColor(color);
	}
	//==============================================================================================
	ColorRGB^ ColorRGB::FromMagickColor(MagickColor^ color)
	{
		if (color == nullptr)
			return nullptr;

		return gcnew ColorRGB(color);
	}
	//==============================================================================================
}
