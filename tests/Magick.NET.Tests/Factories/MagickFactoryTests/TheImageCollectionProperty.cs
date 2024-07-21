// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick.Factories;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickFactoryTests
{
    public class TheImageCollectionProperty
    {
        [Fact]
        public void ShouldReturnInstance()
        {
            var factory = new MagickFactory();

            Assert.NotNull(factory.ImageCollection);
            Assert.IsType<MagickImageCollectionFactory>(factory.ImageCollection);
        }

        [Fact]
        public void ShouldReturnTheSameInstance()
        {
            var factory = new MagickFactory();

            var first = factory.ImageCollection;
            var second = factory.ImageCollection;
            Assert.Same(first, second);
        }
    }
}
