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
	public ref class Coordinate sealed
	{
		//===========================================================================================
	private:
		//===========================================================================================
		double _X;
		double _Y;
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Creates a new DrawableAffine instance.
		///</summary>
		///<param name="x">The X position of the coordinate.</param>
		///<param name="y">The Y position of the coordinate.</param>
		Coordinate(double x, double y);
		///==========================================================================================
		///<summary>
		/// The X position of the coordinate.
		///</summary>
		property double X
		{
			double get()
			{
				return _X;
			}
			void set(double value)
			{
				_X = value;
			}
		}
		///==========================================================================================
		///<summary>
		/// The Y position of the coordinate.
		///</summary>
		property double Y
		{
			double get()
			{
				return _Y;
			}
			void set(double value)
			{
				_Y = value;
			}
		}
		//===========================================================================================
	};
	//==============================================================================================
}