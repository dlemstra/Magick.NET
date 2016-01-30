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

using System.Drawing;
using ImageMagick.Drawables;

namespace ImageMagick
{
  ///<summary>
  /// Sets the border color to be used for drawing bordered objects.
  ///</summary>
  public sealed class DrawableBorderColor : IDrawable
  {
    void IDrawable.Draw(IDrawingWand wand)
    {
      if (wand != null)
        wand.BorderColor(Color);
    }

    ///<summary>
    /// Creates a new DrawableColor instance.
    ///</summary>
    ///<param name="color">The color of the border.</param>
    public DrawableBorderColor(Color color)
      : this(new MagickColor(color))
    {
    }

    ///<summary>
    /// Creates a new DrawableColor instance.
    ///</summary>
    ///<param name="color">The color of the border.</param>
    public DrawableBorderColor(MagickColor color)
    {
      Throw.IfNull("color", color);

      Color = color;
    }

    ///<summary>
    /// The color to use.
    ///</summary>
    public MagickColor Color
    {
      get;
      set;
    }
  }
}