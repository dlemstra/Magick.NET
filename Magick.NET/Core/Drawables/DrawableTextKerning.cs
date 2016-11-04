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
  /// Sets the spacing between characters in text.
  /// </summary>
  public sealed class DrawableTextKerning : IDrawable
  {
    void IDrawable.Draw(IDrawingWand wand)
    {
      if (wand != null)
        wand.TextKerning(Kerning);
    }

    /// <summary>
    /// Creates a new DrawableTextKerning instance.
    /// </summary>
    /// <param name="kerning">Kerning to use.</param>
    public DrawableTextKerning(double kerning)
    {
      Kerning = kerning;
    }

    /// <summary>
    /// Kerning to use.
    /// </summary>
    public double Kerning
    {
      get;
      set;
    }
  }
}