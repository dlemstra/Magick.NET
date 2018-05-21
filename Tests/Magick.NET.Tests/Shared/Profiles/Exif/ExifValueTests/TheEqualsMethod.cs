// Copyright 2013-2018 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

namespace Magick.NET.Tests.Profiles
{
    public partial class ExifValueTests
    {
        [TestClass]
        public class TheEqualsMethod
        {
            [TestMethod]
            public void ShouldReturnTrueWhenValuesAreEqual()
            {
                var first = GetValue();
                var second = GetSameValue();

                Assert.IsTrue(first.Equals(second));
            }

            [TestMethod]
            public void ShouldReturnTrueWhenObjectsAreEqual()
            {
                var first = GetValue();
                var second = GetSameValue();

                Assert.IsTrue(first.Equals((object)second));
            }

            [TestMethod]
            public void ShouldReturnFalseWhenValuesAreNotEqual()
            {
                var first = GetValue();
                var second = GetDifferentValue();

                Assert.IsFalse(first.Equals(second));
            }

            [TestMethod]
            public void ShouldReturnFalseWhenObjectAreNotEqual()
            {
                var first = GetValue();
                var second = GetDifferentValue();

                Assert.IsFalse(first.Equals((object)second));
            }

            private static ExifValue GetValue()
            {
                return new ExifValue(ExifTag.ImageDescription, ExifDataType.Ascii, "Communications", false);
            }

            private static ExifValue GetSameValue()
            {
                return new ExifValue(ExifTag.ImageDescription, ExifDataType.Ascii, "Communications", false);
            }

            private static ExifValue GetDifferentValue()
            {
                return new ExifValue(ExifTag.ImageDescription, ExifDataType.Ascii, "Communication", false);
            }
        }
    }
}
