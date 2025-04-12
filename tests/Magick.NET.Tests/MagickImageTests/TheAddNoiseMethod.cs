// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    [Collection(nameof(IsolatedUnitTest))]
    public class TheAddNoiseMethod
    {
        [Fact]
        public void ShouldCreateDifferentImagesEachRun()
        {
            IsolatedUnitTest.Execute(() =>
            {
                using var imageA = new MagickImage(MagickColors.Black, 10, 10);
                using var imageB = new MagickImage(MagickColors.Black, 10, 10);
                imageA.AddNoise(NoiseType.Random);
                imageB.AddNoise(NoiseType.Random);

                Assert.NotEqual(0.0, imageA.Compare(imageB, ErrorMetric.RootMeanSquared));
            });
        }

        [Fact]
        public void ShouldUseTheRandomSeed()
        {
            IsolatedUnitTest.Execute(() =>
            {
                MagickNET.SetRandomSeed(1337);

                using var first = new MagickImage(Files.Builtin.Logo);
                first.AddNoise(NoiseType.Laplacian);

                ColorAssert.NotEqual(MagickColors.White, first, 46, 62);

                using var second = new MagickImage(Files.Builtin.Logo);
                second.AddNoise(NoiseType.Laplacian, 2.0);

                ColorAssert.NotEqual(MagickColors.White, first, 46, 62);
                Assert.False(first.Equals(second));

                MagickNET.ResetRandomSeed();
            });
        }
    }
}
