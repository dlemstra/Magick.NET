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

using ImageMagick.ImageOptimizers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    [TestClass]
    public class GifOptimizerTests : IImageOptimizerTests
    {
        protected override IImageOptimizer CreateImageOptimizer()
        {
            return new GifOptimizer();
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
            Test_Compress_InvalidFile(Files.InvitationTif);
            Test_LosslessCompress_InvalidFile(Files.InvitationTif);
        }

        [TestMethod]
        public void Test_Compress()
        {
            Test_Compress_NotSmaller(Files.RoseSparkleGIF);
            Test_Compress_Smaller(Files.FujiFilmFinePixS1ProGIF);
        }

        [TestMethod]
        public void Test_LosslessCompress()
        {
            Test_LosslessCompress_NotSmaller(Files.RoseSparkleGIF);
            Test_LosslessCompress_Smaller(Files.FujiFilmFinePixS1ProGIF);
        }
    }
}
