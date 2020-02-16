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
        public class TheAlphaMethod
        {
            [TestMethod]
            public void ShouldMakeImageTransparent()
            {
                using (IMagickImage image = new MagickImage(Files.Builtin.Wizard))
                {
                    Assert.AreEqual(image.HasAlpha, false);

                    image.Alpha(AlphaOption.Transparent);

                    Assert.AreEqual(image.HasAlpha, true);
                    ColorAssert.AreEqual(MagickColors.Transparent, image, 0, 0);
                }
            }

            [TestMethod]
            public void ShouldUseTheBackgroundColor()
            {
                using (IMagickImage image = new MagickImage(Files.Builtin.Wizard))
                {
                    image.Alpha(AlphaOption.Transparent);

                    image.BackgroundColor = new MagickColor("red");
                    image.Alpha(AlphaOption.Background);
                    image.Alpha(AlphaOption.Off);

                    Assert.AreEqual(false, image.HasAlpha);
                    ColorAssert.AreEqual(new MagickColor(Quantum.Max, 0, 0), image, 0, 0);
                }
            }
        }
    }
}
