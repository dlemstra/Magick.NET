// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick.Drawing;
using Xunit;

namespace Magick.NET.Tests;

public partial class DrawableTextAntialiasTests
{
    public class TheEnabledProperty
    {
        [Fact]
        public void ShouldReturnDrawableTextAntialiasThatIsEnabled()
        {
            var result = DrawableTextAntialias.Enabled;

            Assert.True(result.IsEnabled);
        }
    }
}
