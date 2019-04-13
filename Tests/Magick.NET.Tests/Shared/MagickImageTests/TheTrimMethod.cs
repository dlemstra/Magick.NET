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
    public partial class MagickImageTests
    {
        [TestClass]
        public class TheTrimMethod
        {
            [TestMethod]
            public void ShouldTrimTheBackground()
            {
                using (IMagickImage image = new MagickImage("xc:fuchsia", 50, 50))
                {
                    ColorAssert.AreEqual(MagickColors.Fuchsia, image, 0, 0);
                    ColorAssert.AreEqual(MagickColors.Fuchsia, image, 49, 49);

                    image.Extent(100, 60, Gravity.Center, MagickColors.Gold);

                    Assert.AreEqual(100, image.Width);
                    Assert.AreEqual(60, image.Height);
                    ColorAssert.AreEqual(MagickColors.Gold, image, 0, 0);
                    ColorAssert.AreEqual(MagickColors.Fuchsia, image, 50, 30);

                    image.Trim();

                    Assert.AreEqual(50, image.Width);
                    Assert.AreEqual(50, image.Height);
                    ColorAssert.AreEqual(MagickColors.Fuchsia, image, 0, 0);
                    ColorAssert.AreEqual(MagickColors.Fuchsia, image, 49, 49);
                }
            }

            [TestMethod]
            public void ShouldTrimTheBackgroundWithThePercentage()
            {
                using (IMagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProPNG))
                {
                    image.BackgroundColor = MagickColors.Black;
                    image.Rotate(10);

                    image.Trim(new Percentage(5));
#if Q16HDRI
                    Assert.AreEqual(560, image.Width);
                    Assert.AreEqual(320, image.Height);
#else
                    Assert.AreEqual(558, image.Width);
                    Assert.AreEqual(318, image.Height);
#endif
                }
            }
        }
    }
}
