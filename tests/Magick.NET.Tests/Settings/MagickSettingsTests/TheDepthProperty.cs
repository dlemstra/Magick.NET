// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickSettingsTests
{
    public class TheDepthProperty
    {
        [Fact]
        public void ShouldDefaultToZero()
        {
            using var image = new MagickImage();

            Assert.Equal(0U, image.Settings.Depth);
        }

        [Fact]
        public void ShouldChangeTheDepthOfTheOutputImage()
        {
            using var input = new MagickImage(Files.Builtin.Logo);
            input.Settings.Depth = 5;

            var bytes = input.ToByteArray(MagickFormat.Tga);

            using var output = new MagickImage(bytes, MagickFormat.Tga);

            Assert.Equal(5U, output.Depth);
        }
    }
}
