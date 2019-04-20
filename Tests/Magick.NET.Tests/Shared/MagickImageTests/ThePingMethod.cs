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

using System.IO;
using System.Text;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class ThePingMethod
        {
            [TestClass]
            public class WithByteArray
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.ThrowsArgumentNullException("data", () =>
                        {
                            image.Ping((byte[])null);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.ThrowsArgumentException("data", () =>
                        {
                            image.Ping(new byte[] { });
                        });
                    }
                }

                [TestMethod]
                public void ShouldPingImage()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        image.Ping(File.ReadAllBytes(Files.SnakewarePNG));
                        Assert.AreEqual(286, image.Width);
                        Assert.AreEqual(67, image.Height);
                    }
                }
            }

            [TestClass]
            public class WithByteArrayAndOffset
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.ThrowsArgumentNullException("data", () =>
                        {
                            image.Ping((byte[])null, 0, 0);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.ThrowsArgumentException("data", () =>
                        {
                            image.Ping(new byte[] { }, 0, 0);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenOffsetIsNegative()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.ThrowsArgumentException("offset", () =>
                        {
                            image.Ping(new byte[] { 215 }, -1, 0);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenCountIsZero()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.ThrowsArgumentException("count", () =>
                        {
                            image.Ping(new byte[] { 215 }, 0, 0);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenCountIsNegative()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.ThrowsArgumentException("count", () =>
                        {
                            image.Ping(new byte[] { 215 }, 0, -1);
                        });
                    }
                }

                [TestMethod]
                public void ShouldPingImage()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        var fileBytes = File.ReadAllBytes(Files.SnakewarePNG);
                        var bytes = new byte[fileBytes.Length + 10];
                        fileBytes.CopyTo(bytes, 10);

                        image.Ping(bytes, 10, bytes.Length - 10);
                        Assert.AreEqual(286, image.Width);
                        Assert.AreEqual(67, image.Height);
                    }
                }
            }

            [TestClass]
            public class WithByteArrayAndOffsetAndMagickReadSettings
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    var settings = new MagickReadSettings();

                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.ThrowsArgumentNullException("data", () =>
                        {
                            image.Ping(null, 0, 0, settings);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    var settings = new MagickReadSettings();

                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.ThrowsArgumentException("data", () =>
                        {
                            image.Ping(new byte[] { }, 0, 0, settings);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenOffsetIsNegative()
                {
                    var settings = new MagickReadSettings();

                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.ThrowsArgumentException("offset", () =>
                        {
                            image.Ping(new byte[] { 215 }, -1, 0, settings);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenCountIsZero()
                {
                    var settings = new MagickReadSettings();

                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.ThrowsArgumentException("count", () =>
                        {
                            image.Ping(new byte[] { 215 }, 0, 0, settings);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenCountIsNegative()
                {
                    var settings = new MagickReadSettings();

                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.ThrowsArgumentException("count", () =>
                        {
                            image.Ping(new byte[] { 215 }, 0, -1, settings);
                        });
                    }
                }

                [TestMethod]
                public void ShouldPingImage()
                {
                    var settings = new MagickReadSettings();

                    var fileBytes = File.ReadAllBytes(Files.SnakewarePNG);
                    var bytes = new byte[fileBytes.Length + 10];
                    fileBytes.CopyTo(bytes, 10);

                    using (IMagickImage image = new MagickImage())
                    {
                        image.Ping(bytes, 10, bytes.Length - 10, settings);
                        Assert.AreEqual(286, image.Width);
                        Assert.AreEqual(67, image.Height);
                    }
                }

                [TestMethod]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    var bytes = File.ReadAllBytes(Files.CirclePNG);

                    using (IMagickImage image = new MagickImage())
                    {
                        image.Ping(bytes, 0, bytes.Length, null);
                    }
                }
            }

            [TestClass]
            public class WithByteArrayAndMagickReadSettings
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    var settings = new MagickReadSettings();

                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.ThrowsArgumentNullException("data", () =>
                        {
                            image.Ping((byte[])null, settings);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    var settings = new MagickReadSettings();

                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.ThrowsArgumentException("data", () =>
                        {
                            image.Ping(new byte[] { }, settings);
                        });
                    }
                }

                [TestMethod]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        image.Ping(File.ReadAllBytes(Files.CirclePNG), null);
                    }
                }

                [TestMethod]
                public void ShouldUseTheCorrectReaderWhenFormatIsSet()
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
                            image.Ping(bytes, settings);
                        }, "ReadPNGImage");
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

                    using (IMagickImage image = new MagickImage())
                    {
                        image.Ping(bytes, readSettings);

                        Assert.AreEqual(MagickFormat.Unknown, image.Settings.Format);
                    }
                }
            }

            [TestClass]
            public class WithFileInfo
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenFileInfoIsNull()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.ThrowsArgumentNullException("file", () =>
                        {
                            image.Ping((FileInfo)null);
                        });
                    }
                }
            }

            [TestClass]
            public class WithFileInfoAndMagickReadSettings
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenFileInfoIsNull()
                {
                    var settings = new MagickReadSettings();

                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.ThrowsArgumentNullException("file", () =>
                        {
                            image.Ping((FileInfo)null, settings);
                        });
                    }
                }

                [TestMethod]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        image.Ping(new FileInfo(Files.CirclePNG), null);
                    }
                }
            }

            [TestClass]
            public class WithFileName
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsNull()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.ThrowsArgumentNullException("fileName", () =>
                        {
                            image.Ping((string)null);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.ThrowsArgumentException("fileName", () =>
                        {
                            image.Ping(string.Empty);
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
                            image.Ping(Files.Missing);
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
                            image.Ping("png:" + Files.Missing);
                        }, "error/blob.c/OpenBlob");
                    }
                }

                [TestMethod]
                public void ShouldPingImage()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        image.Ping(Files.SnakewarePNG);
                        Assert.AreEqual(286, image.Width);
                        Assert.AreEqual(67, image.Height);
                        Assert.AreEqual(MagickFormat.Png, image.Format);
                    }
                }

                [TestMethod]
                public void ShouldPingBuiltinImage()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        image.Ping(Files.Builtin.Rose);
                        Assert.AreEqual(70, image.Width);
                        Assert.AreEqual(46, image.Height);
                        Assert.AreEqual(MagickFormat.Pnm, image.Format);
                    }
                }

                [TestMethod]
                public void ShouldPingImageWithNonAsciiFileName()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        image.Ping(Files.RoseSparkleGIF);
                        Assert.AreEqual("RöseSparkle.gif", Path.GetFileName(image.FileName));
                        Assert.AreEqual(70, image.Width);
                        Assert.AreEqual(46, image.Height);
                        Assert.AreEqual(MagickFormat.Gif, image.Format);
                    }
                }

                [TestMethod]
                public void ShouldPingImageWithFormat()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        image.Ping("png:" + Files.SnakewarePNG);
                        Assert.AreEqual(286, image.Width);
                        Assert.AreEqual(67, image.Height);
                        Assert.AreEqual(MagickFormat.Png, image.Format);
                    }
                }

                [TestMethod]
                public void ShouldReadTheImageProfile()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        image.Ping(Files.EightBimTIF);

                        Assert.IsNotNull(image.Get8BimProfile());
                    }
                }
            }

            [TestClass]
            public class WithFileNameAndMagickReadSettings
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsNull()
                {
                    var settings = new MagickReadSettings();

                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.ThrowsArgumentNullException("fileName", () =>
                        {
                            image.Ping((string)null, settings);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    var settings = new MagickReadSettings();

                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.ThrowsArgumentException("fileName", () =>
                        {
                            image.Ping(string.Empty, settings);
                        });
                    }
                }

                [TestMethod]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        image.Ping(Files.CirclePNG, null);
                    }
                }

                [TestMethod]
                public void ShouldResetTheFormatAfterReadingFile()
                {
                    var readSettings = new MagickReadSettings()
                    {
                        Format = MagickFormat.Png,
                    };

                    using (IMagickImage image = new MagickImage())
                    {
                        image.Ping(Files.CirclePNG, readSettings);

                        Assert.AreEqual(MagickFormat.Unknown, image.Settings.Format);
                    }
                }
            }

            [TestClass]
            public class WithStream
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenStreamIsNull()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.ThrowsArgumentNullException("stream", () =>
                        {
                            image.Ping((Stream)null);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenStreamIsEmpty()
                {
                    var settings = new MagickReadSettings();

                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.ThrowsArgumentException("stream", () =>
                        {
                            image.Ping(new MemoryStream());
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenStreamIsNotReadable()
                {
                    using (TestStream testStream = new TestStream(false, true, true))
                    {
                        using (IMagickImage image = new MagickImage())
                        {
                            ExceptionAssert.ThrowsArgumentException("stream", () =>
                            {
                                image.Ping(testStream);
                            });
                        }
                    }
                }

                [TestMethod]
                public void ShouldPingImage()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        using (FileStream fs = File.OpenRead(Files.SnakewarePNG))
                        {
                            image.Ping(fs);
                            Assert.AreEqual(286, image.Width);
                            Assert.AreEqual(67, image.Height);
                            Assert.AreEqual(MagickFormat.Png, image.Format);
                        }
                    }
                }

                [TestMethod]
                public void ShouldPingImageFromSeekablePartialStream()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        using (FileStream fs = File.OpenRead(Files.ImageMagickJPG))
                        {
                            image.Ping(fs);

                            fs.Position = 0;
                            using (PartialStream partialStream = new PartialStream(fs, true))
                            {
                                using (IMagickImage testImage = new MagickImage())
                                {
                                    testImage.Ping(partialStream);

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
                public void ShouldPingImageFromNonSeekablePartialStream()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        using (FileStream fs = File.OpenRead(Files.ImageMagickJPG))
                        {
                            image.Ping(fs);

                            fs.Position = 0;
                            using (PartialStream partialStream = new PartialStream(fs, false))
                            {
                                using (IMagickImage testImage = new MagickImage())
                                {
                                    testImage.Ping(partialStream);

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

            [TestClass]
            public class WithStreamAndMagickReadSettings
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenStreamIsNull()
                {
                    var settings = new MagickReadSettings();

                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.ThrowsArgumentNullException("stream", () =>
                        {
                            image.Ping((Stream)null, settings);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenStreamIsEmpty()
                {
                    var settings = new MagickReadSettings();

                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.ThrowsArgumentException("stream", () =>
                        {
                            image.Ping(new MemoryStream(), settings);
                        });
                    }
                }

                [TestMethod]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    using (var fileStream = File.OpenRead(Files.CirclePNG))
                    {
                        using (IMagickImage image = new MagickImage())
                        {
                            image.Ping(fileStream, null);
                        }
                    }
                }

                [TestMethod]
                public void ShouldUseTheCorrectReaderWhenFormatIsSet()
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
                                image.Ping(stream, settings);
                            }, "ReadPNGImage");
                        }
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
                        using (IMagickImage image = new MagickImage())
                        {
                            image.Ping(stream, readSettings);

                            Assert.AreEqual(MagickFormat.Unknown, image.Settings.Format);
                        }
                    }
                }
            }
        }
    }
}
