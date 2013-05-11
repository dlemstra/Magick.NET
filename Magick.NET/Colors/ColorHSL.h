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
	/// Class that represents a HSL color.
	///</summary>
	public ref class ColorHSL sealed : ColorBase
	{
		//===========================================================================================
	private:
		//===========================================================================================
		double _Hue;
		double _Luminosity;
		double _Saturation;
		//===========================================================================================
		ColorHSL(MagickColor^ color);
		//===========================================================================================
	protected:
		//===========================================================================================
		virtual void UpdateValue() override;
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the ColorHSL class.
		///</summary>
		///<param name="hue">Hue component value of this color.</param>
		///<param name="saturation">Saturation component value of this color.</param>
		///<param name="luminosity">Luminosity component value of this color.</param>
		ColorHSL(double hue, double saturation, double luminosity);
		///==========================================================================================
		///<summary>
		/// Hue component value of this color.
		///</summary>
		property double Hue
		{
			double get()
			{
				return _Hue;
			}
			void set(double value)
			{
				_Hue = value;
			}
		}
		///==========================================================================================
		///<summary>
		/// Luminosity component value of this color.
		///</summary>
		property double Luminosity
		{
			double get()
			{
				return _Luminosity;
			}
			void set(double value)
			{
				_Luminosity = value;
			}
		}
		///==========================================================================================
		///<summary>
		/// Saturation component value of this color.
		///</summary>
		property double Saturation
		{
			double get()
			{
				return _Saturation;
			}
			void set(double value)
			{
				_Saturation = value;
			}
		}
		//===========================================================================================
		static operator ColorHSL^ (MagickColor^ color)
		{
			return FromMagickColor(color);
		}
		///==========================================================================================
		///<summary>
		/// Converts the specified MagickColor to an instance of this type.
		///</summary>
		static ColorHSL^ FromMagickColor(MagickColor^ color);
		//===========================================================================================
	};
	//==============================================================================================
}
