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
#include "ColorYUV.h"
#include "..\Quantum.h"

namespace ImageMagick
{
	//==============================================================================================
	ColorYUV::ColorYUV(MagickColor^ color)
		: ColorBase(Magick::Color::PixelType::RGBPixel)
	{
		_Y = (0.29900 * color->R) + (0.58700 * color->G) + (0.11400 * color->B);
		_U = (-0.14740 * color->R) - (0.28950 * color->G) + (0.43690 * color->B);
		_V = (0.61500 * color->R) - (0.51500 * color->G) - (0.10000 * color->B);
	}
	//==============================================================================================
	void ColorYUV::UpdateValue()
	{
		Value->R = Quantum::Scale(_Y + 1.13980 * _V);
		Value->G = Quantum::Scale(_Y - (0.39380 * _U) - (0.58050 * _V));
		Value->B = Quantum::Scale(_Y + 2.02790 * _U);
	}
	//==============================================================================================
	ColorYUV::ColorYUV(double y, double u, double v)
		: ColorBase(Magick::Color::PixelType::RGBPixel)
	{
		_Y = y;
		_U = u;
		_V = v;
	}
	//==============================================================================================
	double ColorYUV::U::get()
	{
		return _U;
	}
	//==============================================================================================
	void ColorYUV::U::set(double value)
	{
		if (value < -0.5 || value > 0.5)
			return;

		_U = value;
	}
	//==============================================================================================
	double ColorYUV::V::get()
	{
		return _V;
	}
	//==============================================================================================
	void ColorYUV::V::set(double value)
	{
		if (value < -0.5 || value > 0.5)
			return;

		_V = value;
	}
	//==============================================================================================
	double ColorYUV::Y::get()
	{
		return _Y;
	}
	//==============================================================================================
	void ColorYUV::Y::set(double value)
	{
		if (value < 0.0 || value > 1.0)
			return;

		_Y = value;
	}
	//==============================================================================================
	ColorYUV::operator ColorYUV^ (MagickColor^ color)
	{
		return FromMagickColor(color);
	}
	//==============================================================================================
	ColorYUV^ ColorYUV::FromMagickColor(MagickColor^ color)
	{
		if (color == nullptr)
			return nullptr;

		return gcnew ColorYUV(color);
	}
	//==============================================================================================
}