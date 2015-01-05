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
#pragma once

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Encapsulates a matrix of doubles.
	///</summary>
	public ref class DoubleMatrix abstract
	{
		//===========================================================================================
	private:
		//===========================================================================================
		int _Order;
		array<double, 2>^ _Values;
		//===========================================================================================
	protected private:
		//===========================================================================================
		DoubleMatrix();
		//===========================================================================================
		void Initialize(int order);
		//===========================================================================================
	internal:
		//===========================================================================================
		double* CreateArray();
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Get or set the value at the specified x/y position.
		///</summary>
		property double default[int, int]
		{
			double get(int x, int y);
			void set(int x, int y, double value);
		}
		///==========================================================================================
		///<summary>
		/// Returns the order of the matrix.
		///</summary>
		property int Order
		{
			int get();
		}
		///==========================================================================================
		///<summary>
		/// Returns the value at the specified x/y position.
		///</summary>
		///<param name="x">The x position</param>
		///<param name="y">The y position</param>
		double GetValue(int x, int y);
		///==========================================================================================
		///<summary>
		/// Set the value at the specified x/y position.
		///</summary>
		///<param name="x">The x position</param>
		///<param name="y">The y position</param>
		///<param name="value">The value</param>
		void SetValue(int x, int y, double value);
		//===========================================================================================
	};
	//==============================================================================================
}