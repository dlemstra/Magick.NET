// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class PngWriteDefinesTests
{
    public class TheIncludeChunksProperty
    {
        [Fact]
        public void ShouldSetTheDefine()
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new PngWriteDefines
            {
                IncludeChunks = PngChunkFlags.iTXt | PngChunkFlags.tEXt,
            });

            Assert.Equal("iTXt, tEXt", image.Settings.GetDefine(MagickFormat.Png, "include-chunks"));
        }

        [Fact]
        public void ShouldSetTheDefineNull()
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new PngWriteDefines
            {
                IncludeChunks = null,
            });

            Assert.Null(image.Settings.GetDefine(MagickFormat.Png, "include-chunks"));
        }
    }
}
