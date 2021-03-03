// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests
{
    public partial class ColorProfileTests
    {
        public class TheEmbeddedResources
        {
            public void ShouldHaveTheCorrectValue()
            {
                TestEmbeddedResource(ColorProfile.AdobeRGB1998);
                TestEmbeddedResource(ColorProfile.AppleRGB);
                TestEmbeddedResource(ColorProfile.CoatedFOGRA39);
                TestEmbeddedResource(ColorProfile.ColorMatchRGB);
                TestEmbeddedResource(ColorProfile.SRGB);
                TestEmbeddedResource(ColorProfile.USWebCoatedSWOP);
            }

            private static void TestEmbeddedResource(ColorProfile profile)
            {
                Assert.NotNull(profile);
                Assert.Equal("icc", profile.Name);
            }
        }
    }
}
