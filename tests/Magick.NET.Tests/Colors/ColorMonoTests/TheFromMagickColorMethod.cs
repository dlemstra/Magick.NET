// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Colors;
using Xunit;

namespace Magick.NET.Tests;

public partial class ColorMonoTests
{
    public class TheFromMagickColorMethod
    {
        [Fact]
        public void ShouldReturnNullWhenValueIsNull()
        {
            var result = ColorMono.FromMagickColor(null!);

            Assert.Null(result);
        }

        [Fact]
        public void ShouldInitializeTheProperties()
        {
            var color = MagickColors.Black;
            var grayColor = ColorMono.FromMagickColor(color);

            Assert.Equal(grayColor, (ColorMono?)color);
        }
    }
}
