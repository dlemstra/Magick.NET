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
        public class TheModulateMethod
        {
            [TestMethod]
            public void ShouldDefaultTo100PercentForSaturationAndHue()
            {
                using (var image = new MagickImage(Files.TestPNG))
                {
                    using (var other = image.Clone())
                    {
                        image.Modulate(new Percentage(50));
                        other.Modulate(new Percentage(50), new Percentage(100), new Percentage(100));

                        var difference = image.Compare(other, ErrorMetric.RootMeanSquared);
                        Assert.AreEqual(0, difference);
                    }
                }
            }

            [TestMethod]
            public void ShouldDefaultTo100PercentForSaturation()
            {
                using (var image = new MagickImage(Files.TestPNG))
                {
                    using (var other = image.Clone())
                    {
                        image.Modulate(new Percentage(50), new Percentage(25));
                        other.Modulate(new Percentage(50), new Percentage(25), new Percentage(100));

                        var difference = image.Compare(other, ErrorMetric.RootMeanSquared);
                        Assert.AreEqual(0, difference);
                    }
                }
            }

            [TestMethod]
            public void ShouldModulateImage()
            {
                using (var image = new MagickImage(Files.TestPNG))
                {
                    image.Modulate(new Percentage(70), new Percentage(30));

#if Q8
                    ColorAssert.AreEqual(new MagickColor("#743e3e"), image, 25, 70);
                    ColorAssert.AreEqual(new MagickColor("#0b0b0b"), image, 25, 40);
                    ColorAssert.AreEqual(new MagickColor("#1f3a1f"), image, 75, 70);
                    ColorAssert.AreEqual(new MagickColor("#5a5a5a"), image, 75, 40);
                    ColorAssert.AreEqual(new MagickColor("#3e3e74"), image, 125, 70);
                    ColorAssert.AreEqual(new MagickColor("#a8a8a8"), image, 125, 40);
#else
                    ColorAssert.AreEqual(new MagickColor(OpenCLValue.Get("#72803da83da8", "#747a3eb83eb8")), image, 25, 70);
                    ColorAssert.AreEqual(new MagickColor(OpenCLValue.Get("#0b2d0b2d0b2d", "#0b5f0b5f0b5f")), image, 25, 40);
                    ColorAssert.AreEqual(new MagickColor(OpenCLValue.Get("#1ef3397a1ef3", "#1f7c3a781f7c")), image, 75, 70);
                    ColorAssert.AreEqual(new MagickColor(OpenCLValue.Get("#592d592d592d", "#5ab75ab75ab7")), image, 75, 40);
                    ColorAssert.AreEqual(new MagickColor(OpenCLValue.Get("#3da83da87280", "#3eb83eb8747a")), image, 125, 70);
                    ColorAssert.AreEqual(new MagickColor(OpenCLValue.Get("#a5aea5aea5ae", "#a88ba88ba88b")), image, 125, 40);
#endif
                }
            }
        }
    }
}
