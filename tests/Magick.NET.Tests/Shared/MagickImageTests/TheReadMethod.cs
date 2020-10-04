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
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public partial class TheReadMethod
        {
            public class WithByteArray
            {
                [Fact]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentNullException>("data", () =>
                        {
                            image.Read((byte[])null);
                        });
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentException>("data", () =>
                        {
                            image.Read(new byte[] { });
                        });
                    }
                }

                [Fact]
                public void ShouldReadImage()
                {
                    using (var image = new MagickImage())
                    {
                        image.Read(FileHelper.ReadAllBytes(Files.SnakewarePNG));
                        Assert.Equal(286, image.Width);
                        Assert.Equal(67, image.Height);
                    }
                }
            }

            public class WithByteArrayAndOffset
            {
                [Fact]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentNullException>("data", () =>
                        {
                            image.Read((byte[])null, 0, 0);
                        });
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentException>("data", () =>
                        {
                            image.Read(new byte[] { }, 0, 0);
                        });
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenOffsetIsNegative()
                {
                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentException>("offset", () =>
                        {
                            image.Read(new byte[] { 215 }, -1, 0);
                        });
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenCountIsZero()
                {
                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentException>("count", () =>
                        {
                            image.Read(new byte[] { 215 }, 0, 0);
                        });
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenCountIsNegative()
                {
                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentException>("count", () =>
                        {
                            image.Read(new byte[] { 215 }, 0, -1);
                        });
                    }
                }

                [Fact]
                public void ShouldReadImage()
                {
                    using (var image = new MagickImage())
                    {
                        var fileBytes = FileHelper.ReadAllBytes(Files.SnakewarePNG);
                        var bytes = new byte[fileBytes.Length + 10];
                        fileBytes.CopyTo(bytes, 10);

                        image.Read(bytes, 10, bytes.Length - 10);
                        Assert.Equal(286, image.Width);
                        Assert.Equal(67, image.Height);
                    }
                }
            }

            public class WithByteArrayAndOffsetAndMagickFormat
            {
                [Fact]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentNullException>("data", () =>
                        {
                            image.Read(null, 0, 0, MagickFormat.Png);
                        });
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentException>("data", () =>
                        {
                            image.Read(new byte[] { }, 0, 0, MagickFormat.Png);
                        });
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenOffsetIsNegative()
                {
                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentException>("offset", () =>
                        {
                            image.Read(new byte[] { 215 }, -1, 0, MagickFormat.Png);
                        });
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenCountIsZero()
                {
                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentException>("count", () =>
                        {
                            image.Read(new byte[] { 215 }, 0, 0, MagickFormat.Png);
                        });
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenCountIsNegative()
                {
                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentException>("count", () =>
                        {
                            image.Read(new byte[] { 215 }, 0, -1, MagickFormat.Png);
                        });
                    }
                }

                [Fact]
                public void ShouldReadImage()
                {
                    var fileBytes = FileHelper.ReadAllBytes(Files.ImageMagickICO);
                    var bytes = new byte[fileBytes.Length + 10];
                    fileBytes.CopyTo(bytes, 10);

                    using (var image = new MagickImage())
                    {
                        image.Read(bytes, 10, bytes.Length - 10, MagickFormat.Ico);
                        Assert.Equal(64, image.Width);
                        Assert.Equal(64, image.Height);
                    }
                }
            }

            public class WithByteArrayAndOffsetAndMagickReadSettings
            {
                [Fact]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    var settings = new MagickReadSettings();

                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentNullException>("data", () =>
                        {
                            image.Read(null, 0, 0, settings);
                        });
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    var settings = new MagickReadSettings();

                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentException>("data", () =>
                        {
                            image.Read(new byte[] { }, 0, 0, settings);
                        });
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenOffsetIsNegative()
                {
                    var settings = new MagickReadSettings();

                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentException>("offset", () =>
                        {
                            image.Read(new byte[] { 215 }, -1, 0, settings);
                        });
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenCountIsZero()
                {
                    var settings = new MagickReadSettings();

                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentException>("count", () =>
                        {
                            image.Read(new byte[] { 215 }, 0, 0, settings);
                        });
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenCountIsNegative()
                {
                    var settings = new MagickReadSettings();

                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentException>("count", () =>
                        {
                            image.Read(new byte[] { 215 }, 0, -1, settings);
                        });
                    }
                }

                [Fact]
                public void ShouldReadImage()
                {
                    var settings = new MagickReadSettings();

                    var fileBytes = FileHelper.ReadAllBytes(Files.SnakewarePNG);
                    var bytes = new byte[fileBytes.Length + 10];
                    fileBytes.CopyTo(bytes, 10);

                    using (var image = new MagickImage())
                    {
                        image.Read(bytes, 10, bytes.Length - 10, settings);
                        Assert.Equal(286, image.Width);
                        Assert.Equal(67, image.Height);
                    }
                }

                [Fact]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    var bytes = FileHelper.ReadAllBytes(Files.CirclePNG);

                    using (var image = new MagickImage())
                    {
                        image.Read(bytes, 0, bytes.Length, null);
                    }
                }
            }

            public class WithByteArrayAndMagickFormat
            {
                [Fact]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentNullException>("data", () =>
                        {
                            image.Read((byte[])null, MagickFormat.Png);
                        });
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentException>("data", () =>
                        {
                            image.Read(new byte[] { }, MagickFormat.Png);
                        });
                    }
                }

                [Fact]
                public void ShouldUseTheCorrectReaderWhenFormatIsSet()
                {
                    var bytes = Encoding.ASCII.GetBytes("%PDF-");

                    using (var image = new MagickImage())
                    {
                        var exception = Assert.Throws<MagickCorruptImageErrorException>(() =>
                        {
                            image.Read(bytes, MagickFormat.Png);
                        });

                        Assert.Contains("ReadPNGImage", exception.Message);
                    }
                }

                [Fact]
                public void ShouldResetTheFormatAfterReadingBytes()
                {
                    var bytes = FileHelper.ReadAllBytes(Files.CirclePNG);

                    using (var image = new MagickImage())
                    {
                        image.Read(bytes, MagickFormat.Png);

                        Assert.Equal(MagickFormat.Unknown, image.Settings.Format);
                    }
                }
            }

            public class WithByteArrayAndMagickReadSettings
            {
                [Fact]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    var settings = new MagickReadSettings();

                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentNullException>("data", () =>
                        {
                            image.Read((byte[])null, settings);
                        });
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    var settings = new MagickReadSettings();

                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentException>("data", () =>
                        {
                            image.Read(new byte[] { }, settings);
                        });
                    }
                }

                [Fact]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    using (var image = new MagickImage())
                    {
                        image.Read(FileHelper.ReadAllBytes(Files.CirclePNG), null);
                    }
                }

                [Fact]
                public void ShouldUseTheCorrectReaderWhenFormatIsSet()
                {
                    var bytes = Encoding.ASCII.GetBytes("%PDF-");
                    var settings = new MagickReadSettings
                    {
                        Format = MagickFormat.Png,
                    };

                    using (var image = new MagickImage())
                    {
                        var exception = Assert.Throws<MagickCorruptImageErrorException>(() =>
                        {
                            image.Read(bytes, settings);
                        });

                        Assert.Contains("ReadPNGImage", exception.Message);
                    }
                }

                [Fact]
                public void ShouldResetTheFormatAfterReadingBytes()
                {
                    var readSettings = new MagickReadSettings
                    {
                        Format = MagickFormat.Png,
                    };

                    var bytes = FileHelper.ReadAllBytes(Files.CirclePNG);

                    using (var image = new MagickImage())
                    {
                        image.Read(bytes, readSettings);

                        Assert.Equal(MagickFormat.Unknown, image.Settings.Format);
                    }
                }
            }

            public class WithColor
            {
                [Fact]
                public void ShouldThrowExceptionWhenColorIsNull()
                {
                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentNullException>("color", () =>
                        {
                            image.Read((MagickColor)null, 1, 1);
                        });
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenWidthIsZero()
                {
                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentException>("width", () =>
                        {
                            image.Read(MagickColors.Red, 0, 1);
                        });
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenHeightIsZero()
                {
                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentException>("height", () =>
                        {
                            image.Read(MagickColors.Red, 1, 0);
                        });
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenWidthIsNegative()
                {
                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentException>("width", () =>
                        {
                            image.Read(MagickColors.Red, -1, 1);
                        });
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenHeightIsNegative()
                {
                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentException>("height", () =>
                        {
                            image.Read(MagickColors.Red, 1, -1);
                        });
                    }
                }

                [Fact]
                public void ShouldReadImage()
                {
                    MagickColor red = new MagickColor("red");

                    using (var image = new MagickImage())
                    {
                        image.Read(red, 20, 30);
                        Assert.Equal(20, image.Width);
                        Assert.Equal(30, image.Height);
                        ColorAssert.Equal(red, image, 10, 10);
                    }
                }

                [Fact]
                public void ShouldReadImageFromCmkyColorName()
                {
                    MagickColor red = new MagickColor("cmyk(0%,100%,100%,0)");

                    using (var image = new MagickImage())
                    {
                        image.Read(red, 20, 30);
                        Assert.Equal(20, image.Width);
                        Assert.Equal(30, image.Height);
                        Assert.Equal(ColorSpace.CMYK, image.ColorSpace);
                        image.Clamp();
                        ColorAssert.Equal(red, image, 10, 10);
                    }
                }
            }

            public class WithFileInfo
            {
                [Fact]
                public void ShouldThrowExceptionWhenFileInfoIsNull()
                {
                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentNullException>("file", () =>
                        {
                            image.Read((FileInfo)null);
                        });
                    }
                }
            }

            public class WithFileInfoAndMagickFormat
            {
                [Fact]
                public void ShouldThrowExceptionWhenFileInfoIsNull()
                {
                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentNullException>("file", () =>
                        {
                            image.Read((FileInfo)null, MagickFormat.Png);
                        });
                    }
                }
            }

            public class WithFileInfoAndMagickReadSettings
            {
                [Fact]
                public void ShouldThrowExceptionWhenFileInfoIsNull()
                {
                    var settings = new MagickReadSettings();

                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentNullException>("file", () =>
                        {
                            image.Read((FileInfo)null, settings);
                        });
                    }
                }

                [Fact]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    using (var image = new MagickImage())
                    {
                        image.Read(new FileInfo(Files.CirclePNG), null);
                    }
                }
            }

            public class WithFileName
            {
                [Fact]
                public void ShouldThrowExceptionWhenFileNameIsNull()
                {
                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentNullException>("fileName", () =>
                        {
                            image.Read((string)null);
                        });
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentException>("fileName", () =>
                        {
                            image.Read(string.Empty);
                        });
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenFileIsMissing()
                {
                    using (var image = new MagickImage())
                    {
                        var exception = Assert.Throws<MagickBlobErrorException>(() =>
                        {
                            image.Read(Files.Missing);
                        });

                        Assert.Contains("error/blob.c/OpenBlob", exception.Message);
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenFileWithFormatIsMissing()
                {
                    using (var image = new MagickImage())
                    {
                        var exception = Assert.Throws<MagickBlobErrorException>(() =>
                        {
                            image.Read("png:" + Files.Missing);
                        });

                        Assert.Contains("error/blob.c/OpenBlob", exception.Message);
                    }
                }

                [Fact]
                public void ShouldReadImage()
                {
                    using (var image = new MagickImage())
                    {
                        image.Read(Files.SnakewarePNG);
                        Assert.Equal(286, image.Width);
                        Assert.Equal(67, image.Height);
                        Assert.Equal(MagickFormat.Png, image.Format);
                    }
                }

                [Fact]
                public void ShouldReadBuiltinImage()
                {
                    using (var image = new MagickImage())
                    {
                        image.Read(Files.Builtin.Rose);
                        Assert.Equal(70, image.Width);
                        Assert.Equal(46, image.Height);
                        Assert.Equal(MagickFormat.Pnm, image.Format);
                    }
                }

                [Fact]
                public void ShouldReadImageWithNonAsciiFileName()
                {
                    using (var image = new MagickImage())
                    {
                        image.Read(Files.RoseSparkleGIF);
                        Assert.Equal("RöseSparkle.gif", Path.GetFileName(image.FileName));
                        Assert.Equal(70, image.Width);
                        Assert.Equal(46, image.Height);
                        Assert.Equal(MagickFormat.Gif, image.Format);
                    }
                }

                [Fact]
                public void ShouldReadImageWithFormat()
                {
                    using (var image = new MagickImage())
                    {
                        image.Read("png:" + Files.SnakewarePNG);
                        Assert.Equal(286, image.Width);
                        Assert.Equal(67, image.Height);
                        Assert.Equal(MagickFormat.Png, image.Format);
                    }
                }

                [Fact]
                public void ShouldReadImageFromXcColorName()
                {
                    using (var image = new MagickImage())
                    {
                        image.Read("xc:red", 50, 50);
                        Assert.Equal(50, image.Width);
                        Assert.Equal(50, image.Height);
                        ColorAssert.Equal(MagickColors.Red, image, 5, 5);
                    }
                }

                [Fact]
                public void ShouldUseBaseDirectoryOfCurrentAppDomainWhenFileNameStartsWithTilde()
                {
                    using (var image = new MagickImage())
                    {
                        var exception = Assert.Throws<MagickBlobErrorException>(() =>
                        {
                            image.Read("~/test.gif");
                        });

                        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
                        Assert.Contains("error/blob.c/OpenBlob", exception.Message);
                        Assert.Contains(baseDirectory, exception.Message);
                    }
                }

                [Fact]
                public void ShouldNotUseBaseDirectoryOfCurrentAppDomainWhenFileNameIsTilde()
                {
                    using (var image = new MagickImage())
                    {
                        var exception = Assert.Throws<MagickBlobErrorException>(() =>
                        {
                            image.Read("~");
                        });

                        Assert.Contains("~", exception.Message);
                        Assert.Contains("error/blob.c/OpenBlob", exception.Message);
                    }
                }
            }

            public class WithFileNameAndMagickFormat
            {
                [Fact]
                public void ShouldThrowExceptionWhenFileNameIsNull()
                {
                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentNullException>("fileName", () =>
                        {
                            image.Read((string)null, MagickFormat.Png);
                        });
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentException>("fileName", () =>
                        {
                            image.Read(string.Empty, MagickFormat.Png);
                        });
                    }
                }

                [Fact]
                public void ShouldResetTheFormatAfterReadingFile()
                {
                    using (var image = new MagickImage())
                    {
                        image.Read(Files.CirclePNG, MagickFormat.Png);

                        Assert.Equal(MagickFormat.Unknown, image.Settings.Format);
                    }
                }
            }

            public class WithFileNameAndMagickReadSettings
            {
                [Fact]
                public void ShouldThrowExceptionWhenFileNameIsNull()
                {
                    var settings = new MagickReadSettings();

                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentNullException>("fileName", () =>
                        {
                            image.Read((string)null, settings);
                        });
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    var settings = new MagickReadSettings();

                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentException>("fileName", () =>
                        {
                            image.Read(string.Empty, settings);
                        });
                    }
                }

                [Fact]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    using (var image = new MagickImage())
                    {
                        image.Read(Files.CirclePNG, null);
                    }
                }

                [Fact]
                public void ShouldResetTheFormatAfterReadingFile()
                {
                    var readSettings = new MagickReadSettings
                    {
                        Format = MagickFormat.Png,
                    };

                    using (var image = new MagickImage())
                    {
                        image.Read(Files.CirclePNG, readSettings);

                        Assert.Equal(MagickFormat.Unknown, image.Settings.Format);
                    }
                }

                [Fact]
                public void ShouldUseTheReadSettings()
                {
                    using (var image = new MagickImage())
                    {
                        using (FileStream fs = File.OpenRead(Files.Logos.MagickNETSVG))
                        {
                            byte[] buffer = new byte[fs.Length + 1];
                            fs.Read(buffer, 0, (int)fs.Length);

                            using (MemoryStream memStream = new MemoryStream(buffer, 0, (int)fs.Length))
                            {
                                image.Read(memStream, new MagickReadSettings
                                {
                                    Density = new Density(72),
                                });

                                ColorAssert.Equal(new MagickColor("#231f20"), image, 129, 101);
                            }
                        }
                    }
                }
            }

            public class WithFileNameAndSize
            {
                [Fact]
                public void ShouldThrowExceptionWhenColorIsNull()
                {
                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentNullException>("fileName", () =>
                        {
                            image.Read((string)null, 1, 1);
                        });
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenWidthIsZero()
                {
                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentException>("width", () =>
                        {
                            image.Read("xc:red", 0, 1);
                        });
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenHeightIsZero()
                {
                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentException>("height", () =>
                        {
                            image.Read("xc:red", 1, 0);
                        });
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenWidthIsNegative()
                {
                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentException>("width", () =>
                        {
                            image.Read("xc:red", -1, 1);
                        });
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenHeightIsNegative()
                {
                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentException>("height", () =>
                        {
                            image.Read("xc:red", 1, -1);
                        });
                    }
                }

                [Fact]
                public void ShouldReadImage()
                {
                    using (var image = new MagickImage())
                    {
                        image.Read("xc:red", 20, 30);

                        Assert.Equal(20, image.Width);
                        Assert.Equal(30, image.Height);
                        ColorAssert.Equal(MagickColors.Red, image, 10, 10);
                    }
                }
            }

            public class WithStream
            {
                [Fact]
                public void ShouldThrowExceptionWhenStreamIsNull()
                {
                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentNullException>("stream", () =>
                        {
                            image.Read((Stream)null);
                        });
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenStreamIsEmpty()
                {
                    var settings = new MagickReadSettings();

                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentException>("stream", () =>
                        {
                            image.Read(new MemoryStream());
                        });
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenStreamIsNotReadable()
                {
                    using (TestStream testStream = new TestStream(false, true, true))
                    {
                        using (var image = new MagickImage())
                        {
                            Assert.Throws<ArgumentException>("stream", () =>
                            {
                                image.Read(testStream);
                            });
                        }
                    }
                }

                [Fact]
                public void ShouldReadImage()
                {
                    using (var image = new MagickImage())
                    {
                        using (FileStream fs = File.OpenRead(Files.SnakewarePNG))
                        {
                            image.Read(fs);
                            Assert.Equal(286, image.Width);
                            Assert.Equal(67, image.Height);
                            Assert.Equal(MagickFormat.Png, image.Format);
                        }
                    }
                }

                [Fact]
                public void ShouldReadImageFromSeekablePartialStream()
                {
                    using (var image = new MagickImage())
                    {
                        using (FileStream fs = File.OpenRead(Files.ImageMagickJPG))
                        {
                            image.Read(fs);

                            fs.Position = 0;
                            using (PartialStream partialStream = new PartialStream(fs, true))
                            {
                                using (var testImage = new MagickImage())
                                {
                                    testImage.Read(partialStream);

                                    Assert.Equal(image.Width, testImage.Width);
                                    Assert.Equal(image.Height, testImage.Height);
                                    Assert.Equal(image.Format, testImage.Format);
                                    Assert.Equal(0.0, image.Compare(testImage, ErrorMetric.RootMeanSquared));
                                }
                            }
                        }
                    }
                }

                [Fact]
                public void ShouldReadImageFromNonSeekablePartialStream()
                {
                    using (var image = new MagickImage())
                    {
                        using (FileStream fs = File.OpenRead(Files.ImageMagickJPG))
                        {
                            image.Read(fs);

                            fs.Position = 0;
                            using (PartialStream partialStream = new PartialStream(fs, false))
                            {
                                using (var testImage = new MagickImage())
                                {
                                    testImage.Read(partialStream);

                                    Assert.Equal(image.Width, testImage.Width);
                                    Assert.Equal(image.Height, testImage.Height);
                                    Assert.Equal(image.Format, testImage.Format);
                                    Assert.Equal(0.0, image.Compare(testImage, ErrorMetric.RootMeanSquared));
                                }
                            }
                        }
                    }
                }
            }

            public class WithStreamAndMagickFormat
            {
                [Fact]
                public void ShouldThrowExceptionWhenStreamIsNull()
                {
                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentNullException>("stream", () =>
                        {
                            image.Read((Stream)null, MagickFormat.Png);
                        });
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenStreamIsEmpty()
                {
                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentException>("stream", () =>
                        {
                            image.Read(new MemoryStream(), MagickFormat.Png);
                        });
                    }
                }

                [Fact]
                public void ShouldUseTheCorrectReaderWhenFormatIsSet()
                {
                    var bytes = Encoding.ASCII.GetBytes("%PDF-");

                    using (MemoryStream stream = new MemoryStream(bytes))
                    {
                        using (var image = new MagickImage())
                        {
                            var exception = Assert.Throws<MagickCorruptImageErrorException>(() =>
                            {
                                image.Read(stream, MagickFormat.Png);
                            });

                            Assert.Contains("ReadPNGImage", exception.Message);
                        }
                    }
                }

                [Fact]
                public void ShouldResetTheFormatAfterReadingStream()
                {
                    using (var stream = File.OpenRead(Files.CirclePNG))
                    {
                        using (var image = new MagickImage())
                        {
                            image.Read(stream, MagickFormat.Png);

                            Assert.Equal(MagickFormat.Unknown, image.Settings.Format);
                        }
                    }
                }
            }

            public class WithStreamAndMagickReadSettings
            {
                [Fact]
                public void ShouldThrowExceptionWhenStreamIsNull()
                {
                    var settings = new MagickReadSettings();

                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentNullException>("stream", () =>
                        {
                            image.Read((Stream)null, settings);
                        });
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenStreamIsEmpty()
                {
                    var settings = new MagickReadSettings();

                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentException>("stream", () =>
                        {
                            image.Read(new MemoryStream(), settings);
                        });
                    }
                }

                [Fact]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    using (var fileStream = File.OpenRead(Files.CirclePNG))
                    {
                        using (var image = new MagickImage())
                        {
                            image.Read(fileStream, null);
                        }
                    }
                }

                [Fact]
                public void ShouldUseTheCorrectReaderWhenFormatIsSet()
                {
                    var bytes = Encoding.ASCII.GetBytes("%PDF-");
                    var settings = new MagickReadSettings
                    {
                        Format = MagickFormat.Png,
                    };

                    using (MemoryStream stream = new MemoryStream(bytes))
                    {
                        using (var image = new MagickImage())
                        {
                            var exception = Assert.Throws<MagickCorruptImageErrorException>(() =>
                            {
                                image.Read(stream, settings);
                            });

                            Assert.Contains("ReadPNGImage", exception.Message);
                        }
                    }
                }

                [Fact]
                public void ShouldResetTheFormatAfterReadingStream()
                {
                    var readSettings = new MagickReadSettings
                    {
                        Format = MagickFormat.Png,
                    };

                    using (var stream = File.OpenRead(Files.CirclePNG))
                    {
                        using (var image = new MagickImage())
                        {
                            image.Read(stream, readSettings);

                            Assert.Equal(MagickFormat.Unknown, image.Settings.Format);
                        }
                    }
                }
            }
        }
    }
}
