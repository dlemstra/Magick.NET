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

using System;
using System.Collections.Generic;
using ImageMagick.Drawables;
using ImageMagick.Drawables.Paths;

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Encapsulation of the PathSmoothCurvetoAbs object.
	///</summary>
	public sealed class PathSmoothCurvetoAbs : DrawableCoordinates<Coordinate>, IPathSmoothCurvetoAbs
	{
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the PathSmoothCurvetoAbs class.
		///</summary>
		///<param name="coordinates">The coordinates to use.</param>
		public PathSmoothCurvetoAbs(params Coordinate[] coordinates)
			: base(coordinates)
		{
			CheckCoordinateCount(2);
		}
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the PathSmoothCurvetoAbs class.
		///</summary>
		///<param name="coordinates">The coordinates to use.</param>
		public PathSmoothCurvetoAbs(IEnumerable<Coordinate> coordinates)
			: base(coordinates)
		{
			CheckCoordinateCount(2);
		}
		//===========================================================================================
	}
	//==============================================================================================
}