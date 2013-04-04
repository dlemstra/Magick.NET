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
	/// Specifies the composite operators.
	///</summary>
	public enum class CompositeOperator
	{
		Undefined = MagickCore::UndefinedCompositeOp,
		NoComposite = MagickCore::NoCompositeOp,
		ModulusAdd = MagickCore::ModulusAddCompositeOp,
		Atop = MagickCore::AtopCompositeOp,
		Blend = MagickCore::BlendCompositeOp,
		Bumpmap = MagickCore::BumpmapCompositeOp,
		ChangeMask = MagickCore::ChangeMaskCompositeOp,
		Clear = MagickCore::ClearCompositeOp,
		ColorBurn = MagickCore::ColorBurnCompositeOp,
		ColorDodge = MagickCore::ColorDodgeCompositeOp,
		Colorize = MagickCore::ColorizeCompositeOp,
		CopyBlack = MagickCore::CopyBlackCompositeOp,
		CopyBlue = MagickCore::CopyBlueCompositeOp,
		Copy = MagickCore::CopyCompositeOp,
		CopyCyan = MagickCore::CopyCyanCompositeOp,
		CopyGreen = MagickCore::CopyGreenCompositeOp,
		CopyMagenta = MagickCore::CopyMagentaCompositeOp,
		CopyOpacity = MagickCore::CopyOpacityCompositeOp,
		CopyRed = MagickCore::CopyRedCompositeOp,
		CopyYellow = MagickCore::CopyYellowCompositeOp,
		Darken = MagickCore::DarkenCompositeOp,
		DstAtop = MagickCore::DstAtopCompositeOp,
		Dst = MagickCore::DstCompositeOp,
		DstIn = MagickCore::DstInCompositeOp,
		DstOut = MagickCore::DstOutCompositeOp,
		DstOver = MagickCore::DstOverCompositeOp,
		Difference = MagickCore::DifferenceCompositeOp,
		Displace = MagickCore::DisplaceCompositeOp,
		Dissolve = MagickCore::DissolveCompositeOp,
		Exclusion = MagickCore::ExclusionCompositeOp,
		HardLight = MagickCore::HardLightCompositeOp,
		Hue = MagickCore::HueCompositeOp,
		In = MagickCore::InCompositeOp,
		Lighten = MagickCore::LightenCompositeOp,
		LinearLight = MagickCore::LinearLightCompositeOp,
		Luminize = MagickCore::LuminizeCompositeOp,
		MinusDst = MagickCore::MinusDstCompositeOp,
		Modulate = MagickCore::ModulateCompositeOp,
		Multiply = MagickCore::MultiplyCompositeOp,
		Out = MagickCore::OutCompositeOp,
		Over = MagickCore::OverCompositeOp,
		Overlay = MagickCore::OverlayCompositeOp,
		Plus = MagickCore::PlusCompositeOp,
		Replace = MagickCore::ReplaceCompositeOp,
		Saturate = MagickCore::SaturateCompositeOp,
		Screen = MagickCore::ScreenCompositeOp,
		SoftLight = MagickCore::SoftLightCompositeOp,
		SrcAtop = MagickCore::SrcAtopCompositeOp,
		Src = MagickCore::SrcCompositeOp,
		SrcIn = MagickCore::SrcInCompositeOp,
		SrcOut = MagickCore::SrcOutCompositeOp,
		SrcOver = MagickCore::SrcOverCompositeOp,
		Modulus = MagickCore::ModulusSubtractCompositeOp,
		Threshold = MagickCore::ThresholdCompositeOp,
		Xor = MagickCore::XorCompositeOp,
		Divide = MagickCore::DivideDstCompositeOp,
		Distort = MagickCore::DistortCompositeOp,
		Blur = MagickCore::BlurCompositeOp,
		PegtopLight = MagickCore::PegtopLightCompositeOp,
		VividLight = MagickCore::VividLightCompositeOp,
		PinLight = MagickCore::PinLightCompositeOp,
		LinearDodge = MagickCore::LinearDodgeCompositeOp,
		LinearBurn = MagickCore::LinearBurnCompositeOp,
		Mathematics = MagickCore::MathematicsCompositeOp,
		DivideSrc = MagickCore::DivideSrcCompositeOp,
		MinusSrc = MagickCore::MinusSrcCompositeOp,
		DarkenIntensity = MagickCore::DarkenIntensityCompositeOp,
		LightenIntensity = MagickCore::LightenIntensityCompositeOp
	};
}