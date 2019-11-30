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

using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class NumberTests
    {
        [TestClass]
        public class TheCompareToMethod
        {
            [TestMethod]
            public void ShouldReturnZeroWhenInstancesAreTheSame()
            {
                var first = new Number(10);

                Assert.AreEqual(0, first.CompareTo(first));
            }

            [TestMethod]
            public void ShouldReturnZeroWhenInstancesAreEqual()
            {
                var first = new Number(10);
                var second = new Number(10);

                Assert.AreEqual(0, first.CompareTo(second));
            }

            [TestMethod]
            public void ShouldReturnOneWhenInstancesAreNotEqual()
            {
                var first = new Number(10);
                var second = new Number(5);

                Assert.AreEqual(1, first.CompareTo(second));
            }
        }
    }
}
