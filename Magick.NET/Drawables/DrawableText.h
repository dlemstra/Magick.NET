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
		void Initialize(double x, double y, String^ text, String^ encoding);
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Creates a new DrawableText instance.
		///</summary>
		///<param name="x">The X coordinate.</param>
		///<param name="y">The Y coordinate.</param>
		///<param name="text">The text to draw.</param>
		DrawableText(double x, double y, String^ text);
		///==========================================================================================
		///<summary>
		/// Creates a new DrawableText instance.
		///</summary>
		///<param name="x">The X coordinate.</param>
		///<param name="y">The Y coordinate.</param>
		///<param name="text">The text to draw.</param>
		///<param name="encoding">The encoding of the text.</param>
		DrawableText(double x, double y, String^ text, Encoding^ encoding);
		///==========================================================================================
		///<summary>
		/// The encoding of the text.
		///</summary>
		property String^ Encoding
		{
			void set(String^ value)
			{
				std::string encoding;
				Value->encoding(Marshaller::Marshal(value, encoding));
			}
		}
		///==========================================================================================
		///<summary>
		/// The text to draw.
		///</summary>
		property String^ Text
		{
			String^ get()
			{
				return Marshaller::Marshal(Value->text());
			}
			void set(String^ value)
			{
				std::string text;
				Value->text(Marshaller::Marshal(value, text));
			}
		}
		///==========================================================================================
		///<summary>
		/// The X coordinate.
		///</summary>
		property double X
		{
			double get()
			{
				return Value->x();
			}
			void set(double value)
			{
				Value->x(value);
			}
		}
		///==========================================================================================
		///<summary>
		/// The Y coordinate.
		///</summary>
		property double Y
		{
			double get()
			{
				return Value->y();
			}
			void set(double value)
			{
				Value->y(value);
			}
		}
	};
	//==============================================================================================
}