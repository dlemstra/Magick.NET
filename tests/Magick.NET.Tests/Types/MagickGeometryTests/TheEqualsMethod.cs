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

namespace Magick.NET.Tests
{
    public partial class MagickGeometryTests
    {
        public class TheEqualsMethod
        {
            [Fact]
            public void ShouldReturnFalseWhenInstanceIsNull()
            {
                var geometry = new MagickGeometry(10, 5);

                Assert.False(geometry.Equals(null));
            }

            [Fact]
            public void ShouldReturnTrueWhenInstanceIsTheSame()
            {
                var geometry = new MagickGeometry(10, 5);

                Assert.True(geometry.Equals(geometry));
            }

            [Fact]
            public void ShouldReturnTrueWhenObjectIsTheSame()
            {
                var geometry = new MagickGeometry(10, 5);

                Assert.True(geometry.Equals((object)geometry));
            }

            [Fact]
            public void ShouldReturnTrueWhenInstanceIsEqual()
            {
                var first = new MagickGeometry(10, 5);
                var second = new MagickGeometry(10, 5);

                Assert.True(first.Equals(second));
            }

            [Fact]
            public void ShouldReturnTrueWhenObjectIsEqual()
            {
                var first = new MagickGeometry(10, 5);
                var second = new MagickGeometry(10, 5);

                Assert.True(first.Equals((object)second));
            }

            [Fact]
            public void ShouldReturnFalseWhenInstanceIsNotEqual()
            {
                var first = new MagickGeometry(10, 5);
                var second = new MagickGeometry(5, 10);

                Assert.False(first.Equals(second));
            }

            [Fact]
            public void ShouldReturnFalseWhenObjectIsNotEqual()
            {
                var first = new MagickGeometry(10, 5);
                var second = new MagickGeometry(5, 10);

                Assert.False(first.Equals((object)second));
            }
        }
    }
}
