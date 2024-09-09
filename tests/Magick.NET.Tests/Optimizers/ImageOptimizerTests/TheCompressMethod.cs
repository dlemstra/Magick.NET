// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class ImageOptimizerTests
{
    public class TheCompressMethod
    {
        public class WithFileInfo : ImageOptimizerTestHelper
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileIsNull()
            {
                var optimizer = new ImageOptimizer();

                Assert.Throws<ArgumentNullException>("file", () => optimizer.Compress((FileInfo)null!));
            }

            [Fact]
            public void ShouldThrowExceptionWhenFileIsEmpty()
            {
                var optimizer = new ImageOptimizer();
                using var tempFile = new TemporaryFile("empty");

                var result = optimizer.Compress(tempFile.File);

                Assert.False(result);
            }

            [Fact]
            public void ShouldThrowExceptionWhenFileIsUnsupportedFormat()
            {
                var optimizer = new ImageOptimizer();

                var exception = Assert.Throws<MagickCorruptImageErrorException>(() => optimizer.Compress(new FileInfo(Files.InvitationTIF)));
                Assert.Contains("Invalid format", exception.Message);
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenIgnoringUnsupportedFileName()
            {
                var optimizer = new ImageOptimizer { IgnoreUnsupportedFormats = true };
                var compressionSuccess = optimizer.Compress(new FileInfo(Files.InvitationTIF));

                Assert.False(compressionSuccess);
            }

            [Fact]
            public void ShouldMakeFileSmallerWhenFileIsCompressibleJpgFile()
            {
                var optimizer = new ImageOptimizer();

                AssertCompressSmaller(Files.ImageMagickJPG, (FileInfo file) => optimizer.Compress(file));
            }

            [Fact]
            public void ShouldMakeFileSmallerWhenFileIsCompressiblePngFile()
            {
                var optimizer = new ImageOptimizer();

                AssertCompressSmaller(Files.SnakewarePNG, (FileInfo file) => optimizer.Compress(file));
            }

            [Fact]
            public void ShouldMakeFileSmallerWhenFileIsCompressibleIcoFile()
            {
                var optimizer = new ImageOptimizer();

                AssertCompressSmaller(Files.WandICO, (FileInfo file) => optimizer.Compress(file));
            }

            [Fact]
            public void ShouldMakeFileSmallerWhenFileIsCompressibleGifFile()
            {
                var optimizer = new ImageOptimizer();

                AssertCompressSmaller(Files.FujiFilmFinePixS1ProGIF, (FileInfo file) => optimizer.Compress(file));
            }

            [Fact]
            public void ShouldNotMakeFileSmallerWhenFileIsUncompressibleJpgFile()
            {
                var optimizer = new ImageOptimizer();

                AssertCompressNotSmaller(Files.LetterJPG, (FileInfo file) => optimizer.Compress(file));
            }

            [Fact]
            public void ShouldNotMakeFileSmallerWhenFileIsUncompressiblePngFile()
            {
                var optimizer = new ImageOptimizer();

                AssertCompressNotSmaller(Files.MagickNETIconPNG, (FileInfo file) => optimizer.Compress(file));
            }

            [Fact]
            public void ShouldNotMakeFileSmallerWhenFileIsUncompressibleIcoFile()
            {
                var optimizer = new ImageOptimizer();

                AssertCompressNotSmaller(Files.ImageMagickICO, (FileInfo file) => optimizer.Compress(file));
            }

            [Fact]
            public void ShouldNotMakeFileSmallerWhenFileIsUncompressibleGifFile()
            {
                var optimizer = new ImageOptimizer();

                AssertCompressNotSmaller(Files.RoseSparkleGIF, (FileInfo file) => optimizer.Compress(file));
            }
        }

        public class WithFileName : ImageOptimizerTestHelper
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsNull()
            {
                var optimizer = new ImageOptimizer();

                Assert.Throws<ArgumentNullException>("fileName", () => optimizer.Compress((string)null!));
            }

            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsEmpty()
            {
                var optimizer = new ImageOptimizer();

                Assert.Throws<ArgumentException>("fileName", () => optimizer.Compress(string.Empty));
            }

            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsInvalid()
            {
                var optimizer = new ImageOptimizer();

                var exception = Assert.Throws<MagickBlobErrorException>(() => optimizer.Compress(Files.Missing));
                Assert.Contains("error/blob.c/OpenBlob", exception.Message);
            }

            [Fact]
            public void ShouldThrowExceptionWhenFileIsUnsupportedFormat()
            {
                var optimizer = new ImageOptimizer();

                var exception = Assert.Throws<MagickCorruptImageErrorException>(() => optimizer.Compress(Files.InvitationTIF));
                Assert.Contains("Invalid format", exception.Message);
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenIgnoringUnsupportedFileName()
            {
                var optimizer = new ImageOptimizer { IgnoreUnsupportedFormats = true };
                var compressionSuccess = optimizer.Compress(Files.InvitationTIF);

                Assert.False(compressionSuccess);
            }

            [Fact]
            public void ShouldMakeFileSmallerWhenFileNameIsCompressibleJpgFile()
            {
                var optimizer = new ImageOptimizer();

                AssertCompressSmaller(Files.ImageMagickJPG, (string fileName) => optimizer.Compress(fileName));
            }

            [Fact]
            public void ShouldMakeFileSmallerWhenFileNameIsCompressiblePngFile()
            {
                var optimizer = new ImageOptimizer();

                AssertCompressSmaller(Files.SnakewarePNG, (string fileName) => optimizer.Compress(fileName));
            }

            [Fact]
            public void ShouldMakeFileSmallerWhenFileNameIsCompressibleIcoFile()
            {
                var optimizer = new ImageOptimizer();

                AssertCompressSmaller(Files.WandICO, (string fileName) => optimizer.Compress(fileName));
            }

            [Fact]
            public void ShouldMakeFileSmallerWhenFileNameIsCompressibleGifFile()
            {
                var optimizer = new ImageOptimizer();

                AssertCompressSmaller(Files.FujiFilmFinePixS1ProGIF, (string fileName) => optimizer.Compress(fileName));
            }

            [Fact]
            public void ShouldNotMakeFileSmallerWhenFileNameIsUncompressibleJpgFile()
            {
                var optimizer = new ImageOptimizer();

                AssertCompressNotSmaller(Files.LetterJPG, (string fileName) => optimizer.Compress(fileName));
            }

            [Fact]
            public void ShouldNotMakeFileSmallerWhenFileNameIsUncompressiblePngFile()
            {
                var optimizer = new ImageOptimizer();

                AssertCompressNotSmaller(Files.MagickNETIconPNG, (string fileName) => optimizer.Compress(fileName));
            }

            [Fact]
            public void ShouldNotMakeFileSmallerWhenFileNameIsUncompressibleIcoFile()
            {
                var optimizer = new ImageOptimizer();

                AssertCompressNotSmaller(Files.ImageMagickICO, (string fileName) => optimizer.Compress(fileName));
            }

            [Fact]
            public void ShouldNotMakeFileSmallerWhenFileNameIsUncompressibleGifFile()
            {
                var optimizer = new ImageOptimizer();

                AssertCompressNotSmaller(Files.RoseSparkleGIF, (string fileName) => optimizer.Compress(fileName));
            }
        }

        public class WithStream : ImageOptimizerTestHelper
        {
            [Fact]
            public void ShouldThrowExceptionWhenStreamIsNull()
            {
                var optimizer = new ImageOptimizer();

                Assert.Throws<ArgumentNullException>("stream", () => optimizer.Compress((Stream)null!));
            }

            [Fact]
            public void ShouldThrowExceptionWhenStreamCannotRead()
            {
                var optimizer = new ImageOptimizer();
                using var stream = TestStream.ThatCannotRead();

                Assert.Throws<ArgumentException>("stream", () => optimizer.Compress(stream));
            }

            [Fact]
            public void ShouldThrowExceptionWhenStreamCannotWrite()
            {
                var optimizer = new ImageOptimizer();
                using var stream = TestStream.ThatCannotWrite();

                Assert.Throws<ArgumentException>("stream", () => optimizer.Compress(stream));
            }

            [Fact]
            public void ShouldThrowExceptionWhenStreamCannotSeek()
            {
                var optimizer = new ImageOptimizer();
                using var stream = TestStream.ThatCannotSeek();

                Assert.Throws<ArgumentException>("stream", () => optimizer.Compress(stream));
            }

            [Fact]
            public void ShouldThrowExceptionWhenStreamIsUnsupportedFormat()
            {
                var optimizer = new ImageOptimizer();
                using var fileStream = OpenStream(Files.InvitationTIF);

                var exception = Assert.Throws<MagickCorruptImageErrorException>(() => optimizer.Compress(fileStream));
                Assert.Contains("Invalid format", exception.Message);
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenIgnoringUnsupportedStream()
            {
                var optimizer = new ImageOptimizer { IgnoreUnsupportedFormats = true };
                using var fileStream = OpenStream(Files.InvitationTIF);

                var compressionSuccess = optimizer.Compress(fileStream);
                Assert.False(compressionSuccess);
            }

            [Fact]
            public void ShouldMakeFileSmallerWhenStreamIsCompressibleJpgFile()
            {
                var optimizer = new ImageOptimizer();

                AssertCompressSmaller(Files.ImageMagickJPG, (Stream stream) => optimizer.Compress(stream));
            }

            [Fact]
            public void ShouldMakeFileSmallerWhenStreamIsCompressiblePngFile()
            {
                var optimizer = new ImageOptimizer();
                AssertCompressSmaller(Files.SnakewarePNG, (Stream stream) => optimizer.Compress(stream));
            }

            [Fact]
            public void ShouldMakeFileSmallerWhenStreamIsCompressibleIcoFile()
            {
                var optimizer = new ImageOptimizer();
                AssertCompressSmaller(Files.WandICO, (Stream stream) => optimizer.Compress(stream));
            }

            [Fact]
            public void ShouldMakeFileSmallerWhenStreamIsCompressibleGifFile()
            {
                var optimizer = new ImageOptimizer();
                AssertCompressSmaller(Files.FujiFilmFinePixS1ProGIF, (Stream stream) => optimizer.Compress(stream));
            }

            [Fact]
            public void ShouldNotMakeFileSmallerWhenStreamIsUncompressibleJpgFile()
            {
                var optimizer = new ImageOptimizer();

                AssertCompressNotSmaller(Files.LetterJPG, (Stream stream) => optimizer.Compress(stream));
            }

            [Fact]
            public void ShouldNotMakeFileSmallerWhenStreamIsUncompressiblePngFile()
            {
                var optimizer = new ImageOptimizer();

                AssertCompressNotSmaller(Files.MagickNETIconPNG, (Stream stream) => optimizer.Compress(stream));
            }

            [Fact]
            public void ShouldNotMakeFileSmallerWhenStreamIsUncompressibleIcoFile()
            {
                var optimizer = new ImageOptimizer();

                AssertCompressNotSmaller(Files.ImageMagickICO, (Stream stream) => optimizer.Compress(stream));
            }

            [Fact]
            public void ShouldNotMakeFileSmallerWhenStreamIsUncompressibleGifFile()
            {
                var optimizer = new ImageOptimizer();

                AssertCompressNotSmaller(Files.RoseSparkleGIF, (Stream stream) => optimizer.Compress(stream));
            }
        }
    }
}
