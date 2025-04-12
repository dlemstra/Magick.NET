// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using System.Threading.Tasks;
using ImageMagick;
using ImageMagick.Factories;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageCollectionFactoryTests
{
    public partial class TheCreateAsyncMethod
    {
        public class WithFileInfo
        {
            [Fact]
            public async Task ShouldThrowExceptionWhenFileInfoIsNull()
            {
                var factory = new MagickImageCollectionFactory();

                await Assert.ThrowsAsync<ArgumentNullException>("file", () => factory.CreateAsync((FileInfo)null!, TestContext.Current.CancellationToken));
            }

            [Fact]
            public async Task ShouldCreateMagickImage()
            {
                var factory = new MagickImageCollectionFactory();
                var file = new FileInfo(Files.ImageMagickJPG);

                using var images = await factory.CreateAsync(file, TestContext.Current.CancellationToken);
                Assert.IsType<MagickImageCollection>(images);
            }
        }

        public class WithFileInfoAndMagickReadSettings
        {
            [Fact]
            public async Task ShouldThrowExceptionWhenFileInfoIsNull()
            {
                var factory = new MagickImageCollectionFactory();
                var settings = new MagickReadSettings();

                await Assert.ThrowsAsync<ArgumentNullException>("file", () => factory.CreateAsync((FileInfo)null!, settings, TestContext.Current.CancellationToken));
            }

            [Fact]
            public async Task ShouldNotThrowExceptionWhenSettingsIsNull()
            {
                var factory = new MagickImageCollectionFactory();

                using var images = await factory.CreateAsync(new FileInfo(Files.CirclePNG), null!, TestContext.Current.CancellationToken);
                Assert.IsType<MagickImageCollection>(images);
            }
        }

        public class WithFileName
        {
            [Fact]
            public async Task ShouldThrowExceptionWhenFileInfoIsNull()
            {
                var factory = new MagickImageCollectionFactory();

                await Assert.ThrowsAsync<ArgumentNullException>("fileName", () => factory.CreateAsync((string)null!, TestContext.Current.CancellationToken));
            }

            [Fact]
            public async Task ShouldThrowExceptionWhenFileNameIsEmpty()
            {
                var factory = new MagickImageCollectionFactory();

                await Assert.ThrowsAsync<ArgumentException>("fileName", () => factory.CreateAsync(string.Empty, TestContext.Current.CancellationToken));
            }

            [Fact]
            public async Task ShouldCreateMagickImage()
            {
                var factory = new MagickImageCollectionFactory();

                using var images = await factory.CreateAsync(Files.ImageMagickJPG, TestContext.Current.CancellationToken);
                Assert.IsType<MagickImageCollection>(images);
            }
        }

        public class WithFileNameAndMagickReadSettings
        {
            [Fact]
            public async Task ShouldThrowExceptionWhenFileNameIsNull()
            {
                var factory = new MagickImageCollectionFactory();
                var settings = new MagickReadSettings();

                await Assert.ThrowsAsync<ArgumentNullException>("fileName", () => factory.CreateAsync((string)null!, settings, TestContext.Current.CancellationToken));
            }

            [Fact]
            public async Task ShouldThrowExceptionWhenFileNameIsEmpty()
            {
                var factory = new MagickImageCollectionFactory();
                var settings = new MagickReadSettings();

                await Assert.ThrowsAsync<ArgumentException>("fileName", () => factory.CreateAsync(string.Empty, settings, TestContext.Current.CancellationToken));
            }

            [Fact]
            public async Task ShouldNotThrowExceptionWhenSettingsIsNull()
            {
                var factory = new MagickImageCollectionFactory();

                using var images = await factory.CreateAsync(Files.CirclePNG, null!, TestContext.Current.CancellationToken);
                Assert.IsType<MagickImageCollection>(images);
            }
        }

        public class WithStream
        {
            [Fact]
            public async Task ShouldThrowExceptionWhenStreamIsNull()
            {
                var factory = new MagickImageCollectionFactory();

                await Assert.ThrowsAsync<ArgumentNullException>("stream", () => factory.CreateAsync((Stream)null!, TestContext.Current.CancellationToken));
            }

            [Fact]
            public async Task ShouldThrowExceptionWhenStreamIsEmpty()
            {
                var factory = new MagickImageCollectionFactory();

                await Assert.ThrowsAsync<ArgumentException>("stream", () => factory.CreateAsync(new MemoryStream(), TestContext.Current.CancellationToken));
            }

            [Fact]
            public async Task ShouldCreateMagickImage()
            {
                var factory = new MagickImageCollectionFactory();

                using var stream = File.OpenRead(Files.ImageMagickJPG);
                using var images = await factory.CreateAsync(stream, TestContext.Current.CancellationToken);
                Assert.IsType<MagickImageCollection>(images);
            }
        }

        public class WithStreamAndMagickReadSettings
        {
            [Fact]
            public async Task ShouldThrowExceptionWhenStreamIsNull()
            {
                var factory = new MagickImageCollectionFactory();
                var settings = new MagickReadSettings();

                await Assert.ThrowsAsync<ArgumentNullException>("stream", () => factory.CreateAsync((Stream)null!, settings, TestContext.Current.CancellationToken));
            }

            [Fact]
            public async Task ShouldThrowExceptionWhenStreamIsEmpty()
            {
                var factory = new MagickImageCollectionFactory();
                var settings = new MagickReadSettings();

                await Assert.ThrowsAsync<ArgumentException>("stream", () => factory.CreateAsync(new MemoryStream(), settings, TestContext.Current.CancellationToken));
            }

            [Fact]
            public async Task ShouldNotThrowExceptionWhenSettingsIsNull()
            {
                var factory = new MagickImageCollectionFactory();

                using var fileStream = File.OpenRead(Files.CirclePNG);
                using var images = await factory.CreateAsync(fileStream, null!, TestContext.Current.CancellationToken);
                Assert.IsType<MagickImageCollection>(images);
            }
        }
    }
}
