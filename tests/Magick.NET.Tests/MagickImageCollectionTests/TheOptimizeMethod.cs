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

namespace Magick.NET.Tests;

public partial class MagickImageCollectionTests
{
    public class TheOptimizeMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenCollectionIsEmpty()
        {
            using var images = new MagickImageCollection();

            Assert.Throws<InvalidOperationException>(() => images.Optimize());
        }

        [Fact]
        public void ShouldCorrectlyOptimizeTheImages()
        {
            using var images = new MagickImageCollection
            {
                new MagickImage(MagickColors.Red, 11, 11),
            };

            var image = new MagickImage(MagickColors.Red, 11, 11);
            using var pixels = image.GetPixels();
            pixels.SetPixel(5, 5, new QuantumType[] { 0, Quantum.Max, 0 });

            images.Add(image);
            images.Optimize();

            Assert.Equal(1U, images[1].Width);
            Assert.Equal(1U, images[1].Height);
            Assert.Equal(5, images[1].Page.X);
            Assert.Equal(5, images[1].Page.Y);
            ColorAssert.Equal(MagickColors.Lime, images[1], 0, 0);
        }

        [Fact]
        public void ShouldCorrectlyOptimizeDuplicateFrames()
        {
            using var images = new MagickImageCollection
            {
                new MagickImage("xc:red", 2, 2),
                new MagickImage("xc:red", 2, 2),
                new MagickImage("xc:green", 2, 2),
            };

            images.Optimize();

            Assert.Equal(3, images.Count);
            var secondFrame = images[1];

            Assert.Equal(1U, secondFrame.Width);
            Assert.Equal(1U, secondFrame.Height);
            ColorAssert.Equal(new MagickColor("#fff0"), secondFrame, 0, 0);
        }
    }
}
