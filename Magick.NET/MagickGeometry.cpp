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
#include "stdafx.h"
#include "MagickGeometry.h"

namespace ImageMagick
{
	//==============================================================================================
	void MagickGeometry::Initialize(long xOffset, long yOffset, int width, int height, bool isPercentage)
	{
		Throw::IfTrue("width", isPercentage && width <= 0, "Invalid percentage specified.");
		Throw::IfTrue("height", isPercentage && height <= 0, "Invalid percentage specified.");

		Value = new Magick::Geometry(width, height, xOffset, yOffset, xOffset < 0, yOffset < 0);
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
	MagickGeometry::MagickGeometry(int width, int height, bool isPercentage)
	{
		Initialize(0, 0, width, height, isPercentage);
	}
	//==============================================================================================
	MagickGeometry::MagickGeometry(long xOffset, long yOffset, int width, int height)
	{
		Initialize(xOffset, yOffset, width, height, false);
	}
	//==============================================================================================
	MagickGeometry::MagickGeometry(long xOffset, long yOffset, int width, int height, bool isPercentage)
	{
		Initialize(xOffset, yOffset, width, height, isPercentage);
	}
	//==============================================================================================
	MagickGeometry::MagickGeometry(Rectangle^ rectangle)
	{
		Initialize(rectangle->X, rectangle->Y, rectangle->Width, rectangle->Height, false);
	}
	//==============================================================================================
	MagickGeometry^ MagickGeometry::FromRectangle(Rectangle^ rectangle)
	{
		if (rectangle == nullptr)
			return nullptr;

		return gcnew MagickGeometry(rectangle);
	}
	//==============================================================================================
}