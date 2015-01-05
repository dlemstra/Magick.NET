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

#include "..\Enums\ColorSpace.h"
#include "..\Enums\DitherMethod.h"

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Class that contains setting for quantize operations.
	///</summary>
	public ref class QuantizeSettings sealed
	{
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the QuantizeSettings class.
		///</summary>
		QuantizeSettings();
		///==========================================================================================
		///<summary>
		/// Maximum number of colors to quantize to.
		///</summary>
		property int Colors;
		///==========================================================================================
		///<summary>
		/// Colorspace to quantize in.
		///</summary>
		property ColorSpace ColorSpace;
		///==========================================================================================
		///<summary>
		/// Dither method to use.
		///</summary>
		property Nullable<DitherMethod> DitherMethod;
		///==========================================================================================
		///<summary>
		/// Measure errors.
		///</summary>
		property bool MeasureErrors;
		///==========================================================================================
		///<summary>
		/// Quantization tree-depth.
		///</summary>
		property int TreeDepth;
		//===========================================================================================
	};
	//==============================================================================================
}