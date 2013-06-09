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
	/// Encapsulation of the PathCurveto object.
	///</summary>
	public ref class PathCurveto sealed : PathArgsWrapper<Magick::PathCurvetoArgs>
	{
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the PathCurveto class.
		///</summary>
		PathCurveto();
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the PathCurveto class.
		///</summary>
		PathCurveto(double x, double y, double x1, double y1, double x2, double y2);
		///==========================================================================================
		property double X
		{
			double get();
			void set(double value);
		}
		///==========================================================================================
		property double Y
		{
			double get();
			void set(double value);
		}
		///==========================================================================================
		property double X1
		{
			double get();
			void set(double value);
		}
		///==========================================================================================
		property double Y1
		{
			double get();
			void set(double value);
		}
		///==========================================================================================
		property double X2
		{
			double get();
			void set(double value);
		}
		///==========================================================================================
		property double Y2
		{
			double get();
			void set(double value);
		}
		//===========================================================================================
	};
	//==============================================================================================
}