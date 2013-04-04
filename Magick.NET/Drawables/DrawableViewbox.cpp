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
#include "DrawableViewbox.h"

namespace ImageMagick
{
	//==============================================================================================
	void DrawableViewbox::Initialize(double upperLeftX, double upperLeftY, double lowerRightX,
		double lowerRightY)
	{
		Value = new Magick::DrawableViewbox((::size_t)upperLeftX, (::size_t)upperLeftY,
			(::size_t)lowerRightX, (::size_t)lowerRightY);
	}
	//==============================================================================================
	DrawableViewbox::DrawableViewbox(double upperLeftX, double upperLeftY, double lowerRightX,
		double lowerRightY)
	{
		Initialize(upperLeftX, upperLeftY, lowerRightX, lowerRightY);
	}
	//==============================================================================================
	DrawableViewbox::DrawableViewbox(Rectangle^ rectangle)
	{
		Initialize(rectangle->X, rectangle->Y, rectangle->X + rectangle->Width,
			rectangle->Y + rectangle->Height);
	}
	//==============================================================================================
}