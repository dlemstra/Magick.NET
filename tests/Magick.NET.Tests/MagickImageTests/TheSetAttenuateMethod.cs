// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheSetAttenuateMethod
    {
        [Fact]
        public void ShouldSetImageArtifact()
        {
            using var image = new MagickImage();
            image.SetAttenuate(5.6);

            Assert.Equal("5.6", image.GetArtifact("attenuate"));
        }
    }
}
