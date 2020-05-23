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

#if WINDOWS_BUILD

using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        [TestClass]
        public class TheClipMethod
        {
            [TestMethod]
            public void ShouldSetTheCorrectColorsWhenInsideIsFalse()
            {
                AssertClipColors(false, 0);
            }

            [TestMethod]
            public void ShouldSetTheCorrectColorsWhenInsideIsTrue()
            {
                AssertClipColors(true, Quantum.Max);
            }

            private static void AssertClipColors(bool inside, QuantumType value)
            {
                using (var image = new MagickImage(Files.InvitationTIF))
                {
                    image.Alpha(AlphaOption.Transparent);
                    image.Clip("Pad A", inside);
                    image.Alpha(AlphaOption.Opaque);

                    using (var mask = image.GetWriteMask())
                    {
                        Assert.IsNotNull(mask);
                        Assert.IsFalse(mask.HasAlpha);

                        using (IPixelCollection pixels = mask.GetPixels())
                        {
                            var pixelA = pixels.GetPixel(0, 0).ToColor();
                            var pixelB = pixels.GetPixel(mask.Width - 1, mask.Height - 1).ToColor();

                            Assert.AreEqual(pixelA, pixelB);
                            Assert.AreEqual(value, pixelA.R);
                            Assert.AreEqual(value, pixelA.G);
                            Assert.AreEqual(value, pixelA.B);

                            var pixelC = pixels.GetPixel(mask.Width / 2, mask.Height / 2).ToColor();
                            Assert.AreEqual(Quantum.Max - value, pixelC.R);
                            Assert.AreEqual(Quantum.Max - value, pixelC.G);
                            Assert.AreEqual(Quantum.Max - value, pixelC.B);
                        }
                    }
                }
            }
        }
    }
}

#endif