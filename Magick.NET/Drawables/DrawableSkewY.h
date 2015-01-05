//=================================================================================================
// Copyright 2013-2015 Dirk Lemstra <https://magick.codeplex.com/>
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
	/// Encapsulation of the DrawableSkewY object.
	///</summary>
	public ref class DrawableSkewY sealed : DrawableWrapper<Magick::DrawableSkewY>
	{
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Creates a new DrawableSkewY instance.
		///</summary>
		///<param name="angle">The angle.</param>
		DrawableSkewY(double angle);
		///==========================================================================================
		///<summary>
		/// The miter limit.
		///</summary>
		property double Angle
		{
			double get();
			void set(double value);
		}
		//===========================================================================================
	};
	//==============================================================================================
}