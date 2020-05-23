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
        public class TheTextGravityProperty
        {
            [TestMethod]
            public void ShouldDetermineThePositionOfTheText()
            {
                using (var image = new MagickImage("xc:red", 300, 300))
                {
                    Assert.AreEqual(Gravity.Undefined, image.Settings.TextGravity);

                    image.Settings.BackgroundColor = MagickColors.Yellow;
                    image.Settings.StrokeColor = MagickColors.Fuchsia;
                    image.Settings.FillColor = MagickColors.Fuchsia;
                    image.Settings.TextGravity = Gravity.Center;

                    image.Read("label:Test");

                    ColorAssert.AreEqual(MagickColors.Yellow, image, 50, 80);
                    ColorAssert.AreEqual(MagickColors.Fuchsia, image, 50, 160);
                }
            }
        }
    }
}
