// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using ImageMagick;
using Xunit;
using Xunit.Sdk;

namespace Magick.NET.Tests
{
    public partial class ResourceLimitsTests
    {
        public class TheAreaProperty
        {
            [Fact]
            public void ShouldHaveTheCorrectValue()
            {
                if (ResourceLimits.Area < 100000000U)
                    throw new XunitException("Invalid memory limit: " + ResourceLimits.Area);
            }

            [Fact]
            public void ShouldReturnTheCorrectValueWhenChanged()
            {
                TestHelper.ExecuteInsideLock(() =>
                {
                    var area = ResourceLimits.Area;

                    ResourceLimits.Area = 10000000U;
                    Assert.Equal(10000000U, ResourceLimits.Area);
                    ResourceLimits.Area = area;
                });
            }
        }
    }
}
