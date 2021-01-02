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
    public partial class SignedRationalTests
    {
        public class TheConstructor
        {
            [Fact]
            public void ShouldSetTheProperties()
            {
                var rational = new SignedRational(7, -55);
                Assert.Equal(7, rational.Numerator);
                Assert.Equal(-55, rational.Denominator);
            }

            [Fact]
            public void ShouldSetThePropertiesWhenOnlyValueIsSpecified()
            {
                var rational = new SignedRational(7);
                Assert.Equal(7, rational.Numerator);
                Assert.Equal(1, rational.Denominator);
            }

            [Fact]
            public void ShouldSimplifyByDefault()
            {
                var rational = new SignedRational(-755, 100);
                Assert.Equal(-151, rational.Numerator);
                Assert.Equal(20, rational.Denominator);
            }

            [Fact]
            public void ShouldNotSimplifyWhenSpecified()
            {
                var rational = new SignedRational(-755, -100, false);
                Assert.Equal(-755, rational.Numerator);
                Assert.Equal(-100, rational.Denominator);
            }

            [Fact]
            public void ShouldHandleNegativeValue()
            {
                var rational = new SignedRational(-7.55);
                Assert.Equal(-151, rational.Numerator);
                Assert.Equal(20, rational.Denominator);
            }
        }
    }
}
