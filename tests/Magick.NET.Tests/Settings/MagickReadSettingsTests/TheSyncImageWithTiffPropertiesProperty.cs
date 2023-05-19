// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickReadSettingsTests
{
    public class TheSyncImageWithTiffPropertiesProperty
    {
        [Fact]
        public void ShouldReturnTrueAsTheDefaultValue()
        {
            var settings = new MagickReadSettings();
            Assert.True(settings.SyncImageWithTiffProperties);
        }

        [Fact]
        public void ShouldNotChangeTheDensityOfTheImageWhenSetToFalse()
        {
            using (var image = new MagickImage(Files.VicelandPNG))
            {
                Assert.Equal(300.0, image.Density.X);
            }

            var settings = new MagickReadSettings
            {
                SyncImageWithTiffProperties = false,
            };

            using (var image = new MagickImage())
            {
                image.Read(Files.VicelandPNG, settings);
                Assert.InRange(image.Density.X, 118, 119);
            }
        }
    }
}
