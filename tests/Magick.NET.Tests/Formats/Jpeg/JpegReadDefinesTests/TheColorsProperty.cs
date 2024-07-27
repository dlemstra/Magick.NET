// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class JpegReadDefinesTests
{
    public class TheColorsProperty
    {
        [Fact]
        public void ShouldSetTheDefine()
        {
            var settings = new MagickReadSettings
            {
                Defines = new JpegReadDefines
                {
                    Colors = 100,
                },
            };

            using var image = new MagickImage();
            image.Read(Files.ImageMagickJPG, settings);

            Assert.Equal("100", image.Settings.GetDefine(MagickFormat.Jpeg, "colors"));
        }

        [Fact]
        public void ShouldLimitTheColors()
        {
            var settings = new MagickReadSettings
            {
                Defines = new JpegReadDefines
                {
                    Colors = 100,
                },
            };

            using var image = new MagickImage();
            image.Read(Files.ImageMagickJPG, settings);

            Assert.InRange(image.TotalColors, 99U, 100U);
        }
    }
}
