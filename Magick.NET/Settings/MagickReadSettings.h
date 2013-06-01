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

#include "Stdafx.h"
#include "..\Colors\ColorProfile.h"
#include "..\Enums\ColorSpace.h"
#include "..\MagickGeometry.h"

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Class that contains setting for when an image is being read.
	///</summary>
	public ref class MagickReadSettings sealed
	{
		//===========================================================================================
	internal:
		//===========================================================================================
		bool Ping;
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Color space.
		///</summary>
		property Nullable<ColorSpace> ColorSpace;
		///==========================================================================================
		///<summary>
		/// Vertical and horizontal resolution in pixels.
		///</summary>
		property MagickGeometry^ Density;
		///==========================================================================================
		///<summary>
		/// The height.
		///</summary>
		property Nullable<int> Height;
		///==========================================================================================
		///<summary>
		/// The width.
		///</summary>
		property Nullable<int> Width;
		//===========================================================================================
	};
	//==============================================================================================
}