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
#include "DrawableCircle.h"

namespace ImageMagick
{
	//==============================================================================================
	DrawableCircle::DrawableCircle(double originX, double originY, double perimeterX, double perimeterY)
	{
		BaseValue = new Magick::DrawableCircle(originX, originY, perimeterX, perimeterY);
	}
	//==============================================================================================
	double DrawableCircle::OriginX::get()
	{
		return Value->originX();
	}
	//==============================================================================================
	void DrawableCircle::OriginX::set(double value)
	{
		Value->originX(value);
	}
	//==============================================================================================
	double DrawableCircle::OriginY::get()
	{
		return Value->originY();
	}
	//==============================================================================================
	void DrawableCircle::OriginY::set(double value)
	{
		Value->originY(value);
	}
	//==============================================================================================
	double DrawableCircle::PerimeterX::get()
	{
		return Value->perimX();
	}
	//==============================================================================================
	void DrawableCircle::PerimeterX::set(double value)
	{
		Value->perimX(value);
	}
	//==============================================================================================
	double DrawableCircle::PerimeterY::get()
	{
		return Value->perimY();
	}
	//==============================================================================================
	void DrawableCircle::PerimeterY::set(double value)
	{
		Value->perimY(value);
	}
	//==============================================================================================
}