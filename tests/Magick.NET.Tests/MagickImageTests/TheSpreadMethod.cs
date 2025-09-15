// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    [Collection(nameof(IsolatedUnitTest))]
    public class TheSpreadMethod
    {
        [Fact]
        public void ShouldSpreadPixelsRandomly()
        {
            IsolatedUnitTest.Execute(static () =>
            {
                using var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG);
                image.Spread(10);

                using var original = new MagickImage(Files.FujiFilmFinePixS1ProJPG);

                Assert.InRange(original.Compare(image, ErrorMetric.RootMeanSquared), 0.120, 0.123);
            });
        }

        [Fact]
        public void ShouldUseTheCorrectDefaultValue()
        {
            IsolatedUnitTest.Execute(static () =>
            {
                MagickNET.SetRandomSeed(42);

                using var image = new MagickImage(Files.Builtin.Wizard);
                using var other = image.Clone();
                image.Spread();
                other.Spread(image.Interpolate, 3);

                var distortion = other.Compare(image, ErrorMetric.RootMeanSquared);

                Assert.Equal(0.0, distortion);

                MagickNET.ResetRandomSeed();
            });
        }
    }
}
