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

#include "Base\DrawableWrapper.h"
#include "..\Enums\CompositeOperator.h"
#include "..\MagickImage.h"

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Encapsulation of the DrawableCompositeImage object.
	///</summary>
	public ref class DrawableCompositeImage sealed : DrawableWrapper<Magick::DrawableCompositeImage>
	{
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Creates a new DrawableCompositeImage instance.
		///</summary>
		///<param name="x">The X coordinate.</param>
		///<param name="y">The Y coordinate.</param>
		///<param name="image">The image to draw.</param>
		DrawableCompositeImage(double x, double y, MagickImage^ image);
		///==========================================================================================
		///<summary>
		/// Creates a new DrawableCompositeImage instance.
		///</summary>
		///<param name="x">The X coordinate.</param>
		///<param name="y">The Y coordinate.</param>
		///<param name="compose">The algorithm to use.</param>
		///<param name="image">The image to draw.</param>
		DrawableCompositeImage(double x, double y, CompositeOperator compose, MagickImage^ image);
		///==========================================================================================
		///<summary>
		/// Creates a new DrawableCompositeImage instance.
		///</summary>
		///<param name="offset">The offset from origin.</param>
		///<param name="image">The image to draw.</param>
		DrawableCompositeImage(MagickGeometry^ offset, MagickImage^ image);
		///==========================================================================================
		///<summary>
		/// Creates a new DrawableCompositeImage instance.
		///</summary>
		///<param name="offset">The offset from origin.</param>
		///<param name="compose">The algorithm to use.</param>
		///<param name="image">The image to draw.</param>
		DrawableCompositeImage(MagickGeometry^ offset, CompositeOperator compose, MagickImage^ image);
		///==========================================================================================
		///<summary>
		/// The height to scale the image to.
		///</summary>
		property double Height
		{
			double get();
			void set(double value);
		}
		///==========================================================================================
		///<summary>
		/// The width to scale the image to.
		///</summary>
		property double Width
		{
			double get();
			void set(double value);
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