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
    public class TheCompressMethod : PngOptimizerTests
    {
        public class WithFileInfoFileNameOrStream : TheCompressMethod
        {
            [Fact]
            public void ShouldCompress()
            {
                var result = AssertCompressSmaller(Files.SnakewarePNG);
                Assert.Equal(6922, result);
            }

            [Fact]
            public void ShouldTryToCompress()
                => AssertCompressNotSmaller(Files.MagickNETIconPNG);

            [Fact]
            public void ShouldBeAbleToCompressFileTwoTimes()
                => AssertCompressTwice(Files.SnakewarePNG);

            [Fact]
            public void ShouldThrowExceptionWhenFileFormatIsInvalid()
                => AssertCompressInvalidFileFormat(Files.ImageMagickJPG);
        }

        public class WithFileInfo : TheCompressMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileIsNull()
                => Assert.Throws<ArgumentNullException>("file", () => Optimizer.Compress((FileInfo)null));

            [Fact]
            public void ShouldNotOptimizeAnimatedPNG()
            {
                var optimizer = new PngOptimizer();
                using var tempFile = new TemporaryFile(Files.Coders.AnimatedPNGexampleBouncingBeachBallPNG);

                var result = optimizer.Compress(tempFile.File);
                Assert.False(result);
            }

            [Fact]
            public void ShouldDisableAlphaChannelWhenPossible()
            {
                using var tempFile = new TemporaryFile("no-alpha.png");
                using var image = new MagickImage(Files.MagickNETIconPNG);

                Assert.True(image.HasAlpha);

                image.ColorAlpha(new MagickColor("yellow"));
                image.HasAlpha = true;
                image.Write(tempFile.File);
                image.Read(tempFile.File);

                Assert.True(image.HasAlpha);

                Optimizer.LosslessCompress(tempFile.File);

                image.Read(tempFile.File);
                Assert.False(image.HasAlpha);
            }
        }

        public class WithFileName : TheCompressMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsNull()
                => Assert.Throws<ArgumentNullException>("fileName", () => Optimizer.Compress((string)null));

            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsEmpty()
                => Assert.Throws<ArgumentException>("fileName", () => Optimizer.Compress(string.Empty));

            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsInvalid()
            {
                var exception = Assert.Throws<MagickBlobErrorException>(() => Optimizer.Compress(Files.Missing));
                Assert.Contains("error/blob.c/OpenBlob", exception.Message);
            }
        }

        public class WithStream : TheCompressMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenStreamIsNull()
            {
                Assert.Throws<ArgumentNullException>("stream", () => Optimizer.Compress((Stream)null));
            }

            [Fact]
            public void ShouldThrowExceptionWhenStreamIsNotReadable()
            {
                using var stream = TestStream.ThatCannotRead();

                Assert.Throws<ArgumentException>("stream", () => Optimizer.Compress(stream));
            }

            [Fact]
            public void ShouldThrowExceptionWhenStreamIsNotWriteable()
            {
                using var stream = TestStream.ThatCannotWrite();

                Assert.Throws<ArgumentException>("stream", () => Optimizer.Compress(stream));
            }

            [Fact]
            public void ShouldThrowExceptionWhenStreamIsNotSeekable()
            {
                using var stream = TestStream.ThatCannotSeek();

                Assert.Throws<ArgumentException>("stream", () => Optimizer.Compress(stream));
            }
        }
    }
}
