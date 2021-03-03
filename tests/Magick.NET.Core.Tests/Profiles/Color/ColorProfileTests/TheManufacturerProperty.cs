// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

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
