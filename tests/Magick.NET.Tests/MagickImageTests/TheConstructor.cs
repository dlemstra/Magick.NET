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
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public partial class TheConstructor
        {
            public class WithByteArray
            {
                [Fact]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    Assert.Throws<ArgumentNullException>("data", () =>
                    {
                        new MagickImage((byte[])null);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    Assert.Throws<ArgumentException>("data", () =>
                    {
                        new MagickImage(new byte[] { });
                    });
                }
            }

            public class WithByteArrayAndOffset
            {
                [Fact]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    Assert.Throws<ArgumentNullException>("data", () =>
                    {
                        new MagickImage((byte[])null, 0, 0);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    Assert.Throws<ArgumentException>("data", () =>
                    {
                        new MagickImage(new byte[] { }, 0, 0);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenOffsetIsNegative()
                {
                    Assert.Throws<ArgumentException>("offset", () =>
                    {
                        new MagickImage(new byte[] { 215 }, -1, 0);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenCountIsZero()
                {
                    Assert.Throws<ArgumentException>("count", () =>
                    {
                        new MagickImage(new byte[] { 215 }, 0, 0);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenCountIsNegative()
                {
                    Assert.Throws<ArgumentException>("count", () =>
                    {
                        new MagickImage(new byte[] { 215 }, 0, -1);
                    });
                }
            }

            public class WithByteArrayAndOffsetAndMagickFormat
            {
                [Fact]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    Assert.Throws<ArgumentNullException>("data", () =>
                    {
                        new MagickImage(null, 0, 0, MagickFormat.Png);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    Assert.Throws<ArgumentException>("data", () =>
                    {
                        new MagickImage(new byte[] { }, 0, 0, MagickFormat.Png);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenOffsetIsNegative()
                {
                    Assert.Throws<ArgumentException>("offset", () =>
                    {
                        new MagickImage(new byte[] { 215 }, -1, 0, MagickFormat.Png);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenCountIsZero()
                {
                    Assert.Throws<ArgumentException>("count", () =>
                    {
                        new MagickImage(new byte[] { 215 }, 0, 0, MagickFormat.Png);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenCountIsNegative()
                {
                    Assert.Throws<ArgumentException>("count", () =>
                    {
                        new MagickImage(new byte[] { 215 }, 0, -1, MagickFormat.Png);
                    });
                }
            }

            public class WithByteArrayAndOffsetAndMagickReadSettings
            {
                [Fact]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    Assert.Throws<ArgumentNullException>("data", () =>
                    {
                        var settings = new MagickReadSettings();

                        new MagickImage(null, 0, 0, settings);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    Assert.Throws<ArgumentException>("data", () =>
                    {
                        var settings = new MagickReadSettings();

                        new MagickImage(new byte[] { }, 0, 0, settings);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenOffsetIsNegative()
                {
                    Assert.Throws<ArgumentException>("offset", () =>
                    {
                        var settings = new MagickReadSettings();

                        new MagickImage(new byte[] { 215 }, -1, 0, settings);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenCountIsZero()
                {
                    Assert.Throws<ArgumentException>("count", () =>
                    {
                        var settings = new MagickReadSettings();

                        new MagickImage(new byte[] { 215 }, 0, 0, settings);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenCountIsNegative()
                {
                    Assert.Throws<ArgumentException>("count", () =>
                    {
                        var settings = new MagickReadSettings();

                        new MagickImage(new byte[] { 215 }, 0, -1, settings);
                    });
                }

                [Fact]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    var bytes = FileHelper.ReadAllBytes(Files.CirclePNG);

                    using (var image = new MagickImage(bytes, 0, bytes.Length, (MagickReadSettings)null))
                    {
                    }
                }
            }

            public class WithByteArrayAndOffsetAndPixelReadSettings
            {
                [Fact]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    Assert.Throws<ArgumentNullException>("data", () =>
                    {
                        var settings = new PixelReadSettings();

                        new MagickImage(null, 0, 0, settings);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    Assert.Throws<ArgumentException>("data", () =>
                    {
                        var settings = new PixelReadSettings();

                        new MagickImage(new byte[] { }, 0, 0, settings);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenOffsetIsNegative()
                {
                    Assert.Throws<ArgumentException>("offset", () =>
                    {
                        var settings = new PixelReadSettings();

                        new MagickImage(new byte[] { 215 }, -1, 0, settings);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenCountIsZero()
                {
                    Assert.Throws<ArgumentException>("count", () =>
                    {
                        var settings = new PixelReadSettings();

                        new MagickImage(new byte[] { 215 }, 0, 0, settings);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenCountIsNegative()
                {
                    Assert.Throws<ArgumentException>("count", () =>
                    {
                        var settings = new PixelReadSettings();

                        new MagickImage(new byte[] { 215 }, 0, -1, settings);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenSettingsIsNull()
                {
                    Assert.Throws<ArgumentNullException>("settings", () =>
                    {
                        new MagickImage(new byte[] { 215 }, 0, 1, (PixelReadSettings)null);
                    });
                }
            }

            public class WithByteArrayAndMagickFormat
            {
                [Fact]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    Assert.Throws<ArgumentNullException>("data", () =>
                    {
                        new MagickImage((byte[])null, MagickFormat.Png);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    Assert.Throws<ArgumentException>("data", () =>
                    {
                        new MagickImage(new byte[] { }, MagickFormat.Png);
                    });
                }
            }

            public class WithByteArrayAndMagickReadSettings
            {
                [Fact]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    Assert.Throws<ArgumentNullException>("data", () =>
                    {
                        var settings = new MagickReadSettings();

                        new MagickImage((byte[])null, settings);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    Assert.Throws<ArgumentException>("data", () =>
                    {
                        var settings = new MagickReadSettings();

                        new MagickImage(new byte[] { }, settings);
                    });
                }

                [Fact]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    using (var image = new MagickImage(FileHelper.ReadAllBytes(Files.CirclePNG), (MagickReadSettings)null))
                    {
                    }
                }
            }

            public class WithByteArrayAndPixelReadSettings
            {
                [Fact]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    Assert.Throws<ArgumentNullException>("data", () =>
                    {
                        var settings = new PixelReadSettings();

                        new MagickImage((byte[])null, settings);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    Assert.Throws<ArgumentException>("data", () =>
                    {
                        var settings = new PixelReadSettings();

                        new MagickImage(new byte[] { }, settings);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenSettingsIsNull()
                {
                    Assert.Throws<ArgumentNullException>("settings", () =>
                    {
                        new MagickImage(new byte[] { 215 }, (PixelReadSettings)null);
                    });
                }

                [Fact]
                public void ShouldReadByteArray()
                {
                    var data = new byte[]
                    {
                        0, 0, 0, 0, 0, 0, 0, 0,
                        0, 0, 0, 0, 0, 0, 0, 0,
                        0, 0, 0, 0, 0, 0, 0, 0,
                        0, 0, 0, 0, 0, 0, 0xf0, 0x3f,
                        0, 0, 0, 0, 0, 0, 0, 0,
                        0, 0, 0, 0, 0, 0, 0xf0, 0x3f,
                        0, 0, 0, 0, 0, 0, 0, 0,
                        0, 0, 0, 0, 0, 0, 0, 0,
                    };

                    var settings = new PixelReadSettings(2, 1, StorageType.Double, PixelMapping.RGBA);

                    using (var image = new MagickImage(data, settings))
                    {
                        Assert.Equal(2, image.Width);
                        Assert.Equal(1, image.Height);

                        using (var pixels = image.GetPixels())
                        {
                            var pixel = pixels.GetPixel(0, 0);
                            Assert.Equal(4, pixel.Channels);
                            Assert.Equal(0, pixel.GetChannel(0));
                            Assert.Equal(0, pixel.GetChannel(1));
                            Assert.Equal(0, pixel.GetChannel(2));
                            Assert.Equal(Quantum.Max, pixel.GetChannel(3));

                            pixel = pixels.GetPixel(1, 0);
                            Assert.Equal(4, pixel.Channels);
                            Assert.Equal(0, pixel.GetChannel(0));
                            Assert.Equal(Quantum.Max, pixel.GetChannel(1));
                            Assert.Equal(0, pixel.GetChannel(2));
                            Assert.Equal(0, pixel.GetChannel(3));
                        }
                    }
                }
            }

            public class WithColor
            {
                [Fact]
                public void ShouldThrowExceptionWhenColorIsNull()
                {
                    Assert.Throws<ArgumentNullException>("color", () =>
                    {
                        new MagickImage((MagickColor)null, 1, 1);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenWidthIsZero()
                {
                    Assert.Throws<ArgumentException>("width", () =>
                    {
                        new MagickImage(MagickColors.Red, 0, 1);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenHeightIsZero()
                {
                    Assert.Throws<ArgumentException>("height", () =>
                    {
                        new MagickImage(MagickColors.Red, 1, 0);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenWidthIsNegative()
                {
                    Assert.Throws<ArgumentException>("width", () =>
                    {
                        new MagickImage(MagickColors.Red, -1, 1);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenHeightIsNegative()
                {
                    Assert.Throws<ArgumentException>("height", () =>
                    {
                        new MagickImage(MagickColors.Red, 1, -1);
                    });
                }

                [Fact]
                public void ShouldReadImage()
                {
                    var color = new MagickColor("red");

                    using (var image = new MagickImage(color, 20, 30))
                    {
                        Assert.Equal(20, image.Width);
                        Assert.Equal(30, image.Height);
                        ColorAssert.Equal(color, image, 10, 10);
                    }
                }
            }

            public class WithFileInfo
            {
                [Fact]
                public void ShouldThrowExceptionWhenFileInfoIsNull()
                {
                    Assert.Throws<ArgumentNullException>("file", () =>
                    {
                        new MagickImage((FileInfo)null);
                    });
                }
            }

            public class WithFileInfoAndMagickFormat
            {
                [Fact]
                public void ShouldThrowExceptionWhenFileInfoIsNull()
                {
                    Assert.Throws<ArgumentNullException>("file", () =>
                    {
                        new MagickImage((FileInfo)null, MagickFormat.Png);
                    });
                }
            }

            public class WithFileInfoAndMagickReadSettings
            {
                [Fact]
                public void ShouldThrowExceptionWhenFileInfoIsNull()
                {
                    Assert.Throws<ArgumentNullException>("file", () =>
                    {
                        var settings = new MagickReadSettings();

                        new MagickImage((FileInfo)null, settings);
                    });
                }

                [Fact]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    using (var image = new MagickImage(new FileInfo(Files.CirclePNG), (MagickReadSettings)null))
                    {
                    }
                }
            }

            public class WithFileInfoAndPixelReadSettings
            {
                [Fact]
                public void ShouldThrowExceptionWhenFileInfoIsNull()
                {
                    Assert.Throws<ArgumentNullException>("file", () =>
                    {
                        var settings = new PixelReadSettings();

                        new MagickImage((FileInfo)null, settings);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenSettingsIsNull()
                {
                    Assert.Throws<ArgumentNullException>("settings", () =>
                    {
                        new MagickImage(new FileInfo(Files.CirclePNG), (PixelReadSettings)null);
                    });
                }

                [Fact]
                public void ShouldReadFileInfo()
                {
                    var settings = new PixelReadSettings(1, 1, StorageType.Quantum, "R");

                    var bytes = BitConverter.GetBytes(Quantum.Max);

                    using (var temporyFile = new TemporaryFile(bytes))
                    {
                        FileInfo file = temporyFile;
                        using (var image = new MagickImage(file, settings))
                        {
                            Assert.Equal(1, image.Width);
                            Assert.Equal(1, image.Height);
                            ColorAssert.Equal(MagickColors.White, image, 0, 0);
                        }
                    }
                }
            }

            public partial class WithFileName
            {
                [Fact]
                public void ShouldThrowExceptionWhenFileNameIsNull()
                {
                    Assert.Throws<ArgumentNullException>("fileName", () =>
                    {
                        new MagickImage((string)null);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    Assert.Throws<ArgumentException>("fileName", () =>
                    {
                        new MagickImage(string.Empty);
                    });
                }

                [Fact]
                public void ShouldUseBaseDirectoryOfCurrentAppDomainWhenFileNameStartsWithTilde()
                {
                    var exception = Assert.Throws<MagickBlobErrorException>(() =>
                    {
                        new MagickImage("~/test.gif");
                    });

                    string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

                    Assert.Contains("error/blob.c/OpenBlob", exception.Message);
                    Assert.Contains(baseDirectory, exception.Message);
                }

                [Fact]
                public void ShouldNotUseBaseDirectoryOfCurrentAppDomainWhenFileNameIsTilde()
                {
                    var exception = Assert.Throws<MagickBlobErrorException>(() =>
                    {
                        new MagickImage("~");
                    });

                    Assert.Contains("error/blob.c/OpenBlob", exception.Message);
                    Assert.Contains("~", exception.Message);
                }
            }

            public class WithFileNameAndMagickFormat
            {
                [Fact]
                public void ShouldThrowExceptionWhenFileNameIsNull()
                {
                    Assert.Throws<ArgumentNullException>("fileName", () =>
                    {
                        new MagickImage((string)null, MagickFormat.Png);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    Assert.Throws<ArgumentException>("fileName", () =>
                    {
                        new MagickImage(string.Empty, MagickFormat.Png);
                    });
                }
            }

            public class WithFileNameAndMagickReadSettings
            {
                [Fact]
                public void ShouldThrowExceptionWhenFileNameIsNull()
                {
                    Assert.Throws<ArgumentNullException>("fileName", () =>
                    {
                        var settings = new MagickReadSettings();

                        new MagickImage((string)null, settings);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    Assert.Throws<ArgumentException>("fileName", () =>
                    {
                        var settings = new MagickReadSettings();

                        new MagickImage(string.Empty, settings);
                    });
                }

                [Fact]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    using (var image = new MagickImage(Files.CirclePNG, (MagickReadSettings)null))
                    {
                    }
                }
            }

            public class WithFileNameAndPixelReadSettings
            {
                [Fact]
                public void ShouldThrowExceptionWhenFileNameIsNull()
                {
                    Assert.Throws<ArgumentNullException>("fileName", () =>
                    {
                        var settings = new PixelReadSettings();

                        new MagickImage((string)null, settings);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    Assert.Throws<ArgumentException>("fileName", () =>
                    {
                        var settings = new PixelReadSettings();

                        new MagickImage(string.Empty, settings);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenSettingsIsNull()
                {
                    Assert.Throws<ArgumentNullException>("settings", () =>
                    {
                        new MagickImage(Files.CirclePNG, (PixelReadSettings)null);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenMappingIsNull()
                {
                    var exception = Assert.Throws<ArgumentException>("settings", () =>
                    {
                        var settings = new PixelReadSettings(1, 1, StorageType.Char, null);

                        new MagickImage(Files.CirclePNG, settings);
                    });

                    Assert.Contains("mapping", exception.Message);
                }

                [Fact]
                public void ShouldThrowExceptionWhenMappingIsEmpty()
                {
                    var exception = Assert.Throws<ArgumentException>("settings", () =>
                    {
                        var settings = new PixelReadSettings(1, 1, StorageType.Char, string.Empty);

                        new MagickImage(Files.CirclePNG, settings);
                    });

                    Assert.Contains("mapping", exception.Message);
                }

                [Fact]
                public void ShouldThrowExceptionWhenWidthIsNull()
                {
                    var exception = Assert.Throws<ArgumentException>("settings", () =>
                    {
                        var settings = new PixelReadSettings(1, 1, StorageType.Char, "RGBA");
                        settings.ReadSettings.Width = null;

                        new MagickImage(Files.CirclePNG, settings);
                    });

                    Assert.Contains("Width", exception.Message);
                }

                [Fact]
                public void ShouldThrowExceptionWhenHeightIsNull()
                {
                    var exception = Assert.Throws<ArgumentException>("settings", () =>
                    {
                        var settings = new PixelReadSettings(1, 1, StorageType.Char, "RGBA");
                        settings.ReadSettings.Height = null;

                        new MagickImage(Files.CirclePNG, settings);
                    });

                    Assert.Contains("Height", exception.Message);
                }

                [Fact]
                public void ShouldReadFileName()
                {
                    var settings = new PixelReadSettings(1, 1, StorageType.Int32, "R");

                    var bytes = BitConverter.GetBytes(4294967295U);

                    using (var temporyFile = new TemporaryFile(bytes))
                    {
                        var fileName = temporyFile.FullName;
                        using (var image = new MagickImage(fileName, settings))
                        {
                            Assert.Equal(1, image.Width);
                            Assert.Equal(1, image.Height);
                            ColorAssert.Equal(MagickColors.White, image, 0, 0);
                        }
                    }
                }
            }

            public class WithFileNameAndSize
            {
                [Fact]
                public void ShouldThrowExceptionWhenColorIsNull()
                {
                    Assert.Throws<ArgumentNullException>("fileName", () =>
                    {
                        new MagickImage((string)null, 1, 1);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenWidthIsZero()
                {
                    Assert.Throws<ArgumentException>("width", () =>
                    {
                        new MagickImage("xc:red", 0, 1);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenHeightIsZero()
                {
                    Assert.Throws<ArgumentException>("height", () =>
                    {
                        new MagickImage("xc:red", 1, 0);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenWidthIsNegative()
                {
                    Assert.Throws<ArgumentException>("width", () =>
                    {
                        new MagickImage("xc:red", -1, 1);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenHeightIsNegative()
                {
                    Assert.Throws<ArgumentException>("height", () =>
                    {
                        new MagickImage("xc:red", 1, -1);
                    });
                }

                [Fact]
                public void ShouldReadImage()
                {
                    using (var image = new MagickImage("xc:red", 20, 30))
                    {
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
                    Assert.Throws<ArgumentNullException>("stream", () =>
                    {
                        new MagickImage((Stream)null);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenStreamIsEmpty()
                {
                    Assert.Throws<ArgumentException>("stream", () =>
                    {
                        var settings = new MagickReadSettings();

                        new MagickImage(new MemoryStream());
                    });
                }
            }

            public class WithStreamAndMagickFormat
            {
                [Fact]
                public void ShouldThrowExceptionWhenStreamIsNull()
                {
                    Assert.Throws<ArgumentNullException>("stream", () =>
                    {
                        new MagickImage((Stream)null, MagickFormat.Png);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenStreamIsEmpty()
                {
                    Assert.Throws<ArgumentException>("stream", () =>
                    {
                        new MagickImage(new MemoryStream(), MagickFormat.Png);
                    });
                }
            }

            public class WithStreamAndMagickReadSettings
            {
                [Fact]
                public void ShouldThrowExceptionWhenStreamIsNull()
                {
                    Assert.Throws<ArgumentNullException>("stream", () =>
                    {
                        var settings = new MagickReadSettings();

                        new MagickImage((Stream)null, settings);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenStreamIsEmpty()
                {
                    Assert.Throws<ArgumentException>("stream", () =>
                    {
                        var settings = new MagickReadSettings();

                        new MagickImage(new MemoryStream(), settings);
                    });
                }

                [Fact]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    using (var fileStream = File.OpenRead(Files.CirclePNG))
                    {
                        using (var image = new MagickImage(fileStream, (MagickReadSettings)null))
                        {
                        }
                    }
                }
            }

            public class WitStreamAndPixelReadSettings
            {
                [Fact]
                public void ShouldThrowExceptionWhenStreamIsNull()
                {
                    Assert.Throws<ArgumentNullException>("stream", () =>
                    {
                        var settings = new PixelReadSettings();

                        new MagickImage((Stream)null, settings);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenStreamIsEmpty()
                {
                    Assert.Throws<ArgumentException>("stream", () =>
                    {
                        var settings = new PixelReadSettings();

                        new MagickImage(new MemoryStream(), settings);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenSettingsIsNull()
                {
                    Assert.Throws<ArgumentNullException>("settings", () =>
                    {
                        new MagickImage(new MemoryStream(new byte[] { 215 }), (PixelReadSettings)null);
                    });
                }

                [Fact]
                public void ShouldReadStream()
                {
                    var settings = new PixelReadSettings(1, 1, StorageType.Double, "R");

                    var bytes = BitConverter.GetBytes(1.0);

                    using (var memoryStream = new MemoryStream(bytes))
                    {
                        using (var image = new MagickImage(memoryStream, settings))
                        {
                            Assert.Equal(1, image.Width);
                            Assert.Equal(1, image.Height);
                            ColorAssert.Equal(MagickColors.White, image, 0, 0);
                        }
                    }
                }
            }
        }
    }
}
