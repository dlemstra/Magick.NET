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
	/// Class that represents a YUV color.
	///</summary>
	public ref class ColorYUV sealed : ColorBase
	{
		//===========================================================================================
	private:
		//===========================================================================================
		double _U;
		double _V;
		double _Y;
		//===========================================================================================
		ColorYUV(MagickColor^ color);
		//===========================================================================================
	protected:
		//===========================================================================================
		virtual void UpdateValue() override;
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the ColorYUV class.
		///</summary>
		///<param name="y">Y component value of this color.</param>
		///<param name="u">U component value of this color.</param>
		///<param name="v">V component value of this color.</param>
		ColorYUV(double y, double u, double v);
		///==========================================================================================
		///<summary>
		/// U component value of this color. (value beteeen -0.5 and 0.5)
		///</summary>
		property double U
		{
			double get()
			{
				return _U;
			}
			void set(double value)
			{
				if (value < -0.5 || value > 0.5)
					return;

				_U = value;
			}
		}
		///==========================================================================================
		///<summary>
		/// V component value of this color. (value beteeen -0.5 and 0.5)
		///</summary>
		property double V
		{
			double get()
			{
				return _V;
			}
			void set(double value)
			{
				if (value < -0.5 || value > 0.5)
					return;

				_V = value;
			}
		}
		///==========================================================================================
		///<summary>
		/// Y component value of this color. (value beteeen 0.0 and 1.0)
		///</summary>
		property double Y
		{
			double get()
			{
				return _Y;
			}
			void set(double value)
			{
				if (value < 0.0 || value > 1.0)
					return;

				_Y = value;
			}
		}
		//===========================================================================================
		static operator ColorYUV^ (MagickColor^ color)
		{
			return FromMagickColor(color);
		}
		///==========================================================================================
		///<summary>
		/// Converts the specified MagickColor to an instane of this type.
		///</summary>
		static ColorYUV^ FromMagickColor(MagickColor^ color);
		//===========================================================================================
	};
	//==============================================================================================
}
