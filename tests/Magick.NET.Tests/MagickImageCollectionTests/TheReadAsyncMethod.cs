// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageCollectionTests
{
    public partial class TheReadAsyncMethod
    {
        public class WithFileInfo
        {
            [Fact]
            public async Task ShouldThrowExceptionWhenFileInfoIsNull()
            {
                using var images = new MagickImageCollection();

                await Assert.ThrowsAsync<ArgumentNullException>("file", () => images.ReadAsync((FileInfo)null));
            }

            public class WithFileInfoAndMagickFormat
            {
                [Fact]
                public async Task ShouldThrowExceptionWhenFileInfoIsNull()
                {
                    using var images = new MagickImageCollection();

                    await Assert.ThrowsAsync<ArgumentNullException>("file", () => images.ReadAsync((FileInfo)null, MagickFormat.Png));
                }

                [Fact]
                public async Task ShouldNotThrowExceptionWhenSettingsIsNull()
                {
                    var file = new FileInfo(Files.SnakewarePNG);

                    using var images = new MagickImageCollection();
                    await images.ReadAsync(file, null);

                    Assert.Single(images);
                }
            }
        }

        public class WithFileInfoAndMagickReadSettings
        {
            [Fact]
            public async Task ShouldThrowExceptionWhenFileInfoIsNull()
            {
                var settings = new MagickReadSettings();
                using var images = new MagickImageCollection();

                await Assert.ThrowsAsync<ArgumentNullException>("file", () => images.ReadAsync((FileInfo)null, settings));
            }

            [Fact]
            public async Task ShouldNotThrowExceptionWhenSettingsIsNull()
            {
                var file = new FileInfo(Files.SnakewarePNG);
                using var images = new MagickImageCollection();
                await images.ReadAsync(file, null);

                Assert.Single(images);
            }
        }

        public class WithFileName
        {
            [Fact]
            public async Task ShouldThrowExceptionWhenFileNameIsNull()
            {
                using var images = new MagickImageCollection();

                await Assert.ThrowsAsync<ArgumentNullException>("fileName", () => images.ReadAsync((string)null));
            }

            [Fact]
            public async Task ShouldThrowExceptionWhenFileNameIsEmpty()
            {
                using var images = new MagickImageCollection();

                await Assert.ThrowsAsync<ArgumentException>("fileName", () => images.ReadAsync(string.Empty));
            }

            [Fact]
            public async Task ShouldResetTheFormatAfterReading()
            {
                var settings = new MagickReadSettings
                {
                    Format = MagickFormat.Png,
                };

                using var images = new MagickImageCollection();
                await images.ReadAsync(Files.CirclePNG, settings);

                Assert.Equal(MagickFormat.Unknown, images[0].Settings.Format);
            }

            [Fact]
            public async Task ShouldUseTheFilename()
            {
                using var images = new MagickImageCollection();
                await images.ReadAsync(Files.ImageMagickICO);

                Assert.Equal(3, images.Count);
                Assert.Equal(64U, images[0].Width);
                Assert.Equal(64U, images[0].Height);
                Assert.Equal(MagickFormat.Ico, images[0].Format);
                Assert.Equal(32U, images[1].Width);
                Assert.Equal(32U, images[1].Height);
                Assert.Equal(MagickFormat.Ico, images[1].Format);
                Assert.Equal(16U, images[2].Width);
                Assert.Equal(16U, images[2].Height);
                Assert.Equal(MagickFormat.Ico, images[2].Format);
            }
        }

        public class WithFileNameAndMagickFormat
        {
            [Fact]
            public async Task ShouldThrowExceptionWhenFileNameIsNull()
            {
                using var images = new MagickImageCollection();

                await Assert.ThrowsAsync<ArgumentNullException>("fileName", () => images.ReadAsync((string)null, MagickFormat.Png));
            }

            [Fact]
            public async Task ShouldThrowExceptionWhenFileNameIsEmpty()
            {
                using var images = new MagickImageCollection();

                await Assert.ThrowsAsync<ArgumentException>("fileName", () => images.ReadAsync(string.Empty, MagickFormat.Png));
            }
        }

        public class WithFileNameAndMagickReadSettings
        {
            [Fact]
            public async Task ShouldThrowExceptionWhenFileNameIsNull()
            {
                var settings = new MagickReadSettings();
                using var images = new MagickImageCollection();

                await Assert.ThrowsAsync<ArgumentNullException>("fileName", () => images.ReadAsync((string)null, settings));
            }

            [Fact]
            public async Task ShouldThrowExceptionWhenFileNameIsEmpty()
            {
                var settings = new MagickReadSettings();
                using var images = new MagickImageCollection();

                await Assert.ThrowsAsync<ArgumentException>("fileName", () => images.ReadAsync(string.Empty, settings));
            }

            [Fact]
            public async Task ShouldNotThrowExceptionWhenSettingsIsNull()
            {
                using var images = new MagickImageCollection();
                await images.ReadAsync(Files.CirclePNG, null);

                Assert.Single(images);
            }
        }

        public class WithStream
        {
            [Fact]
            public async Task ShouldThrowExceptionWhenStreamIsNull()
            {
                using var images = new MagickImageCollection();

                await Assert.ThrowsAsync<ArgumentNullException>("stream", () => images.ReadAsync((Stream)null));
            }

            [Fact]
            public async Task ShouldResetTheFormatAfterReading()
            {
                var settings = new MagickReadSettings
                {
                    Format = MagickFormat.Png,
                };

                using var stream = File.OpenRead(Files.CirclePNG);
                using var input = new MagickImageCollection();
                await input.ReadAsync(stream, settings);

                Assert.Equal(MagickFormat.Unknown, input[0].Settings.Format);
            }
        }

        public class WithStreamAndMagickFormat
        {
            [Fact]
            public async Task ShouldThrowExceptionWhenStreamIsNull()
            {
                using var images = new MagickImageCollection();

                await Assert.ThrowsAsync<ArgumentNullException>("stream", () => images.ReadAsync((Stream)null, MagickFormat.Png));
            }

            [Fact]
            public async Task ShouldThrowExceptionWhenStreamIsEmpty()
            {
                using var images = new MagickImageCollection();

                await Assert.ThrowsAsync<ArgumentException>("stream", () => images.ReadAsync(new MemoryStream(), MagickFormat.Png));
            }

            [Fact]
            public async Task ShouldUseTheCorrectReaderWhenFormatIsSet()
            {
                var bytes = Encoding.ASCII.GetBytes("%PDF-");

                using var stream = new MemoryStream(bytes);
                using var images = new MagickImageCollection();

                var exception = await Assert.ThrowsAsync<MagickCorruptImageErrorException>(() => images.ReadAsync(stream, MagickFormat.Png));

                Assert.Contains("ReadPNGImage", exception.Message);
            }
        }

        public class WithStreamAndMagickReadSettings
        {
            [Fact]
            public async Task ShouldThrowExceptionWhenStreamIsNull()
            {
                var settings = new MagickReadSettings();
                using var images = new MagickImageCollection();

                await Assert.ThrowsAsync<ArgumentNullException>("stream", () => images.ReadAsync((Stream)null, settings));
            }

            [Fact]
            public async Task ShouldThrowExceptionWhenStreamIsEmpty()
            {
                var settings = new MagickReadSettings();
                using var images = new MagickImageCollection();

                await Assert.ThrowsAsync<ArgumentException>("stream", () => images.ReadAsync(new MemoryStream(), settings));
            }

            [Fact]
            public async Task ShouldNotThrowExceptionWhenSettingsIsNull()
            {
                using var fileStream = File.OpenRead(Files.CirclePNG);
                using var images = new MagickImageCollection();

                await images.ReadAsync(fileStream, null);

                Assert.Single(images);
            }

            [Fact]
            public async Task ShouldUseTheCorrectReaderWhenFormatIsSet()
            {
                var bytes = Encoding.ASCII.GetBytes("%PDF-");
                var settings = new MagickReadSettings
                {
                    Format = MagickFormat.Png,
                };

                using var stream = new MemoryStream(bytes);
                using var images = new MagickImageCollection();

                var exception = await Assert.ThrowsAsync<MagickCorruptImageErrorException>(() => images.ReadAsync(stream, settings));

                Assert.Contains("ReadPNGImage", exception.Message);
            }
        }
    }
}
