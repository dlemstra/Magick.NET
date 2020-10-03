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
using Xunit;

namespace Magick.NET.Core.Tests
{
    public partial class PercentageTests
    {
        public class TheOperators
        {
            [Fact]
            public void ShouldReturnCorrectValueWhenValuesAreEqual()
            {
                var first = new Percentage(100);
                var second = new Percentage(100);

                Assert.True(first == second);
                Assert.False(first != second);
                Assert.False(first < second);
                Assert.True(first <= second);
                Assert.False(first > second);
                Assert.True(first >= second);
            }

            [Fact]
            public void ShouldReturnCorrectValueWhenValuesFirstIsHigher()
            {
                var first = new Percentage(100);
                var second = new Percentage(101);

                Assert.False(first == second);
                Assert.True(first != second);
                Assert.True(first < second);
                Assert.True(first <= second);
                Assert.False(first > second);
                Assert.False(first >= second);
            }

            [Fact]
            public void ShouldReturnCorrectValueWhenValuesFirstIsLower()
            {
                var first = new Percentage(100);
                var second = new Percentage(50);

                Assert.False(first == second);
                Assert.True(first != second);
                Assert.False(first < second);
                Assert.False(first <= second);
                Assert.True(first > second);
                Assert.True(first >= second);
            }

            [Fact]
            public void ShouldReturnCorrectValueWhenMultiplying()
            {
                Percentage percentage = default;
                Assert.Equal(0, 10 * percentage);

                percentage = new Percentage(50);
                Assert.Equal(5, 10 * percentage);

                percentage = new Percentage(200);
                Assert.Equal(20.0, 10.0 * percentage);

                percentage = new Percentage(25);
                Assert.Equal(2.5, 10.0 * percentage);
            }
        }
    }
}
