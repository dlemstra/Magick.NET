// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageInfoTests
{
    public partial class TheReadMethod
    {
        public class WithByteArray
        {
            [Fact]
            public void ShouldThrowExceptionWhenDataIsNull()
            {
                var imageInfo = new MagickImageInfo();

                Assert.Throws<ArgumentNullException>("data", () => imageInfo.Read((byte[])null));
            }

            [Fact]
            public void ShouldThrowExceptionWhenDataIsEmpty()
            {
                var imageInfo = new MagickImageInfo();

                Assert.Throws<ArgumentException>("data", () => imageInfo.Read(Array.Empty<byte>()));
            }
        }

        public class WithByteArrayAndOffset
        {
            [Fact]
            public void ShouldThrowExceptionWhenArrayIsNull()
            {
                var imageInfo = new MagickImageInfo();

                Assert.Throws<ArgumentNullException>("data", () => imageInfo.Read(null, 0, 0));
            }

            [Fact]
            public void ShouldThrowExceptionWhenArrayIsEmpty()
            {
                var imageInfo = new MagickImageInfo();

                Assert.Throws<ArgumentException>("data", () => imageInfo.Read(Array.Empty<byte>(), 0, 0));
            }

            [Fact]
            public void ShouldThrowExceptionWhenOffsetIsNegative()
            {
                var imageInfo = new MagickImageInfo();

                Assert.Throws<ArgumentException>("offset", () => imageInfo.Read(new byte[] { 215 }, -1, 0));
            }

            [Fact]
            public void ShouldThrowExceptionWhenCountIsZero()
            {
                var imageInfo = new MagickImageInfo();

                Assert.Throws<ArgumentException>("count", () => imageInfo.Read(new byte[] { 215 }, 0, 0));
            }

            [Fact]
            public void ShouldThrowExceptionWhenCountIsNegative()
            {
                var imageInfo = new MagickImageInfo();

                Assert.Throws<ArgumentException>("count", () => imageInfo.Read(new byte[] { 215 }, 0, -1));
            }
        }

        public class WithFileInfo
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileIsNull()
            {
                var imageInfo = new MagickImageInfo();

                Assert.Throws<ArgumentNullException>("file", () => imageInfo.Read((FileInfo)null));
            }
        }

        public class WithFileName
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsNull()
            {
                var imageInfo = new MagickImageInfo();

                Assert.Throws<ArgumentNullException>("fileName", () => imageInfo.Read((string)null));
            }

            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsEmpty()
            {
                var imageInfo = new MagickImageInfo();

                Assert.Throws<ArgumentException>("fileName", () => imageInfo.Read(string.Empty));
            }

            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsInvalid()
            {
                var imageInfo = new MagickImageInfo();

                var exception = Assert.Throws<MagickBlobErrorException>(() =>
                {
                    imageInfo.Read(Files.Missing);
                });

                Assert.Contains("error/blob.c/OpenBlob", exception.Message);
            }

            [Fact]
            public void ShouldReturnTheCorrectInformation()
            {
                var imageInfo = new MagickImageInfo();
                imageInfo.Read(Files.ImageMagickJPG);

                Assert.Equal(ColorSpace.sRGB, imageInfo.ColorSpace);
                Assert.Equal(CompressionMethod.JPEG, imageInfo.Compression);
                Assert.EndsWith("ImageMagick.jpg", imageInfo.FileName);
                Assert.Equal(MagickFormat.Jpeg, imageInfo.Format);
                Assert.Equal(118, imageInfo.Height);
                Assert.Equal(72, imageInfo.Density.X);
                Assert.Equal(72, imageInfo.Density.Y);
                Assert.Equal(DensityUnit.PixelsPerInch, imageInfo.Density.Units);
                Assert.Equal(Interlace.NoInterlace, imageInfo.Interlace);
                Assert.Equal(100, imageInfo.Quality);
                Assert.Equal(123, imageInfo.Width);
                Assert.Equal(OrientationType.Undefined, imageInfo.Orientation);
            }
        }

        public class WithFileNameAndReadSettings
        {
            [Fact]
            public void ShouldUseTheReadSettings()
            {
                var imageInfo = new MagickImageInfo();
                var settings = new MagickReadSettings(new BmpReadDefines
                {
                    IgnoreFileSize = true,
                });

                imageInfo.Read(Files.Coders.InvalidCrcBMP, settings);
            }
        }

        public class WithStream
        {
            [Fact]
            public void ShouldThrowExceptionWhenStreamIsNull()
            {
                var imageInfo = new MagickImageInfo();

                Assert.Throws<ArgumentNullException>("stream", () => imageInfo.Read((Stream)null));
            }
        }
    }
}
