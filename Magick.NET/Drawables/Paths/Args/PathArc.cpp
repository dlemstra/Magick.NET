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
#include "PathArc.h"

namespace ImageMagick
{
	//==============================================================================================
	PathArc::PathArc()
	{
		BaseValue = new Magick::PathArcArgs();
	}
	//==============================================================================================
	PathArc::PathArc(double x, double y, double radiusX, double radiusY, double rotationX,
		bool useLargeArc, bool useSweep)
	{
		BaseValue = new Magick::PathArcArgs(radiusX, radiusY, rotationX, useLargeArc, useSweep, x, y);
	}
	//==============================================================================================
	double PathArc::RadiusX::get()
	{
		return InternalValue->radiusX();
	}
	//==============================================================================================
	void PathArc::RadiusX::set(double value)
	{
		InternalValue->radiusX(value);
	}
	//==============================================================================================
	double PathArc::RadiusY::get()
	{
		return InternalValue->radiusY();
	}
	//==============================================================================================
	void PathArc::RadiusY::set(double value)
	{
		InternalValue->radiusY(value);
	}
	//==============================================================================================
	double PathArc::RotationX::get()
	{
		return InternalValue->xAxisRotation();
	}
	//==============================================================================================
	void PathArc::RotationX::set(double value)
	{
		InternalValue->xAxisRotation(value);
	}
	//==============================================================================================
	bool PathArc::UseLargeArc::get()
	{
		return InternalValue->largeArcFlag();
	}
	//==============================================================================================
	void PathArc::UseLargeArc::set(bool value)
	{
		InternalValue->largeArcFlag(value);
	}
	//==============================================================================================
	bool PathArc::UseSweep::get()
	{
		return InternalValue->sweepFlag();
	}
	//==============================================================================================
	void PathArc::UseSweep::set(bool value)
	{
		InternalValue->sweepFlag(value);
	}
	//==============================================================================================
	double PathArc::X::get()
	{
		return InternalValue->x();
	}
	//==============================================================================================
	void PathArc::X::set(double value)
	{
		InternalValue->y(value);
	}
	//==============================================================================================
	double PathArc::Y::get()
	{
		return InternalValue->y();
	}
	//==============================================================================================
	void PathArc::Y::set(double value)
	{
		InternalValue->y(value);
	}
	//==============================================================================================
}