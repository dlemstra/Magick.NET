// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.IO;
using ImageMagick;
using Xunit;

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace Magick.NET.Tests;

public partial class MagickImageCollectionTests
{
    public partial class TheConstructor
    {
        public class WithByteArray
        {
            [Fact]
            public void ShouldThrowExceptionWhenArrayIsNull()
            {
                Assert.Throws<ArgumentNullException>("data", () => new MagickImageCollection((byte[])null));
            }

            [Fact]
            public void ShouldThrowExceptionWhenArrayIsEmpty()
            {
                Assert.Throws<ArgumentException>("data", () => new MagickImageCollection(Array.Empty<byte>()));
            }

            [Fact]
            public void ShouldResetTheFormatAfterReading()
            {
                var settings = new MagickReadSettings
                {
                    Format = MagickFormat.Png,
                };

                var bytes = File.ReadAllBytes(Files.CirclePNG);

                using var images = new MagickImageCollection(bytes, settings);

                Assert.Equal(MagickFormat.Unknown, images[0].Settings.Format);
            }
        }

        public class WithByteArrayAndOffset
        {
            [Fact]
            public void ShouldThrowExceptionWhenArrayIsNull()
            {
                Assert.Throws<ArgumentNullException>("data", () => new MagickImageCollection(null, 0, 0));
            }

            [Fact]
            public void ShouldThrowExceptionWhenArrayIsEmpty()
            {
                Assert.Throws<ArgumentException>("data", () => new MagickImageCollection(Array.Empty<byte>(), 0, 0));
            }

            [Fact]
            public void ShouldThrowExceptionWhenCountIsZero()
            {
                Assert.Throws<ArgumentException>("count", () => new MagickImageCollection(new byte[] { 215 }, 0, 0));
            }

            [Fact]
            public void ShouldReadImage()
            {
                var fileBytes = File.ReadAllBytes(Files.SnakewarePNG);
                var bytes = new byte[fileBytes.Length + 10];
                fileBytes.CopyTo(bytes, 10);

                using var images = new MagickImageCollection(bytes, 10, (uint)bytes.Length - 10);

                Assert.Single(images);
            }
        }

        public class WithByteArrayAndOffsetAndMagickFormat
        {
            [Fact]
            public void ShouldThrowExceptionWhenArrayIsNull()
            {
                Assert.Throws<ArgumentNullException>("data", () => new MagickImageCollection(null, 0, 0, MagickFormat.Png));
            }

            [Fact]
            public void ShouldThrowExceptionWhenArrayIsEmpty()
            {
                Assert.Throws<ArgumentException>("data", () => new MagickImageCollection(Array.Empty<byte>(), 0, 0, MagickFormat.Png));
            }

            [Fact]
            public void ShouldThrowExceptionWhenCountIsZero()
            {
                Assert.Throws<ArgumentException>("count", () => new MagickImageCollection(new byte[] { 215 }, 0, 0, MagickFormat.Png));
            }

            [Fact]
            public void ShouldReadImage()
            {
                var fileBytes = File.ReadAllBytes(Files.SnakewarePNG);
                var bytes = new byte[fileBytes.Length + 10];
                fileBytes.CopyTo(bytes, 10);

                using var images = new MagickImageCollection(bytes, 10, (uint)bytes.Length - 10, MagickFormat.Png);

                Assert.Single(images);
            }
        }

        public class WithByteArrayAndOffsetAndMagickReadSettings
        {
            [Fact]
            public void ShouldThrowExceptionWhenArrayIsNull()
            {
                var settings = new MagickReadSettings();

                Assert.Throws<ArgumentNullException>("data", () => new MagickImageCollection(null, 0, 0, settings));
            }

            [Fact]
            public void ShouldThrowExceptionWhenArrayIsEmpty()
            {
                var settings = new MagickReadSettings();

                Assert.Throws<ArgumentException>("data", () => new MagickImageCollection(Array.Empty<byte>(), 0, 0, settings));
            }

            [Fact]
            public void ShouldThrowExceptionWhenCountIsZero()
            {
                var settings = new MagickReadSettings();

                Assert.Throws<ArgumentException>("count", () => new MagickImageCollection(new byte[] { 215 }, 0, 0, settings));
            }

            [Fact]
            public void ShouldReadImage()
            {
                var settings = new MagickReadSettings();

                var fileBytes = File.ReadAllBytes(Files.SnakewarePNG);
                var bytes = new byte[fileBytes.Length + 10];
                fileBytes.CopyTo(bytes, 10);

                using var images = new MagickImageCollection(bytes, 10, (uint)bytes.Length - 10, settings);

                Assert.Single(images);
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenSettingsIsNull()
            {
                var bytes = File.ReadAllBytes(Files.CirclePNG);

                using var image = new MagickImageCollection(bytes, 0, (uint)bytes.Length, null);
            }
        }

        public class WithByteArrayAndMagickReadSettings
        {
            [Fact]
            public void ShouldNotThrowExceptionWhenSettingsIsNull()
            {
                var bytes = File.ReadAllBytes(Files.SnakewarePNG);

                using var images = new MagickImageCollection(bytes, null);

                Assert.Single(images);
            }
        }

        public class WithFileInfo
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileInfoIsNull()
            {
                Assert.Throws<ArgumentNullException>("file", () => new MagickImageCollection((FileInfo)null));
            }
        }

        public class WithFileInfoAndMagickFormat
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileInfoIsNull()
            {
                Assert.Throws<ArgumentNullException>("file", () => new MagickImageCollection((FileInfo)null, MagickFormat.Png));
            }
        }

