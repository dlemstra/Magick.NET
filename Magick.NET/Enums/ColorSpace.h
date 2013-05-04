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
	/// Specifies a kind of color space.
	///</summary>
	public enum class ColorSpace
	{
		Undefined = MagickCore::UndefinedColorspace,
		RGB = MagickCore::RGBColorspace,
		GRAY = MagickCore::GRAYColorspace,
		Transparent = MagickCore::TransparentColorspace,
		OHTA = MagickCore::OHTAColorspace,
		Lab = MagickCore::LabColorspace,
		XYZ = MagickCore::XYZColorspace,
		YCbCr = MagickCore::YCbCrColorspace,
		YCC = MagickCore::YCCColorspace,
		YIQ = MagickCore::YIQColorspace,
		YPbPr = MagickCore::YPbPrColorspace,
		YUV = MagickCore::YUVColorspace,
		CMYK = MagickCore::CMYKColorspace,
		sRGB = MagickCore::sRGBColorspace,
		HSB = MagickCore::HSBColorspace,
		HSL = MagickCore::HSLColorspace,
		HWB = MagickCore::HWBColorspace,
		Rec601Luma = MagickCore::Rec601LumaColorspace,
		Rec601YCbCr = MagickCore::Rec601YCbCrColorspace,
		Rec709Luma = MagickCore::Rec709LumaColorspace,
		Rec709YCbCr = MagickCore::Rec709YCbCrColorspace,
		Log = MagickCore::LogColorspace,
		CMY = MagickCore::CMYColorspace,
		Luv = MagickCore::LuvColorspace,
		HCL = MagickCore::HCLColorspace,
		LCH = MagickCore::LCHColorspace,
		LMS = MagickCore::LMSColorspace,
		LCHab = MagickCore::LCHabColorspace,
		LCHuv = MagickCore::LCHuvColorspace,
		scRGB = MagickCore::scRGBColorspace
	};
	//==============================================================================================
}