// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class TheLabelCoder
    {
        [Fact]
        public void ShouldUseTheCorrectFontSize()
        {
            var settings = new MagickReadSettings
            {
                Width = 300,
                Font = Files.Fonts.KaushanScript,
            };

            using (var image = new MagickImage("label:asf", settings))
            {
                ColorAssert.Equal(MagickColors.Black, image, 293, 68);
                ColorAssert.Equal(MagickColors.Black, image, 17, 200);
            }
        }
    }
}
