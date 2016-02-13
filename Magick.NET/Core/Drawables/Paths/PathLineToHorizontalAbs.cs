//=================================================================================================
// Copyright 2013-2016 Dirk Lemstra <https://magick.codeplex.com/>
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

using ImageMagick.Drawables;

namespace ImageMagick
{
  ///<summary>
  /// Draws a horizontal line path from the current point to the target point using absolute
  /// coordinates. The target point then becomes the new current point.
  ///</summary>
  public sealed class PathLineToHorizontalAbs : IPath
  {
    void IPath.Draw(IDrawingWand wand)
    {
      if (wand != null)
        wand.PathLineToHorizontalAbs(X);
    }

    ///<summary>
    /// Initializes a new instance of the PathLineToHorizontalAbs class.
    ///</summary>
    ///<param name="x">The X coordinate.</param>
    public PathLineToHorizontalAbs(double x)
    {
      X = x;
    }

    /// <summary>
    /// The X coordinate.
    /// </summary>
    public double X
    {
      get;
      set;
    }
  }
}