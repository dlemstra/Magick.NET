// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheAnimationDelayProperty
    {
        [Fact]
        public void ShouldChangeTheAnimationDelay()
        {
            using var image = new MagickImage();
            image.AnimationDelay = 60;

            Assert.Equal(60U, image.AnimationDelay);
        }

        [Fact]
        public void ShouldAllowZeroValue()
        {
            using var image = new MagickImage();
            image.AnimationDelay = 60;
            image.AnimationDelay = 0;

            Assert.Equal(0U, image.AnimationDelay);
        }
    }
}
