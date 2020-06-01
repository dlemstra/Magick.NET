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
        [TestClass]
        public class TheCompareToMethod
        {
            [TestMethod]
            public void ShouldReturnZeroWhenValuesAreSame()
            {
                var first = MagickColors.White;

                Assert.AreEqual(0, first.CompareTo(first));
            }

            [TestMethod]
            public void ShouldReturnOneWhenValueIsNull()
            {
                var first = MagickColors.White;

                Assert.AreEqual(1, first.CompareTo(null));
            }

            [TestMethod]
            public void ShouldReturnZeroWhenValuesAreEqual()
            {
                var first = MagickColors.White;
                var second = new MagickColor(MagickColors.White);

                Assert.AreEqual(0, first.CompareTo(second));
            }

            [TestMethod]
            public void ShouldReturnMinusOneWhenValueIsHigher()
            {
                var half = (QuantumType)(Quantum.Max / 2.0);
                var first = new MagickColor(half, half, half, half, half);

                var second = new MagickColor(half, half, Quantum.Max, half, half);
                Assert.AreEqual(-1, first.CompareTo(second));

                second = new MagickColor(half, half, Quantum.Max, half, half);
                Assert.AreEqual(-1, first.CompareTo(second));

                second = new MagickColor(half, half, half, Quantum.Max, half);
                Assert.AreEqual(-1, first.CompareTo(second));

                second = new MagickColor(half, half, half, half, Quantum.Max);
                Assert.AreEqual(-1, first.CompareTo(second));
            }

            [TestMethod]
            public void ShouldReturnOneWhenValueIsLower()
            {
                var half = (QuantumType)(Quantum.Max / 2.0);
                var first = MagickColors.White;

                var second = new MagickColor(half, 0, half, half, half);
                Assert.AreEqual(1, first.CompareTo(second));

                second = new MagickColor(half, half, 0, half, half);
                Assert.AreEqual(1, first.CompareTo(second));

                second = new MagickColor(half, half, half, 0, half);
                Assert.AreEqual(1, first.CompareTo(second));

                second = new MagickColor(half, half, half, half, 0);
                Assert.AreEqual(1, first.CompareTo(second));
            }
        }
    }
}
