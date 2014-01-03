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

#include "Base\PathArgsWrapper.h"

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Encapsulation of the PathArc object.
	///</summary>
	public ref class PathArc sealed : PathArgsWrapper<Magick::PathArcArgs>
	{
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the PathArc class.
		///</summary>
		PathArc();
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the PathArc class.
		///</summary>
		///<param name="x">The X offset from origin.</param>
		///<param name="y">The Y offset from origin.</param>
		///<param name="radiusX">The X radius.</param>
		///<param name="radiusY">The Y radius.</param>
		///<param name="rotationX">Indicates how the ellipse as a whole is rotated relative to the
		/// current coordinate system.</param>
		///<param name="useLargeArc">If true then draw the larger of the available arcs.</param>
		///<param name="useSweep">If true then draw the arc matching a clock-wise rotation.</param>
		PathArc(double x, double y, double radiusX, double radiusY, double rotationX, bool useLargeArc,
			bool useSweep);
		///==========================================================================================
		///<summary>
		/// The X radius.
		///</summary>
		property double RadiusX
		{
			double get();
			void set(double value);
		}
		///==========================================================================================
		///<summary>
		/// The Y radius.
		///</summary>
		property double RadiusY
		{
			double get();
			void set(double value);
		}
		///==========================================================================================
		///<summary>
		/// Indicates how the ellipse as a whole is rotated relative to the current coordinate system.
		///</summary>
		property double RotationX
		{
			double get();
			void set(double value);
		}
		///==========================================================================================
		///<summary>
		/// If true then draw the larger of the available arcs.
		///</summary>
		property bool UseLargeArc
		{
			bool get();
			void set(bool value);
		}
		///==========================================================================================
		///<summary>
		/// If true then draw the arc matching a clock-wise rotation.
		///</summary>
		property bool UseSweep
		{
			bool get();
			void set(bool value);
		}
		///==========================================================================================
		///<summary>
		/// The X offset from origin.
		///</summary>
		property double X
		{
			double get();
			void set(double value);
		}
		///==========================================================================================
		///<summary>
		/// The Y offset from origin.
		///</summary>
		property double Y
		{
			double get();
			void set(double value);
		}
		//===========================================================================================
	};
	//==============================================================================================
}