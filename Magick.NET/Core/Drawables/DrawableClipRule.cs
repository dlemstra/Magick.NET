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
  /// Sets the polygon fill rule to be used by the clipping path.
  /// </summary>
  public sealed class DrawableClipRule : IDrawable
  {
    void IDrawable.Draw(IDrawingWand wand)
    {
      if (wand != null)
        wand.ClipRule(FillRule);
    }

    /// <summary>
    /// Creates a new DrawableClipRule instance.
    /// </summary>
    /// <param name="fillRule">The rule to use when filling drawn objects.</param>
    public DrawableClipRule(FillRule fillRule)
    {
      FillRule = fillRule;
    }

    /// <summary>
    /// The rule to use when filling drawn objects.
    /// </summary>
    public FillRule FillRule
    {
      get;
      set;
    }
  }
}