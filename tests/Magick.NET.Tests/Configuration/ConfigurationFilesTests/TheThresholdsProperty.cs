// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick.Configuration;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class ConfigurationFilesTests
    {
        public class TheThresholdsProperty
        {
            [Fact]
            public void ShouldBeInitialized()
            {
                var configurationFiles = ConfigurationFiles.Default;

                Assert.NotNull(configurationFiles.Thresholds);
                Assert.Equal("thresholds.xml", configurationFiles.Thresholds.FileName);
                Assert.NotNull(configurationFiles.Thresholds.Data);
                Assert.Contains(@"<thresholds>", configurationFiles.Thresholds.Data);
            }
        }
    }
}
