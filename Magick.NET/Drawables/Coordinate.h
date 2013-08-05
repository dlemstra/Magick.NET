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

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Encapsulation of the Coordinate object.
	///</summary>
	public value struct Coordinate
	{
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Creates a new Coordinate instance.
		///</summary>
		///<param name="x">The X position of the coordinate.</param>
		///<param name="y">The Y position of the coordinate.</param>
		Coordinate(double x, double y);
		///==========================================================================================
		///<summary>
		/// The X position of the coordinate.
		///</summary>
		property double X;
		///==========================================================================================
		///<summary>
		/// The Y position of the coordinate.
		///</summary>
		property double Y;
		//===========================================================================================
		static bool operator == (Coordinate left, Coordinate right);
		//===========================================================================================
		static bool operator != (Coordinate left, Coordinate right);
		///==========================================================================================
		///<summary>
		/// Determines whether the specified object is equal to the current coordinate.
		///</summary>
		///<param name="obj">The object to compare this coordinate with.</param>
		virtual bool Equals(Object^ obj) override;
		///==========================================================================================
		///<summary>
		/// Determines whether the specified coordinate is equal to the current coordinate.
		///</summary>
		///<param name="coordinate">The coordinate to compare this coordinate with.</param>
		bool Equals(Coordinate coordinate);
		///==========================================================================================
		///<summary>
		/// Servers as a hash of this type.
		///</summary>
		virtual int GetHashCode() override;
		//===========================================================================================
	};
	//==============================================================================================
}