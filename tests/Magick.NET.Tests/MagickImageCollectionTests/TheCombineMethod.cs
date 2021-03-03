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
                    using (var collection = new MagickImageCollection())
                    {
                        Assert.Throws<InvalidOperationException>(() =>
                        {
                            collection.Combine();
                        });
                    }
                }
            }

            [Fact]
            public void ShouldCombineSeparatedImages()
            {
                using (var rose = new MagickImage(Files.Builtin.Rose))
                {
                    using (var collection = new MagickImageCollection())
                    {
                        collection.AddRange(rose.Separate(Channels.RGB));

                        Assert.Equal(3, collection.Count);

                        using (var image = collection.Combine())
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
                    using (var collection = new MagickImageCollection())
                    {
                        collection.AddRange(cmyk.Separate(Channels.CMYK));

                        Assert.Equal(4, collection.Count);

                        using (var image = collection.Combine(ColorSpace.CMYK))
                        {
                            Assert.Equal(0.0, cmyk.Compare(image, ErrorMetric.RootMeanSquared));
                        }
                    }
                }
            }
        }
    }
}
