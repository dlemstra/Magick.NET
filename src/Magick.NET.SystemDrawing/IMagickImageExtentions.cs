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

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace ImageMagick
{
    /// <content>
    /// Contains code that is not compatible with .NET Core.
    /// </content>
    public static partial class IMagickImageExtentions
    {
        /// <summary>
        /// Read single image frame.
        /// </summary>
        /// <param name="self">The image.</param>
        /// <param name="bitmap">The bitmap to read the image from.</param>
        /// <typeparam name="TQuantumType">The quantum type.</typeparam>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public static void Read<TQuantumType>(this IMagickImage<TQuantumType> self, Bitmap bitmap)
        {
            Throw.IfNull(nameof(self), self);
            Throw.IfNull(nameof(bitmap), bitmap);

            using (var memStream = new MemoryStream())
            {
                if (IsSupportedImageFormat(bitmap.RawFormat))
                    bitmap.Save(memStream, bitmap.RawFormat);
                else
                    bitmap.Save(memStream, ImageFormat.Bmp);

                memStream.Position = 0;
                self.Read(memStream);
            }
        }

        /// <summary>
        /// Converts this instance to a <see cref="Bitmap"/> using <see cref="ImageFormat.Bmp"/>.
        /// </summary>
        /// <param name="self">The image.</param>
        /// <typeparam name="TQuantumType">The quantum type.</typeparam>
        /// <returns>A <see cref="Bitmap"/> that has the format <see cref="ImageFormat.Bmp"/>.</returns>
        public static Bitmap ToBitmap<TQuantumType>(this IMagickImage<TQuantumType> self)
            => ToBitmap(self, false);

        /// <summary>
        /// Converts this instance to a <see cref="Bitmap"/> using <see cref="ImageFormat.Bmp"/>.
        /// </summary>
        /// <param name="self">The image.</param>
        /// <typeparam name="TQuantumType">The quantum type.</typeparam>
        /// <returns>A <see cref="Bitmap"/> that has the format <see cref="ImageFormat.Bmp"/>.</returns>
        public static Bitmap ToBitmapWithDensity<TQuantumType>(this IMagickImage<TQuantumType> self)
            => ToBitmap(self, true);

        /// <summary>
        /// Converts this instance to a <see cref="Bitmap"/> using the specified <see cref="ImageFormat"/>.
        /// Supported formats are: Bmp, Gif, Icon, Jpeg, Png, Tiff.
        /// </summary>
        /// <param name="self">The image.</param>
        /// <param name="imageFormat">The image format.</param>
        /// <typeparam name="TQuantumType">The quantum type.</typeparam>
        /// <returns>A <see cref="Bitmap"/> that has the specified <see cref="ImageFormat"/>.</returns>
        public static Bitmap ToBitmap<TQuantumType>(this IMagickImage<TQuantumType> self, ImageFormat imageFormat)
            => ToBitmap(self, imageFormat, false);

        /// <summary>
        /// Converts this instance to a <see cref="Bitmap"/> using the specified <see cref="ImageFormat"/>.
        /// Supported formats are: Bmp, Gif, Icon, Jpeg, Png, Tiff.
        /// </summary>
        /// <param name="self">The image.</param>
        /// <param name="imageFormat">The image format.</param>
        /// <typeparam name="TQuantumType">The quantum type.</typeparam>
        /// <returns>A <see cref="Bitmap"/> that has the specified <see cref="ImageFormat"/>.</returns>
        public static Bitmap ToBitmapWithDensity<TQuantumType>(this IMagickImage<TQuantumType> self, ImageFormat imageFormat)
            => ToBitmap(self, imageFormat, true);

        private static Bitmap ToBitmap<TQuantumType>(this IMagickImage<TQuantumType> self, bool useDensity)
        {
            Throw.IfNull(nameof(self), self);

            IMagickImage<TQuantumType> image = self;

            string mapping = "BGR";
            var format = PixelFormat.Format24bppRgb;

            try
            {
                if (image.ColorSpace != ColorSpace.sRGB)
                {
                    image = self.Clone();
                    image.ColorSpace = ColorSpace.sRGB;
                }

                if (image.HasAlpha)
                {
                    mapping = "BGRA";
                    format = PixelFormat.Format32bppArgb;
                }

                using (var pixels = image.GetPixelsUnsafe())
                {
                    var bitmap = new Bitmap(image.Width, image.Height, format);
                    var data = bitmap.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, format);
                    var destination = data.Scan0;
                    for (int y = 0; y < self.Height; y++)
                    {
                        var bytes = pixels.ToByteArray(0, y, self.Width, 1, mapping);
                        Marshal.Copy(bytes, 0, destination, bytes.Length);

                        destination = new IntPtr(destination.ToInt64() + data.Stride);
                    }

                    bitmap.UnlockBits(data);

                    SetBitmapDensity(self, bitmap, useDensity);
                    return bitmap;
                }
            }
            finally
            {
                if (!ReferenceEquals(self, image))
                    image.Dispose();
            }
        }

        private static Bitmap ToBitmap<TQuantumType>(this IMagickImage<TQuantumType> self, ImageFormat imageFormat, bool useDensity)
        {
            Throw.IfNull(nameof(self), self);
            Throw.IfNull(nameof(imageFormat), imageFormat);

            self.Format = imageFormat.ToFormat();

            var memStream = new MemoryStream();
            self.Write(memStream);
            memStream.Position = 0;

            /* Do not dispose the memStream, the bitmap owns it. */
            var bitmap = new Bitmap(memStream);
            SetBitmapDensity(self, bitmap, useDensity);

            return bitmap;
        }

        private static void SetBitmapDensity<TQuantumType>(IMagickImage<TQuantumType> image, Bitmap bitmap, bool useDpi)
        {
            if (useDpi)
            {
                var dpi = image.GetDefaultDensity(useDpi ? DensityUnit.PixelsPerInch : DensityUnit.Undefined);
                bitmap.SetResolution((float)dpi.X, (float)dpi.Y);
            }
        }

        private static bool IsSupportedImageFormat(ImageFormat format)
        {
            return
                format.Guid.Equals(ImageFormat.Bmp.Guid) ||
                format.Guid.Equals(ImageFormat.Gif.Guid) ||
                format.Guid.Equals(ImageFormat.Icon.Guid) ||
                format.Guid.Equals(ImageFormat.Jpeg.Guid) ||
                format.Guid.Equals(ImageFormat.Png.Guid) ||
                format.Guid.Equals(ImageFormat.Tiff.Guid);
        }
    }
}