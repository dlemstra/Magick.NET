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
#include "DrawableEllipse.h"

namespace ImageMagick
{
	//==============================================================================================
	DrawableEllipse::DrawableEllipse(double originX, double originY, double radiusX, double radiusY, 
		double startDegrees, double endDegrees)
	{
		BaseValue = new Magick::DrawableEllipse(originX, originY, radiusX, radiusY, startDegrees, endDegrees);
	}
	//==============================================================================================
	double DrawableEllipse::EndDegrees::get()
	{
		return Value->arcEnd();
	}
	void DrawableEllipse::EndDegrees::set(double value)
	{
		Value->arcEnd(value);
	}
	//==============================================================================================
	double DrawableEllipse::OriginX::get()
	{
		return Value->originX();
	}
	//==============================================================================================
	void DrawableEllipse::OriginX::set(double value)
	{
		Value->originX(value);
	}
	//==============================================================================================
	double DrawableEllipse::OriginY::get()
	{
		return Value->originY();
	}
	//==============================================================================================
	void DrawableEllipse::OriginY::set(double value)
	{
		Value->originY(value);
	}
	//==============================================================================================
	double DrawableEllipse::RadiusX::get()
	{
		return Value->radiusX();
	}
	//==============================================================================================
	void DrawableEllipse::RadiusX::set(double value)
	{
		Value->radiusX(value);
	}
	//==============================================================================================
	double DrawableEllipse::RadiusY::get()
	{
		return Value->radiusY();
	}
	//==============================================================================================
	void DrawableEllipse::RadiusY::set(double value)
	{
		Value->radiusY(value);
	}
	//==============================================================================================
	double DrawableEllipse::StartDegrees::get()
	{
		return Value->arcStart();
	}
	//==============================================================================================
	void DrawableEllipse::StartDegrees::set(double value)
	{
		Value->arcStart(value);
	}
	//==============================================================================================
}