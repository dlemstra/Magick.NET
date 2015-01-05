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
#include "PointD.h"

namespace ImageMagick
{
	//==============================================================================================
	PointD::PointD(Magick::Point point)
	{
		Initialize(point);
	}
	//==============================================================================================
	void PointD::Initialize(Magick::Point point)
	{
		_X = point.x();
		_Y = point.y();
	}
	//==============================================================================================
	const Magick::Point* PointD::CreatePoint()
	{
		return new Magick::Point(_X, _Y);
	}
	//==============================================================================================
	PointD::PointD(double xy)
	{
		_X = xy;
		_Y = xy;
	}
	//==============================================================================================
	PointD::PointD(double x, double y)
	{
		_X = x;
		_Y = y;
	}
	//==============================================================================================
	PointD::PointD(String^ value)
	{
		Throw::IfNullOrEmpty("value", value);

		std::string pointSpec;
		Marshaller::Marshal(value, pointSpec);

		Magick::Point point = Magick::Point(pointSpec);
		Throw::IfFalse("value", point.isValid(), "Invalid point specified.");

		Initialize(point);
	}
	//==============================================================================================
	double PointD::X::get()
	{
		return _X;
	}
	//==============================================================================================
	void PointD::X::set(double value)
	{
		_X = value;
	}
	//==============================================================================================
	double PointD::Y::get()
	{
		return _Y;
	}
	//==============================================================================================
	void PointD::Y::set(double value)
	{
		_Y = value;
	}
	//==============================================================================================
	bool PointD::operator == (PointD left, PointD right)
	{
		return Object::Equals(left, right);
	}
	//==============================================================================================
	bool PointD::operator != (PointD left, PointD right)
	{
		return !Object::Equals(left, right);
	}
	//==============================================================================================
	bool PointD::Equals(Object^ obj)
	{
		if (obj == nullptr)
			return false;

		if (obj->GetType() != PointD::typeid)
			return false;

		return Equals((PointD)obj);
	}
	//==============================================================================================
	bool PointD::Equals(PointD other)
	{
		return
			X == other.X &&
			Y == other.Y;
	}
	//==============================================================================================
	int PointD::GetHashCode()
	{
		return
			X.GetHashCode() ^
			Y.GetHashCode();
	}
	//==============================================================================================
}