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
    public partial class MagickImageTests
    {
        [TestClass]
        public class TheBlackPointCompensationProperty
        {
            [TestMethod]
            public void ShouldBeDisabledByDefault()
            {
                using (IMagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProPNG))
                {
                    Assert.IsFalse(image.BlackPointCompensation);
                    image.RenderingIntent = RenderingIntent.Relative;

                    image.TransformColorSpace(ColorProfile.SRGB, ColorProfile.USWebCoatedSWOP);
#if Q8 || Q16
                    ColorAssert.AreEqual(new MagickColor("#da478d06323d"), image, 130, 100);
#elif Q16HDRI
                    ColorAssert.AreEqual(new MagickColor("#da7b8d1c318a"), image, 130, 100);
#else
#error Not implemented!
#endif
                }
            }

            [TestMethod]
            public void ShouldBeUsedInTheColorTransformation()
            {
                using (IMagickImage image = new MagickImage(Files.FujiFilmFinePixS1ProPNG))
                {
                    image.RenderingIntent = RenderingIntent.Relative;
                    image.BlackPointCompensation = true;

                    image.TransformColorSpace(ColorProfile.SRGB, ColorProfile.USWebCoatedSWOP);
#if Q8 || Q16
                    ColorAssert.AreEqual(new MagickColor("#cd0a844e3209"), image, 130, 100);
#elif Q16HDRI
                    ColorAssert.AreEqual(new MagickColor("#ccf7847331b2"), image, 130, 100);
#else
#error Not implemented!
#endif
                }
            }
        }
    }
}
