// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Linq;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheCropToTilesMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenGeometryIsNull()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentNullException>("geometry", () => image.CropToTiles(null));
        }

        [Fact]
        public void ShouldThrowExceptionWhenWidthIsZero()
        {
            using var image = new MagickImage();

            var exception = Assert.Throws<MagickCorruptImageErrorException>(() => image.CropToTiles(0, 1));
            Assert.Contains("negative or zero image size", exception.Message);
        }

        [Fact]
        public void ShouldThrowExceptionWhenWidthIsNegative()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentException>("width", () => image.CropToTiles(-1, 1));
        }

        [Fact]
        public void ShouldThrowExceptionWhenHeightIsZero()
        {
            using var image = new MagickImage();

            var exception = Assert.Throws<MagickCorruptImageErrorException>(() => image.CropToTiles(1, 0));
            Assert.Contains("negative or zero image size", exception.Message);
        }

        [Fact]
        public void ShouldThrowExceptionWhenHeightIsNegative()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentException>("height", () => image.CropToTiles(1, -1));
        }

        [Fact]
        public void ShouldCreateTilesOfTheSpecifiedSize()
        {
            using var image = new MagickImage(Files.Builtin.Logo);
            var tiles = image.CropToTiles(48, 48).ToArray();
            Assert.Equal(140, tiles.Length);

            for (var i = 0; i < tiles.Length; i++)
            {
                var tile = tiles[i];

                Assert.Equal(48, tile.Height);

                if (i == 13 || (i - 13) % 14 == 0)
                    Assert.Equal(16, tile.Width);
                else
                    Assert.Equal(48, tile.Width);

                tile.Dispose();
            }
        }
    }
}
