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

using namespace System::Text;

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Encapsulation of the DrawableCompositeImage object.
	///</summary>
	public ref class DrawableText sealed : DrawableWrapper<Magick::DrawableText>
	{
		//===========================================================================================
	private:
		//===========================================================================================
		void Initialize(double x, double y, String^ value, String^ encoding);
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Creates a new DrawableText instance.
		///</summary>
		///<param name="x">The X coordinate.</param>
		///<param name="y">The Y coordinate.</param>
		///<param name="value">The text to draw.</param>
		DrawableText(double x, double y, String^ value);
		///==========================================================================================
		///<summary>
		/// Creates a new DrawableText instance.
		///</summary>
		///<param name="x">The X coordinate.</param>
		///<param name="y">The Y coordinate.</param>
		///<param name="value">The text to draw.</param>
		///<param name="encoding">The encoding of the text.</param>
		DrawableText(double x, double y, String^ value, Encoding^ encoding);
		///==========================================================================================
		///<summary>
		/// The encoding of the text.
		///</summary>
		property String^ Encoding
		{
			void set(String^ value);
		}
		///==========================================================================================
		///<summary>
		/// The text to draw.
		///</summary>
		property String^ Text
		{
			String^ get();
			void set(String^ value);
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