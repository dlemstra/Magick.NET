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
#include "ColorGray.h"

namespace ImageMagick
{
	//==============================================================================================
	ColorGray::ColorGray(MagickColor^ color)
		: ColorBase(false, color)
	{
		_Shade = Magick::Color::scaleQuantumToDouble(color->R);
	}
	//==============================================================================================
	void ColorGray::UpdateValue()
	{
		Magick::Quantum gray = Magick::Color::scaleDoubleToQuantum(_Shade);
		Value->R = gray;
		Value->G = gray;
		Value->B = gray;
	}
	//==============================================================================================
	ColorGray::ColorGray(double shade)
		: ColorBase(false)
	{
		Shade = shade;
	}
	//==============================================================================================
	double ColorGray::Shade::get()
	{
		return _Shade;
	}
	//==============================================================================================
	void ColorGray::Shade::set(double value)
	{
		if (value < 0.0 || value > 1.0)
			return;

		_Shade = value;
	}
	//==============================================================================================
	ColorGray^ ColorGray::FromMagickColor(MagickColor^ color)
	{
		if (color == nullptr)
			return nullptr;

		return gcnew ColorGray(color);
	}
	//==============================================================================================
}
