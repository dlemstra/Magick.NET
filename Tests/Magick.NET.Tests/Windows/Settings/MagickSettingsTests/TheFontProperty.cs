// Copyright 2013-2018 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

#if WINDOWS_BUILD

using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class MagickSettingsTests
    {
        [TestClass]
        public class TheFontProperty
        {
            [TestMethod]
            public void ShouldSetTheFontWhenReadingImage()
            {
                using (IMagickImage image = new MagickImage())
                {
                    Assert.AreEqual(null, image.Settings.Font);

                    image.Settings.Font = "Courier New Bold Oblique";
                    image.Settings.FontPointsize = 40;
                    image.Read("pango:Test");

                    Assert.AreEqual(120, image.Width);
                    Assert.AreEqual(57, image.Height);
                    ColorAssert.AreEqual(MagickColors.Black, image, 21, 18);
                }
            }
        }
    }
}

#endif