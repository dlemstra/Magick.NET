// Copyright 2013-2020 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
    public sealed partial class MagickGeometry
    {
        /// <summary>
        /// Converts the specified <see cref="Rectangle"/> to a <see cref="MagickColor"/> instance.
        /// </summary>
        /// <param name="rectangle">The <see cref="Rectangle"/> to convert.</param>
        /// <returns>A <see cref="MagickColor"/> instance.</returns>
        public MagickGeometry FromRectangle(Rectangle rectangle)
        {
            Initialize(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);
            return this;
        }

        /// <summary>
        /// Converts the value of this instance to an equivalent <see cref="Rectangle"/>.
        /// </summary>
        /// <returns>A <see cref="Color"/> instance.</returns>
        public Rectangle ToRectangle() => new Rectangle(X, Y, Width, Height);
    }
}

#endif