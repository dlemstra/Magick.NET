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

#if !NETCORE

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public partial class TheConstructor
        {
            [TestClass]
            public class WithBitmap
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenBitmapIsNull()
                {
                    ExceptionAssert.Throws<ArgumentNullException>("bitmap", () =>
                    {
                        new MagickImage((Bitmap)null);
                    });
                }

                [TestMethod]
                public void ShouldUsePngFormatWhenBitmapIsPng()
                {
                    using (Bitmap bitmap = new Bitmap(Files.SnakewarePNG))
                    {
                        using (var image = new MagickImage(bitmap))
                        {
                            Assert.AreEqual(286, image.Width);
                            Assert.AreEqual(67, image.Height);
                            Assert.AreEqual(MagickFormat.Png, image.Format);
                        }
                    }
                }

                [TestMethod]
                public void ShouldUseBmpFormatWhenBitmapIsMemoryBmp()
                {
                    using (Bitmap bitmap = new Bitmap(50, 100, PixelFormat.Format24bppRgb))
                    {
                        Assert.AreEqual(bitmap.RawFormat, ImageFormat.MemoryBmp);

                        using (var image = new MagickImage(bitmap))
                        {
                            Assert.AreEqual(50, image.Width);
                            Assert.AreEqual(100, image.Height);
                            Assert.AreEqual(MagickFormat.Bmp3, image.Format);
                        }
                    }
                }

                [TestMethod]
                public void ShouldCreateCorrectImageWithByteArrayFromSystemDrawing()
                {
                    using (Image img = Image.FromFile(Files.Coders.PageTIF))
                    {
                        byte[] bytes = null;
                        using (MemoryStream memStream = new MemoryStream())
                        {
                            img.Save(memStream, ImageFormat.Tiff);
                            bytes = memStream.GetBuffer();
                        }

                        using (var image = new MagickImage(bytes))
                        {
                            image.Settings.Compression = CompressionMethod.Group4;

                            using (MemoryStream memStream = new MemoryStream())
                            {
                                image.Write(memStream);
                                memStream.Position = 0;

                                using (var before = new MagickImage(Files.Coders.PageTIF))
                                {
                                    using (var after = new MagickImage(memStream))
                                    {
                                        Assert.AreEqual(0.0, before.Compare(after, ErrorMetric.RootMeanSquared));
                                    }
                                }
                            }
                        }
                    }
                }
            }

            public partial class WithFileName
            {
                [TestMethod]
                public void ShouldUseBaseDirectoryOfCurrentAppDomainWhenFileNameStartsWithTilde()
                {
                    var exception = ExceptionAssert.Throws<MagickBlobErrorException>(() =>
                    {
                        new MagickImage("~/test.gif");
                    }, "error/blob.c/OpenBlob");

                    string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    Assert.IsTrue(exception.Message.Contains(baseDirectory));
                }

                [TestMethod]
                public void ShouldNotUseBaseDirectoryOfCurrentAppDomainWhenFileNameIsTilde()
                {
                    var exception = ExceptionAssert.Throws<MagickBlobErrorException>(() =>
                    {
                        new MagickImage("~");
                    }, "error/blob.c/OpenBlob");

                    Assert.IsTrue(exception.Message.Contains("~"));
                }
            }
        }
    }
}

#endif