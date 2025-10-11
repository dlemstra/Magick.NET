// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests;

public partial class ColorProfileTests
{
    public class TheModelProperty
    {
        [Fact]
        public void ShouldReturnTheCorrectValue()
        {
            Assert.Null(ColorProfiles.AdobeRGB1998.Model);
            Assert.Null(ColorProfiles.AppleRGB.Model);
            Assert.Null(ColorProfiles.CoatedFOGRA39.Model);
            Assert.Null(ColorProfiles.ColorMatchRGB.Model);
            Assert.Equal("IEC 61966-2.1 Default RGB colour space - sRGB", ColorProfiles.SRGB.Model);
            Assert.Null(ColorProfiles.USWebCoatedSWOP.Model);
        }
    }
}
