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
    public partial class NumberTests
    {
        public class TheOperators
        {
            [Fact]
            public void ShouldReturnTheCorrectValueWhenValuesAreEqual()
            {
                var first = new Number(10U);
                var second = new Number(10);

                Assert.True(first == second);
                Assert.False(first != second);
                Assert.False(first < second);
                Assert.True(first <= second);
                Assert.False(first > second);
                Assert.True(first >= second);
            }

            [Fact]
            public void ShouldReturnTheCorrectValueWhenValuesAreNotEqual()
            {
                var first = new Number(24);
                var second = new Number(42);

                Assert.False(first == second);
                Assert.True(first != second);
                Assert.True(first < second);
                Assert.True(first <= second);
                Assert.False(first > second);
                Assert.False(first >= second);
            }

            [Fact]
            public void ShouldBeAbleToImplicitCastFromInt()
            {
                Number number = 10;

                Assert.Equal(10U, (uint)number);
            }

            [Fact]
            public void ShouldBeAbleToImplicitCastFromUInt()
            {
                Number number = 10U;

                Assert.Equal(10, (ushort)number);
            }

            [Fact]
            public void ShouldBeAbleToImplicitCastFromShort()
            {
                Number number = (short)10;

                Assert.Equal(10U, (uint)number);
            }

            [Fact]
            public void ShouldBeAbleToImplicitCastFromUShort()
            {
                Number number = (ushort)10U;

                Assert.Equal(10, (ushort)number);
            }
        }
    }
}
