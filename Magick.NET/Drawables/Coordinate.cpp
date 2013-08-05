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
#include "Coordinate.h"

namespace ImageMagick
{
	//==============================================================================================
	Coordinate::Coordinate(double x, double y)
	{
		X = x;
		Y = y;
	}
	//==============================================================================================
	bool Coordinate::operator == (Coordinate left, Coordinate right)
	{
		return Object::Equals(left, right);
	}
	//==============================================================================================
	bool Coordinate::operator != (Coordinate left, Coordinate right)
	{
		return !Object::Equals(left, right);
	}
	//==============================================================================================
	bool Coordinate::Equals(Object^ obj)
	{
		if (obj == nullptr)
			return false;

		if (obj->GetType() == Coordinate::typeid)
			return Equals((Coordinate)obj);

		return false;
	}
	//==============================================================================================
	bool Coordinate::Equals(Coordinate coordinate)
	{
		return
			X.Equals(coordinate.X) &&
			Y.Equals(coordinate.Y);
	}
	//==============================================================================================
	int Coordinate::GetHashCode()
	{
		return X.GetHashCode() ^ Y.GetHashCode();
	}
	//==============================================================================================
}