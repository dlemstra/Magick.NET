// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheChopHorizontalMethod
    {
        [Fact]
        public void ShouldChopTheImageHorizontaly()
        {
            using var image = new MagickImage(Files.Builtin.Wizard);
            image.ChopHorizontal(10, 200);

            Assert.Equal(280U, image.Width);
            Assert.Equal(640U, image.Height);
        }
    }
}
