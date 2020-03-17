﻿// Copyright 2013-2020 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

#if !NETSTANDARD1_3

using System;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

#if !NET20 && !NETSTANDARD2_0
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
    public sealed partial class MagickImage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MagickImage"/> class.
        /// </summary>
        /// <param name="bitmap">The bitmap to use.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public MagickImage(Bitmap bitmap)
          : this()
        {
            Read(bitmap);
        }

        /// <summary>
        /// Read single image frame.
        /// </summary>
        /// <param name="bitmap">The bitmap to read the image from.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public void Read(Bitmap bitmap)
        {
            Throw.IfNull(nameof(bitmap), bitmap);

            using (MemoryStream memStream = new MemoryStream())
            {
                if (IsSupportedImageFormat(bitmap.RawFormat))
                    bitmap.Save(memStream, bitmap.RawFormat);
                else
                    bitmap.Save(memStream, ImageFormat.Bmp);

                memStream.Position = 0;
                Read(memStream);
            }
        }

        /// <summary>
        /// Converts this instance to a <see cref="Bitmap"/> using <see cref="ImageFormat.Bmp"/>.
        /// </summary>
        /// <returns>A <see cref="Bitmap"/> that has the format <see cref="ImageFormat.Bmp"/>.</returns>
        public Bitmap ToBitmap() => ToBitmap(BitmapDensity.Ignore);

        /// <summary>
        /// Converts this instance to a <see cref="Bitmap"/> using <see cref="ImageFormat.Bmp"/>.
        /// </summary>
        /// <param name="bitmapDensity">The bitmap density.</param>
        /// <returns>A <see cref="Bitmap"/> that has the format <see cref="ImageFormat.Bmp"/>.</returns>
        [SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "False positive.")]
        public Bitmap ToBitmap(BitmapDensity bitmapDensity)
        {
            IMagickImage image = this;

            string mapping = "BGR";
            var format = PixelFormat.Format24bppRgb;

            try
            {
                if (image.ColorSpace != ColorSpace.sRGB)
                {
                    image = Clone();
                    image.ColorSpace = ColorSpace.sRGB;
                }

                if (image.HasAlpha)
                {
                    mapping = "BGRA";
                    format = PixelFormat.Format32bppArgb;
                }

                using (IPixelCollection pixels = image.GetPixelsUnsafe())
                {
                    var bitmap = new Bitmap(image.Width, image.Height, format);
                    var data = bitmap.LockBits(new Rectangle(0, 0, image.Width, image.Height), ImageLockMode.ReadWrite, format);
                    var destination = data.Scan0;
                    for (int y = 0; y < Height; y++)
                    {
                        byte[] bytes = pixels.ToByteArray(0, y, Width, 1, mapping);
                        Marshal.Copy(bytes, 0, destination, bytes.Length);

                        destination = new IntPtr(destination.ToInt64() + data.Stride);
                    }

                    bitmap.UnlockBits(data);

                    SetBitmapDensity(bitmap, bitmapDensity);
                    return bitmap;
                }
            }
            finally
            {
                if (!ReferenceEquals(this, image))
                    image.Dispose();
            }
        }

        /// <summary>
        /// Converts this instance to a <see cref="Bitmap"/> using the specified <see cref="ImageFormat"/>.
        /// Supported formats are: Bmp, Gif, Icon, Jpeg, Png, Tiff.
        /// </summary>
        /// <param name="imageFormat">The image format.</param>
        /// <returns>A <see cref="Bitmap"/> that has the specified <see cref="ImageFormat"/>.</returns>
        public Bitmap ToBitmap(ImageFormat imageFormat) => ToBitmap(imageFormat, BitmapDensity.Ignore);

        /// <summary>
        /// Converts this instance to a <see cref="Bitmap"/> using the specified <see cref="ImageFormat"/>.
        /// Supported formats are: Bmp, Gif, Icon, Jpeg, Png, Tiff.
        /// </summary>
        /// <param name="imageFormat">The image format.</param>
        /// <param name="bitmapDensity">The bitmap density.</param>
        /// <returns>A <see cref="Bitmap"/> that has the specified <see cref="ImageFormat"/>.</returns>
        public Bitmap ToBitmap(ImageFormat imageFormat, BitmapDensity bitmapDensity)
        {
            Throw.IfNull(nameof(imageFormat), imageFormat);

            Format = MagickFormatInfo.GetFormat(imageFormat);

            MemoryStream memStream = new MemoryStream();
            Write(memStream);
            memStream.Position = 0;

            /* Do not dispose the memStream, the bitmap owns it. */
            var bitmap = new Bitmap(memStream);

            SetBitmapDensity(bitmap, bitmapDensity);

            return bitmap;
        }

#if !NET20 && !NETSTANDARD2_0
        /// <summary>
        /// Converts this instance to a <see cref="BitmapSource"/>.
        /// </summary>
        /// <returns>A <see cref="BitmapSource"/>.</returns>
        public BitmapSource ToBitmapSource()
        {
            return ToBitmapSource(BitmapDensity.Ignore);
        }

        /// <summary>
        /// Converts this instance to a <see cref="BitmapSource"/>.
        /// </summary>
        /// <param name="bitmapDensity">The bitmap density.</param>
        /// <returns>A <see cref="BitmapSource"/>.</returns>
        [SuppressMessage("Reliability", "CA2000:Dispose objects before losing scope", Justification = "False positive.")]
        public BitmapSource ToBitmapSource(BitmapDensity bitmapDensity)
        {
            IMagickImage image = this;

            var mapping = "RGB";
            var format = MediaPixelFormats.Rgb24;

            try
            {
                if (ColorSpace == ColorSpace.CMYK && !image.HasAlpha)
                {
                    mapping = "CMYK";
                    format = MediaPixelFormats.Cmyk32;
                }
                else
                {
                    if (ColorSpace != ColorSpace.sRGB)
                    {
                        image = Clone();
                        image.ColorSpace = ColorSpace.sRGB;
                    }

                    if (image.HasAlpha)
                    {
                        mapping = "BGRA";
                        format = MediaPixelFormats.Bgra32;
                    }
                }

                var step = format.BitsPerPixel / 8;
                var stride = Width * step;

                using (IPixelCollection pixels = image.GetPixelsUnsafe())
                {
                    var bytes = pixels.ToByteArray(mapping);
                    var dpi = GetDpi(bitmapDensity);
                    return BitmapSource.Create(Width, Height, dpi.X, dpi.Y, format, null, bytes, stride);
                }
            }
            finally
            {
                if (!ReferenceEquals(this, image))
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

        private void SetBitmapDensity(Bitmap bitmap, BitmapDensity bitmapDensity)
        {
            if (bitmapDensity == BitmapDensity.Use)
            {
                var dpi = GetDpi(bitmapDensity);
                bitmap.SetResolution((float)dpi.X, (float)dpi.Y);
            }
        }

        private Density GetDpi(BitmapDensity bitmapDensity)
        {
            if (bitmapDensity == BitmapDensity.Ignore || (Density.Units == DensityUnit.Undefined && Density.X == 0 && Density.Y == 0))
                return new Density(96);

            return Density.ChangeUnits(DensityUnit.PixelsPerInch);
        }
    }
}

#endif
