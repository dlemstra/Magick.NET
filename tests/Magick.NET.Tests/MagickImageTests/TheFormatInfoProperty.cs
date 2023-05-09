// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheFormatInfoProperty
    {
        [Fact]
        public void ShouldReturnTheFormatInformationOfTheImage()
        {
            using (var image = new MagickImage(Files.SnakewarePNG))
            {
                var info = image.FormatInfo;

                Assert.NotNull(info);
                Assert.Equal(MagickFormat.Png, info.Format);
                Assert.Equal("image/png", info.MimeType);
            }
        }
    }
}
