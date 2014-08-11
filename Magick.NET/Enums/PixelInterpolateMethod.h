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
#pragma once

#include "Stdafx.h"

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Pixel color interpolate methods.
	///</summary>
	public enum class PixelInterpolateMethod
	{
		Undefined = Magick::UndefinedInterpolatePixel,
		Average = Magick::AverageInterpolatePixel,
		Average9 = Magick::Average9InterpolatePixel,
		Average16 = Magick::Average16InterpolatePixel,
		Background = Magick::BackgroundInterpolatePixel,
		Bilinear = Magick::BilinearInterpolatePixel,
		Blend = Magick::BlendInterpolatePixel,
		Catrom = Magick::CatromInterpolatePixel,
		Integer = Magick::IntegerInterpolatePixel,
		Mesh = Magick::MeshInterpolatePixel,
		Nearest = Magick::NearestInterpolatePixel,
		Spline = Magick::SplineInterpolatePixel
	};
	//==============================================================================================
}