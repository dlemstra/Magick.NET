// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Linq;
using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class HeicReadDefinesTests
{
    public class TheChromaUpsamplingProperty
    {
        [Fact]
        public void ShouldSetTheDefine()
        {
            foreach (var value in Enum.GetValues(typeof(HeicChromaUpsampling)).OfType<HeicChromaUpsampling>())
            {
                using var image = new MagickImage();
                image.Settings.SetDefines(new HeicReadDefines
                {
                    ChromaUpsampling = value,
                });

                switch (value)
                {
                    case HeicChromaUpsampling.Bilinear:
                        Assert.Equal("bilinear", image.Settings.GetDefine(MagickFormat.Heic, "chroma-upsampling"));
                        break;
                    case HeicChromaUpsampling.NearestNeighbor:
                        Assert.Equal("nearest-neighbor", image.Settings.GetDefine(MagickFormat.Heic, "chroma-upsampling"));
                        break;
                }
            }
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
