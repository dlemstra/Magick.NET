// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheNegateMethod
    {
        [Fact]
        public void ShouldNegateTheImage()
        {
            using var image = new MagickImage("xc:white", 1, 1);
            image.Negate();

            ColorAssert.Equal(MagickColors.Black, image, 0, 0);
        }

        [Fact]
        public void ShouldNegateTheSpecifiedChannels()
        {
            using var image = new MagickImage("xc:white", 1, 1);
            image.Negate(Channels.Red);

            ColorAssert.Equal(MagickColors.Aqua, image, 0, 0);
        }
    }
}
