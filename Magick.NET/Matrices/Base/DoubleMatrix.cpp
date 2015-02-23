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
	void DoubleMatrix::Initialize(array<double>^ values)
	{
		Throw::IfFalse("values", (_Order * _Order) == values->Length, "Invalid number of values specified");

		for (int x = 0; x < _Order; x++)
		{
			for (int y = 0; y < _Order; y++)
			{
				_Values[x, y] = values[(y * _Order) + x];
			}
		}
	}
	//==============================================================================================
	DoubleMatrix::DoubleMatrix()
	{
	}
	//==============================================================================================
	void DoubleMatrix::Initialize(int order, array<double>^ values)
	{
		_Values = gcnew array<double, 2>(order, order);
		_Order = order;

		if (values != nullptr)
			Initialize(values);
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
	void DoubleMatrix::SetColumn(int x, ... array<double>^ values)
	{
		Throw::IfOutOfRange("x", x, _Order);
		Throw::IfNull("values", values);
		Throw::IfTrue("values", values->Length != _Order, "Invalid length");

		for (int y=0; y < _Order; y++)
		{
			_Values[x, y] = values[y];
		}
	}
	//==============================================================================================
	void DoubleMatrix::SetRow(int y, ... array<double>^ values)
	{
		Throw::IfOutOfRange("y", y, _Order);
		Throw::IfNull("values", values);
		Throw::IfTrue("values", values->Length != _Order, "Invalid length");

		for (int x=0; x < _Order; x++)
		{
			_Values[x, y] = values[x];
		}
	}
	//==============================================================================================
	void DoubleMatrix::SetValue(int x, int y, double value)
	{
		Throw::IfOutOfRange("x", x, _Order);
		Throw::IfOutOfRange("y", y, _Order);

		_Values[x, y] = value;
	}
	//==============================================================================================
}