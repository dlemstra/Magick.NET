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

namespace ImageMagick
{
  ///<summary>
  /// Adjusts the scaling factor to apply in the horizontal and vertical directions to the current
  /// coordinate space.
  ///</summary>
  public sealed class DrawableScaling : IDrawable
  {
    void IDrawable.Draw(IDrawingWand wand)
    {
      if (wand != null)
        wand.Scaling(X, Y);
    }

    ///<summary>
    /// Creates a new DrawableScaling instance.
    ///</summary>
    ///<param name="x">Horizontal scale factor.</param>
    ///<param name="y">Vertical scale factor.</param>
    public DrawableScaling(double x, double y)
    {
      X = x;
      Y = y;
    }

    ///<summary>
    /// The X coordinate.
    ///</summary>
    public double X
    {
      get;
      set;
    }

    ///<summary>
    /// The Y coordinate.
    ///</summary>
    public double Y
    {
      get;
      set;
    }
  }
}