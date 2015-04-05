//=================================================================================================
// Copyright 2013-2015 Dirk Lemstra <https://magick.codeplex.com/>
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

using System;

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Encapsulation of the Coordinate object.
	///</summary>
	public struct Coordinate : IEquatable<Coordinate>
	{
		//===========================================================================================
		private double _X;
		private double _Y;
		///==========================================================================================
		///<summary>
		/// Creates a new Coordinate instance.
		///</summary>
		///<param name="x">The X position of the coordinate.</param>
		///<param name="y">The Y position of the coordinate.</param>
		public Coordinate(double x, double y)
		{
			_X = x;
			_Y = y;
		}
		///==========================================================================================
		///<summary>
		/// The X position of the coordinate.
		///</summary>
		public double X
		{
			get
			{
				return _X;
			}
			set
			{
				_X = value;
			}
		}
		///==========================================================================================
		///<summary>
		/// The Y position of the coordinate.
		///</summary>
		public double Y
		{
			get
			{
				return _Y;
			}
			set
			{
				_Y = value;
			}
		}
		///==========================================================================================
		/// <summary>
		/// Determines whether the specified Coordinate instances are considered equal.
		/// </summary>
		/// <param name="left">The first Coordinate to compare.</param>
		/// <param name="right"> The second Coordinate to compare.</param>
		/// <returns></returns>
		public static bool operator ==(Coordinate left, Coordinate right)
		{
			return object.Equals(left, right);
		}
		///==========================================================================================
		/// <summary>
		/// Determines whether the specified Coordinate instances are not considered equal.
		/// </summary>
		/// <param name="left">The first Coordinate to compare.</param>
		/// <param name="right"> The second Coordinate to compare.</param>
		/// <returns></returns>
		public static bool operator !=(Coordinate left, Coordinate right)
		{
			return !object.Equals(left, right);
		}
		///==========================================================================================
		///<summary>
		/// Determines whether the specified object is equal to the current coordinate.
		///</summary>
		///<param name="obj">The object to compare this coordinate with.</param>
		public override bool Equals(object obj)
		{
			if (obj == null)
				return false;

			if (obj.GetType() == typeof(Coordinate))
				return Equals((Coordinate)obj);

			return false;
		}
		///==========================================================================================
		///<summary>
		/// Determines whether the specified coordinate is equal to the current coordinate.
		///</summary>
		///<param name="other">The coordinate to compare this coordinate with.</param>
		public bool Equals(Coordinate other)
		{
			return
				X.Equals(other.X) &&
				Y.Equals(other.Y);
		}
		///==========================================================================================
		///<summary>
		/// Servers as a hash of this type.
		///</summary>
		public override int GetHashCode()
		{
			return
				X.GetHashCode() ^
				Y.GetHashCode();
		}
		//===========================================================================================
	}
	//==============================================================================================
}