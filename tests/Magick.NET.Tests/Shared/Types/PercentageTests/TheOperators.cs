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
    public partial class PercentageTests
    {
        [TestClass]
        public class TheOperators
        {
            [TestMethod]
            public void ShouldReturnCorrectValueWhenValuesAreEqual()
            {
                var first = new Percentage(100);
                var second = new Percentage(100);

                Assert.IsTrue(first == second);
                Assert.IsFalse(first != second);
                Assert.IsFalse(first < second);
                Assert.IsTrue(first <= second);
                Assert.IsFalse(first > second);
                Assert.IsTrue(first >= second);
            }

            [TestMethod]
            public void ShouldReturnCorrectValueWhenValuesFirstIsHigher()
            {
                var first = new Percentage(100);
                var second = new Percentage(101);

                Assert.IsFalse(first == second);
                Assert.IsTrue(first != second);
                Assert.IsTrue(first < second);
                Assert.IsTrue(first <= second);
                Assert.IsFalse(first > second);
                Assert.IsFalse(first >= second);
            }

            [TestMethod]
            public void ShouldReturnCorrectValueWhenValuesFirstIsLower()
            {
                var first = new Percentage(100);
                var second = new Percentage(50);

                Assert.IsFalse(first == second);
                Assert.IsTrue(first != second);
                Assert.IsFalse(first < second);
                Assert.IsFalse(first <= second);
                Assert.IsTrue(first > second);
                Assert.IsTrue(first >= second);
            }

            [TestMethod]
            public void ShouldReturnCorrectValueWhenMultiplying()
            {
                Percentage percentage = default;
                Assert.AreEqual(0, 10 * percentage);

                percentage = new Percentage(50);
                Assert.AreEqual(5, 10 * percentage);

                percentage = new Percentage(200);
                Assert.AreEqual(20.0, 10.0 * percentage);

                percentage = new Percentage(25);
                Assert.AreEqual(2.5, 10.0 * percentage);
            }
        }
    }
}
