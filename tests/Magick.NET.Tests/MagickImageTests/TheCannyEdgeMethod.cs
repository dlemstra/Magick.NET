// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheCannyEdgeMethod
    {
        [Fact]
        public void ShouldDetectEdgesInImage()
        {
            using var image = new MagickImage(Files.ConnectedComponentsPNG);
            image.Threshold(new Percentage(50));

            image.CannyEdge();

            ColorAssert.Equal(MagickColors.White, image, 92, 585);
            ColorAssert.Equal(MagickColors.Black, image, 93, 585);
            ColorAssert.Equal(MagickColors.White, image, 232, 670);
            ColorAssert.Equal(MagickColors.Black, image, 233, 670);
        }
    }
}
