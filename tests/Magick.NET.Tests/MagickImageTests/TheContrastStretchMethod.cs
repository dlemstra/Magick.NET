// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheContrastStretchMethod
    {
        [Fact]
        public void ShouldImproveTheContrast()
        {
            using var image = new MagickImage(Files.Builtin.Wizard);
            image.ContrastStretch(new Percentage(50), new Percentage(80));
            image.Alpha(AlphaOption.Opaque);

            ColorAssert.Equal(MagickColors.Black, image, 160, 300);
            ColorAssert.Equal(MagickColors.Red, image, 325, 175);
        }

        [Fact]
        public void ShouldImproveTheContrastForTheSpecifiedChannels()
        {
            using var image = new MagickImage(Files.Builtin.Wizard);
            image.ContrastStretch(new Percentage(50), new Percentage(80), Channels.Red);
            image.Alpha(AlphaOption.Opaque);

            ColorAssert.Equal(new MagickColor("#005bc9ff"), image, 160, 300);
            ColorAssert.Equal(new MagickColor("#fffcdcff"), image, 325, 175);
        }
    }
}
