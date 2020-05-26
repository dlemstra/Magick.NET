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

#if !NET20
using System.Windows.Media.Imaging;
using MediaPixelFormat = System.Windows.Media.PixelFormat;
using MediaPixelFormats = System.Windows.Media.PixelFormats;
#endif

namespace ImageMagick
{
    /// <content>
    /// Contains code that is not compatible with .NET Core.
    /// </content>
    public static partial class IMagickImageExtentions
    {
#if !NET20
        /// <summary>
        /// Converts this instance to a <see cref="BitmapSource"/>.
        /// </summary>
        /// <param name="self">The image.</param>
        /// <typeparam name="TQuantumType">The quantum type.</typeparam>
        /// <returns>A <see cref="BitmapSource"/>.</returns>
        public static BitmapSource ToBitmapSource<TQuantumType>(this IMagickImage<TQuantumType> self)
            => ToBitmapSource(self, false);

        /// <summary>
        /// Converts this instance to a <see cref="BitmapSource"/>.
        /// </summary>
        /// <param name="self">The image.</param>
        /// <typeparam name="TQuantumType">The quantum type.</typeparam>
        /// <returns>A <see cref="BitmapSource"/>.</returns>
        public static BitmapSource ToBitmapSourceWithDensity<TQuantumType>(this IMagickImage<TQuantumType> self)
            => ToBitmapSource(self, true);

        private static BitmapSource ToBitmapSource<TQuantumType>(this IMagickImage<TQuantumType> self, bool useDensity)
        {
            Throw.IfNull(nameof(self), self);

            IMagickImage<TQuantumType> image = self;

            var mapping = "RGB";
            var format = MediaPixelFormats.Rgb24;

            try
            {
                if (self.ColorSpace == ColorSpace.CMYK && !image.HasAlpha)
                {
                    mapping = "CMYK";
                    format = MediaPixelFormats.Cmyk32;
                }
                else
                {
                    if (image.ColorSpace != ColorSpace.sRGB)
                    {
                        image = self.Clone();
                        image.ColorSpace = ColorSpace.sRGB;
                    }

                    if (image.HasAlpha)
                    {
                        mapping = "BGRA";
                        format = MediaPixelFormats.Bgra32;
                    }
                }

                var step = format.BitsPerPixel / 8;
                var stride = image.Width * step;

                using (var pixels = image.GetPixelsUnsafe())
                {
                    var bytes = pixels.ToByteArray(mapping);
                    var dpi = image.GetDefaultDensity(useDensity ? DensityUnit.PixelsPerInch : DensityUnit.Undefined);
                    return BitmapSource.Create(image.Width, image.Height, dpi.X, dpi.Y, format, null, bytes, stride);
                }
            }
            finally
            {
                if (!ReferenceEquals(self, image))
                    image.Dispose();
            }
        }
#endif
    }
}

#endif