//=================================================================================================
// Copyright 2013-2015 Dirk Lemstra <https://magick.codeplex.com/>
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
using ImageMagick.Drawables;

namespace ImageMagick
{
  ///<summary>
  /// Encapsulation of the DrawableCompositeImage object.
  ///</summary>
  public sealed class DrawableText : IDrawableText
  {

    ///<summary>
    /// Creates a new DrawableText instance.
    ///</summary>
    ///<param name="x">The X coordinate.</param>
    ///<param name="y">The Y coordinate.</param>
    ///<param name="value">The text to draw.</param>
    public DrawableText(double x, double y, string value)
      : this(x, y, value, null)
    {
    }

    ///<summary>
    /// Creates a new DrawableText instance.
    ///</summary>
    ///<param name="x">The X coordinate.</param>
    ///<param name="y">The Y coordinate.</param>
    ///<param name="value">The text to draw.</param>
    ///<param name="encoding">The encoding of the text.</param>
    public DrawableText(double x, double y, string value, Encoding encoding)
    {
      X = x;
      Y = y;
      Text = value;
      Encoding = encoding;
    }

    ///<summary>
    /// The encoding of the text.
    ///</summary>
    public Encoding Encoding
    {
      get;
      set;
    }

    ///<summary>
    /// The text to draw.
    ///</summary>
    public string Text
    {
      get;
      set;
    }

    ///<summary>
    /// The X coordinate.
    ///</summary>
    public double X
    {
      get;
      set;
    }

    ///<summary>
    /// The Y coordinate.
    ///</summary>
    public double Y
    {
      get;
      set;
    }
  }
}