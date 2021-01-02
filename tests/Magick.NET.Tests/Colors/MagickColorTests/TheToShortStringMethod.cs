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
    public partial class MagickColorTests
    {
        public class TheToShortStringMethod
        {
            [Fact]
            public void ShouldReturnTheCorrectString()
            {
                var color = new MagickColor(MagickColors.Red);
#if Q8
                Assert.Equal("#FF0000", color.ToShortString());
#else
                Assert.Equal("#FFFF00000000", color.ToShortString());
#endif
            }

            [Fact]
            public void ShouldIncludeTheAlphaChannelWhenNotFullyOpquery()
            {
                var color = new MagickColor(MagickColors.Red)
                {
                    A = 0,
                };

#if Q8
                Assert.Equal("#FF000000", color.ToShortString());
#else
                Assert.Equal("#FFFF000000000000", color.ToShortString());
#endif
            }

            [Fact]
            public void ShouldReturnTheCorrectStringForCmykColor()
            {
                var color = new MagickColor(0, Quantum.Max, 0, 0, Quantum.Max);
                Assert.Equal("cmyk(0," + Quantum.Max + ",0,0)", color.ToShortString());
            }
        }
    }
}
