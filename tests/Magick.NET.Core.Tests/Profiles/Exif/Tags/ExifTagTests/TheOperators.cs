// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests
{
    public partial class ExifTagTests
    {
        public class TheOperators
        {
            [Fact]
            public void ShouldReturnTheCorrectValueWhenInstanceIsNull()
            {
                var tag = new ExifTag<uint>(ExifTagValue.SubIFDOffset);

                Assert.False(tag == null);
                Assert.True(tag != null);
                Assert.False(null == tag);
                Assert.True(null != tag);
            }

            [Fact]
            public void ShouldReturnTheCorrectValueWhenValuesAreEqual()
            {
                var first = new ExifTag<uint>(ExifTagValue.SubIFDOffset);
                var second = new ExifTag<uint>(ExifTagValue.SubIFDOffset);

                Assert.True(first == second);
                Assert.False(first != second);
            }

            [Fact]
            public void ShouldReturnTheCorrectValueWhenValuesAreNotEqual()
            {
                var first = new ExifTag<uint>(ExifTagValue.SubIFDOffset);
                var second = new ExifTag<uint>(ExifTagValue.CodingMethods);

                Assert.False(first == second);
                Assert.True(first != second);
            }
        }
    }
}
