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
	private:
		//===========================================================================================
		Nullable<ColorSpace> _ColorSpace;
		MagickGeometry^ _Density; 
		Nullable<int> _Height;
		Nullable<int> _Width;
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
		property Nullable<ColorSpace> ColorSpace
		{
			Nullable<ImageMagick::ColorSpace> get()
			{
				return _ColorSpace;
			}
			void set(Nullable<ImageMagick::ColorSpace> value)
			{
				_ColorSpace = value;
			}
		}
		///==========================================================================================
		///<summary>
		/// Vertical and horizontal resolution in pixels.
		///</summary>
		property MagickGeometry^ Density
		{
			MagickGeometry^ get()
			{
				return _Density;
			}
			void set(MagickGeometry^ value)
			{
				_Density = value;
			}
		}
		///==========================================================================================
		///<summary>
		/// The height.
		///</summary>
		property Nullable<int> Height
		{
			Nullable<int> get()
			{
				return _Height;
			}
			void set(Nullable<int> value)
			{
				_Height = value;
			}
		}
		///==========================================================================================
		///<summary>
		/// The width.
		///</summary>
		property Nullable<int> Width
		{
			Nullable<int> get()
			{
				return _Width;
			}
			void set(Nullable<int> value)
			{
				_Width = value;
			}
		}
		//===========================================================================================
	};
	//==============================================================================================
}