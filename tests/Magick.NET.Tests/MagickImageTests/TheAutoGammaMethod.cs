// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheAutoGammaMethod
        {
            [Fact]
            public void ShouldAdjustTheImage()
            {
                using (var image = new MagickImage(Files.Builtin.Logo))
                {
                    image.AutoGamma();

                    ColorAssert.Equal(new MagickColor("#00000003017E"), image, 496, 429);
                }
            }

            [Fact]
            public void ShouldUseTheCorrectDefaultChannel()
            {
                using (var imageA = new MagickImage(Files.MagickNETIconPNG))
                {
                    using (var imageB = imageA.Clone())
                    {
                        imageA.AutoGamma();
                        imageB.AutoGamma(Channels.Composite);

                        var distortion = imageA.Compare(imageB, ErrorMetric.RootMeanSquared);
                        Assert.Equal(0.0, distortion);
                    }
                }
            }
        }
    }
}
