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
	/// Specifies the filter types.
	///</summary>
	public enum class FilterType
	{
		Undefined = MagickCore::UndefinedFilter,
		Point = MagickCore::PointFilter,
		Box = MagickCore::BoxFilter,
		Triangle = MagickCore::TriangleFilter,
		Hermite = MagickCore::HermiteFilter,
		Hanning = MagickCore::HanningFilter,
		Hamming = MagickCore::HammingFilter,
		Blackman = MagickCore::BlackmanFilter,
		Gaussian = MagickCore::GaussianFilter,
		Quadratic = MagickCore::QuadraticFilter,
		Cubic = MagickCore::CubicFilter,
		Catrom = MagickCore::CatromFilter,
		Mitchell = MagickCore::MitchellFilter,
		Jinc = MagickCore::JincFilter,
		Sinc = MagickCore::SincFilter,
		SincFast = MagickCore::SincFastFilter,
		Kaiser = MagickCore::KaiserFilter,
		Welsh = MagickCore::WelshFilter,
		Parzen = MagickCore::ParzenFilter,
		Bohman = MagickCore::BohmanFilter,
		Bartlett = MagickCore::BartlettFilter,
		Lagrange = MagickCore::LagrangeFilter,
		Lanczos = MagickCore::LanczosFilter,
		LanczosSharp = MagickCore::LanczosSharpFilter,
		Lanczos2 = MagickCore::Lanczos2Filter,
		Lanczos2Sharp = MagickCore::Lanczos2SharpFilter,
		Robidoux = MagickCore::RobidouxFilter,
		RobidouxSharp = MagickCore::RobidouxSharpFilter,
		Cosine = MagickCore::CosineFilter,
		Spline = MagickCore::SplineFilter,
		LanczosRadius = MagickCore::LanczosRadiusFilter
	};
	//==============================================================================================
}