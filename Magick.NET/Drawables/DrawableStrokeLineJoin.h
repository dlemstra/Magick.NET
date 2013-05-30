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
#include "..\Enums\LineJoin.h"

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Encapsulation of the DrawableStrokeLineJoin object.
	///</summary>
	public ref class DrawableStrokeLineJoin sealed : DrawableWrapper<Magick::DrawableStrokeLineJoin>
	{
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Creates a new DrawableStrokeLineJoin instance.
		///</summary>
		///<param name="lineJoin">The line join.</param>
		DrawableStrokeLineJoin(LineJoin lineJoin);
		///==========================================================================================
		///<summary>
		/// The line cap.
		///</summary>
		property LineJoin LineCap
		{
			ImageMagick::LineJoin get();
			void set(ImageMagick::LineJoin value);
		}
		//===========================================================================================
	};
	//==============================================================================================
}