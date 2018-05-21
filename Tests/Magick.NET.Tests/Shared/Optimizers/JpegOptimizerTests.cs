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
using ImageMagick.ImageOptimizers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests.Optimizers
{
    [TestClass]
    public class JpegOptimizerTests : ImageOptimizerTestHelper<JpegOptimizer>
    {
        [TestMethod]
        public void OptimalCompression_DefaultIsFalse()
        {
            Assert.IsFalse(Optimizer.OptimalCompression);
        }

        [TestMethod]
        public void Compress_FileIsNull_ThrowsException()
        {
            ExceptionAssert.ThrowsArgumentNullException("file", () =>
            {
                Optimizer.Compress((FileInfo)null);
            });
        }

        [TestMethod]
        public void Compress_FileNameIsNull_ThrowsException()
        {
            ExceptionAssert.ThrowsArgumentNullException("fileName", () =>
            {
                Optimizer.Compress((string)null);
            });
        }

        [TestMethod]
        public void Compress_FileNameIsEmpty_ThrowsException()
        {
            ExceptionAssert.ThrowsArgumentException("fileName", () =>
            {
                Optimizer.Compress(string.Empty);
            });
        }

        [TestMethod]
        public void Compress_FileNameIsInvalid_ThrowsException()
        {
            ExceptionAssert.Throws<MagickCorruptImageErrorException>(() =>
            {
                Optimizer.Compress(Files.Missing);
            }, "Unable to decompress the jpeg file.");
        }

        [TestMethod]
        public void Compress_InvalidFile_ThrowsException()
        {
            ExceptionAssert.Throws<MagickCorruptImageErrorException>(() =>
            {
                Optimizer.Compress(Files.InvitationTif);
            });
        }

        [TestMethod]
        public void Compress_StreamIsNull_ThrowsException()
        {
            ExceptionAssert.ThrowsArgumentNullException("stream", () =>
            {
                Optimizer.Compress((Stream)null);
            });
        }

        [TestMethod]
        public void Compress_StreamCannotRead_ThrowsException()
        {
            using (TestStream stream = new TestStream(false, true, true))
            {
                ExceptionAssert.ThrowsArgumentException("stream", () =>
                {
                    Optimizer.Compress(stream);
                });
            }
        }

        [TestMethod]
        public void Compress_StreamCannotWrite_ThrowsException()
        {
            using (TestStream stream = new TestStream(true, false, true))
            {
                ExceptionAssert.ThrowsArgumentException("stream", () =>
                {
                    Optimizer.Compress(stream);
                });
            }
        }

        [TestMethod]
        public void Compress_StreamCannotSeek_ThrowsException()
        {
            using (TestStream stream = new TestStream(true, true, false))
            {
                ExceptionAssert.ThrowsArgumentException("stream", () =>
                {
                    Optimizer.Compress(stream);
                });
            }
        }

        [TestMethod]
        public void Compress_CanCompress_FileIsSmaller()
        {
            AssertCompressSmaller(Files.ImageMagickJPG);
        }

        [TestMethod]
        public void Compress_CannotCompress_FileIsNotSmaller()
        {
            AssertCompressNotSmaller(Files.LetterJPG);
        }

        [TestMethod]
        public void Compress_CanCompress_CanBeCalledTwice()
        {
            AssertCompressTwice(Files.ImageMagickJPG);
        }

        [TestMethod]
        public void LosslessCompress_FileIsNull_ThrowsException()
        {
            ExceptionAssert.ThrowsArgumentNullException("file", () =>
            {
                Optimizer.LosslessCompress((FileInfo)null);
            });
        }

        [TestMethod]
        public void Compress_WithQualitySetTo40_FileIsSmallerThanNormalCompression()
        {
            using (TemporaryFile tempFile = new TemporaryFile(Files.ImageMagickJPG))
            {
                JpegOptimizer optimizer = new JpegOptimizer();
                optimizer.Compress(tempFile);

                IMagickImageInfo info = new MagickImageInfo(tempFile);
                Assert.AreEqual(85, info.Quality);

                File.Copy(Files.ImageMagickJPG, tempFile.FullName, true);

                optimizer.Compress(tempFile, 40);

                info = new MagickImageInfo(tempFile);
                Assert.AreEqual(40, info.Quality);
            }
        }

        [TestMethod]
        public void Compress_UTF8PathName_CanCompressFile()
        {
            using (TemporaryDirectory tempDir = new TemporaryDirectory("爱"))
            {
                string tempFile = Path.Combine(tempDir.FullName, "ImageMagick.jpg");
                File.Copy(Files.ImageMagickJPG, tempFile);

                Optimizer.Compress(tempFile);
            }
        }

        [TestMethod]
        public void Compress_ShouldPreserveTheColorProfile()
        {
            using (MagickImage image = new MagickImage())
            {
                image.Ping(Files.PictureJPG);

                Assert.IsNotNull(image.GetColorProfile());
            }

            using (TemporaryFile tempFile = new TemporaryFile(Files.PictureJPG))
            {
                var result = Optimizer.Compress(tempFile);

                Assert.IsTrue(result);

                using (MagickImage image = new MagickImage())
                {
                    image.Ping(tempFile);

                    Assert.IsNotNull(image.GetColorProfile());
                }
            }
        }

        [TestMethod]
        public void Compress_ShouldNotPreserveTheExifProfile()
        {
            using (MagickImage image = new MagickImage())
            {
                image.Ping(Files.PictureJPG);

                Assert.IsNotNull(image.GetExifProfile());
            }

            using (TemporaryFile tempFile = new TemporaryFile(Files.PictureJPG))
            {
                var result = Optimizer.Compress(tempFile);

                Assert.IsTrue(result);

                using (MagickImage image = new MagickImage())
                {
                    image.Ping(tempFile);

                    Assert.IsNull(image.GetExifProfile());
                }
            }
        }

        [TestMethod]
        public void LosslessCompress_FileNameIsNull_ThrowsException()
        {
            ExceptionAssert.ThrowsArgumentNullException("fileName", () =>
            {
                Optimizer.LosslessCompress((string)null);
            });
        }

        [TestMethod]
        public void LosslessCompress_FileNameIsEmpty_ThrowsException()
        {
            ExceptionAssert.ThrowsArgumentException("fileName", () =>
            {
                Optimizer.LosslessCompress(string.Empty);
            });
        }

        [TestMethod]
        public void LosslessCompress_FileNameIsInvalid_ThrowsException()
        {
            ExceptionAssert.Throws<MagickCorruptImageErrorException>(() =>
            {
                Optimizer.LosslessCompress(Files.Missing);
            }, "Unable to decompress the jpeg file.");
        }

        [TestMethod]
        public void LosslessCompress_InvalidFile_ThrowsException()
        {
            ExceptionAssert.Throws<MagickCorruptImageErrorException>(() =>
            {
                Optimizer.LosslessCompress(Files.InvitationTif);
            });
        }

        [TestMethod]
        public void LosslessCompress_StreamIsNull_ThrowsException()
        {
            ExceptionAssert.ThrowsArgumentNullException("stream", () =>
            {
                Optimizer.LosslessCompress((Stream)null);
            });
        }

        [TestMethod]
        public void LosslessCompress_StreamCannotRead_ThrowsException()
        {
            using (TestStream stream = new TestStream(false, true, true))
            {
                ExceptionAssert.ThrowsArgumentException("stream", () =>
                {
                    Optimizer.LosslessCompress(stream);
                });
            }
        }

        [TestMethod]
        public void LosslessCompress_StreamCannotWrite_ThrowsException()
        {
            using (TestStream stream = new TestStream(true, false, true))
            {
                ExceptionAssert.ThrowsArgumentException("stream", () =>
                {
                    Optimizer.LosslessCompress(stream);
                });
            }
        }

        [TestMethod]
        public void LosslessCompress_StreamCannotSeek_ThrowsException()
        {
            using (TestStream stream = new TestStream(true, true, false))
            {
                ExceptionAssert.ThrowsArgumentException("stream", () =>
                {
                    Optimizer.LosslessCompress(stream);
                });
            }
        }

        [TestMethod]
        public void LosslessCompress_CanCompress_FileIsSmaller()
        {
            AssertLosslessCompressSmaller(Files.ImageMagickJPG);
        }

        [TestMethod]
        public void LosslessCompress_CannotCompress_FileIsNotSmaller()
        {
            AssertLosslessCompressNotSmaller(Files.LetterJPG);
        }

        [TestMethod]
        public void LosslessCompress_CanCompress_CanBeCalledTwice()
        {
            AssertCompressTwice(Files.ImageMagickJPG);
        }

        [TestMethod]
        public void LosslessCompress_CompareWithCompress_CompressIsSmallerThanLosslessCompress()
        {
            long compress = AssertCompressSmaller(Files.ImageMagickJPG);
            long losslessCompress = AssertLosslessCompressSmaller(Files.ImageMagickJPG);

            Assert.IsTrue(compress < losslessCompress, "{0} is not smaller than {1}", compress, losslessCompress);
        }

        [TestMethod]
        public void LosslessCompress_ShouldPreserveTheColorProfile()
        {
            using (MagickImage image = new MagickImage())
            {
                image.Ping(Files.PictureJPG);

                Assert.IsNotNull(image.GetColorProfile());
            }

            using (TemporaryFile tempFile = new TemporaryFile(Files.PictureJPG))
            {
                var result = Optimizer.LosslessCompress(tempFile);

                Assert.IsTrue(result);

                using (MagickImage image = new MagickImage())
                {
                    image.Ping(tempFile);

                    Assert.IsNotNull(image.GetColorProfile());
                }
            }
        }

        [TestMethod]
        public void LosslessCompress_ShouldPreserveTheExifProfile()
        {
            using (MagickImage image = new MagickImage())
            {
                image.Ping(Files.PictureJPG);

                Assert.IsNotNull(image.GetExifProfile());
            }

            using (TemporaryFile tempFile = new TemporaryFile(Files.PictureJPG))
            {
                var result = Optimizer.LosslessCompress(tempFile);

                Assert.IsTrue(result);

                using (MagickImage image = new MagickImage())
                {
                    image.Ping(tempFile);

                    Assert.IsNotNull(image.GetExifProfile());
                }
            }
        }
    }
}
