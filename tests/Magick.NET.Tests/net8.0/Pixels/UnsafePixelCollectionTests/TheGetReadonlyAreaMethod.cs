// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if NETCOREAPP

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class UnsafePixelCollectionTests
{
    public partial class TheGetReadonlyAreaMethod
    {
        [Fact]
        public void ShouldNotThrowExceptionWhenAreaIsInvalid()
        {
            using var image = new MagickImage(MagickColors.Red, 1, 1);
            using var pixels = image.GetPixelsUnsafe();
            var area = pixels.GetReadOnlyArea(1, 2, 3, 4);

            Assert.Equal(36, area.Length);
        }

        [Fact]
        public void ShouldNotThrowExceptionWhenGeometryIsNull()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            using var pixels = image.GetPixelsUnsafe();
            var area = pixels.GetReadOnlyArea(null!);

            Assert.Equal(0, area.Length);
        }

        [Fact]
        public void ShouldReturnAllPixelsOfTheImage()
        {
            using var input = new MagickImage(Files.ImageMagickJPG);
            using var pixels = input.GetPixelsUnsafe();
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
