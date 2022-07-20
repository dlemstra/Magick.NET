// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheKuwaharaMethod
        {
            [Fact]
            public void ShouldUseTheCorrectDefaultValues()
            {
                using (var image = new MagickImage(Files.NoisePNG))
                {
                    using (var other = new MagickImage(Files.NoisePNG))
                    {
                        image.Kuwahara();
                        other.Kuwahara(0.0, 1.0);

                        Assert.Equal(image, other);
                    }
                }
            }

            [Fact]
            public void ShouldApplyEdgePreservingNoiseReductionFilter()
            {
                using (var image = new MagickImage(Files.NoisePNG))
                {
                    image.Kuwahara(13.4, 2.5);
                    image.ColorType = ColorType.Bilevel;

                    ColorAssert.Equal(MagickColors.White, image, 216, 120);
                    ColorAssert.Equal(MagickColors.Black, image, 39, 138);
                }
            }
        }
    }
}
