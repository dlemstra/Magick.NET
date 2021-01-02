// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
using Xunit;

namespace Magick.NET.Core.Tests
{
    public partial class ExifValueTests
    {
        public class TheOperators
        {
            [Fact]
            public void ShouldReturnTheCorrectValueWhenInstanceIsNull()
            {
                var value = new ExifLong(ExifTag.SubIFDOffset);

                Assert.False(value == null);
                Assert.True(value != null);
                Assert.False(null == value);
                Assert.True(null != value);
            }

            [Fact]
            public void ShouldReturnTheCorrectValueWhenValuesAreEqual()
            {
                var first = new ExifLong(ExifTag.SubIFDOffset);
                var second = ExifTag.SubIFDOffset;

                Assert.True(first == second);
                Assert.False(first != second);
            }

            [Fact]
            public void ShouldReturnTheCorrectValueWhenValuesAreNotEqual()
            {
                var first = new ExifLong(ExifTag.SubIFDOffset);
                var second = ExifTag.CodingMethods;

                Assert.False(first == second);
                Assert.True(first != second);
            }
        }
    }
}
