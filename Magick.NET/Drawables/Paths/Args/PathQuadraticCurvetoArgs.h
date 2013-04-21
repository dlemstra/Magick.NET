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

#include "Base\PathArgsWrapper.h"

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Encapsulation of the PathQuadraticCurvetoArgs object.
	///</summary>
	public ref class PathQuadraticCurvetoArgs sealed : PathArgsWrapper<Magick::PathQuadraticCurvetoArgs>
	{
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the PathQuadraticCurvetoArgs class.
		///</summary>
		PathQuadraticCurvetoArgs();
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the PathQuadraticCurvetoArgs class.
		///</summary>
		PathQuadraticCurvetoArgs(double x, double y, double x1, double y1);
		///==========================================================================================
		property double X
		{
			double get()
			{
				return InternalValue->x();
			}
			void set(double value)
			{
				InternalValue->x(value);
			}
		}
		///==========================================================================================
		property double Y
		{
			double get()
			{
				return InternalValue->y();
			}
			void set(double value)
			{
				InternalValue->y(value);
			}
		}
		///==========================================================================================
		property double X1
		{
			double get()
			{
				return InternalValue->x1();
			}
			void set(double value)
			{
				InternalValue->x1(value);
			}
		}
		///==========================================================================================
		property double Y1
		{
			double get()
			{
				return InternalValue->y1();
			}
			void set(double value)
			{
				InternalValue->y1(value);
			}
		}
		//===========================================================================================
	};
	//==============================================================================================
}