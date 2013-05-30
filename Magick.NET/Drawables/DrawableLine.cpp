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
#include "DrawableLine.h"

namespace ImageMagick
{
	//==============================================================================================
	DrawableLine::DrawableLine(double startX, double startY, double endX, double endY)
	{
		BaseValue = new Magick::DrawableLine(startX, startY, endX, endY);
	}
	//==============================================================================================
	double DrawableLine::EndX::get()
	{
		return Value->endX();
	}
	//==============================================================================================
	void DrawableLine::EndX::set(double value)
	{
		Value->endX(value);
	}
	//==============================================================================================
	double DrawableLine::EndY::get()
	{
		return Value->endY();
	}
	//==============================================================================================
	void DrawableLine::EndY::set(double value)
	{
		Value->endY(value);
	}
	//==============================================================================================
	double DrawableLine::StartX::get()
	{
		return Value->startX();
	}
	//==============================================================================================
	void DrawableLine::StartX::set(double value)
	{
		Value->startX(value);
	}
	//==============================================================================================
	double DrawableLine::StartY::get()
	{
		return Value->startY();
	}
	//==============================================================================================
	void DrawableLine::StartY::set(double value)
	{
		Value->startY(value);
	}
	//==============================================================================================
}