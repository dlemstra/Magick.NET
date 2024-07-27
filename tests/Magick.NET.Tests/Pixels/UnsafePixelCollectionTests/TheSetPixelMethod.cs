// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections.Generic;
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

public partial class UnsafePixelCollectionTests
{
    public partial class TheSetPixelMethod
    {
        [Fact]
        public void ShouldNotThrowExceptionWhenPixelIsNull()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixelsUnsafe();
            pixels.SetPixel((Pixel)null);
        }

        [Fact]
        public void ShouldThrowExceptionWhenPixelWidthIsOutsideImage()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixelsUnsafe();

            Assert.Throws<MagickCacheErrorException>(() => pixels.SetPixel(new Pixel((int)image.Width + 1, 0, 3)));
        }

        [Fact]
        public void ShouldThrowExceptionWhenPixelHeightIsOutsideImage()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixelsUnsafe();

            Assert.Throws<MagickCacheErrorException>(() => pixels.SetPixel(new Pixel(0, (int)image.Height + 1, 3)));
        }

        [Fact]
        public void ShouldChangePixelWhenNotEnoughChannelsAreSupplied()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixelsUnsafe();
            var pixel = new Pixel(0, 0, new QuantumType[] { 0 });
            pixels.SetPixel(pixel);

            ColorAssert.Equal(MagickColors.Cyan, image, 0, 0);
        }

        [Fact]
        public void ShouldChangePixelWhenTooManyChannelsAreSupplied()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixelsUnsafe();
            var pixel = new Pixel(0, 0, new QuantumType[] { 0, 0, 0, 0 });
            pixels.SetPixel(pixel);

            ColorAssert.Equal(MagickColors.Black, image, 0, 0);
        }

        [Fact]
        public void ShouldChangePixelWhenCompletePixelIsSupplied()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixelsUnsafe();
            var pixel = new Pixel(0, 0, new QuantumType[] { 0, Quantum.Max, 0 });
            pixels.SetPixel(pixel);

            ColorAssert.Equal(MagickColors.Lime, image, 0, 0);
        }

        [Fact]
        public void ShouldNotThrowExceptionWhenIEnumerablePixelIsNull()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixelsUnsafe();
            pixels.SetPixel((IEnumerable<Pixel>)null);
        }

        [Fact]
        public void ShouldChangePixelsWhenMultipleIncompletePixelsAreSupplied()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixelsUnsafe();
            var pixelA = new Pixel(0, 0, new QuantumType[] { 0 });
            var pixelB = new Pixel(1, 0, new QuantumType[] { 0, 0 });
            pixels.SetPixel(new Pixel[] { pixelA, pixelB });

            ColorAssert.Equal(MagickColors.Cyan, image, 0, 0);
            ColorAssert.Equal(MagickColors.Blue, image, 1, 0);
        }

        [Fact]
        public void ShouldChangePixelsWhenMultipleCompletePixelsAreSupplied()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixelsUnsafe();
            var pixelA = new Pixel(0, 0, new QuantumType[] { Quantum.Max, 0, 0 });
            var pixelB = new Pixel(1, 0, new QuantumType[] { 0, 0, Quantum.Max });
            pixels.SetPixel(new Pixel[] { pixelA, pixelB });

            ColorAssert.Equal(MagickColors.Red, image, 0, 0);
            ColorAssert.Equal(MagickColors.Blue, image, 1, 0);
        }

        [Fact]
        public void ShouldNotThrowExceptionWhenArrayIsNull()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixelsUnsafe();
            pixels.SetPixel(0, 0, null);
        }

        [Fact]
        public void ShouldThrowExceptionWhenArrayIsEmpty()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixelsUnsafe();
            pixels.SetPixel(0, 0, System.Array.Empty<QuantumType>());
        }

        [Fact]
        public void ShouldThrowExceptionWhenOffsetWidthIsOutsideImage()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixelsUnsafe();

            Assert.Throws<MagickCacheErrorException>(() => pixels.SetPixel((int)image.Width + 1, 0, new QuantumType[] { 0 }));
        }

        [Fact]
        public void ShouldThrowExceptionWhenOffsetHeightIsOutsideImage()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixelsUnsafe();

            Assert.Throws<MagickCacheErrorException>(() => pixels.SetPixel(0, (int)image.Height + 1, new QuantumType[] { 0 }));
        }

        [Fact]
        public void ShouldChangePixelsWhenOneChannelAndOffsetAreSupplied()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixelsUnsafe();
            pixels.SetPixel(0, 0, new QuantumType[] { 0 });

            ColorAssert.Equal(MagickColors.Cyan, image, 0, 0);
        }

        [Fact]
        public void ShouldChangePixelsWhenTooManyChannelsAndOffsetAreSupplied()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixelsUnsafe();
            pixels.SetPixel(0, 0, new QuantumType[] { 0, 0, 0, 0 });

            ColorAssert.Equal(MagickColors.Black, image, 0, 0);
        }

        [Fact]
        public void ShouldChangePixelsWhenCorrectNumberOfChannelsAndOffsetAreSupplied()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixelsUnsafe();
            pixels.SetPixel(0, 0, new QuantumType[] { 0, 0, 0 });

            ColorAssert.Equal(MagickColors.Black, image, 0, 0);
        }
    }
}
