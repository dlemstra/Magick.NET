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

#include "DrawableWrapper.h"
#include "..\Enums\FontStretch.h"
#include "..\Enums\FontStyleType.h"
#include "..\Enums\FontWeight.h"

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Encapsulation of the DrawableFont object.
	///</summary>
	public ref class DrawableFont sealed : DrawableWrapper<Magick::DrawableFont>
	{
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Creates a new DrawableFont instance.
		///</summary>
		///<param name="family">The font family.</param>
		DrawableFont(String^ family);
		///==========================================================================================
		///<summary>
		/// Creates a new DrawableFont instance.
		///</summary>
		///<param name="family">The font family.</param>
		///<param name="style">The style of the font.</param>
		///<param name="weight">The weight of the font.</param>
		///<param name="stretch">The font stretching type.</param>
		DrawableFont(String^ family, FontStyleType style, FontWeight weight, FontStretch stretch);
		///==========================================================================================
		///<summary>
		/// The font family.
		///</summary>
		property String^ Family
		{
			String^ get()
			{
				return Marshaller::Marshal(Value->font());
			}
			void set(String^ value)
			{
				std::string font;
				Marshaller::Marshal(value, font);
				Value->font(font);
			}
		}
		//===========================================================================================
	};
	//==============================================================================================
}