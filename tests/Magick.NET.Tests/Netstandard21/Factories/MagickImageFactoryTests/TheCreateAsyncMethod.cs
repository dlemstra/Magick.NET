// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if NETCOREAPP

using System;
using System.IO;
using System.Threading.Tasks;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageFactoryTests
    {
        public partial class TheCreateAsyncMethod
        {
            public class WithFileInfo
            {
                [Fact]
                public async Task ShouldThrowExceptionWhenFileInfoIsNull()
                {
                    var factory = new MagickImageFactory();

                    await Assert.ThrowsAsync<ArgumentNullException>("file", () => factory.CreateAsync((FileInfo)null));
                }

                [Fact]
                public async Task ShouldCreateMagickImage()
                {
                    var factory = new MagickImageFactory();
                    var file = new FileInfo(Files.ImageMagickJPG);

                    using (var image = await factory.CreateAsync(file))
                    {
                        Assert.IsType<MagickImage>(image);
                        Assert.Equal(123, image.Width);
                    }
                }
            }

            public class WithFileInfoAndMagickReadSettings
            {
                [Fact]
                public async Task ShouldThrowExceptionWhenFileInfoIsNull()
                {
                    var factory = new MagickImageFactory();
                    var settings = new MagickReadSettings();

                    await Assert.ThrowsAsync<ArgumentNullException>("file", () => factory.CreateAsync((FileInfo)null, settings));
                }

                [Fact]
                public async Task ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    var factory = new MagickImageFactory();

                    using (var image = await factory.CreateAsync(new FileInfo(Files.CirclePNG), (MagickReadSettings)null))
                    {
                        Assert.IsType<MagickImage>(image);
                    }
                }
            }

            public class WithFileInfoAndPixelReadSettings
            {
                [Fact]
                public async Task ShouldThrowExceptionWhenFileInfoIsNull()
                {
                    var factory = new MagickImageFactory();
                    var settings = new PixelReadSettings();

                    await Assert.ThrowsAsync<ArgumentNullException>("file", () => factory.CreateAsync((FileInfo)null, settings));
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenSettingsIsNull()
                {
                    var factory = new MagickImageFactory();

                    await Assert.ThrowsAsync<ArgumentNullException>("settings", () => factory.CreateAsync(new FileInfo(Files.CirclePNG), (PixelReadSettings)null));
                }

                [Fact]
                public async Task ShouldCreateMagickImage()
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
                        using (var image = await factory.CreateAsync(temporaryFile.FileInfo, settings))
                        {
                            Assert.IsType<MagickImage>(image);
                            Assert.Equal(2, image.Width);
                        }
                    }
                }
            }

            public class WithFileName
            {
                [Fact]
                public async Task ShouldThrowExceptionWhenFileInfoIsNull()
                {
                    var factory = new MagickImageFactory();

                    await Assert.ThrowsAsync<ArgumentNullException>("fileName", () => factory.CreateAsync((string)null));
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    var factory = new MagickImageFactory();

                    await Assert.ThrowsAsync<ArgumentException>("fileName", () => factory.CreateAsync(string.Empty));
                }

                [Fact]
                public async Task ShouldCreateMagickImage()
                {
                    var factory = new MagickImageFactory();

                    using (var image = await factory.CreateAsync(Files.ImageMagickJPG))
                    {
                        Assert.IsType<MagickImage>(image);
                        Assert.Equal(123, image.Width);
                    }
                }
            }

            public class WithFileNameAndMagickReadSettings
            {
                [Fact]
                public async Task ShouldThrowExceptionWhenFileNameIsNull()
                {
                    var factory = new MagickImageFactory();
                    var settings = new MagickReadSettings();

                    await Assert.ThrowsAsync<ArgumentNullException>("fileName", () => factory.CreateAsync((string)null, settings));
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    var factory = new MagickImageFactory();
                    var settings = new MagickReadSettings();

                    await Assert.ThrowsAsync<ArgumentException>("fileName", () => factory.CreateAsync(string.Empty, settings));
                }

                [Fact]
                public async Task ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    var factory = new MagickImageFactory();

                    using (var image = await factory.CreateAsync(Files.CirclePNG, (MagickReadSettings)null))
                    {
                        Assert.IsType<MagickImage>(image);
                    }
                }
            }

            public class WithFileNameAndPixelReadSettings
            {
                [Fact]
                public async Task ShouldThrowExceptionWhenFileNameIsNull()
                {
                    var factory = new MagickImageFactory();
                    var settings = new PixelReadSettings();

                    await Assert.ThrowsAsync<ArgumentNullException>("fileName", () => factory.CreateAsync((string)null, settings));
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    var factory = new MagickImageFactory();
                    var settings = new PixelReadSettings();

                    await Assert.ThrowsAsync<ArgumentException>("fileName", () => factory.CreateAsync(string.Empty, settings));
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenSettingsIsNull()
                {
                    var factory = new MagickImageFactory();
                    var settings = new PixelReadSettings();

                    await Assert.ThrowsAsync<ArgumentNullException>("settings", () => factory.CreateAsync(Files.CirclePNG, (PixelReadSettings)null));
                }

                [Fact]
                public async Task ShouldCreateMagickImage()
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
                        using (var image = await factory.CreateAsync(temporaryFile.FullName, settings))
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

#endif
