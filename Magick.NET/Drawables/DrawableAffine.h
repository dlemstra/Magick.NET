//=================================================================================================
// Copyright 2013-2014 Dirk Lemstra <https://magick.codeplex.com/>
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

using namespace System::Drawing::Drawing2D;

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Encapsulates a 3-by-3 affine matrix that represents a geometric transform.
	///</summary>
	public ref class DrawableAffine sealed : DrawableWrapper<Magick::DrawableAffine>
	{
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Creates a new DrawableAffine instance.
		///</summary>
		///<param name="scaleX">The X coordinate scaling element.</param>
		///<param name="scaleY">The Y coordinate scaling element.</param>
		///<param name="shearX">The X coordinate shearing element.</param>
		///<param name="shearY">The Y coordinate shearing element.</param>
		///<param name="translateX">The X coordinate of the translation element.</param>
		///<param name="translateY">The Y coordinate of the translation element.</param>
		DrawableAffine(double scaleX, double scaleY, double shearX, double shearY, double translateX,
			double translateY);
		///==========================================================================================
		///<summary>
		/// Creates a new DrawableAffine instance using the specified Matrix.
		///</summary>
		///<param name="matrix">The matrix.</param>
		DrawableAffine(Matrix^ matrix);
		///==========================================================================================
		///<summary>
		/// The X coordinate scaling element.
		///</summary>
		property double ScaleX
		{
			double get();
			void set(double value);
		}
		///==========================================================================================
		///<summary>
		/// The Y coordinate scaling element.
		///</summary>
		property double ScaleY
		{
			double get();
			void set(double value);
		}
		///==========================================================================================
		///<summary>
		/// The X coordinate shearing element.
		///</summary>
		property double ShearX
		{
			double get();
			void set(double value);
		}
		///==========================================================================================
		///<summary>
		/// The Y coordinate shearing element.
		///</summary>
		property double ShearY
		{
			double get();
			void set(double value);
		}
		///==========================================================================================
		///<summary>
		/// The X coordinate of the translation element.
		///</summary>
		property double TranslateX
		{
			double get();
			void set(double value);
		}
		///==========================================================================================
		///<summary>
		/// The Y coordinate of the translation element.
		///</summary>
		property double TranslateY
		{
			double get();
			void set(double value);
		}
		//===========================================================================================
	};
	//==============================================================================================
}