// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickSettingsTests
{
    public class TheFontStyleProperty
    {
        [Fact]
        public void ShouldDefaultToUndefined()
        {
            using var image = new MagickImage();

            Assert.Equal(FontStyleType.Undefined, image.Settings.FontStyle);
        }
    }
}
