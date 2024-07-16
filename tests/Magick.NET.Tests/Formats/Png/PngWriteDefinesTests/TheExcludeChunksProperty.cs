// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class PngWriteDefinesTests
{
    public class TheExcludeChunksProperty
    {
        [Fact]
        public void ShouldSetTheDefine()
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new PngWriteDefines
            {
                ExcludeChunks = PngChunkFlags.bKGD | PngChunkFlags.iCCP,
            });

            Assert.Equal("bKGD, iCCP", image.Settings.GetDefine(MagickFormat.Png, "exclude-chunks"));
        }

        [Fact]
        public void ShouldNotSetTheDefineWhenNull()
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new PngWriteDefines
            {
                ExcludeChunks = null,
            });

            Assert.Null(image.Settings.GetDefine(MagickFormat.Png, "exclude-chunks"));
        }

        [Fact]
        public void ShouldSetTheDefineToNone()
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new PngWriteDefines
            {
                ExcludeChunks = PngChunkFlags.None,
            });

            Assert.Equal("None", image.Settings.GetDefine(MagickFormat.Png, "exclude-chunks"));
        }
    }
}
