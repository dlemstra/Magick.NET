// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheDistortMethod
    {
        [Fact]
        public void ShouldThrowAnExceptionWhenArgumentsIsNull()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentNullException>("arguments", () => image.Distort(DistortMethod.Perspective, null));
        }

        [Fact]
        public void ShouldThrowAnExceptionWhenArgumentsIsNullAndSettingsIsNot()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentNullException>("arguments", () => image.Distort(new DistortSettings(DistortMethod.Perspective), null));
        }

        [Fact]
        public void ShouldThrowAnExceptionWhenArgumentsIsEmpty()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentException>("arguments", () => image.Distort(DistortMethod.Perspective, Array.Empty<double>()));
        }

        [Fact]
        public void ShouldThrowAnExceptionWhenArgumentsIsEmptyAndSettingsIsNot()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentException>("arguments", () => image.Distort(new DistortSettings(DistortMethod.Perspective), Array.Empty<double>()));
        }

        [Fact]
        public void ShouldThrowAnExceptionWhenSettingsIsNull()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentNullException>("settings", () => image.Distort(null, [0]));
        }

        [Fact]
        public void ShouldBeAbleToPerformPerspectiveDistortion()
        {
            using var image = new MagickImage(Files.MagickNETIconPNG);
            image.BackgroundColor = MagickColors.Cornsilk;
            image.VirtualPixelMethod = VirtualPixelMethod.Background;
            image.Distort(DistortMethod.Perspective, [0, 0, 0, 0, 0, 90, 0, 90, 90, 0, 90, 25, 90, 90, 90, 65]);
            image.Clamp();

            ColorAssert.Equal(new MagickColor("#0000"), image, 1, 64);
            ColorAssert.Equal(MagickColors.Cornsilk, image, 104, 50);
            ColorAssert.Equal(new MagickColor("#a8d8e007f90a"), image, 66, 62);
        }
    }
}
