// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheAdaptiveBlurMethod
        {
            [Fact]
            public void ShouldBlurTheImage()
            {
                using (var image = new MagickImage(Files.MagickNETIconPNG))
                {
                    image.AdaptiveBlur(10, 5);

#if Q8 || Q16
                    ColorAssert.Equal(new MagickColor("#a872dfb1f8ddfe8b"), image, 56, 68);
#else
                    ColorAssert.Equal(new MagickColor("#a8a8dfdff8f8"), image, 56, 68);
#endif
                }
            }

            [Fact]
            public void ShouldUseTheCorrectDefaultValuesWithoutArguments()
            {
                using (var imageA = new MagickImage(Files.MagickNETIconPNG))
                {
                    using (var imageB = imageA.Clone())
                    {
                        imageA.AdaptiveBlur();
                        imageB.AdaptiveBlur(0.0, 1.0);

                        var distortion = imageA.Compare(imageB, ErrorMetric.RootMeanSquared);
                        Assert.Equal(0.0, distortion);
                    }
                }
            }

            [Fact]
            public void ShouldUseTheCorrectDefaultValuesWithRadius()
            {
                using (var imageA = new MagickImage(Files.MagickNETIconPNG))
                {
                    using (var imageB = imageA.Clone())
                    {
                        imageA.AdaptiveBlur(2.0);
                        imageB.AdaptiveBlur(2.0, 1.0);

                        var distortion = imageA.Compare(imageB, ErrorMetric.RootMeanSquared);
                        Assert.Equal(0.0, distortion);
                    }
                }
            }
        }
    }
}
