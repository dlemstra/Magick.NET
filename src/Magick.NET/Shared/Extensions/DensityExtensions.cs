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

namespace ImageMagick
{
    /// <summary>
    /// Extension methods for the <see cref="Density"/> class.
    /// </summary>
    public static class DensityExtensions
    {
        /// <summary>
        /// Returns a <see cref="MagickGeometry"/> based on the specified width and height.
        /// </summary>
        /// <param name="self">The density.</param>
        /// <param name="width">The width in cm or inches.</param>
        /// <param name="height">The height in cm or inches.</param>
        /// <returns>A <see cref="MagickGeometry"/> based on the specified width and height in cm or inches.</returns>
        public static IMagickGeometry ToGeometry(this Density self, double width, double height)
        {
            if (self == null)
                return null;

            int pixelWidth = (int)(width * self.X);
            int pixelHeight = (int)(height * self.Y);

            return new MagickGeometry(pixelWidth, pixelHeight);
        }
    }
}
