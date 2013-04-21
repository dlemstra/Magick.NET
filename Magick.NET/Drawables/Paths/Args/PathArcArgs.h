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

#include "Base\PathArgsWrapper.h"

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Encapsulation of the PathArcArgs object.
	///</summary>
	public ref class PathArcArgs sealed : PathArgsWrapper<Magick::PathArcArgs>
	{
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the PathArcArgs class.
		///</summary>
		PathArcArgs();
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the PathArcArgs class.
		///</summary>
		///<param name="x">The X offset from origin.</param>
		///<param name="y">The Y offset from origin.</param>
		///<param name="radiusX">The X radius.</param>
		///<param name="radiusY">The Y radius.</param>
		///<param name="rotationX">Indicates how the ellipse as a whole is rotated relative to the
		/// current coordinate system.</param>
		///<param name="useLargeArc">If true then draw the larger of the available arcs.</param>
		///<param name="useSweep">If true then draw the arc matching a clock-wise rotation.</param>
		PathArcArgs(double x, double y, double radiusX, double radiusY, double rotationX, bool useLargeArc,
			bool useSweep);
		///==========================================================================================
		///<summary>
		/// The X radius.
		///</summary>
		property double RadiusX
		{
			double get()
			{
				return InternalValue->radiusX();
			}
			void set(double value)
			{
				InternalValue->radiusX(value);
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
				return InternalValue->radiusY();
			}
			void set(double value)
			{
				InternalValue->radiusY(value);
			}
		}
		///==========================================================================================
		///<summary>
		/// Indicates how the ellipse as a whole is rotated relative to the current coordinate system.
		///</summary>
		property double RotationX
		{
			double get()
			{
				return InternalValue->xAxisRotation();
			}
			void set(double value)
			{
				InternalValue->xAxisRotation(value);
			}
		}
		///==========================================================================================
		///<summary>
		/// If true then draw the larger of the available arcs.
		///</summary>
		property bool UseLargeArc
		{
			bool get()
			{
				return InternalValue->largeArcFlag();
			}
			void set(bool value)
			{
				InternalValue->largeArcFlag(value);
			}
		}
		///==========================================================================================
		///<summary>
		/// If true then draw the arc matching a clock-wise rotation.
		///</summary>
		property bool UseSweep
		{
			bool get()
			{
				return InternalValue->sweepFlag();
			}
			void set(bool value)
			{
				InternalValue->sweepFlag(value);
			}
		}
		///==========================================================================================
		///<summary>
		/// The X offset from origin.
		///</summary>
		property double X
		{
			double get()
			{
				return InternalValue->x();
			}
			void set(double value)
			{
				InternalValue->y(value);
			}
		}
		///==========================================================================================
		///<summary>
		/// The Y offset from origin.
		///</summary>
		property double Y
		{
			double get()
			{
				return InternalValue->y();
			}
			void set(double value)
			{
				InternalValue->y(value);
			}
		}
		//===========================================================================================
	};
	//==============================================================================================
}