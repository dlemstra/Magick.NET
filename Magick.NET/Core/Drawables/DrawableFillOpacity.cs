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
  /// Sets the alpha to use when drawing using the fill color or fill texture.
  /// </summary>
  public sealed class DrawableFillOpacity : IDrawable
  {
    void IDrawable.Draw(IDrawingWand wand)
    {
      if (wand != null)
        wand.FillOpacity(Opacity.ToDouble() / 100);
    }

    /// <summary>
    /// Creates a new DrawableFillOpacity instance.
    /// </summary>
    /// <param name="opacity">The opacity.</param>
    public DrawableFillOpacity(Percentage opacity)
    {
      Opacity = opacity;
    }

    /// <summary>
    /// The alpha.
    /// </summary>
    public Percentage Opacity
    {
      get;
      set;
    }
  }
}