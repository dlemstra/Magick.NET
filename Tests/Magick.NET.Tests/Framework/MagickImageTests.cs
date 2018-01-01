// Copyright 2013-2018 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

#if !NETCOREAPP1_1

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
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

using System.Windows.Media.Imaging;
using MediaPixelFormats = System.Windows.Media.PixelFormats;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        [TestMethod]
        public void Constructor_BitmapIsNull_ThrowsException()
        {
            ExceptionAssert.ThrowsArgumentNullException("bitmap", () =>
            {
                new MagickImage((Bitmap)null);
            });
        }

        [TestMethod]
        public void Constructor_WithPngBitmap_CreatesPngImage()
        {
            using (Bitmap bitmap = new Bitmap(Files.SnakewarePNG))
            {
                using (IMagickImage image = new MagickImage(bitmap))
                {
                    Assert.AreEqual(286, image.Width);
                    Assert.AreEqual(67, image.Height);
                    Assert.AreEqual(MagickFormat.Png, image.Format);
                }
            }
        }

        [TestMethod]
        public void Constructor_WithMemoryBmp_CreatesBmpImage()
        {
            using (Bitmap bitmap = new Bitmap(50, 100, PixelFormat.Format24bppRgb))
            {
                Assert.AreEqual(bitmap.RawFormat, ImageFormat.MemoryBmp);

                using (IMagickImage image = new MagickImage(bitmap))
                {
                    Assert.AreEqual(50, image.Width);
                    Assert.AreEqual(100, image.Height);
                    Assert.AreEqual(MagickFormat.Bmp3, image.Format);
                }
            }
        }

        [TestMethod]
        public void Constructor_WithByteArrayFromSystemDrawing_CreatesCorrectImage()
        {
            using (Image img = Image.FromFile(Files.Coders.PageTIF))
            {
                byte[] bytes = null;
                using (MemoryStream memStream = new MemoryStream())
                {
                    img.Save(memStream, ImageFormat.Tiff);
                    bytes = memStream.GetBuffer();
                }

                using (IMagickImage image = new MagickImage(bytes))
                {
                    image.Settings.Compression = Compression.Group4;

                    using (MemoryStream memStream = new MemoryStream())
                    {
                        image.Write(memStream);
                        memStream.Position = 0;

                        using (IMagickImage before = new MagickImage(Files.Coders.PageTIF))
                        {
                            using (IMagickImage after = new MagickImage(memStream))
                            {
                                Assert.AreEqual(0.0, before.Compare(after, ErrorMetric.RootMeanSquared));
                            }
                        }
                    }
                }
            }
        }

        [TestMethod]
        public void Read_BitmapIsNull_ThrowsException()
        {
            using (IMagickImage image = new MagickImage())
            {
                ExceptionAssert.ThrowsArgumentNullException("bitmap", () =>
                {
                    image.Read((Bitmap)null);
                });
            }
        }

        [TestMethod]
        public void Read_WithPngBitmap_CreatesPngImage()
        {
            using (Bitmap bitmap = new Bitmap(Files.SnakewarePNG))
            {
                using (IMagickImage image = new MagickImage())
                {
                    image.Read(bitmap);

                    Assert.AreEqual(286, image.Width);
                    Assert.AreEqual(67, image.Height);
                    Assert.AreEqual(MagickFormat.Png, image.Format);
                }
            }
        }

        [TestMethod]
        public void Read_WithMemoryBmp_CreatesBmpImage()
        {
            using (Bitmap bitmap = new Bitmap(100, 50, PixelFormat.Format24bppRgb))
            {
                Assert.AreEqual(bitmap.RawFormat, ImageFormat.MemoryBmp);

                using (IMagickImage image = new MagickImage())
                {
                    image.Read(bitmap);

                    Assert.AreEqual(100, image.Width);
                    Assert.AreEqual(50, image.Height);
                    Assert.AreEqual(MagickFormat.Bmp3, image.Format);
                }
            }
        }

        [TestMethod]
        public void Read_FileNameStartsWithTilde_UsesBaseDirectoryOfCurrentAppDomain()
        {
            using (IMagickImage image = new MagickImage())
            {
                Exception exception = ExceptionAssert.Throws<MagickBlobErrorException>(() =>
                {
                    image.Read("~/test.gif");
                }, "error/blob.c/OpenBlob");

                string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                Assert.IsTrue(exception.Message.Contains(baseDirectory));
            }
        }

        [TestMethod]
        public void Read_FileNameSetToTilde_DoesNotUseBaseDirectoryOfCurrentAppDomain()
        {
            using (IMagickImage image = new MagickImage())
            {
                Exception exception = ExceptionAssert.Throws<MagickBlobErrorException>(() =>
                {
                    image.Read("~");
                }, "error/blob.c/OpenBlob");

                Assert.IsTrue(exception.Message.Contains("~"));
            }
        }

        [TestMethod]
        public void ToBitmap_ImageFormatIsExif_ThrowsException()
        {
            ToBitmap_UnsupportedImageFormat_ThrowsException(ImageFormat.Exif);
        }

        [TestMethod]
        public void ToBitmap_ImageFormatIsEmf_ThrowsException()
        {
            ToBitmap_UnsupportedImageFormat_ThrowsException(ImageFormat.Emf);
        }

        [TestMethod]
        public void ToBitmap_ImageFormatIsWmf_ThrowsException()
        {
            ToBitmap_UnsupportedImageFormat_ThrowsException(ImageFormat.Wmf);
        }

        [TestMethod]
        public void ToBitmap_ImageFormatIsBmp_ReturnsBitmap()
        {
            ToBitmap_SupportedFormat_ReturnsBitmap(ImageFormat.Bmp);
        }

        [TestMethod]
        public void ToBitmap_ImageFormatIsGif_ReturnsBitmap()
        {
            ToBitmap_SupportedFormat_ReturnsBitmap(ImageFormat.Gif);
        }

        [TestMethod]
        public void ToBitmap_ImageFormatIsIcon_ReturnsBitmap()
        {
            ToBitmap_SupportedFormat_ReturnsBitmap(ImageFormat.Icon);
        }

        [TestMethod]
        public void ToBitmap_ImageFormatIsJpeg_ReturnsBitmap()
        {
            ToBitmap_SupportedFormat_ReturnsBitmap(ImageFormat.Jpeg);
        }

        [TestMethod]
        public void ToBitmap_ImageFormatIsPng_ReturnsBitmap()
        {
            ToBitmap_SupportedFormat_ReturnsBitmap(ImageFormat.Png);
        }

        [TestMethod]
        public void ToBitmap_ImageFormatIsTiff_ReturnsBitmap()
        {
            ToBitmap_SupportedFormat_ReturnsBitmap(ImageFormat.Tiff);
        }

        [TestMethod]
        public void ToBitmap_CmykImage_ReturnsBitmapWithCorrectColors()
        {
            using (IMagickImage image = new MagickImage(Files.CMYKJPG))
            {
                using (Bitmap bitmap = image.ToBitmap())
                {
                    Assert.AreEqual(ImageFormat.MemoryBmp, bitmap.RawFormat);

                    ColorAssert.AreEqual(new MagickColor("#26ffb1"), bitmap.GetPixel(1142, 42));
                }
            }
        }

        [TestMethod]
        public void ToBitmapSource_RgbImage_ReturnsBitmapSourceWithRgb24Format()
        {
            byte[] pixels = new byte[150];

            using (IMagickImage image = new MagickImage(MagickColors.Red, 5, 10))
            {
                BitmapSource bitmapSource = image.ToBitmapSource();

                Assert.AreEqual(MediaPixelFormats.Rgb24, bitmapSource.Format);
                Assert.AreEqual(5, bitmapSource.Width);
                Assert.AreEqual(10, bitmapSource.Height);

                bitmapSource.CopyPixels(pixels, 15, 0);

                Assert.AreEqual(255, pixels[0]);
                Assert.AreEqual(0, pixels[1]);
                Assert.AreEqual(0, pixels[2]);
            }
        }

        [TestMethod]
        public void ToBitmapSource_CmykImage_ReturnsBitmapSourceWithCmyk32Format()
        {
            byte[] pixels = new byte[200];

            using (IMagickImage image = new MagickImage(MagickColors.Red, 10, 5))
            {
                image.ColorSpace = ColorSpace.CMYK;

                BitmapSource bitmapSource = image.ToBitmapSource();

                Assert.AreEqual(MediaPixelFormats.Cmyk32, bitmapSource.Format);
                Assert.AreEqual(10, bitmapSource.Width);
                Assert.AreEqual(5, bitmapSource.Height);

                bitmapSource.CopyPixels(pixels, 40, 0);

                Assert.AreEqual(0, pixels[0]);
                Assert.AreEqual(255, pixels[1]);
                Assert.AreEqual(255, pixels[2]);
                Assert.AreEqual(0, pixels[3]);
            }
        }

        [TestMethod]
        public void ToBitmapSource_RgbaImage_ReturnsBitmapSourceWithBgra32Format()
        {
            byte[] pixels = new byte[200];

            using (IMagickImage image = new MagickImage(MagickColors.Red, 5, 10))
            {
                image.HasAlpha = true;

                BitmapSource bitmapSource = image.ToBitmapSource();

                Assert.AreEqual(MediaPixelFormats.Bgra32, bitmapSource.Format);
                Assert.AreEqual(5, bitmapSource.Width);
                Assert.AreEqual(10, bitmapSource.Height);

                bitmapSource.CopyPixels(pixels, 20, 0);

                Assert.AreEqual(0, pixels[0]);
                Assert.AreEqual(0, pixels[1]);
                Assert.AreEqual(255, pixels[2]);
                Assert.AreEqual(255, pixels[3]);
            }
        }

        private static void ToBitmap_UnsupportedImageFormat_ThrowsException(ImageFormat imageFormat)
        {
            using (IMagickImage image = new MagickImage(MagickColors.Red, 10, 10))
            {
                ExceptionAssert.Throws<NotSupportedException>(() =>
                {
                    image.ToBitmap(imageFormat);
                });
            }
        }

        private static void ToBitmap_SupportedFormat_ReturnsBitmap(ImageFormat imageFormat)
        {
            using (IMagickImage image = new MagickImage(MagickColors.Red, 10, 10))
            {
                using (Bitmap bitmap = image.ToBitmap(imageFormat))
                {
                    Assert.AreEqual(imageFormat, bitmap.RawFormat);

                    // Cannot test JPEG due to rounding issues.
                    if (imageFormat != ImageFormat.Jpeg)
                    {
                        ColorAssert.AreEqual(MagickColors.Red, bitmap.GetPixel(0, 0));
                        ColorAssert.AreEqual(MagickColors.Red, bitmap.GetPixel(5, 5));
                        ColorAssert.AreEqual(MagickColors.Red, bitmap.GetPixel(9, 9));
                    }
                }
            }
        }
    }
}

#endif