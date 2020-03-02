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

using System.Collections.Generic;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        [TestClass]
        public class TheAutoThresholdMethod
        {
            [TestMethod]
            public void ShouldThresholdImageWithKapurMethod()
            {
                using (IMagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProPNG))
                {
                    image.AutoThreshold(AutoThresholdMethod.Kapur);

                    Dictionary<MagickColor, int> colors = image.Histogram();

                    Assert.AreEqual(ColorType.Bilevel, image.DetermineColorType());
                    Assert.AreEqual(236359, colors[MagickColors.Black]);
                    Assert.AreEqual(3641, colors[MagickColors.White]);
                }
            }

            [TestMethod]
            public void ShouldThresholdImageWithOTSUMethod()
            {
                using (IMagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProPNG))
                {
                    image.AutoThreshold(AutoThresholdMethod.OTSU);

                    Dictionary<MagickColor, int> colors = image.Histogram();

                    Assert.AreEqual(ColorType.Bilevel, image.DetermineColorType());
                    Assert.AreEqual(67844, colors[MagickColors.Black]);
                    Assert.AreEqual(172156, colors[MagickColors.White]);
                }
            }

            [TestMethod]
            public void ShouldThresholdImageWithTriangleMethod()
            {
                using (IMagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProPNG))
                {
                    image.AutoThreshold(AutoThresholdMethod.Triangle);

                    Dictionary<MagickColor, int> colors = image.Histogram();

                    Assert.AreEqual(ColorType.Bilevel, image.DetermineColorType());
                    Assert.AreEqual(210553, colors[MagickColors.Black]);
                    Assert.AreEqual(29447, colors[MagickColors.White]);
                }
            }
        }
    }
}
