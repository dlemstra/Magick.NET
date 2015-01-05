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
#pragma once

#include "Stdafx.h"

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Specifies the built-in kernels.
	///</summary>
	public enum class Kernel
	{
		Undefined = Magick::UndefinedKernel,
		Unity = Magick::UnityKernel,
		Gaussian = Magick::GaussianKernel,
		DoG = Magick::DoGKernel,
		LoG = Magick::LoGKernel,
		Blur = Magick::BlurKernel,
		Comet = Magick::CometKernel,
		Laplacian = Magick::LaplacianKernel,
		Sobel = Magick::SobelKernel,
		FreiChen = Magick::FreiChenKernel,
		Roberts = Magick::RobertsKernel,
		Prewitt = Magick::PrewittKernel,
		Compass = Magick::CompassKernel,
		Kirsch = Magick::KirschKernel,
		Diamond = Magick::DiamondKernel,
		Square = Magick::SquareKernel,
		Rectangle = Magick::RectangleKernel,
		Octagon = Magick::OctagonKernel,
		Disk = Magick::DiskKernel,
		Plus = Magick::PlusKernel,
		Cross = Magick::CrossKernel,
		Ring = Magick::RingKernel,
		Peaks = Magick::PeaksKernel,
		Edges = Magick::EdgesKernel,
		Corners = Magick::CornersKernel,
		Diagonals = Magick::DiagonalsKernel,
		LineEnds = Magick::LineEndsKernel,
		LineJunctions = Magick::LineJunctionsKernel,
		Ridges = Magick::RidgesKernel,
		ConvexHull = Magick::ConvexHullKernel,
		ThinSE = Magick::ThinSEKernel,
		Skeleton = Magick::SkeletonKernel,
		Chebyshev = Magick::ChebyshevKernel,
		Manhattan = Magick::ManhattanKernel,
		Octagonal = Magick::OctagonalKernel,
		Euclidean = Magick::EuclideanKernel,
		UserDefined = Magick::UserDefinedKernel,
		Binomial = Magick::BinomialKernel
	};
	//==============================================================================================
}