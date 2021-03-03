// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public class DrawableDensityTests
    {
        [Fact]
        public void Test_ImageSize()
        {
            using (var image = CreateImage(null))
            {
                Assert.Equal(107, image.Width);
                Assert.Equal(19, image.Height);
            }

            using (var image = CreateImage(97))
            {
                Assert.Equal(142, image.Width);
                Assert.Equal(24, image.Height);
            }
        }

        [Fact]
        public void Test_Constructor()
        {
            var density = new DrawableDensity(new PointD(4, 2));
            Assert.Equal(4, density.Density.X);
            Assert.Equal(2, density.Density.Y);
        }

        private MagickImage CreateImage(int? density)
        {
            var image = new MagickImage(MagickColors.Purple, 500, 500);
            var pointSize = new DrawableFontPointSize(20);
            var text = new DrawableText(250, 250, "Magick.NET");

            if (!density.HasValue)
                image.Draw(pointSize, text);
            else
                image.Draw(pointSize, new DrawableDensity(density.Value), text);

            image.Trim();

            return image;
        }
    }
}
