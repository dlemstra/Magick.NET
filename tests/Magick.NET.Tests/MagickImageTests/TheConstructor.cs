// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public partial class TheConstructor
    {
        public class WithByteArray
        {
            [Fact]
            public void ShouldThrowExceptionWhenArrayIsNull()
            {
                Assert.Throws<ArgumentNullException>("data", () => new MagickImage((byte[])null!));
            }

            [Fact]
            public void ShouldThrowExceptionWhenArrayIsEmpty()
            {
                Assert.Throws<ArgumentException>("data", () => new MagickImage(Array.Empty<byte>()));
            }
        }

        public class WithByteArrayAndOffset
        {
            [Fact]
            public void ShouldThrowExceptionWhenArrayIsNull()
            {
                Assert.Throws<ArgumentNullException>("data", () => new MagickImage((byte[])null!, 0, 0));
            }

            [Fact]
            public void ShouldThrowExceptionWhenArrayIsEmpty()
            {
                Assert.Throws<ArgumentException>("data", () => new MagickImage(Array.Empty<byte>(), 0, 0));
            }

            [Fact]
            public void ShouldThrowExceptionWhenCountIsZero()
            {
                Assert.Throws<ArgumentException>("count", () => new MagickImage(new byte[] { 215 }, 0, 0));
            }
        }

        public class WithByteArrayAndOffsetAndMagickFormat
        {
            [Fact]
            public void ShouldThrowExceptionWhenArrayIsNull()
            {
                Assert.Throws<ArgumentNullException>("data", () => new MagickImage(null!, 0, 0, MagickFormat.Png));
            }

            [Fact]
            public void ShouldThrowExceptionWhenArrayIsEmpty()
            {
                Assert.Throws<ArgumentException>("data", () => new MagickImage(Array.Empty<byte>(), 0, 0, MagickFormat.Png));
            }

            [Fact]
            public void ShouldThrowExceptionWhenCountIsZero()
            {
                Assert.Throws<ArgumentException>("count", () => new MagickImage(new byte[] { 215 }, 0, 0, MagickFormat.Png));
            }
        }

        public class WithByteArrayAndOffsetAndMagickReadSettings
        {
            [Fact]
            public void ShouldThrowExceptionWhenArrayIsNull()
            {
                var settings = new MagickReadSettings();

                Assert.Throws<ArgumentNullException>("data", () => new MagickImage(null!, 0, 0, settings));
            }

            [Fact]
            public void ShouldThrowExceptionWhenArrayIsEmpty()
            {
                var settings = new MagickReadSettings();

                Assert.Throws<ArgumentException>("data", () => new MagickImage(Array.Empty<byte>(), 0, 0, settings));
            }

            [Fact]
            public void ShouldThrowExceptionWhenCountIsZero()
            {
                var settings = new MagickReadSettings();

                Assert.Throws<ArgumentException>("count", () => new MagickImage(new byte[] { 215 }, 0, 0, settings));
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenSettingsIsNull()
            {
                var bytes = File.ReadAllBytes(Files.CirclePNG);

                using var image = new MagickImage(bytes, 0, (uint)bytes.Length, null!);
            }
        }

        public class WithByteArrayAndMagickFormat
        {
            [Fact]
            public void ShouldThrowExceptionWhenArrayIsNull()
            {
                Assert.Throws<ArgumentNullException>("data", () => new MagickImage((byte[])null!, MagickFormat.Png));
            }

            [Fact]
            public void ShouldThrowExceptionWhenArrayIsEmpty()
            {
                Assert.Throws<ArgumentException>("data", () => new MagickImage(Array.Empty<byte>(), MagickFormat.Png));
            }
        }

        public class WithByteArrayAndMagickReadSettings
        {
            [Fact]
            public void ShouldThrowExceptionWhenArrayIsNull()
            {
                var settings = new MagickReadSettings();

                Assert.Throws<ArgumentNullException>("data", () => new MagickImage((byte[])null!, settings));
            }

            [Fact]
            public void ShouldThrowExceptionWhenArrayIsEmpty()
            {
                var settings = new MagickReadSettings();

                Assert.Throws<ArgumentException>("data", () => new MagickImage(Array.Empty<byte>(), settings));
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenSettingsIsNull()
            {
                var bytes = File.ReadAllBytes(Files.CirclePNG);
                using var image = new MagickImage(bytes, null!);
            }
        }

        public class WithColor
        {
            [Fact]
            public void ShouldThrowExceptionWhenColorIsNull()
            {
                Assert.Throws<ArgumentNullException>("color", () => new MagickImage((MagickColor)null!, 1, 1));
            }

            [Fact]
            public void ShouldThrowExceptionWhenWidthIsZero()
            {
                Assert.Throws<ArgumentException>("width", () => new MagickImage(MagickColors.Red, 0, 1));
            }

            [Fact]
            public void ShouldThrowExceptionWhenHeightIsZero()
            {
                Assert.Throws<ArgumentException>("height", () => new MagickImage(MagickColors.Red, 1, 0));
            }

            [Fact]
            public void ShouldReadImage()
            {
                var color = new MagickColor("red");

                using var image = new MagickImage(color, 20, 30);

                Assert.Equal(20U, image.Width);
                Assert.Equal(30U, image.Height);
                ColorAssert.Equal(color, image, 10, 10);
            }
        }

        public class WithFileInfo
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileInfoIsNull()
            {
                Assert.Throws<ArgumentNullException>("file", () => new MagickImage((FileInfo)null!));
            }
        }

        public class WithFileInfoAndMagickFormat
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileInfoIsNull()
            {
                Assert.Throws<ArgumentNullException>("file", () => new MagickImage((FileInfo)null!, MagickFormat.Png));
            }
        }

        public class WithFileInfoAndMagickReadSettings
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileInfoIsNull()
            {
                var settings = new MagickReadSettings();

                Assert.Throws<ArgumentNullException>("file", () => new MagickImage((FileInfo)null!, settings));
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenSettingsIsNull()
            {
                using var image = new MagickImage(new FileInfo(Files.CirclePNG), null!);
            }
        }

        public partial class WithFileName
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsNull()
            {
                Assert.Throws<ArgumentNullException>("fileName", () => new MagickImage((string)null!));
            }

            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsEmpty()
            {
                Assert.Throws<ArgumentException>("fileName", () => new MagickImage(string.Empty));
            }

            [Fact]
            public void ShouldUseBaseDirectoryOfCurrentAppDomainWhenFileNameStartsWithTilde()
            {
                var exception = Assert.Throws<MagickBlobErrorException>(() => new MagickImage("~/test.gif"));
                var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

                Assert.Contains("error/blob.c/OpenBlob", exception.Message);
                Assert.Contains(baseDirectory, exception.Message);
            }

            [Fact]
            public void ShouldNotUseBaseDirectoryOfCurrentAppDomainWhenFileNameIsTilde()
            {
                var exception = Assert.Throws<MagickBlobErrorException>(() => new MagickImage("~"));

                Assert.Contains("error/blob.c/OpenBlob", exception.Message);
                Assert.Contains("~", exception.Message);
            }
        }

        public class WithFileNameAndMagickFormat
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsNull()
            {
                Assert.Throws<ArgumentNullException>("fileName", () => new MagickImage((string)null!, MagickFormat.Png));
            }

            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsEmpty()
            {
                Assert.Throws<ArgumentException>("fileName", () => new MagickImage(string.Empty, MagickFormat.Png));
            }
        }

        public class WithFileNameAndMagickReadSettings
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsNull()
            {
                var settings = new MagickReadSettings();

                Assert.Throws<ArgumentNullException>("fileName", () => new MagickImage((string)null!, settings));
            }

            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsEmpty()
            {
                var settings = new MagickReadSettings();

                Assert.Throws<ArgumentException>("fileName", () => new MagickImage(string.Empty, settings));
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenSettingsIsNull()
            {
                using var image = new MagickImage(Files.CirclePNG, null!);
            }
        }

        public class WithFileNameAndSize
        {
            [Fact]
            public void ShouldThrowExceptionWhenColorIsNull()
            {
                Assert.Throws<ArgumentNullException>("fileName", () => new MagickImage((string)null!, 1, 1));
            }

            [Fact]
            public void ShouldThrowExceptionWhenWidthIsZero()
            {
                Assert.Throws<ArgumentException>("width", () => new MagickImage("xc:red", 0, 1));
            }

            [Fact]
            public void ShouldThrowExceptionWhenHeightIsZero()
            {
                Assert.Throws<ArgumentException>("height", () => new MagickImage("xc:red", 1, 0));
            }

            [Fact]
            public void ShouldReadImage()
            {
                using var image = new MagickImage("xc:red", 20, 30);

                Assert.Equal(20U, image.Width);
                Assert.Equal(30U, image.Height);
                ColorAssert.Equal(MagickColors.Red, image, 10, 10);
            }
        }

        public class WithStream
        {
            [Fact]
            public void ShouldThrowExceptionWhenStreamIsNull()
            {
                Assert.Throws<ArgumentNullException>("stream", () => new MagickImage((Stream)null!));
            }

            [Fact]
            public void ShouldThrowExceptionWhenStreamIsEmpty()
            {
                var settings = new MagickReadSettings();

                Assert.Throws<ArgumentException>("stream", () => new MagickImage(new MemoryStream()));
            }
        }

        public class WithStreamAndMagickFormat
        {
            [Fact]
            public void ShouldThrowExceptionWhenStreamIsNull()
            {
                Assert.Throws<ArgumentNullException>("stream", () => new MagickImage((Stream)null!, MagickFormat.Png));
            }

            [Fact]
            public void ShouldThrowExceptionWhenStreamIsEmpty()
            {
                Assert.Throws<ArgumentException>("stream", () => new MagickImage(new MemoryStream(), MagickFormat.Png));
            }
        }

        public class WithStreamAndMagickReadSettings
        {
            [Fact]
            public void ShouldThrowExceptionWhenStreamIsNull()
            {
                var settings = new MagickReadSettings();

                Assert.Throws<ArgumentNullException>("stream", () => new MagickImage((Stream)null!, settings));
            }

            [Fact]
            public void ShouldThrowExceptionWhenStreamIsEmpty()
            {
                var settings = new MagickReadSettings();

                Assert.Throws<ArgumentException>("stream", () => new MagickImage(new MemoryStream(), settings));
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenSettingsIsNull()
            {
                using var fileStream = File.OpenRead(Files.CirclePNG);
                using var image = new MagickImage(fileStream, null!);
            }
        }
    }
}
