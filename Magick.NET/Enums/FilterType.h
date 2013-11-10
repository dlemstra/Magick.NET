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
		Undefined = Magick::UndefinedFilter,
		Point = Magick::PointFilter,
		Box = Magick::BoxFilter,
		Triangle = Magick::TriangleFilter,
		Hermite = Magick::HermiteFilter,
		Hanning = Magick::HanningFilter,
		Hamming = Magick::HammingFilter,
		Blackman = Magick::BlackmanFilter,
		Gaussian = Magick::GaussianFilter,
		Quadratic = Magick::QuadraticFilter,
		Cubic = Magick::CubicFilter,
		Catrom = Magick::CatromFilter,
		Mitchell = Magick::MitchellFilter,
		Jinc = Magick::JincFilter,
		Sinc = Magick::SincFilter,
		SincFast = Magick::SincFastFilter,
		Kaiser = Magick::KaiserFilter,
		Welsh = Magick::WelshFilter,
		Parzen = Magick::ParzenFilter,
		Bohman = Magick::BohmanFilter,
		Bartlett = Magick::BartlettFilter,
		Lagrange = Magick::LagrangeFilter,
		Lanczos = Magick::LanczosFilter,
		LanczosSharp = Magick::LanczosSharpFilter,
		Lanczos2 = Magick::Lanczos2Filter,
		Lanczos2Sharp = Magick::Lanczos2SharpFilter,
		Robidoux = Magick::RobidouxFilter,
		RobidouxSharp = Magick::RobidouxSharpFilter,
		Cosine = Magick::CosineFilter,
		Spline = Magick::SplineFilter,
		LanczosRadius = Magick::LanczosRadiusFilter
	};
	//==============================================================================================
}