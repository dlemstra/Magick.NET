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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        [TestClass]
        public class TheSeparateMethod
        {
            [TestMethod]
            public void ShouldReturnTheCorrectNumberOfChannels()
            {
                using (IMagickImage rose = new MagickImage(Files.Builtin.Rose))
                {
                    var i = 0;
                    foreach (MagickImage image in rose.Separate())
                    {
                        i++;
                        image.Dispose();
                    }

                    Assert.AreEqual(3, i);
                }
            }

            [TestMethod]
            public void ShouldReturnTheSpecifiedChannels()
            {
                using (IMagickImage rose = new MagickImage(Files.Builtin.Rose))
                {
                    var i = 0;
                    foreach (MagickImage image in rose.Separate(Channels.Red | Channels.Green))
                    {
                        i++;
                        image.Dispose();
                    }

                    Assert.AreEqual(2, i);
                }
            }

            [TestMethod]
            public void Test_Separate_Composite()
            {
                using (IMagickImage logo = new MagickImage(Files.Builtin.Logo))
                {
                    using (IMagickImage blue = logo.Separate(Channels.Blue).First())
                    {
                        AssertSeparate(blue, ColorSpace.Gray, 146);

                        using (IMagickImage green = logo.Separate(Channels.Green).First())
                        {
                            AssertSeparate(green, ColorSpace.Gray, 62);

                            blue.Composite(green, CompositeOperator.Modulate);

                            AssertSeparate(blue, ColorSpace.sRGB, 15);
                        }
                    }
                }
            }

            private static void AssertSeparate(IMagickImage image, ColorSpace colorSpace, byte value)
            {
                Assert.AreEqual(colorSpace, image.ColorSpace);

                using (IPixelCollection pixels = image.GetPixels())
                {
                    Pixel pixel = pixels.GetPixel(340, 260);
                    ColorAssert.AreEqual(MagickColor.FromRgb(value, value, value), pixel.ToColor());
                }
            }
        }
    }
}
