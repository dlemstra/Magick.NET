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
    /// <summary>
    /// Extension methods for the <see cref="Rectangle"/> struct.
    /// </summary>
    public static class RectangleExtensions
    {
        /// <summary>
        /// Convert the specified <see cref="Rectangle"/> to a <see cref="MagickGeometry"/>.
        /// </summary>
        /// <param name="self">The rectangle to use.</param>
        /// <returns>A <see cref="MagickGeometry"/> instance.</returns>
        public static MagickGeometry ToGeometry(this Rectangle self) => new MagickGeometry().FromRectangle(self);
    }
}

#endif