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
#include "DrawableRoundRectangle.h"

namespace ImageMagick
{
	//==============================================================================================
	DrawableRoundRectangle::DrawableRoundRectangle(double centerX, double centerY, double width, 
		double height, double cornerWidth, double cornerHeight)
	{
		BaseValue = new Magick::DrawableRoundRectangle(centerX, centerY, width, height,
			cornerWidth, cornerHeight);
	}
	//==============================================================================================
	double DrawableRoundRectangle::CenterX::get()
	{
		return Value->centerX();
	}
	//==============================================================================================
	void DrawableRoundRectangle::CenterX::set(double value)
	{
		Value->centerX(value);
	}
	//==============================================================================================
	double DrawableRoundRectangle::CenterY::get()
	{
		return Value->centerY();
	}
	//==============================================================================================
	void DrawableRoundRectangle::CenterY::set(double value)
	{
		Value->centerY(value);
	}
	//==============================================================================================
	double DrawableRoundRectangle::CornerHeight::get()
	{
		return Value->cornerHeight();
	}
	//==============================================================================================
	void DrawableRoundRectangle::CornerHeight::set(double value)
	{
		Value->cornerHeight(value);
	}
	//==============================================================================================
	double DrawableRoundRectangle::CornerWidth::get()
	{
		return Value->cornerWidth();
	}
	//==============================================================================================
	void DrawableRoundRectangle::CornerWidth::set(double value)
	{
		Value->cornerWidth(value);
	}
	//==============================================================================================
	double DrawableRoundRectangle::Height::get()
	{
		return Value->hight();
	}
	//==============================================================================================
	void DrawableRoundRectangle::Height::set(double value)
	{
		Value->hight(value);
	}
	//==============================================================================================
	double DrawableRoundRectangle::Width::get()
	{
		return Value->width();
	}
	//==============================================================================================
	void DrawableRoundRectangle::Width::set(double value)
	{
		Value->width(value);
	}
	//==============================================================================================
}