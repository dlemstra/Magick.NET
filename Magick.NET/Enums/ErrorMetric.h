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
	/// Specifies the error metric types.
	///</summary>
	public enum class ErrorMetric
	{
		Undefined = Magick::UndefinedErrorMetric,
		Absolute = Magick::AbsoluteErrorMetric,
		Fuzz = Magick::FuzzErrorMetric,
		MeanAbsolute = Magick::MeanAbsoluteErrorMetric,
		MeanErrorPerPixel = Magick::MeanErrorPerPixelErrorMetric,
		MeanSquared = Magick::MeanSquaredErrorMetric,
		NormalizedCrossCorrelation = Magick::NormalizedCrossCorrelationErrorMetric,
		PeakAbsolute = Magick::PeakAbsoluteErrorMetric,
		PeakSignalToNoiseRatio = Magick::PeakSignalToNoiseRatioErrorMetric,
		PerceptualHash = Magick::PerceptualHashErrorMetric,
		RootMeanSquared = Magick::RootMeanSquaredErrorMetric
	};
	//==============================================================================================
}