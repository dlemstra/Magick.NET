// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class HeicWriteDefinesTests
{
    public class TheConstructor
    {
        [Fact]
        public void ShouldNotSetAnyDefines()
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new HeicWriteDefines());

            Assert.Null(image.Settings.GetDefine(MagickFormat.Heic, "chroma"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.Heic, "chroma-downsampling"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.Heic, "cicp"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.Heic, "clli"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.Heic, "preserve-cicp"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.Heic, "preserve-clli"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.Heic, "speed"));
        }
    }
}
