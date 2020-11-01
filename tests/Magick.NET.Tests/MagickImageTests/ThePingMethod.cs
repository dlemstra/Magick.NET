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
        public class ThePingMethod
        {
            public class WithByteArray
            {
                [Fact]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentNullException>("data", () => image.Ping((byte[])null));
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentException>("data", () => image.Ping(new byte[] { }));
                    }
                }

                [Fact]
                public void ShouldPingImage()
                {
                    using (var image = new MagickImage())
                    {
                        image.Ping(FileHelper.ReadAllBytes(Files.SnakewarePNG));
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
                        Assert.Throws<ArgumentNullException>("data", () => image.Ping((byte[])null, 0, 0));
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentException>("data", () => image.Ping(new byte[] { }, 0, 0));
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenOffsetIsNegative()
                {
                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentException>("offset", () => image.Ping(new byte[] { 215 }, -1, 0));
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenCountIsZero()
                {
                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentException>("count", () => image.Ping(new byte[] { 215 }, 0, 0));
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenCountIsNegative()
                {
                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentException>("count", () => image.Ping(new byte[] { 215 }, 0, -1));
                    }
                }

                [Fact]
                public void ShouldPingImage()
                {
                    using (var image = new MagickImage())
                    {
                        var fileBytes = FileHelper.ReadAllBytes(Files.SnakewarePNG);
                        var bytes = new byte[fileBytes.Length + 10];
                        fileBytes.CopyTo(bytes, 10);

                        image.Ping(bytes, 10, bytes.Length - 10);
                        Assert.Equal(286, image.Width);
                        Assert.Equal(67, image.Height);
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
                        Assert.Throws<ArgumentNullException>("data", () => image.Ping(null, 0, 0, settings));
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    var settings = new MagickReadSettings();

                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentException>("data", () => image.Ping(new byte[] { }, 0, 0, settings));
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenOffsetIsNegative()
                {
                    var settings = new MagickReadSettings();

                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentException>("offset", () => image.Ping(new byte[] { 215 }, -1, 0, settings));
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenCountIsZero()
                {
                    var settings = new MagickReadSettings();

                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentException>("count", () => image.Ping(new byte[] { 215 }, 0, 0, settings));
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenCountIsNegative()
                {
                    var settings = new MagickReadSettings();

                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentException>("count", () => image.Ping(new byte[] { 215 }, 0, -1, settings));
                    }
                }

                [Fact]
                public void ShouldPingImage()
                {
                    var settings = new MagickReadSettings();

                    var fileBytes = FileHelper.ReadAllBytes(Files.SnakewarePNG);
                    var bytes = new byte[fileBytes.Length + 10];
                    fileBytes.CopyTo(bytes, 10);

                    using (var image = new MagickImage())
                    {
                        image.Ping(bytes, 10, bytes.Length - 10, settings);
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
                        image.Ping(bytes, 0, bytes.Length, null);
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
                        Assert.Throws<ArgumentNullException>("data", () => image.Ping((byte[])null, settings));
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    var settings = new MagickReadSettings();

                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentException>("data", () => image.Ping(new byte[] { }, settings));
                    }
                }

                [Fact]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    using (var image = new MagickImage())
                    {
                        image.Ping(FileHelper.ReadAllBytes(Files.CirclePNG), null);
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
                        var exception = Assert.Throws<MagickCorruptImageErrorException>(() => image.Ping(bytes, settings));

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
                        image.Ping(bytes, readSettings);

                        Assert.Equal(MagickFormat.Unknown, image.Settings.Format);
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
                        Assert.Throws<ArgumentNullException>("file", () => image.Ping((FileInfo)null));
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
                        Assert.Throws<ArgumentNullException>("file", () => image.Ping((FileInfo)null, settings));
                    }
                }

                [Fact]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    using (var image = new MagickImage())
                    {
                        image.Ping(new FileInfo(Files.CirclePNG), null);
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
                        Assert.Throws<ArgumentNullException>("fileName", () => image.Ping((string)null));
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentException>("fileName", () => image.Ping(string.Empty));
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenFileIsMissing()
                {
                    using (var image = new MagickImage())
                    {
                        var exception = Assert.Throws<MagickBlobErrorException>(() => image.Ping(Files.Missing));

                        Assert.Contains("error/blob.c/OpenBlob", exception.Message);
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenFileWithFormatIsMissing()
                {
                    using (var image = new MagickImage())
                    {
                        var exception = Assert.Throws<MagickBlobErrorException>(() => image.Ping("png:" + Files.Missing));

                        Assert.Contains("error/blob.c/OpenBlob", exception.Message);
                    }
                }

                [Fact]
                public void ShouldPingImage()
                {
                    using (var image = new MagickImage())
                    {
                        image.Ping(Files.SnakewarePNG);
                        Assert.Equal(286, image.Width);
                        Assert.Equal(67, image.Height);
                        Assert.Equal(MagickFormat.Png, image.Format);
                    }
                }

                [Fact]
                public void ShouldPingBuiltinImage()
                {
                    using (var image = new MagickImage())
                    {
                        image.Ping(Files.Builtin.Rose);
                        Assert.Equal(70, image.Width);
                        Assert.Equal(46, image.Height);
                        Assert.Equal(MagickFormat.Pnm, image.Format);
                    }
                }

                [Fact]
                public void ShouldPingImageWithNonAsciiFileName()
                {
                    using (var image = new MagickImage())
                    {
                        image.Ping(Files.RoseSparkleGIF);
                        Assert.Equal("RöseSparkle.gif", Path.GetFileName(image.FileName));
                        Assert.Equal(70, image.Width);
                        Assert.Equal(46, image.Height);
                        Assert.Equal(MagickFormat.Gif, image.Format);
                    }
                }

                [Fact]
                public void ShouldPingImageWithFormat()
                {
                    using (var image = new MagickImage())
                    {
                        image.Ping("png:" + Files.SnakewarePNG);
                        Assert.Equal(286, image.Width);
                        Assert.Equal(67, image.Height);
                        Assert.Equal(MagickFormat.Png, image.Format);
                    }
                }

                [Fact]
                public void ShouldReadTheImageProfile()
                {
                    using (var image = new MagickImage())
                    {
                        image.Ping(Files.EightBimTIF);

                        Assert.NotNull(image.Get8BimProfile());
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
                        Assert.Throws<ArgumentNullException>("fileName", () => image.Ping((string)null, settings));
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    var settings = new MagickReadSettings();

                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentException>("fileName", () => image.Ping(string.Empty, settings));
                    }
                }

                [Fact]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    using (var image = new MagickImage())
                    {
                        image.Ping(Files.CirclePNG, null);
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
                        image.Ping(Files.CirclePNG, readSettings);

                        Assert.Equal(MagickFormat.Unknown, image.Settings.Format);
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
                        Assert.Throws<ArgumentNullException>("stream", () => image.Ping((Stream)null));
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenStreamIsEmpty()
                {
                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentException>("stream", () => image.Ping(new MemoryStream()));
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenStreamIsNotReadable()
                {
                    using (var testStream = new TestStream(false, true, true))
                    {
                        using (var image = new MagickImage())
                        {
                            Assert.Throws<ArgumentException>("stream", () => image.Ping(testStream));
                        }
                    }
                }

                [Fact]
                public void ShouldPingImage()
                {
                    using (var image = new MagickImage())
                    {
                        using (var fileStream = File.OpenRead(Files.SnakewarePNG))
                        {
                            image.Ping(fileStream);
                            Assert.Equal(286, image.Width);
                            Assert.Equal(67, image.Height);
                            Assert.Equal(MagickFormat.Png, image.Format);
                        }
                    }
                }

                [Fact]
                public void ShouldPingImageFromSeekablePartialStream()
                {
                    using (var image = new MagickImage())
                    {
                        using (var fileStream = File.OpenRead(Files.ImageMagickJPG))
                        {
                            image.Ping(fileStream);

                            fileStream.Position = 0;
                            using (var partialStream = new PartialStream(fileStream, true))
                            {
                                using (var testImage = new MagickImage())
                                {
                                    testImage.Ping(partialStream);

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
                public void ShouldPingImageFromNonSeekablePartialStream()
                {
                    using (var image = new MagickImage())
                    {
                        using (var fileStream = File.OpenRead(Files.ImageMagickJPG))
                        {
                            image.Ping(fileStream);

                            fileStream.Position = 0;
                            using (var partialStream = new PartialStream(fileStream, false))
                            {
                                using (var testImage = new MagickImage())
                                {
                                    testImage.Ping(partialStream);

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

            public class WithStreamAndMagickReadSettings
            {
                [Fact]
                public void ShouldThrowExceptionWhenStreamIsNull()
                {
                    var settings = new MagickReadSettings();

                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentNullException>("stream", () => image.Ping((Stream)null, settings));
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenStreamIsEmpty()
                {
                    var settings = new MagickReadSettings();

                    using (var image = new MagickImage())
                    {
                        Assert.Throws<ArgumentException>("stream", () => image.Ping(new MemoryStream(), settings));
                    }
                }

                [Fact]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    using (var fileStream = File.OpenRead(Files.CirclePNG))
                    {
                        using (var image = new MagickImage())
                        {
                            image.Ping(fileStream, null);
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

                    using (var stream = new MemoryStream(bytes))
                    {
                        using (var image = new MagickImage())
                        {
                            var exception = Assert.Throws<MagickCorruptImageErrorException>(() => image.Ping(stream, settings));

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
                            image.Ping(stream, readSettings);

                            Assert.Equal(MagickFormat.Unknown, image.Settings.Format);
                        }
                    }
                }
            }
        }
    }
}
