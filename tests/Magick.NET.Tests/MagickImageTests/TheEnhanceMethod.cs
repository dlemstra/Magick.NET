// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheEnhanceMethod
    {
        [Fact]
        public void ShouldImproveTheQualityOfNoiseImage()
        {
            using (var enhanced = new MagickImage(Files.NoisePNG))
            {
                using (var original = enhanced.Clone())
                {
                    enhanced.Enhance();

                    Assert.InRange(enhanced.Compare(original, ErrorMetric.RootMeanSquared), 0.0115, 0.0118);
                }
            }
        }
    }
}
