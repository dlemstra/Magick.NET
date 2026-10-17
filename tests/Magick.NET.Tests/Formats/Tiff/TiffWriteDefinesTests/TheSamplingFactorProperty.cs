// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Formats;
using Xunit;

namespace Magick.NET.Tests;

public partial class TiffWriteDefinesTests
{
    public class TheSamplingFactorProperty
    {
        [Theory]
        [InlineData(JpegSamplingFactor.Ratio410, "4x2,1x1,1x1")]
        [InlineData(JpegSamplingFactor.Ratio411, "4x1,1x1,1x1")]
        [InlineData(JpegSamplingFactor.Ratio420, "2x2,1x1,1x1")]
        [InlineData(JpegSamplingFactor.Ratio422, "2x1,1x1,1x1")]
        [InlineData(JpegSamplingFactor.Ratio440, "1x2,1x1,1x1")]
        [InlineData(JpegSamplingFactor.Ratio444, "1x1,1x1,1x1")]
        public void ShouldSetTheDefine(JpegSamplingFactor value, string expected)
        {
            using var image = new MagickImage();
            image.Settings.SetDefines(new TiffWriteDefines
            {
                SamplingFactor = value,
            });

            Assert.Equal(expected, image.Settings.GetDefine("jpeg:sampling-factor"));
        }
    }
}
