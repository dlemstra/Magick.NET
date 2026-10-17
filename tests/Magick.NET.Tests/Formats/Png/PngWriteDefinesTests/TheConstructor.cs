// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class PngWriteDefinesTests
{
    public class TheConstructor
    {
        [Fact]
        public void ShouldNotSetAnyDefines()
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new PngWriteDefines());

            Assert.Null(image.Settings.GetDefine(MagickFormat.Png, "bit-depth"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.Png, "color-type"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.Png, "compression-filter"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.Png, "compression-level"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.Png, "compression-strategy"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.Png, "exclude-chunks"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.Png, "include-chunks"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.Png, "preserve-iCCP"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.Png, "preserve-colormap"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.Png, "tIME"));
        }
    }
}
