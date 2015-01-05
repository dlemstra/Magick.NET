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
#include "DrawableArc.h"

namespace ImageMagick
{
	//==============================================================================================
	DrawableArc::DrawableArc(double startX, double startY, double endX, double endY, 
		double startDegrees, double endDegrees)
	{
		BaseValue = new Magick::DrawableArc(startX, startY, endX, endY, startDegrees, endDegrees);
	}
	//==============================================================================================
	double DrawableArc::EndDegrees::get()
	{
		return Value->endDegrees();
	}
	//==============================================================================================
	void DrawableArc::EndDegrees::set(double value)
	{
		Value->endDegrees(value);
	}
	//==============================================================================================
	double DrawableArc::EndX::get()
	{
		return Value->endX();
	}
	//==============================================================================================
	void DrawableArc::EndX::set(double value)
	{
		Value->endX(value);
	}
	//==============================================================================================
	double DrawableArc::EndY::get()
	{
		return Value->endY();
	}
	//==============================================================================================
	void DrawableArc::EndY::set(double value)
	{
		Value->endY(value);
	}
	//==============================================================================================
	double DrawableArc::StartDegrees::get()
	{
		return Value->startDegrees();
	}
	//==============================================================================================
	void DrawableArc::StartDegrees::set(double value)
	{
		Value->startDegrees(value);
	}
	//==============================================================================================
	double DrawableArc::StartX::get()
	{
		return Value->startX();
	}
	//==============================================================================================
	void DrawableArc::StartX::set(double value)
	{
		Value->startX(value);
	}
	//==============================================================================================
	double DrawableArc::StartY::get()
	{
		return Value->startY();
	}
	//==============================================================================================
	void DrawableArc::StartY::set(double value)
	{
		Value->startY(value);
	}
	//==============================================================================================
}