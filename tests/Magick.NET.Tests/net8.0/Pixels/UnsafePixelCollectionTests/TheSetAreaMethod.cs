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

public partial class UnsafePixelCollectionTests
{
    public partial class TheSetAreaMethod
    {
        [Fact]
        public void ShouldNotThrowExceptionWhenSpanHasInvalidSize()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixelsUnsafe();
            pixels.SetArea(10, 10, 1000, 1000, new Span<QuantumType>(new QuantumType[] { 0, 0, 0, 0 }));
        }

        [Fact]
        public void ShouldNotThrowExceptionWhenSpanHasTooManyValues()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixelsUnsafe();
            var values = new QuantumType[(113 * 108 * image.ChannelCount) + image.ChannelCount];
            pixels.SetArea(10, 10, 113, 108, new Span<QuantumType>(values));
        }

        [Fact]
        public void ShouldChangePixelsWhenSpanHasMaxNumberOfValues()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixelsUnsafe();
            var values = new QuantumType[113 * 108 * image.ChannelCount];
            pixels.SetArea(10, 10, 113, 108, new Span<QuantumType>(values));

            ColorAssert.Equal(MagickColors.Black, image, (int)image.Width - 1, (int)image.Height - 1);
        }

        [Fact]
        public void ShouldNotThrowExceptionWhenSpanIsSpecifiedAndGeometryIsNull()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixelsUnsafe();
            pixels.SetArea(null, new Span<QuantumType>(new QuantumType[] { 0 }));
        }

        [Fact]
        public void ShouldChangePixelsWhenGeometryAndSpanAreSpecified()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixelsUnsafe();
            var values = new QuantumType[113 * 108 * image.ChannelCount];
            pixels.SetArea(new MagickGeometry(10, 10, 113, 108), new Span<QuantumType>(values));

            ColorAssert.Equal(MagickColors.Black, image, (int)image.Width - 1, (int)image.Height - 1);
        }
    }
}

#endif
