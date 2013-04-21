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
#pragma once

#include "Stdafx.h"

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// The sparse color methods.
	///</summary>
	public enum class SparseColorMethod
	{
		Undefined = MagickCore::UndefinedColorInterpolate,
		Barycentric = MagickCore::BarycentricColorInterpolate,
		Bilinear = MagickCore::BilinearColorInterpolate,
		Polynomial = MagickCore::PolynomialColorInterpolate,
		Shepards = MagickCore::ShepardsColorInterpolate,
		Voronoi = MagickCore::VoronoiColorInterpolate,
		Inverse = MagickCore::InverseColorInterpolate
	};
	//==============================================================================================
}