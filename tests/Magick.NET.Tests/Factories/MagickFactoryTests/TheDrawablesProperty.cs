// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick.Factories;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickFactoryTests
{
    public class TheDrawablesProperty
    {
        [Fact]
        public void ShouldReturnInstance()
        {
            var factory = new MagickFactory();

            Assert.NotNull(factory.Drawables);
            Assert.IsType<DrawablesFactory>(factory.Drawables);
        }

        [Fact]
        public void ShouldReturnTheSameInstance()
        {
            var factory = new MagickFactory();

            var first = factory.Drawables;
            var second = factory.Drawables;
            Assert.Same(first, second);
        }
    }
}
