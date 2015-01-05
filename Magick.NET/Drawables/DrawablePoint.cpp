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
#include "DrawablePoint.h"

namespace ImageMagick
{
	//==============================================================================================
	DrawablePoint::DrawablePoint(double x, double y)
	{
		BaseValue = new Magick::DrawablePoint(x, y);
	}
	//==============================================================================================
	double DrawablePoint::X::get()
	{
		return Value->x();
	}
	//==============================================================================================
	void DrawablePoint::X::set(double value)
	{
		Value->x(value);
	}
	//==============================================================================================
	double DrawablePoint::Y::get()
	{
		return Value->y();
	}
	//==============================================================================================
	void DrawablePoint::Y::set(double value)
	{
		Value->y(value);
	}
	//==============================================================================================
}