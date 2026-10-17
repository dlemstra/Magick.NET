// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class JxlWriteDefinesTests
{
    public class TheQualityProperty
    {
        [Theory]
        [InlineData(0.0)]
        [InlineData(100.0)]
        public void ShouldSetTheDefine(double value)
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new JxlWriteDefines
            {
                Quality = value,
            });

            Assert.Equal(value.ToString(System.Globalization.CultureInfo.InvariantCulture), image.Settings.GetDefine(MagickFormat.Jxl, "quality"));
        }
    }
}
