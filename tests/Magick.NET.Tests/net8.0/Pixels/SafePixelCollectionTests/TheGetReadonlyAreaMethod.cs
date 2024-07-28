// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if NETCOREAPP

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class SafePixelCollectionTests
{
    public partial class TheGetReadonlyAreaMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenAreaIsInvalid()
        {
            using var image = new MagickImage(MagickColors.Red, 1, 1);
            using var pixels = image.GetPixels();

            Assert.Throws<ArgumentOutOfRangeException>(() => pixels.GetReadOnlyArea(1, 2, 3, 4));
        }

        [Fact]
        public void ShouldNotThrowExceptionWhenGeometryIsNull()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixels();

            Assert.Throws<ArgumentNullException>("geometry", () => pixels.GetReadOnlyArea(null));
        }

        [Fact]
        public void ShouldReturnAllPixelsOfTheImage()
        {
            using var input = new MagickImage(Files.ImageMagickJPG);
            using var pixels = input.GetPixels();
            var area = pixels.GetReadOnlyArea(0, 0, input.Width, input.Height);

            Assert.Equal(43542, area.Length);

            var settings = new PixelReadSettings(input.Width, input.Height, StorageType.Quantum, PixelMapping.RGB);
            using var output = new MagickImage();
            output.ReadPixels(area, settings);

            var difference = output.Compare(input, ErrorMetric.RootMeanSquared);
            Assert.Equal(0.0, difference);
        }
    }
}

#endif
