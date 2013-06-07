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
#include "DrawableFillColor.h"

namespace ImageMagick
{
	//==============================================================================================
	void DrawableFillColor::Initialize(MagickColor^ color)
	{
		const Magick::Color* magickColor = color->CreateColor();
		BaseValue = new Magick::DrawableFillColor(*magickColor);
		delete magickColor;
	}
	//==============================================================================================
	DrawableFillColor::DrawableFillColor(System::Drawing::Color color)
	{
		Initialize(gcnew MagickColor(color));
	}
	//==============================================================================================
	DrawableFillColor::DrawableFillColor(MagickColor^ color)
	{
		Initialize(color);
	}
	//==============================================================================================
	MagickColor^ DrawableFillColor::Color::get()
	{
		return gcnew MagickColor(Value->color());
	}
	//==============================================================================================
	void DrawableFillColor::Color::set(MagickColor^ value)
	{
		const Magick::Color* color = ReferenceEquals(value, nullptr) ? new Magick::Color() : value->CreateColor();
		Value->color(*color);
		delete color;
	}
	//==============================================================================================
}