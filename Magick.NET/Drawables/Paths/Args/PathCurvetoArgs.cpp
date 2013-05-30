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
#include "PathCurvetoArgs.h"

namespace ImageMagick
{
	//==============================================================================================
	PathCurvetoArgs::PathCurvetoArgs()
	{
		BaseValue = new Magick::PathCurvetoArgs();
	}
	//==============================================================================================
	PathCurvetoArgs::PathCurvetoArgs(double x, double y, double x1, double y1, double x2, double y2)
	{
		BaseValue = new Magick::PathCurvetoArgs(x, y, x1, y1, x2, y2);
	}
	//==============================================================================================
	double PathCurvetoArgs::X::get()
	{
		return InternalValue->x();
	}
	//==============================================================================================
	void PathCurvetoArgs::X::set(double value)
	{
		InternalValue->x(value);
	}
	//==============================================================================================
	double PathCurvetoArgs::Y::get()
	{
		return InternalValue->y();
	}
	//==============================================================================================
	void PathCurvetoArgs::Y::set(double value)
	{
		InternalValue->y(value);
	}
	//==============================================================================================
	double PathCurvetoArgs::X1::get()
	{
		return InternalValue->x1();
	}
	//==============================================================================================
	void PathCurvetoArgs::X1::set(double value)
	{
		InternalValue->x1(value);
	}
	//==============================================================================================
	double PathCurvetoArgs::Y1::get()
	{
		return InternalValue->y1();
	}
	//==============================================================================================
	void PathCurvetoArgs::Y1::set(double value)
	{
		InternalValue->y1(value);
	}
	//==============================================================================================
	double PathCurvetoArgs::X2::get()
	{
		return InternalValue->x2();
	}
	//==============================================================================================
	void PathCurvetoArgs::X2::set(double value)
	{
		InternalValue->x2(value);
	}
	//==============================================================================================
	double PathCurvetoArgs::Y2::get()
	{
		return InternalValue->y2();
	}
	//==============================================================================================
	void PathCurvetoArgs::Y2::set(double value)
	{
		InternalValue->y2(value);
	}
	//==============================================================================================
}