// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class HeicReadDefinesTests
{
    public class TheConstructor
    {
        [Fact]
        public void ShouldNotSetAnyDefines()
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new HeicReadDefines());

            Assert.Null(image.Settings.GetDefine(MagickFormat.Heic, "chroma-upsampling"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.Heic, "depth-image"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.Heic, "preserve-orientation"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.Heic, "max-bayer-pattern-pixels"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.Heic, "max-children-per-box"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.Heic, "max-components"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.Heic, "max-iloc-extents-per-item"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.Heic, "max-items"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.Heic, "max-number-of-tiles"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.Heic, "max-size-entity-group"));
        }
    }
}
