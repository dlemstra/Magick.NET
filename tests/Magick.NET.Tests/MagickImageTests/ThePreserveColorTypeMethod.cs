// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class ThePreserveColorTypeMethod
    {
        [Fact]
        public void ShouldPreserveTheColorTypeWhenWritingImage()
        {
            using var image = new MagickImage(Files.WireframeTIF);

            Assert.Equal(ColorType.TrueColor, image.ColorType);

            image.PreserveColorType();

            var bytes = image.ToByteArray(MagickFormat.Psd);

            using var result = new MagickImage(bytes);

            Assert.Equal(ColorType.TrueColor, result.ColorType);
        }
    }
}
