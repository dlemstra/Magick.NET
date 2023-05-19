// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickSettingsTests
{
    public class TheStrokeColorProperty
    {
        [Fact]
        public void ShouldDefaultToTransparentWhite()
        {
            using var image = new MagickImage();

            Assert.Equal(new MagickColor(Quantum.Max, Quantum.Max, Quantum.Max, 0), image.Settings.StrokeColor);
        }
    }
}
