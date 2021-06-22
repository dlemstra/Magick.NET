// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheGaussianBlurMethod
        {
            [Fact]
            public void ShouldBlurTheImage()
            {
                using (var gaussian = new MagickImage(Files.Builtin.Wizard))
                {
                    gaussian.GaussianBlur(5.5, 10.2);

                    using (var blur = new MagickImage(Files.Builtin.Wizard))
                    {
                        blur.Blur(5.5, 10.2);

                        double distortion = blur.Compare(gaussian, ErrorMetric.RootMeanSquared);
#if Q8
                        Assert.InRange(distortion, 0.00066, 0.00067);
#elif Q16
                        Assert.InRange(distortion, 0.0000033, 0.0000034);
#else
                        Assert.InRange(distortion, 0.0000011, 0.0000012);
#endif
                    }
                }
            }
        }
    }
}
