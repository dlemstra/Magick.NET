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
	/// Specifies the morphology methods.
	///</summary>
	public enum class MorphologyMethod
	{
		Undefined = MagickCore::UndefinedMorphology,
		Convolve = MagickCore::ConvolveMorphology,
		Correlate = MagickCore::CorrelateMorphology,
		Erode = MagickCore::ErodeMorphology,
		Dilate = MagickCore::DilateMorphology,
		ErodeIntensity = MagickCore::ErodeIntensityMorphology,
		DilateIntensity = MagickCore::DilateIntensityMorphology,
		Distance = MagickCore::DistanceMorphology,
		Open = MagickCore::OpenMorphology,
		Close = MagickCore::CloseMorphology,
		OpenIntensity = MagickCore::OpenIntensityMorphology,
		CloseIntensity = MagickCore::CloseIntensityMorphology,
		Smooth = MagickCore::SmoothMorphology,
		EdgeIn = MagickCore::EdgeInMorphology,
		EdgeOut = MagickCore::EdgeOutMorphology,
		Edge = MagickCore::EdgeMorphology,
		TopHat = MagickCore::TopHatMorphology,
		BottomHat = MagickCore::BottomHatMorphology,
		HitAndMiss = MagickCore::HitAndMissMorphology,
		Thinning = MagickCore::ThinningMorphology,
		Thicken = MagickCore::ThickenMorphology,
		Voronoi = MagickCore::VoronoiMorphology,
		IterativeDistance = MagickCore::IterativeDistanceMorphology,
	};
	//==============================================================================================
}