// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickReadSettingsTests
{
    public class TheFrameIndexProperty
    {
        [Fact]
        public void ShouldReturnTheFrameAtTheSpecifiedIndex()
        {
            using var images = new MagickImageCollection(Files.RoseSparkleGIF);
            using var secondImage = images[1].Clone();

            var settings = new MagickReadSettings
            {
                FrameIndex = 1,
            };
            images.Read(Files.RoseSparkleGIF, settings);

            Assert.Single(images);
            Assert.Equal(0.0, images[0].Compare(secondImage, ErrorMetric.RootMeanSquared));
        }

        [Fact]
        public void ShouldReturnTheImageAtTheSpecifiedIndex()
        {
            var settings = new MagickReadSettings()
            {
                FrameIndex = 1,
            };

            using var image = new MagickImage();
            image.Read(Files.RoseSparkleGIF, settings);

            using var images = new MagickImageCollection(Files.RoseSparkleGIF);

            Assert.Equal(0.0, image.Compare(images[1], ErrorMetric.RootMeanSquared));
        }

        [Fact]
        public void ShouldThrowExceptionWhenValueIsTooHigh()
        {
            var settings = new MagickReadSettings
            {
                FrameIndex = 3,
            };
            using var image = new MagickImage();

            Assert.Throws<MagickOptionErrorException>(() => image.Read(Files.RoseSparkleGIF, settings));
        }
    }
}
