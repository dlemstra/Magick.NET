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

using System.Drawing;

namespace ImageMagick
{
    /// <summary>
    /// Extension methods for the <see cref="IMagickGeometry"/> interface.
    /// </summary>
    public static class IMagickGeometryExtensions
    {
        /// <summary>
        /// Sets the values of this class using the specified <see cref="Rectangle"/>.
        /// </summary>
        /// /// <param name="self">The geometry.</param>
        /// <param name="rectangle">The <see cref="Rectangle"/> to convert.</param>
        public static void SetFromRectangle(this IMagickGeometry self, Rectangle rectangle)
            => self?.Initialize(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height);

        /// <summary>
        /// Converts the value of this instance to an equivalent <see cref="Rectangle"/>.
        /// </summary>
        /// <param name="self">The geometry.</param>
        /// <returns>A <see cref="Color"/> instance.</returns>
        public static Rectangle ToRectangle(this IMagickGeometry self)
        {
            if (self == null)
                return default;

            return new Rectangle(self.X, self.Y, self.Width, self.Height);
        }
    }
}