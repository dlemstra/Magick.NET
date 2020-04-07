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

using System;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        [TestClass]
        public class TheDeskewMethod
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenSettingsIsNull()
            {
                using (IMagickImage image = new MagickImage())
                {
                    ExceptionAssert.Throws<ArgumentNullException>("settings", () => image.Deskew(null));
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenSettingsThresholdIsNegative()
            {
                using (IMagickImage image = new MagickImage())
                {
                    var settings = new DeskewSettings()
                    {
                        Threshold = new Percentage(-1),
                    };

                    ExceptionAssert.Throws<ArgumentException>("settings", () => image.Deskew(settings));
                }
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenThresholdIsNegative()
            {
                using (IMagickImage image = new MagickImage())
                {
                    ExceptionAssert.Throws<ArgumentException>("settings", () => image.Deskew(new Percentage(-1)));
                }
            }

            [TestMethod]
            public void ShouldDeskewTheImage()
            {
                using (IMagickImage image = new MagickImage(Files.LetterJPG))
                {
                    image.ColorType = ColorType.Bilevel;

                    ColorAssert.AreEqual(MagickColors.White, image, 471, 92);

                    image.Deskew(new Percentage(10));

                    ColorAssert.AreEqual(new MagickColor("#007400740074"), image, 471, 92);
                }
            }

            [TestMethod]
            public void ShouldUseAutoCrop()
            {
                using (IMagickImage image = new MagickImage(Files.LetterJPG))
                {
                    var settings = new DeskewSettings()
                    {
                        AutoCrop = true,
                        Threshold = new Percentage(10),
                    };

                    image.Deskew(settings);

                    Assert.AreEqual(480, image.Width);
                    Assert.AreEqual(577, image.Height);
                }
            }

            [TestMethod]
            public void ShouldReturnTheAngle()
            {
                using (IMagickImage image = new MagickImage(Files.LetterJPG))
                {
                    var angle = image.Deskew(new Percentage(10));

                    Assert.AreEqual(7.01, angle, 0.01);
                }
            }
        }
    }
}
