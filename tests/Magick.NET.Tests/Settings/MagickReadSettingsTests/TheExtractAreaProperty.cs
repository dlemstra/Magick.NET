// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickReadSettingsTests
{
    public class TheExtractAreaProperty
    {
        [Fact]
        public void ShouldHaveNullAsTheDefaultValue()
        {
            var settings = new MagickReadSettings();

            Assert.Null(settings.ExtractArea);
        }

        [Fact]
        public void ShouldExtractAreaFromImage()
        {
            var extractArea = new MagickGeometry(10, 10, 20, 30);
            var settings = new MagickReadSettings
            {
                ExtractArea = extractArea,
            };

            using var image = new MagickImage(Files.Coders.GrimJP2, settings);
            Assert.Equal(20U, image.Width);
            Assert.Equal(30U, image.Height);

            using var area = new MagickImage(Files.Coders.GrimJP2);
            area.Crop(extractArea);

            Assert.Equal(0.0, area.Compare(image, ErrorMetric.RootMeanSquared));
        }
    }
}
