// Copyright 2013-2020 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://www.imagemagick.org/script/license.php
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
        public class TheLosslessCompressMethod
        {
            public class WithFile : ImageOptimizerTestHelper
            {
                [Fact]
                public void ShouldThrowExceptionWhenFileIsNull()
                {
                    var optimizer = new ImageOptimizer();
                    Assert.Throws<ArgumentNullException>("file", () =>
                    {
                        optimizer.LosslessCompress((FileInfo)null);
                    });
                }

                [Fact]
                public void ShouldReturnFalseWhenFileIsEmpty()
                {
                    var optimizer = new ImageOptimizer();
                    using (TemporaryFile file = new TemporaryFile("empty"))
                    {
                        var result = optimizer.LosslessCompress(file);
                        Assert.False(result);
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenFileIsUnsupportedFormat()
                {
                    var optimizer = new ImageOptimizer();
                    var exception = Assert.Throws<MagickCorruptImageErrorException>(() =>
                    {
                        optimizer.LosslessCompress(new FileInfo(Files.InvitationTIF));
                    });

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
                    AssertCompress(Files.ImageMagickJPG, true, (FileInfo file) =>
                    {
                        return optimizer.LosslessCompress(file);
                    });
                }

                [Fact]
                public void ShouldMakeFileSmallerWhenFileIsCompressiblePngFile()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.SnakewarePNG, true, (FileInfo file) =>
                    {
                        return optimizer.LosslessCompress(file);
                    });
                }

                [Fact]
                public void ShouldMakeFileSmallerWhenFileIsCompressibleIcoFile()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.WandICO, true, (FileInfo file) =>
                    {
                        return optimizer.LosslessCompress(file);
                    });
                }

                [Fact]
                public void ShouldMakeFileSmallerWhenFileIsCompressibleGifFile()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.FujiFilmFinePixS1ProGIF, true, (FileInfo file) =>
                    {
                        return optimizer.LosslessCompress(file);
                    });
                }

                [Fact]
                public void ShouldNotMakeFileSmallerWhenFileIsUnCompressibleJpgFile()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.LetterJPG, false, (FileInfo file) =>
                    {
                        return optimizer.LosslessCompress(file);
                    });
                }

                [Fact]
                public void ShouldNotMakeFileSmallerWhenFileIsUnCompressiblePngFile()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.MagickNETIconPNG, false, (FileInfo file) =>
                    {
                        return optimizer.LosslessCompress(file);
                    });
                }

                [Fact]
                public void ShouldNotMakeFileSmallerWhenFileIsUnCompressibleIcoFile()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.ImageMagickICO, false, (FileInfo file) =>
                    {
                        return optimizer.LosslessCompress(file);
                    });
                }

                [Fact]
                public void ShouldNotMakeFileSmallerWhenFileIsUnCompressibleGifFile()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.RoseSparkleGIF, false, (FileInfo file) =>
                    {
                        return optimizer.LosslessCompress(file);
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
                        optimizer.LosslessCompress((string)null);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    var optimizer = new ImageOptimizer();
                    Assert.Throws<ArgumentException>("fileName", () =>
                    {
                        optimizer.LosslessCompress(string.Empty);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenFileNameIsInvalid()
                {
                    var optimizer = new ImageOptimizer();
                    var exception = Assert.Throws<MagickBlobErrorException>(() =>
                    {
                        optimizer.LosslessCompress(Files.Missing);
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
                    var compressionSuccess = optimizer.LosslessCompress(Files.InvitationTIF);
                    Assert.False(compressionSuccess);
                }

                [Fact]
                public void ShouldMakeFileSmallerWhenFileNameIsCompressibleJpgFile()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.ImageMagickJPG, true, (string file) =>
                    {
                        return optimizer.LosslessCompress(file);
                    });
                }

                [Fact]
                public void ShouldMakeFileSmallerWhenFileNameIsCompressiblePngFile()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.SnakewarePNG, true, (string file) =>
                    {
                        return optimizer.LosslessCompress(file);
                    });
                }

                [Fact]
                public void ShouldMakeFileSmallerWhenFileNameIsCompressibleIcoFile()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.WandICO, true, (string file) =>
                    {
                        return optimizer.LosslessCompress(file);
                    });
                }

                [Fact]
                public void ShouldMakeFileSmallerWhenFileNameIsCompressibleGifFile()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.FujiFilmFinePixS1ProGIF, true, (string file) =>
                    {
                        return optimizer.LosslessCompress(file);
                    });
                }

                [Fact]
                public void ShouldNotMakeFileSmallerWhenFileNameIsUncompressibleJpgFile()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.LetterJPG, false, (string file) =>
                    {
                        return optimizer.LosslessCompress(file);
                    });
                }

                [Fact]
                public void ShouldNotMakeFileSmallerWhenFileNameIsUncompressiblePngFile()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.MagickNETIconPNG, false, (string file) =>
                    {
                        return optimizer.LosslessCompress(file);
                    });
                }

                [Fact]
                public void ShouldNotMakeFileSmallerWhenFileNameIsUncompressibleIcoFile()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.ImageMagickICO, false, (string file) =>
                    {
                        return optimizer.LosslessCompress(file);
                    });
                }

                [Fact]
                public void ShouldNotMakeFileSmallerWhenFileNameIsUncompressibleGifFile()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.RoseSparkleGIF, false, (string file) =>
                    {
                        return optimizer.LosslessCompress(file);
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
                        optimizer.LosslessCompress((Stream)null);
                    });
                }

                [Fact]
                public void ShouldThrowExceptionWhenStreamCannotRead()
                {
                    var optimizer = new ImageOptimizer();
                    using (TestStream stream = new TestStream(false, true, true))
                    {
                        Assert.Throws<ArgumentException>("stream", () =>
                        {
                            optimizer.LosslessCompress(stream);
                        });
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenStreamCannotWrite()
                {
                    var optimizer = new ImageOptimizer();
                    using (TestStream stream = new TestStream(true, false, true))
                    {
                        Assert.Throws<ArgumentException>("stream", () =>
                        {
                            optimizer.LosslessCompress(stream);
                        });
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenStreamCannotSeek()
                {
                    var optimizer = new ImageOptimizer();
                    using (TestStream stream = new TestStream(true, true, false))
                    {
                        Assert.Throws<ArgumentException>("stream", () =>
                        {
                            optimizer.LosslessCompress(stream);
                        });
                    }
                }

                [Fact]
                public void ShouldThrowExceptionWhenStreamIsUnsupportedFormat()
                {
                    var optimizer = new ImageOptimizer();
                    using (FileStream fileStream = OpenStream(Files.InvitationTIF))
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
                    using (FileStream fileStream = OpenStream(Files.InvitationTIF))
                    {
                        var compressionSuccess = optimizer.LosslessCompress(fileStream);
                        Assert.False(compressionSuccess);
                    }
                }

                [Fact]
                public void ShouldMakeFileSmallerWhenStreamIsCompressibleJpgStrea()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.ImageMagickJPG, true, (Stream stream) =>
                    {
                        return optimizer.LosslessCompress(stream);
                    });
                }

                [Fact]
                public void ShouldMakeFileSmallerWhenStreamIsCompressiblePngStream()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.SnakewarePNG, true, (Stream stream) =>
                    {
                        return optimizer.LosslessCompress(stream);
                    });
                }

                [Fact]
                public void ShouldMakeFileSmallerWhenStreamIsCompressibleIcoStream()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.WandICO, true, (Stream stream) =>
                    {
                        return optimizer.LosslessCompress(stream);
                    });
                }

                [Fact]
                public void ShouldMakeFileSmallerWhenStreamIsCompressibleGifStream()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.FujiFilmFinePixS1ProGIF, true, (Stream stream) =>
                    {
                        return optimizer.LosslessCompress(stream);
                    });
                }

                [Fact]
                public void ShouldNotMakeFileSmallerWhenStreamIsCompressibleJpgStream()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.LetterJPG, false, (Stream stream) =>
                    {
                        return optimizer.LosslessCompress(stream);
                    });
                }

                [Fact]
                public void ShouldNotMakeFileSmallerWhenStreamIsCompressiblePngStream()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.MagickNETIconPNG, false, (Stream stream) =>
                    {
                        return optimizer.LosslessCompress(stream);
                    });
                }

                [Fact]
                public void ShouldNotMakeFileSmallerWhenStreamIsCompressibleIcoStream()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.ImageMagickICO, false, (Stream stream) =>
                    {
                        return optimizer.LosslessCompress(stream);
                    });
                }

                [Fact]
                public void ShouldNotMakeFileSmallerWhenStreamIsCompressibleGifStream()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.RoseSparkleGIF, false, (Stream stream) =>
                    {
                        return optimizer.LosslessCompress(stream);
                    });
                }
            }
        }
    }
}