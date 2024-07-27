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
    public partial class TheSetPixelsMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenSpanHasInvalidSize()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixels();

            Assert.Throws<ArgumentException>("values", () => pixels.SetPixels(new Span<QuantumType>(new QuantumType[] { 0, 0, 0, 0 })));
        }

        [Fact]
        public void ShouldThrowExceptionWhenSpanIsTooLong()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixels();
            var values = new QuantumType[(image.Width * image.Height * image.ChannelCount) + 1];

            Assert.Throws<ArgumentException>("values", () => pixels.SetPixels(new Span<QuantumType>(values)));
        }

        [Fact]
        public void ShouldChangePixelsWhenSpanHasMaxNumberOfValues()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixels();
            var values = new QuantumType[image.Width * image.Height * image.ChannelCount];
            pixels.SetPixels(new Span<QuantumType>(values));

            ColorAssert.Equal(MagickColors.Black, image, (int)image.Width - 1, (int)image.Height - 1);
        }
    }
}

#endif
