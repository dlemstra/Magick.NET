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

namespace Magick.NET.Core.Tests
{
    public partial class DensityTests
    {
        public class TheToStringMethod
        {
            [Fact]
            public void ShouldReturnTheCorrectValueForPixelsPerCentimeterUnits()
            {
                var density = new Density(1, 2, DensityUnit.PixelsPerCentimeter);

                Assert.Equal("1x2 cm", density.ToString());
            }

            [Fact]
            public void ShouldReturnTheCorrectValueForPixelsPerInchUnits()
            {
                var density = new Density(1, 2, DensityUnit.PixelsPerInch);

                Assert.Equal("1x2 inch", density.ToString());
            }

            [Fact]
            public void ShouldReturnTheCorrectValueForUndefinedUnits()
            {
                var density = new Density(1, 2, DensityUnit.Undefined);

                Assert.Equal("1x2", density.ToString());
            }

            [Fact]
            public void ShouldNotConvertTheValueWhenUnitsMatch()
            {
                var density = new Density(1, 2, DensityUnit.PixelsPerCentimeter);

                Assert.Equal("1x2 cm", density.ToString(DensityUnit.PixelsPerCentimeter));
            }

            [Fact]
            public void ShouldNotAddUnitsWhenUndefinedIsSpecified()
            {
                var density = new Density(1, 2, DensityUnit.PixelsPerCentimeter);

                Assert.Equal("1x2", density.ToString(DensityUnit.Undefined));
            }

            [Fact]
            public void ShouldConvertPixelsPerInchToPixelsPerCentimeter()
            {
                var density = new Density(2.54, 5.08, DensityUnit.PixelsPerInch);

                Assert.Equal("1x2 cm", density.ToString(DensityUnit.PixelsPerCentimeter));
            }

            [Fact]
            public void ShouldConvertPixelsPerCentimeterToPixelsPerInch()
            {
                var density = new Density(1, 2, DensityUnit.PixelsPerCentimeter);

                Assert.Equal("2.54x5.08 inch", density.ToString(DensityUnit.PixelsPerInch));
            }
        }
    }
}
