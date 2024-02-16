// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Configuration;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickFactoryTests
{
    public class TheConfigurationFilesProperty
    {
        [Fact]
        public void ShouldReturnInstance()
        {
            var factory = new MagickFactory();

            Assert.NotNull(factory.ConfigurationFiles);
            Assert.IsType<ConfigurationFiles>(factory.ConfigurationFiles);
        }

        [Fact]
        public void ShouldReturnDifferentInstance()
        {
            var factory = new MagickFactory();

            var first = factory.ConfigurationFiles;
            var second = factory.ConfigurationFiles;
            Assert.NotSame(first, second);
        }
    }
}
