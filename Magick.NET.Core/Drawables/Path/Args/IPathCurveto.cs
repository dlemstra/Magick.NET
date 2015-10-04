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

namespace ImageMagick.Drawables.Paths
{
  ///<summary>
  /// Encapsulation of the PathCurveto object.
  ///</summary>
  public interface IPathCurveto
  {
    /// <summary>
    /// X ordinate of the end of the curve
    /// </summary>
    double X
    {
      get;
    }

    /// <summary>
    /// Y ordinate of the end of the curve
    /// </summary>
    double Y
    {
      get;
    }

    /// <summary>
    /// X ordinate of control point for curve beginning
    /// </summary>
    double X1
    {
      get;
    }

    /// <summary>
    /// Y ordinate of control point for curve beginning
    /// </summary>
    double Y1
    {
      get;
    }

    /// <summary>
    /// X ordinate of control point for curve ending
    /// </summary>
    double X2
    {
      get;
    }

    /// <summary>
    /// Y ordinate of control point for curve ending
    /// </summary>
    double Y2
    {
      get;
    }
  }
}