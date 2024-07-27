// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Collections;
using System.Linq;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class UnsafePixelCollectionTests
{
    public class TheGetEnumeratorMethod
    {
        [Fact]
        public void ShouldReturnEnumerator()
        {
            using var image = new MagickImage(Files.CirclePNG);
            using var pixels = image.GetPixelsUnsafe();
            var enumerator = pixels.GetEnumerator();

            Assert.NotNull(enumerator);
        }

        [Fact]
        public void ShouldReturnEnumeratorForInterfaceImplementation()
        {
            using var image = new MagickImage(Files.CirclePNG);
            using var pixels = image.GetPixelsUnsafe();
            IEnumerable enumerable = pixels;

            Assert.NotNull(enumerable.GetEnumerator());
        }

        [Fact]
        public void ShouldReturnEnumeratorForFirst()
        {
            using var image = new MagickImage(Files.ConnectedComponentsPNG, 10, 10);
            var pixel = image.GetPixelsUnsafe().First(p => p.ToColor().Equals(MagickColors.Black));

            Assert.NotNull(pixel);
            Assert.Equal(350, pixel.X);
            Assert.Equal(196, pixel.Y);
            Assert.Equal(2U, pixel.Channels);
        }

        [Fact]
        public void ShouldReturnEnumeratorForCount()
        {
            using var image = new MagickImage(MagickColors.Red, 5, 10);
            using var pixels = image.GetPixelsUnsafe();

            Assert.Equal(50, pixels.Count());
        }
    }
}
