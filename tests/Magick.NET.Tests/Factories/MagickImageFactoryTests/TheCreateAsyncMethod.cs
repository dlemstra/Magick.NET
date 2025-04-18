// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using ImageMagick;
using ImageMagick.Factories;
using Xunit;

namespace Magick.NET.Tests;

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

                await Assert.ThrowsAsync<ArgumentNullException>("file", () => factory.CreateAsync((FileInfo)null!, TestContext.Current.CancellationToken));
            }

            [Fact]
            public async Task ShouldCreateMagickImage()
            {
                var factory = new MagickImageFactory();
                var file = new FileInfo(Files.ImageMagickJPG);

                using var image = await factory.CreateAsync(file, TestContext.Current.CancellationToken);

                Assert.IsType<MagickImage>(image);
                Assert.Equal(123U, image.Width);
            }
        }

        public class WithFileInfoAndMagickReadSettings
        {
            [Fact]
            public async Task ShouldThrowExceptionWhenFileInfoIsNull()
            {
                var factory = new MagickImageFactory();
                var settings = new MagickReadSettings();

                await Assert.ThrowsAsync<ArgumentNullException>("file", () => factory.CreateAsync((FileInfo)null!, settings, TestContext.Current.CancellationToken));
            }

            [Fact]
            public async Task ShouldNotThrowExceptionWhenSettingsIsNull()
            {
                var factory = new MagickImageFactory();

                using var image = await factory.CreateAsync(new FileInfo(Files.CirclePNG), (MagickReadSettings)null!, TestContext.Current.CancellationToken);

                Assert.IsType<MagickImage>(image);
            }
        }

        public class WithFileInfoAndPixelReadSettings
        {
            [Fact]
            public async Task ShouldThrowExceptionWhenFileInfoIsNull()
            {
                var factory = new MagickImageFactory();
                var settings = new PixelReadSettings();

                await Assert.ThrowsAsync<ArgumentNullException>("file", () => factory.CreateAsync((FileInfo)null!, settings, TestContext.Current.CancellationToken));
            }

            [Fact]
            public async Task ShouldThrowExceptionWhenSettingsIsNull()
            {
                var factory = new MagickImageFactory();

                await Assert.ThrowsAsync<ArgumentNullException>("settings", () => factory.CreateAsync(new FileInfo(Files.CirclePNG), (PixelReadSettings)null!, TestContext.Current.CancellationToken));
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

                using var tempFile = new TemporaryFile(data);
                using var image = await factory.CreateAsync(tempFile.File, settings, TestContext.Current.CancellationToken);

                Assert.IsType<MagickImage>(image);
                Assert.Equal(2U, image.Width);
            }
        }

        public class WithFileName
        {
            [Fact]
            public async Task ShouldThrowExceptionWhenFileInfoIsNull()
            {
                var factory = new MagickImageFactory();

                await Assert.ThrowsAsync<ArgumentNullException>("fileName", () => factory.CreateAsync((string)null!, TestContext.Current.CancellationToken));
            }

            [Fact]
            public async Task ShouldThrowExceptionWhenFileNameIsEmpty()
            {
                var factory = new MagickImageFactory();

                await Assert.ThrowsAsync<ArgumentException>("fileName", () => factory.CreateAsync(string.Empty, TestContext.Current.CancellationToken));
            }

            [Fact]
            public async Task ShouldCreateMagickImage()
            {
                var factory = new MagickImageFactory();

                using var image = await factory.CreateAsync(Files.ImageMagickJPG, TestContext.Current.CancellationToken);

                Assert.IsType<MagickImage>(image);
                Assert.Equal(123U, image.Width);
            }
        }

        public class WithFileNameAndMagickReadSettings
        {
            [Fact]
            public async Task ShouldThrowExceptionWhenFileNameIsNull()
            {
                var factory = new MagickImageFactory();
                var settings = new MagickReadSettings();

                await Assert.ThrowsAsync<ArgumentNullException>("fileName", () => factory.CreateAsync((string)null!, settings, TestContext.Current.CancellationToken));
            }

            [Fact]
            public async Task ShouldThrowExceptionWhenFileNameIsEmpty()
            {
                var factory = new MagickImageFactory();
                var settings = new MagickReadSettings();

                await Assert.ThrowsAsync<ArgumentException>("fileName", () => factory.CreateAsync(string.Empty, settings, TestContext.Current.CancellationToken));
            }

            [Fact]
            public async Task ShouldNotThrowExceptionWhenSettingsIsNull()
            {
                var factory = new MagickImageFactory();

                using var image = await factory.CreateAsync(Files.CirclePNG, (MagickReadSettings)null!, TestContext.Current.CancellationToken);

                Assert.IsType<MagickImage>(image);
            }
        }

        public class WithFileNameAndPixelReadSettings
        {
            [Fact]
            public async Task ShouldThrowExceptionWhenFileNameIsNull()
            {
                var factory = new MagickImageFactory();
                var settings = new PixelReadSettings();

                await Assert.ThrowsAsync<ArgumentNullException>("fileName", () => factory.CreateAsync((string)null!, settings, TestContext.Current.CancellationToken));
            }

            [Fact]
            public async Task ShouldThrowExceptionWhenFileNameIsEmpty()
            {
                var factory = new MagickImageFactory();
                var settings = new PixelReadSettings();

                await Assert.ThrowsAsync<ArgumentException>("fileName", () => factory.CreateAsync(string.Empty, settings, TestContext.Current.CancellationToken));
            }

            [Fact]
            public async Task ShouldThrowExceptionWhenSettingsIsNull()
            {
                var factory = new MagickImageFactory();
                var settings = new PixelReadSettings();

                await Assert.ThrowsAsync<ArgumentNullException>("settings", () => factory.CreateAsync(Files.CirclePNG, (PixelReadSettings)null!, TestContext.Current.CancellationToken));
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

                using var tempFile = new TemporaryFile(data);
                using var image = await factory.CreateAsync(tempFile.File.FullName, settings, TestContext.Current.CancellationToken);

                Assert.IsType<MagickImage>(image);
                Assert.Equal(2U, image.Width);
            }
        }

        public class WithStream
        {
            [Fact]
            public async Task ShouldThrowExceptionWhenStreamIsNull()
            {
                var factory = new MagickImageFactory();

                await Assert.ThrowsAsync<ArgumentNullException>("stream", () => factory.CreateAsync((Stream)null!, TestContext.Current.CancellationToken));
            }

            [Fact]
            public async Task ShouldThrowExceptionWhenStreamIsEmpty()
            {
                var factory = new MagickImageFactory();

                await Assert.ThrowsAsync<ArgumentException>("stream", () => factory.CreateAsync(new MemoryStream(), TestContext.Current.CancellationToken));
            }

            [Fact]
            public async Task ShouldCreateMagickImage()
            {
                var factory = new MagickImageFactory();

                using var stream = File.OpenRead(Files.ImageMagickJPG);
                using var image = await factory.CreateAsync(stream, TestContext.Current.CancellationToken);

                Assert.IsType<MagickImage>(image);
                Assert.Equal(123U, image.Width);
            }
        }

        public class WithStreamAndMagickReadSettings
        {
            [Fact]
            public async Task ShouldThrowExceptionWhenStreamIsNull()
            {
                var factory = new MagickImageFactory();
                var settings = new MagickReadSettings();

                await Assert.ThrowsAsync<ArgumentNullException>("stream", () => factory.CreateAsync((Stream)null!, settings, TestContext.Current.CancellationToken));
            }

            [Fact]
            public async Task ShouldThrowExceptionWhenStreamIsEmpty()
            {
                var factory = new MagickImageFactory();
                var settings = new MagickReadSettings();

                await Assert.ThrowsAsync<ArgumentException>("stream", () => factory.CreateAsync(new MemoryStream(), settings, TestContext.Current.CancellationToken));
            }

            [Fact]
            public async Task ShouldNotThrowExceptionWhenSettingsIsNull()
            {
                var factory = new MagickImageFactory();

                using var fileStream = File.OpenRead(Files.CirclePNG);
                using var image = await factory.CreateAsync(fileStream, (MagickReadSettings)null!, TestContext.Current.CancellationToken);

                Assert.IsType<MagickImage>(image);
            }
        }

        public class WithStreamAndPixelReadSettings
        {
            [Fact]
            public async Task ShouldThrowExceptionWhenStreamIsNull()
            {
                var factory = new MagickImageFactory();
                var settings = new PixelReadSettings();

                await Assert.ThrowsAsync<ArgumentNullException>("stream", () => factory.CreateAsync((Stream)null!, settings, TestContext.Current.CancellationToken));
            }

            [Fact]
            public async Task ShouldThrowExceptionWhenStreamIsEmpty()
            {
                var factory = new MagickImageFactory();
                var settings = new PixelReadSettings();

                await Assert.ThrowsAsync<ArgumentException>("stream", () => factory.CreateAsync(new MemoryStream(), settings, TestContext.Current.CancellationToken));
            }

            [Fact]
            public async Task ShouldNotThrowExceptionWhenSettingsIsNull()
            {
                var factory = new MagickImageFactory();

                using var fileStream = File.OpenRead(Files.CirclePNG);

                await Assert.ThrowsAsync<ArgumentNullException>("settings", () => factory.CreateAsync(fileStream, (PixelReadSettings)null!, TestContext.Current.CancellationToken));
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

                using var stream = new MemoryStream(data);
                using var image = await factory.CreateAsync(stream, settings, TestContext.Current.CancellationToken);

                Assert.IsType<MagickImage>(image);
                Assert.Equal(2U, image.Width);
            }

            [Fact]
            public async Task ShouldCreateMagickImageFromNonSeekableStream()
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

                using var stream = new NonSeekableStream(data);
                using var image = await factory.CreateAsync(stream, settings, CancellationToken.None);

                Assert.IsType<MagickImage>(image);
                Assert.Equal(2U, image.Width);
            }
        }
    }
}
