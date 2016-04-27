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
  ///<summary>
  /// Specifies a decoration to be applied when annotating with text.
  ///</summary>
  public sealed class DrawableTextDecoration : IDrawable
  {
    void IDrawable.Draw(IDrawingWand wand)
    {
      if (wand != null)
        wand.TextDecoration(Decoration);
    }

    ///<summary>
    /// Creates a new DrawableTextDecoration instance.
    ///</summary>
    ///<param name="decoration">The text decoration.</param>
    public DrawableTextDecoration(TextDecoration decoration)
    {
      Decoration = decoration;
    }

    ///<summary>
    /// The text decoration
    ///</summary>
    public TextDecoration Decoration
    {
      get;
      set;
    }
  }
}