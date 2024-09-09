// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageCollectionTests
{
    public partial class ThePingMethod
    {
        public class WithByteArray
        {
            [Fact]
            public void ShouldThrowExceptionWhenArrayIsNull()
            {
                using var images = new MagickImageCollection();

                Assert.Throws<ArgumentNullException>("data", () => images.Ping((byte[])null!));
            }

            [Fact]
            public void ShouldThrowExceptionWhenArrayIsEmpty()
            {
                using var images = new MagickImageCollection();

                Assert.Throws<ArgumentException>("data", () => images.Ping(Array.Empty<byte>()));
            }

            [Fact]
            public void ShouldResetTheFormatAfterReading()
            {
                var settings = new MagickReadSettings
                {
                    Format = MagickFormat.Png,
                };

                var bytes = File.ReadAllBytes(Files.CirclePNG);

                using var images = new MagickImageCollection();
                images.Ping(bytes, settings);

                Assert.Equal(MagickFormat.Unknown, images[0].Settings.Format);
                Assert.Throws<InvalidOperationException>(() => images[0].GetPixelsUnsafe());
            }
        }

        public class WithByteArrayAndOffset
        {
            [Fact]
            public void ShouldThrowExceptionWhenArrayIsNull()
            {
                using var images = new MagickImageCollection();

                Assert.Throws<ArgumentNullException>("data", () => images.Ping(null!, 0, 0));
            }

            [Fact]
            public void ShouldThrowExceptionWhenArrayIsEmpty()
            {
                using var images = new MagickImageCollection();

                Assert.Throws<ArgumentException>("data", () => images.Ping(Array.Empty<byte>(), 0, 0));
            }

            [Fact]
            public void ShouldThrowExceptionWhenCountIsZero()
            {
                using var images = new MagickImageCollection();

                Assert.Throws<ArgumentException>("count", () => images.Ping(new byte[] { 215 }, 0, 0));
            }

            [Fact]
            public void ShouldPingImage()
            {
                using var images = new MagickImageCollection();
                var fileBytes = File.ReadAllBytes(Files.SnakewarePNG);
                var bytes = new byte[fileBytes.Length + 10];
                fileBytes.CopyTo(bytes, 10);

                images.Ping(bytes, 10, (uint)bytes.Length - 10);
                Assert.Single(images);
                Assert.Throws<InvalidOperationException>(() => images[0].GetPixelsUnsafe());
            }
        }

        public class WithByteArrayAndOffsetAndMagickReadSettings
        {
            [Fact]
            public void ShouldThrowExceptionWhenArrayIsNull()
            {
                var settings = new MagickReadSettings();
                using var images = new MagickImageCollection();

                Assert.Throws<ArgumentNullException>("data", () => images.Ping(null!, 0, 0, settings));
            }

            [Fact]
            public void ShouldThrowExceptionWhenArrayIsEmpty()
            {
                var settings = new MagickReadSettings();
                using var images = new MagickImageCollection();

                Assert.Throws<ArgumentException>("data", () => images.Ping(Array.Empty<byte>(), 0, 0, settings));
            }

            [Fact]
            public void ShouldThrowExceptionWhenCountIsZero()
            {
                var settings = new MagickReadSettings();
                using var images = new MagickImageCollection();

                Assert.Throws<ArgumentException>("count", () => images.Ping(new byte[] { 215 }, 0, 0, settings));
            }

            [Fact]
            public void ShouldPingImage()
            {
                var settings = new MagickReadSettings();

                var fileBytes = File.ReadAllBytes(Files.SnakewarePNG);
                var bytes = new byte[fileBytes.Length + 10];
                fileBytes.CopyTo(bytes, 10);

                using var images = new MagickImageCollection();
                images.Ping(bytes, 10, (uint)bytes.Length - 10, settings);

                Assert.Single(images);
                Assert.Throws<InvalidOperationException>(() => images[0].GetPixelsUnsafe());
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenSettingsIsNull()
            {
                var bytes = File.ReadAllBytes(Files.CirclePNG);

                using var images = new MagickImageCollection();
                images.Ping(bytes, 0, (uint)bytes.Length, null);

                Assert.Throws<InvalidOperationException>(() => images[0].GetPixelsUnsafe());
            }
        }

        public class WithByteArrayAndMagickReadSettings
        {
            [Fact]
            public void ShouldNotThrowExceptionWhenSettingsIsNull()
            {
                var bytes = File.ReadAllBytes(Files.SnakewarePNG);

                using var images = new MagickImageCollection();
                images.Ping(bytes, null);

                Assert.Single(images);
                Assert.Throws<InvalidOperationException>(() => images[0].GetPixelsUnsafe());
            }
        }

        public class WithFileInfo
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileInfoIsNull()
            {
                using var images = new MagickImageCollection();

                Assert.Throws<ArgumentNullException>("file", () => images.Ping((FileInfo)null!));
            }
        }

        public class WithFileInfoAndMagickReadSettings
        {
            [Fact]
            public void ShouldNotThrowExceptionWhenSettingsIsNull()
            {
                var file = new FileInfo(Files.SnakewarePNG);

                using var images = new MagickImageCollection();
                images.Ping(file, null);

                Assert.Single(images);
                Assert.Throws<InvalidOperationException>(() => images[0].GetPixelsUnsafe());
            }
        }

        public class WithFileName
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsNull()
            {
                using var images = new MagickImageCollection();

                Assert.Throws<ArgumentNullException>("fileName", () => images.Ping((string)null!));
            }

            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsEmpty()
            {
                using var images = new MagickImageCollection();

                Assert.Throws<ArgumentException>("fileName", () => images.Ping(string.Empty));
            }

            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsInvalid()
            {
                using var images = new MagickImageCollection();

                var exception = Assert.Throws<MagickBlobErrorException>(() => images.Ping(Files.Missing));
                Assert.Contains("error/blob.c/OpenBlob", exception.Message);
            }

            [Fact]
            public void ShouldResetTheFormatAfterReading()
            {
                var settings = new MagickReadSettings
                {
                    Format = MagickFormat.Png,
                };

                using var images = new MagickImageCollection();
                images.Ping(Files.CirclePNG, settings);

                Assert.Equal(MagickFormat.Unknown, images[0].Settings.Format);
                Assert.Throws<InvalidOperationException>(() => images[0].GetPixelsUnsafe());
            }
        }

        public class WithFileNameAndMagickReadSettings
        {
            [Fact]
            public void ShouldNotThrowExceptionWhenFileNameSettingsIsNull()
            {
                using var images = new MagickImageCollection();
                images.Ping(Files.SnakewarePNG, null);

                Assert.Single(images);
                Assert.Throws<InvalidOperationException>(() => images[0].GetPixelsUnsafe());
            }

            [Fact]
            public void ShouldUseTheReadSettings()
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
                images.Ping(Files.ImageMagickTXT, settings);

                Assert.Equal(2, images.Count);
            }
        }

        public class WithStream
        {
            [Fact]
            public void ShouldThrowExceptionWhenStreamIsNull()
            {
                using var images = new MagickImageCollection();

                Assert.Throws<ArgumentNullException>("stream", () => images.Ping((Stream)null!));
            }

            [Fact]
            public void ShouldResetTheFormatAfterReading()
            {
                var settings = new MagickReadSettings
                {
                    Format = MagickFormat.Png,
                };

                using var stream = File.OpenRead(Files.CirclePNG);
                using var images = new MagickImageCollection();
                images.Ping(stream, settings);

                Assert.Equal(MagickFormat.Unknown, images[0].Settings.Format);
                Assert.Throws<InvalidOperationException>(() => images[0].GetPixelsUnsafe());
            }
        }

        public class WithStreamAndMagickReadSettings
        {
            [Fact]
            public void ShouldNotThrowExceptionWhenStreamSettingsIsNull()
            {
                using var stream = File.OpenRead(Files.SnakewarePNG);
                using var images = new MagickImageCollection();
                images.Ping(stream, null);

                Assert.Single(images);
                Assert.Throws<InvalidOperationException>(() => images[0].GetPixelsUnsafe());
            }
        }
    }
}
