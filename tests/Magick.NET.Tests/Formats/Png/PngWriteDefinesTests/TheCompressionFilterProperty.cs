// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class PngWriteDefinesTests
{
    public class TheCompressionFilterProperty
    {
        [Fact]
        public void ShouldSetTheDefine()
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new PngWriteDefines
            {
                CompressionFilter = PngCompressionFilter.Paeth,
            });

            Assert.Equal("paeth", image.Settings.GetDefine(MagickFormat.Png, "compression-filter"));
        }

        [Fact]
        public void ShouldSetTheDefineNull()
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new PngWriteDefines
            {
                CompressionFilter = null,
            });

            Assert.Null(image.Settings.GetDefine(MagickFormat.Png, "compression-filter"));
        }
    }
}
