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
	/// Specifies distortion methods.
	///</summary>
	public enum class DistortMethod
	{
		Undefined = Magick::UndefinedDistortion,
		Affine = Magick::AffineDistortion,
		AffineProjection = Magick::AffineProjectionDistortion,
		ScaleRotateTranslate = Magick::ScaleRotateTranslateDistortion,
		Perspective = Magick::PerspectiveDistortion,
		PerspectiveProjection = Magick::PerspectiveProjectionDistortion,
		BilinearForward = Magick::BilinearForwardDistortion,
		Bilinear = BilinearForward,
		BilinearReverse = Magick::BilinearReverseDistortion,
		Polynomial = Magick::PolynomialDistortion,
		Arc = Magick::ArcDistortion,
		Polar = Magick::PolarDistortion,
		DePolar = Magick::DePolarDistortion,
		Cylinder2Plane = Magick::Cylinder2PlaneDistortion,
		Plane2Cylinder = Magick::Plane2CylinderDistortion,
		Barrel = Magick::BarrelDistortion,
		BarrelInverse = Magick::BarrelInverseDistortion,
		Shepards = Magick::ShepardsDistortion,
		Resize = Magick::ResizeDistortion,
		Sentinel = Magick::SentinelDistortion
	};
	//==============================================================================================
}