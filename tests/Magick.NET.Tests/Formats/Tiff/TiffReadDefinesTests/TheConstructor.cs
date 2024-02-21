// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class TiffReadDefinesTests
{
    public class TheConstructor
    {
        [Fact]
        public void ShouldNotSetAnyDefine()
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new TiffReadDefines());

            Assert.Null(image.Settings.GetDefine(MagickFormat.Tiff, "assume-alpha"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.Tiff, "exif-properties"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.Tiff, "ignore-layers"));
            Assert.Null(image.Settings.GetDefine(MagickFormat.Tiff, "ignore-tags"));
        }
    }
}
