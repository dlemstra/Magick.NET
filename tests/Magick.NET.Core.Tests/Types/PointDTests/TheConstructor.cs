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

namespace Magick.NET.Core.Tests
{
    public partial class PointDTests
    {
        public class TheConstructor
        {
            [Fact]
            public void ShouldThrowExceptionWhenValueIsNull()
            {
                Assert.Throws<ArgumentNullException>("value", () => { new PointD(null); });
            }

            [Fact]
            public void ShouldThrowExceptionWhenValueIsEmpty()
            {
                Assert.Throws<ArgumentException>("value", () => { new PointD(string.Empty); });
            }

            [Fact]
            public void ShouldThrowExceptionWhenValueIsInvalid()
            {
                Assert.Throws<ArgumentException>("value", () => { new PointD("1.0x"); });

                Assert.Throws<ArgumentException>("value", () => { new PointD("x1.0"); });

                Assert.Throws<ArgumentException>("value", () => { new PointD("ax1.0"); });

                Assert.Throws<ArgumentException>("value", () => { new PointD("1.0xb"); });

                Assert.Throws<ArgumentException>("value", () => { new PointD("1.0x6 magick"); });
            }

            [Fact]
            public void ShouldSetTheXAndYToZeroByDefault()
            {
                PointD point = default;
                Assert.Equal(0.0, point.X);
                Assert.Equal(0.0, point.Y);
            }

            [Fact]
            public void ShouldSetTheXAndYValue()
            {
                var point = new PointD(5, 10);
                Assert.Equal(5.0, point.X);
                Assert.Equal(10.0, point.Y);
            }

            [Fact]
            public void ShouldUseTheXValueWhenTValueIsNotSet()
            {
                var point = new PointD(5);
                Assert.Equal(5.0, point.X);
                Assert.Equal(5.0, point.Y);
            }

            [Fact]
            public void ShouldSetTheValuesFromString()
            {
                var point = new PointD("1.0x2.5");
                Assert.Equal(1.0, point.X);
                Assert.Equal(2.5, point.Y);
                Assert.Equal("1x2.5", point.ToString());
            }
        }
    }
}
