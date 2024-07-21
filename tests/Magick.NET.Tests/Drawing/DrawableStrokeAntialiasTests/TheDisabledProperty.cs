// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick.Drawing;
using Xunit;

namespace Magick.NET.Tests;

public partial class DrawableStrokeAntialiasTests
{
    public class TheDisabledProperty
    {
        [Fact]
        public void ShouldReturnDrawableStrokeAntialiasThatIsEnabled()
        {
            var result = DrawableStrokeAntialias.Disabled;

            Assert.False(result.IsEnabled);
        }
    }
}
