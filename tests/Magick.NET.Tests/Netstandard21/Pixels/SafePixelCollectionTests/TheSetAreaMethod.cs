// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if NETCOREAPP

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

public partial class SafePixelCollectionTests
{
    public partial class TheSetAreaMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenSpanHasInvalidSize()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixels();

            Assert.Throws<ArgumentException>("values", () => pixels.SetArea(10, 10, 1000, 1000, new Span<QuantumType>(new QuantumType[] { 0, 0, 0, 0 })));
        }

        [Fact]
        public void ShouldThrowExceptionWhenSpanHasTooManyValues()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixels();
            var values = new QuantumType[(113 * 108 * image.ChannelCount) + image.ChannelCount];

            Assert.Throws<ArgumentException>("values", () => pixels.SetArea(10, 10, 113, 108, new Span<QuantumType>(values)));
        }

        [Fact]
        public void ShouldChangePixelsWhenSpanHasMaxNumberOfValues()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixels();
            var values = new QuantumType[113 * 108 * image.ChannelCount];
            pixels.SetArea(10, 10, 113, 108, new Span<QuantumType>(values));

            ColorAssert.Equal(MagickColors.Black, image, (int)image.Width - 1, (int)image.Height - 1);
        }

        [Fact]
        public void ShouldThrowExceptionWhenSpanIsSpecifiedAndGeometryIsNull()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixels();

            Assert.Throws<ArgumentNullException>("geometry", () => pixels.SetArea(null, new Span<QuantumType>(new QuantumType[] { 0 })));
        }

        [Fact]
        public void ShouldChangePixelsWhenGeometryAndSpanAreSpecified()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixels();
            var values = new QuantumType[113 * 108 * image.ChannelCount];
            pixels.SetArea(new MagickGeometry(10, 10, 113, 108), new Span<QuantumType>(values));

            ColorAssert.Equal(MagickColors.Black, image, (int)image.Width - 1, (int)image.Height - 1);
        }
    }
}

#endif
