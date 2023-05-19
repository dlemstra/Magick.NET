// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickReadSettingsTests
{
    public class TheDensityProperty
    {
        [Fact]
        public void ShouldBeUseWhenReadingImage()
        {
            var settings = new MagickReadSettings
            {
                Density = new Density(300, 150),
            };
            using var image = new MagickImage();
            image.Read(Files.RoseSparkleGIF, settings);

            Assert.Equal(300, image.Density.X);
            Assert.Equal(150, image.Density.Y);
        }

        [Fact]
        public void ShouldBeUseWhenReadingImageCollection()
        {
            var settings = new MagickReadSettings
            {
                Density = new Density(150, 300),
            };
            using var images = new MagickImageCollection();
            images.Read(Files.RoseSparkleGIF, settings);

            foreach (var image in images)
            {
                Assert.Equal(150, image.Density.X);
                Assert.Equal(300, image.Density.Y);
            }
        }
    }
}
