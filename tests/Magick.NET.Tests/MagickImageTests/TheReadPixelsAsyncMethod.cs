// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using System.Threading.Tasks;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheReadPixelsAsyncMethod
    {
        public class WithFileInfo
        {
            [Fact]
            public async Task ShouldThrowExceptionWhenFileInfoIsNull()
            {
                var settings = new PixelReadSettings();

                using var image = new MagickImage();

                await Assert.ThrowsAsync<ArgumentNullException>("file", () => image.ReadPixelsAsync((FileInfo)null, settings));
            }

            [Fact]
            public async Task ShouldThrowExceptionWhenSettingsIsNull()
            {
                using var image = new MagickImage();

                await Assert.ThrowsAsync<ArgumentNullException>("settings", () => image.ReadPixelsAsync(new FileInfo(Files.CirclePNG), null));
            }

            [Fact]
            public async Task ShouldReadFileInfo()
            {
                var settings = new PixelReadSettings(1, 1, StorageType.Float, "R");

                var bytes = BitConverter.GetBytes(1.0F);

                using var tempFile = new TemporaryFile(bytes);
                using var image = new MagickImage();
                await image.ReadPixelsAsync(tempFile.File, settings);

                Assert.Equal(1, image.Width);
                Assert.Equal(1, image.Height);
                ColorAssert.Equal(MagickColors.White, image, 0, 0);
            }
        }

        public class WithFileName
        {
            [Fact]
            public async Task ShouldThrowExceptionWhenFileNameIsNull()
            {
                var settings = new PixelReadSettings();
                using var image = new MagickImage();

                await Assert.ThrowsAsync<ArgumentNullException>("fileName", () => image.ReadPixelsAsync((string)null, settings));
            }

            [Fact]
            public async Task ShouldThrowExceptionWhenFileNameIsEmpty()
            {
                var settings = new PixelReadSettings();
                using var image = new MagickImage();

                await Assert.ThrowsAsync<ArgumentException>("fileName", () => image.ReadPixelsAsync(string.Empty, settings));
            }

            [Fact]
            public async Task ShouldThrowExceptionWhenSettingsIsNull()
            {
                using var image = new MagickImage();

                await Assert.ThrowsAsync<ArgumentNullException>("settings", () => image.ReadPixelsAsync(Files.CirclePNG, null));
            }

            [Fact]
            public async Task ShouldThrowExceptionWhenMappingIsNull()
            {
                var settings = new PixelReadSettings(1, 1, StorageType.Char, null);

                using var image = new MagickImage();

                var exception = await Assert.ThrowsAsync<ArgumentNullException>("settings", () => image.ReadPixelsAsync(Files.CirclePNG, settings));
                Assert.Contains("mapping", exception.Message);
            }

            [Fact]
            public async Task ShouldThrowExceptionWhenMappingIsEmpty()
            {
                var settings = new PixelReadSettings(1, 1, StorageType.Char, string.Empty);

                using var image = new MagickImage();

                var exception = await Assert.ThrowsAsync<ArgumentException>("settings", () => image.ReadPixelsAsync(Files.CirclePNG, settings));
                Assert.Contains("mapping", exception.Message);
            }

            [Fact]
            public async Task ShouldThrowExceptionWhenWidthIsNull()
            {
                var settings = new PixelReadSettings(1, 1, StorageType.Char, "RGBA");
                settings.ReadSettings.Width = null;

                using var image = new MagickImage();

                var exception = await Assert.ThrowsAsync<ArgumentNullException>("settings", () => image.ReadPixelsAsync(Files.CirclePNG, settings));

                Assert.Contains("Width", exception.Message);
            }

            [Fact]
            public async Task ShouldThrowExceptionWhenHeightIsNull()
            {
                var settings = new PixelReadSettings(1, 1, StorageType.Char, "RGBA");
                settings.ReadSettings.Height = null;

                using var image = new MagickImage();

                var exception = await Assert.ThrowsAsync<ArgumentNullException>("settings", () => image.ReadPixelsAsync(Files.CirclePNG, settings));
                Assert.Contains("Height", exception.Message);
            }

            [Fact]
            public async Task ShouldReadFileName()
            {
                var settings = new PixelReadSettings(1, 1, StorageType.Short, "R");
                var bytes = BitConverter.GetBytes(ushort.MaxValue);

                using var tempFile = new TemporaryFile(bytes);
                using var image = new MagickImage();
                await image.ReadPixelsAsync(tempFile.File.FullName, settings);

                Assert.Equal(1, image.Width);
                Assert.Equal(1, image.Height);
                ColorAssert.Equal(MagickColors.White, image, 0, 0);
            }
        }

        public class WithStream
        {
            [Fact]
            public async Task ShouldThrowExceptionWhenStreamIsNull()
            {
                var settings = new PixelReadSettings();
                using var image = new MagickImage();

                await Assert.ThrowsAsync<ArgumentNullException>("stream", () => image.ReadPixelsAsync((Stream)null, settings));
            }

            [Fact]
            public async Task ShouldThrowExceptionWhenStreamIsEmpty()
            {
                var settings = new PixelReadSettings();
                using var image = new MagickImage();

                await Assert.ThrowsAsync<ArgumentException>("stream", () => image.ReadPixelsAsync(new MemoryStream(), settings));
            }

            [Fact]
            public async Task ShouldThrowExceptionWhenSettingsIsNull()
            {
                using var image = new MagickImage();

                await Assert.ThrowsAsync<ArgumentNullException>("settings", () => image.ReadPixelsAsync(new MemoryStream(new byte[] { 215 }), null));
            }

            [Fact]
            public async Task ShouldReadStream()
            {
                var settings = new PixelReadSettings(1, 1, StorageType.Int64, "R");
                var bytes = BitConverter.GetBytes(ulong.MaxValue);

                using var memoryStream = new MemoryStream(bytes);
                using var image = new MagickImage();
                await image.ReadPixelsAsync(memoryStream, settings);

                Assert.Equal(1, image.Width);
                Assert.Equal(1, image.Height);
                ColorAssert.Equal(MagickColors.White, image, 0, 0);
            }
        }
    }
}
