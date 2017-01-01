//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://magick.codeplex.com/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   http://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
// express or implied. See the License for the specific language governing permissions and
// limitations under the License.
//=================================================================================================

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
  public sealed partial class MagickImage
  {
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
    public Bitmap ToBitmap()
    {
      if (ColorSpace == ColorSpace.CMYK)
        ColorSpace = ColorSpace.sRGB;

      string mapping = "BGR";
      PixelFormat format = PixelFormat.Format24bppRgb;
      if (HasAlpha)
      {
        mapping = "BGRA";
        format = PixelFormat.Format32bppArgb;
      }

      using (PixelCollection pixels = GetPixels())
      {
        Bitmap bitmap = new Bitmap(Width, Height, format);
        BitmapData data = bitmap.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadWrite, format);
        IntPtr destination = data.Scan0;
        for (int y = 0; y < Height; y++)
        {
          byte[] bytes = pixels.ToByteArray(0, y, Width, 1, mapping);
          Marshal.Copy(bytes, 0, destination, bytes.Length);

          destination = new IntPtr(destination.ToInt64() + data.Stride);
        }

        bitmap.UnlockBits(data);
        return bitmap;
      }
    }

    /// <summary>
    /// Converts this instance to a <see cref="Bitmap"/> using the specified <see cref="ImageFormat"/>.
    /// Supported formats are: Bmp, Gif, Icon, Jpeg, Png, Tiff.
    /// </summary>
    /// <param name="imageFormat">The image format.</param>
    /// <returns>A <see cref="Bitmap"/> that has the specified <see cref="ImageFormat"/></returns>
    public Bitmap ToBitmap(ImageFormat imageFormat)
    {
      Format = MagickFormatInfo.GetFormat(imageFormat);

      MemoryStream memStream = new MemoryStream();
      Write(memStream);
      memStream.Position = 0;
      /* Do not dispose the memStream, the bitmap owns it. */
      return new Bitmap(memStream);
    }

#if !NET20
    /// <summary>
    /// Converts this instance to a <see cref="BitmapSource"/>.
    /// </summary>
    /// <returns>A <see cref="BitmapSource"/>.</returns>
    public BitmapSource ToBitmapSource()
    {
      string mapping = "RGB";
      MediaPixelFormat format = MediaPixelFormats.Rgb24;
      if (HasAlpha)
      {
        mapping = "BGRA";
        format = MediaPixelFormats.Bgra32;
      }

      if (ColorSpace == ColorSpace.CMYK)
      {
        mapping = "CMYK";
        format = MediaPixelFormats.Cmyk32;
      }

      int step = format.BitsPerPixel / 8;
      int stride = Width * step;

      using (PixelCollection pixels = GetPixels())
      {
        byte[] bytes = pixels.ToByteArray(mapping);
        return BitmapSource.Create(Width, Height, 96, 96, format, null, bytes, stride);
      }
    }
#endif
  }
}