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

#if WINDOWS_BUILD

using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class MagickSettingsTests
    {
        [TestClass]
        public class TheFontFamilyProperty
        {
            [TestMethod]
            public void ShouldChangeTheFont()
            {
                using (IMagickImage image = new MagickImage())
                {
                    Assert.AreEqual(null, image.Settings.FontFamily);
                    Assert.AreEqual(0, image.Settings.FontPointsize);
                    Assert.AreEqual(FontStyleType.Undefined, image.Settings.FontStyle);
                    Assert.AreEqual(FontWeight.Undefined, image.Settings.FontWeight);

                    image.Settings.FontFamily = "Courier New";
                    image.Settings.FontPointsize = 40;
                    image.Settings.FontStyle = FontStyleType.Oblique;
                    image.Settings.FontWeight = FontWeight.ExtraBold;
                    image.Read("label:Test");

                    Assert.AreEqual(97, image.Width);
                    Assert.AreEqual(48, image.Height);
                    ColorAssert.AreEqual(MagickColors.Black, image, 16, 16);
                }
            }
        }
    }
}

#endif
