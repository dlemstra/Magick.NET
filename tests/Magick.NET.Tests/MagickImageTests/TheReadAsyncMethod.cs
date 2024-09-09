// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public partial class TheReadAsyncMethod
    {
        public class WithFileInfo
        {
            [Fact]
            public async Task ShouldThrowExceptionWhenFileInfoIsNull()
            {
                using var image = new MagickImage();

                await Assert.ThrowsAsync<ArgumentNullException>("file", () => image.ReadAsync((FileInfo)null!));
            }
        }

        public class WithFileInfoAndMagickFormat
        {
            [Fact]
            public async Task ShouldThrowExceptionWhenFileInfoIsNull()
            {
                using var image = new MagickImage();

                await Assert.ThrowsAsync<ArgumentNullException>("file", () => image.ReadAsync((FileInfo)null!, MagickFormat.Png));
            }
        }

        public class WithFileInfoAndMagickReadSettings
        {
            [Fact]
            public async Task ShouldThrowExceptionWhenFileInfoIsNull()
            {
                var settings = new MagickReadSettings();
                using var image = new MagickImage();

                await Assert.ThrowsAsync<ArgumentNullException>("file", () => image.ReadAsync((FileInfo)null!, settings));
            }

            [Fact]
            public async Task ShouldNotThrowExceptionWhenSettingsIsNull()
            {
                using var image = new MagickImage();
                await image.ReadAsync(new FileInfo(Files.CirclePNG), null);
            }
        }

        public class WithFileName
        {
            [Fact]
            public async Task ShouldThrowExceptionWhenFileNameIsNull()
            {
                using var image = new MagickImage();

                await Assert.ThrowsAsync<ArgumentNullException>("fileName", () => image.ReadAsync((string)null!));
            }

            [Fact]
            public async Task ShouldThrowExceptionWhenFileNameIsEmpty()
            {
                using var image = new MagickImage();

                await Assert.ThrowsAsync<ArgumentException>("fileName", () => image.ReadAsync(string.Empty));
            }

            [Fact]
            public async Task ShouldReadImage()
            {
                using var image = new MagickImage();
                await image.ReadAsync(Files.SnakewarePNG);

                Assert.Equal(286U, image.Width);
                Assert.Equal(67U, image.Height);
                Assert.Equal(MagickFormat.Png, image.Format);
            }

            [Fact]
            public async Task ShouldUseTheFilename()
            {
                using var image = new MagickImage();
                await image.ReadAsync(Files.ImageMagickICO);

                Assert.Equal(64U, image.Width);
                Assert.Equal(64U, image.Height);
                Assert.Equal(MagickFormat.Ico, image.Format);
            }

            [Fact]
            public async Task ShouldNotUseBaseDirectoryOfCurrentAppDomainWhenFileNameIsTilde()
            {
                using var image = new MagickImage();

                var exception = await Assert.ThrowsAsync<FileNotFoundException>(() => image.ReadAsync("~"));
                Assert.Contains("~", exception.Message);
            }
        }

        public class WithFileNameAndMagickFormat
        {
            [Fact]
            public async Task ShouldThrowExceptionWhenFileNameIsNull()
            {
                using var image = new MagickImage();

                await Assert.ThrowsAsync<ArgumentNullException>("fileName", () => image.ReadAsync((string)null!, MagickFormat.Png));
            }

            [Fact]
            public async Task ShouldThrowExceptionWhenFileNameIsEmpty()
            {
                using var image = new MagickImage();

                await Assert.ThrowsAsync<ArgumentException>("fileName", () => image.ReadAsync(string.Empty, MagickFormat.Png));
            }

            [Fact]
            public async Task ShouldResetTheFormatAfterReadingFile()
            {
                using var image = new MagickImage();
                await image.ReadAsync(Files.CirclePNG, MagickFormat.Png);

                Assert.Equal(MagickFormat.Unknown, image.Settings.Format);
            }
        }

        public class WithFileNameAndMagickReadSettings
        {
            [Fact]
            public async Task ShouldThrowExceptionWhenFileNameIsNull()
            {
                var settings = new MagickReadSettings();
                using var image = new MagickImage();

                await Assert.ThrowsAsync<ArgumentNullException>("fileName", () => image.ReadAsync((string)null!, settings));
            }

            [Fact]
            public async Task ShouldThrowExceptionWhenFileNameIsEmpty()
            {
                var settings = new MagickReadSettings();
                using var image = new MagickImage();

                await Assert.ThrowsAsync<ArgumentException>("fileName", () => image.ReadAsync(string.Empty, settings));
            }

            [Fact]
            public async Task ShouldNotThrowExceptionWhenSettingsIsNull()
            {
                using var image = new MagickImage();
                await image.ReadAsync(Files.CirclePNG, null);
            }

            [Fact]
            public async Task ShouldResetTheFormatAfterReadingFile()
            {
                var settings = new MagickReadSettings
                {
                    Format = MagickFormat.Png,
                };
                using var image = new MagickImage();
                await image.ReadAsync(Files.CirclePNG, settings);

                Assert.Equal(MagickFormat.Unknown, image.Settings.Format);
            }

            [Fact]
            public async Task ShouldUseTheReadSettings()
            {
                using var image = new MagickImage();
                await image.ReadAsync(Files.Logos.MagickNETSVG, new MagickReadSettings
                {
                    Density = new Density(72),
                });

                ColorAssert.Equal(new MagickColor("#231f20"), image, 129, 101);
            }
        }

        public class WithStream
        {
            [Fact]
            public async Task ShouldThrowExceptionWhenStreamIsNull()
            {
                using var image = new MagickImage();

                await Assert.ThrowsAsync<ArgumentNullException>("stream", () => image.ReadAsync((Stream)null!));
            }

            [Fact]
            public async Task ShouldThrowExceptionWhenStreamIsEmpty()
            {
                using var image = new MagickImage();

                await Assert.ThrowsAsync<ArgumentException>("stream", () => image.ReadAsync(new MemoryStream()));
            }

            [Fact]
            public async Task ShouldThrowExceptionWhenStreamIsNotReadable()
            {
                using var testStream = TestStream.ThatCannotRead();
                using var image = new MagickImage();

                await Assert.ThrowsAsync<ArgumentException>("stream", () => image.ReadAsync(testStream));
            }

            [Fact]
            public async Task ShouldReadImage()
            {
                using var image = new MagickImage();
                using var fileStream = File.OpenRead(Files.SnakewarePNG);
                await image.ReadAsync(fileStream);

                Assert.Equal(286U, image.Width);
                Assert.Equal(67U, image.Height);
                Assert.Equal(MagickFormat.Png, image.Format);
            }

            [Fact]
            public async Task ShouldReadImageFromSeekablePartialStream()
            {
                using var image = new MagickImage();
                using var fileStream = File.OpenRead(Files.ImageMagickJPG);
                await image.ReadAsync(fileStream);

                fileStream.Position = 0;
                using var partialStream = new PartialStream(fileStream, true);
                using var testImage = new MagickImage();
                await testImage.ReadAsync(partialStream);

                Assert.Equal(image.Width, testImage.Width);
                Assert.Equal(image.Height, testImage.Height);
                Assert.Equal(image.Format, testImage.Format);
                Assert.Equal(0.0, image.Compare(testImage, ErrorMetric.RootMeanSquared));
            }

            [Fact]
            public async Task ShouldReadImageFromNonSeekablePartialStream()
            {
                using var image = new MagickImage();
                using var fileStream = File.OpenRead(Files.ImageMagickJPG);
                await image.ReadAsync(fileStream);

                fileStream.Position = 0;
                using var partialStream = new PartialStream(fileStream, false);
                using var testImage = new MagickImage();
                await testImage.ReadAsync(partialStream);

                Assert.Equal(image.Width, testImage.Width);
                Assert.Equal(image.Height, testImage.Height);
                Assert.Equal(image.Format, testImage.Format);
                Assert.Equal(0.0, image.Compare(testImage, ErrorMetric.RootMeanSquared));
            }
        }

        public class WithStreamAndMagickFormat
        {
            [Fact]
            public async Task ShouldThrowExceptionWhenStreamIsNull()
            {
                using var image = new MagickImage();

                await Assert.ThrowsAsync<ArgumentNullException>("stream", () => image.ReadAsync((Stream)null!, MagickFormat.Png));
            }

            [Fact]
            public async Task ShouldThrowExceptionWhenStreamIsEmpty()
            {
                using var image = new MagickImage();

                await Assert.ThrowsAsync<ArgumentException>("stream", () => image.ReadAsync(new MemoryStream(), MagickFormat.Png));
            }

            [Fact]
            public async Task ShouldUseTheCorrectReaderWhenFormatIsSet()
            {
                var bytes = Encoding.ASCII.GetBytes("%PDF-");
                using var stream = new MemoryStream(bytes);
                using var image = new MagickImage();

                var exception = await Assert.ThrowsAsync<MagickCorruptImageErrorException>(() => image.ReadAsync(stream, MagickFormat.Png));
                Assert.Contains("ReadPNGImage", exception.Message);
            }

            [Fact]
            public async Task ShouldResetTheFormatAfterReadingStream()
            {
                using var stream = File.OpenRead(Files.CirclePNG);
                using var image = new MagickImage();
                await image.ReadAsync(stream, MagickFormat.Png);

                Assert.Equal(MagickFormat.Unknown, image.Settings.Format);
            }
        }

        public class WithStreamAndMagickReadSettings
        {
            [Fact]
            public async Task ShouldThrowExceptionWhenStreamIsNull()
            {
                var settings = new MagickReadSettings();
                using var image = new MagickImage();

                await Assert.ThrowsAsync<ArgumentNullException>("stream", () => image.ReadAsync((Stream)null!, settings));
            }

            [Fact]
            public async Task ShouldThrowExceptionWhenStreamIsEmpty()
            {
                var settings = new MagickReadSettings();
                using var image = new MagickImage();

                await Assert.ThrowsAsync<ArgumentException>("stream", () => image.ReadAsync(new MemoryStream(), settings));
            }

            [Fact]
            public async Task ShouldNotThrowExceptionWhenSettingsIsNull()
            {
                using var fileStream = File.OpenRead(Files.CirclePNG);
                using var image = new MagickImage();
                await image.ReadAsync(fileStream, null);
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
                using var image = new MagickImage();

                var exception = await Assert.ThrowsAsync<MagickCorruptImageErrorException>(() => image.ReadAsync(stream, settings));
                Assert.Contains("ReadPNGImage", exception.Message);
            }

            [Fact]
            public async Task ShouldResetTheFormatAfterReadingStream()
            {
                var settings = new MagickReadSettings
                {
                    Format = MagickFormat.Png,
                };
                using var stream = File.OpenRead(Files.CirclePNG);
                using var image = new MagickImage();
                await image.ReadAsync(stream, settings);

                Assert.Equal(MagickFormat.Unknown, image.Settings.Format);
            }
        }
    }
}
