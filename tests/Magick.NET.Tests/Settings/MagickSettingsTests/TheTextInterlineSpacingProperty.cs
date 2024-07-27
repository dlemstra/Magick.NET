// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickSettingsTests
{
    public class TheTextInterlineSpacingProperty
    {
        [Fact]
        public void ShouldDefaultToZero()
        {
            using var image = new MagickImage();

            Assert.Equal(0, image.Settings.TextInterlineSpacing);
        }

        [Fact]
        public void ShouldBeUsedWhenRenderingText()
        {
            using var image = new MagickImage();
            image.Settings.TextInterlineSpacing = 10;
            image.Read("label:First\nSecond");

            Assert.Equal(42U, image.Width);
            Assert.Equal(39U, image.Height);

            image.Settings.TextInterlineSpacing = 20;
            image.Read("label:First\nSecond");

            Assert.Equal(42U, image.Width);
            Assert.Equal(49U, image.Height);
        }
    }
}
