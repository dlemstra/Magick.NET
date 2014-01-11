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

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Encapsulation of the DrawableTextKerning object.
	///</summary>
	public ref class DrawableTextKerning sealed : DrawableWrapper<Magick::DrawableTextKerning>
	{
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Creates a new DrawableTextKerning instance.
		///</summary>
		///<param name="kerning">Kerning to use.</param>
		DrawableTextKerning(double kerning);
		///==========================================================================================
		///<summary>
		/// Kerning to use.
		///</summary>
		property double Kerning
		{
			double get();
			void set(double value);
		}
		//===========================================================================================
	};
	//==============================================================================================
}