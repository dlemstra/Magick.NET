// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
using System.Drawing.Imaging;
using System.IO;

namespace ImageMagick
{
    /// <summary>
    /// Extension methods for the <see cref="IMagickImageCollection{QuantumType}"/> interface.
    /// </summary>
    public static class IMagickImageCollectionExtensions
    {
        /// <summary>
        /// Converts this instance to a <see cref="Bitmap"/> using <see cref="ImageFormat.Tiff"/>.
        /// </summary>
        /// <param name="self">The image collection.</param>
        /// <typeparam name="TQuantumType">The quantum type.</typeparam>
        /// <returns>A <see cref="Bitmap"/> that has the format <see cref="ImageFormat.Tiff"/>.</returns>
        public static Bitmap ToBitmap<TQuantumType>(this IMagickImageCollection<TQuantumType> self)
            where TQuantumType : struct
        {
            Throw.IfNull(nameof(self), self);

            if (self.Count == 1)
                return self[0].ToBitmap();

            return ToBitmap(self, ImageFormat.Tiff);
        }

        /// <summary>
        /// Converts this instance to a <see cref="Bitmap"/> using the specified <see cref="ImageFormat"/>.
        /// Supported formats are: Gif, Icon, Tiff.
        /// </summary>
        /// <param name="self">The image collection.</param>
        /// <param name="imageFormat">The image format.</param>
        /// <typeparam name="TQuantumType">The quantum type.</typeparam>
        /// <returns>A <see cref="Bitmap"/> that has the specified <see cref="ImageFormat"/>.</returns>
        public static Bitmap ToBitmap<TQuantumType>(this IMagickImageCollection<TQuantumType> self, ImageFormat imageFormat)
            where TQuantumType : struct
        {
            Throw.IfNull(nameof(self), self);
            Throw.IfNull(nameof(imageFormat), imageFormat);

            var format = imageFormat.ToFormat();

            foreach (var image in self)
            {
                image.Settings.Format = format;
            }

            var memStream = new MemoryStream();
            self.Write(memStream);
            memStream.Position = 0;

            /* Do not dispose the memStream, the bitmap owns it. */
            return new Bitmap(memStream);
        }
    }
}