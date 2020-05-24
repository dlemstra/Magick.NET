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

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

#if !NET20
using System.Windows.Media.Imaging;
using MediaPixelFormat = System.Windows.Media.PixelFormat;
using MediaPixelFormats = System.Windows.Media.PixelFormats;
#endif

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

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
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public static void Read(this IMagickImage<QuantumType> self, Bitmap bitmap)
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
        /// <returns>A <see cref="Bitmap"/> that has the format <see cref="ImageFormat.Bmp"/>.</returns>
        public static Bitmap ToBitmap(this IMagickImage<QuantumType> self)
            => ToBitmap(self, BitmapDensity.Ignore);

        /// <summary>
        /// Converts this instance to a <see cref="Bitmap"/> using <see cref="ImageFormat.Bmp"/>.
        /// </summary>
        /// <param name="self">The image.</param>
        /// <param name="bitmapDensity">The bitmap density.</param>
        /// <returns>A <see cref="Bitmap"/> that has the format <see cref="ImageFormat.Bmp"/>.</returns>
        public static Bitmap ToBitmap(this IMagickImage<QuantumType> self, BitmapDensity bitmapDensity)
        {
            Throw.IfNull(nameof(self), self);

            IMagickImage<QuantumType> image = self;

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
                        byte[] bytes = pixels.ToByteArray(0, y, self.Width, 1, mapping);
                        Marshal.Copy(bytes, 0, destination, bytes.Length);

                        destination = new IntPtr(destination.ToInt64() + data.Stride);
                    }

                    bitmap.UnlockBits(data);

                    SetBitmapDensity(image, bitmap, bitmapDensity);
                    return bitmap;
                }
            }
            finally
            {
                if (!ReferenceEquals(self, image))
                    image.Dispose();
            }
        }

        /// <summary>
        /// Converts this instance to a <see cref="Bitmap"/> using the specified <see cref="ImageFormat"/>.
        /// Supported formats are: Bmp, Gif, Icon, Jpeg, Png, Tiff.
        /// </summary>
        /// <param name="self">The image.</param>
        /// <param name="imageFormat">The image format.</param>
        /// <returns>A <see cref="Bitmap"/> that has the specified <see cref="ImageFormat"/>.</returns>
        public static Bitmap ToBitmap(this IMagickImage<QuantumType> self, ImageFormat imageFormat)
            => ToBitmap(self, imageFormat, BitmapDensity.Ignore);

        /// <summary>
        /// Converts this instance to a <see cref="Bitmap"/> using the specified <see cref="ImageFormat"/>.
        /// Supported formats are: Bmp, Gif, Icon, Jpeg, Png, Tiff.
        /// </summary>
        /// <param name="self">The image.</param>
        /// <param name="imageFormat">The image format.</param>
        /// <param name="bitmapDensity">The bitmap density.</param>
        /// <returns>A <see cref="Bitmap"/> that has the specified <see cref="ImageFormat"/>.</returns>
        public static Bitmap ToBitmap(this IMagickImage<QuantumType> self, ImageFormat imageFormat, BitmapDensity bitmapDensity)
        {
            Throw.IfNull(nameof(self), self);
            Throw.IfNull(nameof(imageFormat), imageFormat);

            self.Format = imageFormat.ToFormat();

            MemoryStream memStream = new MemoryStream();
            self.Write(memStream);
            memStream.Position = 0;

            /* Do not dispose the memStream, the bitmap owns it. */
            var bitmap = new Bitmap(memStream);

            SetBitmapDensity(self, bitmap, bitmapDensity);

            return bitmap;
        }

#if !NET20
        /// <summary>
        /// Converts this instance to a <see cref="BitmapSource"/>.
        /// </summary>
        /// <param name="self">The image.</param>
        /// <returns>A <see cref="BitmapSource"/>.</returns>
        public static BitmapSource ToBitmapSource(this IMagickImage<QuantumType> self)
            => ToBitmapSource(self, BitmapDensity.Ignore);

        /// <summary>
        /// Converts this instance to a <see cref="BitmapSource"/>.
        /// </summary>
        /// <param name="self">The image.</param>
        /// <param name="bitmapDensity">The bitmap density.</param>
        /// <returns>A <see cref="BitmapSource"/>.</returns>
        public static BitmapSource ToBitmapSource(this IMagickImage<QuantumType> self, BitmapDensity bitmapDensity)
        {
            Throw.IfNull(nameof(self), self);

            IMagickImage<QuantumType> image = self;

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
                    var dpi = GetDpi(image, bitmapDensity);
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

        private static void SetBitmapDensity(IMagickImage<QuantumType> image, Bitmap bitmap, BitmapDensity bitmapDensity)
        {
            if (bitmapDensity == BitmapDensity.Use)
            {
                var dpi = GetDpi(image, bitmapDensity);
                bitmap.SetResolution((float)dpi.X, (float)dpi.Y);
            }
        }

        private static Density GetDpi(IMagickImage<QuantumType> image, BitmapDensity bitmapDensity)
        {
            if (bitmapDensity == BitmapDensity.Ignore || (image.Density.Units == DensityUnit.Undefined && image.Density.X == 0 && image.Density.Y == 0))
                return new Density(96);

            return image.Density.ChangeUnits(DensityUnit.PixelsPerInch);
        }
    }
}

#endif