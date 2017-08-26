// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
    [TestClass]
    public class ImageOptimizerTests : ImageOptimizerTestHelper
    {
        private ImageOptimizer Optimizer => new ImageOptimizer();

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
            ExceptionAssert.Throws<MagickBlobErrorException>("error/blob.c/OpenBlob", () =>
            {
                Optimizer.Compress(Files.Missing);
            });
        }

        [TestMethod]
        public void Compress_InvalidFile_ThrowsException()
        {
            ExceptionAssert.Throws<MagickCorruptImageErrorException>("Invalid format", () =>
            {
                Optimizer.Compress(Files.InvitationTif);
            });
        }

        [TestMethod]
        public void Compress_EmptyFile_ThrowsException()
        {
            using (TemporaryFile file = new TemporaryFile("empty"))
            {
                ExceptionAssert.Throws<MagickMissingDelegateErrorException>(() =>
                {
                    Optimizer.Compress(file);
                });
            }
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
            ExceptionAssert.Throws<MagickBlobErrorException>("error/blob.c/OpenBlob", () =>
            {
                Optimizer.LosslessCompress(Files.Missing);
            });
        }

        [TestMethod]
        public void LosslessCompress_InvalidFile_ThrowsException()
        {
            ExceptionAssert.Throws<MagickCorruptImageErrorException>("Invalid format", () =>
            {
                Optimizer.LosslessCompress(Files.InvitationTif);
            });
        }

        [TestMethod]
        public void LosslessCompress_EmptyFile_ThrowsException()
        {
            using (TemporaryFile file = new TemporaryFile("empty"))
            {
                ExceptionAssert.Throws<MagickMissingDelegateErrorException>(() =>
                {
                    Optimizer.LosslessCompress(file);
                });
            }
        }

        [TestMethod]
        public void IsSupported_FileIsNull_ThrowsException()
        {
            ExceptionAssert.ThrowsArgumentNullException("file", () =>
            {
                Optimizer.IsSupported((FileInfo)null);
            });
        }

        [TestMethod]
        public void IsSupported_FileNameIsNull_ThrowsException()
        {
            ExceptionAssert.ThrowsArgumentNullException("fileName", () =>
            {
                Optimizer.IsSupported((string)null);
            });
        }

        [TestMethod]
        public void IsSupported_FileNameIsEmpty_ThrowsException()
        {
            ExceptionAssert.ThrowsArgumentException("fileName", () =>
            {
                Optimizer.IsSupported(string.Empty);
            });
        }

        [TestMethod]
        public void IsSupported_FileNameIsInvalid_ThrowsException()
        {
            ExceptionAssert.ThrowsArgumentNullException("formatInfo", () =>
            {
                Optimizer.IsSupported("invalid");
            });
        }

        [TestMethod]
        public void IsSupported_FileIsMissingPngFile_ReturnsTrue()
        {
            Assert.IsTrue(Optimizer.IsSupported(new FileInfo(Files.Missing)));
        }

        [TestMethod]
        public void IsSupported_FileIsGifFile_ReturnsTrue()
        {
            Assert.IsTrue(Optimizer.IsSupported(new FileInfo(Files.FujiFilmFinePixS1ProGIF)));
        }

        [TestMethod]
        public void IsSupported_FileIsJpgFile_ReturnsTrue()
        {
            Assert.IsTrue(Optimizer.IsSupported(new FileInfo(Files.ImageMagickJPG)));
        }

        [TestMethod]
        public void IsSupported_FileIsPngFile_ReturnsTrue()
        {
            Assert.IsTrue(Optimizer.IsSupported(new FileInfo(Files.SnakewarePNG)));
        }

        [TestMethod]
        public void IsSupported_FileIsTifFile_ReturnsFalse()
        {
            Assert.IsFalse(Optimizer.IsSupported(new FileInfo(Files.InvitationTif)));
        }

        [TestMethod]
        public void IsSupported_FileNameIsMissingPngFile_ReturnsTrue()
        {
            Assert.IsTrue(Optimizer.IsSupported(Files.Missing));
        }

        [TestMethod]
        public void IsSupported_FileNameIsGifFile_ReturnsTrue()
        {
            Assert.IsTrue(Optimizer.IsSupported(Files.FujiFilmFinePixS1ProGIF));
        }

        [TestMethod]
        public void IsSupported_FileNameIsJpgFile_ReturnsTrue()
        {
            Assert.IsTrue(Optimizer.IsSupported(Files.ImageMagickJPG));
        }

        [TestMethod]
        public void IsSupported_FileNameIsPngFile_ReturnsTrue()
        {
            Assert.IsTrue(Optimizer.IsSupported(Files.SnakewarePNG));
        }

        [TestMethod]
        public void IsSupported_FileNameIsTifFile_ReturnsFalse()
        {
            Assert.IsFalse(Optimizer.IsSupported(Files.InvitationTif));
        }

        [TestMethod]
        public void Compress_CanCompressGifFile_FileIsSmaller()
        {
            AssertCompress(Files.FujiFilmFinePixS1ProGIF, true, (FileInfo file) =>
            {
                return Optimizer.Compress(file);
            });
        }

        [TestMethod]
        public void Compress_CanCompressJpgFile_FileIsSmaller()
        {
            AssertCompress(Files.ImageMagickJPG, true, (FileInfo file) =>
            {
                return Optimizer.Compress(file);
            });
        }

        [TestMethod]
        public void Compress_CanCompressPngFile_FileIsSmaller()
        {
            AssertCompress(Files.SnakewarePNG, true, (FileInfo file) =>
            {
                return Optimizer.Compress(file);
            });
        }

        [TestMethod]
        public void Compress_CannotCompressGifFile_FileNotIsSmaller()
        {
            AssertCompress(Files.RoseSparkleGIF, false, (string file) =>
            {
                return Optimizer.Compress(file);
            });
        }

        [TestMethod]
        public void Compress_CannotCompressJpgFile_FileIsNotSmaller()
        {
            AssertCompress(Files.LetterJPG, false, (string file) =>
            {
                return Optimizer.Compress(file);
            });
        }

        [TestMethod]
        public void Compress_CannotCompressPngFile_FileIsNotSmaller()
        {
            AssertCompress(Files.MagickNETIconPNG, false, (string file) =>
            {
                return Optimizer.Compress(file);
            });
        }

        [TestMethod]
        public void LosslessCompress_CanCompressGifFile_FileIsSmaller()
        {
            AssertCompress(Files.FujiFilmFinePixS1ProGIF, true, (FileInfo file) =>
            {
                return Optimizer.LosslessCompress(file);
            });
        }

        [TestMethod]
        public void LosslessCompress_CanCompressJpgFile_FileIsSmaller()
        {
            AssertCompress(Files.ImageMagickJPG, true, (FileInfo file) =>
            {
                return Optimizer.LosslessCompress(file);
            });
        }

        [TestMethod]
        public void LosslessCompress_CanCompressPngFile_FileIsSmaller()
        {
            AssertCompress(Files.SnakewarePNG, true, (FileInfo file) =>
            {
                return Optimizer.LosslessCompress(file);
            });
        }

        [TestMethod]
        public void LosslessCompress_CannotCompressGifFile_FileNotIsSmaller()
        {
            AssertCompress(Files.RoseSparkleGIF, false, (string file) =>
            {
                return Optimizer.LosslessCompress(file);
            });
        }

        [TestMethod]
        public void LosslessCompress_CannotCompressJpgFile_FileIsNotSmaller()
        {
            AssertCompress(Files.LetterJPG, false, (string file) =>
            {
                return Optimizer.LosslessCompress(file);
            });
        }

        [TestMethod]
        public void LosslessCompress_CannotCompressPngFile_FileIsNotSmaller()
        {
            AssertCompress(Files.MagickNETIconPNG, false, (string file) =>
            {
                return Optimizer.LosslessCompress(file);
            });
        }
    }
}
