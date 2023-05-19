// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickReadSettingsTests
{
    public class TheSyncImageWithExifProfileProperty
    {
        [Fact]
        public void ShouldReturnTrueAsTheDefaultValue()
        {
            var settings = new MagickReadSettings();
            Assert.True(settings.SyncImageWithExifProfile);
        }

        [Fact]
        public void ShouldNotChangeTheDensityOfTheImageWhenSetToFalse()
        {
            using var original = new MagickImage(Files.EightBimJPG);
            Assert.Equal(300.0, original.Density.X);

            var settings = new MagickReadSettings
            {
                SyncImageWithExifProfile = false,
            };
            using var image = new MagickImage();
            image.Read(Files.EightBimJPG, settings);

            Assert.Equal(72.0, image.Density.X);
        }
    }
}
