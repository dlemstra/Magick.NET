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

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickGeometryTests
    {
        public partial class TheConstructor
        {
            [Fact]
            public void ShouldThrowExceptionWhenValueIsNull()
            {
                Assert.Throws<ArgumentNullException>("value", () => new MagickGeometry(null));
            }

            [Fact]
            public void ShouldThrowExceptionWhenValueIsEmpty()
            {
                Assert.Throws<ArgumentException>("value", () => new MagickGeometry(string.Empty));
            }

            [Fact]
            public void ShouldThrowExceptionWhenWidthIsNegative()
            {
                Assert.Throws<ArgumentException>("width", () => new MagickGeometry(-1, 0));
            }

            [Fact]
            public void ShouldThrowExceptionWhenHeightIsNegative()
            {
                Assert.Throws<ArgumentException>("height", () => new MagickGeometry(0, -1));
            }

            [Fact]
            public void ShouldSetIgnoreAspectRatio()
            {
                var geometry = new MagickGeometry("5x10!");

                Assert.Equal(0, geometry.X);
                Assert.Equal(0, geometry.Y);
                Assert.Equal(5, geometry.Width);
                Assert.Equal(10, geometry.Height);
                Assert.True(geometry.IgnoreAspectRatio);
            }

            [Fact]
            public void ShouldSetLess()
            {
                var geometry = new MagickGeometry("10x5+2+1<");

                Assert.Equal(2, geometry.X);
                Assert.Equal(1, geometry.Y);
                Assert.Equal(10, geometry.Width);
                Assert.Equal(5, geometry.Height);
                Assert.True(geometry.Less);
            }

            [Fact]
            public void ShouldSetGreater()
            {
                var geometry = new MagickGeometry("5x10>");

                Assert.Equal(0, geometry.X);
                Assert.Equal(0, geometry.Y);
                Assert.Equal(5, geometry.Width);
                Assert.Equal(10, geometry.Height);
                Assert.True(geometry.Greater);
            }

            [Fact]
            public void ShouldSetFillArea()
            {
                var geometry = new MagickGeometry("10x15^");

                Assert.Equal(0, geometry.X);
                Assert.Equal(0, geometry.Y);
                Assert.Equal(10, geometry.Width);
                Assert.Equal(15, geometry.Height);
                Assert.True(geometry.FillArea);
            }

            [Fact]
            public void ShouldSetLimitPixels()
            {
                var geometry = new MagickGeometry("10@");

                Assert.Equal(0, geometry.X);
                Assert.Equal(0, geometry.Y);
                Assert.Equal(10, geometry.Width);
                Assert.Equal(0, geometry.Height);
                Assert.True(geometry.LimitPixels);
            }

            [Fact]
            public void ShouldSetGreaterAndIsPercentage()
            {
                var geometry = new MagickGeometry("50%x0>");

                Assert.Equal(0, geometry.X);
                Assert.Equal(0, geometry.Y);
                Assert.Equal(50, geometry.Width);
                Assert.Equal(0, geometry.Height);
                Assert.True(geometry.IsPercentage);
                Assert.True(geometry.Greater);
            }

            [Fact]
            public void ShouldSetAspectRatio()
            {
                var geometry = new MagickGeometry("3:2");

                Assert.Equal(0, geometry.X);
                Assert.Equal(0, geometry.Y);
                Assert.Equal(3, geometry.Width);
                Assert.Equal(2, geometry.Height);
                Assert.True(geometry.AspectRatio);
            }

            [Fact]
            public void ShouldSetAspectRatioWithOnlyXOffset()
            {
                var geometry = new MagickGeometry("4:3+2");

                Assert.Equal(2, geometry.X);
                Assert.Equal(0, geometry.Y);
                Assert.Equal(4, geometry.Width);
                Assert.Equal(3, geometry.Height);
                Assert.True(geometry.AspectRatio);
            }

            [Fact]
            public void ShouldSetAspectRatioWithOffset()
            {
                var geometry = new MagickGeometry("4:3+2+1");

                Assert.Equal(2, geometry.X);
                Assert.Equal(1, geometry.Y);
                Assert.Equal(4, geometry.Width);
                Assert.Equal(3, geometry.Height);
                Assert.True(geometry.AspectRatio);
            }

            [Fact]
            public void ShouldSetAspectRatioWithNegativeOffset()
            {
                var geometry = new MagickGeometry("4:3-2+1");

                Assert.Equal(-2, geometry.X);
                Assert.Equal(1, geometry.Y);
                Assert.Equal(4, geometry.Width);
                Assert.Equal(3, geometry.Height);
                Assert.True(geometry.AspectRatio);
            }

            [Fact]
            public void ShouldSetWidthAndHeightWhenSizeIsSupplied()
            {
                var geometry = new MagickGeometry(5);

                Assert.Equal(0, geometry.X);
                Assert.Equal(0, geometry.Y);
                Assert.Equal(5, geometry.Width);
                Assert.Equal(5, geometry.Height);
            }

            [Fact]
            public void ShouldSetWidthAndHeight()
            {
                var geometry = new MagickGeometry(5, 10);

                Assert.Equal(0, geometry.X);
                Assert.Equal(0, geometry.Y);
                Assert.Equal(5, geometry.Width);
                Assert.Equal(10, geometry.Height);
            }

            [Fact]
            public void ShouldSetXAndY()
            {
                var geometry = new MagickGeometry(5, 10, 15, 20);

                Assert.Equal(5, geometry.X);
                Assert.Equal(10, geometry.Y);
                Assert.Equal(15, geometry.Width);
                Assert.Equal(20, geometry.Height);
            }

            [Fact]
            public void ShouldSetWidthAndHeightAndIsPercentage()
            {
                var geometry = new MagickGeometry(new Percentage(50.0), new Percentage(10.0));

                Assert.Equal(0, geometry.X);
                Assert.Equal(0, geometry.Y);
                Assert.Equal(50, geometry.Width);
                Assert.Equal(10, geometry.Height);
                Assert.True(geometry.IsPercentage);
            }

            [Fact]
            public void ShouldSetXAndYAndIsPercentage()
            {
                var geometry = new MagickGeometry(5, 10, (Percentage)15.0, (Percentage)20.0);

                Assert.Equal(5, geometry.X);
                Assert.Equal(10, geometry.Y);
                Assert.Equal(15, geometry.Width);
                Assert.Equal(20, geometry.Height);
                Assert.True(geometry.IsPercentage);
            }
        }
    }
}
