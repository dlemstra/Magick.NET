// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick.Configuration;
using Xunit;

namespace Magick.NET.Tests;

public partial class ConfigurationFilesTests
{
    public class TheTypeProperty
    {
        [Fact]
        public void ShouldBeInitialized()
        {
            var configurationFiles = ConfigurationFiles.Default;

            Assert.NotNull(configurationFiles.Type);
            Assert.Equal("type.xml", configurationFiles.Type.FileName);
            Assert.NotNull(configurationFiles.Type.Data);
            Assert.Contains(@"<typemap>", configurationFiles.Type.Data);
            Assert.Contains(@"<include file=""", configurationFiles.Type.Data);
        }
    }
}
