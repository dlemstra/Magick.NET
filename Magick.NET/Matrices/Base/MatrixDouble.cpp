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
#include "stdafx.h"
#include "MatrixDouble.h"

namespace ImageMagick
{	
	//==============================================================================================
	MatrixDouble::MatrixDouble()
	{
	}
	//==============================================================================================
	void MatrixDouble::Initialize(int order)
	{
		_Values = gcnew array<double, 2>(order, order);
		_Order = order;
	}
	//==============================================================================================
	double* MatrixDouble::CreateArray()
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
}