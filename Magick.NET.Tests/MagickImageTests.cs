//=================================================================================================
// Copyright 2013-2016 Dirk Lemstra <https://magick.codeplex.com/>
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
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

#if !(NET20)
using System.Windows.Media.Imaging;
#endif

namespace Magick.NET.Tests
{
  public partial class MagickImageTests
  {
    private static void Test_ToBitmap(MagickImage image, ImageFormat format)
    {
      using (Bitmap bmp = image.ToBitmap(format))
      {
        Assert.AreEqual(format, bmp.RawFormat);
      }
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_Constructor_Bitmap()
    {
      ExceptionAssert.Throws<ArgumentNullException>(delegate ()
      {
        new MagickImage((Bitmap)null);
      });

      using (Bitmap bitmap = new Bitmap(Files.SnakewarePNG))
      {
        using (MagickImage image = new MagickImage(bitmap))
        {
          Assert.AreEqual(286, image.Width);
          Assert.AreEqual(67, image.Height);
          Assert.AreEqual(MagickFormat.Png, image.Format);
        }
      }

      using (Bitmap bitmap = new Bitmap(50, 100, PixelFormat.Format24bppRgb))
      {
        using (MagickImage image = new MagickImage(bitmap))
        {
          Assert.AreEqual(50, image.Width);
          Assert.AreEqual(100, image.Height);
          Assert.AreEqual(MagickFormat.Bmp3, image.Format);

          image.Dispose();
        }
      }
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_Bitmap()
    {
      using (Bitmap bitmap = new Bitmap(400, 400, PixelFormat.Format24bppRgb))
      {
        using (MagickImage image = new MagickImage(bitmap))
        {
          Assert.AreEqual(400, image.Width);
          Assert.AreEqual(400, image.Height);
        }
      }
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_Read_Bitmap()
    {
      using (MagickImage image = new MagickImage())
      {
        ExceptionAssert.Throws<ArgumentNullException>(delegate ()
        {
          image.Read((Bitmap)null);
        });

        using (Bitmap bitmap = new Bitmap(Files.SnakewarePNG))
        {
          image.Read(bitmap);
          Assert.AreEqual(286, image.Width);
          Assert.AreEqual(67, image.Height);
          Assert.AreEqual(MagickFormat.Png, image.Format);
        }

        using (Bitmap bitmap = new Bitmap(100, 50, PixelFormat.Format24bppRgb))
        {
          image.Read(bitmap);
          Assert.AreEqual(100, image.Width);
          Assert.AreEqual(50, image.Height);
          Assert.AreEqual(MagickFormat.Bmp3, image.Format);
        }
      }
    }

    [TestMethod, TestCategory(_Category)]
    public void Test_ToBitmap()
    {
      using (MagickImage image = new MagickImage(MagickColors.Red, 10, 10))
      {
        ExceptionAssert.Throws<NotSupportedException>(delegate ()
        {
          image.ToBitmap(ImageFormat.Exif);
        });

        Bitmap bitmap = image.ToBitmap();
        Assert.AreEqual(ImageFormat.MemoryBmp, bitmap.RawFormat);
        ColorAssert.AreEqual(MagickColors.Red, bitmap.GetPixel(0, 0));
        ColorAssert.AreEqual(MagickColors.Red, bitmap.GetPixel(5, 5));
        ColorAssert.AreEqual(MagickColors.Red, bitmap.GetPixel(9, 9));
        bitmap.Dispose();

        Test_ToBitmap(image, ImageFormat.Bmp);
        Test_ToBitmap(image, ImageFormat.Gif);
        Test_ToBitmap(image, ImageFormat.Icon);
        Test_ToBitmap(image, ImageFormat.Jpeg);
        Test_ToBitmap(image, ImageFormat.Png);
        Test_ToBitmap(image, ImageFormat.Tiff);
      }

      using (MagickImage image = new MagickImage(new MagickColor(0, Quantum.Max, Quantum.Max, 0), 10, 10))
      {
        Bitmap bitmap = image.ToBitmap();
        Assert.AreEqual(ImageFormat.MemoryBmp, bitmap.RawFormat);
        MagickColor color = MagickColor.FromRgba(0, 255, 255, 0);
        ColorAssert.AreEqual(color, bitmap.GetPixel(0, 0));
        ColorAssert.AreEqual(color, bitmap.GetPixel(5, 5));
        ColorAssert.AreEqual(color, bitmap.GetPixel(9, 9));
        bitmap.Dispose();
      }
    }

#if !(NET20)
    [TestMethod, TestCategory(_Category)]
    public void Test_ToBitmapSource()
    {
      byte[] pixels = new byte[600];

      using (MagickImage image = new MagickImage(MagickColors.Red, 10, 10))
      {
        BitmapSource bitmap = image.ToBitmapSource();
        bitmap.CopyPixels(pixels, 60, 0);

        Assert.AreEqual(255, pixels[0]);
        Assert.AreEqual(0, pixels[1]);
        Assert.AreEqual(0, pixels[2]);

        image.ColorSpace = ColorSpace.CMYK;

        bitmap = image.ToBitmapSource();
        bitmap.CopyPixels(pixels, 60, 0);

        Assert.AreEqual(0, pixels[0]);
        Assert.AreEqual(255, pixels[1]);
        Assert.AreEqual(255, pixels[2]);
        Assert.AreEqual(0, pixels[3]);

        image.AddProfile(ColorProfile.USWebCoatedSWOP);
        image.AddProfile(ColorProfile.SRGB);

        bitmap = image.ToBitmapSource();
        bitmap.CopyPixels(pixels, 60, 0);

        Assert.AreEqual(237, pixels[0]);
        Assert.AreEqual(28, pixels[1]);
        Assert.AreEqual(36, pixels[2]);
      }
    }
#endif
  }
}