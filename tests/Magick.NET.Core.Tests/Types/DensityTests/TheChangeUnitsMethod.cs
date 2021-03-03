// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

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
