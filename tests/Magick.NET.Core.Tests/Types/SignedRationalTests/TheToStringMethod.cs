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

namespace Magick.NET.Core.Tests
{
    public partial class SignedRationalTests
    {
        [TestClass]
        public class TheToStringMethod
        {
            [TestMethod]
            public void ShouldReturnPositiveInfinityWhenValueIsNan()
            {
                var rational = new SignedRational(double.NaN);
                Assert.AreEqual("Indeterminate", rational.ToString());
            }

            [TestMethod]
            public void ShouldReturnPositiveInfinityWhenValueIsPositiveInfinity()
            {
                var rational = new SignedRational(double.PositiveInfinity);
                Assert.AreEqual("PositiveInfinity", rational.ToString());
            }

            [TestMethod]
            public void ShouldReturnNegativeInfinityWhenValueIsNegativeInfinity()
            {
                var rational = new SignedRational(double.NegativeInfinity);
                Assert.AreEqual("NegativeInfinity", rational.ToString());
            }

            [TestMethod]
            public void ShouldReturnTheCorrectValue()
            {
                var rational = new SignedRational(0, 1);
                Assert.AreEqual("0", rational.ToString());

                rational = new SignedRational(-2, 1);
                Assert.AreEqual("-2", rational.ToString());

                rational = new SignedRational(-1, 2);
                Assert.AreEqual("-1/2", rational.ToString());
            }
        }
    }
}
