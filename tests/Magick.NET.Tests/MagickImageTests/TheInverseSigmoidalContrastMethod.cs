// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheInverseSigmoidalContrastMethod : MagickImageTests
        {
            [Fact]
            public void ShouldUseHalfOfQuantumForMidpointByDefault()
            {
                using (var image = new MagickImage(Files.NoisePNG))
                {
                    using (var other = image.Clone())
                    {
                        image.InverseSigmoidalContrast(4.0);
                        other.InverseSigmoidalContrast(4.0, new Percentage(50));

                        var difference = other.Compare(image, ErrorMetric.RootMeanSquared);
                        Assert.Equal(0.0, difference);
                    }
                }
            }

            [Fact]
            public void ShouldAdjustTheImageContrast()
            {
                using (var image = new MagickImage(Files.NoisePNG))
                {
                    using (var other = image.Clone())
                    {
                        other.InverseSigmoidalContrast(4.0, new Percentage(25));

                        var difference = other.Compare(image, ErrorMetric.RootMeanSquared);
                        Assert.InRange(difference, 0.073, 0.074);
                    }
                }
            }
        }
    }
}
