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
using System.IO;
using System.Text;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public partial class TheReadMethod
        {
            [TestClass]
            public class WithByteArray
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("data", () =>
                        {
                            image.Read((byte[])null);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentException>("data", () =>
                        {
                            image.Read(new byte[] { });
                        });
                    }
                }

                [TestMethod]
                public void ShouldReadImage()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        image.Read(File.ReadAllBytes(Files.SnakewarePNG));
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
                        ExceptionAssert.Throws<ArgumentNullException>("data", () =>
                        {
                            image.Read((byte[])null, 0, 0);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentException>("data", () =>
                        {
                            image.Read(new byte[] { }, 0, 0);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenOffsetIsNegative()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentException>("offset", () =>
                        {
                            image.Read(new byte[] { 215 }, -1, 0);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenCountIsZero()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentException>("count", () =>
                        {
                            image.Read(new byte[] { 215 }, 0, 0);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenCountIsNegative()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentException>("count", () =>
                        {
                            image.Read(new byte[] { 215 }, 0, -1);
                        });
                    }
                }

                [TestMethod]
                public void ShouldReadImage()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        var fileBytes = File.ReadAllBytes(Files.SnakewarePNG);
                        var bytes = new byte[fileBytes.Length + 10];
                        fileBytes.CopyTo(bytes, 10);

                        image.Read(bytes, 10, bytes.Length - 10);
                        Assert.AreEqual(286, image.Width);
                        Assert.AreEqual(67, image.Height);
                    }
                }
            }

            [TestClass]
            public class WithByteArrayAndOffsetAndMagickFormat
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("data", () =>
                        {
                            image.Read(null, 0, 0, MagickFormat.Png);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentException>("data", () =>
                        {
                            image.Read(new byte[] { }, 0, 0, MagickFormat.Png);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenOffsetIsNegative()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentException>("offset", () =>
                        {
                            image.Read(new byte[] { 215 }, -1, 0, MagickFormat.Png);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenCountIsZero()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentException>("count", () =>
                        {
                            image.Read(new byte[] { 215 }, 0, 0, MagickFormat.Png);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenCountIsNegative()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentException>("count", () =>
                        {
                            image.Read(new byte[] { 215 }, 0, -1, MagickFormat.Png);
                        });
                    }
                }

                [TestMethod]
                public void ShouldReadImage()
                {
                    var fileBytes = File.ReadAllBytes(Files.ImageMagickICO);
                    var bytes = new byte[fileBytes.Length + 10];
                    fileBytes.CopyTo(bytes, 10);

                    using (IMagickImage image = new MagickImage())
                    {
                        image.Read(bytes, 10, bytes.Length - 10, MagickFormat.Ico);
                        Assert.AreEqual(64, image.Width);
                        Assert.AreEqual(64, image.Height);
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
                        ExceptionAssert.Throws<ArgumentNullException>("data", () =>
                        {
                            image.Read(null, 0, 0, settings);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    var settings = new MagickReadSettings();

                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentException>("data", () =>
                        {
                            image.Read(new byte[] { }, 0, 0, settings);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenOffsetIsNegative()
                {
                    var settings = new MagickReadSettings();

                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentException>("offset", () =>
                        {
                            image.Read(new byte[] { 215 }, -1, 0, settings);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenCountIsZero()
                {
                    var settings = new MagickReadSettings();

                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentException>("count", () =>
                        {
                            image.Read(new byte[] { 215 }, 0, 0, settings);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenCountIsNegative()
                {
                    var settings = new MagickReadSettings();

                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentException>("count", () =>
                        {
                            image.Read(new byte[] { 215 }, 0, -1, settings);
                        });
                    }
                }

                [TestMethod]
                public void ShouldReadImage()
                {
                    var settings = new MagickReadSettings();

                    var fileBytes = File.ReadAllBytes(Files.SnakewarePNG);
                    var bytes = new byte[fileBytes.Length + 10];
                    fileBytes.CopyTo(bytes, 10);

                    using (IMagickImage image = new MagickImage())
                    {
                        image.Read(bytes, 10, bytes.Length - 10, settings);
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
                        image.Read(bytes, 0, bytes.Length, null);
                    }
                }
            }

            [TestClass]
            public class WithByteArrayAndMagickFormat
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("data", () =>
                        {
                            image.Read((byte[])null, MagickFormat.Png);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentException>("data", () =>
                        {
                            image.Read(new byte[] { }, MagickFormat.Png);
                        });
                    }
                }

                [TestMethod]
                public void ShouldUseTheCorrectReaderWhenFormatIsSet()
                {
                    var bytes = Encoding.ASCII.GetBytes("%PDF-");

                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<MagickCorruptImageErrorException>(() =>
                        {
                            image.Read(bytes, MagickFormat.Png);
                        }, "ReadPNGImage");
                    }
                }

                [TestMethod]
                public void ShouldResetTheFormatAfterReadingBytes()
                {
                    var bytes = File.ReadAllBytes(Files.CirclePNG);

                    using (IMagickImage image = new MagickImage())
                    {
                        image.Read(bytes, MagickFormat.Png);

                        Assert.AreEqual(MagickFormat.Unknown, image.Settings.Format);
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
                        ExceptionAssert.Throws<ArgumentNullException>("data", () =>
                        {
                            image.Read((byte[])null, settings);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    var settings = new MagickReadSettings();

                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentException>("data", () =>
                        {
                            image.Read(new byte[] { }, settings);
                        });
                    }
                }

                [TestMethod]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        image.Read(File.ReadAllBytes(Files.CirclePNG), null);
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
                            image.Read(bytes, settings);
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
                        image.Read(bytes, readSettings);

                        Assert.AreEqual(MagickFormat.Unknown, image.Settings.Format);
                    }
                }
            }

            [TestClass]
            public class WithColor
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenColorIsNull()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("color", () =>
                        {
                            image.Read((MagickColor)null, 1, 1);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenWidthIsZero()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentException>("width", () =>
                        {
                            image.Read(MagickColors.Red, 0, 1);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenHeightIsZero()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentException>("height", () =>
                        {
                            image.Read(MagickColors.Red, 1, 0);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenWidthIsNegative()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentException>("width", () =>
                        {
                            image.Read(MagickColors.Red, -1, 1);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenHeightIsNegative()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentException>("height", () =>
                        {
                            image.Read(MagickColors.Red, 1, -1);
                        });
                    }
                }

                [TestMethod]
                public void ShouldReadImage()
                {
                    MagickColor red = new MagickColor("red");

                    using (IMagickImage image = new MagickImage())
                    {
                        image.Read(red, 20, 30);
                        Assert.AreEqual(20, image.Width);
                        Assert.AreEqual(30, image.Height);
                        ColorAssert.AreEqual(red, image, 10, 10);
                    }
                }

                [TestMethod]
                public void ShouldReadImageFromCmkyColorName()
                {
                    MagickColor red = new MagickColor("cmyk(0%,100%,100%,0)");

                    using (IMagickImage image = new MagickImage())
                    {
                        image.Read(red, 20, 30);
                        Assert.AreEqual(20, image.Width);
                        Assert.AreEqual(30, image.Height);
                        Assert.AreEqual(ColorSpace.CMYK, image.ColorSpace);
                        image.Clamp();
                        ColorAssert.AreEqual(red, image, 10, 10);
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
                        ExceptionAssert.Throws<ArgumentNullException>("file", () =>
                        {
                            image.Read((FileInfo)null);
                        });
                    }
                }
            }

            [TestClass]
            public class WithFileInfoAndMagickFormat
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenFileInfoIsNull()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("file", () =>
                        {
                            image.Read((FileInfo)null, MagickFormat.Png);
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
                        ExceptionAssert.Throws<ArgumentNullException>("file", () =>
                        {
                            image.Read((FileInfo)null, settings);
                        });
                    }
                }

                [TestMethod]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        image.Read(new FileInfo(Files.CirclePNG), null);
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
                        ExceptionAssert.Throws<ArgumentNullException>("fileName", () =>
                        {
                            image.Read((string)null);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentException>("fileName", () =>
                        {
                            image.Read(string.Empty);
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
                public void ShouldReadImage()
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
                public void ShouldReadBuiltinImage()
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
                public void ShouldReadImageWithNonAsciiFileName()
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
                public void ShouldReadImageWithFormat()
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
                public void ShouldReadImageFromXcColorName()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        image.Read("xc:red", 50, 50);
                        Assert.AreEqual(50, image.Width);
                        Assert.AreEqual(50, image.Height);
                        ColorAssert.AreEqual(MagickColors.Red, image, 5, 5);
                    }
                }
            }

            [TestClass]
            public class WithFileNameAndMagickFormat
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsNull()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("fileName", () =>
                        {
                            image.Read((string)null, MagickFormat.Png);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentException>("fileName", () =>
                        {
                            image.Read(string.Empty, MagickFormat.Png);
                        });
                    }
                }

                [TestMethod]
                public void ShouldResetTheFormatAfterReadingFile()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        image.Read(Files.CirclePNG, MagickFormat.Png);

                        Assert.AreEqual(MagickFormat.Unknown, image.Settings.Format);
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
                        ExceptionAssert.Throws<ArgumentNullException>("fileName", () =>
                        {
                            image.Read((string)null, settings);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    var settings = new MagickReadSettings();

                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentException>("fileName", () =>
                        {
                            image.Read(string.Empty, settings);
                        });
                    }
                }

                [TestMethod]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        image.Read(Files.CirclePNG, null);
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
                        image.Read(Files.CirclePNG, readSettings);

                        Assert.AreEqual(MagickFormat.Unknown, image.Settings.Format);
                    }
                }
            }

            [TestClass]
            public class WithFileNameAndSize
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenColorIsNull()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("fileName", () =>
                        {
                            image.Read((string)null, 1, 1);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenWidthIsZero()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentException>("width", () =>
                        {
                            image.Read("xc:red", 0, 1);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenHeightIsZero()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentException>("height", () =>
                        {
                            image.Read("xc:red", 1, 0);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenWidthIsNegative()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentException>("width", () =>
                        {
                            image.Read("xc:red", -1, 1);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenHeightIsNegative()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentException>("height", () =>
                        {
                            image.Read("xc:red", 1, -1);
                        });
                    }
                }

                [TestMethod]
                public void ShouldReadImage()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        image.Read("xc:red", 20, 30);

                        Assert.AreEqual(20, image.Width);
                        Assert.AreEqual(30, image.Height);
                        ColorAssert.AreEqual(MagickColors.Red, image, 10, 10);
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
                        ExceptionAssert.Throws<ArgumentNullException>("stream", () =>
                        {
                            image.Read((Stream)null);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenStreamIsEmpty()
                {
                    var settings = new MagickReadSettings();

                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentException>("stream", () =>
                        {
                            image.Read(new MemoryStream());
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
                            ExceptionAssert.Throws<ArgumentException>("stream", () =>
                            {
                                image.Read(testStream);
                            });
                        }
                    }
                }

                [TestMethod]
                public void ShouldReadImage()
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

            [TestClass]
            public class WithStreamAndMagickFormat
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenStreamIsNull()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentNullException>("stream", () =>
                        {
                            image.Read((Stream)null, MagickFormat.Png);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenStreamIsEmpty()
                {
                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentException>("stream", () =>
                        {
                            image.Read(new MemoryStream(), MagickFormat.Png);
                        });
                    }
                }

                [TestMethod]
                public void ShouldUseTheCorrectReaderWhenFormatIsSet()
                {
                    var bytes = Encoding.ASCII.GetBytes("%PDF-");

                    using (MemoryStream stream = new MemoryStream(bytes))
                    {
                        using (IMagickImage image = new MagickImage())
                        {
                            ExceptionAssert.Throws<MagickCorruptImageErrorException>(() =>
                            {
                                image.Read(stream, MagickFormat.Png);
                            }, "ReadPNGImage");
                        }
                    }
                }

                [TestMethod]
                public void ShouldResetTheFormatAfterReadingStream()
                {
                    using (var stream = File.OpenRead(Files.CirclePNG))
                    {
                        using (IMagickImage image = new MagickImage())
                        {
                            image.Read(stream, MagickFormat.Png);

                            Assert.AreEqual(MagickFormat.Unknown, image.Settings.Format);
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
                        ExceptionAssert.Throws<ArgumentNullException>("stream", () =>
                        {
                            image.Read((Stream)null, settings);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenStreamIsEmpty()
                {
                    var settings = new MagickReadSettings();

                    using (IMagickImage image = new MagickImage())
                    {
                        ExceptionAssert.Throws<ArgumentException>("stream", () =>
                        {
                            image.Read(new MemoryStream(), settings);
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
                            image.Read(fileStream, null);
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
                                image.Read(stream, settings);
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
                            image.Read(stream, readSettings);

                            Assert.AreEqual(MagickFormat.Unknown, image.Settings.Format);
                        }
                    }
                }
            }
        }
    }
}
