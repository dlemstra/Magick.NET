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
    public partial class DensityTests
    {
        public class TheChangeUnitsMethod
        {
            [Fact]
            public void ShouldNotConvertPixelsPerCentimeterToPixelsPerCentimeter()
            {
                var density = new Density(1, 2, DensityUnit.PixelsPerCentimeter);

                var newDensity = density.ChangeUnits(DensityUnit.PixelsPerCentimeter);

                Assert.Equal(1, newDensity.X);
                Assert.Equal(2, newDensity.Y);
                Assert.Equal(DensityUnit.PixelsPerCentimeter, newDensity.Units);
            }

            [Fact]
            public void ShouldConvertPixelsPerCentimeterToPixelsPerInch()
            {
                var density = new Density(1, 2, DensityUnit.PixelsPerCentimeter);

                var newDensity = density.ChangeUnits(DensityUnit.PixelsPerInch);

                Assert.Equal(2.54, newDensity.X);
                Assert.Equal(5.08, newDensity.Y);
                Assert.Equal(DensityUnit.PixelsPerInch, newDensity.Units);
            }

            [Fact]
            public void ShouldNotConvertPixelsPerCentimeterToUndefined()
            {
                var density = new Density(1, 2, DensityUnit.PixelsPerCentimeter);

                var newDensity = density.ChangeUnits(DensityUnit.Undefined);

                Assert.Equal(1, newDensity.X);
                Assert.Equal(2, newDensity.Y);
                Assert.Equal(DensityUnit.Undefined, newDensity.Units);
            }

            [Fact]
            public void ShouldConvertPixelsPerInchToPixelsPerCentimeter()
            {
                var density = new Density(2.54, 5.08, DensityUnit.PixelsPerInch);

                var newDensity = density.ChangeUnits(DensityUnit.PixelsPerCentimeter);

                Assert.Equal(1, newDensity.X);
                Assert.Equal(2, newDensity.Y);
                Assert.Equal(DensityUnit.PixelsPerCentimeter, newDensity.Units);
            }

            [Fact]
            public void ShouldNotConvertPixelsPerInchToPixelsPerInch()
            {
                var density = new Density(1, 2, DensityUnit.PixelsPerInch);

                var newDensity = density.ChangeUnits(DensityUnit.PixelsPerInch);

                Assert.Equal(1, newDensity.X);
                Assert.Equal(2, newDensity.Y);
                Assert.Equal(DensityUnit.PixelsPerInch, newDensity.Units);
            }

            [Fact]
            public void ShouldNotConvertPixelsPerInchToUndefined()
            {
                var density = new Density(1, 2, DensityUnit.PixelsPerInch);

                var newDensity = density.ChangeUnits(DensityUnit.Undefined);

                Assert.Equal(1, newDensity.X);
                Assert.Equal(2, newDensity.Y);
                Assert.Equal(DensityUnit.Undefined, newDensity.Units);
            }

            [Fact]
            public void ShouldNotConvertUndefinedToPixelsPerCentimeter()
            {
                var density = new Density(1, 2, DensityUnit.Undefined);

                var newDensity = density.ChangeUnits(DensityUnit.PixelsPerCentimeter);

                Assert.Equal(1, newDensity.X);
                Assert.Equal(2, newDensity.Y);
                Assert.Equal(DensityUnit.PixelsPerCentimeter, newDensity.Units);
            }

            [Fact]
            public void ShouldNotConvertUndefinedToPixelsPerInch()
            {
                var density = new Density(1, 2, DensityUnit.Undefined);

                var newDensity = density.ChangeUnits(DensityUnit.PixelsPerInch);

                Assert.Equal(1, newDensity.X);
                Assert.Equal(2, newDensity.Y);
                Assert.Equal(DensityUnit.PixelsPerInch, newDensity.Units);
            }

            [Fact]
            public void ShouldNotConvertUndefinedToUndefined()
            {
                var density = new Density(1, 2, DensityUnit.Undefined);

                var newDensity = density.ChangeUnits(DensityUnit.Undefined);

                Assert.Equal(1, newDensity.X);
                Assert.Equal(2, newDensity.Y);
                Assert.Equal(DensityUnit.Undefined, newDensity.Units);
            }
        }
    }
}
