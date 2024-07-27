// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class PixelReadSettingsTests
{
    public class TheConstructor
    {
        [Fact]
        public void ShouldSetTheReadSettings()
        {
            var settings = new PixelReadSettings();

            Assert.NotNull(settings.ReadSettings);
        }

        [Fact]
        public void ShouldSetTheMappingCorrectly()
        {
            var settings = new PixelReadSettings(1, 2, StorageType.Int64, PixelMapping.CMYK);

            Assert.NotNull(settings.ReadSettings);
            Assert.Equal(1U, settings.ReadSettings.Width);
            Assert.Equal(2U, settings.ReadSettings.Height);
            Assert.Equal(StorageType.Int64, settings.StorageType);
            Assert.Equal("CMYK", settings.Mapping);
        }

        [Fact]
        public void ShouldSetTheProperties()
        {
            var settings = new PixelReadSettings(3, 4, StorageType.Quantum, "CMY");

            Assert.NotNull(settings.ReadSettings);
            Assert.Equal(3U, settings.ReadSettings.Width);
            Assert.Equal(4U, settings.ReadSettings.Height);
            Assert.Equal(StorageType.Quantum, settings.StorageType);
            Assert.Equal("CMY", settings.Mapping);
        }
    }
}
