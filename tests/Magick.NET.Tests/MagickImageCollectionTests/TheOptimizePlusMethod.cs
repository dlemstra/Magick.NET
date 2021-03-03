// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
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
    public partial class MagickImageCollectionTests
    {
        public class TheOptimizePlusMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenCollectionIsEmpty()
            {
                using (var images = new MagickImageCollection())
                {
                    Assert.Throws<InvalidOperationException>(() => images.OptimizePlus());
                }
            }

            [Fact]
            public void ShouldRemoveDuplicateImages()
            {
                using (var collection = new MagickImageCollection())
                {
                    collection.Add(new MagickImage(MagickColors.Red, 11, 11));
                    /* The second image will not be removed if it is a duplicate so we need to add an extra one. */
                    collection.Add(new MagickImage(MagickColors.Red, 11, 11));
                    collection.Add(new MagickImage(MagickColors.Red, 11, 11));

                    var image = new MagickImage(MagickColors.Red, 11, 11);
                    using (var pixels = image.GetPixels())
                    {
                        pixels.SetPixel(5, 5, new QuantumType[] { 0, Quantum.Max, 0 });
                    }

                    collection.Add(image);
                    collection.OptimizePlus();

                    Assert.Equal(3, collection.Count);

                    Assert.Equal(1, collection[1].Width);
                    Assert.Equal(1, collection[1].Height);
                    Assert.Equal(-1, collection[1].Page.X);
                    Assert.Equal(-1, collection[1].Page.Y);
                    ColorAssert.Equal(new MagickColor("#FF000000"), collection[1], 0, 0);

                    Assert.Equal(1, collection[2].Width);
                    Assert.Equal(1, collection[2].Height);
                    Assert.Equal(5, collection[2].Page.X);
                    Assert.Equal(5, collection[2].Page.Y);
                    ColorAssert.Equal(MagickColors.Lime, collection[2], 0, 0);
                }
            }
        }
    }
}
