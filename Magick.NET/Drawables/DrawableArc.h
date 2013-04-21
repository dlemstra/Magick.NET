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
	/// Encapsulation of the DrawableArc object.
	///</summary>
	public ref class DrawableArc sealed : DrawableWrapper<Magick::DrawableArc>
	{
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Creates a new DrawableArc instance.
		///</summary>
		///<param name="startX">The starting X coordinate of the bounding rectangle.</param>
		///<param name="startY">The starting Y coordinate of thebounding rectangle.</param>
		///<param name="endX">The ending X coordinate of the bounding rectangle.</param>
		///<param name="endY">The ending Y coordinate of the bounding rectangle.</param>
		///<param name="startDegrees">The starting degrees of rotation.</param>
		///<param name="endDegrees">The ending degrees of rotation.</param>
		DrawableArc(double startX, double startY, double endX, double endY, double startDegrees,
			double endDegrees);
		///==========================================================================================
		///<summary>
		/// The ending degrees of rotation.
		///</summary>
		property double EndDegrees
		{
			double get()
			{
				return Value->endDegrees();
			}
			void set(double value)
			{
				Value->endDegrees(value);
			}
		}
		///==========================================================================================
		///<summary>
		/// The ending X coordinate of the bounding rectangle.
		///</summary>
		property double EndX
		{
			double get()
			{
				return Value->endX();
			}
			void set(double value)
			{
				Value->endX(value);
			}
		}
		///==========================================================================================
		///<summary>
		/// The ending Y coordinate of the bounding rectangle.
		///</summary>
		property double EndY
		{
			double get()
			{
				return Value->endY();
			}
			void set(double value)
			{
				Value->endY(value);
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
				return Value->startDegrees();
			}
			void set(double value)
			{
				Value->startDegrees(value);
			}
		}
		///==========================================================================================
		///<summary>
		/// The starting X coordinate of the bounding rectangle.
		///</summary>
		property double StartX
		{
			double get()
			{
				return Value->startX();
			}
			void set(double value)
			{
				Value->startX(value);
			}
		}
		///==========================================================================================
		///<summary>
		/// The starting Y coordinate of the bounding rectangle.
		///</summary>
		property double StartY
		{
			double get()
			{
				return Value->startY();
			}
			void set(double value)
			{
				Value->startY(value);
			}
		}
		//===========================================================================================
	};
	//==============================================================================================
}