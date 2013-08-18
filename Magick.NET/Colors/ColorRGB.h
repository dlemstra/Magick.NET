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

#include "Base\ColorBase.h"

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Class that represents a RGB color.
	///</summary>
	public ref class ColorRGB sealed : ColorBase
	{
		//===========================================================================================
	private:
		//===========================================================================================
		ColorRGB(MagickColor^ color);
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the ColorRGB class.
		///</summary>
		///<param name="red">Red component value of this color.</param>
		///<param name="green">Green component value of this color.</param>
		///<param name="blue">Blue component value of this color.</param>
		ColorRGB(Magick::Quantum red, Magick::Quantum green, Magick::Quantum blue);
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the ColorRGB class using the specified color.
		///</summary>
		///<param name="color">The color to use.</param>
		ColorRGB(Color color);
		///==========================================================================================
		///<summary>
		/// Blue component value of this color.
		///</summary>
		property Magick::Quantum B
		{
			Magick::Quantum get();
			void set(Magick::Quantum value);
		}
		///==========================================================================================
		///<summary>
		/// Green component value of this color.
		///</summary>
		property Magick::Quantum G
		{
			Magick::Quantum get();
			void set(Magick::Quantum value);
		}
		///==========================================================================================
		///<summary>
		/// Red component value of this color.
		///</summary>
		property Magick::Quantum R
		{
			Magick::Quantum get();
			void set(Magick::Quantum value);
		}
		//===========================================================================================
		static operator ColorRGB^ (MagickColor^ color);
		///==========================================================================================
		///<summary>
		/// Converts the specified MagickColor to an instance of this type.
		///</summary>
		static ColorRGB^ FromMagickColor(MagickColor^ color);
		//===========================================================================================
	};
}