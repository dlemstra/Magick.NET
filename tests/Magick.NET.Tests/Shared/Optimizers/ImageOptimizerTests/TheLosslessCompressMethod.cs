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
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests.Shared.Optimizers.ImageOptimizerTests
{
    public partial class ImageOptimizerTests
    {
        public class TheLosslessCompressMethod
        {
            [TestClass]
            public class WithFile : ImageOptimizerTestHelper
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenFileIsNull()
                {
                    var optimizer = new ImageOptimizer();
                    ExceptionAssert.Throws<ArgumentNullException>("file", () =>
                    {
                        optimizer.LosslessCompress((FileInfo)null);
                    });
                }

                [TestMethod]
                public void ShouldReturnFalseWhenFileIsEmpty()
                {
                    var optimizer = new ImageOptimizer();
                    using (TemporaryFile file = new TemporaryFile("empty"))
                    {
                        var result = optimizer.LosslessCompress(file);
                        Assert.IsFalse(result);
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenFileIsUnsupportedFormat()
                {
                    var optimizer = new ImageOptimizer();
                    ExceptionAssert.Throws<MagickCorruptImageErrorException>(() =>
                    {
                        optimizer.LosslessCompress(new FileInfo(Files.InvitationTIF));
                    }, "Invalid format");
                }

                [TestMethod]
                public void ShouldNotThrowExceptionWhenIgnoringUnsupportedFileName()
                {
                    var optimizer = new ImageOptimizer { IgnoreUnsupportedFormats = true };
                    var compressionSuccess = optimizer.LosslessCompress(new FileInfo(Files.InvitationTIF));
                    Assert.IsFalse(compressionSuccess);
                }

                [TestMethod]
                public void ShouldMakeFileSmallerWhenFileIsCompressibleJpgFile()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.ImageMagickJPG, true, (FileInfo file) =>
                    {
                        return optimizer.LosslessCompress(file);
                    });
                }

                [TestMethod]
                public void ShouldMakeFileSmallerWhenFileIsCompressiblePngFile()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.SnakewarePNG, true, (FileInfo file) =>
                    {
                        return optimizer.LosslessCompress(file);
                    });
                }

                [TestMethod]
                public void ShouldMakeFileSmallerWhenFileIsCompressibleIcoFile()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.WandICO, true, (FileInfo file) =>
                    {
                        return optimizer.LosslessCompress(file);
                    });
                }

                [TestMethod]
                public void ShouldMakeFileSmallerWhenFileIsCompressibleGifFile()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.FujiFilmFinePixS1ProGIF, true, (FileInfo file) =>
                    {
                        return optimizer.LosslessCompress(file);
                    });
                }

                [TestMethod]
                public void ShouldNotMakeFileSmallerWhenFileIsUnCompressibleJpgFile()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.LetterJPG, false, (FileInfo file) =>
                    {
                        return optimizer.LosslessCompress(file);
                    });
                }

                [TestMethod]
                public void ShouldNotMakeFileSmallerWhenFileIsUnCompressiblePngFile()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.MagickNETIconPNG, false, (FileInfo file) =>
                    {
                        return optimizer.LosslessCompress(file);
                    });
                }

                [TestMethod]
                public void ShouldNotMakeFileSmallerWhenFileIsUnCompressibleIcoFile()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.ImageMagickICO, false, (FileInfo file) =>
                    {
                        return optimizer.LosslessCompress(file);
                    });
                }

                [TestMethod]
                public void ShouldNotMakeFileSmallerWhenFileIsUnCompressibleGifFile()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.RoseSparkleGIF, false, (FileInfo file) =>
                    {
                        return optimizer.LosslessCompress(file);
                    });
                }
            }

            [TestClass]
            public class WithFileName : ImageOptimizerTestHelper
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsNull()
                {
                    var optimizer = new ImageOptimizer();
                    ExceptionAssert.Throws<ArgumentNullException>("fileName", () =>
                    {
                        optimizer.LosslessCompress((string)null);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    var optimizer = new ImageOptimizer();
                    ExceptionAssert.Throws<ArgumentException>("fileName", () =>
                    {
                        optimizer.LosslessCompress(string.Empty);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsInvalid()
                {
                    var optimizer = new ImageOptimizer();
                    ExceptionAssert.Throws<MagickBlobErrorException>(() =>
                    {
                        optimizer.LosslessCompress(Files.Missing);
                    }, "error/blob.c/OpenBlob");
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenFileIsUnsupportedFormat()
                {
                    var optimizer = new ImageOptimizer();
                    ExceptionAssert.Throws<MagickCorruptImageErrorException>(() =>
                    {
                        optimizer.Compress(Files.InvitationTIF);
                    }, "Invalid format");
                }

                [TestMethod]
                public void ShouldNotThrowExceptionWhenIgnoringUnsupportedFileName()
                {
                    var optimizer = new ImageOptimizer { IgnoreUnsupportedFormats = true };
                    var compressionSuccess = optimizer.LosslessCompress(Files.InvitationTIF);
                    Assert.IsFalse(compressionSuccess);
                }

                [TestMethod]
                public void ShouldMakeFileSmallerWhenFileNameIsCompressibleJpgFile()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.ImageMagickJPG, true, (string file) =>
                    {
                        return optimizer.LosslessCompress(file);
                    });
                }

                [TestMethod]
                public void ShouldMakeFileSmallerWhenFileNameIsCompressiblePngFile()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.SnakewarePNG, true, (string file) =>
                    {
                        return optimizer.LosslessCompress(file);
                    });
                }

                [TestMethod]
                public void ShouldMakeFileSmallerWhenFileNameIsCompressibleIcoFile()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.WandICO, true, (string file) =>
                    {
                        return optimizer.LosslessCompress(file);
                    });
                }

                [TestMethod]
                public void ShouldMakeFileSmallerWhenFileNameIsCompressibleGifFile()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.FujiFilmFinePixS1ProGIF, true, (string file) =>
                    {
                        return optimizer.LosslessCompress(file);
                    });
                }

                [TestMethod]
                public void ShouldNotMakeFileSmallerWhenFileNameIsUncompressibleJpgFile()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.LetterJPG, false, (string file) =>
                    {
                        return optimizer.LosslessCompress(file);
                    });
                }

                [TestMethod]
                public void ShouldNotMakeFileSmallerWhenFileNameIsUncompressiblePngFile()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.MagickNETIconPNG, false, (string file) =>
                    {
                        return optimizer.LosslessCompress(file);
                    });
                }

                [TestMethod]
                public void ShouldNotMakeFileSmallerWhenFileNameIsUncompressibleIcoFile()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.ImageMagickICO, false, (string file) =>
                    {
                        return optimizer.LosslessCompress(file);
                    });
                }

                [TestMethod]
                public void ShouldNotMakeFileSmallerWhenFileNameIsUncompressibleGifFile()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.RoseSparkleGIF, false, (string file) =>
                    {
                        return optimizer.LosslessCompress(file);
                    });
                }
            }

            [TestClass]
            public class WithStream : ImageOptimizerTestHelper
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenStreamIsNull()
                {
                    var optimizer = new ImageOptimizer();
                    ExceptionAssert.Throws<ArgumentNullException>("stream", () =>
                    {
                        optimizer.LosslessCompress((Stream)null);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenStreamCannotRead()
                {
                    var optimizer = new ImageOptimizer();
                    using (TestStream stream = new TestStream(false, true, true))
                    {
                        ExceptionAssert.Throws<ArgumentException>("stream", () =>
                        {
                            optimizer.LosslessCompress(stream);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenStreamCannotWrite()
                {
                    var optimizer = new ImageOptimizer();
                    using (TestStream stream = new TestStream(true, false, true))
                    {
                        ExceptionAssert.Throws<ArgumentException>("stream", () =>
                        {
                            optimizer.LosslessCompress(stream);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenStreamCannotSeek()
                {
                    var optimizer = new ImageOptimizer();
                    using (TestStream stream = new TestStream(true, true, false))
                    {
                        ExceptionAssert.Throws<ArgumentException>("stream", () =>
                        {
                            optimizer.LosslessCompress(stream);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenStreamIsUnsupportedFormat()
                {
                    var optimizer = new ImageOptimizer();
                    using (FileStream fileStream = OpenStream(Files.InvitationTIF))
                    {
                        ExceptionAssert.Throws<MagickCorruptImageErrorException>(() =>
                        {
                            optimizer.Compress(fileStream);
                        }, "Invalid format");
                    }
                }

                [TestMethod]
                public void ShouldNotThrowExceptionWhenIgnoringUnsupportedStream()
                {
                    var optimizer = new ImageOptimizer { IgnoreUnsupportedFormats = true };
                    using (FileStream fileStream = OpenStream(Files.InvitationTIF))
                    {
                        var compressionSuccess = optimizer.LosslessCompress(fileStream);
                        Assert.IsFalse(compressionSuccess);
                    }
                }

                [TestMethod]
                public void ShouldMakeFileSmallerWhenStreamIsCompressibleJpgStrea()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.ImageMagickJPG, true, (Stream stream) =>
                    {
                        return optimizer.LosslessCompress(stream);
                    });
                }

                [TestMethod]
                public void ShouldMakeFileSmallerWhenStreamIsCompressiblePngStream()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.SnakewarePNG, true, (Stream stream) =>
                    {
                        return optimizer.LosslessCompress(stream);
                    });
                }

                [TestMethod]
                public void ShouldMakeFileSmallerWhenStreamIsCompressibleIcoStream()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.WandICO, true, (Stream stream) =>
                    {
                        return optimizer.LosslessCompress(stream);
                    });
                }

                [TestMethod]
                public void ShouldMakeFileSmallerWhenStreamIsCompressibleGifStream()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.FujiFilmFinePixS1ProGIF, true, (Stream stream) =>
                    {
                        return optimizer.LosslessCompress(stream);
                    });
                }

                [TestMethod]
                public void ShouldNotMakeFileSmallerWhenStreamIsCompressibleJpgStream()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.LetterJPG, false, (Stream stream) =>
                    {
                        return optimizer.LosslessCompress(stream);
                    });
                }

                [TestMethod]
                public void ShouldNotMakeFileSmallerWhenStreamIsCompressiblePngStream()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.MagickNETIconPNG, false, (Stream stream) =>
                    {
                        return optimizer.LosslessCompress(stream);
                    });
                }

                [TestMethod]
                public void ShouldNotMakeFileSmallerWhenStreamIsCompressibleIcoStream()
                {
                    var optimizer = new ImageOptimizer();
                    AssertCompress(Files.ImageMagickICO, false, (Stream stream) =>
                    {
                        return optimizer.LosslessCompress(stream);
                    });
                }

                [TestMethod]
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