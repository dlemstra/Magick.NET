// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick.Factories;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickFactoryTests
{
    public class TheSettingsProperty
    {
        [Fact]
        public void ShouldReturnInstance()
        {
            var factory = new MagickFactory();

            Assert.NotNull(factory.Settings);
            Assert.IsType<SettingsFactory>(factory.Settings);
        }

        [Fact]
        public void ShouldReturnTheSameInstance()
        {
            var factory = new MagickFactory();

            var first = factory.Settings;
            var second = factory.Settings;
            Assert.Same(first, second);
        }
    }
}
