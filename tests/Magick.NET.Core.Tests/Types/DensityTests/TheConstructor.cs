// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests;

public partial class DensityTests
{
    public class TheConstructor
    {
        [Fact]
        public void ShouldThrowExceptionWhenValueIsNull()
            => Assert.Throws<ArgumentNullException>("value", () => new Density(null!));

        [Fact]
        public void ShouldThrowExceptionWhenValueIsEmpty()
            => Assert.Throws<ArgumentException>("value", () => new Density(string.Empty));

        [Fact]
        public void ShouldThrowExceptionWhenValueIsInvalid()
        {
            Assert.Throws<ArgumentException>("value", () => { new Density("1.0x"); });
            Assert.Throws<ArgumentException>("value", () => { new Density("x1.0"); });
            Assert.Throws<ArgumentException>("value", () => { new Density("ax1.0"); });
            Assert.Throws<ArgumentException>("value", () => { new Density("1.0xb"); });
            Assert.Throws<ArgumentException>("value", () => { new Density("1.0x6 magick"); });
        }

        [Fact]
        public void ShouldUsePixelsPerInchAsTheDefaultUnits()
        {
            var density = new Density(5);
            Assert.Equal(5.0, density.X);
            Assert.Equal(5.0, density.Y);
            Assert.Equal(DensityUnit.PixelsPerInch, density.Units);
        }

        [Fact]
        public void ShouldSetTheUnits()
        {
            var density = new Density(8.5, DensityUnit.PixelsPerCentimeter);
            Assert.Equal(8.5, density.X);
            Assert.Equal(8.5, density.Y);
            Assert.Equal(DensityUnit.PixelsPerCentimeter, density.Units);
        }

        [Fact]
        public void ShouldSetTheXAndYDensity()
        {
            var density = new Density(2, 3);
            Assert.Equal(2.0, density.X);
            Assert.Equal(3.0, density.Y);
            Assert.Equal(DensityUnit.PixelsPerInch, density.Units);
        }

        [Fact]
        public void ShouldSetTheXAndYDensityAndUnits()
        {
            var density = new Density(2.2, 3.3, DensityUnit.Undefined);
            Assert.Equal(2.2, density.X);
            Assert.Equal(3.3, density.Y);
            Assert.Equal(DensityUnit.Undefined, density.Units);
        }

        [Fact]
        public void ShouldSetTheXAndYDensityFromTheValue()
        {
            var density = new Density("1.0x2.5");
            Assert.Equal(1.0, density.X);
            Assert.Equal(2.5, density.Y);
            Assert.Equal(DensityUnit.Undefined, density.Units);
            Assert.Equal("1x2.5", density.ToString());
        }

        [Fact]
        public void ShouldSetTheCorrectUnitsForCm()
        {
            var density = new Density("2.5x1.0 cm");
            Assert.Equal(2.5, density.X);
            Assert.Equal(1.0, density.Y);
            Assert.Equal(DensityUnit.PixelsPerCentimeter, density.Units);
            Assert.Equal("2.5x1 cm", density.ToString());
        }

        [Fact]
        public void ShouldSetTheCorrectUnitsForInch()
        {
            var density = new Density("2.5x1.0 inch");
            Assert.Equal(2.5, density.X);
            Assert.Equal(1.0, density.Y);
            Assert.Equal(DensityUnit.PixelsPerInch, density.Units);
            Assert.Equal("2.5x1 inch", density.ToString());
        }
    }
}
