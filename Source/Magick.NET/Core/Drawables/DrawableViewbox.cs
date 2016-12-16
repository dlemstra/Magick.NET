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
  /// Sets the overall canvas size to be recorded with the drawing vector data. Usually this will
  /// be specified using the same size as the canvas image. When the vector data is saved to SVG
  /// or MVG formats, the viewbox is use to specify the size of the canvas image that a viewer
  /// will render the vector data on.
  /// </summary>
  public sealed partial class DrawableViewbox : IDrawable
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="DrawableViewbox"/> class.
    /// </summary>
    /// <param name="upperLeftX">The upper left X coordinate.</param>
    /// <param name="upperLeftY">The upper left Y coordinate.</param>
    /// <param name="lowerRightX">The lower right X coordinate.</param>
    /// <param name="lowerRightY">The lower right Y coordinate.</param>
    public DrawableViewbox(double upperLeftX, double upperLeftY, double lowerRightX, double lowerRightY)
    {
      UpperLeftX = upperLeftX;
      UpperLeftY = upperLeftY;
      LowerRightX = lowerRightX;
      LowerRightY = lowerRightY;
    }

    /// <summary>
    /// Gets or sets the upper left X coordinate.
    /// </summary>
    public double LowerRightX
    {
      get;
      set;
    }

    /// <summary>
    /// Gets or sets the upper left Y coordinate.
    /// </summary>
    public double LowerRightY
    {
      get;
      set;
    }

    /// <summary>
    /// Gets or sets the upper left X coordinate.
    /// </summary>
    public double UpperLeftX
    {
      get;
      set;
    }

    /// <summary>
    /// Gets or sets the upper left Y coordinate.
    /// </summary>
    public double UpperLeftY
    {
      get;
      set;
    }

    /// <summary>
    /// Draws this instance with the drawing wand.
    /// </summary>
    /// <param name="wand">The want to draw on.</param>
    void IDrawable.Draw(IDrawingWand wand)
    {
      if (wand != null)
        wand.Viewbox(UpperLeftX, UpperLeftY, LowerRightX, LowerRightY);
    }
  }
}