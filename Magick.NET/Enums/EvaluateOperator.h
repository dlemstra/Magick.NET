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
	/// Specifies the evaluate operator.
	///</summary>
	public enum class EvaluateOperator
	{
		Undefined = MagickCore::UndefinedEvaluateOperator,
		Add = MagickCore::AddEvaluateOperator,
		And = MagickCore::AndEvaluateOperator,
		Devide = MagickCore::DivideEvaluateOperator,
		LeftShift = MagickCore::LeftShiftEvaluateOperator,
		Max = MagickCore::MaxEvaluateOperator,
		Min = MagickCore::MinEvaluateOperator,
		Multiply = MagickCore::MultiplyEvaluateOperator,
		Or = MagickCore::OrEvaluateOperator,
		Right = MagickCore::RightShiftEvaluateOperator,
		Set = MagickCore::SetEvaluateOperator,
		Subtract = MagickCore::SubtractEvaluateOperator,
		Xor = MagickCore::XorEvaluateOperator,
		Pow = MagickCore::PowEvaluateOperator,
		Log = MagickCore::LogEvaluateOperator,
		Threshold = MagickCore::ThresholdEvaluateOperator,
		ThresholdBlack = MagickCore::ThresholdBlackEvaluateOperator,
		ThresholdWhite = MagickCore::ThresholdWhiteEvaluateOperator,
		GaussianNoise = MagickCore::GaussianNoiseEvaluateOperator,
		ImpulseNoise = MagickCore::ImpulseNoiseEvaluateOperator,
		LaplacianNoise = MagickCore::LaplacianNoiseEvaluateOperator,
		MultiplicativeNoise = MagickCore::MultiplicativeNoiseEvaluateOperator,
		PoissonNoise = MagickCore::PoissonNoiseEvaluateOperator,
		UniformNoise = MagickCore::UniformNoiseEvaluateOperator,
		Cosine = MagickCore::CosineEvaluateOperator,
		Sine = MagickCore::SineEvaluateOperator,
		AddModulus = MagickCore::AddModulusEvaluateOperator,
		Mean = MagickCore::MeanEvaluateOperator,
		Abs = MagickCore::AbsEvaluateOperator,
		Exponential = MagickCore::ExponentialEvaluateOperator,
		Median = MagickCore::MedianEvaluateOperator,
		Sum = MagickCore::SumEvaluateOperator
	};
	//==============================================================================================
}