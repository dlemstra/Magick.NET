// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick.Configuration;
using Xunit;

namespace Magick.NET.Tests;

public partial class ConfigurationFilesTests
{
    public class TheTypeGhostscriptProperty
    {
        [Fact]
        public void ShouldBeInitialized()
        {
            var configurationFiles = ConfigurationFiles.Default;

            Assert.NotNull(configurationFiles.TypeGhostscript);
            Assert.Equal("type-ghostscript.xml", configurationFiles.TypeGhostscript.FileName);
            Assert.NotNull(configurationFiles.TypeGhostscript.Data);
            Assert.Contains(@"<typemap>", configurationFiles.TypeGhostscript.Data);
            Assert.Contains(@"<type name=""", configurationFiles.TypeGhostscript.Data);
        }
    }
}
