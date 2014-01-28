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
#include "ColorMono.h"
#include "..\Quantum.h"

namespace ImageMagick
{
	//==============================================================================================
	ColorMono::ColorMono(MagickColor^ color)
		: ColorBase(false, color)
	{
		IsBlack = color->R == 0.0;
	}
	//==============================================================================================
	void ColorMono::UpdateValue()
	{
		Magick::Quantum color = (IsBlack ? (Magick::Quantum)0.0 : Quantum::Max);
		Value->R = color;
		Value->G = color;
		Value->B = color;
	}
	//==============================================================================================
	ColorMono::ColorMono(bool isBlack)
		: ColorBase(false)
	{
		IsBlack = isBlack;
	}
	//==============================================================================================
	ColorMono::operator ColorMono^ (MagickColor^ color)
	{
		return FromMagickColor(color);
	}
	//==============================================================================================
	ColorMono^ ColorMono::FromMagickColor(MagickColor^ color)
	{
		if (color == nullptr)
			return nullptr;

		return gcnew ColorMono(color);
	}
	//==============================================================================================
}
