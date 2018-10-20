using System.IO;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests.Shared.Optimizers.ImageOptimizerTests
{
    public partial class ImageOptimizerTests
    {
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
                    ExceptionAssert.ThrowsArgumentException("fileName", () =>
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
                    Assert.IsFalse(optimizer.IsSupported(Files.InvitationTif));
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
