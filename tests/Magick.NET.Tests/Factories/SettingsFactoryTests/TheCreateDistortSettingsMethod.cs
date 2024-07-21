// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Factories;
using Xunit;

namespace Magick.NET.Tests;

public partial class SettingsFactoryTests
{
    public class TheCreateDistortSettingsMethod
    {
        [Fact]
        public void ShouldCreateInstance()
        {
            var factory = new SettingsFactory();

            var settings = factory.CreateDistortSettings();

            Assert.NotNull(settings);
            Assert.IsType<DistortSettings>(settings);
        }
    }
}
