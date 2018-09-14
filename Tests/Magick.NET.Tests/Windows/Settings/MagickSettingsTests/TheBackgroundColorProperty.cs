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
        public class TheBackgroundColorProperty
        {
            [TestMethod]
            public void ShouldSetTheBackgroundColorWhenReadingImage()
            {
                using (IMagickImage image = new MagickImage())
                {
                    ColorAssert.AreEqual(MagickColors.White, image.Settings.BackgroundColor);

                    image.Read(Files.Logos.MagickNETSVG);
                    ColorAssert.AreEqual(MagickColors.White, image, 0, 0);

                    image.Settings.BackgroundColor = MagickColors.Yellow;
                    image.Read(Files.Logos.MagickNETSVG);
                    ColorAssert.AreEqual(MagickColors.Yellow, image, 0, 0);
                }
            }
        }
    }
}

#endif