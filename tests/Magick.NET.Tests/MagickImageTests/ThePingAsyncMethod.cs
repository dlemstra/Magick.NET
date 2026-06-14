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
    public partial class ThePingAsyncMethod
    {
        public class WithFileInfo
        {
            [Fact]
            public async Task ShouldThrowExceptionWhenFileInfoIsNull()
            {
                using var image = new MagickImage();

                await Assert.ThrowsAsync<ArgumentNullException>("file", () => image.PingAsync((FileInfo)null!, TestContext.Current.CancellationToken));
            }
        }

        public class WithFileInfoAndMagickReadSettings
        {
            [Fact]
            public async Task ShouldThrowExceptionWhenFileInfoIsNull()
            {
                var settings = new MagickReadSettings();
                using var image = new MagickImage();

                await Assert.ThrowsAsync<ArgumentNullException>("file", () => image.PingAsync((FileInfo)null!, settings, TestContext.Current.CancellationToken));
            }

            [Fact]
            public async Task ShouldNotThrowExceptionWhenSettingsIsNull()
            {
                using var image = new MagickImage();
                await image.PingAsync(new FileInfo(Files.CirclePNG), null, TestContext.Current.CancellationToken);

                Assert.Throws<InvalidOperationException>(() => image.GetPixelsUnsafe());
            }
        }

        public class WithFileName
        {
            [Fact]
            public async Task ShouldThrowExceptionWhenFileNameIsNull()
            {
                using var image = new MagickImage();

                await Assert.ThrowsAsync<ArgumentNullException>("fileName", () => image.PingAsync((string)null!, TestContext.Current.CancellationToken));
            }

            [Fact]
            public async Task ShouldThrowExceptionWhenFileNameIsEmpty()
            {
                using var image = new MagickImage();

                await Assert.ThrowsAsync<ArgumentException>("fileName", () => image.PingAsync(string.Empty, TestContext.Current.CancellationToken));
            }

            [Fact]
            public async Task ShouldThrowExceptionWhenFileIsMissing()
            {
                using var image = new MagickImage();

                await Assert.ThrowsAsync<DirectoryNotFoundException>(() => image.PingAsync(Files.Missing, TestContext.Current.CancellationToken));
            }

            [Fact]
            public async Task ShouldPingImage()
            {
                using var image = new MagickImage();
                await image.PingAsync(Files.SnakewarePNG, TestContext.Current.CancellationToken);

                Assert.Equal(286U, image.Width);
                Assert.Equal(67U, image.Height);
                Assert.Equal(MagickFormat.Png, image.Format);
                Assert.Throws<InvalidOperationException>(() => image.GetPixelsUnsafe());
            }

            [Fact]
            public async Task ShouldPingImageWithNonAsciiFileName()
            {
                using var image = new MagickImage();
                await image.PingAsync(Files.RoseSparkleGIF, TestContext.Current.CancellationToken);

                Assert.Equal(70U, image.Width);
                Assert.Equal(46U, image.Height);
                Assert.Equal(MagickFormat.Gif, image.Format);
                Assert.Throws<InvalidOperationException>(() => image.GetPixelsUnsafe());
            }

            [Fact]
            public async Task ShouldReadTheImageProfile()
            {
                using var image = new MagickImage();
                await image.PingAsync(Files.EightBimTIF, TestContext.Current.CancellationToken);

                Assert.NotNull(image.Get8BimProfile());
                Assert.Throws<InvalidOperationException>(() => image.GetPixelsUnsafe());
            }
        }

        public class WithFileNameAndMagickReadSettings
        {
            [Fact]
            public async Task ShouldThrowExceptionWhenFileNameIsNull()
            {
                var settings = new MagickReadSettings();
                using var image = new MagickImage();

                await Assert.ThrowsAsync<ArgumentNullException>("fileName", () => image.PingAsync((string)null!, settings, TestContext.Current.CancellationToken));
            }

            [Fact]
            public async Task ShouldThrowExceptionWhenFileNameIsEmpty()
            {
                var settings = new MagickReadSettings();
                using var image = new MagickImage();

                await Assert.ThrowsAsync<ArgumentException>("fileName", () => image.PingAsync(string.Empty, settings, TestContext.Current.CancellationToken));
            }

            [Fact]
            public async Task ShouldNotThrowExceptionWhenSettingsIsNull()
            {
                using var image = new MagickImage();
                await image.PingAsync(Files.CirclePNG, null, TestContext.Current.CancellationToken);

                Assert.Throws<InvalidOperationException>(() => image.GetPixelsUnsafe());
            }

            [Fact]
            public async Task ShouldResetTheFormatAfterReadingFile()
            {
                var settings = new MagickReadSettings
                {
                    Format = MagickFormat.Png,
                };

                using var image = new MagickImage();
                await image.PingAsync(Files.CirclePNG, settings, TestContext.Current.CancellationToken);

                Assert.Equal(MagickFormat.Unknown, image.Settings.Format);
                Assert.Throws<InvalidOperationException>(() => image.GetPixelsUnsafe());
            }
        }

        public class WithStream
        {
            [Fact]
            public async Task ShouldThrowExceptionWhenStreamIsNull()
            {
                using var image = new MagickImage();

                await Assert.ThrowsAsync<ArgumentNullException>("stream", () => image.PingAsync((Stream)null!, TestContext.Current.CancellationToken));
            }

            [Fact]
            public async Task ShouldThrowExceptionWhenStreamIsEmpty()
            {
                using var image = new MagickImage();

                await Assert.ThrowsAsync<ArgumentException>("stream", () => image.PingAsync(new MemoryStream(), TestContext.Current.CancellationToken));
            }

            [Fact]
            public async Task ShouldThrowExceptionWhenStreamIsNotReadable()
            {
                using var testStream = TestStream.ThatCannotRead();
                using var image = new MagickImage();

                await Assert.ThrowsAsync<ArgumentException>("stream", () => image.PingAsync(testStream, TestContext.Current.CancellationToken));
            }

            [Fact]
            public async Task ShouldPingImage()
            {
                using var image = new MagickImage();
                using var fileStream = File.OpenRead(Files.SnakewarePNG);
                await image.PingAsync(fileStream, TestContext.Current.CancellationToken);

                Assert.Equal(286U, image.Width);
                Assert.Equal(67U, image.Height);
                Assert.Equal(MagickFormat.Png, image.Format);
                Assert.Throws<InvalidOperationException>(() => image.GetPixelsUnsafe());
            }

            [Fact]
            public async Task ShouldPingImageFromSeekablePartialStream()
            {
                using var image = new MagickImage();
                using var fileStream = File.OpenRead(Files.ImageMagickJPG);
                await image.PingAsync(fileStream, TestContext.Current.CancellationToken);

                fileStream.Position = 0;
                using var partialStream = new PartialStream(fileStream, true);
                using var testImage = new MagickImage();
                await testImage.PingAsync(partialStream, TestContext.Current.CancellationToken);

                Assert.Equal(image.Width, testImage.Width);
                Assert.Equal(image.Height, testImage.Height);
                Assert.Equal(image.Format, testImage.Format);
                Assert.Equal(0.0, image.Compare(testImage, ErrorMetric.RootMeanSquared));
                Assert.Throws<InvalidOperationException>(() => image.GetPixelsUnsafe());
            }

            [Fact]
            public async Task ShouldPingImageFromNonSeekablePartialStream()
            {
                using var image = new MagickImage();
                using var fileStream = File.OpenRead(Files.ImageMagickJPG);
                await image.PingAsync(fileStream, TestContext.Current.CancellationToken);

                fileStream.Position = 0;
                using var partialStream = new PartialStream(fileStream, false);
                using var testImage = new MagickImage();
                await testImage.PingAsync(partialStream, TestContext.Current.CancellationToken);

                Assert.Equal(image.Width, testImage.Width);
                Assert.Equal(image.Height, testImage.Height);
                Assert.Equal(image.Format, testImage.Format);
                Assert.Equal(0.0, image.Compare(testImage, ErrorMetric.RootMeanSquared));
                Assert.Throws<InvalidOperationException>(() => image.GetPixelsUnsafe());
            }
        }

        public class WithStreamAndMagickReadSettings
        {
            [Fact]
            public async Task ShouldThrowExceptionWhenStreamIsNull()
            {
                var settings = new MagickReadSettings();
                using var image = new MagickImage();

                await Assert.ThrowsAsync<ArgumentNullException>("stream", () => image.PingAsync((Stream)null!, settings, TestContext.Current.CancellationToken));

            }

            [Fact]
            public async Task ShouldThrowExceptionWhenStreamIsEmpty()
            {
                var settings = new MagickReadSettings();
                using var image = new MagickImage();

                await Assert.ThrowsAsync<ArgumentException>("stream", () => image.PingAsync(new MemoryStream(), settings, TestContext.Current.CancellationToken));
            }

            [Fact]
            public async Task ShouldNotThrowExceptionWhenSettingsIsNull()
            {
                using var fileStream = File.OpenRead(Files.CirclePNG);
                using var image = new MagickImage();
                await image.PingAsync(fileStream, null, TestContext.Current.CancellationToken);

                Assert.Throws<InvalidOperationException>(() => image.GetPixelsUnsafe());
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

                var exception = await Assert.ThrowsAsync<MagickCorruptImageErrorException>(() => image.PingAsync(stream, settings, TestContext.Current.CancellationToken));
                ExceptionAssert.Contains("ReadPNGImage", exception);
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
                await image.PingAsync(stream, settings, TestContext.Current.CancellationToken);

                Assert.Equal(MagickFormat.Unknown, image.Settings.Format);
                Assert.Throws<InvalidOperationException>(() => image.GetPixelsUnsafe());
            }
        }
    }
}
