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

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace Magick.NET.Tests
{
    public partial class ResourceLimitsTests
    {
        public class TheWidthProperty
        {
            [Fact]
            public void ShouldHaveTheCorrectValue()
            {
                if (OperatingSystem.Is64Bit)
                {
                    Assert.Equal(1844674407370955161U / sizeof(QuantumType), ResourceLimits.Width);
                }
                else
                {
                    Assert.Equal(429496729U / sizeof(QuantumType), ResourceLimits.Width);
                }
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
