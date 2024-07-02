// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class PngWriteDefinesTests
{
    public class ThePreserveColorMapPropety
    {
        [Fact]
        public void ShouldSetTheDefineWhenSetToTrue()
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new PngWriteDefines
            {
                PreserveColorMap = true,
            });

            Assert.Equal("true", image.Settings.GetDefine(MagickFormat.Png, "preserve-colormap"));
        }

        [Fact]
        public void ShouldNotSetTheDefineWhenSetToFalse()
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new PngWriteDefines
            {
                PreserveColorMap = false,
            });

            Assert.Null(image.Settings.GetDefine(MagickFormat.Png, "preserve-colormap"));
        }
    }
}
