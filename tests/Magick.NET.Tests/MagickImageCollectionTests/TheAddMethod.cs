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
    public class TheAddMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenItemIsNull()
        {
            using var images = new MagickImageCollection();

            Assert.Throws<ArgumentNullException>("item", () => images.Add((IMagickImage<QuantumType>)null!));
        }

        [Fact]
        public void ShouldThrowExceptionWhenFileNameIsNull()
        {
            using var images = new MagickImageCollection();

            Assert.Throws<ArgumentNullException>("fileName", () => images.Add((string)null!));
        }

        [Fact]
        public void ShouldThrowExceptionWhenFileNameIsEmpty()
        {
            using var images = new MagickImageCollection();

            Assert.Throws<ArgumentException>("fileName", () => images.Add(string.Empty));
        }

        [Fact]
        public void ShouldThrowExceptionWhenCollectionAlreadyContainsItem()
        {
            using var images = new MagickImageCollection();

            var image = new MagickImage();
            images.Add(image);

            Assert.Throws<InvalidOperationException>(() => images.Add(image));
        }
    }
}
