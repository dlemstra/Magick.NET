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
	/// Encapsulation of the PathCurvetoArgs object.
	///</summary>
	public ref class PathCurvetoArgs sealed : PathArgsWrapper<Magick::PathCurvetoArgs>
	{
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the PathCurvetoArgs class.
		///</summary>
		PathCurvetoArgs();
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the PathCurvetoArgs class.
		///</summary>
		PathCurvetoArgs(double x, double y, double x1, double y1, double x2, double y2);
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
		///==========================================================================================
		property double X2
		{
			double get()
			{
				return InternalValue->x2();
			}
			void set(double value)
			{
				InternalValue->x2(value);
			}
		}
		///==========================================================================================
		property double Y2
		{
			double get()
			{
				return InternalValue->y2();
			}
			void set(double value)
			{
				InternalValue->y2(value);
			}
		}
		//===========================================================================================
	};
	//==============================================================================================
}