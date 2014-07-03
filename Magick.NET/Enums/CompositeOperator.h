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
	/// Specifies the composite operators.
	///</summary>
	public enum class CompositeOperator
	{
		Undefined = Magick::UndefinedCompositeOp,
		NoComposite = Magick::NoCompositeOp,
		ModulusAdd = Magick::ModulusAddCompositeOp,
		Atop = Magick::AtopCompositeOp,
		Blend = Magick::BlendCompositeOp,
		Bumpmap = Magick::BumpmapCompositeOp,
		ChangeMask = Magick::ChangeMaskCompositeOp,
		Clear = Magick::ClearCompositeOp,
		ColorBurn = Magick::ColorBurnCompositeOp,
		ColorDodge = Magick::ColorDodgeCompositeOp,
		Colorize = Magick::ColorizeCompositeOp,
		CopyBlack = Magick::CopyBlackCompositeOp,
		CopyBlue = Magick::CopyBlueCompositeOp,
		Copy = Magick::CopyCompositeOp,
		CopyCyan = Magick::CopyCyanCompositeOp,
		CopyGreen = Magick::CopyGreenCompositeOp,
		CopyMagenta = Magick::CopyMagentaCompositeOp,
		CopyOpacity = Magick::CopyOpacityCompositeOp,
		CopyRed = Magick::CopyRedCompositeOp,
		CopyYellow = Magick::CopyYellowCompositeOp,
		Darken = Magick::DarkenCompositeOp,
		DstAtop = Magick::DstAtopCompositeOp,
		Dst = Magick::DstCompositeOp,
		DstIn = Magick::DstInCompositeOp,
		DstOut = Magick::DstOutCompositeOp,
		DstOver = Magick::DstOverCompositeOp,
		Difference = Magick::DifferenceCompositeOp,
		Displace = Magick::DisplaceCompositeOp,
		Dissolve = Magick::DissolveCompositeOp,
		Exclusion = Magick::ExclusionCompositeOp,
		HardLight = Magick::HardLightCompositeOp,
		Hue = Magick::HueCompositeOp,
		In = Magick::InCompositeOp,
		Lighten = Magick::LightenCompositeOp,
		LinearLight = Magick::LinearLightCompositeOp,
		Luminize = Magick::LuminizeCompositeOp,
		MinusDst = Magick::MinusDstCompositeOp,
		Modulate = Magick::ModulateCompositeOp,
		Multiply = Magick::MultiplyCompositeOp,
		Out = Magick::OutCompositeOp,
		Over = Magick::OverCompositeOp,
		Overlay = Magick::OverlayCompositeOp,
		Plus = Magick::PlusCompositeOp,
		Replace = Magick::ReplaceCompositeOp,
		Saturate = Magick::SaturateCompositeOp,
		Screen = Magick::ScreenCompositeOp,
		SoftLight = Magick::SoftLightCompositeOp,
		SrcAtop = Magick::SrcAtopCompositeOp,
		Src = Magick::SrcCompositeOp,
		SrcIn = Magick::SrcInCompositeOp,
		SrcOut = Magick::SrcOutCompositeOp,
		SrcOver = Magick::SrcOverCompositeOp,
		Modulus = Magick::ModulusSubtractCompositeOp,
		Threshold = Magick::ThresholdCompositeOp,
		Xor = Magick::XorCompositeOp,
		Divide = Magick::DivideDstCompositeOp,
		Distort = Magick::DistortCompositeOp,
		Blur = Magick::BlurCompositeOp,
		PegtopLight = Magick::PegtopLightCompositeOp,
		VividLight = Magick::VividLightCompositeOp,
		PinLight = Magick::PinLightCompositeOp,
		LinearDodge = Magick::LinearDodgeCompositeOp,
		LinearBurn = Magick::LinearBurnCompositeOp,
		Mathematics = Magick::MathematicsCompositeOp,
		DivideSrc = Magick::DivideSrcCompositeOp,
		MinusSrc = Magick::MinusSrcCompositeOp,
		DarkenIntensity = Magick::DarkenIntensityCompositeOp,
		LightenIntensity = Magick::LightenIntensityCompositeOp,
		HardMix = Magick::HardMixCompositeOp
	};
	//==============================================================================================
}