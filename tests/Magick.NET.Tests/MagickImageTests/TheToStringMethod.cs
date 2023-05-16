// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheToStringMethod
    {
        [Fact]
        public void ShouldReturnTheStringRepresentationOfTheImage()
        {
            using var image = new MagickImage(Files.Builtin.Wizard);

            Assert.Equal("Gif 480x640 8-bit sRGB", image.ToString());

            image.Read(Files.TestPNG);

            Assert.Equal("Png 150x100 16-bit sRGB", image.ToString());
        }
    }
}
