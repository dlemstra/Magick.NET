// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Linq;
using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class HeicWriteDefinesTests
{
    public class TheChromaDownsamplingProperty
    {
        [Fact]
        public void ShouldSetTheDefine()
        {
            foreach (var value in Enum.GetValues(typeof(HeicChromaDownsampling)).OfType<HeicChromaDownsampling>())
            {
                using var image = new MagickImage();
                image.Settings.SetDefines(new HeicWriteDefines
                {
                    ChromaDownsampling = value,
                });

                switch (value)
                {
                    case HeicChromaDownsampling.Average:
                        Assert.Equal("average", image.Settings.GetDefine(MagickFormat.Heic, "chroma-downsampling"));
                        break;
                    case HeicChromaDownsampling.NearestNeighbor:
                        Assert.Equal("nearest-neighbor", image.Settings.GetDefine(MagickFormat.Heic, "chroma-downsampling"));
                        break;
                    case HeicChromaDownsampling.SharpYUV:
                        Assert.Equal("sharp-yuv", image.Settings.GetDefine(MagickFormat.Heic, "chroma-downsampling"));
                        break;
                }
            }
        }

        [Fact]
        public void ShouldNotSetTheDefineWhenNull()
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new HeicWriteDefines
            {
                ChromaDownsampling = null,
            });

            Assert.Null(image.Settings.GetDefine(MagickFormat.Heic, "chroma-downsampling"));
        }
    }
}
