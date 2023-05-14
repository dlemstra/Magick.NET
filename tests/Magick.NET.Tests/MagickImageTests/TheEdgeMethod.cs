// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheEdgeMethod
    {
        [Fact]
        public void ShouldHighlightEdges()
        {
            using var image = new MagickImage(Files.Builtin.Logo);

            ColorAssert.NotEqual(MagickColors.Black, image, 400, 295);
            ColorAssert.NotEqual(MagickColors.Blue, image, 455, 126);

            image.Edge(2);
            image.Clamp();

            ColorAssert.Equal(MagickColors.Black, image, 400, 295);
            ColorAssert.Equal(MagickColors.Blue, image, 455, 126);
        }
    }
}
