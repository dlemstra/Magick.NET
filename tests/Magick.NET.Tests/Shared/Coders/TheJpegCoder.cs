// Copyright 2013-2019 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    [TestClass]
    public class TheJpegCoder
    {
        [TestMethod]
        public void ShouldDecodeCorrectly()
        {
            using (IMagickImage image = new MagickImage(Files.WhiteJPG))
            {
                using (var pixels = image.GetPixels())
                {
                    var color = pixels.GetPixel(0, 0).ToColor();

                    Assert.AreEqual(Quantum.Max, color.R);
                    Assert.AreEqual(Quantum.Max, color.G);
                    Assert.AreEqual(Quantum.Max, color.B);
                    Assert.AreEqual(Quantum.Max, color.A);
                }
            }
        }
    }
}
