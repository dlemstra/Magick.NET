//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   http://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
// express or implied. See the License for the specific language governing permissions and
// limitations under the License.
//=================================================================================================

using System;
using System.IO;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    [TestClass]
    public class ImageOptimizerTests
    {
        private static FileInfo CreateTemporaryFile(string fileName)
        {
            string tempFile = GetTemporaryFileName(Path.GetExtension(fileName));
            File.Copy(fileName, tempFile, true);

            return new FileInfo(tempFile);
        }

        protected static string GetTemporaryFileName(string extension)
        {
            string tempFile = Path.GetTempFileName();
            File.Move(tempFile, tempFile + extension);

            return tempFile + extension;
        }

        private void Test_CompressWithTempFile(string fileName)
        {
            string tempFile = Path.GetTempFileName();

            try
            {
                File.Copy(fileName, tempFile, true);
                Test_Compress_Smaller(tempFile);
            }
            finally
            {
                if (File.Exists(tempFile))
                    File.Delete(tempFile);
            }
        }

        protected void Test_Compress_Smaller(string fileName)
        {
            FileInfo tempFile = CreateTemporaryFile(fileName);
            try
            {
                ImageOptimizer optimizer = new ImageOptimizer();
                Assert.IsNotNull(optimizer);

                long before = tempFile.Length;
                optimizer.Compress(tempFile);

                long after = tempFile.Length;

                Assert.IsTrue(after < before, "{0} is not smaller than {1}", after, before);
            }
            finally
            {
                tempFile.Delete();
            }
        }

        private void Test_LosslessCompressWithTempFile(string fileName)
        {
            string tempFile = Path.GetTempFileName();

            try
            {
                File.Copy(fileName, tempFile, true);
                Test_LosslessCompress_Smaller(tempFile);
            }
            finally
            {
                if (File.Exists(tempFile))
                    File.Delete(tempFile);
            }
        }

        private void Test_LosslessCompress_Smaller(string fileName)
        {
            FileInfo tempFile = CreateTemporaryFile(fileName);
            try
            {
                ImageOptimizer optimizer = new ImageOptimizer();
                Assert.IsNotNull(optimizer);

                long before = tempFile.Length;
                optimizer.LosslessCompress(tempFile);

                long after = tempFile.Length;

                Assert.IsTrue(after < before, "{0} is not smaller than {1}", after, before);
            }
            finally
            {
                tempFile.Delete();
            }
        }

        [TestMethod]
        public void Test_InvalidArguments()
        {
            ImageOptimizer optimizer = new ImageOptimizer();
            Assert.IsNotNull(optimizer);

            ExceptionAssert.Throws<ArgumentNullException>(delegate ()
            {
                optimizer.Compress((FileInfo)null);
            });

            ExceptionAssert.Throws<ArgumentNullException>(delegate ()
            {
                optimizer.Compress((string)null);
            });

            ExceptionAssert.Throws<ArgumentException>(delegate ()
            {
                optimizer.Compress("");
            });

            ExceptionAssert.Throws<ArgumentException>(delegate ()
            {
                optimizer.Compress(Files.Missing);
            });

            ExceptionAssert.Throws<ArgumentNullException>(delegate ()
            {
                optimizer.LosslessCompress((FileInfo)null);
            });

            ExceptionAssert.Throws<ArgumentNullException>(delegate ()
            {
                optimizer.LosslessCompress((string)null);
            });

            ExceptionAssert.Throws<ArgumentException>(delegate ()
            {
                optimizer.LosslessCompress("");
            });

            ExceptionAssert.Throws<ArgumentException>(delegate ()
            {
                optimizer.LosslessCompress(Files.Missing);
            });
        }

        [TestMethod]
        public void Test_IsSupported()
        {
            ImageOptimizer optimizer = new ImageOptimizer();

            ExceptionAssert.Throws<ArgumentNullException>(delegate ()
            {
                optimizer.IsSupported((FileInfo)null);
            });

            ExceptionAssert.Throws<ArgumentNullException>(delegate ()
            {
                optimizer.IsSupported((string)null);
            });

            ExceptionAssert.Throws<ArgumentException>(delegate ()
            {
                optimizer.IsSupported("");
            });

            Assert.IsTrue(optimizer.IsSupported(Files.FujiFilmFinePixS1ProGIF));
            Assert.IsTrue(optimizer.IsSupported(Files.ImageMagickJPG));
            Assert.IsTrue(optimizer.IsSupported(Files.SnakewarePNG));
            Assert.IsTrue(optimizer.IsSupported(Files.Missing));
            Assert.IsFalse(optimizer.IsSupported(Files.InvitationTif));
        }

        [TestMethod]
        public void Test_Compress()
        {
            Test_Compress_Smaller(Files.FujiFilmFinePixS1ProGIF);
            Test_Compress_Smaller(Files.ImageMagickJPG);
            Test_Compress_Smaller(Files.SnakewarePNG);
            Test_CompressWithTempFile(Files.ImageMagickJPG);
            Test_CompressWithTempFile(Files.SnakewarePNG);
        }

        [TestMethod]
        public void Test_LosslessCompress()
        {
            Test_LosslessCompress_Smaller(Files.FujiFilmFinePixS1ProGIF);
            Test_LosslessCompress_Smaller(Files.ImageMagickJPG);
            Test_LosslessCompress_Smaller(Files.SnakewarePNG);
            Test_LosslessCompressWithTempFile(Files.ImageMagickJPG);
            Test_LosslessCompressWithTempFile(Files.SnakewarePNG);
        }
    }
}
