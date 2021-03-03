// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using ImageMagick;
using ImageMagick.ImageOptimizers;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class JpegOptimizerTests
    {
        public class TheCompressMethod : JpegOptimizerTests
        {
            [Fact]
            public void ShouldCompress()
            {
                var result = AssertCompressSmaller(Files.ImageMagickJPG);
                Assert.Equal(5146, result);
            }

            [Fact]
            public void ShouldTryToCompress()
            {
                AssertCompressNotSmaller(Files.LetterJPG);
            }

            [Fact]
            public void ShouldBeAbleToCompressFileTwoTimes()
            {
                AssertCompressTwice(Files.ImageMagickJPG);
            }

            [Fact]
            public void ShouldThrowExceptionWhenFileFormatIsInvalid()
            {
                AssertCompressInvalidFileFormat(Files.CirclePNG);
            }

            public class WithFile : TheCompressMethod
            {
                [Fact]
                public void ShouldThrowExceptionWhenFileIsNull()
                {
                    Assert.Throws<ArgumentNullException>("file", () => Optimizer.Compress((FileInfo)null));
                }

                [Fact]
                public void ShouldResultInSmallerFileWHenQualityIsSetTo40()
                {
                    using (var tempFile = new TemporaryFile(Files.ImageMagickJPG))
                    {
                        var optimizer = new JpegOptimizer();
                        optimizer.Compress(tempFile);

                        var info = new MagickImageInfo(tempFile);
                        Assert.Equal(85, info.Quality);

                        FileHelper.Copy(Files.ImageMagickJPG, tempFile.FullName);

                        optimizer.Compress(tempFile, 40);

                        info = new MagickImageInfo(tempFile);
                        Assert.Equal(40, info.Quality);
                    }
                }

                [Fact]
                public void ShouldPreserveTheColorProfile()
                {
                    using (var image = new MagickImage())
                    {
                        image.Ping(Files.PictureJPG);

                        Assert.NotNull(image.GetColorProfile());
                    }

                    using (var tempFile = new TemporaryFile(Files.PictureJPG))
                    {
                        var result = Optimizer.Compress(tempFile);

                        Assert.True(result);

                        using (var image = new MagickImage())
                        {
                            image.Ping(tempFile);

                            Assert.NotNull(image.GetColorProfile());
                        }
                    }
                }

                [Fact]
                public void ShouldNotPreserveTheExifProfile()
                {
                    using (var image = new MagickImage())
                    {
                        image.Ping(Files.PictureJPG);

                        Assert.NotNull(image.GetExifProfile());
                    }

                    using (var tempFile = new TemporaryFile(Files.PictureJPG))
                    {
                        var result = Optimizer.Compress(tempFile);

                        Assert.True(result);

                        using (var image = new MagickImage())
                        {
                            image.Ping(tempFile);

                            Assert.Null(image.GetExifProfile());
                        }
                    }
                }
            }

            public class WithFileName : TheCompressMethod
            {
                [Fact]
                public void ShouldThrowExceptionWhenFileNameIsNull()
                {
                    Assert.Throws<ArgumentNullException>("fileName", () => Optimizer.Compress((string)null));
                }

                [Fact]
                public void ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    Assert.Throws<ArgumentException>("fileName", () => Optimizer.Compress(string.Empty));
                }

                [Fact]
                public void ShouldThrowExceptionWhenFileNameIsInvalid()
                {
                    var exception = Assert.Throws<MagickCorruptImageErrorException>(() =>
                    {
                        Optimizer.Compress(Files.Missing);
                    });

                    Assert.Contains("Input file read error", exception.Message);
                }

                [Fact]
                public void ShouldCompressUTF8PathName()
                {
                    using (var tempDir = new TemporaryDirectory("爱"))
                    {
                        string tempFile = Path.Combine(tempDir.FullName, "ImageMagick.jpg");
                        FileHelper.Copy(Files.ImageMagickJPG, tempFile);

                        Optimizer.Compress(tempFile);
                    }
                }
            }

            public class WithStreamName : TheCompressMethod
            {
                [Fact]
                public void ShouldThrowExceptionWhenStreamIsNull()
                {
                    Assert.Throws<ArgumentNullException>("stream", () => Optimizer.Compress((Stream)null));
                }

                [Fact]
                public void ShouldThrowExceptionWhenStreamIsNotReadable()
                {
                    using (var stream = new TestStream(false, true, true))
                    {
                        Assert.Throws<ArgumentException>("stream", () => Optimizer.Compress(stream));
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenStreamIsNotWriteable()
                {
                    using (var stream = new TestStream(true, false, true))
                    {
                        Assert.Throws<ArgumentException>("stream", () => Optimizer.Compress(stream));
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenStreamIsNotSeekable()
                {
                    using (var stream = new TestStream(true, true, false))
                    {
                        Assert.Throws<ArgumentException>("stream", () => Optimizer.Compress(stream));
                    }
                }
            }
        }
    }
}
