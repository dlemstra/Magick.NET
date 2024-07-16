// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class PngWriteDefinesTests
{
    public class TheBitDepthProperty
    {
        [Fact]
        public void ShouldSetTheDefine()
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new PngWriteDefines
            {
                BitDepth = 8U,
            });

            Assert.Equal("8", image.Settings.GetDefine(MagickFormat.Png, "bit-depth"));
        }

        [Fact]
        public void ShouldNotSetTheDefineWhenNull()
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new PngWriteDefines
            {
                BitDepth = null,
            });

            Assert.Null(image.Settings.GetDefine(MagickFormat.Png, "bit-depth"));
        }
    }
}
