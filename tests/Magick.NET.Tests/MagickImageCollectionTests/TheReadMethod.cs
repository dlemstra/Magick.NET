// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using System.Text;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageCollectionTests
{
    public partial class TheReadMethod
    {
        public class WithByteArray
        {
            [Fact]
            public void ShouldThrowExceptionWhenArrayIsNull()
            {
                using var images = new MagickImageCollection();

                Assert.Throws<ArgumentNullException>("data", () => images.Read((byte[])null));
            }

            [Fact]
            public void ShouldThrowExceptionWhenArrayIsEmpty()
            {
                using var images = new MagickImageCollection();

                Assert.Throws<ArgumentException>("data", () => images.Read(Array.Empty<byte>()));
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
                images.Read(bytes, settings);

                Assert.Equal(MagickFormat.Unknown, images[0].Settings.Format);
            }
        }

        public class WithByteArrayAndOffset
        {
            [Fact]
            public void ShouldThrowExceptionWhenArrayIsNull()
            {
                using var images = new MagickImageCollection();

                Assert.Throws<ArgumentNullException>("data", () => images.Read(null, 0, 0));
            }

            [Fact]
            public void ShouldThrowExceptionWhenArrayIsEmpty()
            {
                using var images = new MagickImageCollection();

                Assert.Throws<ArgumentException>("data", () => images.Read(Array.Empty<byte>(), 0, 0));
            }

            [Fact]
            public void ShouldThrowExceptionWhenCountIsZero()
            {
                using var images = new MagickImageCollection();

                Assert.Throws<ArgumentException>("count", () => images.Read(new byte[] { 215 }, 0, 0));
            }

            [Fact]
            public void ShouldReadImage()
            {
                using var images = new MagickImageCollection();
                var fileBytes = File.ReadAllBytes(Files.SnakewarePNG);
                var bytes = new byte[fileBytes.Length + 10];
                fileBytes.CopyTo(bytes, 10);

                images.Read(bytes, 10, (uint)bytes.Length - 10);
                Assert.Single(images);
            }
        }

        public class WithByteArrayAndOffsetAndMagickFormat
        {
            [Fact]
            public void ShouldThrowExceptionWhenArrayIsNull()
            {
                using var images = new MagickImageCollection();

                Assert.Throws<ArgumentNullException>("data", () => images.Read(null, 0, 0, MagickFormat.Png));
            }

            [Fact]
            public void ShouldThrowExceptionWhenArrayIsEmpty()
            {
                using var images = new MagickImageCollection();

                Assert.Throws<ArgumentException>("data", () => images.Read(Array.Empty<byte>(), 0, 0, MagickFormat.Png));
            }

            [Fact]
            public void ShouldThrowExceptionWhenCountIsZero()
            {
                using var images = new MagickImageCollection();

                Assert.Throws<ArgumentException>("count", () => images.Read(new byte[] { 215 }, 0, 0, MagickFormat.Png));
            }

            [Fact]
            public void ShouldReadImage()
            {
                var fileBytes = File.ReadAllBytes(Files.SnakewarePNG);
                var bytes = new byte[fileBytes.Length + 10];
                fileBytes.CopyTo(bytes, 10);

                using var images = new MagickImageCollection();
                images.Read(bytes, 10, (uint)bytes.Length - 10, MagickFormat.Png);

                Assert.Single(images);
            }
        }

        public class WithByteArrayAndOffsetAndMagickReadSettings
        {
            [Fact]
            public void ShouldThrowExceptionWhenArrayIsNull()
            {
                var settings = new MagickReadSettings();
                using var images = new MagickImageCollection();

                Assert.Throws<ArgumentNullException>("data", () => images.Read(null, 0, 0, settings));
            }

            [Fact]
            public void ShouldThrowExceptionWhenArrayIsEmpty()
            {
                var settings = new MagickReadSettings();
                using var images = new MagickImageCollection();

                Assert.Throws<ArgumentException>("data", () => images.Read(Array.Empty<byte>(), 0, 0, settings));
            }

            [Fact]
            public void ShouldThrowExceptionWhenCountIsZero()
            {
                var settings = new MagickReadSettings();
                using var images = new MagickImageCollection();

                Assert.Throws<ArgumentException>("count", () => images.Read(new byte[] { 215 }, 0, 0, settings));
            }

            [Fact]
            public void ShouldReadImage()
            {
                var settings = new MagickReadSettings();

                var fileBytes = File.ReadAllBytes(Files.SnakewarePNG);
                var bytes = new byte[fileBytes.Length + 10];
                fileBytes.CopyTo(bytes, 10);

                using var images = new MagickImageCollection();
                images.Read(bytes, 10, (uint)bytes.Length - 10, settings);

                Assert.Single(images);
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenSettingsIsNull()
            {
                var bytes = File.ReadAllBytes(Files.CirclePNG);
                using var image = new MagickImageCollection();
                image.Read(bytes, 0, (uint)bytes.Length, null);
            }
        }

        public class WithByteArrayAndMagickFormat
        {
            [Fact]
            public void ShouldThrowExceptionWhenArrayIsNull()
            {
                using var images = new MagickImageCollection();

                Assert.Throws<ArgumentNullException>("data", () => images.Read((byte[])null, MagickFormat.Png));
            }

            [Fact]
            public void ShouldThrowExceptionWhenArrayIsEmpty()
            {
                using var images = new MagickImageCollection();

                Assert.Throws<ArgumentException>("data", () => images.Read(Array.Empty<byte>(), MagickFormat.Png));
            }

            [Fact]
            public void ShouldUseTheCorrectReaderWhenFormatIsSet()
            {
                var bytes = Encoding.ASCII.GetBytes("%PDF-");
                using var images = new MagickImageCollection();

                var exception = Assert.Throws<MagickCorruptImageErrorException>(() => images.Read(bytes, MagickFormat.Png));
                Assert.Contains("ReadPNGImage", exception.Message);
            }
        }

        public class WithByteArrayAndMagickReadSettings
        {
            [Fact]
            public void ShouldThrowExceptionWhenArrayIsNull()
            {
                var settings = new MagickReadSettings();
                using var images = new MagickImageCollection();

                Assert.Throws<ArgumentNullException>("data", () => images.Read((byte[])null, settings));
            }

            [Fact]
            public void ShouldThrowExceptionWhenArrayIsEmpty()
            {
                var settings = new MagickReadSettings();
                using var images = new MagickImageCollection();

                Assert.Throws<ArgumentException>("data", () => images.Read(Array.Empty<byte>(), settings));
            }

            [Fact]
            public void ShouldUseTheCorrectReaderWhenFormatIsSet()
            {
                var bytes = Encoding.ASCII.GetBytes("%PDF-");
                var settings = new MagickReadSettings
                {
                    Format = MagickFormat.Png,
                };
                using var images = new MagickImageCollection();

                var exception = Assert.Throws<MagickCorruptImageErrorException>(() => images.Read(bytes, settings));
                Assert.Contains("ReadPNGImage", exception.Message);
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenSettingsIsNull()
            {
                var bytes = File.ReadAllBytes(Files.SnakewarePNG);
                using var images = new MagickImageCollection();
                images.Read(bytes, null);

                Assert.Single(images);
            }
        }

        public class WithFileInfo
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileInfoIsNull()
            {
                using var images = new MagickImageCollection();

                Assert.Throws<ArgumentNullException>("file", () => images.Read((FileInfo)null));
            }
        }

        public class WithFileInfoAndMagickFormat
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileInfoIsNull()
            {
                using var images = new MagickImageCollection();

                Assert.Throws<ArgumentNullException>("file", () => images.Read((FileInfo)null, MagickFormat.Png));
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenSettingsIsNull()
            {
                var file = new FileInfo(Files.SnakewarePNG);
                using var images = new MagickImageCollection();
                images.Read(file, null);

                Assert.Single(images);
            }
        }

        public class WithFileInfoAndMagickReadSettings
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileInfoIsNull()
            {
                var settings = new MagickReadSettings();
                using var images = new MagickImageCollection();

                Assert.Throws<ArgumentNullException>("file", () => images.Read((FileInfo)null, settings));
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenSettingsIsNull()
            {
                var file = new FileInfo(Files.SnakewarePNG);
                using var images = new MagickImageCollection();
                images.Read(file, null);

                Assert.Single(images);
            }
        }

        public class WithFileName
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsNull()
            {
                using var images = new MagickImageCollection();

                Assert.Throws<ArgumentNullException>("fileName", () => images.Read((string)null));
            }

            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsEmpty()
            {
                using var images = new MagickImageCollection();

                Assert.Throws<ArgumentException>("fileName", () => images.Read(string.Empty));
            }

            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsInvalid()
            {
                using var images = new MagickImageCollection();

                var exception = Assert.Throws<MagickBlobErrorException>(() => images.Read(Files.Missing));
                Assert.Contains("error/blob.c/OpenBlob", exception.Message);
            }

            [Fact]
            public void ShouldResetTheFormatAfterReading()
            {
                var settings = new MagickReadSettings
                {
                    Format = MagickFormat.Png,
                };
                using var input = new MagickImageCollection();
                input.Read(Files.CirclePNG, settings);

                Assert.Equal(MagickFormat.Unknown, input[0].Settings.Format);
            }
        }

        public class WithFileNameAndMagickFormat
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsNull()
            {
                using var images = new MagickImageCollection();

                Assert.Throws<ArgumentNullException>("fileName", () => images.Read((string)null, MagickFormat.Png));
            }

            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsEmpty()
            {
                using var images = new MagickImageCollection();

                Assert.Throws<ArgumentException>("fileName", () => images.Read(string.Empty, MagickFormat.Png));
            }
        }

        public class WithFileNameAndMagickReadSettings
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsNull()
            {
                var settings = new MagickReadSettings();
                using var images = new MagickImageCollection();

                Assert.Throws<ArgumentNullException>("fileName", () => images.Read((string)null, settings));
            }

            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsEmpty()
            {
                var settings = new MagickReadSettings();

                using var images = new MagickImageCollection();

                Assert.Throws<ArgumentException>("fileName", () => images.Read(string.Empty, settings));
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenSettingsIsNull()
            {
                using var images = new MagickImageCollection();
                images.Read(Files.CirclePNG, null);

                Assert.Single(images);
            }

            [Fact]
            public void ShouldUseTheSpecifiedFrameIndex()
            {
                using var image = new MagickImage();
                image.Read($"{Files.RoseSparkleGIF}[1]");

                using var images = new MagickImageCollection(Files.RoseSparkleGIF);

                Assert.Equal(0.0, image.Compare(images[1], ErrorMetric.RootMeanSquared));
            }
        }

        public class WithStream
        {
            [Fact]
            public void ShouldThrowExceptionWhenStreamIsNull()
            {
                using var images = new MagickImageCollection();

                Assert.Throws<ArgumentNullException>("stream", () => images.Read((Stream)null));
            }

            [Fact]
            public void ShouldResetTheFormatAfterReading()
            {
                var settings = new MagickReadSettings
                {
                    Format = MagickFormat.Png,
                };

                using var stream = File.OpenRead(Files.CirclePNG);
                using var input = new MagickImageCollection();
                input.Read(stream, settings);

                Assert.Equal(MagickFormat.Unknown, input[0].Settings.Format);
            }
        }

        public class WithStreamAndMagickFormat
        {
            [Fact]
            public void ShouldThrowExceptionWhenStreamIsNull()
            {
                using var images = new MagickImageCollection();

                Assert.Throws<ArgumentNullException>("stream", () => images.Read((Stream)null, MagickFormat.Png));
            }

            [Fact]
            public void ShouldThrowExceptionWhenStreamIsEmpty()
            {
                using var images = new MagickImageCollection();

                Assert.Throws<ArgumentException>("stream", () => images.Read(new MemoryStream(), MagickFormat.Png));
            }

            [Fact]
            public void ShouldUseTheCorrectReaderWhenFormatIsSet()
            {
                var bytes = Encoding.ASCII.GetBytes("%PDF-");

                using var stream = new MemoryStream(bytes);
                using var images = new MagickImageCollection();

                var exception = Assert.Throws<MagickCorruptImageErrorException>(() => images.Read(stream, MagickFormat.Png));
                Assert.Contains("ReadPNGImage", exception.Message);
            }
        }

        public class WithStreamAndMagickReadSettings
        {
            [Fact]
            public void ShouldThrowExceptionWhenStreamIsNull()
            {
                var settings = new MagickReadSettings();
                using var images = new MagickImageCollection();

                Assert.Throws<ArgumentNullException>("stream", () => images.Read((Stream)null, settings));
            }

            [Fact]
            public void ShouldThrowExceptionWhenStreamIsEmpty()
            {
                var settings = new MagickReadSettings();
                using var images = new MagickImageCollection();

                Assert.Throws<ArgumentException>("stream", () => images.Read(new MemoryStream(), settings));
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenSettingsIsNull()
            {
                using var fileStream = File.OpenRead(Files.CirclePNG);
                using var images = new MagickImageCollection();
                images.Read(fileStream, null);

                Assert.Single(images);
            }

            [Fact]
            public void ShouldUseTheCorrectReaderWhenFormatIsSet()
            {
                var bytes = Encoding.ASCII.GetBytes("%PDF-");
                var settings = new MagickReadSettings
                {
                    Format = MagickFormat.Png,
                };

                using var stream = new MemoryStream(bytes);
                using var images = new MagickImageCollection();

                var exception = Assert.Throws<MagickCorruptImageErrorException>(() => images.Read(stream, settings));
                Assert.Contains("ReadPNGImage", exception.Message);
            }
        }
    }
}
