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
        public class TheTextKerningProperty
        {
            [TestMethod]
            public void ShouldDefaultToZero()
            {
                using (var image = new MagickImage())
                {
                    Assert.AreEqual(0, image.Settings.TextKerning);
                }
            }

            [TestMethod]
            public void ShouldBeUsedWhenRenderingText()
            {
                using (var image = new MagickImage())
                {
                    image.Settings.TextKerning = 10;
                    image.Read("label:First");

                    Assert.AreEqual(65, image.Width);
                    Assert.AreEqual(15, image.Height);

                    image.Settings.TextKerning = 20;
                    image.Read("label:First");

                    Assert.AreEqual(105, image.Width);
                    Assert.AreEqual(15, image.Height);
                }
            }
        }
    }
}
