//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
// express or implied. See the License for the specific language governing permissions and
// limitations under the License.
//=================================================================================================

using System.IO;
using ImageMagick;
using ImageMagick.ImageOptimizers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    [TestClass]
    public class PngOptimizerTests : IImageOptimizerTests
    {
        protected override IImageOptimizer CreateImageOptimizer()
        {
            return new PngOptimizer();
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
            Test_Compress_InvalidFile(Files.ImageMagickJPG);
            Test_LosslessCompress_InvalidFile(Files.ImageMagickJPG);
        }

        [TestMethod]
        public void Test_LosslessCompress()
        {
            Test_LosslessCompress_Smaller(Files.SnakewarePNG);
        }

        [TestMethod]
        public void Test_RemoveAlpha()
        {
            string tempFile = GetTemporaryFileName(".png");

            try
            {
                using (IMagickImage image = new MagickImage(Files.MagickNETIconPNG))
                {
                    Assert.IsTrue(image.HasAlpha);
                    image.ColorAlpha(new MagickColor("yellow"));
                    image.HasAlpha = true;
                    image.Write(tempFile);

                    image.Read(tempFile);

                    Assert.IsTrue(image.HasAlpha);

                    PngOptimizer optimizer = new PngOptimizer();
                    optimizer.LosslessCompress(tempFile);

                    image.Read(tempFile);
                    Assert.IsFalse(image.HasAlpha);
                }
            }
            finally
            {
                File.Delete(tempFile);
            }
        }
    }
}
