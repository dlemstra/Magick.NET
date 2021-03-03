// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickNETTests
    {
        public class TheDelegatesProperty
        {
            [Fact]
            public void ShouldReturnAllDelegates()
            {
                var delegates = MagickNET.Delegates;

                if (OperatingSystem.IsWindows)
                    Assert.Equal("bzlib cairo flif freetype gslib heic jng jp2 jpeg jxl lcms lqr lzma openexr pangocairo png ps raw rsvg tiff webp xml zip zlib", delegates);
                else if (OperatingSystem.IsLinux)
                    Assert.Equal("bzlib cairo fontconfig freetype heic jng jp2 jpeg jxl lcms lqr lzma openexr pangocairo png raw rsvg tiff webp xml zip zlib", delegates);
                else
                    Assert.Equal("cairo fontconfig freetype heic jng jp2 jpeg lcms lqr openexr pangocairo png raw rsvg tiff webp xml zlib", delegates);
            }
        }
    }
}