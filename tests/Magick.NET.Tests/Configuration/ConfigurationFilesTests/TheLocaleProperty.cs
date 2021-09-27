// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick.Configuration;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class ConfigurationFilesTests
    {
        public class TheLocaleProperty
        {
            [Fact]
            public void ShouldBeInitialized()
            {
                var configurationFiles = ConfigurationFiles.Default;

                Assert.NotNull(configurationFiles.Locale);
                Assert.Equal("locale.xml", configurationFiles.Locale.FileName);
                Assert.NotNull(configurationFiles.Locale.Data);
                Assert.Contains(@"<localemap>", configurationFiles.Locale.Data);
            }
        }
    }
}
