// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

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
