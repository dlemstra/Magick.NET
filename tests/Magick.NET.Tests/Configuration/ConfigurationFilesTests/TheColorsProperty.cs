// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick.Configuration;
using Xunit;

namespace Magick.NET.Tests;

public partial class ConfigurationFilesTests
{
    public class TheColorsProperty
    {
        [Fact]
        public void ShouldBeInitialized()
        {
            var configurationFiles = ConfigurationFiles.Default;

            Assert.NotNull(configurationFiles.Colors);
            Assert.Equal("colors.xml", configurationFiles.Colors.FileName);
            Assert.NotNull(configurationFiles.Colors.Data);
            Assert.Contains("<colormap>", configurationFiles.Colors.Data);
        }
    }
}
