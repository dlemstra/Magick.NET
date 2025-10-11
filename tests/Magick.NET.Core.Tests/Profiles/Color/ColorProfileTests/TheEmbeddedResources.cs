// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests;

public partial class ColorProfileTests
{
    public class TheEmbeddedResources
    {
        public void ShouldHaveTheCorrectValue()
        {
            TestEmbeddedResource(ColorProfiles.AdobeRGB1998);
            TestEmbeddedResource(ColorProfiles.AppleRGB);
            TestEmbeddedResource(ColorProfiles.CoatedFOGRA39);
            TestEmbeddedResource(ColorProfiles.ColorMatchRGB);
            TestEmbeddedResource(ColorProfiles.SRGB);
            TestEmbeddedResource(ColorProfiles.USWebCoatedSWOP);
        }

        private static void TestEmbeddedResource(ColorProfile profile)
        {
            Assert.NotNull(profile);
            Assert.Equal("icc", profile.Name);
        }
    }
}
