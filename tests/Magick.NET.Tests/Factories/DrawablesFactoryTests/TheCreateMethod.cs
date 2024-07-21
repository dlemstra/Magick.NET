// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Drawing;
using Xunit;

namespace Magick.NET.Tests;

public partial class SettingsFactoryTests
{
    public class TheCreateMethod
    {
        [Fact]
        public void ShouldCreateInstance()
        {
            var factory = new DrawablesFactory();

            var drawables = factory.Create();

            Assert.NotNull(drawables);
            Assert.IsType<Drawables>(drawables);
        }
    }
}
