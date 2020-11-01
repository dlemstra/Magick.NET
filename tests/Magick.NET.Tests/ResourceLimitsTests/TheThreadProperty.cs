// Copyright 2013-2020 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
using Xunit.Sdk;

namespace Magick.NET.Tests
{
    public partial class ResourceLimitsTests
    {
        public class TheThreadProperty
        {
            [Fact]
            public void ShouldHaveTheCorrectValue()
            {
                if (ResourceLimits.Thread < 1U)
                    throw new XunitException("Invalid thread limit: " + ResourceLimits.Thread);
            }

            [Fact]
            public void ShouldReturnTheCorrectValueWhenChanged()
            {
                TestHelper.ExecuteInsideLock(() =>
                {
#if OPENMP
                    var thread = ResourceLimits.Thread;

                    Assert.NotEqual(1U, ResourceLimits.Thread);
                    ResourceLimits.Thread = 1U;
                    Assert.Equal(1U, ResourceLimits.Thread);
                    ResourceLimits.Thread = thread;
#else
                    Assert.Equal(1U, ResourceLimits.Thread);
                    ResourceLimits.Thread = 2U;
                    Assert.Equal(1U, ResourceLimits.Thread);
#endif
                });
            }
        }
    }
}
