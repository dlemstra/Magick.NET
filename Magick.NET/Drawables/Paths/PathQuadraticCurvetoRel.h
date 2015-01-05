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

#include "Args\PathQuadraticCurveto.h"
#include "Base\PathWrapper.h"

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Encapsulation of the PathQuadraticCurvetoRel object.
	///</summary>
	public ref class PathQuadraticCurvetoRel sealed : PathWrapper<Magick::PathQuadraticCurvetoRel>
	{
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the PathQuadraticCurvetoRel class.
		///</summary>
		///<param name="pathQuadraticCurveto">The coordinate to use.</param>
		PathQuadraticCurvetoRel(PathQuadraticCurveto^ pathQuadraticCurveto);
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the PathQuadraticCurvetoRel class.
		///</summary>
		///<param name="pathQuadraticCurvetos">The coordinates to use.</param>
		PathQuadraticCurvetoRel(IEnumerable<PathQuadraticCurveto^>^ pathQuadraticCurvetos);
		//===========================================================================================
	};
	//==============================================================================================
}