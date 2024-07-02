// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class PngWriteDefinesTests
{
    public class TheCompressionLevelProperty
    {
        [Fact]
        public void ShouldSetTheDefine()
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new PngWriteDefines
            {
                CompressionLevel = 5,
            });

            Assert.Equal("5", image.Settings.GetDefine(MagickFormat.Png, "compression-level"));
        }

        [Fact]
        public void ShouldNotSetTheDefineWhenNull()
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new PngWriteDefines
            {
                CompressionLevel = null,
            });

            Assert.Null(image.Settings.GetDefine(MagickFormat.Png, "compression-level"));
        }
    }
}
