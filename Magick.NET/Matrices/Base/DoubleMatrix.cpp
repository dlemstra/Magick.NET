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
#include "Stdafx.h"
#include "DoubleMatrix.h"

namespace ImageMagick
{	
	//==============================================================================================
	DoubleMatrix::DoubleMatrix()
	{
	}
	//==============================================================================================
	void DoubleMatrix::Initialize(int order)
	{
		_Values = gcnew array<double, 2>(order, order);
		_Order = order;
	}
	//==============================================================================================
	double* DoubleMatrix::CreateArray()
	{
		double* matrixData = new double[_Order * _Order];

		for(int x = 0; x < _Order; x++)
		{
			for(int y = 0; y < _Order; y++)
			{
				matrixData[(y * _Order) + x] = _Values[x, y];
			}
		}

		return matrixData;
	}
	//==============================================================================================
	double DoubleMatrix::default::get(int x, int y)
	{
		return GetValue(x, y);
	}
	//==============================================================================================
	void DoubleMatrix::default::set(int x, int y, double value)
	{
		SetValue(x, y, value);
	}
	//==============================================================================================
	int DoubleMatrix::Order::get()
	{
		return _Order;
	}
	//==============================================================================================
	double DoubleMatrix::GetValue(int x, int y)
	{
		if (x < 0 || x >= _Order || y < 0 || y >= _Order)
			return 0.0;

		return _Values[x, y];
	}
	//==============================================================================================
	void DoubleMatrix::SetValue(int x, int y, double value)
	{
		if (x < 0 || x >= _Order || y < 0 || y >= _Order)
			return;

		_Values[x, y] = value;
	}
	//==============================================================================================
}