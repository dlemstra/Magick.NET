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
    public class PixelTests
    {
        [Fact]
        public void Test_IEquatable()
        {
            var first = new Pixel(0, 0, 3);
            first.SetChannel(0, 100);
            first.SetChannel(1, 100);
            first.SetChannel(2, 100);

            Assert.False(first.Equals(null));
            Assert.True(first.Equals(first));
            Assert.True(first.Equals((object)first));

            var second = new Pixel(10, 10, 3);
            second.SetChannel(0, 100);
            second.SetChannel(1, 0);
            second.SetChannel(2, 100);

            Assert.True(!first.Equals(second));
            Assert.True(!first.Equals((object)second));

            second.SetChannel(1, 100);

            Assert.True(first.Equals(second));
            Assert.True(first.Equals((object)second));
        }

        [Fact]
        public void Test_GetAndSetChannel()
        {
            QuantumType half = (QuantumType)(Quantum.Max / 2.0);

            var first = new Pixel(0, 0, 3);
            first.SetValues(new QuantumType[] { Quantum.Max, 0, half });

            Assert.Equal(Quantum.Max, first.GetChannel(0));
            Assert.Equal(0, first.GetChannel(1));
            Assert.Equal(half, first.GetChannel(2));

            first.SetChannel(0, 0);
            first.SetChannel(1, half);
            first.SetChannel(2, Quantum.Max);

            Assert.Equal(0, first.GetChannel(0));
            Assert.Equal(half, first.GetChannel(1));
            Assert.Equal(Quantum.Max, first.GetChannel(2));
        }

        [Fact]
        public void Test_ToColor()
        {
            QuantumType half = (QuantumType)(Quantum.Max / 2.0);

            var pixel = new Pixel(0, 0, 1);
            pixel.SetValues(new QuantumType[] { Quantum.Max });
            ColorAssert.Equal(new MagickColor(Quantum.Max, Quantum.Max, Quantum.Max), pixel.ToColor());

            pixel = new Pixel(0, 0, 2);
            pixel.SetValues(new QuantumType[] { Quantum.Max, 0 });
            ColorAssert.Equal(new MagickColor(Quantum.Max, Quantum.Max, Quantum.Max, 0), pixel.ToColor());

            pixel = new Pixel(0, 0, 3);
            pixel.SetValues(new QuantumType[] { Quantum.Max, 0, half });
            ColorAssert.Equal(new MagickColor(Quantum.Max, 0, half), pixel.ToColor());

            pixel = new Pixel(0, 0, 4);
            pixel.SetValues(new QuantumType[] { 0, half, Quantum.Max, Quantum.Max });
            ColorAssert.Equal(new MagickColor(0, half, Quantum.Max, Quantum.Max), pixel.ToColor());

            pixel = new Pixel(0, 0, 5);
            pixel.SetValues(new QuantumType[] { Quantum.Max, 0, half, Quantum.Max, Quantum.Max });
            ColorAssert.Equal(new MagickColor(Quantum.Max, 0, half, Quantum.Max), pixel.ToColor());
        }
    }
}
