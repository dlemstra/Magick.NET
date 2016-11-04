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
  /// Encapsulation of the DrawableDensity object.
  /// </summary>
  public sealed class DrawableDensity : IDrawable
  {
    void IDrawable.Draw(IDrawingWand wand)
    {
      if (wand != null)
        wand.Density(Density);
    }

    /// <summary>
    /// Creates a new DrawableDensity instance.
    /// </summary>
    /// <param name="density">The vertical and horizontal resolution.</param>
    public DrawableDensity(double density)
    {
      Density = new PointD(density);
    }

    /// <summary>
    /// Creates a new DrawableDensity instance.
    /// </summary>
    /// <param name="pointDensity">The vertical and horizontal resolution.</param>
    public DrawableDensity(PointD pointDensity)
    {
      Density = pointDensity;
    }

    /// <summary>
    /// The vertical and horizontal resolution.
    /// </summary>
    public PointD Density
    {
      get;
      set;
    }
  }
}