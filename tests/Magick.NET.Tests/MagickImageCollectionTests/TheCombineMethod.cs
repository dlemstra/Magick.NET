// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageCollectionTests
    {
        public class TheCombineMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenCollectionIsEmpty()
            {
                using (var rose = new MagickImage(Files.Builtin.Rose))
                {
                    using (var images = new MagickImageCollection())
                    {
                        Assert.Throws<InvalidOperationException>(() =>
                        {
                            images.Combine();
                        });
                    }
                }
            }

            [Fact]
            public void ShouldCombineSeparatedImages()
            {
                using (var rose = new MagickImage(Files.Builtin.Rose))
                {
                    using (var images = new MagickImageCollection())
                    {
                        images.AddRange(rose.Separate(Channels.RGB));

                        Assert.Equal(3, images.Count);

                        using (var image = images.Combine())
                        {
                            Assert.Equal(rose.TotalColors, image.TotalColors);
                        }
                    }
                }
            }

            [Fact]
            public void ShouldCombineCmykImage()
            {
                using (var cmyk = new MagickImage(Files.CMYKJPG))
                {
                    using (var images = new MagickImageCollection())
                    {
                        images.AddRange(cmyk.Separate(Channels.CMYK));

                        Assert.Equal(4, images.Count);

                        using (var image = images.Combine(ColorSpace.CMYK))
                        {
                            Assert.Equal(0.0, cmyk.Compare(image, ErrorMetric.RootMeanSquared));
                        }
                    }
                }
            }
        }
    }
}
