// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class HeicReadDefinesTests
{
    public class TheChromaUpsamplingProperty
    {
        [Theory]
        [InlineData(HeicChromaUpsampling.Bilinear, "bilinear")]
        [InlineData(HeicChromaUpsampling.NearestNeighbor, "nearest-neighbor")]
        public void ShouldSetTheDefine(HeicChromaUpsampling chromaUpsampling, string excpeted)
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new HeicReadDefines
            {
                ChromaUpsampling = chromaUpsampling,
            });

            Assert.Equal(excpeted, image.Settings.GetDefine(MagickFormat.Heic, "chroma-upsampling"));
        }

        [Fact]
        public void ShouldNotSetTheDefineWhenNull()
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new HeicReadDefines
            {
                ChromaUpsampling = null,
            });

            Assert.Null(image.Settings.GetDefine(MagickFormat.Heic, "chroma-upsampling"));
        }
    }
}
