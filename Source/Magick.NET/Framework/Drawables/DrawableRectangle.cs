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

namespace ImageMagick
{
  /// <content>
  /// Contains code that is not compatible with .NET Core.
  /// </content>
  public sealed partial class DrawableRectangle
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="DrawableRectangle"/> class.
    /// </summary>
    /// <param name="rectangle">The <see cref="Rectangle"/> to use.</param>
    public DrawableRectangle(Rectangle rectangle)
    {
      UpperLeftX = rectangle.X;
      UpperLeftY = rectangle.Y;
      LowerRightX = rectangle.Right;
      LowerRightY = rectangle.Bottom;
    }

    /// <summary>
    /// Converts the specified <see cref="Rectangle"/> to an instance of this type.
    /// </summary>
    /// <param name="rectangle">The <see cref="Rectangle"/> to use.</param>
    public static explicit operator DrawableRectangle(Rectangle rectangle)
    {
      return FromRectangle(rectangle);
    }

    /// <summary>
    /// Converts the specified <see cref="Rectangle"/> to an instance of this type.
    /// </summary>
    /// <param name="rectangle">The <see cref="Rectangle"/> to use.</param>
    /// <returns>A <see cref="DrawableRectangle"/> instance.</returns>
    public static DrawableRectangle FromRectangle(Rectangle rectangle)
    {
      return new DrawableRectangle(rectangle);
    }
  }
}