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

namespace Magick.NET.Tests
{
    public partial class MagickNETTests
    {
        public partial class TheVersionProperty
        {
            [Fact]
            public void ShouldContainTheCorrectPlatform()
            {
#if PLATFORM_AnyCPU
                Assert.Contains("AnyCPU", MagickNET.Version);
#elif PLATFORM_x64
                Assert.Contains("x64", MagickNET.Version);
#else
                Assert.Contains("x86", MagickNET.Version);
#endif
            }

            [Fact]
            public void ShouldContainCorrectQuantum()
            {
#if Q8
                Assert.Contains("Q8", MagickNET.Version);
#elif Q16
                Assert.Contains("Q16", MagickNET.Version);
#else
                Assert.Contains("Q16-HDRI", MagickNET.Version);
#endif
            }

            [Fact]
            public void ShouldContainTheCorrectFramework()
            {
#if NETCORE
                Assert.Contains("netstandard20", MagickNET.Version);
#else
                Assert.Contains("net40", MagickNET.Version);
#endif
            }
        }
    }
}
