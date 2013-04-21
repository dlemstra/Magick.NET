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
#include "..\Enums\LineCap.h"

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Encapsulation of the DrawableStrokeLineCap object.
	///</summary>
	public ref class DrawableStrokeLineCap sealed : DrawableWrapper<Magick::DrawableStrokeLineCap>
	{
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Creates a new DrawableStrokeLineCap instance.
		///</summary>
		///<param name="lineCap">The line cap.</param>
		DrawableStrokeLineCap(LineCap lineCap);
		///==========================================================================================
		///<summary>
		/// The line cap.
		///</summary>
		property LineCap LineCap
		{
			ImageMagick::LineCap get()
			{
				return (ImageMagick::LineCap)Value->linecap();
			}
			void set(ImageMagick::LineCap value)
			{
				Value->linecap((MagickCore::LineCap)value);
			}
		}
		//===========================================================================================
	};
	//==============================================================================================
}