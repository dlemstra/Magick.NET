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

#include "ColorBase.h"

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Class that represents a gray color.
	///</summary>
	public ref class ColorGray sealed : ColorBase
	{
		//===========================================================================================
	private:
		//===========================================================================================
		double _Shade;
		//===========================================================================================
		ColorGray(MagickColor^ color);
		//===========================================================================================
	protected:
		//===========================================================================================
		virtual void UpdateValue() override;
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the ColorGray class.
		///</summary>
		///<param name="shade">Value between 0.0 - 1.0.</param>
		ColorGray(double shade);
		///==========================================================================================
		///<summary>
		/// The shade of this color (value between 0.0 - 1.0).
		///</summary>
		property double Shade
		{
			double get()
			{
				return _Shade;
			}
			void set(double value)
			{
				if (value < 0.0 || value > 1.0)
					return;

				_Shade = value;
			}
		}
		//===========================================================================================
		static operator ColorGray^ (MagickColor^ color)
		{
			return FromMagickColor(color);
		}
		///==========================================================================================
		///<summary>
		/// Converts the specified MagickColor to an instane of this type.
		///</summary>
		static ColorGray^ FromMagickColor(MagickColor^ color);
		//===========================================================================================
	};
}