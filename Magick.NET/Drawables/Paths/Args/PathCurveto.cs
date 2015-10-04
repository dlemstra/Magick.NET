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
  /// Encapsulation of the PathCurveto object.
  ///</summary>
  public sealed class PathCurveto : IPathCurveto
  {
    ///<summary>
    /// Initializes a new instance of the PathCurveto class.
    ///</summary>
    public PathCurveto()
    {
    }

    ///<summary>
    /// Initializes a new instance of the PathCurveto class.
    ///</summary>
    public PathCurveto(double x1, double y1, double x2, double y2, double x, double y)
    {
      X1 = x1;
      Y1 = y1;
      X2 = x2;
      Y2 = y2;
      X = x;
      Y = y;
    }

    /// <summary>
    /// X ordinate of the end of the curve
    /// </summary>
    public double X
    {
      get;
      set;
    }

    /// <summary>
    /// Y ordinate of the end of the curve
    /// </summary>
    public double Y
    {
      get;
      set;
    }

    /// <summary>
    /// X ordinate of control point for curve beginning
    /// </summary>
    public double X1
    {
      get;
      set;
    }

    /// <summary>
    /// Y ordinate of control point for curve beginning
    /// </summary>
    public double Y1
    {
      get;
      set;
    }

    /// <summary>
    /// X ordinate of control point for curve ending
    /// </summary>
    public double X2
    {
      get;
      set;
    }

    /// <summary>
    /// Y ordinate of control point for curve ending
    /// </summary>
    public double Y2
    {
      get;
      set;
    }
  }
}