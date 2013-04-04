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
	/// Encapsulates a matrix of doubles.
	///</summary>
	public ref class MatrixDouble abstract
	{
		//===========================================================================================
	private:
		//===========================================================================================
		int _Order;
		array<double, 2>^ _Values;
		//===========================================================================================
	internal:
		//===========================================================================================
		static explicit operator double* (MatrixDouble^ matrix)
		{
			double* matrixData = new double[matrix->_Order * matrix->_Order];

			for(int x = 0; x < matrix->_Order; x++)
			{
				for(int y = 0; y < matrix->_Order; y++)
				{
					matrixData[(y * matrix->_Order) + x] = matrix->_Values[x, y];
				}
			}

			return matrixData;
		}
		//===========================================================================================
	protected:
		//===========================================================================================
		MatrixDouble();
		//===========================================================================================
		void Initialize(int order);
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Get or set the value at the specified x/y position.
		///</summary>
		property double default[int, int]
		{
			double get(int x, int y)
			{
				if (x < 0 || x >= _Order || y < 0 || y >= _Order)
					return 0.0;

				return _Values[x, y];
			}
			void set(int x, int y, double value)
			{
				if (x < 0 || x >= _Order || y < 0 || y >= _Order)
					return;

				_Values[x, y] = value;
			}
		}
		///==========================================================================================
		///<summary>
		/// Returns the order of the matrix.
		///</summary>
		property int Order
		{
			int get()
			{
				return _Order;
			}
		}
		//===========================================================================================
	};
	//==============================================================================================
}