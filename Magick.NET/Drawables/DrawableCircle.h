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

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Encapsulation of the DrawableCircle object.
	///</summary>
	public ref class DrawableCircle sealed : DrawableWrapper<Magick::DrawableCircle>
	{
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Creates a new DrawableCircle instance.
		///</summary>
		///<param name="originX">The origin X coordinate.</param>
		///<param name="originY">The origin Y coordinate.</param>
		///<param name="perimeterX">The perimeter X coordinate.</param>
		///<param name="perimeterY">The perimeter Y coordinate.</param>
		DrawableCircle(double originX, double originY, double perimeterX, double perimeterY);
		///==========================================================================================
		///<summary>
		/// The origin X coordinate.
		///</summary>
		property double OriginX
		{
			double get()
			{
				return Value->originX();
			}
			void set(double value)
			{
				Value->originX(value);
			}
		}
		///==========================================================================================
		///<summary>
		/// The origin X coordinate.
		///</summary>
		property double OriginY
		{
			double get()
			{
				return Value->originY();
			}
			void set(double value)
			{
				Value->originY(value);
			}
		}
		///==========================================================================================
		///<summary>
		/// The perimeter X coordinate.
		///</summary>
		property double PerimeterX
		{
			double get()
			{
				return Value->perimX();
			}
			void set(double value)
			{
				Value->perimX(value);
			}
		}
		///==========================================================================================
		///<summary>
		/// The perimeter X coordinate.
		///</summary>
		property double PerimeterY
		{
			double get()
			{
				return Value->perimY();
			}
			void set(double value)
			{
				Value->perimY(value);
			}
		}
		//===========================================================================================
	};
	//==============================================================================================
}