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
#include "DrawableMatte.h"

namespace ImageMagick
{
	//==============================================================================================
	DrawableMatte::DrawableMatte(double x, double y, ImageMagick::PaintMethod paintMethod)
	{
		BaseValue = new Magick::DrawableMatte(x, y, (Magick::PaintMethod)paintMethod);
	}
	PaintMethod DrawableMatte::PaintMethod::get()
	{
		return (ImageMagick::PaintMethod)Value->paintMethod();
	}
	//==============================================================================================
	void DrawableMatte::PaintMethod::set(ImageMagick::PaintMethod value)
	{
		Value->paintMethod((Magick::PaintMethod)value);
	}
	//==============================================================================================
	double DrawableMatte::X::get()
	{
		return Value->x();
	}
	//==============================================================================================
	void DrawableMatte::X::set(double value)
	{
		Value->x(value);
	}
	//==============================================================================================
	double DrawableMatte::Y::get()
	{
		return Value->y();
	}
	//==============================================================================================
	void DrawableMatte::Y::set(double value)
	{
		Value->y(value);
	}
	//==============================================================================================
}