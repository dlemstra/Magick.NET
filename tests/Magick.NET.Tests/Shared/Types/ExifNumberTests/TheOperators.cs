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
    public partial class ExifNumberTests
    {
        [TestClass]
        public class TheOperators
        {
            [TestMethod]
            public void ShouldReturnTheCorrectValueWhenValuesAreEqual()
            {
                var first = new ExifNumber(10U);
                var second = new ExifNumber(10);

                Assert.IsTrue(first == second);
                Assert.IsFalse(first != second);
            }

            [TestMethod]
            public void ShouldReturnTheCorrectValueWhenValuesAreNotEqual()
            {
                var first = new ExifNumber(24);
                var second = new ExifNumber(42);

                Assert.IsFalse(first == second);
                Assert.IsTrue(first != second);
            }

            [TestMethod]
            public void ShouldBeAbleToImplicitCastFromInt()
            {
                ExifNumber number = 10;

                Assert.AreEqual(10U, (uint)number);
            }

            [TestMethod]
            public void ShouldBeAbleToImplicitCastFromUInt()
            {
                ExifNumber number = 10U;

                Assert.AreEqual(10, (ushort)number);
            }

            [TestMethod]
            public void ShouldBeAbleToImplicitCastFromShort()
            {
                ExifNumber number = (short)10;

                Assert.AreEqual(10U, (uint)number);
            }

            [TestMethod]
            public void ShouldBeAbleToImplicitCastFromUShort()
            {
                ExifNumber number = (ushort)10U;

                Assert.AreEqual(10, (ushort)number);
            }
        }
    }
}
