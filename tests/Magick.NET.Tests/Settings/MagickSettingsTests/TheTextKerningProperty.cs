// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickSettingsTests
{
    public class TheTextKerningProperty
    {
        [Fact]
        public void ShouldDefaultToZero()
        {
            using var image = new MagickImage();

            Assert.Equal(0, image.Settings.TextKerning);
        }

        [Fact]
        public void ShouldBeUsedWhenRenderingText()
        {
            using var image = new MagickImage();
            image.Settings.TextKerning = 10;
            image.Read("label:First");

            Assert.Equal(65U, image.Width);
            Assert.Equal(15U, image.Height);

            image.Settings.TextKerning = 20;
            image.Read("label:First");

            Assert.Equal(105U, image.Width);
            Assert.Equal(15U, image.Height);
        }
    }
}
