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
#include "..\Colors\MagickColor.h"

using namespace System::Drawing;

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Encapsulation of the DrawableTextUnderColor object.
	///</summary>
	public ref class DrawableTextUnderColor sealed : DrawableWrapper<Magick::DrawableTextUnderColor>
	{
		//===========================================================================================
	private:
		//===========================================================================================
		void Initialize(MagickColor^ color);
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Creates a new DrawableTextUnderColor instance.
		///</summary>
		///<param name="color">The color to use.</param>
		DrawableTextUnderColor(Color color);
		///==========================================================================================
		///<summary>
		/// Creates a new DrawableTextUnderColor instance.
		///</summary>
		///<param name="color">The color to use.</param>
		DrawableTextUnderColor(MagickColor^ color);
		///==========================================================================================
		///<summary>
		/// The color to use.
		///</summary>
		property MagickColor^ Color
		{
			MagickColor^ get();
			void set(MagickColor^ value);
		}
		//===========================================================================================
	};
	//==============================================================================================
}