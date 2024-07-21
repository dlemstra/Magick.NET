// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class DrawableTextAntialiasTests
{
    public class TheDisabledProperty
    {
        [Fact]
        public void ShouldReturnDrawableTextAntialiasThatIsEnabled()
        {
            var result = DrawableTextAntialias.Disabled;

            Assert.False(result.IsEnabled);
        }
    }
}
