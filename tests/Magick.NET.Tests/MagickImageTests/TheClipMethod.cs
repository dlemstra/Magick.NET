// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using ImageMagick;
using Xunit;

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
        public class TheClipMethod
        {
            [Fact]
            public void ShouldSetTheCorrectColorsWhenInsideIsFalse()
            {
                AssertClipColors(false, 0);
            }

            [Fact]
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
                        Assert.NotNull(mask);
                        Assert.False(mask.HasAlpha);

                        using (var pixels = mask.GetPixels())
                        {
                            var pixelA = pixels.GetPixel(0, 0).ToColor();
                            var pixelB = pixels.GetPixel(mask.Width - 1, mask.Height - 1).ToColor();

                            Assert.Equal(pixelA, pixelB);
                            Assert.Equal(value, pixelA.R);
                            Assert.Equal(value, pixelA.G);
                            Assert.Equal(value, pixelA.B);

                            var pixelC = pixels.GetPixel(mask.Width / 2, mask.Height / 2).ToColor();
                            Assert.Equal(Quantum.Max - value, pixelC.R);
                            Assert.Equal(Quantum.Max - value, pixelC.G);
                            Assert.Equal(Quantum.Max - value, pixelC.B);
                        }
                    }
                }
            }
        }
    }
}
