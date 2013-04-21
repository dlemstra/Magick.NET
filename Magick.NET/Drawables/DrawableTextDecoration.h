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
#include "..\Enums\TextDecoration.h"

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Encapsulation of the DrawableTextDecoration object.
	///</summary>
	public ref class DrawableTextDecoration sealed : DrawableWrapper<Magick::DrawableTextDecoration>
	{
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Creates a new DrawableTextDecoration instance.
		///</summary>
		///<param name="decoration">The text decoration.</param>
		DrawableTextDecoration(TextDecoration decoration);
		///==========================================================================================
		///<summary>
		/// The text decoration
		///</summary>
		property TextDecoration Decoration
		{
			TextDecoration get()
			{
				return (ImageMagick::TextDecoration)Value->decoration();
			}
			void set(TextDecoration value)
			{
				Value->decoration((MagickCore::DecorationType)value);
			}
		}
		//===========================================================================================
	};
	//==============================================================================================
}