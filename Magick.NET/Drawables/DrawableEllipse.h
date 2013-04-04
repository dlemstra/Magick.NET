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

#include "DrawableWrapper.h"

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Encapsulation of the DrawableEllipse object.
	///</summary>
	public ref class DrawableEllipse sealed : DrawableWrapper<Magick::DrawableEllipse>
	{
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Creates a new DrawableEllipse instance.
		///</summary>
		///<param name="originX">The origin X coordinate.</param>
		///<param name="originY">The origin Y coordinate.</param>
		///<param name="radiusX">The X radius.</param>
		///<param name="radiusY">The Y radius.</param>
		///<param name="startDegrees">The starting degrees of rotation.</param>
		///<param name="endDegrees">The ending degrees of rotation.</param>
		DrawableEllipse(double originX, double originY, double radiusX, double radiusY, 
			double startDegrees, double endDegrees);
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
		/// The X radius.
		///</summary>
		property double RadiusX
		{
			double get()
			{
				return Value->radiusX();
			}
			void set(double value)
			{
				Value->radiusX(value);
			}
		}
		///==========================================================================================
		///<summary>
		/// The Y radius.
		///</summary>
		property double RadiusY
		{
			double get()
			{
				return Value->radiusY();
			}
			void set(double value)
			{
				Value->radiusY(value);
			}
		}
		///==========================================================================================
		///<summary>
		/// The starting degrees of rotation.
		///</summary>
		property double StartDegrees
		{
			double get()
			{
				return Value->arcStart();
			}
			void set(double value)
			{
				Value->arcStart(value);
			}
		}
		///==========================================================================================
		///<summary>
		/// The ending degrees of rotation.
		///</summary>
		property double EndDegrees
		{
			double get()
			{
				return Value->arcEnd();
			}
			void set(double value)
			{
				Value->arcEnd(value);
			}
		}
		//===========================================================================================
	};
	//==============================================================================================
}