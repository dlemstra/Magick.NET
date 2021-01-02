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
        public class TheToStringMethod
        {
            [Fact]
            public void ShouldOnlyReturnWidthAndHeight()
            {
                var geometry = new MagickGeometry(10, 5);

                Assert.Equal("10x5", geometry.ToString());
            }

            [Fact]
            public void ShouldReturnCorrectValueForPositiveValues()
            {
                var geometry = new MagickGeometry(1, 2, 10, 20);

                Assert.Equal("10x20+1+2", geometry.ToString());
            }

            [Fact]
            public void ShouldReturnCorrectValueForNegativeValues()
            {
                var geometry = new MagickGeometry(-1, -2, 20, 10);

                Assert.Equal("20x10-1-2", geometry.ToString());
            }

            [Fact]
            public void ShouldReturnCorrectValueForIgnoreAspectRatio()
            {
                var geometry = new MagickGeometry(5, 10)
                {
                    IgnoreAspectRatio = true,
                };

                Assert.Equal("5x10!", geometry.ToString());
            }

            [Fact]
            public void ShouldReturnCorrectValueForLess()
            {
                var geometry = new MagickGeometry(2, 1, 10, 5)
                {
                    Less = true,
                };

                Assert.Equal("10x5+2+1<", geometry.ToString());
            }

            [Fact]
            public void ShouldReturnCorrectValueForGreater()
            {
                var geometry = new MagickGeometry(0, 10)
                {
                    Greater = true,
                };

                Assert.Equal("x10>", geometry.ToString());
            }

            [Fact]
            public void ShouldReturnCorrectValueForFillArea()
            {
                var geometry = new MagickGeometry(10, 15)
                {
                    FillArea = true,
                };

                Assert.Equal("10x15^", geometry.ToString());
            }

            [Fact]
            public void ShouldReturnCorrectValueForLimitPixels()
            {
                var geometry = new MagickGeometry(10, 0)
                {
                    LimitPixels = true,
                };

                Assert.Equal("10x@", geometry.ToString());
            }

            [Fact]
            public void ShouldReturnCorrectValueForAspectRation()
            {
                var geometry = new MagickGeometry(3, 2)
                {
                    AspectRatio = true,
                };

                Assert.Equal("3:2", geometry.ToString());
            }

            [Fact]
            public void ShouldSetGreaterAndIsPercentage()
            {
                var geometry = new MagickGeometry(new Percentage(50), new Percentage(0))
                {
                    Greater = true,
                };

                Assert.Equal("50%>", geometry.ToString());
            }
        }
    }
}
