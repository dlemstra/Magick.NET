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

#if WINDOWS_BUILD

using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class MagickSettingsTests
    {
        [TestClass]
        public class TheDensityProperty
        {
            [TestMethod]
            public void ShouldSetTheCorrectDimensionsWhenReadingImage()
            {
                using (IMagickImage image = new MagickImage())
                {
                    Assert.AreEqual(null, image.Settings.Density);

                    image.Settings.Density = new Density(100);

                    image.Read(Files.Logos.MagickNETSVG);
                    Assert.AreEqual(new Density(100, DensityUnit.Undefined), image.Density);
                    Assert.AreEqual(524, image.Width);
                    Assert.AreEqual(252, image.Height);
                }
            }
        }
    }
}

#endif