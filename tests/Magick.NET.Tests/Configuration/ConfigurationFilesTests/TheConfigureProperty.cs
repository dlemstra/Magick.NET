// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using ImageMagick.Configuration;
using Xunit;

namespace Magick.NET.Tests;

public partial class ConfigurationFilesTests
{
    [Fact]
    public void ShouldBeInitialized()
    {
        var configurationFiles = ConfigurationFiles.Default;

        Assert.NotNull(configurationFiles.Configure);
        Assert.Equal("configure.xml", configurationFiles.Configure.FileName);
        Assert.NotNull(configurationFiles.Configure.Data);
        Assert.Contains("<configuremap>", configurationFiles.Configure.Data);
        Assert.Contains($@"<configure name=""TARGET_CPU"" value=""{Runtime.Architecture}""/>", configurationFiles.Configure.Data, StringComparison.OrdinalIgnoreCase);
    }
}
