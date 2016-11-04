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
  /// Sets the font pointsize to use when annotating with text.
  /// </summary>
  public sealed class DrawableFontPointSize : IDrawable
  {
    void IDrawable.Draw(IDrawingWand wand)
    {
      if (wand != null)
        wand.FontPointSize(PointSize);
    }

    /// <summary>
    /// Creates a new DrawablePointSize instance.
    /// </summary>
    /// <param name="pointSize">The point size.</param>
    public DrawableFontPointSize(double pointSize)
    {
      PointSize = pointSize;
    }

    /// <summary>
    /// The point size.
    /// </summary>
    public double PointSize
    {
      get;
      set;
    }
  }
}