// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class GifOptimizerTests
{
    public class TheLosslessCompressMethod : GifOptimizerTests
    {
        public class WithFileInfoFileNameOrStream : TheLosslessCompressMethod
        {
            [Fact]
            public void ShouldCompressLossless()
            {
                var result = AssertLosslessCompressSmaller(Files.FujiFilmFinePixS1ProGIF);
                Assert.Equal(172861, result);
            }

            [Fact]
            public void ShouldTryToCompressLossLess()
                => AssertLosslessCompressNotSmaller(Files.RoseSparkleGIF);

            [Fact]
            public void ShouldBeAbleToCompressFileTwoTimes()
                => AssertLosslessCompressTwice(Files.FujiFilmFinePixS1ProGIF);

            [Fact]
            public void ShouldThrowExceptionWhenFileFormatIsInvalid()
                => AssertLosslessCompressInvalidFileFormat(Files.ImageMagickJPG);
        }

        public class WithFileInfo : TheLosslessCompressMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileIsNull()
                => Assert.Throws<ArgumentNullException>("file", () => Optimizer.LosslessCompress((FileInfo)null!));
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
