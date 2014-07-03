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

#include "Stdafx.h"
#include "..\Colors\MagickColor.h"

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Represents an argument for the SparseColor method.
	///</summary>
	public ref class SparseColorArgs sealed
	{
		//===========================================================================================
		double _X;
		double _Y;
		MagickColor^ _Color;
		//===========================================================================================
	public:
		//===========================================================================================
		SparseColorArgs(double x, double y, MagickColor^ color);
		///==========================================================================================
		///<summary>
		/// The X position.
		///</summary>
		property double X
		{
			double get();
		}
		///==========================================================================================
		///<summary>
		/// The Y position.
		///</summary>
		property double Y
		{
			double get();
		}
		///==========================================================================================
		///<summary>
		/// The color.
		///</summary>
		property MagickColor^ Color
		{
			MagickColor^ get();
		}
		//===========================================================================================
	};
	//==============================================================================================
}