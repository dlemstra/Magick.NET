// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheHoughLineMethod
    {
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
