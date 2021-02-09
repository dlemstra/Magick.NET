// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using System;
using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class ImageOptimizerTests
    {
        public class TheCompressMethod
        {
            public class WithFile : ImageOptimizerTestHelper
            {
                [Fact]
                public void ShouldThrowExceptionWhenFileIsNull()
                {
                    var optimizer = new ImageOptimizer();
                    Assert.Throws<ArgumentNullException>("file", () =>
                    {
                        optimizer.Compress((FileInfo)null);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenFileIsEmpty()
                {
                    var optimizer = new ImageOptimizer();
                    using (var file = new TemporaryFile("empty"))
                    {
                        var result = optimizer.Compress(file);
                        Assert.False(result);
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenFileIsUnsupportedFormat()
                {
                    var optimizer = new ImageOptimizer();
                    var exception = Assert.Throws<MagickCorruptImageErrorException>(() =>
                    {
                        optimizer.Compress(new FileInfo(Files.InvitationTIF));
                    });

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
                    AssertCompress(Files.ImageMagickJPG, true, (FileInfo file) =>
                    {
                        return optimizer.Compress(file);
                    });
                }

                [Fact]
                public void ShouldMakeFileSmallerWhenFileIsCompressiblePngFile()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.SnakewarePNG, true, (FileInfo file) =>
                    {
                        return optimizer.Compress(file);
                    });
                }

                [Fact]
                public void ShouldMakeFileSmallerWhenFileIsCompressibleIcoFile()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.WandICO, true, (FileInfo file) =>
                    {
                        return optimizer.Compress(file);
                    });
                }

                [Fact]
                public void ShouldMakeFileSmallerWhenFileIsCompressibleGifFile()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.FujiFilmFinePixS1ProGIF, true, (FileInfo file) =>
                    {
                        return optimizer.Compress(file);
                    });
                }

                [Fact]
                public void ShouldNotMakeFileSmallerWhenFileIsUncompressibleJpgFile()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.LetterJPG, false, (FileInfo file) =>
                    {
                        return optimizer.Compress(file);
                    });
                }

                [Fact]
                public void ShouldNotMakeFileSmallerWhenFileIsUncompressiblePngFile()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.MagickNETIconPNG, false, (FileInfo file) =>
                    {
                        return optimizer.Compress(file);
                    });
                }

                [Fact]
                public void ShouldNotMakeFileSmallerWhenFileIsUncompressibleIcoFile()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.ImageMagickICO, false, (FileInfo file) =>
                    {
                        return optimizer.Compress(file);
                    });
                }

                [Fact]
                public void ShouldNotMakeFileSmallerWhenFileIsUncompressibleGifFile()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.RoseSparkleGIF, false, (FileInfo file) =>
                    {
                        return optimizer.Compress(file);
                    });
                }
            }

            public class WithFileName : ImageOptimizerTestHelper
            {
                [Fact]
                public void ShouldThrowExceptionWhenFileNameIsNull()
                {
                    var optimizer = new ImageOptimizer();
                    Assert.Throws<ArgumentNullException>("fileName", () =>
                    {
                        optimizer.Compress((string)null);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    var optimizer = new ImageOptimizer();
                    Assert.Throws<ArgumentException>("fileName", () =>
                    {
                        optimizer.Compress(string.Empty);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenFileNameIsInvalid()
                {
                    var optimizer = new ImageOptimizer();
                    var exception = Assert.Throws<MagickBlobErrorException>(() =>
                    {
                        optimizer.Compress(Files.Missing);
                    });

                    Assert.Contains("error/blob.c/OpenBlob", exception.Message);
                }

                [Fact]
                public void ShouldThrowExceptionWhenFileIsUnsupportedFormat()
                {
                    var optimizer = new ImageOptimizer();
                    var exception = Assert.Throws<MagickCorruptImageErrorException>(() =>
                    {
                        optimizer.Compress(Files.InvitationTIF);
                    });

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
                    AssertCompress(Files.ImageMagickJPG, true, (string fileName) =>
                    {
                        return optimizer.Compress(fileName);
                    });
                }

                [Fact]
                public void ShouldMakeFileSmallerWhenFileNameIsCompressiblePnggFile()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.SnakewarePNG, true, (string fileName) =>
                    {
                        return optimizer.Compress(fileName);
                    });
                }

                [Fact]
                public void ShouldMakeFileSmallerWhenFileNameIsCompressibleIcoFile()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.WandICO, true, (string fileName) =>
                    {
                        return optimizer.Compress(fileName);
                    });
                }

                [Fact]
                public void ShouldMakeFileSmallerWhenFileNameIsCompressibleGifFile()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.FujiFilmFinePixS1ProGIF, true, (string fileName) =>
                    {
                        return optimizer.Compress(fileName);
                    });
                }

                [Fact]
                public void ShouldNotMakeFileSmallerWhenFileNameIsUncompressibleJpgFile()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.LetterJPG, false, (string fileName) =>
                    {
                        return optimizer.Compress(fileName);
                    });
                }

                [Fact]
                public void ShouldNotMakeFileSmallerWhenFileNameIsUncompressiblePngFile()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.MagickNETIconPNG, false, (string fileName) =>
                    {
                        return optimizer.Compress(fileName);
                    });
                }

                [Fact]
                public void ShouldNotMakeFileSmallerWhenFileNameIsUncompressibleIcoFile()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.ImageMagickICO, false, (string fileName) =>
                    {
                        return optimizer.Compress(fileName);
                    });
                }

                [Fact]
                public void ShouldNotMakeFileSmallerWhenFileNameIsUncompressibleGifFile()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.RoseSparkleGIF, false, (string fileName) =>
                    {
                        return optimizer.Compress(fileName);
                    });
                }
            }

            public class WithStream : ImageOptimizerTestHelper
            {
                [Fact]
                public void ShouldThrowExceptionWhenStreamIsNull()
                {
                    var optimizer = new ImageOptimizer();
                    Assert.Throws<ArgumentNullException>("stream", () =>
                    {
                        optimizer.Compress((Stream)null);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenStreamCannotRead()
                {
                    var optimizer = new ImageOptimizer();
                    using (var stream = new TestStream(false, true, true))
                    {
                        Assert.Throws<ArgumentException>("stream", () =>
                        {
                            optimizer.Compress(stream);
                        });
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenStreamCannotWrite()
                {
                    var optimizer = new ImageOptimizer();
                    using (var stream = new TestStream(true, false, true))
                    {
                        Assert.Throws<ArgumentException>("stream", () =>
                        {
                            optimizer.Compress(stream);
                        });
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenStreamCannotSeek()
                {
                    var optimizer = new ImageOptimizer();
                    using (var stream = new TestStream(true, true, false))
                    {
                        Assert.Throws<ArgumentException>("stream", () =>
                        {
                            optimizer.Compress(stream);
                        });
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenStreamIsUnsupportedFormat()
                {
                    var optimizer = new ImageOptimizer();
                    using (var fileStream = OpenStream(Files.InvitationTIF))
                    {
                        var exception = Assert.Throws<MagickCorruptImageErrorException>(() =>
                        {
                            optimizer.Compress(fileStream);
                        });

                        Assert.Contains("Invalid format", exception.Message);
                    }
                }

                [Fact]
                public void ShouldNotThrowExceptionWhenIgnoringUnsupportedStream()
                {
                    var optimizer = new ImageOptimizer { IgnoreUnsupportedFormats = true };
                    using (var fileStream = OpenStream(Files.InvitationTIF))
                    {
                        var compressionSuccess = optimizer.Compress(fileStream);
                        Assert.False(compressionSuccess);
                    }
                }

                [Fact]
                public void ShouldMakeFileSmallerWhenStreamIsCompressibleJpgFile()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.ImageMagickJPG, true, (Stream stream) =>
                    {
                        return optimizer.Compress(stream);
                    });
                }

                [Fact]
                public void ShouldMakeFileSmallerWhenStreamIsCompressiblePngFile()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.SnakewarePNG, true, (Stream stream) =>
                    {
                        return optimizer.Compress(stream);
                    });
                }

                [Fact]
                public void ShouldMakeFileSmallerWhenStreamIsCompressibleIcoFile()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.WandICO, true, (Stream stream) =>
                    {
                        return optimizer.Compress(stream);
                    });
                }

                [Fact]
                public void ShouldMakeFileSmallerWhenStreamIsCompressibleGifFile()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.FujiFilmFinePixS1ProGIF, true, (Stream stream) =>
                    {
                        return optimizer.Compress(stream);
                    });
                }

                [Fact]
                public void ShouldNotMakeFileSmallerWhenStreamIsUncompressibleJpgFile()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.LetterJPG, false, (Stream stream) =>
                    {
                        return optimizer.Compress(stream);
                    });
                }

                [Fact]
                public void ShouldNotMakeFileSmallerWhenStreamIsUncompressiblePngFile()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.MagickNETIconPNG, false, (Stream stream) =>
                    {
                        return optimizer.Compress(stream);
                    });
                }

                [Fact]
                public void ShouldNotMakeFileSmallerWhenStreamIsUncompressibleIcoFile()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.ImageMagickICO, false, (Stream stream) =>
                    {
                        return optimizer.Compress(stream);
                    });
                }

                [Fact]
                public void ShouldNotMakeFileSmallerWhenStreamIsUncompressibleGifFile()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.RoseSparkleGIF, false, (Stream stream) =>
                    {
                        return optimizer.Compress(stream);
                    });
                }
            }
        }
    }
}
