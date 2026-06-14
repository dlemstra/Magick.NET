// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using System.Threading.Tasks;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageCollectionTests
{
    public partial class ThePingAsyncMethod
    {
        public class WithFileInfo
        {
            [Fact]
            public async Task ShouldThrowExceptionWhenFileInfoIsNull()
            {
                using var images = new MagickImageCollection();

                await Assert.ThrowsAsync<ArgumentNullException>("file", () => images.PingAsync((FileInfo)null!, TestContext.Current.CancellationToken));
            }
        }

        public class WithFileInfoAndMagickReadSettings
        {
            [Fact]
            public async Task ShouldNotThrowExceptionWhenSettingsIsNull()
            {
                var file = new FileInfo(Files.SnakewarePNG);

                using var images = new MagickImageCollection();
                await images.PingAsync(file, null, TestContext.Current.CancellationToken);

                Assert.Single(images);
                Assert.Throws<InvalidOperationException>(() => images[0].GetPixelsUnsafe());
            }
        }

        public class WithFileName
        {
            [Fact]
            public async Task ShouldThrowExceptionWhenFileNameIsNull()
            {
                using var images = new MagickImageCollection();

                await Assert.ThrowsAsync<ArgumentNullException>("fileName", () => images.PingAsync((string)null!, TestContext.Current.CancellationToken));
            }

            [Fact]
            public async Task ShouldThrowExceptionWhenFileNameIsEmpty()
            {
                using var images = new MagickImageCollection();

                await Assert.ThrowsAsync<ArgumentException>("fileName", () => images.PingAsync(string.Empty, TestContext.Current.CancellationToken));
            }

            [Fact]
            public async Task ShouldThrowExceptionWhenFileNameIsInvalid()
            {
                using var images = new MagickImageCollection();

                await Assert.ThrowsAsync<DirectoryNotFoundException>(() => images.PingAsync(Files.Missing, TestContext.Current.CancellationToken));
            }

            [Fact]
            public async Task ShouldResetTheFormatAfterReading()
            {
                var settings = new MagickReadSettings
                {
                    Format = MagickFormat.Png,
                };

                using var images = new MagickImageCollection();
                await images.PingAsync(Files.CirclePNG, settings, TestContext.Current.CancellationToken);

                Assert.Equal(MagickFormat.Unknown, images[0].Settings.Format);
                Assert.Throws<InvalidOperationException>(() => images[0].GetPixelsUnsafe());
            }
        }

        public class WithFileNameAndMagickReadSettings
        {
            [Fact]
            public async Task ShouldNotThrowExceptionWhenFileNameSettingsIsNull()
            {
                using var images = new MagickImageCollection();
                await images.PingAsync(Files.SnakewarePNG, null, TestContext.Current.CancellationToken);

                Assert.Single(images);
                Assert.Throws<InvalidOperationException>(() => images[0].GetPixelsUnsafe());
            }

            [Fact]
            public async Task ShouldUseTheReadSettings()
            {
                var settings = new MagickReadSettings
                {
                    FontFamily = "Courier New",
                    FillColor = MagickColors.Gold,
                    FontPointsize = 80,
                    Format = MagickFormat.Text,
                    TextGravity = Gravity.Center,
                };

                using var images = new MagickImageCollection();
                await images.PingAsync(Files.ImageMagickTXT, settings, TestContext.Current.CancellationToken);

                Assert.Equal(2, images.Count);
            }
        }

        public class WithStream
        {
            [Fact]
            public async Task ShouldThrowExceptionWhenStreamIsNull()
            {
                using var images = new MagickImageCollection();

                await Assert.ThrowsAsync<ArgumentNullException>("stream", () => images.PingAsync((Stream)null!, TestContext.Current.CancellationToken));
            }

            [Fact]
            public async Task ShouldResetTheFormatAfterReading()
            {
                var settings = new MagickReadSettings
                {
                    Format = MagickFormat.Png,
                };

                using var stream = File.OpenRead(Files.CirclePNG);
                using var images = new MagickImageCollection();
                await images.PingAsync(stream, settings, TestContext.Current.CancellationToken);

                Assert.Equal(MagickFormat.Unknown, images[0].Settings.Format);
                Assert.Throws<InvalidOperationException>(() => images[0].GetPixelsUnsafe());
            }
        }

        public class WithStreamAndMagickReadSettings
        {
            [Fact]
            public async Task ShouldNotThrowExceptionWhenStreamSettingsIsNull()
            {
                using var stream = File.OpenRead(Files.SnakewarePNG);
                using var images = new MagickImageCollection();
                await images.PingAsync(stream, null, TestContext.Current.CancellationToken);

                Assert.Single(images);
                Assert.Throws<InvalidOperationException>(() => images[0].GetPixelsUnsafe());
            }
        }
    }
}
