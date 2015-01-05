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

#include "Base\DrawableWrapper.h"
#include "..\Enums\PaintMethod.h"

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Encapsulation of the DrawableColor object.
	///</summary>
	public ref class DrawableColor sealed : DrawableWrapper<Magick::DrawableColor>
	{
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Creates a new DrawableColor instance.
		///</summary>
		///<param name="x">The X coordinate.</param>
		///<param name="y">The Y coordinate.</param>
		///<param name="paintMethod">The paint method to use.</param>
		DrawableColor(double x, double y, PaintMethod paintMethod);
		///==========================================================================================
		///<summary>
		/// The PaintMethod to use.
		///</summary>
		property PaintMethod PaintMethod
		{
			ImageMagick::PaintMethod get();
			void set(ImageMagick::PaintMethod value);
		}
		///==========================================================================================
		///<summary>
		/// The X coordinate.
		///</summary>
		property double X
		{
			double get();
			void set(double value);
		}
		///==========================================================================================
		///<summary>
		/// The Y coordinate.
		///</summary>
		property double Y
		{
			double get();
			void set(double value);
		}
		//===========================================================================================
	};
	//==============================================================================================
}