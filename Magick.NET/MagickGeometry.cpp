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
#include "MagickGeometry.h"

namespace ImageMagick
{
	//==============================================================================================
	void MagickGeometry::Initialize(int x, int y, int width, int height, bool isPercentage)
	{
		Value = new Magick::Geometry(width, height, x, y, x < 0, y < 0);
		Value->percent(isPercentage);
	}
	//==============================================================================================
	MagickGeometry::MagickGeometry(Magick::Geometry geometry)
	{
		Value = new Magick::Geometry(geometry);
	}
	//==============================================================================================
	MagickGeometry::MagickGeometry(int width, int height)
	{
		Initialize(0, 0, width, height, false);
	}
	//==============================================================================================
	MagickGeometry::MagickGeometry(Percentage percentageWidth, Percentage percentageHeight)
	{
		Initialize(0, 0, (int)percentageWidth, (int)percentageHeight, true);
	}
	//==============================================================================================
	MagickGeometry::MagickGeometry(int x, int y, int width, int height)
	{
		Initialize(x, y, width, height, false);
	}
	//==============================================================================================
	MagickGeometry::MagickGeometry(int x, int y, Percentage percentageWidth, Percentage percentageHeight)
	{
		Initialize(x, y, (int)percentageWidth, (int)percentageHeight, true);
	}
	//==============================================================================================
	MagickGeometry::MagickGeometry(Rectangle rectangle)
	{
		Initialize(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, false);
	}
	//==============================================================================================
	MagickGeometry::MagickGeometry(String^ value)
	{
		std::string geometrySpec;
		Marshaller::Marshal(value, geometrySpec);
		Value = new Magick::Geometry(geometrySpec);

		Throw::IfFalse("geometry", Value->isValid(), "Invalid geometry specified.");
	}
	//==============================================================================================
	int MagickGeometry::Height::get()
	{
		return Convert::ToInt32(Value->height());
	}
	//==============================================================================================
	void  MagickGeometry::Height::set(int value)
	{
		Value->height(value);
	}
	//==============================================================================================
	bool MagickGeometry::IsPercentage::get()
	{
		return Value->percent();
	}
	//==============================================================================================
	void MagickGeometry::IsPercentage::set(bool value)
	{
		Value->percent(value);
	}
	//==============================================================================================
	int MagickGeometry::Width::get()
	{
		return Convert::ToInt32(Value->width());
	}
	//==============================================================================================
	void MagickGeometry::Width::set(int value)
	{
		Value->width(value);
	}
	//==============================================================================================
	int MagickGeometry::X::get()
	{
		return Convert::ToInt32(Value->xNegative() ? -1 * Value->xOff() : Value->xOff());
	}
	//==============================================================================================
	void MagickGeometry::X::set(int value)
	{
		Value->xOff(value);
		Value->xNegative(value < 0);
	}
	//==============================================================================================
	int MagickGeometry::Y::get()
	{
		return Convert::ToInt32(Value->yNegative() ? -1 * Value->yOff() : Value->yOff());
	}
	//==============================================================================================
	void MagickGeometry::Y::set(int value)
	{
		Value->yOff(value);
		Value->yNegative(value < 0);
	}
	//==============================================================================================
	int MagickGeometry::CompareTo(MagickGeometry^ other)
	{
		if (ReferenceEquals(other, nullptr))
			return 1;

		int left = (this->Width * this->Height);
		int right = (other->Width * other->Height);

		if (left == right)
			return 0;

		return left < right ? -1 : 1;
	}
	//==============================================================================================
	bool MagickGeometry::Equals(Object^ obj)
	{
		if (ReferenceEquals(this, obj))
			return true;

		return Equals(dynamic_cast<MagickGeometry^>(obj));
	}
	//==============================================================================================
	bool MagickGeometry::Equals(MagickGeometry^ other)
	{
		if (ReferenceEquals(other, nullptr))
			return false;

		if (ReferenceEquals(this, other))
			return true;

		return (Magick::operator == (*Value, *other->Value)) ? true : false;
	}
	//==============================================================================================
	int MagickGeometry::GetHashCode()
	{
		return
			Value->width().GetHashCode() ^
			Value->height().GetHashCode() ^
			Value->xOff().GetHashCode() ^
			Value->yOff().GetHashCode() ^
			Value->xNegative().GetHashCode() ^
			Value->yNegative().GetHashCode() ^
			Value->percent().GetHashCode() ^
			Value->aspect().GetHashCode() ^
			Value->greater().GetHashCode() ^
			Value->less().GetHashCode();
	}
	//==============================================================================================
}