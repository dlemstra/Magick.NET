// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheOilPaintMethod
        {
            [Fact]
            public void ShouldCreateOilPaintImage()
            {
                using (var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
                {
                    image.OilPaint(2, 5);
                    ColorAssert.Equal(new MagickColor("#6a7e85"), image, 180, 98);
                }
            }

            [Fact]
            public void ShouldUseTheCorrectDefaultValues()
            {
                using (var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
                {
                    using (var other = new MagickImage(Files.FujiFilmFinePixS1ProJPG))
                    {
                        image.OilPaint();
                        other.OilPaint(3.0, 1.0);

                        var distortion = image.Compare(other, ErrorMetric.RootMeanSquared);
                        Assert.Equal(0.0, distortion);
                    }
                }
            }
        }
    }
}
