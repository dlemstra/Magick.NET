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
#include "MagickColor.h"

namespace ImageMagick
{
	//==============================================================================================
	MagickColor::MagickColor(MagickColor^ color)
	{
		_Red = color->_Red;
		_Green = color->_Green;
		_Blue = color->_Blue;
		_Alpha = color->_Alpha;
	}
	//==============================================================================================
	MagickColor::MagickColor(Magick::Color color)
	{
		_Red = color.redQuantum();
		_Green = color.greenQuantum();
		_Blue = color.blueQuantum();
		_Alpha = color.alphaQuantum();
	}
	//==============================================================================================
	void MagickColor::Initialize(Color color)
	{
#if (MAGICKCORE_QUANTUM_DEPTH == 8)
		_Red = color.R;
		_Green = color.G;
		_Blue = color.B;
		_Alpha = MaxMap - color.A;
#else if (MAGICKCORE_QUANTUM_DEPTH == 16)
		_Red = (color.R << 8 | color.R);
		_Green = (color.G << 8 | color.G);
		_Blue = (color.B << 8 | color.B);
		_Alpha = MaxMap - (color.A << 8 | color.A);
#endif
	}
	//==============================================================================================
	Magick::Color* MagickColor::CreateColor()
	{
		return new Magick::Color(_Red, _Green, _Blue, _Alpha);
	}
	//==============================================================================================
	MagickColor::MagickColor()
	{
		_Alpha = MaxMap;
	}
	//==============================================================================================
	MagickColor::MagickColor(Magick::Quantum red, Magick::Quantum green, Magick::Quantum blue)
	{
		_Red = red;
		_Green = green;
		_Blue = blue;
		_Alpha = MaxMap;
	}
	//==============================================================================================
	MagickColor::MagickColor(Magick::Quantum red, Magick::Quantum green, Magick::Quantum blue, 
		Magick::Quantum alpha)
	{
		_Red = red;
		_Green = green;
		_Blue = blue;
		_Alpha = alpha;
	}
	//==============================================================================================
	MagickColor::MagickColor(Color color)
	{
		Initialize(color);
	}
	//==============================================================================================
	Color MagickColor::ToColor()
	{
		return Color::FromArgb(_Alpha, _Red, _Green, _Blue);
	}
	//==============================================================================================
}