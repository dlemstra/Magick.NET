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
#include "ColorCMYK.h"

namespace ImageMagick
{
	//==============================================================================================
	ColorCMYK::ColorCMYK(MagickColor^ color)
		: ColorBase(Magick::Color::PixelType::CMYKAPixel)
	{
		Value->A = color->A;
		Value->B = color->B;
		Value->G = color->G;
		Value->K = color->K;
		Value->R = color->R;
	}
	//==============================================================================================
	ColorCMYK::ColorCMYK(Magick::Quantum cyan, Magick::Quantum magenta, Magick::Quantum yellow,
		Magick::Quantum key)
		: ColorBase(Magick::Color::PixelType::CMYKPixel)
	{
		Value->B = yellow;
		Value->G = magenta;
		Value->K = key;
		Value->R = cyan;
	}
	//==============================================================================================
	ColorCMYK::ColorCMYK(Magick::Quantum cyan, Magick::Quantum magenta, Magick::Quantum yellow,
		Magick::Quantum key, Magick::Quantum alpha)
		: ColorBase(Magick::Color::PixelType::CMYKAPixel)
	{
		Value->A = alpha;
		Value->B = yellow;
		Value->G = magenta;
		Value->K = key;
		Value->R = cyan;
	}
	//==============================================================================================
	Magick::Quantum ColorCMYK::A::get()
	{
		return Value->A;
	}
	//==============================================================================================
	void ColorCMYK::A::set(Magick::Quantum value)
	{
		Value->A = value;
	}
	//==============================================================================================
	Magick::Quantum ColorCMYK::C::get()
	{
		return Value->R;
	}
	//==============================================================================================
	void ColorCMYK::C::set(Magick::Quantum value)
	{
		Value->R = value;
	}
	//==============================================================================================
	Magick::Quantum ColorCMYK::K::get()
	{
		return Value->K;
	}
	//==============================================================================================
	void ColorCMYK::K::set(Magick::Quantum value)
	{
		Value->K = value;
	}
	//==============================================================================================
	Magick::Quantum ColorCMYK::M::get()
	{
		return Value->G;
	}
	//==============================================================================================
	void ColorCMYK::M::set(Magick::Quantum value)
	{
		Value->G = value;
	}
	//==============================================================================================
	Magick::Quantum ColorCMYK::Y::get()
	{
		return Value->B;
	}
	//==============================================================================================
	void ColorCMYK::Y::set(Magick::Quantum value)
	{
		Value->B = value;
	}
	//==============================================================================================
	ColorCMYK::operator ColorCMYK^ (MagickColor^ color)
	{
		return FromMagickColor(color);
	}
	//==============================================================================================
	ColorCMYK^ ColorCMYK::FromMagickColor(MagickColor^ color)
	{
		if (color == nullptr)
			return nullptr;

		return gcnew ColorCMYK(color);
	}
	//==============================================================================================
}
