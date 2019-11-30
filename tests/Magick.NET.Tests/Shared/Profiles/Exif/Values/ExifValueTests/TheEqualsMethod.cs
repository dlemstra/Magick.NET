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
    public partial class ExifValueTests
    {
        [TestClass]
        public class TheEqualsMethod
        {
            [TestMethod]
            public void ShouldReturnFalseWhenInstanceIsNull()
            {
                var value = new ExifLong(ExifTag.SubIFDOffset);

                Assert.IsFalse(value.Equals(null));
            }

            [TestMethod]
            public void ShouldReturnTrueWhenInstanceIsTheSame()
            {
                var value = new ExifLong(ExifTag.SubIFDOffset);

                Assert.IsTrue(value.Equals(value));
            }

            [TestMethod]
            public void ShouldReturnTrueWhenObjectIsTheSame()
            {
                var value = new ExifLong(ExifTag.SubIFDOffset);

                Assert.IsTrue(value.Equals(value));
            }

            [TestMethod]
            public void ShouldReturnTrueWhenInstanceIsEqual()
            {
                var first = new ExifLong(ExifTag.SubIFDOffset);
                var second = ExifTag.SubIFDOffset;

                Assert.IsTrue(first.Equals(second));
            }

            [TestMethod]
            public void ShouldReturnTrueWhenObjectIsEqual()
            {
                var first = new ExifLong(ExifTag.SubIFDOffset);
                var second = new ExifLong(ExifTag.SubIFDOffset);

                Assert.IsTrue(first.Equals(second));
            }

            [TestMethod]
            public void ShouldReturnFalseWhenInstanceIsNotEqual()
            {
                var first = new ExifLong(ExifTag.SubIFDOffset);
                var second = ExifTag.CodingMethods;

                Assert.IsFalse(first.Equals(second));
            }

            [TestMethod]
            public void ShouldReturnFalseWhenObjectIsNotEqual()
            {
                var first = new ExifLong(ExifTag.SubIFDOffset);
                var second = new ExifLong(ExifTag.CodingMethods);

                Assert.IsFalse(first.Equals(second));
            }
        }
    }
}
