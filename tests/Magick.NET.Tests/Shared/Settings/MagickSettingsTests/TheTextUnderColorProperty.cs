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
    public partial class MagickSettingsTests
    {
        [TestClass]
        public class TheTextUnderColorProperty
        {
            [TestMethod]
            public void ShouldDefaultToBlack()
            {
                using (IMagickImage image = new MagickImage())
                {
                    ColorAssert.AreEqual(new MagickColor(0, 0, 0, 0), image.Settings.TextUnderColor);
                }
            }

            [TestMethod]
            public void ShouldUseBlackWhenSetToNull()
            {
                using (IMagickImage image = new MagickImage())
                {
                    image.Settings.TextUnderColor = null;
                    image.Read("label:First");

                    Assert.AreEqual(25, image.Width);
                    Assert.AreEqual(15, image.Height);

                    ColorAssert.AreEqual(MagickColors.White, image, 0, 0);
                    ColorAssert.AreEqual(MagickColors.White, image, 23, 0);
                }
            }

            [TestMethod]
            public void ShouldUseTheSpecifiedColor()
            {
                using (IMagickImage image = new MagickImage())
                {
                    image.Settings.TextUnderColor = MagickColors.Purple;
                    image.Read("label:First");

                    Assert.AreEqual(25, image.Width);
                    Assert.AreEqual(15, image.Height);

                    ColorAssert.AreEqual(MagickColors.Purple, image, 0, 0);
                    ColorAssert.AreEqual(MagickColors.Purple, image, 23, 0);
                }
            }
        }
    }
}
