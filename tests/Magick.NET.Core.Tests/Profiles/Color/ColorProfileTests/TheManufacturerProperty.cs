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
    public partial class ColorProfileTests
    {
        public class TheManufacturerProperty
        {
            [Fact]
            public void ShouldReturnTheCorrectValue()
            {
                Assert.Null(ColorProfile.AdobeRGB1998.Manufacturer);
                Assert.Null(ColorProfile.AppleRGB.Manufacturer);
                Assert.Null(ColorProfile.CoatedFOGRA39.Manufacturer);
                Assert.Null(ColorProfile.ColorMatchRGB.Manufacturer);
                Assert.Equal("IEC http://www.iec.ch", ColorProfile.SRGB.Manufacturer);
                Assert.Null(ColorProfile.USWebCoatedSWOP.Manufacturer);
            }
        }
    }
}
