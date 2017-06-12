//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://magick.codeplex.com/>
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

using ImageMagick;
using ImageMagick.ImageOptimizers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace Magick.NET.Tests
{
    [TestClass]
    public class JpegOptimizerTests : IImageOptimizerTests
    {
        protected override IImageOptimizer CreateImageOptimizer()
        {
            return new JpegOptimizer();
        }

        [TestMethod]
        public void Test_InvalidArguments()
        {
            Test_Compress_InvalidArguments();
            Test_LosslessCompress_InvalidArguments();
        }

        [TestMethod]
        public void Test_InvalidFile()
        {
            Test_Compress_InvalidFile(Files.SnakewarePNG);
            Test_LosslessCompress_InvalidFile(Files.SnakewarePNG);
        }

        [TestMethod]
        public void Test_Compress_LosslessCompress()
        {
            long compress = Test_Compress_Smaller(Files.ImageMagickJPG);
            long losslessCompress = Test_LosslessCompress_Smaller(Files.ImageMagickJPG);

            Assert.IsTrue(compress < losslessCompress, "{0} is not smaller than {1}", compress, losslessCompress);
        }

        [TestMethod]
        public void Test_Compress_Quality()
        {
            FileInfo tempFile = CreateTemporaryFile(Files.ImageMagickJPG);

            try
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
            finally
            {
                tempFile.Delete();
            }
        }
    }
}
