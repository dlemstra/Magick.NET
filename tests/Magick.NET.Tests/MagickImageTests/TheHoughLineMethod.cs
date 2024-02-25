// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheHoughLineMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenWidthIsNegative()
        {
            using var image = new MagickImage(Files.ConnectedComponentsPNG);
            Assert.Throws<ArgumentException>("width", () => image.HoughLine(-1, 0, 0));
        }

        [Fact]
        public void ShouldThrowExceptionWhenHeightIsNegative()
        {
            using var image = new MagickImage(Files.ConnectedComponentsPNG);
            Assert.Throws<ArgumentException>("height", () => image.HoughLine(0, -1, 0));
        }

        [Fact]
        public void ShouldThrowExceptionWhenThresholdIsNegative()
        {
            using var image = new MagickImage(Files.ConnectedComponentsPNG);
            Assert.Throws<ArgumentException>("threshold", () => image.HoughLine(0, 0, -1));
        }

        [Fact]
        public void ShouldIdentifyLinesInImage()
        {
            using var image = new MagickImage(Files.ConnectedComponentsPNG);
            image.Crop(new MagickGeometry(260, 180, 215, 200));

            image.Threshold(new Percentage(50));
            image.CannyEdge();

            image.Settings.FillColor = MagickColors.Red;
            image.Settings.StrokeColor = MagickColors.Red;

            image.HoughLine();

            ColorAssert.Equal(MagickColors.Red, image, 34, 77);
            ColorAssert.Equal(MagickColors.White, image, 39, 77);
            ColorAssert.Equal(MagickColors.Red, image, 195, 105);
            ColorAssert.Equal(MagickColors.White, image, 201, 105);
        }
    }
}
