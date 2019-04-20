// Copyright 2013-2019 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

#if !NETSTANDARD

using System.Drawing;

namespace ImageMagick
{
    /// <content>
    /// Contains code that is not compatible with .NET Core.
    /// </content>
    public sealed partial class DrawableViewbox
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DrawableViewbox"/> class.
        /// </summary>
        /// <param name="rectangle">The <see cref="Rectangle"/> to use.</param>
        public DrawableViewbox(Rectangle rectangle)
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
        public static explicit operator DrawableViewbox(Rectangle rectangle) => FromRectangle(rectangle);

        /// <summary>
        /// Converts the specified <see cref="Rectangle"/> to an instance of this type.
        /// </summary>
        /// <param name="rectangle">The <see cref="Rectangle"/> to use.</param>
        /// <returns>A <see cref="DrawableViewbox"/> instance.</returns>
        public static DrawableViewbox FromRectangle(Rectangle rectangle) => new DrawableViewbox(rectangle);
    }
}

#endif