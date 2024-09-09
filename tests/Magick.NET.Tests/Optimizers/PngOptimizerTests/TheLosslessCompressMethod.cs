// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using ImageMagick;
using ImageMagick.ImageOptimizers;
using Xunit;

namespace Magick.NET.Tests;

public partial class PngOptimizerTests
{
    public class TheLosslessCompressMethod : PngOptimizerTests
    {
        public class WithFileInfoFileNameOrStream : TheLosslessCompressMethod
        {
            [Fact]
            public void ShouldCompressLossless()
            {
                var result = AssertLosslessCompressSmaller(Files.SnakewarePNG);
                Assert.Equal(8684, result);
            }

            [Fact]
            public void ShouldTryToCompressLossLess()
                => AssertLosslessCompressNotSmaller(Files.MagickNETIconPNG);

            [Fact]
            public void ShouldBeAbleToCompressFileTwoTimes()
                => AssertLosslessCompressTwice(Files.SnakewarePNG);

            [Fact]
            public void ShouldThrowExceptionWhenFileFormatIsInvalid()
                => AssertLosslessCompressInvalidFileFormat(Files.ImageMagickJPG);
        }

        public class WithFileInfo : TheLosslessCompressMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileIsNull()
                => Assert.Throws<ArgumentNullException>("file", () => Optimizer.LosslessCompress((FileInfo)null!));

            [Fact]
            public void ShouldNotOptimizeAnimatedPNG()
            {
                var optimizer = new PngOptimizer();
                using var tempFile = new TemporaryFile(Files.Coders.AnimatedPNGexampleBouncingBeachBallPNG);

                var result = optimizer.LosslessCompress(tempFile.File);
                Assert.False(result);
            }
        }

        public class WithFileName : TheLosslessCompressMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsNull()
                => Assert.Throws<ArgumentNullException>("fileName", () => Optimizer.LosslessCompress((string)null!));

            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsEmpty()
                => Assert.Throws<ArgumentException>("fileName", () => Optimizer.LosslessCompress(string.Empty));

            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsInvalid()
            {
                var exception = Assert.Throws<MagickBlobErrorException>(() => Optimizer.LosslessCompress(Files.Missing));
                Assert.Contains("error/blob.c/OpenBlob", exception.Message);
            }
        }

        public class WithStream : TheLosslessCompressMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenStreamIsNull()
                => Assert.Throws<ArgumentNullException>("stream", () => Optimizer.LosslessCompress((Stream)null!));

            [Fact]
            public void ShouldThrowExceptionWhenStreamIsNotReadable()
            {
                using var stream = TestStream.ThatCannotRead();

                Assert.Throws<ArgumentException>("stream", () => Optimizer.LosslessCompress(stream));
            }

            [Fact]
            public void ShouldThrowExceptionWhenStreamIsNotWriteable()
            {
                using var stream = TestStream.ThatCannotWrite();

                Assert.Throws<ArgumentException>("stream", () => Optimizer.LosslessCompress(stream));
            }

            [Fact]
            public void ShouldThrowExceptionWhenStreamIsNotSeekable()
            {
                using var stream = TestStream.ThatCannotSeek();

                Assert.Throws<ArgumentException>("stream", () => Optimizer.LosslessCompress(stream));
            }
        }
    }
}
