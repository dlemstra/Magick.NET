// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests;

public partial class ColorProfileTests
{
    public class TheDescriptionProperty
    {
        [Fact]
        public void ShouldReturnTheCorrectValue()
        {
            Assert.Equal("Adobe RGB (1998)", ColorProfiles.AdobeRGB1998.Description);
            Assert.Equal("Apple RGB", ColorProfiles.AppleRGB.Description);
            Assert.Equal("Coated FOGRA39 (ISO 12647-2:2004)", ColorProfiles.CoatedFOGRA39.Description);
            Assert.Equal("ColorMatch RGB", ColorProfiles.ColorMatchRGB.Description);
            Assert.Equal("sRGB IEC61966-2.1", ColorProfiles.SRGB.Description);
            Assert.Equal("U.S. Web Coated (SWOP) v2", ColorProfiles.USWebCoatedSWOP.Description);
        }
    }
}
