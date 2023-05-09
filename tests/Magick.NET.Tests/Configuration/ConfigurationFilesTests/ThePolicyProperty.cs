// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick.Configuration;
using Xunit;

namespace Magick.NET.Tests;

public partial class ConfigurationFilesTests
{
    public class ThePolicyProperty
    {
        [Fact]
        public void ShouldBeInitialized()
        {
            var configurationFiles = ConfigurationFiles.Default;

            Assert.NotNull(configurationFiles.Policy);
            Assert.Equal("policy.xml", configurationFiles.Policy.FileName);
            Assert.NotNull(configurationFiles.Policy.Data);
            Assert.Contains(@"<policymap>", configurationFiles.Policy.Data);
        }
    }
}
