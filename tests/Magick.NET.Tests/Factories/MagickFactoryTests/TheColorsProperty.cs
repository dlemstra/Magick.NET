// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Factories;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickFactoryTests
{
    public class TheColorsProperty
    {
        [Fact]
        public void ShouldReturnInstance()
        {
            var factory = new MagickFactory();

            Assert.NotNull(factory.Colors);
            Assert.IsType<MagickColors>(factory.Colors);
        }

        [Fact]
        public void ShouldReturnTheSameInstance()
        {
            var factory = new MagickFactory();

            var first = factory.Colors;
            var second = factory.Colors;
            Assert.Same(first, second);
        }
    }
}
