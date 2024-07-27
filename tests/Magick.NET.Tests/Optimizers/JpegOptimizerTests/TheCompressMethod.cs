// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using ImageMagick;
using ImageMagick.ImageOptimizers;
using Xunit;

namespace Magick.NET.Tests;

public partial class JpegOptimizerTests
{
    public class TheCompressMethod : JpegOptimizerTests
    {
        public class WithFileInfoFileNameOrStream : TheCompressMethod
        {
            [Fact]
            public void ShouldCompress()
            {
                var result = AssertCompressSmaller(Files.ImageMagickJPG);
                Assert.Equal(5146, result);
            }

            [Fact]
            public void ShouldTryToCompress()
                => AssertCompressNotSmaller(Files.LetterJPG);

            [Fact]
            public void ShouldBeAbleToCompressFileTwoTimes()
                => AssertCompressTwice(Files.ImageMagickJPG);

            [Fact]
            public void ShouldThrowExceptionWhenFileFormatIsInvalid()
                => AssertCompressInvalidFileFormat(Files.CirclePNG);
        }

        public class WithFileInfo : TheCompressMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileIsNull()
                => Assert.Throws<ArgumentNullException>("file", () => Optimizer.Compress((FileInfo)null));

            [Fact]
            public void ShouldResultInSmallerFileWHenQualityIsSetTo40()
            {
                using var tempFile = new TemporaryFile(Files.ImageMagickJPG);

                var optimizer = new JpegOptimizer();
                optimizer.Compress(tempFile.File);

                var info = new MagickImageInfo(tempFile.File);
                Assert.Equal(85U, info.Quality);

                FileHelper.Copy(Files.ImageMagickJPG, tempFile.File.FullName);

                optimizer.Compress(tempFile.File, 40);

                info = new MagickImageInfo(tempFile.File);
                Assert.Equal(40U, info.Quality);
            }

            [Fact]
            public void ShouldPreserveTheColorProfile()
            {
                using var input = new MagickImage();
                input.Ping(Files.PictureJPG);

                Assert.NotNull(input.GetColorProfile());

                using var tempFile = new TemporaryFile(Files.PictureJPG);
                var result = Optimizer.Compress(tempFile.File);

                Assert.True(result);

                using var output = new MagickImage();
                output.Ping(tempFile.File);

                Assert.NotNull(output.GetColorProfile());
            }

            [Fact]
            public void ShouldNotPreserveTheExifProfile()
            {
                using var input = new MagickImage();
                input.Ping(Files.PictureJPG);

                Assert.NotNull(input.GetExifProfile());

                using var tempFile = new TemporaryFile(Files.PictureJPG);
                var result = Optimizer.Compress(tempFile.File);

                Assert.True(result);

                using var output = new MagickImage();
                output.Ping(tempFile.File);

                Assert.Null(output.GetExifProfile());
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
                var exception = Assert.Throws<MagickCorruptImageErrorException>(() => Optimizer.Compress(Files.Missing));
                Assert.Contains("Input file read error", exception.Message);
            }

            [Fact]
            public void ShouldCompressUTF8PathName()
            {
                using var tempDir = new TemporaryDirectory("爱");
                var tempFile = Path.Combine(tempDir.FullName, "ImageMagick.jpg");
                FileHelper.Copy(Files.ImageMagickJPG, tempFile);

                Optimizer.Compress(tempFile);
            }
        }

        public class WithStream : TheCompressMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenStreamIsNull()
                => Assert.Throws<ArgumentNullException>("stream", () => Optimizer.Compress((Stream)null));

            [Fact]
            public void ShouldThrowExceptionWhenStreamIsNotReadable()
            {
                using var stream = TestStream.ThatCannotRead();

                Assert.Throws<ArgumentException>("stream", () => Optimizer.Compress(stream));
            }

            [Fact]
            public void ShouldThrowExceptionWhenStreamIsNotWriteable()
            {
                using var stream = TestStream.ThatCannotSeek();

                Assert.Throws<ArgumentException>("stream", () => Optimizer.Compress(stream));
            }

            [Fact]
            public void ShouldThrowExceptionWhenStreamIsNotSeekable()
            {
                using var stream = TestStream.ThatCannotWrite();

                Assert.Throws<ArgumentException>("stream", () => Optimizer.Compress(stream));
            }
        }
    }
}
