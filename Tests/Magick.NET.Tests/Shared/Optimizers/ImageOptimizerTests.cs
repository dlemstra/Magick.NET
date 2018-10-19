// Copyright 2013-2018 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

using System.IO;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class ImageOptimizerTests
    {
        private static FileStream OpenFile(string path)
        {
            return File.Open(path, FileMode.Open, FileAccess.ReadWrite);
        }

        [TestClass]
        public class TheCompressMethod
        {
            [TestClass]
            public class WithFile
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenFileIsNull()
                {
                    var optimizer = new ImageOptimizer();
                    ExceptionAssert.ThrowsArgumentNullException("file", () =>
                    {
                        optimizer.Compress((FileInfo)null);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenFileIsEmpty()
                {
                    var optimizer = new ImageOptimizer();
                    using (TemporaryFile file = new TemporaryFile("empty"))
                    {
                        ExceptionAssert.Throws<MagickMissingDelegateErrorException>(() =>
                        {
                            optimizer.Compress(file);
                        });
                    }
                }

                [TestMethod]
                public void ShouldMakeFileSmallerWhenFileIsCompressibleJpgFile()
                {
                    var optimizer = new ImageOptimizer();
                    ImageOptimizerTestHelper.AssertCompress(Files.ImageMagickJPG, true, (FileInfo file) =>
                    {
                        return optimizer.Compress(file);
                    });
                }

                [TestMethod]
                public void ShouldNotMakeFileSmallerWhenFileIsUncompressibleJpgFile()
                {
                    var optimizer = new ImageOptimizer();
                    ImageOptimizerTestHelper.AssertCompress(Files.LetterJPG, false, (FileInfo file) =>
                    {
                        return optimizer.Compress(file);
                    });
                }
            }

            [TestClass]
            public class WithFileName
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsNull()
                {
                    var optimizer = new ImageOptimizer();
                    ExceptionAssert.ThrowsArgumentNullException("fileName", () =>
                    {
                        optimizer.Compress((string)null);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    var optimizer = new ImageOptimizer();
                    ExceptionAssert.ThrowsArgumentNullException("fileName", () =>
                    {
                        optimizer.Compress(string.Empty);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsInvalid()
                {
                    var optimizer = new ImageOptimizer();
                    ExceptionAssert.Throws<MagickBlobErrorException>(() =>
                    {
                        optimizer.Compress(Files.Missing);
                    }, "error/blob.c/OpenBlob");
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenFileIsUnsupportedFormat()
                {
                    var optimizer = new ImageOptimizer();
                    ExceptionAssert.Throws<MagickCorruptImageErrorException>(() =>
                    {
                        optimizer.Compress(Files.InvitationTif);
                    }, "Invalid format");
                }

                [TestMethod]
                public void ShouldMakeFileSmallerWhenFileNameIsCompressibleGifFile()
                {
                    var optimizer = new ImageOptimizer();
                    ImageOptimizerTestHelper.AssertCompress(Files.FujiFilmFinePixS1ProGIF, true, (string file) =>
                    {
                        return optimizer.Compress(file);
                    });
                }

                [TestMethod]
                public void ShouldNotMakeFileSmallerWhenFileNameIsUncompressiblePngFile()
                {
                    var optimizer = new ImageOptimizer();
                    ImageOptimizerTestHelper.AssertCompress(Files.MagickNETIconPNG, false, (string file) =>
                    {
                        return optimizer.Compress(file);
                    });
                }

                [TestMethod]
                public void ShouldNotThrowExceptionWhenIgnoringUnsupportedFileName()
                {
                    var optimizer = new ImageOptimizer { IgnoreUnsupportedFormats = true };
                    var compressionSuccess = optimizer.Compress(Files.InvitationTif);
                    Assert.IsFalse(compressionSuccess);
                }
            }

            [TestClass]
            public class WithStream
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenStreamIsNull()
                {
                    var optimizer = new ImageOptimizer();
                    ExceptionAssert.ThrowsArgumentNullException("stream", () =>
                    {
                        optimizer.Compress((Stream)null);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenStreamCannotRead()
                {
                    var optimizer = new ImageOptimizer();
                    using (TestStream stream = new TestStream(false, true, true))
                    {
                        ExceptionAssert.ThrowsArgumentException("stream", () =>
                        {
                            optimizer.Compress(stream);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenStreamCannotWrite()
                {
                    var optimizer = new ImageOptimizer();
                    using (TestStream stream = new TestStream(true, false, true))
                    {
                        ExceptionAssert.ThrowsArgumentException("stream", () =>
                        {
                            optimizer.Compress(stream);
                        });
                    }
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenStreamCannotSeek()
                {
                    var optimizer = new ImageOptimizer();
                    using (TestStream stream = new TestStream(true, true, false))
                    {
                        ExceptionAssert.ThrowsArgumentException("stream", () =>
                        {
                            optimizer.Compress(stream);
                        });
                    }
                }

                [TestMethod]
                public void ShouldMakeFileSmallerWhenStreamIsCompressiblePngFile()
                {
                    var optimizer = new ImageOptimizer();
                    ImageOptimizerTestHelper.AssertCompress(Files.SnakewarePNG, true, (Stream file) =>
                    {
                        return optimizer.Compress(file);
                    });
                }

                [TestMethod]
                public void ShouldNotMakeFileSmallerWhenStreamIsUncompressibleGifFile()
                {
                    var optimizer = new ImageOptimizer();
                    ImageOptimizerTestHelper.AssertCompress(Files.RoseSparkleGIF, false, (Stream file) =>
                    {
                        return optimizer.Compress(file);
                    });
                }

                [TestMethod]
                public void ShouldNotThrowExceptionWhenIgnoringUnsupportedStream()
                {
                    var optimizer = new ImageOptimizer { IgnoreUnsupportedFormats = true };
                    using (FileStream fileStream = OpenFile(Files.InvitationTif))
                    {
                        var compressionSuccess = optimizer.Compress(fileStream);
                        Assert.IsFalse(compressionSuccess);
                    }
                }
            }
        }

        [TestClass]
        public class TheLosslessCompressMethod
        {
            [TestClass]
            public class WithFile
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenFileIsNull()
                {
                    var optimizer = new ImageOptimizer();
                    ExceptionAssert.ThrowsArgumentNullException("file", () =>
                    {
                        optimizer.LosslessCompress((FileInfo)null);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenFileIsEmpty()
                {
                    var optimizer = new ImageOptimizer();
                    using (TemporaryFile file = new TemporaryFile("empty"))
                    {
                        ExceptionAssert.Throws<MagickMissingDelegateErrorException>(() =>
                        {
                            optimizer.LosslessCompress(file);
                        });
                    }
                }

                [TestMethod]
                public void ShouldMakeFileSmallerWhenFileIsCompressibleGifFile()
                {
                    var optimizer = new ImageOptimizer();
                    ImageOptimizerTestHelper.AssertCompress(Files.FujiFilmFinePixS1ProGIF, true, (FileInfo file) =>
                    {
                        return optimizer.LosslessCompress(file);
                    });
                }

                [TestMethod]
                public void ShouldNotMakeFileSmallerWhenFileIsUnCompressibleGifFile()
                {
                    var optimizer = new ImageOptimizer();
                    ImageOptimizerTestHelper.AssertCompress(Files.RoseSparkleGIF, false, (FileInfo file) =>
                    {
                        return optimizer.LosslessCompress(file);
                    });
                }
            }

            [TestClass]
            public class WithFileName
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsNull()
                {
                    var optimizer = new ImageOptimizer();
                    ExceptionAssert.ThrowsArgumentNullException("fileName", () =>
                    {
                        optimizer.LosslessCompress((string)null);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    var optimizer = new ImageOptimizer();
                    ExceptionAssert.ThrowsArgumentNullException("fileName", () =>
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
                        optimizer.LosslessCompress(Files.InvitationTif);
                    }, "Invalid format");
                }

                [TestMethod]
                public void ShouldMakeFileSmallerWhenFileNameIsCompressibleJpgFile()
                {
                    var optimizer = new ImageOptimizer();
                    ImageOptimizerTestHelper.AssertCompress(Files.ImageMagickJPG, true, (string file) =>
                    {
                        return optimizer.LosslessCompress(file);
                    });
                }

                [TestMethod]
                public void ShouldNotMakeFileSmallerWhenFileNameIsUncompressiblePngFile()
                {
                    var optimizer = new ImageOptimizer();
                    ImageOptimizerTestHelper.AssertCompress(Files.MagickNETIconPNG, false, (string file) =>
                    {
                        return optimizer.LosslessCompress(file);
                    });
                }

                [TestMethod]
                public void ShouldNotThrowExceptionWhenIgnoringUnsupportedFileName()
                {
                    var optimizer = new ImageOptimizer { IgnoreUnsupportedFormats = true };
                    var compressionSuccess = optimizer.LosslessCompress(Files.InvitationTif);
                    Assert.IsFalse(compressionSuccess);
                }
            }

            [TestClass]
            public class WithStream
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenStreamIsNull()
                {
                    var optimizer = new ImageOptimizer();
                    ExceptionAssert.ThrowsArgumentNullException("stream", () =>
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
                        ExceptionAssert.ThrowsArgumentException("stream", () =>
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
                        ExceptionAssert.ThrowsArgumentException("stream", () =>
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
                        ExceptionAssert.ThrowsArgumentException("stream", () =>
                        {
                            optimizer.LosslessCompress(stream);
                        });
                    }
                }

                [TestMethod]
                public void ShouldMakeFileSmallerWhenStreamIsCompressiblePngFile()
                {
                    var optimizer = new ImageOptimizer();
                    ImageOptimizerTestHelper.AssertCompress(Files.SnakewarePNG, true, (Stream file) =>
                    {
                        return optimizer.LosslessCompress(file);
                    });
                }

                [TestMethod]
                public void ShouldNotMakeFileSmallerWhenStreamIsCompressibleJpgFile()
                {
                    var optimizer = new ImageOptimizer();
                    ImageOptimizerTestHelper.AssertCompress(Files.LetterJPG, false, (Stream file) =>
                    {
                        return optimizer.LosslessCompress(file);
                    });
                }

                [TestMethod]
                public void ShouldNotThrowExceptionWhenIgnoringUnsupportedStream()
                {
                    var optimizer = new ImageOptimizer { IgnoreUnsupportedFormats = true };
                    using (FileStream fileStream = OpenFile(Files.InvitationTif))
                    {
                        var compressionSuccess = optimizer.LosslessCompress(fileStream);
                        Assert.IsFalse(compressionSuccess);
                    }
                }
            }
        }

        [TestClass]
        public class TheIsSupportedMethod
        {
            [TestClass]
            public class WithFile
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenFileIsNull()
                {
                    var optimizer = new ImageOptimizer();
                    ExceptionAssert.ThrowsArgumentNullException("file", () =>
                    {
                        optimizer.IsSupported((FileInfo)null);
                    });
                }

                [TestMethod]
                public void ShouldReturnTrueWhenFileIsMissingPngFile()
                {
                    var optimizer = new ImageOptimizer();
                    Assert.IsTrue(optimizer.IsSupported(new FileInfo(Files.Missing)));
                }

                [TestMethod]
                public void ShouldReturnTrueWhenFileIsFileIsGifFile()
                {
                    var optimizer = new ImageOptimizer();
                    Assert.IsTrue(optimizer.IsSupported(new FileInfo(Files.FujiFilmFinePixS1ProGIF)));
                }

                [TestMethod]
                public void ShouldReturnTrueWhenFileIsFileIsJpgFile()
                {
                    var optimizer = new ImageOptimizer();
                    Assert.IsTrue(optimizer.IsSupported(new FileInfo(Files.ImageMagickJPG)));
                }

                [TestMethod]
                public void ShouldReturnTrueWhenFileIsFileIsPngFile()
                {
                    var optimizer = new ImageOptimizer();
                    Assert.IsTrue(optimizer.IsSupported(new FileInfo(Files.SnakewarePNG)));
                }

                [TestMethod]
                public void ShouldReturnFalseWhenFileIsFileIsTifFile()
                {
                    var optimizer = new ImageOptimizer();
                    Assert.IsFalse(optimizer.IsSupported(new FileInfo(Files.InvitationTif)));
                }
            }

            [TestClass]
            public class WithFileName
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsNull()
                {
                    var optimizer = new ImageOptimizer();
                    ExceptionAssert.ThrowsArgumentNullException("fileName", () =>
                    {
                        optimizer.IsSupported((string)null);
                    });
                }

                [TestMethod]
                public void ShouldThrowExceptionWhenFileNameIsEmpty()
                {
                    var optimizer = new ImageOptimizer();
                    ExceptionAssert.ThrowsArgumentNullException("fileName", () =>
                    {
                        optimizer.IsSupported(string.Empty);
                    });
                }

                [TestMethod]
                public void ShouldReturnFalseWhenFileNameIsInvalid()
                {
                    var optimizer = new ImageOptimizer();
                    Assert.IsFalse(optimizer.IsSupported("invalid"));
                }

                [TestMethod]
                public void ShouldReturnTrueWhenFileNameIsMissingPngFile()
                {
                    var optimizer = new ImageOptimizer();
                    Assert.IsTrue(optimizer.IsSupported(Files.Missing));
                }

                [TestMethod]
                public void ShouldReturnTrueWhenFileNameIsGifFile()
                {
                    var optimizer = new ImageOptimizer();
                    Assert.IsTrue(optimizer.IsSupported(Files.FujiFilmFinePixS1ProGIF));
                }

                [TestMethod]
                public void ShouldReturnTrueWhenFileNameIsJpgFile()
                {
                    var optimizer = new ImageOptimizer();
                    Assert.IsTrue(optimizer.IsSupported(Files.ImageMagickJPG));
                }

                [TestMethod]
                public void ShouldReturnTrueWhenFileNameIsPngFile()
                {
                    var optimizer = new ImageOptimizer();
                    Assert.IsTrue(optimizer.IsSupported(Files.SnakewarePNG));
                }

                [TestMethod]
                public void ShouldReturnFalseWhenFileNameIsTifFile()
                {
                    var optimizer = new ImageOptimizer();
                    Assert.IsTrue(optimizer.IsSupported(Files.InvitationTif));
                }
            }

            [TestClass]
            public class WithStream
            {
                [TestMethod]
                public void ShouldThrowExceptionWhenStreamIsNull()
                {
                    var optimizer = new ImageOptimizer();
                    ExceptionAssert.ThrowsArgumentNullException("stream", () =>
                    {
                        optimizer.IsSupported((Stream)null);
                    });
                }

                [TestMethod]
                public void ShouldReturnFalseWhenStreamCannotRead()
                {
                    var optimizer = new ImageOptimizer();
                    using (TestStream stream = new TestStream(false, true, true))
                    {
                        Assert.IsFalse(optimizer.IsSupported(stream));
                    }
                }

                [TestMethod]
                public void ShouldReturnFalseWhenStreamCannotWrite()
                {
                    var optimizer = new ImageOptimizer();
                    using (TestStream stream = new TestStream(true, false, true))
                    {
                        Assert.IsFalse(optimizer.IsSupported(stream));
                    }
                }

                [TestMethod]
                public void ShouldReturnFalseWhenStreamCannotSeek()
                {
                    var optimizer = new ImageOptimizer();
                    using (TestStream stream = new TestStream(true, true, false))
                    {
                        Assert.IsFalse(optimizer.IsSupported(stream));
                    }
                }

                [TestMethod]
                public void ShouldReturnTrueWhenStreamIsGifFile()
                {
                    var optimizer = new ImageOptimizer();
                    using (FileStream fileStream = OpenFile(Files.FujiFilmFinePixS1ProGIF))
                    {
                        Assert.IsTrue(optimizer.IsSupported(fileStream));
                        Assert.AreEqual(0, fileStream.Position);
                    }
                }

                [TestMethod]
                public void ShouldReturnTrueWhenStreamIsJpgFile()
                {
                    var optimizer = new ImageOptimizer();
                    using (FileStream fileStream = OpenFile(Files.ImageMagickJPG))
                    {
                        Assert.IsTrue(optimizer.IsSupported(fileStream));
                        Assert.AreEqual(0, fileStream.Position);
                    }
                }

                [TestMethod]
                public void ShouldReturnTrueWhenStreamIsPngFile()
                {
                    var optimizer = new ImageOptimizer();
                    using (FileStream fileStream = OpenFile(Files.SnakewarePNG))
                    {
                        Assert.IsTrue(optimizer.IsSupported(fileStream));
                        Assert.AreEqual(0, fileStream.Position);
                    }
                }

                [TestMethod]
                public void ShouldReturnFalseWhenStreamIsTifFile()
                {
                    var optimizer = new ImageOptimizer();
                    using (FileStream fileStream = OpenFile(Files.InvitationTif))
                    {
                        Assert.IsFalse(optimizer.IsSupported(fileStream));
                        Assert.AreEqual(0, fileStream.Position);
                    }
                }
            }
        }
    }
}
