// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickSettingsTests
{
    public class TheFontPointSizeProperty
    {
        [Fact]
        public void ShouldDefaultToZero()
        {
            using var image = new MagickImage();

            Assert.Equal(0, image.Settings.FontPointsize);
        }
    }
}
