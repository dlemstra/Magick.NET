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
	/// Specifies the color type of the image
	///</summary>
	public enum class ColorType
	{
		Undefined = MagickCore::UndefinedType,
		Bilevel = MagickCore::BilevelType,
		Grayscale = MagickCore::GrayscaleType,
		GrayscaleMatte = MagickCore::GrayscaleMatteType,
		Palette = MagickCore::PaletteType,
		PaletteMatte = MagickCore::PaletteMatteType,
		TrueColor = MagickCore::TrueColorType,
		TrueColorMatte = MagickCore::TrueColorMatteType,
		ColorSeparation = MagickCore::ColorSeparationType,
		ColorSeparationMatte = MagickCore::ColorSeparationMatteType,
		Optimize = MagickCore::OptimizeType,
		PaletteBilevelMatte = MagickCore::PaletteBilevelMatteType
	};
	//==============================================================================================
}