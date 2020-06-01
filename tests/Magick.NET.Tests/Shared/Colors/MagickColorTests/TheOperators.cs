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
    public partial class MagickColorTests
    {
        [TestClass]
        public class TheOperators
        {
            [TestMethod]
            public void ShouldReturnTheCorrectValueWhenInstanceIsNull()
            {
                var color = MagickColors.Red;

                Assert.IsFalse(color == null);
                Assert.IsTrue(color != null);
                Assert.IsFalse(color < null);
                Assert.IsFalse(color <= null);
                Assert.IsTrue(color > null);
                Assert.IsTrue(color >= null);
                Assert.IsFalse(null == color);
                Assert.IsTrue(null != color);
                Assert.IsTrue(null < color);
                Assert.IsTrue(null <= color);
                Assert.IsFalse(null > color);
                Assert.IsFalse(null >= color);
            }

            [TestMethod]
            public void ShouldReturnTheCorrectValueWhenInstanceIsSpecified()
            {
                var first = MagickColors.Red;
                var second = MagickColors.Green;

                Assert.IsFalse(first == second);
                Assert.IsTrue(first != second);
                Assert.IsFalse(first < second);
                Assert.IsFalse(first <= second);
                Assert.IsTrue(first > second);
                Assert.IsTrue(first >= second);
            }

            [TestMethod]
            public void ShouldReturnTheCorrectValueWhenInstanceAreEqual()
            {
                var first = MagickColors.Red;
                var second = new MagickColor("red");

                Assert.IsTrue(first == second);
                Assert.IsFalse(first != second);
                Assert.IsFalse(first < second);
                Assert.IsTrue(first <= second);
                Assert.IsFalse(first > second);
                Assert.IsTrue(first >= second);
            }
        }
    }
}
