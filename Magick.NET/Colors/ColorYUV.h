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
			double get();
			void set(double value);
		}
		///==========================================================================================
		///<summary>
		/// V component value of this color. (value beteeen -0.5 and 0.5)
		///</summary>
		property double V
		{
			double get();
			void set(double value);
		}
		///==========================================================================================
		///<summary>
		/// Y component value of this color. (value beteeen 0.0 and 1.0)
		///</summary>
		property double Y
		{
			double get();
			void set(double value);
		}
		//===========================================================================================
		static operator ColorYUV^ (MagickColor^ color);
		///==========================================================================================
		///<summary>
		/// Converts the specified MagickColor to an instance of this type.
		///</summary>
		static ColorYUV^ FromMagickColor(MagickColor^ color);
		//===========================================================================================
	};
	//==============================================================================================
}
