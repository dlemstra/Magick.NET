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

using System.Collections.Generic;
using ImageMagick.Drawables;
using ImageMagick.Drawables.Paths;

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Encapsulation of the PathCurvetoAbs object.
	///</summary>
	public sealed class PathCurvetoAbs : DrawableCoordinates<IPathCurveto>, IPathCurvetoAbs
	{
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the PathCurvetoAbs class.
		///</summary>
		///<param name="pathCurvetos">The coordinates to use.</param>
		public PathCurvetoAbs(params PathCurveto[] pathCurvetos)
			: base(pathCurvetos)
		{
		}
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the PathCurvetoAbs class.
		///</summary>
		///<param name="pathCurvetos">The coordinates to use.</param>
		public PathCurvetoAbs(IEnumerable<PathCurveto> pathCurvetos)
			: base(CastPathCurveto(pathCurvetos))
		{
		}
		//===========================================================================================
	}
	//==============================================================================================
}