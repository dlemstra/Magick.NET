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

using System.Collections.Generic;

namespace ImageMagick
{
  /// <summary>
  /// Draws a bezier curve through a set of points on the image.
  /// </summary>
  public sealed class DrawableBezier : IDrawable
  {
    PointDCoordinates _Coordinates;

    void IDrawable.Draw(IDrawingWand wand)
    {
      if (wand != null)
        wand.Bezier(_Coordinates.ToList());
    }

    /// <summary>
    /// Creates a new DrawableBezier instance.
    /// </summary>
    /// <param name="coordinates">The coordinates.</param>
    public DrawableBezier(params PointD[] coordinates)
    {
      _Coordinates = new PointDCoordinates(coordinates, 3);
    }

    /// <summary>
    /// Creates a new DrawableBezier instance.
    /// </summary>
    /// <param name="coordinates">The coordinates.</param>
    public DrawableBezier(IEnumerable<PointD> coordinates)
    {
      _Coordinates = new PointDCoordinates(coordinates, 3);
    }

    /// <summary>
    /// The coordinates.
    /// </summary>
    public IEnumerable<PointD> Coordinates
    {
      get
      {
        return _Coordinates.ToList();
      }
    }
  }
}