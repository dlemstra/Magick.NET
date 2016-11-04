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
  /// <summary>
  /// Draws a circle on the image.
  /// </summary>
  public sealed class DrawableCircle : IDrawable
  {
    void IDrawable.Draw(IDrawingWand wand)
    {
      if (wand != null)
        wand.Circle(OriginX, OriginY, PerimeterX, PerimeterY);
    }

    /// <summary>
    /// Creates a new DrawableCircle instance.
    /// </summary>
    /// <param name="originX">The origin X coordinate.</param>
    /// <param name="originY">The origin Y coordinate.</param>
    /// <param name="perimeterX">The perimeter X coordinate.</param>
    /// <param name="perimeterY">The perimeter Y coordinate.</param>
    public DrawableCircle(double originX, double originY, double perimeterX, double perimeterY)
    {
      OriginX = originX;
      OriginY = originY;
      PerimeterX = perimeterX;
      PerimeterY = perimeterY;
    }

    /// <summary>
    /// The origin X coordinate.
    /// </summary>
    public double OriginX
    {
      get;
      set;
    }

    /// <summary>
    /// The origin X coordinate.
    /// </summary>
    public double OriginY
    {
      get;
      set;
    }

    /// <summary>
    /// The perimeter X coordinate.
    /// </summary>
    public double PerimeterX
    {
      get;
      set;
    }

    /// <summary>
    /// The perimeter X coordinate.
    /// </summary>
    public double PerimeterY
    {
      get;
      set;
    }
  }
}