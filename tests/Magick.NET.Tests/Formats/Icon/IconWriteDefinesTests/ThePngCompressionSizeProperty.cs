// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class IconWriteDefinesTests
{
    public class PngCompressionSize
    {
        [Fact]
        public void ShouldSetTheDefine()
        {
            var defines = new IconWriteDefines
            {
                PngCompressionSize = 64,
            };

            using var image = new MagickImage();
            image.Settings.SetDefines(defines);

            Assert.Equal("64", image.Settings.GetDefine(MagickFormat.Icon, "png-compression-size"));
        }
    }
}
