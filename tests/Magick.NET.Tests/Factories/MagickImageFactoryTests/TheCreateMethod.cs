// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageFactoryTests
    {
        public partial class TheCreateMethod
        {
            [Fact]
            public void ShouldCreateMagickImage()
            {
                var factory = new MagickImageFactory();

                using (var image = factory.Create())
                {
                    Assert.IsType<MagickImage>(image);
                    Assert.Equal(0, image.Width);
                }
            }

            public class WithByteArray
            {
                [Fact]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    var factory = new MagickImageFactory();

                    Assert.Throws<ArgumentNullException>("data", () =>
                    {
                        factory.Create((byte[])null);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    var factory = new MagickImageFactory();

                    Assert.Throws<ArgumentException>("data", () =>
                    {
                        factory.Create(new byte[] { });
                    });
                }

                [Fact]
                public void ShouldCreateMagickImage()
                {
                    var factory = new MagickImageFactory();
                    var data = FileHelper.ReadAllBytes(Files.ImageMagickJPG);

                    using (var image = factory.Create(data))
                    {
                        Assert.IsType<MagickImage>(image);
                        Assert.Equal(123, image.Width);
                    }
                }
            }

            public class WithByteArrayAndOffset
            {
                [Fact]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    var factory = new MagickImageFactory();

                    Assert.Throws<ArgumentNullException>("data", () =>
                    {
                        factory.Create((byte[])null, 0, 0);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    var factory = new MagickImageFactory();

                    Assert.Throws<ArgumentException>("data", () =>
                    {
                        factory.Create(new byte[] { }, 0, 0);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenOffsetIsNegative()
                {
                    var factory = new MagickImageFactory();

                    Assert.Throws<ArgumentException>("offset", () =>
                    {
                        factory.Create(new byte[] { 215 }, -1, 0);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenCountIsZero()
                {
                    var factory = new MagickImageFactory();

                    Assert.Throws<ArgumentException>("count", () =>
                    {
                        factory.Create(new byte[] { 215 }, 0, 0);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenCountIsNegative()
                {
                    var factory = new MagickImageFactory();

                    Assert.Throws<ArgumentException>("count", () =>
                    {
                        factory.Create(new byte[] { 215 }, 0, -1);
                    });
                }

                [Fact]
                public void ShouldCreateMagickImage()
                {
                    var factory = new MagickImageFactory();
                    var data = FileHelper.ReadAllBytes(Files.ImageMagickJPG);

                    using (var image = factory.Create(data, 0, data.Length))
                    {
                        Assert.IsType<MagickImage>(image);
                        Assert.Equal(123, image.Width);
                    }
                }
            }

            public class WithByteArrayAndOffsetAndMagickReadSettings
            {
                [Fact]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    var factory = new MagickImageFactory();
                    var settings = new MagickReadSettings();

                    Assert.Throws<ArgumentNullException>("data", () =>
                    {
                        factory.Create(null, 0, 0, settings);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    var factory = new MagickImageFactory();
                    var settings = new MagickReadSettings();

                    Assert.Throws<ArgumentException>("data", () =>
                    {
                        factory.Create(new byte[] { }, 0, 0, settings);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenOffsetIsNegative()
                {
                    var factory = new MagickImageFactory();
                    var settings = new MagickReadSettings();

                    Assert.Throws<ArgumentException>("offset", () =>
                    {
                        factory.Create(new byte[] { 215 }, -1, 0, settings);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenCountIsZero()
                {
                    var factory = new MagickImageFactory();
                    var settings = new MagickReadSettings();

                    Assert.Throws<ArgumentException>("count", () =>
                    {
                        factory.Create(new byte[] { 215 }, 0, 0, settings);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenCountIsNegative()
                {
                    var factory = new MagickImageFactory();
                    var settings = new MagickReadSettings();

                    Assert.Throws<ArgumentException>("count", () =>
                    {
                        factory.Create(new byte[] { 215 }, 0, -1, settings);
                    });
                }

                [Fact]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    var factory = new MagickImageFactory();
                    var bytes = FileHelper.ReadAllBytes(Files.CirclePNG);

                    using (var image = factory.Create(bytes, 0, bytes.Length, (MagickReadSettings)null))
                    {
                    }
                }

                [Fact]
                public void ShouldCreateMagickImage()
                {
                    var factory = new MagickImageFactory();
                    var settings = new MagickReadSettings
                    {
                        BackgroundColor = MagickColors.Purple,
                    };
                    var data = FileHelper.ReadAllBytes(Files.ImageMagickJPG);

                    using (var image = factory.Create(data, 0, data.Length, settings))
                    {
                        Assert.IsType<MagickImage>(image);
                        Assert.Equal(123, image.Width);
                        Assert.Equal(MagickColors.Purple, image.BackgroundColor);
                    }
                }
            }

            public class WithByteArrayAndOffsetAndPixelReadSettings
            {
                [Fact]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    var factory = new MagickImageFactory();
                    var settings = new PixelReadSettings();

                    Assert.Throws<ArgumentNullException>("data", () =>
                    {
                        factory.Create(null, 0, 0, settings);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    var factory = new MagickImageFactory();
                    var settings = new PixelReadSettings();

                    Assert.Throws<ArgumentException>("data", () =>
                    {
                        factory.Create(new byte[] { }, 0, 0, settings);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenOffsetIsNegative()
                {
                    var factory = new MagickImageFactory();
                    var settings = new PixelReadSettings();

                    Assert.Throws<ArgumentException>("offset", () =>
                    {
                        factory.Create(new byte[] { 215 }, -1, 0, settings);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenCountIsZero()
                {
                    var factory = new MagickImageFactory();
                    var settings = new PixelReadSettings();

                    Assert.Throws<ArgumentException>("count", () =>
                    {
                        factory.Create(new byte[] { 215 }, 0, 0, settings);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenCountIsNegative()
                {
                    var factory = new MagickImageFactory();
                    var settings = new PixelReadSettings();

                    Assert.Throws<ArgumentException>("count", () =>
                    {
                        factory.Create(new byte[] { 215 }, 0, -1, settings);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenSettingsIsNull()
                {
                    var factory = new MagickImageFactory();
                    var bytes = FileHelper.ReadAllBytes(Files.CirclePNG);

                    Assert.Throws<ArgumentNullException>("settings", () =>
                    {
                        factory.Create(bytes, 0, bytes.Length, (PixelReadSettings)null);
                    });
                }

                [Fact]
                public void ShouldCreateMagickImage()
                {
                    var factory = new MagickImageFactory();
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

                    using (var image = factory.Create(data, 0, data.Length, settings))
                    {
                        Assert.IsType<MagickImage>(image);
                        Assert.Equal(2, image.Width);
                    }
                }
            }

            public class WithByteArrayAndMagickReadSettings
            {
                [Fact]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    var factory = new MagickImageFactory();
                    var settings = new MagickReadSettings();

                    Assert.Throws<ArgumentNullException>("data", () =>
                    {
                        factory.Create((byte[])null, settings);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    var factory = new MagickImageFactory();
                    var settings = new MagickReadSettings();

                    Assert.Throws<ArgumentException>("data", () =>
                    {
                        factory.Create(new byte[] { }, settings);
                    });
                }

                [Fact]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    var factory = new MagickImageFactory();

                    using (var image = factory.Create(FileHelper.ReadAllBytes(Files.CirclePNG), (MagickReadSettings)null))
                    {
                    }
                }

                [Fact]
                public void ShouldCreateMagickImage()
                {
                    var factory = new MagickImageFactory();
                    var data = FileHelper.ReadAllBytes(Files.ImageMagickJPG);
                    var readSettings = new MagickReadSettings
                    {
                        BackgroundColor = MagickColors.Goldenrod,
                    };

                    using (var image = factory.Create(data, readSettings))
                    {
                        Assert.IsType<MagickImage>(image);
                        Assert.Equal(123, image.Width);
                        Assert.Equal(MagickColors.Goldenrod, image.Settings.BackgroundColor);
                    }
                }
            }

            public class WithByteArrayAndPixelReadSettings
            {
                [Fact]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    var factory = new MagickImageFactory();
                    var settings = new PixelReadSettings();

                    Assert.Throws<ArgumentNullException>("data", () =>
                    {
                        factory.Create((byte[])null, settings);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    var factory = new MagickImageFactory();
                    var settings = new PixelReadSettings();

                    Assert.Throws<ArgumentException>("data", () =>
                    {
                        factory.Create(new byte[] { }, settings);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenSettingsIsNull()
                {
                    var factory = new MagickImageFactory();

                    Assert.Throws<ArgumentNullException>("settings", () =>
                    {
                        factory.Create(FileHelper.ReadAllBytes(Files.CirclePNG), (PixelReadSettings)null);
                    });
                }

                [Fact]
                public void ShouldCreateMagickImage()
                {
                    var factory = new MagickImageFactory();
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

                    using (var image = factory.Create(data, settings))
                    {
                        Assert.IsType<MagickImage>(image);
                        Assert.Equal(2, image.Width);
                    }
                }
            }

            public class WithColor
            {
                [Fact]
                public void ShouldThrowExceptionWhenColorIsNull()
                {
                    var factory = new MagickImageFactory();

                    Assert.Throws<ArgumentNullException>("color", () =>
                    {
                        factory.Create((MagickColor)null, 1, 1);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenWidthIsZero()
                {
                    var factory = new MagickImageFactory();

                    Assert.Throws<ArgumentException>("width", () =>
                    {
                        factory.Create(MagickColors.Red, 0, 1);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenHeightIsZero()
                {
                    var factory = new MagickImageFactory();

                    Assert.Throws<ArgumentException>("height", () =>
                    {
                        factory.Create(MagickColors.Red, 1, 0);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenWidthIsNegative()
                {
                    var factory = new MagickImageFactory();

                    Assert.Throws<ArgumentException>("width", () =>
                    {
                        factory.Create(MagickColors.Red, -1, 1);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenHeightIsNegative()
                {
                    var factory = new MagickImageFactory();

                    Assert.Throws<ArgumentException>("height", () =>
                    {
                        factory.Create(MagickColors.Red, 1, -1);
                    });
                }

                [Fact]
                public void ShouldCreateMagickImage()
                {
                    var factory = new MagickImageFactory();
                    var color = MagickColors.Goldenrod;

                    using (var image = factory.Create(color, 10, 5))
                    {
                        Assert.IsType<MagickImage>(image);
                        Assert.Equal(10, image.Width);
                    }
                }
            }

            public class WithFileInfo
            {
                [Fact]
                public void ShouldThrowExceptionWhenFileInfoIsNull()
                {
                    var factory = new MagickImageFactory();

                    Assert.Throws<ArgumentNullException>("file", () =>
                    {
                        factory.Create((FileInfo)null);
                    });
                }

                [Fact]
                public void ShouldCreateMagickImage()
                {
                    var factory = new MagickImageFactory();
                    var file = new FileInfo(Files.ImageMagickJPG);

                    using (var image = factory.Create(file))
                    {
                        Assert.IsType<MagickImage>(image);
                        Assert.Equal(123, image.Width);
                    }
                }
            }

            public class WithFileInfoAndMagickReadSettings
            {
                [Fact]
                public void ShouldThrowExceptionWhenFileInfoIsNull()
                {
                    var factory = new MagickImageFactory();
                    var settings = new MagickReadSettings();

                    Assert.Throws<ArgumentNullException>("file", () =>
                    {
                        factory.Create((FileInfo)null, settings);
                    });
                }

                [Fact]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    var factory = new MagickImageFactory();

                    using (var image = factory.Create(new FileInfo(Files.CirclePNG), (MagickReadSettings)null))
                    {
                        Assert.IsType<MagickImage>(image);
                    }
                }
            }

            public class WithFileInfoAndPixelReadSettings
            {
                [Fact]
                public void ShouldThrowExceptionWhenFileInfoIsNull()
                {
                    var factory = new MagickImageFactory();
                    var settings = new PixelReadSettings();

                    Assert.Throws<ArgumentNullException>("file", () =>
                    {
                        factory.Create((FileInfo)null, settings);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenSettingsIsNull()
                {
                    var factory = new MagickImageFactory();

                    Assert.Throws<ArgumentNullException>("settings", () =>
                    {
                        factory.Create(new FileInfo(Files.CirclePNG), (PixelReadSettings)null);
                    });
                }

                [Fact]
                public void ShouldCreateMagickImage()
                {
                    var factory = new MagickImageFactory();
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

                    using (var temporaryFile = new TemporaryFile(data))
                    {
                        using (var image = factory.Create(temporaryFile, settings))
                        {
                            Assert.IsType<MagickImage>(image);
                            Assert.Equal(2, image.Width);
                        }
                    }
                }
            }

            public class WithFileNameAndSize
            {
                [Fact]
                public void ShouldThrowExceptionWhenColorIsNull()
                {
                    var factory = new MagickImageFactory();

                    Assert.Throws<ArgumentNullException>("fileName", () =>
                    {
                        factory.Create((string)null, 1, 1);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenWidthIsZero()
                {
                    var factory = new MagickImageFactory();

                    Assert.Throws<ArgumentException>("width", () =>
                    {
                        factory.Create("xc:red", 0, 1);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenHeightIsZero()
                {
                    var factory = new MagickImageFactory();

                    Assert.Throws<ArgumentException>("height", () =>
                    {
                        factory.Create("xc:red", 1, 0);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenWidthIsNegative()
                {
                    var factory = new MagickImageFactory();

                    Assert.Throws<ArgumentException>("width", () =>
                    {
                        factory.Create("xc:red", -1, 1);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenHeightIsNegative()
                {
                    var factory = new MagickImageFactory();

                    Assert.Throws<ArgumentException>("height", () =>
                    {
                        factory.Create("xc:red", 1, -1);
                    });
                }

                [Fact]
                public void ShouldReadImage()
                {
                    var factory = new MagickImageFactory();

                    using (var image = factory.Create("xc:red", 20, 30))
                    {
                        Assert.Equal(20, image.Width);
                        Assert.Equal(30, image.Height);
                        ColorAssert.Equal(MagickColors.Red, image, 10, 10);
                    }
                }
            }

            public class WithFileName
            {
                [Fact]
                public void ShouldThrowExceptionWhenFileInfoIsNull()
                {
                    var factory = new MagickImageFactory();

                    Assert.Throws<ArgumentNullException>("fileName", () =>
                    {
                        factory.Create((string)null);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    var factory = new MagickImageFactory();

                    Assert.Throws<ArgumentException>("fileName", () =>
                    {
                        factory.Create(string.Empty);
                    });
                }

                [Fact]
                public void ShouldCreateMagickImage()
                {
                    var factory = new MagickImageFactory();

                    using (var image = factory.Create(Files.ImageMagickJPG))
                    {
                        Assert.IsType<MagickImage>(image);
                        Assert.Equal(123, image.Width);
                    }
                }
            }

            public class WithFileNameAndMagickReadSettings
            {
                [Fact]
                public void ShouldThrowExceptionWhenFileNameIsNull()
                {
                    var factory = new MagickImageFactory();
                    var settings = new MagickReadSettings();

                    Assert.Throws<ArgumentNullException>("fileName", () =>
                    {
                        factory.Create((string)null, settings);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    var factory = new MagickImageFactory();
                    var settings = new MagickReadSettings();

                    Assert.Throws<ArgumentException>("fileName", () =>
                    {
                        factory.Create(string.Empty, settings);
                    });
                }

                [Fact]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    var factory = new MagickImageFactory();

                    using (var image = factory.Create(Files.CirclePNG, (MagickReadSettings)null))
                    {
                        Assert.IsType<MagickImage>(image);
                    }
                }
            }

            public class WithFileNameAndPixelReadSettings
            {
                [Fact]
                public void ShouldThrowExceptionWhenFileNameIsNull()
                {
                    var factory = new MagickImageFactory();
                    var settings = new PixelReadSettings();

                    Assert.Throws<ArgumentNullException>("fileName", () =>
                    {
                        factory.Create((string)null, settings);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    var factory = new MagickImageFactory();
                    var settings = new PixelReadSettings();

                    Assert.Throws<ArgumentException>("fileName", () =>
                    {
                        factory.Create(string.Empty, settings);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenSettingsIsNull()
                {
                    var factory = new MagickImageFactory();
                    var settings = new PixelReadSettings();

                    Assert.Throws<ArgumentNullException>("settings", () =>
                    {
                        factory.Create(Files.CirclePNG, (PixelReadSettings)null);
                    });
                }

                [Fact]
                public void ShouldCreateMagickImage()
                {
                    var factory = new MagickImageFactory();
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

                    using (var temporaryFile = new TemporaryFile(data))
                    {
                        using (var image = factory.Create(temporaryFile.FullName, settings))
                        {
                            Assert.IsType<MagickImage>(image);
                            Assert.Equal(2, image.Width);
                        }
                    }
                }
            }

            public class WithStream
            {
                [Fact]
                public void ShouldThrowExceptionWhenStreamIsNull()
                {
                    var factory = new MagickImageFactory();

                    Assert.Throws<ArgumentNullException>("stream", () =>
                    {
                        factory.Create((Stream)null);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenStreamIsEmpty()
                {
                    var factory = new MagickImageFactory();

                    Assert.Throws<ArgumentException>("stream", () =>
                    {
                        factory.Create(new MemoryStream());
                    });
                }

                [Fact]
                public void ShouldCreateMagickImage()
                {
                    var factory = new MagickImageFactory();

                    using (var stream = File.OpenRead(Files.ImageMagickJPG))
                    {
                        using (var image = factory.Create(stream))
                        {
                            Assert.IsType<MagickImage>(image);
                            Assert.Equal(123, image.Width);
                        }
                    }
                }
            }

            public class WithStreamAndMagickReadSettings
            {
                [Fact]
                public void ShouldThrowExceptionWhenStreamIsNull()
                {
                    var factory = new MagickImageFactory();
                    var settings = new MagickReadSettings();

                    Assert.Throws<ArgumentNullException>("stream", () =>
                    {
                        factory.Create((Stream)null, settings);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenStreamIsEmpty()
                {
                    var factory = new MagickImageFactory();
                    var settings = new MagickReadSettings();

                    Assert.Throws<ArgumentException>("stream", () =>
                    {
                        factory.Create(new MemoryStream(), settings);
                    });
                }

                [Fact]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    var factory = new MagickImageFactory();

                    using (var fileStream = File.OpenRead(Files.CirclePNG))
                    {
                        using (var image = factory.Create(fileStream, (MagickReadSettings)null))
                        {
                            Assert.IsType<MagickImage>(image);
                        }
                    }
                }
            }

            public class WithStreamAndPixelReadSettings
            {
                [Fact]
                public void ShouldThrowExceptionWhenStreamIsNull()
                {
                    var factory = new MagickImageFactory();
                    var settings = new PixelReadSettings();

                    Assert.Throws<ArgumentNullException>("stream", () =>
                    {
                        factory.Create((Stream)null, settings);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenStreamIsEmpty()
                {
                    var factory = new MagickImageFactory();
                    var settings = new PixelReadSettings();

                    Assert.Throws<ArgumentException>("stream", () =>
                    {
                        factory.Create(new MemoryStream(), settings);
                    });
                }

                [Fact]
                public void ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    var factory = new MagickImageFactory();

                    using (var fileStream = File.OpenRead(Files.CirclePNG))
                    {
                        Assert.Throws<ArgumentNullException>("settings", () =>
                        {
                            factory.Create(fileStream, (PixelReadSettings)null);
                        });
                    }
                }

                [Fact]
                public void ShouldCreateMagickImage()
                {
                    var factory = new MagickImageFactory();
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

                    using (var stream = new MemoryStream(data))
                    {
                        using (var image = factory.Create(stream, settings))
                        {
                            Assert.IsType<MagickImage>(image);
                            Assert.Equal(2, image.Width);
                        }
                    }
                }
            }
        }
    }
}
