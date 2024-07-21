// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick.Factories;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickFactoryTests
{
    public class TheImageInfoProperty
    {
        [Fact]
        public void ShouldReturnInstance()
        {
            var factory = new MagickFactory();

            Assert.NotNull(factory.ImageInfo);
            Assert.IsType<MagickImageInfoFactory>(factory.ImageInfo);
        }

        [Fact]
        public void ShouldReturnTheSameInstance()
        {
            var factory = new MagickFactory();

            var first = factory.ImageInfo;
            var second = factory.ImageInfo;
            Assert.Same(first, second);
        }
    }
}
