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
  /// Applies a translation to the current coordinate system which moves the coordinate system
  /// origin to the specified coordinate.
  ///</summary>
  public sealed class DrawableTranslation : IDrawable
  {
    void IDrawable.Draw(IDrawingWand wand)
    {
      if (wand != null)
        wand.Translation(X, Y);
    }

    ///<summary>
    /// Creates a new DrawableTranslation instance.
    ///</summary>
    ///<param name="x">The X coordinate.</param>
    ///<param name="y">The Y coordinate.</param>
    public DrawableTranslation(double x, double y)
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