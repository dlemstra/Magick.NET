// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageInfoTests
    {
        public class TheConstructor
        {
            public class WithByteArray
            {
                [Fact]
                public void ShouldThrowExceptionWhenDataIsNull()
                {
                    Assert.Throws<ArgumentNullException>("data", () => new MagickImageInfo((byte[])null));
                }

                [Fact]
                public void ShouldThrowExceptionWhenDataIsEmpty()
                {
                    Assert.Throws<ArgumentException>("data", () => new MagickImageInfo(new byte[0]));
                }
            }

            public class WithByteArrayAndOffset
            {
                [Fact]
                public void ShouldThrowExceptionWhenArrayIsNull()
                {
                    Assert.Throws<ArgumentNullException>("data", () => new MagickImageInfo(null, 0, 0));
                }

                [Fact]
                public void ShouldThrowExceptionWhenArrayIsEmpty()
                {
                    Assert.Throws<ArgumentException>("data", () => new MagickImageInfo(new byte[] { }, 0, 0));
                }

                [Fact]
                public void ShouldThrowExceptionWhenOffsetIsNegative()
                {
                    Assert.Throws<ArgumentException>("offset", () => new MagickImageInfo(new byte[] { 215 }, -1, 0));
                }

                [Fact]
                public void ShouldThrowExceptionWhenCountIsZero()
                {
                    Assert.Throws<ArgumentException>("count", () => new MagickImageInfo(new byte[] { 215 }, 0, 0));
                }

                [Fact]
                public void ShouldThrowExceptionWhenCountIsNegative()
                {
                    Assert.Throws<ArgumentException>("count", () => new MagickImageInfo(new byte[] { 215 }, 0, -1));
                }
            }

            public class WithFileInfo
            {
                [Fact]
                public void ShouldThrowExceptionWhenFileIsNull()
                {
                    Assert.Throws<ArgumentNullException>("file", () => new MagickImageInfo((FileInfo)null));
                }
            }

            public class WithFileName
            {
                [Fact]
                public void ShouldThrowExceptionWhenFileNameIsNull()
                {
                    Assert.Throws<ArgumentNullException>("fileName", () => new MagickImageInfo((string)null));
                }

                [Fact]
                public void ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    Assert.Throws<ArgumentException>("fileName", () => new MagickImageInfo(string.Empty));
                }

                [Fact]
                public void ShouldThrowExceptionWhenFileNameIsInvalid()
                {
                    var exception = Assert.Throws<MagickBlobErrorException>(() =>
                    {
                        new MagickImageInfo(Files.Missing);
                    });

                    Assert.Contains("error/blob.c/OpenBlob", exception.Message);
                }
            }

            public class WithStream
            {
                [Fact]
                public void ShouldThrowExceptionWhenStreamIsNull()
                {
                    Assert.Throws<ArgumentNullException>("stream", () => new MagickImageInfo((Stream)null));
                }
            }
        }
    }
}
