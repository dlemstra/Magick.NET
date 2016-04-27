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
  /// Specifies a text alignment to be applied when annotating with text.
  ///</summary>
  public sealed class DrawableTextAlignment : IDrawable
  {
    void IDrawable.Draw(IDrawingWand wand)
    {
      if (wand != null)
        wand.TextAlignment(Alignment);
    }

    ///<summary>
    /// Creates a new DrawableTextAntialias instance.
    ///</summary>
    ///<param name="alignment">Text alignment.</param>
    public DrawableTextAlignment(TextAlignment alignment)
    {
      Alignment = alignment;
    }

    ///<summary>
    /// True if text antialiasing is enabled otherwise false.
    ///</summary>
    public TextAlignment Alignment
    {
      get;
      set;
    }
  }
}