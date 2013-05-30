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
#include "PathQuadraticCurvetoArgs.h"

namespace ImageMagick
{
	//==============================================================================================
	PathQuadraticCurvetoArgs::PathQuadraticCurvetoArgs()
	{
		BaseValue = new Magick::PathQuadraticCurvetoArgs();
	}
	//==============================================================================================
	PathQuadraticCurvetoArgs::PathQuadraticCurvetoArgs(double x, double y, double x1, double y1)
	{
		BaseValue = new Magick::PathQuadraticCurvetoArgs(x, y, x1, y1);
	}
	//==============================================================================================
	double PathQuadraticCurvetoArgs::X::get()
	{
		return InternalValue->x();
	}
	//==============================================================================================
	void PathQuadraticCurvetoArgs::X::set(double value)
	{
		InternalValue->x(value);
	}
	//==============================================================================================
	double PathQuadraticCurvetoArgs::Y::get()
	{
		return InternalValue->y();
	}
	//==============================================================================================
	void PathQuadraticCurvetoArgs::Y::set(double value)
	{
		InternalValue->y(value);
	}
	//==============================================================================================
	double PathQuadraticCurvetoArgs::X1::get()
	{
		return InternalValue->x1();
	}
	//==============================================================================================
	void PathQuadraticCurvetoArgs::X1::set(double value)
	{
		InternalValue->x1(value);
	}
	//==============================================================================================
	double PathQuadraticCurvetoArgs::Y1::get()
	{
		return InternalValue->y1();
	}
	//==============================================================================================
	void PathQuadraticCurvetoArgs::Y1::set(double value)
	{
		InternalValue->y1(value);
	}
	//==============================================================================================
}