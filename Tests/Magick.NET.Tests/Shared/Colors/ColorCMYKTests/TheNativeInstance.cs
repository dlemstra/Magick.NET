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

using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class ColorCMYKTests : ColorBaseTests<ColorCMYK>
    {
        [TestClass]
        public class TheNativeInstance
        {
            [TestMethod]
            public void ShouldHaveTheCorrectColorspace()
            {
                using (IMagickImage image = new MagickImage(MagickColors.Black, 1, 1))
                {
                    image.ColorSpace = ColorSpace.CMYK;
                    image.Opaque(MagickColors.Black, new MagickColor("cmyk(128,23,250,156)"));

                    using (IPixelCollection pixels = image.GetPixelsUnsafe())
                    {
                        var color = pixels.GetPixel(0, 0).ToColor();
#if Q8
                        Assert.AreEqual("cmyka(128,23,250,156,1.0)", color.ToString());
#elif Q16 || Q16HDRI
                        Assert.AreEqual("cmyka(32896,5911,64250,40092,1.0)", color.ToString());
#else
#error Not implemented!
#endif
                    }
                }
            }
        }
    }
}
