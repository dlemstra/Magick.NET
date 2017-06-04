//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://magick.codeplex.com/>
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
  /// Draws an arc falling within a specified bounding rectangle on the image.
  /// </summary>
  public sealed class DrawableArc : IDrawable, IDrawingWand
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="DrawableArc"/> class.
    /// </summary>
    /// <param name="startX">The starting X coordinate of the bounding rectangle.</param>
    /// <param name="startY">The starting Y coordinate of thebounding rectangle.</param>
    /// <param name="endX">The ending X coordinate of the bounding rectangle.</param>
    /// <param name="endY">The ending Y coordinate of the bounding rectangle.</param>
    /// <param name="startDegrees">The starting degrees of rotation.</param>
    /// <param name="endDegrees">The ending degrees of rotation.</param>
    public DrawableArc(double startX, double startY, double endX, double endY, double startDegrees, double endDegrees)
    {
      StartX = startX;
      StartY = startY;
      EndX = endX;
      EndY = endY;
      StartDegrees = startDegrees;
      EndDegrees = endDegrees;
    }

    /// <summary>
    /// Gets or sets the ending degrees of rotation.
    /// </summary>
    public double EndDegrees
    {
      get;
      set;
    }

    /// <summary>
    /// Gets or sets the ending X coordinate of the bounding rectangle.
    /// </summary>
    public double EndX
    {
      get;
      set;
    }

    /// <summary>
    /// Gets or sets the ending Y coordinate of the bounding rectangle.
    /// </summary>
    public double EndY
    {
      get;
      set;
    }

    /// <summary>
    /// Gets or sets the starting degrees of rotation.
    /// </summary>
    public double StartDegrees
    {
      get;
      set;
    }

    /// <summary>
    /// Gets or sets the starting X coordinate of the bounding rectangle.
    /// </summary>
    public double StartX
    {
      get;
      set;
    }

    /// <summary>
    /// Gets or sets the starting Y coordinate of the bounding rectangle.
    /// </summary>
    public double StartY
    {
      get;
      set;
    }

    /// <summary>
    /// Draws this instance with the drawing wand.
    /// </summary>
    /// <param name="wand">The want to draw on.</param>
    void IDrawingWand.Draw(DrawingWand wand)
    {
      if (wand != null)
        wand.Arc(StartX, StartY, EndX, EndY, StartDegrees, EndDegrees);
    }
  }
}