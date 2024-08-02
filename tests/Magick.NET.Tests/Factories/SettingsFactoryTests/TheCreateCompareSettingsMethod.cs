// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Factories;
using Xunit;

namespace Magick.NET.Tests;

public partial class SettingsFactoryTests
{
    public class TheCreateCompareSettingsMethod
    {
        [Fact]
        public void ShouldCreateInstance()
        {
            var factory = new SettingsFactory();

            var settings = factory.CreateCompareSettings(ErrorMetric.NormalizedCrossCorrelation);

            Assert.NotNull(settings);
            Assert.IsType<CompareSettings>(settings);
            Assert.Equal(ErrorMetric.NormalizedCrossCorrelation, settings.Metric);
        }
    }
}
