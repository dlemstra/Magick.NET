// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public class ThePcxCoder
{
    [Fact]
    public void ShouldBeAbleToWriteOneBitImages()
    {
        using var input = new MagickImage(MagickColors.Purple, 1, 1);
        input.ColorType = ColorType.Bilevel;
        Assert.Equal(ClassType.Pseudo, input.ClassType);

        using var memoryStream = new MemoryStream();
        input.Write(memoryStream, MagickFormat.Pcx);
        memoryStream.Position = 0;

        using var output = new MagickImage(memoryStream);

        Assert.Equal(1U, output.Depth);
        Assert.Equal(ClassType.Pseudo, output.ClassType);
    }
}
