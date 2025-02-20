// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickReadSettingsTests
{
    public class TheFrameCountProperty
    {
        [Fact]
        public void ShouldReturnTheCorrectNumberOfFrames()
        {
            using var images = new MagickImageCollection(Files.RoseSparkleGIF);
            using var secondImage = images[1].Clone();
            using var thirdImage = images[2].Clone();

            var settings = new MagickReadSettings
            {
                FrameCount = 2,
                FrameIndex = 1,
            };
            images.Read(Files.RoseSparkleGIF, settings);

            Assert.Equal(2, images.Count);
            Assert.Equal(0.0, images[0].Compare(secondImage, ErrorMetric.RootMeanSquared));
            Assert.Equal(0.0, images[1].Compare(thirdImage, ErrorMetric.RootMeanSquared));
        }

        [Fact]
        public void ShouldThrowExceptionWhenFrameCountIsHigherThanOneAndImageIsRead()
        {
            var settings = new MagickReadSettings
            {
                FrameCount = 2,
            };

            var exception = Assert.Throws<ArgumentException>("readSettings", () => new MagickImage(Files.RoseSparkleGIF, settings));
            ExceptionAssert.Contains("The frame count can only be set to 1 when a single image is being read.", exception);
        }
    }
}
