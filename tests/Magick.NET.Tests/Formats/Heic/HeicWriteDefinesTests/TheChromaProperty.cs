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
    public class TheChromaProperty
    {
        [Fact]
        public void ShouldSetTheDefine()
        {
            foreach (var value in Enum.GetValues(typeof(HeicChroma)).OfType<HeicChroma>())
            {
                using var image = new MagickImage();
                image.Settings.SetDefines(new HeicWriteDefines
                {
                    Chroma = value,
                });

                switch (value)
                {
                    case HeicChroma.Chroma420:
                        Assert.Equal("420", image.Settings.GetDefine(MagickFormat.Heic, "chroma"));
                        break;
                    case HeicChroma.Chroma422:
                        Assert.Equal("422", image.Settings.GetDefine(MagickFormat.Heic, "chroma"));
                        break;
                    case HeicChroma.Chroma444:
                        Assert.Equal("444", image.Settings.GetDefine(MagickFormat.Heic, "chroma"));
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
                Chroma = null,
            });

            Assert.Null(image.Settings.GetDefine(MagickFormat.Heic, "chroma"));
        }
    }
}
