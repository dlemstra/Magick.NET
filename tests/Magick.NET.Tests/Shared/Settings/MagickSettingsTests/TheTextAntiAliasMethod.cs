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

namespace Magick.NET.Tests.Shared.Settings
{
    public partial class MagickSettingsTests
    {
        [TestClass]
        public class TheTextAntiAliasMethod
        {
            [TestMethod]
            public void ShouldDisableTextAntialiasingWhenFalse()
            {
                using (IMagickImage image = new MagickImage(MagickColors.Azure, 300, 300))
                {
                    Assert.AreEqual(true, image.Settings.TextAntiAlias);

                    image.Settings.TextAntiAlias = false;
                    image.Settings.FontPointsize = 100;
                    image.Annotate("TEST", Gravity.Center);

                    ColorAssert.AreEqual(MagickColors.Azure, image, 156, 119);
                    ColorAssert.AreEqual(MagickColors.Azure, image, 173, 119);
                }
            }
        }
    }
}
