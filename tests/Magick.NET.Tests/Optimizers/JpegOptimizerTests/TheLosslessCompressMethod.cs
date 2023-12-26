// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class JpegOptimizerTests
{
    public class TheLosslessCompressMethod : JpegOptimizerTests
    {
        public class WithFileInfoFileNameOrStream : TheLosslessCompressMethod
        {
            [Fact]
            public void ShouldCompressLossless()
            {
                var result = AssertLosslessCompressSmaller(Files.ImageMagickJPG);
                Assert.Equal(18533, result);
            }

            [Fact]
            public void ShouldTryToCompressLossLess()
                => AssertLosslessCompressNotSmaller(Files.LetterJPG);

            [Fact]
            public void ShouldBeAbleToCompressFileTwoTimes()
                => AssertLosslessCompressTwice(Files.ImageMagickJPG);

            [Fact]
            public void ShouldThrowExceptionWhenFileFormatIsInvalid()
                => AssertLosslessCompressInvalidFileFormat(Files.CirclePNG);
        }

        public class WithFileInfo : TheLosslessCompressMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileIsNull()
                => Assert.Throws<ArgumentNullException>("file", () => Optimizer.LosslessCompress((FileInfo)null));

            [Fact]
            public void ShouldPreserveTheColorProfile()
            {
                using var input = new MagickImage();
                input.Ping(Files.PictureJPG);

                Assert.NotNull(input.GetColorProfile());

                using var tempFile = new TemporaryFile(Files.PictureJPG);
                var result = Optimizer.LosslessCompress(tempFile.File);

                Assert.True(result);

                using var output = new MagickImage();
                input.Ping(tempFile.File);

                Assert.NotNull(input.GetColorProfile());
            }

            [Fact]
            public void ShouldPreserveTheExifProfile()
            {
                using var input = new MagickImage();
                input.Ping(Files.PictureJPG);

                Assert.NotNull(input.GetExifProfile());

                using var tempFile = new TemporaryFile(Files.PictureJPG);
                var result = Optimizer.LosslessCompress(tempFile.File);

                Assert.True(result);

                using var image = new MagickImage();
                input.Ping(tempFile.File);

                Assert.NotNull(input.GetExifProfile());
            }
        }

        public class WithFileName : TheLosslessCompressMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsNull()
                => Assert.Throws<ArgumentNullException>("fileName", () => Optimizer.LosslessCompress((string)null));

            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsEmpty()
                => Assert.Throws<ArgumentException>("fileName", () => Optimizer.LosslessCompress(string.Empty));

            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsInvalid()
            {
                var exception = Assert.Throws<MagickCorruptImageErrorException>(() => Optimizer.LosslessCompress(Files.Missing));
                Assert.Contains("Input file read error", exception.Message);
            }
        }

        public class WithStream : TheLosslessCompressMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenStreamIsNull()
                => Assert.Throws<ArgumentNullException>("stream", () => Optimizer.LosslessCompress((Stream)null));

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
