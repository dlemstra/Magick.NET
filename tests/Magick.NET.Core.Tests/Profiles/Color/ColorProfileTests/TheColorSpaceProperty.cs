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

namespace Magick.NET.Core.Tests
{
    public partial class ColorProfileTests
    {
        public class TheColorSpaceProperty
        {
            [Fact]
            public void ShouldReturnTheCorrectValue()
            {
                Assert.Equal(ColorSpace.sRGB, ColorProfile.AdobeRGB1998.ColorSpace);
                Assert.Equal(ColorSpace.sRGB, ColorProfile.AppleRGB.ColorSpace);
                Assert.Equal(ColorSpace.CMYK, ColorProfile.CoatedFOGRA39.ColorSpace);
                Assert.Equal(ColorSpace.sRGB, ColorProfile.ColorMatchRGB.ColorSpace);
                Assert.Equal(ColorSpace.sRGB, ColorProfile.SRGB.ColorSpace);
                Assert.Equal(ColorSpace.CMYK, ColorProfile.USWebCoatedSWOP.ColorSpace);
            }
        }
    }
}
