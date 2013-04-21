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
	/// Class that represents a CMYK color.
	///</summary>
	public ref class ColorCMYK sealed : ColorBase
	{
		//===========================================================================================
	private:
		//===========================================================================================
		ColorCMYK(MagickColor^ color);
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the ColorCMYK class.
		///</summary>
		///<param name="cyan">Cyan component value of this color.</param>
		///<param name="magenta">Magenta component value of this color.</param>
		///<param name="yellow">Yellow component value of this color.</param>
		///<param name="key">Key (black) component value of this color.</param>
		ColorCMYK(Magick::Quantum cyan, Magick::Quantum magenta, Magick::Quantum yellow,
			Magick::Quantum key);
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the ColorCMYK class using the specified color.
		///</summary>
		///<param name="color">The color to use.</param>
		ColorCMYK(Color color);
		///==========================================================================================
		///<summary>
		/// Cyan component value of this color.
		///</summary>
		property Magick::Quantum C
		{
			Magick::Quantum get()
			{
				return Value->R;
			}
			void set(Magick::Quantum value)
			{
				Value->R = value;
			}
		}
		///==========================================================================================
		///<summary>
		/// Key (black) component value of this color.
		///</summary>
		property Magick::Quantum K
		{
			Magick::Quantum get()
			{
				return Value->A;
			}
			void set(Magick::Quantum value)
			{
				Value->A = value;
			}
		}
		///==========================================================================================
		///<summary>
		/// Magenta component value of this color.
		///</summary>
		property Magick::Quantum M
		{
			Magick::Quantum get()
			{
				return Value->G;
			}
			void set(Magick::Quantum value)
			{
				Value->G = value;
			}
		}
		///==========================================================================================
		///<summary>
		/// Yellow component value of this color.
		///</summary>
		property Magick::Quantum Y
		{
			Magick::Quantum get()
			{
				return Value->B;
			}
			void set(Magick::Quantum value)
			{
				Value->B = value;
			}
		}
		//===========================================================================================
		static operator ColorCMYK^ (MagickColor^ color)
		{
			return FromMagickColor(color);
		}
		///==========================================================================================
		///<summary>
		/// Converts the specified MagickColor to an instane of this type.
		///</summary>
		static ColorCMYK^ FromMagickColor(MagickColor^ color);
		//===========================================================================================
	};
}