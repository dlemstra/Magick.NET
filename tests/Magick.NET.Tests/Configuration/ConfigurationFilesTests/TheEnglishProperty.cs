// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick.Configuration;
using Xunit;

namespace Magick.NET.Tests;

public partial class ConfigurationFilesTests
{
    public class TheEnglishProperty
    {
        [Fact]
        public void ShouldBeInitialized()
        {
            var configurationFiles = ConfigurationFiles.Default;

            Assert.NotNull(configurationFiles.English);
            Assert.Equal("english.xml", configurationFiles.English.FileName);
            Assert.NotNull(configurationFiles.English.Data);
            Assert.Contains(@"<locale name=""english"">", configurationFiles.English.Data);
        }
    }
}
