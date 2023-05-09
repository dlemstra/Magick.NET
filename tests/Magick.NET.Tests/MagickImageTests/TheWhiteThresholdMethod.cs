// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheWhiteThresholdMethod
    {
        [Fact]
        public void ShouldMakePixelsAboveThresholdWhite()
        {
            using (var image = new MagickImage(Files.MagickNETIconPNG))
            {
                image.WhiteThreshold(new Percentage(10));
                ColorAssert.Equal(MagickColors.White, image, 43, 74);
                ColorAssert.Equal(MagickColors.White, image, 60, 74);
            }
        }
    }
}
