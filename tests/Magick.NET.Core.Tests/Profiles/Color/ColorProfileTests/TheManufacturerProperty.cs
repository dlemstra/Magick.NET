// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests;

public partial class ColorProfileTests
{
    public class TheManufacturerProperty
    {
        [Fact]
        public void ShouldReturnTheCorrectValue()
        {
            Assert.Null(ColorProfiles.AdobeRGB1998.Manufacturer);
            Assert.Null(ColorProfiles.AppleRGB.Manufacturer);
            Assert.Null(ColorProfiles.CoatedFOGRA39.Manufacturer);
            Assert.Null(ColorProfiles.ColorMatchRGB.Manufacturer);
            Assert.Equal("IEC http://www.iec.ch", ColorProfiles.SRGB.Manufacturer);
            Assert.Null(ColorProfiles.USWebCoatedSWOP.Manufacturer);
        }
    }
}
