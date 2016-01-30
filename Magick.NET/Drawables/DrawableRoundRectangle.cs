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

using System;
using ImageMagick.Drawables;

namespace ImageMagick
{
  ///<summary>
  /// Draws a rounted rectangle given two coordinates, x &amp; y corner radiuses and using the current
  /// stroke, stroke width, and fill settings.
  ///</summary>
  public sealed class DrawableRoundRectangle : IDrawable
  {
    void IDrawable.Draw(IDrawingWand wand)
    {
      if (wand != null)
        wand.RoundRectangle(CenterX, CenterY, Width, Height, CornerWidth, CornerHeight);
    }

    ///<summary>
    /// Creates a new DrawableRoundRectangle instance.
    ///</summary>
    ///<param name="centerX">The center X coordinate.</param>
    ///<param name="centerY">The center Y coordinate.</param>
    ///<param name="width">The width.</param>
    ///<param name="height">The height.</param>
    ///<param name="cornerWidth">The corner width.</param>
    ///<param name="cornerHeight">The corner height.</param>
    public DrawableRoundRectangle(double centerX, double centerY, double width, double height, double cornerWidth, double cornerHeight)
    {
      CenterX = centerX;
      CenterY = centerY;
      Width = width;
      Height = height;
      CornerWidth = cornerWidth;
      CornerHeight = cornerHeight;
    }

    ///<summary>
    /// The center X coordinate.
    ///</summary>
    public double CenterX
    {
      get;
      set;
    }

    ///<summary>
    /// The center Y coordinate.
    ///</summary>
    public double CenterY
    {
      get;
      set;
    }

    ///<summary>
    /// The corner height.
    ///</summary>
    public double CornerHeight
    {
      get;
      set;
    }

    ///<summary>
    /// The corner width.
    ///</summary>
    public double CornerWidth
    {
      get;
      set;
    }

    ///<summary>
    /// The height.
    ///</summary>
    public double Height
    {
      get;
      set;
    }

    ///<summary>
    /// The width.
    ///</summary>
    public double Width
    {
      get;
      set;
    }
  }
}