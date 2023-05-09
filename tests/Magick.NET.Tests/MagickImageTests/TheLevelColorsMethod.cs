// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheLevelColorsMethod
    {
        [Fact]
        public void ShouldLevelTheColors()
        {
            using (var image = new MagickImage(Files.MagickNETIconPNG))
            {
                image.LevelColors(MagickColors.Fuchsia, MagickColors.Goldenrod);
                ColorAssert.Equal(new MagickColor("#ffffbed54bc4"), image, 42, 75);
                ColorAssert.Equal(new MagickColor("#ffffffff0809"), image, 62, 75);
            }
        }
    }
}
