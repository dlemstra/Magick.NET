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
    public partial class PercentageTests
    {
        public class TheConstructor
        {
            [Fact]
            public void ShouldDefaultToZero()
            {
                Percentage percentage = default;
                Assert.Equal("0%", percentage.ToString());
            }

            [Fact]
            public void ShouldSetValue()
            {
                var percentage = new Percentage(50);
                Assert.Equal("50%", percentage.ToString());
            }

            [Fact]
            public void ShouldHandleValueAbove100()
            {
                var percentage = new Percentage(200.0);
                Assert.Equal("200%", percentage.ToString());
            }

            [Fact]
            public void ShouldHandleNegativeValue()
            {
                var percentage = new Percentage(-25);
                Assert.Equal("-25%", percentage.ToString());
            }
        }
    }
}
