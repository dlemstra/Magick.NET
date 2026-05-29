// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests.Coders;

public class TheEmfCoder
{
    [Fact]
    public void ShouldDecodeTheImage()
    {
        Assert.SkipUnless(Runtime.IsWindows, "File can only be read on Windows.");

        using var image = new MagickImage(Files.Coders.TestEMF);
        Assert.Equal(MagickFormat.Emf, image.Format);
        Assert.Equal(201U, image.Width);
        Assert.Equal(81U, image.Height);
    }
}
