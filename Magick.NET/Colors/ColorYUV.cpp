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
#include "ColorYUV.h"

namespace ImageMagick
{
	//==============================================================================================
	ColorYUV::ColorYUV(MagickColor^ color)
		: ColorBase(false, color)
	{
		_Y = (0.29900 * color->R) + (0.58700 * color->G) + (0.11400 * color->B);
		_U = (-0.14740 * color->R) - (0.28950 * color->G) + (0.43690 * color->B);
		_V = (0.61500 * color->R) - (0.51500 * color->G) - (0.10000 * color->B);
	}
	//==============================================================================================
	void ColorYUV::UpdateValue()
	{
		Value->R = Magick::Color::scaleDoubleToQuantum(_Y + 1.13980 * _V);
		Value->G = Magick::Color::scaleDoubleToQuantum(_Y - (0.39380 * _U) - (0.58050 * _V));
		Value->B = Magick::Color::scaleDoubleToQuantum(_Y + 2.02790 * _U);
	}
	//==============================================================================================
	ColorYUV::ColorYUV(double y, double u, double v)
		: ColorBase(false)
	{
		_Y = y;
		_U = u;
		_V = v;
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