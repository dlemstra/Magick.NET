// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheCropMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenWidthIsNegative()
        {
            {
                using var image = new MagickImage(Files.Builtin.Logo);
                Assert.Throws<ArgumentException>("width", () => image.Crop(-1, 50));
            }

            {
                using var image = new MagickImage(Files.Builtin.Logo);
                Assert.Throws<ArgumentException>("width", () => image.Crop(-1, 50, Gravity.Center));
            }
        }

        [Fact]
        public void ShouldThrowExceptionWhenHeightIsNegative()
        {
            {
                using var image = new MagickImage(Files.Builtin.Logo);
                Assert.Throws<ArgumentException>("height", () => image.Crop(40, -1));
            }

            {
                using var image = new MagickImage(Files.Builtin.Logo);
                Assert.Throws<ArgumentException>("height", () => image.Crop(40, -1, Gravity.Center));
            }
        }

        [Fact]
        public void ShouldSetImageToCorrectDimensions()
        {
            using var image = new MagickImage(Files.Builtin.Logo);
            image.Crop(40, 50);

            Assert.Equal(40, image.Width);
            Assert.Equal(50, image.Height);
        }

        [Fact]
        public void ShouldUseUndefinedGravityAsTheDefault()
        {
            using var image = new MagickImage(Files.Builtin.Logo);
            image.Crop(150, 40);

            Assert.Equal(150, image.Width);
            Assert.Equal(40, image.Height);
            ColorAssert.Equal(new MagickColor("#fecd08ff"), image, 146, 25);
        }

        [Fact]
        public void ShouldUseCenterGravity()
        {
            using var image = new MagickImage(Files.Builtin.Logo);
            image.Crop(50, 40, Gravity.Center);

            Assert.Equal(50, image.Width);
            Assert.Equal(40, image.Height);
            ColorAssert.Equal(new MagickColor("#223e92ff"), image, 25, 20);
        }

        [Fact]
        public void ShouldUseEastGravity()
        {
            using var image = new MagickImage(Files.Builtin.Logo);
            image.Crop(50, 40, Gravity.East);

            Assert.Equal(50, image.Width);
            Assert.Equal(40, image.Height);
            ColorAssert.Equal(MagickColors.White, image, 25, 20);
        }

        [Fact]
        public void ShouldUseAspectRatioOfMagickGeometry()
        {
            using var image = new MagickImage(Files.Builtin.Logo);
            image.Crop(new MagickGeometry("3:2"));

            Assert.Equal(640, image.Width);
            Assert.Equal(427, image.Height);
            ColorAssert.Equal(MagickColors.White, image, 222, 0);
        }

        [Fact]
        public void ShouldUseAspectRatioOfMagickGeometryAndGravity()
        {
            using var image = new MagickImage(Files.Builtin.Logo);
            image.Crop(new MagickGeometry("3:2"), Gravity.South);

            Assert.Equal(640, image.Width);
            Assert.Equal(427, image.Height);
            ColorAssert.Equal(MagickColors.Red, image, 222, 0);
        }

        [Fact]
        public void ShouldUseOffsetFromMagickGeometryAndGravity()
        {
            using var image = new MagickImage(Files.Builtin.Logo);
            image.Crop(new MagickGeometry(10, 10, 100, 100), Gravity.Center);

            Assert.Equal(100, image.Width);
            Assert.Equal(100, image.Height);
            ColorAssert.Equal(MagickColors.White, image, 99, 99);
        }

        [Fact]
        public void ShouldUseUndefinedGravityAsTheDefaultForMagickGeometry()
        {
            using var image = new MagickImage(Files.Builtin.Logo);
            image.Crop(new MagickGeometry("150x40"));

            Assert.Equal(150, image.Width);
            Assert.Equal(40, image.Height);

            ColorAssert.Equal(new MagickColor("#fecd08ff"), image, 146, 25);
        }
    }
}
