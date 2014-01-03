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
#include "MagickGeometry.h"

namespace ImageMagick
{
	//==============================================================================================
	void MagickGeometry::Initialize(Magick::Geometry geometry)
	{
		X =  (int)(geometry.xNegative() ? -geometry.xOff() : geometry.xOff());
		Y =  (int)(geometry.yNegative() ? -geometry.yOff() : geometry.yOff());
		Width = (int)geometry.width();
		Height = (int)geometry.height();
		IsPercentage = geometry.percent();
		IgnoreAspectRatio = geometry.aspect();
		Less = geometry.less();
		Greater = geometry.greater();
		FillArea = geometry.fillArea();
		LimitPixels = geometry.limitPixels();
	}
	//==============================================================================================
	void MagickGeometry::Initialize(int x, int y, int width, int height, bool isPercentage)
	{
		X = x;
		Y = y;
		Width = width;
		Height = height;
		IsPercentage = isPercentage;
	}
	//==============================================================================================
	MagickGeometry::MagickGeometry(Magick::Geometry geometry)
	{
		Initialize(geometry);
	}
	//==============================================================================================
	const Magick::Geometry* MagickGeometry::CreateGeometry()
	{
		Magick::Geometry* result = new Magick::Geometry(Width, Height, Math::Abs(X), Math::Abs(Y), X < 0, Y < 0);
		result->percent(IsPercentage);
		result->aspect(IgnoreAspectRatio);
		result->less(Less);
		result->greater(Greater);
		result->fillArea(FillArea);
		result->limitPixels(LimitPixels);

		return result;
	}
	//==============================================================================================
	MagickGeometry::MagickGeometry(int width, int height)
	{
		Initialize(0, 0, width, height, false);
	}
	//==============================================================================================
	MagickGeometry::MagickGeometry(Percentage percentageWidth, Percentage percentageHeight)
	{
		Throw::IfNegative("percentageWidth", percentageWidth);
		Throw::IfNegative("percentageHeight", percentageHeight);

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
		Throw::IfNegative("percentageWidth", percentageWidth);
		Throw::IfNegative("percentageHeight", percentageHeight);

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
		Throw::IfNullOrEmpty("value", value);

		std::string geometrySpec;
		Marshaller::Marshal(value, geometrySpec);

		Magick::Geometry geometry = Magick::Geometry(geometrySpec);
		Throw::IfFalse("geometry", geometry.isValid(), "Invalid geometry specified.");

		Initialize(geometry);
	}
	//==============================================================================================
	bool MagickGeometry::operator == (MagickGeometry^ left, MagickGeometry^ right)
	{
		return Object::Equals(left, right);
	}
	//==============================================================================================
	bool MagickGeometry::operator != (MagickGeometry^ left, MagickGeometry^ right)
	{
		return !Object::Equals(left, right);
	}
	//==============================================================================================
	bool MagickGeometry::operator > (MagickGeometry^ left, MagickGeometry^ right)
	{
		if (ReferenceEquals(left, nullptr))
			return ReferenceEquals(right, nullptr);

		return left->CompareTo(right) == 1;
	}
	//==============================================================================================
	bool MagickGeometry::operator < (MagickGeometry^ left, MagickGeometry^ right)
	{
		if (ReferenceEquals(left, nullptr))
			return !ReferenceEquals(right, nullptr);

		return left->CompareTo(right) == -1;
	}
	//==============================================================================================
	bool MagickGeometry::operator >= (MagickGeometry^ left, MagickGeometry^ right)
	{
		if (ReferenceEquals(left, nullptr))
			return ReferenceEquals(right, nullptr);

		return left->CompareTo(right) >= 0;
	}
	//==============================================================================================
	bool MagickGeometry::operator <= (MagickGeometry^ left, MagickGeometry^ right)
	{
		if (ReferenceEquals(left, nullptr))
			return !ReferenceEquals(right, nullptr);

		return left->CompareTo(right) <= 0;
	}
	//==============================================================================================
	MagickGeometry::operator MagickGeometry^(Rectangle rectangle)
	{
		return gcnew MagickGeometry(rectangle);
	}
	//==============================================================================================
	MagickGeometry::operator MagickGeometry^(String^ geometry)
	{
		return gcnew MagickGeometry(geometry);
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

		return
			Width == other->Width &&
			Height == other->Height && 
			X == other->X &&
			Y == other->Y &&
			IsPercentage == other->IsPercentage &&
			IgnoreAspectRatio == other->IgnoreAspectRatio &&
			Less == other->Less &&
			Greater == other->Greater &&
			FillArea == other->FillArea &&
			LimitPixels == other->LimitPixels;
	}
	//==============================================================================================
	int MagickGeometry::GetHashCode()
	{
		return
			Width.GetHashCode() ^
			Height.GetHashCode() ^
			X.GetHashCode() ^
			Y.GetHashCode() ^
			IsPercentage.GetHashCode() ^
			IgnoreAspectRatio.GetHashCode() ^
			Less.GetHashCode() ^
			Greater.GetHashCode() ^
			FillArea.GetHashCode() ^
			LimitPixels.GetHashCode();
	}
	//==============================================================================================
}