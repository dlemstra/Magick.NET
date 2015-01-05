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

#include "Base\ColorBase.h"

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Class that represents a monochrome color.
	///</summary>
	public ref class ColorMono sealed : ColorBase
	{
		//===========================================================================================
	private:
		//===========================================================================================
		ColorMono(MagickColor^ color);
		//===========================================================================================
	protected:
		//===========================================================================================
		virtual void UpdateValue() override;
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the ColorMono class.
		///</summary>
		///<param name="isBlack">Specifies if the color is black or white.</param>
		ColorMono(bool isBlack);
		///==========================================================================================
		///<summary>
		/// Specifies if the color is black or white.
		///</summary>
		property bool IsBlack;
		//===========================================================================================
		static operator ColorMono^ (MagickColor^ color);
		///==========================================================================================
		///<summary>
		/// Converts the specified MagickColor to an instance of this type.
		///</summary>
		static ColorMono^ FromMagickColor(MagickColor^ color);
		//===========================================================================================
	};
	//==============================================================================================
}