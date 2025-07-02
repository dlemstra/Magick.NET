// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick.Configuration;
using Xunit;

namespace Magick.NET.Tests;

public partial class ConfigurationFilesTests
{
    public class TheMimeProperty
    {
        [Fact]
        public void ShouldBeInitialized()
        {
            var configurationFiles = ConfigurationFiles.Default;

            Assert.NotNull(configurationFiles.Mime);
            Assert.Equal("mime.xml", configurationFiles.Mime.FileName);
            Assert.NotNull(configurationFiles.Mime.Data);
            Assert.Contains(@"<mimemap>", configurationFiles.Mime.Data);
        }
    }
}
