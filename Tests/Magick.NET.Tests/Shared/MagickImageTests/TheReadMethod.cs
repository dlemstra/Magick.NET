// Copyright 2013-2019 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
using System.IO;
using System.Text;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        [TestClass]
        public partial class TheReadMethod
        {
            [TestMethod]
            public void ShouldUseTheCorrectReaderWhenReadingFromStreamAndFormatIsSet()
            {
                var bytes = Encoding.ASCII.GetBytes("%PDF-");
                var settings = new MagickReadSettings()
                {
                    Format = MagickFormat.Png,
                };

                using (MemoryStream stream = new MemoryStream(bytes))
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<MagickCorruptImageErrorException>(() =>
                        {
                            image.Read(stream, settings);
                        }, "ReadPNGImage");
                    }
                }
            }

            [TestMethod]
            public void ShouldUseTheCorrectReaderWhenReadingFromBytesAndFormatIsSet()
            {
                var bytes = Encoding.ASCII.GetBytes("%PDF-");
                var settings = new MagickReadSettings()
                {
                    Format = MagickFormat.Png,
                };

                using (IMagickImage image = new MagickImage())
                {
                    ExceptionAssert.Throws<MagickCorruptImageErrorException>(() =>
                    {
                        image.Read(bytes, settings);
                    }, "ReadPNGImage");
                }
            }

            [TestMethod]
            public void ShouldThrowAnExceptionWhenTheStreamIsEmpty()
            {
                using (MemoryStream memStream = new MemoryStream())
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentException>(() =>
                        {
                            image.Read(memStream);
                        }, "Value cannot be empty.");
                    }
                }
            }

            [TestMethod]
            public void ShouldResetTheFormatAfterReadingFile()
            {
                var readSettings = new MagickReadSettings()
                {
                    Format = MagickFormat.Png,
                };

                using (IMagickImage input = new MagickImage(Files.CirclePNG, readSettings))
                {
                    Assert.AreEqual(MagickFormat.Unknown, input.Settings.Format);
                }
            }

            [TestMethod]
            public void ShouldResetTheFormatAfterReadingStream()
            {
                var readSettings = new MagickReadSettings()
                {
                    Format = MagickFormat.Png,
                };

                using (var stream = File.OpenRead(Files.CirclePNG))
                {
                    using (IMagickImage input = new MagickImage(stream, readSettings))
                    {
                        Assert.AreEqual(MagickFormat.Unknown, input.Settings.Format);
                    }
                }
            }

            [TestMethod]
            public void ShouldResetTheFormatAfterReadingBytes()
            {
                var readSettings = new MagickReadSettings()
                {
                    Format = MagickFormat.Png,
                };

                var bytes = File.ReadAllBytes(Files.CirclePNG);

                using (IMagickImage input = new MagickImage(bytes, readSettings))
                {
                    Assert.AreEqual(MagickFormat.Unknown, input.Settings.Format);
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenStreamIsNotReadable()
            {
                ExceptionAssert.ThrowsArgumentException("stream", () =>
                {
                    using (TestStream testStream = new TestStream(false, true, true))
                    {
                        new MagickImage(testStream);
                    }
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenByteArrayIsNull()
            {
                using (IMagickImage image = new MagickImage())
                {
                    ExceptionAssert.ThrowsArgumentNullException("data", () =>
                    {
                        image.Read((byte[])null);
                    });
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenByteArrayIsEmpty()
            {
                using (IMagickImage image = new MagickImage())
                {
                    ExceptionAssert.ThrowsArgumentException("data", () =>
                    {
                        image.Read(new byte[0]);
                    });
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenFileIsNull()
            {
                using (IMagickImage image = new MagickImage())
                {
                    ExceptionAssert.ThrowsArgumentNullException("file", () =>
                    {
                        image.Read((FileInfo)null);
                    });
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenFileNameIsNull()
            {
                using (IMagickImage image = new MagickImage())
                {
                    ExceptionAssert.ThrowsArgumentNullException("fileName", () =>
                    {
                        image.Read((string)null);
                    });
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenStreamIsNull()
            {
                using (IMagickImage image = new MagickImage())
                {
                    ExceptionAssert.ThrowsArgumentNullException("stream", () =>
                    {
                        image.Read((Stream)null);
                    });
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenFileIsMissing()
            {
                using (IMagickImage image = new MagickImage())
                {
                    ExceptionAssert.Throws<MagickBlobErrorException>(() =>
                    {
                        image.Read(Files.Missing);
                    }, "error/blob.c/OpenBlob");
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenFileWithFormatIsMissing()
            {
                using (IMagickImage image = new MagickImage())
                {
                    ExceptionAssert.Throws<MagickBlobErrorException>(() =>
                    {
                        image.Read("png:" + Files.Missing);
                    }, "error/blob.c/OpenBlob");
                }
            }

            [TestMethod]
            public void ShouldReadImageFromBytes()
            {
                using (IMagickImage image = new MagickImage())
                {
                    image.Read(File.ReadAllBytes(Files.SnakewarePNG));
                    Assert.AreEqual(286, image.Width);
                    Assert.AreEqual(67, image.Height);
                }
            }

            [TestMethod]
            public void ShouldReadImageFromStream()
            {
                using (IMagickImage image = new MagickImage())
                {
                    using (FileStream fs = File.OpenRead(Files.SnakewarePNG))
                    {
                        image.Read(fs);
                        Assert.AreEqual(286, image.Width);
                        Assert.AreEqual(67, image.Height);
                        Assert.AreEqual(MagickFormat.Png, image.Format);
                    }
                }
            }

            [TestMethod]
            public void ShouldReadImageFromPngFileName()
            {
                using (IMagickImage image = new MagickImage())
                {
                    image.Read(Files.SnakewarePNG);
                    Assert.AreEqual(286, image.Width);
                    Assert.AreEqual(67, image.Height);
                    Assert.AreEqual(MagickFormat.Png, image.Format);
                }
            }

            [TestMethod]
            public void ShouldReadImageFromBuiltinRose()
            {
                using (IMagickImage image = new MagickImage())
                {
                    image.Read(Files.Builtin.Rose);
                    Assert.AreEqual(70, image.Width);
                    Assert.AreEqual(46, image.Height);
                    Assert.AreEqual(MagickFormat.Pnm, image.Format);
                }
            }

            [TestMethod]
            public void ShouldReadImageFromGifFileName()
            {
                using (IMagickImage image = new MagickImage())
                {
                    image.Read(Files.RoseSparkleGIF);
                    Assert.AreEqual("RöseSparkle.gif", Path.GetFileName(image.FileName));
                    Assert.AreEqual(70, image.Width);
                    Assert.AreEqual(46, image.Height);
                    Assert.AreEqual(MagickFormat.Gif, image.Format);
                }
            }

            [TestMethod]
            public void ShouldReadImageFromFileNameWithFormat()
            {
                using (IMagickImage image = new MagickImage())
                {
                    image.Read("png:" + Files.SnakewarePNG);
                    Assert.AreEqual(286, image.Width);
                    Assert.AreEqual(67, image.Height);
                    Assert.AreEqual(MagickFormat.Png, image.Format);
                }
            }

            [TestMethod]
            public void ShouldReadImageFromColor()
            {
                MagickColor red = new MagickColor("red");

                using (IMagickImage image = new MagickImage())
                {
                    image.Read(red, 50, 50);
                    Assert.AreEqual(50, image.Width);
                    Assert.AreEqual(50, image.Height);
                    ColorAssert.AreEqual(red, image, 10, 10);
                }
            }

            [TestMethod]
            public void ShouldReadImageFromXcColorName()
            {
                MagickColor red = new MagickColor("red");

                using (IMagickImage image = new MagickImage())
                {
                    image.Read("xc:red", 50, 50);
                    Assert.AreEqual(50, image.Width);
                    Assert.AreEqual(50, image.Height);
                    ColorAssert.AreEqual(red, image, 5, 5);
                }
            }

            [TestMethod]
            public void ShouldReadImageFromCmkyColorName()
            {
                MagickColor red = new MagickColor("cmyk(0%,100%,100%,0)");

                using (IMagickImage image = new MagickImage())
                {
                    image.Read(red, 50, 50);
                    Assert.AreEqual(50, image.Width);
                    Assert.AreEqual(50, image.Height);
                    Assert.AreEqual(ColorSpace.CMYK, image.ColorSpace);
                    image.Clamp();
                    ColorAssert.AreEqual(red, image, 10, 10);
                }
            }

            [TestMethod]
            public void ShouldReadImageFromSeekablePartialStream()
            {
                using (IMagickImage image = new MagickImage())
                {
                    using (FileStream fs = File.OpenRead(Files.ImageMagickJPG))
                    {
                        image.Read(fs);

                        fs.Position = 0;
                        using (PartialStream partialStream = new PartialStream(fs, true))
                        {
                            using (IMagickImage testImage = new MagickImage())
                            {
                                testImage.Read(partialStream);

                                Assert.AreEqual(image.Width, testImage.Width);
                                Assert.AreEqual(image.Height, testImage.Height);
                                Assert.AreEqual(image.Format, testImage.Format);
                                Assert.AreEqual(0.0, image.Compare(testImage, ErrorMetric.RootMeanSquared));
                            }
                        }
                    }
                }
            }

            [TestMethod]
            public void ShouldReadImageFromNonSeekablePartialStream()
            {
                using (IMagickImage image = new MagickImage())
                {
                    using (FileStream fs = File.OpenRead(Files.ImageMagickJPG))
                    {
                        image.Read(fs);

                        fs.Position = 0;
                        using (PartialStream partialStream = new PartialStream(fs, false))
                        {
                            using (IMagickImage testImage = new MagickImage())
                            {
                                testImage.Read(partialStream);

                                Assert.AreEqual(image.Width, testImage.Width);
                                Assert.AreEqual(image.Height, testImage.Height);
                                Assert.AreEqual(image.Format, testImage.Format);
                                Assert.AreEqual(0.0, image.Compare(testImage, ErrorMetric.RootMeanSquared));
                            }
                        }
                    }
                }
            }
        }
    }
}
