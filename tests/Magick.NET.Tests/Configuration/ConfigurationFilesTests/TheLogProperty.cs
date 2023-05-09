// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick.Configuration;
using Xunit;

namespace Magick.NET.Tests;

public partial class ConfigurationFilesTests
{
    public class TheLogProperty
    {
        [Fact]
        public void ShouldBeInitialized()
        {
            var configurationFiles = ConfigurationFiles.Default;

            Assert.NotNull(configurationFiles.Log);
            Assert.Equal("log.xml", configurationFiles.Log.FileName);
            Assert.NotNull(configurationFiles.Log.Data);
            Assert.Contains(@"<logmap>", configurationFiles.Log.Data);
        }
    }
}
