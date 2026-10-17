// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class TiffReadDefinesTests
{
    public class TheIgnoreGpsPropertiesProperty
    {
        [Fact]
        public void ShouldSetTheDefineWhenTrue()
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new TiffReadDefines
            {
                IgnoreGpsProperties = true,
            });

            Assert.Equal("false", image.Settings.GetDefine(MagickFormat.Tiff, "gps-properties"));
        }

        [Theory]
        [InlineData(false)]
        [InlineData(null)]
        public void ShouldNotSetTheDefineWhenFalseOrNull(bool? value)
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new TiffReadDefines
            {
                IgnoreGpsProperties = value,
            });

            Assert.Null(image.Settings.GetDefine(MagickFormat.Tiff, "gps-properties"));
        }
    }
}
