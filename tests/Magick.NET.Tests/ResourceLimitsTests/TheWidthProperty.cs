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
    public partial class ResourceLimitsTests
    {
        public class TheWidthProperty
        {
            [Fact]
            public void ShouldHaveTheCorrectValue()
            {
                Assert.Equal(429496729U, ResourceLimits.Width);
            }

            [Fact]
            public void ShouldReturnTheCorrectValueWhenChanged()
            {
                TestHelper.ExecuteInsideLock(() =>
                {
                    var width = ResourceLimits.Width;

                    ResourceLimits.Width = 200000U;
                    Assert.Equal(200000U, ResourceLimits.Width);
                    ResourceLimits.Width = width;
                });
            }
        }
    }
}
