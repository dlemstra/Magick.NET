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

using ImageMagick.Drawables.Paths;

namespace ImageMagick
{
  ///<summary>
  /// Encapsulation of the PathQuadraticCurveto object.
  ///</summary>
  public sealed class PathQuadraticCurveto : IPathQuadraticCurveto
  {
    ///<summary>
    /// Initializes a new instance of the PathQuadraticCurveto class.
    ///</summary>
    public PathQuadraticCurveto()
    {
    }

    ///<summary>
    /// Initializes a new instance of the PathQuadraticCurveto class.
    ///</summary>
    public PathQuadraticCurveto(double x1, double y1, double x, double y)
    {
      X1 = x1;
      Y1 = y1;
      X = x;
      Y = y;
    }

    /// <summary>
    /// X ordinate of the control point
    /// </summary>
    public double X
    {
      get;
      set;
    }

    /// <summary>
    /// Y ordinate of the control point
    /// </summary>
    public double Y
    {
      get;
      set;
    }

    /// <summary>
    /// X ordinate of final point
    /// </summary>
    public double X1
    {
      get;
      set;
    }

    /// <summary>
    /// Y ordinate of final point
    /// </summary>
    public double Y1
    {
      get;
      set;
    }
  }
}