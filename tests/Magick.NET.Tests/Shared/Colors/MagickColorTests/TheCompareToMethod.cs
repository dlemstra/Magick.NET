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
    public partial class MagickColorTests
    {
        public class TheCompareToMethod
        {
            [Fact]
            public void ShouldReturnZeroWhenValuesAreSame()
            {
                var first = MagickColors.White;

                Assert.Equal(0, first.CompareTo(first));
            }

            [Fact]
            public void ShouldReturnOneWhenValueIsNull()
            {
                var first = MagickColors.White;

                Assert.Equal(1, first.CompareTo(null));
            }

            [Fact]
            public void ShouldReturnZeroWhenValuesAreEqual()
            {
                var first = MagickColors.White;
                var second = new MagickColor(MagickColors.White);

                Assert.Equal(0, first.CompareTo(second));
            }

            [Fact]
            public void ShouldReturnMinusOneWhenValueIsHigher()
            {
                var half = (QuantumType)(Quantum.Max / 2.0);
                var first = new MagickColor(half, half, half, half, half);

                var second = new MagickColor(half, half, Quantum.Max, half, half);
                Assert.Equal(-1, first.CompareTo(second));

                second = new MagickColor(half, half, Quantum.Max, half, half);
                Assert.Equal(-1, first.CompareTo(second));

                second = new MagickColor(half, half, half, Quantum.Max, half);
                Assert.Equal(-1, first.CompareTo(second));

                second = new MagickColor(half, half, half, half, Quantum.Max);
                Assert.Equal(-1, first.CompareTo(second));
            }

            [Fact]
            public void ShouldReturnOneWhenValueIsLower()
            {
                var half = (QuantumType)(Quantum.Max / 2.0);
                var first = MagickColors.White;

                var second = new MagickColor(half, 0, half, half, half);
                Assert.Equal(1, first.CompareTo(second));

                second = new MagickColor(half, half, 0, half, half);
                Assert.Equal(1, first.CompareTo(second));

                second = new MagickColor(half, half, half, 0, half);
                Assert.Equal(1, first.CompareTo(second));

                second = new MagickColor(half, half, half, half, 0);
                Assert.Equal(1, first.CompareTo(second));
            }
        }
    }
}
