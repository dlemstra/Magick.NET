// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if NETCORE

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
            public class WithStream
            {
                [Fact]
                public async Task ShouldThrowExceptionWhenStreamIsNull()
                {
                    var factory = new MagickImageFactory();

                    await Assert.ThrowsAsync<ArgumentNullException>("stream", () => factory.CreateAsync((Stream)null));
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenStreamIsEmpty()
                {
                    var factory = new MagickImageFactory();

                    await Assert.ThrowsAsync<ArgumentException>("stream", () => factory.CreateAsync(new MemoryStream()));
                }

                [Fact]
                public async Task ShouldCreateMagickImage()
                {
                    var factory = new MagickImageFactory();

                    using (var stream = File.OpenRead(Files.ImageMagickJPG))
                    {
                        using (var image = await factory.CreateAsync(stream))
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
                public async Task ShouldThrowExceptionWhenStreamIsNull()
                {
                    var factory = new MagickImageFactory();
                    var settings = new MagickReadSettings();

                    await Assert.ThrowsAsync<ArgumentNullException>("stream", () => factory.CreateAsync((Stream)null, settings));
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenStreamIsEmpty()
                {
                    var factory = new MagickImageFactory();
                    var settings = new MagickReadSettings();

                    await Assert.ThrowsAsync<ArgumentException>("stream", () => factory.CreateAsync(new MemoryStream(), settings));
                }

                [Fact]
                public async Task ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    var factory = new MagickImageFactory();

                    using (var fileStream = File.OpenRead(Files.CirclePNG))
                    {
                        using (var image = await factory.CreateAsync(fileStream, (MagickReadSettings)null))
                        {
                            Assert.IsType<MagickImage>(image);
                        }
                    }
                }
            }

            public class WithStreamAndPixelReadSettings
            {
                [Fact]
                public async Task ShouldThrowExceptionWhenStreamIsNull()
                {
                    var factory = new MagickImageFactory();
                    var settings = new PixelReadSettings();

                    await Assert.ThrowsAsync<ArgumentNullException>("stream", () => factory.CreateAsync((Stream)null, settings));
                }

                [Fact]
                public async Task ShouldThrowExceptionWhenStreamIsEmpty()
                {
                    var factory = new MagickImageFactory();
                    var settings = new PixelReadSettings();

                    await Assert.ThrowsAsync<ArgumentException>("stream", () => factory.CreateAsync(new MemoryStream(), settings));
                }

                [Fact]
                public async Task ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    var factory = new MagickImageFactory();

                    using (var fileStream = File.OpenRead(Files.CirclePNG))
                    {
                        await Assert.ThrowsAsync<ArgumentNullException>("settings", () => factory.CreateAsync(fileStream, (PixelReadSettings)null));
                    }
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

                    using (var stream = new MemoryStream(data))
                    {
                        using (var image = await factory.CreateAsync(stream, settings))
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
