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
  /// Draws an ellipse on the image.
  /// </summary>
  public sealed class DrawableEllipse : IDrawable
  {
    void IDrawable.Draw(IDrawingWand wand)
    {
      if (wand != null)
        wand.Ellipse(OriginX, OriginY, RadiusX, RadiusY, StartDegrees, EndDegrees);
    }

    /// <summary>
    /// Creates a new DrawableEllipse instance.
    /// </summary>
    /// <param name="originX">The origin X coordinate.</param>
    /// <param name="originY">The origin Y coordinate.</param>
    /// <param name="radiusX">The X radius.</param>
    /// <param name="radiusY">The Y radius.</param>
    /// <param name="startDegrees">The starting degrees of rotation.</param>
    /// <param name="endDegrees">The ending degrees of rotation.</param>
    public DrawableEllipse(double originX, double originY, double radiusX, double radiusY, double startDegrees, double endDegrees)
    {
      OriginX = originX;
      OriginY = originY;
      RadiusX = radiusX;
      RadiusY = radiusY;
      StartDegrees = startDegrees;
      EndDegrees = endDegrees;
    }

    /// <summary>
    /// The ending degrees of rotation.
    /// </summary>
    public double EndDegrees
    {
      get;
      set;
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
    /// The X radius.
    /// </summary>
    public double RadiusX
    {
      get;
      set;
    }

    /// <summary>
    /// The Y radius.
    /// </summary>
    public double RadiusY
    {
      get;
      set;
    }

    /// <summary>
    /// The starting degrees of rotation.
    /// </summary>
    public double StartDegrees
    {
      get;
      set;
    }
  }
}