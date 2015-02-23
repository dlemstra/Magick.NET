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

#include "Base\DoubleMatrix.h"

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Encapsulates a convolution kernel.
	///</summary>
	public ref class ConvolveMatrix sealed : DoubleMatrix
	{
		//===========================================================================================
	private:
		//===========================================================================================
		static void CheckOrder(int order);
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Creates a new ConvolveMatrix instance with the specified order.
		///</summary>
		///<param name="order">The order.</param>
		ConvolveMatrix(int order);
		///==========================================================================================
		///<summary>
		/// Creates a new ConvolveMatrix instance with the specified order.
		///</summary>
		///<param name="order">The order.</param>
		///<param name="values">The values to initialize the matrix with.</param>
		ConvolveMatrix(int order, ... array<double>^ values);
		//===========================================================================================
	};
	//==============================================================================================
}