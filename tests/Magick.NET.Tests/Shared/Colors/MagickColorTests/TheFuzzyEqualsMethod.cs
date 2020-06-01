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
        public class TheFuzzyEqualsMethod
        {
            [TestMethod]
            public void ShouldReturnFalseWhenValueIsNull()
            {
                var first = MagickColors.White;

                Assert.IsFalse(first.FuzzyEquals(null, (Percentage)0));
            }

            [TestMethod]
            public void ShouldReturnTrueWhenValuesAreSame()
            {
                var first = MagickColors.White;

                Assert.IsTrue(first.FuzzyEquals(first, (Percentage)0));
            }

            [TestMethod]
            public void ShouldReturnTrueWhenValuesAreEqual()
            {
                var first = MagickColors.White;
                var second = new MagickColor(Quantum.Max, Quantum.Max, Quantum.Max);

                Assert.IsTrue(first.FuzzyEquals(second, (Percentage)0));
            }

            [TestMethod]
            public void ShouldReturnTheCorrectValue()
            {
                var first = new MagickColor(Quantum.Max, Quantum.Max, Quantum.Max);

                var half = (QuantumType)(Quantum.Max / 2.0);
                var second = new MagickColor(Quantum.Max, half, Quantum.Max);

                Assert.IsFalse(first.FuzzyEquals(second, (Percentage)0));
                Assert.IsFalse(first.FuzzyEquals(second, (Percentage)10));
                Assert.IsFalse(first.FuzzyEquals(second, (Percentage)20));
                Assert.IsTrue(first.FuzzyEquals(second, (Percentage)30));
            }
        }
    }
}
