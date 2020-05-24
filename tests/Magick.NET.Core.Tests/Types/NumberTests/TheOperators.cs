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
    public partial class NumberTests
    {
        [TestClass]
        public class TheOperators
        {
            [TestMethod]
            public void ShouldReturnTheCorrectValueWhenValuesAreEqual()
            {
                var first = new Number(10U);
                var second = new Number(10);

                Assert.IsTrue(first == second);
                Assert.IsFalse(first != second);
                Assert.IsFalse(first < second);
                Assert.IsTrue(first <= second);
                Assert.IsFalse(first > second);
                Assert.IsTrue(first >= second);
            }

            [TestMethod]
            public void ShouldReturnTheCorrectValueWhenValuesAreNotEqual()
            {
                var first = new Number(24);
                var second = new Number(42);

                Assert.IsFalse(first == second);
                Assert.IsTrue(first != second);
                Assert.IsTrue(first < second);
                Assert.IsTrue(first <= second);
                Assert.IsFalse(first > second);
                Assert.IsFalse(first >= second);
            }

            [TestMethod]
            public void ShouldBeAbleToImplicitCastFromInt()
            {
                Number number = 10;

                Assert.AreEqual(10U, (uint)number);
            }

            [TestMethod]
            public void ShouldBeAbleToImplicitCastFromUInt()
            {
                Number number = 10U;

                Assert.AreEqual(10, (ushort)number);
            }

            [TestMethod]
            public void ShouldBeAbleToImplicitCastFromShort()
            {
                Number number = (short)10;

                Assert.AreEqual(10U, (uint)number);
            }

            [TestMethod]
            public void ShouldBeAbleToImplicitCastFromUShort()
            {
                Number number = (ushort)10U;

                Assert.AreEqual(10, (ushort)number);
            }
        }
    }
}