        public class WithFileInfoAndMagickReadSettings
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileInfoIsNull()
            {
                var settings = new MagickReadSettings();

                Assert.Throws<ArgumentNullException>("file", () => new MagickImageCollection((FileInfo)null, settings));
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenSettingsIsNull()
            {
                var file = new FileInfo(Files.SnakewarePNG);

                using var images = new MagickImageCollection(file, null);

                Assert.Single(images);
            }
        }

        public class WithFileName
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsNull()
            {
                Assert.Throws<ArgumentNullException>("fileName", () => new MagickImageCollection((string)null));
            }

            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsEmpty()
            {
                Assert.Throws<ArgumentException>("fileName", () => new MagickImageCollection(string.Empty));
            }

            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsInvalid()
            {
                var exception = Assert.Throws<MagickBlobErrorException>(() => new MagickImageCollection(Files.Missing));

                Assert.Contains("error/blob.c/OpenBlob", exception.Message);
            }

            [Fact]
            public void ShouldResetTheFormatAfterReading()
            {
                var settings = new MagickReadSettings
                {
                    Format = MagickFormat.Png,
                };

                using var images = new MagickImageCollection(Files.CirclePNG, settings);

                Assert.Equal(MagickFormat.Unknown, images[0].Settings.Format);
            }
        }

        public class WithFileNameAndMagickFormat
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsNull()
            {
                Assert.Throws<ArgumentNullException>("fileName", () => new MagickImageCollection((string)null, MagickFormat.Png));
            }

            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsEmpty()
            {
                Assert.Throws<ArgumentException>("fileName", () => new MagickImageCollection(string.Empty, MagickFormat.Png));
            }
        }

        public class WithFileNameAndMagickReadSettings
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsNull()
            {
                var settings = new MagickReadSettings();

                Assert.Throws<ArgumentNullException>("fileName", () => new MagickImageCollection((string)null, settings));
            }

            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsEmpty()
            {
                var settings = new MagickReadSettings();

                Assert.Throws<ArgumentException>("fileName", () => new MagickImageCollection(string.Empty, settings));
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenFileNameSettingsIsNull()
            {
                using var images = new MagickImageCollection(Files.SnakewarePNG, null);

                Assert.Single(images);
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

                using var images = new MagickImageCollection(Files.ImageMagickTXT, settings);

                Assert.Equal(2, images.Count);
                ColorAssert.Equal(MagickColors.Gold, images[0], 348, 648);
            }
        }

        public class WithImages
        {
            [Fact]
            public void ShouldThrowExceptionWhenImagesIsNull()
            {
                Assert.Throws<ArgumentNullException>("images", () => new MagickImageCollection((IEnumerable<IMagickImage<QuantumType>>)null));
            }

            [Fact]
            public void ShouldThrowExceptionWhenImagesIsMagickImageCollection()
            {
                using var images = new MagickImageCollection(Files.SnakewarePNG);

                Assert.Throws<ArgumentException>("images", () => new MagickImageCollection(images));
            }

            [Fact]
            public void ShouldThrowExceptionWhenImagesContainsDuplicates()
            {
                using var image = new MagickImage();

                Assert.Throws<InvalidOperationException>(() => new MagickImageCollection(new[] { image, image }));
            }

            [Fact]
            public void ShouldNotCloneTheInputImages()
            {
                var image = new MagickImage("xc:red", 100, 100);

                var list = new List<IMagickImage<QuantumType>> { image };

                using var images = new MagickImageCollection(list);

                Assert.True(ReferenceEquals(image, list[0]));
            }
        }

        public class WithStream
        {
            [Fact]
            public void ShouldThrowExceptionWhenStreamIsNull()
            {
                Assert.Throws<ArgumentNullException>("stream", () => new MagickImageCollection((Stream)null));
            }

            [Fact]
            public void ShouldResetTheFormatAfterReading()
            {
                var settings = new MagickReadSettings
                {
                    Format = MagickFormat.Png,
                };

                using var stream = File.OpenRead(Files.CirclePNG);
                using var images = new MagickImageCollection(stream, settings);

                Assert.Equal(MagickFormat.Unknown, images[0].Settings.Format);
            }
        }

        public class WithStreamAndMagickFormat
        {
            [Fact]
            public void ShouldThrowExceptionWhenStreamIsNull()
            {
                Assert.Throws<ArgumentNullException>("stream", () => new MagickImageCollection((Stream)null, MagickFormat.Png));
            }

            [Fact]
            public void ShouldThrowExceptionWhenStreamIsEmpty()
            {
                Assert.Throws<ArgumentException>("stream", () => new MagickImageCollection(new MemoryStream(), MagickFormat.Png));
            }
        }

        public class WithStreamAndMagickReadSettings
        {
            [Fact]
            public void ShouldThrowExceptionWhenStreamIsNull()
            {
                var settings = new MagickReadSettings();

                Assert.Throws<ArgumentNullException>("stream", () => new MagickImageCollection((Stream)null, settings));
            }

            [Fact]
            public void ShouldThrowExceptionWhenStreamIsEmpty()
            {
                var settings = new MagickReadSettings();

                Assert.Throws<ArgumentException>("stream", () => new MagickImageCollection(new MemoryStream(), settings));
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenStreamSettingsIsNull()
            {
                using var stream = File.OpenRead(Files.SnakewarePNG);
                using var images = new MagickImageCollection(stream, null);

                Assert.Single(images);
            }
        }
    }
}
