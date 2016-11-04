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
  /// Controls whether stroked outlines are antialiased. Stroked outlines are antialiased by default.
  /// When antialiasing is disabled stroked pixels are thresholded to determine if the stroke color
  /// or underlying canvas color should be used.
  /// </summary>
  public sealed class DrawableStrokeAntialias : IDrawable
  {
    void IDrawable.Draw(IDrawingWand wand)
    {
      if (wand != null)
        wand.StrokeAntialias(IsEnabled);
    }

    /// <summary>
    /// Creates a new DrawableStrokeAntialias instance.
    /// </summary>
    /// <param name="isEnabled">True if stroke antialiasing is enabled otherwise false.</param>
    public DrawableStrokeAntialias(bool isEnabled)
    {
      IsEnabled = isEnabled;
    }

    /// <summary>
    /// True if stroke antialiasing is enabled otherwise false.
    /// </summary>
    public bool IsEnabled
    {
      get;
      set;
    }
  }
}