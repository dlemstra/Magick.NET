// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class ImageOptimizerTests
{
    public class TheIsSupportedMethod
    {
        public class WithFileInfo
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileIsNull()
            {
                var optimizer = new ImageOptimizer();

                Assert.Throws<ArgumentNullException>("file", () => optimizer.IsSupported((FileInfo)null!));
            }

            [Fact]
            public void ShouldReturnTrueWhenFileIsMissingPngFile()
            {
                var optimizer = new ImageOptimizer();

                Assert.True(optimizer.IsSupported(new FileInfo(Files.Missing)));
            }

            [Fact]
            public void ShouldReturnTrueWhenFileIsFileIsJpgFile()
            {
                var optimizer = new ImageOptimizer();

                Assert.True(optimizer.IsSupported(new FileInfo(Files.ImageMagickJPG)));
            }

            [Fact]
            public void ShouldReturnTrueWhenFileIsFileIsPngFile()
            {
                var optimizer = new ImageOptimizer();

                Assert.True(optimizer.IsSupported(new FileInfo(Files.SnakewarePNG)));
            }

            [Fact]
            public void ShouldReturnTrueWhenFileIsFileIsIcoFile()
            {
                var optimizer = new ImageOptimizer();

                Assert.True(optimizer.IsSupported(new FileInfo(Files.WandICO)));
            }

            [Fact]
            public void ShouldReturnTrueWhenFileIsFileIsGifFile()
            {
                var optimizer = new ImageOptimizer();

                Assert.True(optimizer.IsSupported(new FileInfo(Files.FujiFilmFinePixS1ProGIF)));
            }

            [Fact]
            public void ShouldReturnFalseWhenFileIsFileIsTifFile()
            {
                var optimizer = new ImageOptimizer();

                Assert.False(optimizer.IsSupported(new FileInfo(Files.InvitationTIF)));
            }
        }

        public class WithFileName
        {
            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsNull()
            {
                var optimizer = new ImageOptimizer();

                Assert.Throws<ArgumentNullException>("fileName", () => optimizer.IsSupported((string)null!));
            }

            [Fact]
            public void ShouldThrowExceptionWhenFileNameIsEmpty()
            {
                var optimizer = new ImageOptimizer();

                Assert.Throws<ArgumentException>("fileName", () => optimizer.IsSupported(string.Empty));
            }

            [Fact]
            public void ShouldReturnFalseWhenFileNameIsInvalid()
            {
                var optimizer = new ImageOptimizer();

                Assert.False(optimizer.IsSupported("invalid"));
            }

            [Fact]
            public void ShouldReturnTrueWhenFileNameIsMissingPngFile()
            {
                var optimizer = new ImageOptimizer();

                Assert.True(optimizer.IsSupported(Files.Missing));
            }

            [Fact]
            public void ShouldReturnTrueWhenFileNameIsJpgFile()
            {
                var optimizer = new ImageOptimizer();

                Assert.True(optimizer.IsSupported(Files.ImageMagickJPG));
            }

            [Fact]
            public void ShouldReturnTrueWhenFileNameIsPngFile()
            {
                var optimizer = new ImageOptimizer();

                Assert.True(optimizer.IsSupported(Files.SnakewarePNG));
            }

            [Fact]
            public void ShouldReturnTrueWhenFileNameIsIcoFile()
            {
                var optimizer = new ImageOptimizer();

                Assert.True(optimizer.IsSupported(Files.WandICO));
            }

            [Fact]
            public void ShouldReturnTrueWhenFileNameIsGifFile()
            {
                var optimizer = new ImageOptimizer();

                Assert.True(optimizer.IsSupported(Files.FujiFilmFinePixS1ProGIF));
            }

            [Fact]
            public void ShouldReturnFalseWhenFileNameIsTifFile()
            {
                var optimizer = new ImageOptimizer();

                Assert.False(optimizer.IsSupported(Files.InvitationTIF));
            }
        }

        public class WithStream
        {
            [Fact]
            public void ShouldThrowExceptionWhenStreamIsNull()
            {
                var optimizer = new ImageOptimizer();

                Assert.Throws<ArgumentNullException>("stream", () => optimizer.IsSupported((Stream)null!));
            }

            [Fact]
            public void ShouldReturnFalseWhenStreamCannotRead()
            {
                var optimizer = new ImageOptimizer();
                using var stream = TestStream.ThatCannotRead();

                Assert.False(optimizer.IsSupported(stream));
            }

            [Fact]
            public void ShouldReturnFalseWhenStreamCannotWrite()
            {
                var optimizer = new ImageOptimizer();
                using var stream = TestStream.ThatCannotWrite();

                Assert.False(optimizer.IsSupported(stream));
            }

            [Fact]
            public void ShouldReturnFalseWhenStreamCannotSeek()
            {
                var optimizer = new ImageOptimizer();
                using var stream = TestStream.ThatCannotSeek();

                Assert.False(optimizer.IsSupported(stream));
            }

            [Fact]
            public void ShouldReturnTrueWhenStreamIsJpgFile()
            {
                var optimizer = new ImageOptimizer();
                using var fileStream = OpenStream(Files.ImageMagickJPG);

                Assert.True(optimizer.IsSupported(fileStream));
                Assert.Equal(0, fileStream.Position);
            }

            [Fact]
            public void ShouldReturnTrueWhenStreamIsPngFile()
            {
                var optimizer = new ImageOptimizer();
                using var fileStream = OpenStream(Files.SnakewarePNG);

                Assert.True(optimizer.IsSupported(fileStream));
                Assert.Equal(0, fileStream.Position);
            }

            [Fact]
            public void ShouldReturnTrueWhenStreamIsIcoFile()
            {
                var optimizer = new ImageOptimizer();
                using var fileStream = OpenStream(Files.WandICO);

                Assert.True(optimizer.IsSupported(fileStream));
                Assert.Equal(0, fileStream.Position);
            }

            [Fact]
            public void ShouldReturnTrueWhenStreamIsGifFile()
            {
                var optimizer = new ImageOptimizer();
                using var fileStream = OpenStream(Files.FujiFilmFinePixS1ProGIF);

                Assert.True(optimizer.IsSupported(fileStream));
                Assert.Equal(0, fileStream.Position);
            }

            [Fact]
            public void ShouldReturnFalseWhenStreamIsTifFile()
            {
                var optimizer = new ImageOptimizer();
                using var fileStream = OpenStream(Files.InvitationTIF);

                Assert.False(optimizer.IsSupported(fileStream));
                Assert.Equal(0, fileStream.Position);
            }
        }
    }
}
