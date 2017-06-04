//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://magick.codeplex.com/>
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

using System.Text;

namespace ImageMagick
{
  /// <summary>
  /// Encapsulation of the DrawableTextEncoding object.
  /// </summary>
  public sealed class DrawableTextEncoding : IDrawable, IDrawingWand
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="DrawableTextEncoding"/> class.
    /// </summary>
    /// <param name="encoding">Encoding to use.</param>
    public DrawableTextEncoding(Encoding encoding)
    {
      Throw.IfNull(nameof(encoding), encoding);

      Encoding = encoding;
    }

    /// <summary>
    /// Gets or sets the encoding of the text.
    /// </summary>
    public Encoding Encoding
    {
      get;
      set;
    }

    /// <summary>
    /// Draws this instance with the drawing wand.
    /// </summary>
    /// <param name="wand">The want to draw on.</param>
    void IDrawingWand.Draw(DrawingWand wand)
    {
      if (wand != null)
        wand.TextEncoding(Encoding);
    }
  }
}