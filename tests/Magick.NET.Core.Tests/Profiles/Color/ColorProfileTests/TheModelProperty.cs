// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests
{
    public partial class ColorProfileTests
    {
        public class TheModelProperty
        {
            [Fact]
            public void ShouldReturnTheCorrectValue()
            {
                Assert.Null(ColorProfile.AdobeRGB1998.Model);
                Assert.Null(ColorProfile.AppleRGB.Model);
                Assert.Null(ColorProfile.CoatedFOGRA39.Model);
                Assert.Null(ColorProfile.ColorMatchRGB.Model);
                Assert.Equal("IEC 61966-2.1 Default RGB colour space - sRGB", ColorProfile.SRGB.Model);
                Assert.Null(ColorProfile.USWebCoatedSWOP.Model);
            }
        }
    }
}
