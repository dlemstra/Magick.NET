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

using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        [TestClass]
        public class TheWhiteBalanceMethod
        {
            [TestMethod]
            public void ShouldWhiteBalanceTheImage()
            {
                using (var image = new MagickImage(Files.Builtin.Rose))
                {
                    image.WhiteBalance();
#if Q8
                    ColorAssert.AreEqual(new MagickColor("#dd4946"), image, 45, 25);
#else
                    ColorAssert.AreEqual(new MagickColor("#de494a714698"), image, 45, 25);
#endif
                }
            }

            [TestMethod]
            public void ShouldUseTheVibrance()
            {
                using (var image = new MagickImage(Files.Builtin.Rose))
                {
                    image.WhiteBalance(new Percentage(70));
#if Q8
                    ColorAssert.AreEqual(new MagickColor("#00a13b"), image, 45, 25);
#elif Q16
                    ColorAssert.AreEqual(new MagickColor("#e079a2033c3c"), image, 45, 25);
#else
                    image.Clamp();
                    ColorAssert.AreEqual(new MagickColor("#0000a2033c3c"), image, 45, 25);
#endif
                }
            }
        }
    }
}
