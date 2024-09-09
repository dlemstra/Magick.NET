// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class ImageOptimizerTests
{
    public class TheLosslessCompressMethod
    {
        public class WithFileInfo : ImageOptimizerTestHelper
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileIsNull()
            {
                var optimizer = new ImageOptimizer();

                Assert.Throws<ArgumentNullException>("file", () => optimizer.LosslessCompress((FileInfo)null!));
            }

            [Fact]
            public void ShouldReturnFalseWhenFileIsEmpty()
            {
                var optimizer = new ImageOptimizer();
                using var tempFile = new TemporaryFile("empty");
                var result = optimizer.LosslessCompress(tempFile.File);

                Assert.False(result);
            }

            [Fact]
            public void ShouldThrowExceptionWhenFileIsUnsupportedFormat()
            {
                var optimizer = new ImageOptimizer();
                var exception = Assert.Throws<MagickCorruptImageErrorException>(() => optimizer.LosslessCompress(new FileInfo(Files.InvitationTIF)));

                Assert.Contains("Invalid format", exception.Message);
            }

            [Fact]
            public void ShouldNotThrowExceptionWhenIgnoringUnsupportedFileName()
            {
                var optimizer = new ImageOptimizer { IgnoreUnsupportedFormats = true };
                var compressionSuccess = optimizer.LosslessCompress(new FileInfo(Files.InvitationTIF));

                Assert.False(compressionSuccess);
            }

            [Fact]
            public void ShouldMakeFileSmallerWhenFileIsCompressibleJpgFile()
            {
                var optimizer = new ImageOptimizer();

                AssertCompressSmaller(Files.ImageMagickJPG, (FileInfo file) => optimizer.LosslessCompress(file));
            }

            [Fact]
            public void ShouldMakeFileSmallerWhenFileIsCompressiblePngFile()
            {
                var optimizer = new ImageOptimizer();

                AssertCompressSmaller(Files.SnakewarePNG, (FileInfo file) => optimizer.LosslessCompress(file));
            }

            [Fact]
            public void ShouldMakeFileSmallerWhenFileIsCompressibleIcoFile()
            {
                var optimizer = new ImageOptimizer();

                AssertCompressSmaller(Files.WandICO, (FileInfo file) => optimizer.LosslessCompress(file));
            }

            [Fact]
            public void ShouldMakeFileSmallerWhenFileIsCompressibleGifFile()
            {
                var optimizer = new ImageOptimizer();

                AssertCompressSmaller(Files.FujiFilmFinePixS1ProGIF, (FileInfo file) => optimizer.LosslessCompress(file));
            }

            [Fact]
            public void ShouldNotMakeFileSmallerWhenFileIsUnCompressibleJpgFile()
            {
                var optimizer = new ImageOptimizer();

                AssertCompressNotSmaller(Files.LetterJPG, (FileInfo file) => optimizer.LosslessCompress(file));
            }

            [Fact]
            public void ShouldNotMakeFileSmallerWhenFileIsUnCompressiblePngFile()
            {
                var optimizer = new ImageOptimizer();

                AssertCompressNotSmaller(Files.MagickNETIconPNG, (FileInfo file) => optimizer.LosslessCompress(file));
            }

            [Fact]
            public void ShouldNotMakeFileSmallerWhenFileIsUnCompressibleIcoFile()
            {
                var optimizer = new ImageOptimizer();

                AssertCompressNotSmaller(Files.ImageMagickICO, (FileInfo file) => optimizer.LosslessCompress(file));
            }

            [Fact]
            public void ShouldNotMakeFileSmallerWhenFileIsUnCompressibleGifFile()
            {
                var optimizer = new ImageOptimizer();

                AssertCompressNotSmaller(Files.RoseSparkleGIF, (FileInfo file) => optimizer.LosslessCompress(file));
            }
        }

        public class WithFileName : ImageOptimizerTestHelper
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsNull()
            {
                var optimizer = new ImageOptimizer();

                Assert.Throws<ArgumentNullException>("fileName", () => optimizer.LosslessCompress((string)null!));
            }

            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsEmpty()
            {
                var optimizer = new ImageOptimizer();

                Assert.Throws<ArgumentException>("fileName", () => optimizer.LosslessCompress(string.Empty));
            }

            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsInvalid()
            {
                var optimizer = new ImageOptimizer();

                var exception = Assert.Throws<MagickBlobErrorException>(() => optimizer.LosslessCompress(Files.Missing));
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
                var compressionSuccess = optimizer.LosslessCompress(Files.InvitationTIF);

                Assert.False(compressionSuccess);
            }

            [Fact]
            public void ShouldMakeFileSmallerWhenFileNameIsCompressibleJpgFile()
            {
                var optimizer = new ImageOptimizer();

                AssertCompressSmaller(Files.ImageMagickJPG, (string file) => optimizer.LosslessCompress(file));
            }

            [Fact]
            public void ShouldMakeFileSmallerWhenFileNameIsCompressiblePngFile()
            {
                var optimizer = new ImageOptimizer();

                AssertCompressSmaller(Files.SnakewarePNG, (string file) => optimizer.LosslessCompress(file));
            }

            [Fact]
            public void ShouldMakeFileSmallerWhenFileNameIsCompressibleIcoFile()
            {
                var optimizer = new ImageOptimizer();

                AssertCompressSmaller(Files.WandICO, (string file) => optimizer.LosslessCompress(file));
            }

            [Fact]
            public void ShouldMakeFileSmallerWhenFileNameIsCompressibleGifFile()
            {
                var optimizer = new ImageOptimizer();

                AssertCompressSmaller(Files.FujiFilmFinePixS1ProGIF, (string file) => optimizer.LosslessCompress(file));
            }

            [Fact]
            public void ShouldNotMakeFileSmallerWhenFileNameIsUncompressibleJpgFile()
            {
                var optimizer = new ImageOptimizer();

                AssertCompressNotSmaller(Files.LetterJPG, (string file) => optimizer.LosslessCompress(file));
            }

            [Fact]
            public void ShouldNotMakeFileSmallerWhenFileNameIsUncompressiblePngFile()
            {
                var optimizer = new ImageOptimizer();

                AssertCompressNotSmaller(Files.MagickNETIconPNG, (string file) => optimizer.LosslessCompress(file));
            }

            [Fact]
            public void ShouldNotMakeFileSmallerWhenFileNameIsUncompressibleIcoFile()
            {
                var optimizer = new ImageOptimizer();

                AssertCompressNotSmaller(Files.ImageMagickICO, (string file) => optimizer.LosslessCompress(file));
            }

            [Fact]
            public void ShouldNotMakeFileSmallerWhenFileNameIsUncompressibleGifFile()
            {
                var optimizer = new ImageOptimizer();

                AssertCompressNotSmaller(Files.RoseSparkleGIF, (string file) => optimizer.LosslessCompress(file));
            }
        }

        public class WithStream : ImageOptimizerTestHelper
        {
            [Fact]
            public void ShouldThrowExceptionWhenStreamIsNull()
            {
                var optimizer = new ImageOptimizer();

                Assert.Throws<ArgumentNullException>("stream", () => optimizer.LosslessCompress((Stream)null!));
            }

            [Fact]
            public void ShouldThrowExceptionWhenStreamCannotRead()
            {
                var optimizer = new ImageOptimizer();
                using var stream = TestStream.ThatCannotRead();

                Assert.Throws<ArgumentException>("stream", () => optimizer.LosslessCompress(stream));
            }

            [Fact]
            public void ShouldThrowExceptionWhenStreamCannotWrite()
            {
                var optimizer = new ImageOptimizer();
                using var stream = TestStream.ThatCannotWrite();

                Assert.Throws<ArgumentException>("stream", () => optimizer.LosslessCompress(stream));
            }

            [Fact]
            public void ShouldThrowExceptionWhenStreamCannotSeek()
            {
                var optimizer = new ImageOptimizer();
                using var stream = TestStream.ThatCannotSeek();

                Assert.Throws<ArgumentException>("stream", () => optimizer.LosslessCompress(stream));
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

                var compressionSuccess = optimizer.LosslessCompress(fileStream);

                Assert.False(compressionSuccess);
            }

            [Fact]
            public void ShouldMakeFileSmallerWhenStreamIsCompressibleJpgStream()
            {
                var optimizer = new ImageOptimizer();

                AssertCompressSmaller(Files.ImageMagickJPG, (Stream stream) => optimizer.LosslessCompress(stream));
            }

            [Fact]
            public void ShouldMakeFileSmallerWhenStreamIsCompressiblePngStream()
            {
                var optimizer = new ImageOptimizer();

                AssertCompressSmaller(Files.SnakewarePNG, (Stream stream) => optimizer.LosslessCompress(stream));
            }

            [Fact]
            public void ShouldMakeFileSmallerWhenStreamIsCompressibleIcoStream()
            {
                var optimizer = new ImageOptimizer();

                AssertCompressSmaller(Files.WandICO, (Stream stream) => optimizer.LosslessCompress(stream));
            }

            [Fact]
            public void ShouldMakeFileSmallerWhenStreamIsCompressibleGifStream()
            {
                var optimizer = new ImageOptimizer();

                AssertCompressSmaller(Files.FujiFilmFinePixS1ProGIF, (Stream stream) => optimizer.LosslessCompress(stream));
            }

            [Fact]
            public void ShouldNotMakeFileSmallerWhenStreamIsCompressibleJpgStream()
            {
                var optimizer = new ImageOptimizer();

                AssertCompressNotSmaller(Files.LetterJPG, (Stream stream) => optimizer.LosslessCompress(stream));
            }

            [Fact]
            public void ShouldNotMakeFileSmallerWhenStreamIsCompressiblePngStream()
            {
                var optimizer = new ImageOptimizer();

                AssertCompressNotSmaller(Files.MagickNETIconPNG, (Stream stream) => optimizer.LosslessCompress(stream));
            }

            [Fact]
            public void ShouldNotMakeFileSmallerWhenStreamIsCompressibleIcoStream()
            {
                var optimizer = new ImageOptimizer();

                AssertCompressNotSmaller(Files.ImageMagickICO, (Stream stream) => optimizer.LosslessCompress(stream));
            }

            [Fact]
            public void ShouldNotMakeFileSmallerWhenStreamIsCompressibleGifStream()
            {
                var optimizer = new ImageOptimizer();

                AssertCompressNotSmaller(Files.RoseSparkleGIF, (Stream stream) => optimizer.LosslessCompress(stream));
            }
        }
    }
}
