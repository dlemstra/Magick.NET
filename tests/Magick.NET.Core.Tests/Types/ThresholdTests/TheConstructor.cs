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
    public partial class ThresholdTests
    {
        public class TheConstructor
        {
            [Fact]
            public void ShouldSetTheMinimumAndMaximumToZeroByDefault()
            {
                Threshold point = default;
                Assert.Equal(0.0, point.Minimum);
                Assert.Equal(0.0, point.Maximum);
            }

            [Fact]
            public void ShouldSetTheMinimumAndMaximumValue()
            {
                var point = new Threshold(5, 10);
                Assert.Equal(5.0, point.Minimum);
                Assert.Equal(10.0, point.Maximum);
            }

            [Fact]
            public void ShouldUseTheMinimumValueWhenTValueIsNotSet()
            {
                var point = new Threshold(5);
                Assert.Equal(5.0, point.Minimum);
                Assert.Equal(0.0, point.Maximum);
            }
        }
    }
}
