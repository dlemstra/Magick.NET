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
    public partial class ExifTagTests
    {
        [TestClass]
        public class TheOperators
        {
            [TestMethod]
            public void ShouldReturnTheCorrectValueWhenInstanceIsNull()
            {
                var tag = new ExifTag<uint>(ExifTagValue.SubIFDOffset);

                Assert.IsFalse(tag == null);
                Assert.IsTrue(tag != null);
                Assert.IsFalse(null == tag);
                Assert.IsTrue(null != tag);
            }

            [TestMethod]
            public void ShouldReturnTheCorrectValueWhenValuesAreEqual()
            {
                var first = new ExifTag<uint>(ExifTagValue.SubIFDOffset);
                var second = new ExifTag<uint>(ExifTagValue.SubIFDOffset);

                Assert.IsTrue(first == second);
                Assert.IsFalse(first != second);
            }

            [TestMethod]
            public void ShouldReturnTheCorrectValueWhenValuesAreNotEqual()
            {
                var first = new ExifTag<uint>(ExifTagValue.SubIFDOffset);
                var second = new ExifTag<uint>(ExifTagValue.CodingMethods);

                Assert.IsFalse(first == second);
                Assert.IsTrue(first != second);
            }
        }
    }
}
