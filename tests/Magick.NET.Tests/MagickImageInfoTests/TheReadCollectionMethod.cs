// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using System.Linq;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageInfoTests
{
    public partial class TheReadCollectionMethod
    {
        public class WithByteArray
        {
            [Fact]
            public void ShouldThrowExceptionWhenDataIsNull()
            {
                Assert.Throws<ArgumentNullException>("data", () => MagickImageInfo.ReadCollection((byte[])null).ToArray());
            }

            [Fact]
            public void ShouldThrowExceptionWhenDataIsEmpty()
            {
                Assert.Throws<ArgumentException>("data", () => MagickImageInfo.ReadCollection(Array.Empty<byte>()).ToArray());
            }
        }

        public class WithByteArrayAndOffset
        {
            [Fact]
            public void ShouldThrowExceptionWhenArrayIsNull()
            {
                Assert.Throws<ArgumentNullException>("data", () => MagickImageInfo.ReadCollection(null, 0, 0).ToArray());
            }

            [Fact]
            public void ShouldThrowExceptionWhenArrayIsEmpty()
            {
                Assert.Throws<ArgumentException>("data", () => MagickImageInfo.ReadCollection(Array.Empty<byte>(), 0, 0).ToArray());
            }

            [Fact]
            public void ShouldThrowExceptionWhenCountIsZero()
            {
                Assert.Throws<ArgumentException>("count", () => MagickImageInfo.ReadCollection(new byte[] { 215 }, 0, 0).ToArray());
            }
        }

        public class WithFileInfo
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileIsNull()
            {
                Assert.Throws<ArgumentNullException>("file", () => MagickImageInfo.ReadCollection((FileInfo)null).ToArray());
            }
        }

        public class WithFileName
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsNull()
            {
                Assert.Throws<ArgumentNullException>("fileName", () => MagickImageInfo.ReadCollection((string)null).ToArray());
            }

            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsEmpty()
            {
                Assert.Throws<ArgumentException>("fileName", () => MagickImageInfo.ReadCollection(string.Empty).ToArray());
            }

            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsInvalid()
            {
                var exception = Assert.Throws<MagickBlobErrorException>(() =>
                {
                    MagickImageInfo.ReadCollection(Files.Missing).ToArray();
                });

                Assert.Contains("error/blob.c/OpenBlob", exception.Message);
            }

            [Fact]
            public void ShouldReturnEnumerableWithCorrectCount()
            {
                var info = MagickImageInfo.ReadCollection(Files.RoseSparkleGIF);
                Assert.Equal(3, info.Count());
            }

            [Fact]
            public void ShouldReturnTheCorrectInformation()
            {
                var info = MagickImageInfo.ReadCollection(Files.InvitationTIF);

                var first = info.First();
                Assert.Equal(ColorSpace.sRGB, first.ColorSpace);
                Assert.Equal(CompressionMethod.Zip, first.Compression);
                Assert.EndsWith("Invitation.tif", first.FileName);
                Assert.Equal(MagickFormat.Tiff, first.Format);
                Assert.Equal(700U, first.Height);
                Assert.Equal(350, first.Density.X);
                Assert.Equal(350, first.Density.Y);
                Assert.Equal(DensityUnit.PixelsPerInch, first.Density.Units);
                Assert.Equal(Interlace.NoInterlace, first.Interlace);
                Assert.Equal(827U, first.Width);
                Assert.Equal(0U, first.Quality);
                Assert.Equal(OrientationType.TopLeft, first.Orientation);
            }
        }

        public class WithStream
        {
            [Fact]
            public void ShouldThrowExceptionWhenStreamIsNull()
            {
                Assert.Throws<ArgumentNullException>("stream", () => MagickImageInfo.ReadCollection((Stream)null).ToArray());
            }
        }
    }
}
