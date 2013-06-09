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
#include "DrawableRectangle.h"

namespace ImageMagick
{
	//==============================================================================================
	void DrawableRectangle::Initialize(double upperLeftX, double upperLeftY, double lowerRightX,
		double lowerRightY)
	{
		BaseValue = new Magick::DrawableRectangle(upperLeftX, upperLeftY, lowerRightX, lowerRightY);
	}
	//==============================================================================================
	DrawableRectangle::DrawableRectangle(double upperLeftX, double upperLeftY, double lowerRightX,
		double lowerRightY)
	{
		Initialize(upperLeftX, upperLeftY, lowerRightX, lowerRightY);
	}
	//==============================================================================================
	DrawableRectangle::DrawableRectangle(Rectangle rectangle)
	{
		Initialize(rectangle.X, rectangle.Y, rectangle.X + rectangle.Width,
			rectangle.Y + rectangle.Height);
	}
	//==============================================================================================
	double DrawableRectangle::LowerRightX::get()
	{
		return Value->lowerRightX();
	}
	//==============================================================================================
	void DrawableRectangle::LowerRightX::set(double value)
	{
		Value->lowerRightX(value);
	}
	//==============================================================================================
	double DrawableRectangle::LowerRightY::get()
	{
		return Value->lowerRightY();
	}
	//==============================================================================================
	void DrawableRectangle::LowerRightY::set(double value)
	{
		Value->lowerRightY(value);
	}
	//==============================================================================================
	double DrawableRectangle::UpperLeftX::get()
	{
		return Value->upperLeftX();
	}
	//==============================================================================================
	void DrawableRectangle::UpperLeftX::set(double value)
	{
		Value->upperLeftX(value);
	}
	//==============================================================================================
	double DrawableRectangle::UpperLeftY::get()
	{
		return Value->upperLeftY();
	}
	//==============================================================================================
	void DrawableRectangle::UpperLeftY::set(double value)
	{
		Value->upperLeftY(value);
	}
	//==============================================================================================
}