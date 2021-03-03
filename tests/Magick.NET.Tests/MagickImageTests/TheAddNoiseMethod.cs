// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheAddNoiseMethod
        {
            [Fact]
            public void ShouldCreateDifferentImagesEachRun()
            {
                TestHelper.ExecuteInsideLock(() =>
                {
                    using (var imageA = new MagickImage(MagickColors.Black, 10, 10))
                    {
                        using (var imageB = new MagickImage(MagickColors.Black, 10, 10))
                        {
                            imageA.AddNoise(NoiseType.Random);
                            imageB.AddNoise(NoiseType.Random);

                            Assert.NotEqual(0.0, imageA.Compare(imageB, ErrorMetric.RootMeanSquared));
                        }
                    }
                });
            }
        }
    }
}
