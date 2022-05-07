// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick.Configuration;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class ConfigurationFilesTests
    {
        public class TheConfigureProperty
        {
            public class TheDelegatesProperty
            {
                [Fact]
                public void ShouldBeInitialized()
                {
                    var configurationFiles = ConfigurationFiles.Default;

                    Assert.NotNull(configurationFiles.Delegates);
                    Assert.Equal("delegates.xml", configurationFiles.Delegates.FileName);
                    Assert.NotNull(configurationFiles.Delegates.Data);
                    Assert.Contains("<delegatemap>", configurationFiles.Delegates.Data);
                }
            }
        }
    }
}
