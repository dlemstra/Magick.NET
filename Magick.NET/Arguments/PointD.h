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

using namespace System;

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Struct for a point with doubles.
	///</summary>
	public value struct PointD sealed: IEquatable<PointD>
	{
		//===========================================================================================
	private:
		//===========================================================================================
		double _X;
		double _Y;
		//===========================================================================================
	internal:
		//===========================================================================================
		PointD(Magick::Point point);
		//===========================================================================================
		const Magick::Point* CreatePoint();
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the PointD struct using the specified x and y.
		///</summary>
		///<param name="xy">The x and y.</param>
		PointD(double xy);
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the PointD struct using the specified x and y.
		///</summary>
		///<param name="x">The x.</param>
		///<param name="y">The y.</param>
		//===========================================================================================
		PointD(double x, double y);
		///==========================================================================================
		///<summary>
		/// The x-coordinate of this Point.
		///</summary>
		property double X
		{
			double get();
		internal:
			void set(double value);
		}
		///==========================================================================================
		///<summary>
		/// The y-coordinate of this Point.
		///</summary>
		property double Y
		{
			double get();
		internal:
			void set(double value);
		}
		//===========================================================================================
		static bool operator == (PointD left, PointD right);
		//===========================================================================================
		static bool operator != (PointD left, PointD right);
		///==========================================================================================
		///<summary>
		/// Determines whether the specified object is equal to the current point.
		///</summary>
		///<param name="obj">The object to compare this point with.</param>
		virtual bool Equals(Object^ obj) override;
		///==========================================================================================
		///<summary>
		/// Determines whether the specified point is equal to the current point.
		///</summary>
		///<param name="other">The point to compare this point with.</param>
		virtual bool Equals(PointD other);
		///==========================================================================================
		///<summary>
		/// Servers as a hash of this type.
		///</summary>
		virtual int GetHashCode() override;
		//===========================================================================================
	};
	//==============================================================================================
}