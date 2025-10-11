﻿// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheTransformColorSpaceMethod
    {
        [Fact]
        public void ShouldReturnFalseWhenTheImageHasNoProfile()
        {
            using var image = new MagickImage(Files.Builtin.Logo);
            var result = image.TransformColorSpace(ColorProfiles.AdobeRGB1998);

            Assert.False(result);
        }

        [Fact]
        public void ShouldReturnTrueWhenTheImageHasProfile()
        {
            using var image = new MagickImage(Files.PictureJPG);
            var result = image.TransformColorSpace(ColorProfiles.SRGB);

            Assert.True(result);
        }

        [Fact]
        public void ShouldReturnFalseWhenSourceProfileColorSpaceIsIncorrect()
        {
            using var image = new MagickImage(Files.MagickNETIconPNG);
            var result = image.TransformColorSpace(ColorProfiles.USWebCoatedSWOP, ColorProfiles.AdobeRGB1998);

            Assert.False(result);
        }

        [Fact]
        public void ShouldReturnTrueWhenSourceProfileColorSpaceIsCorrect()
        {
            using var image = new MagickImage(Files.MagickNETIconPNG);
            var result = image.TransformColorSpace(ColorProfiles.SRGB, ColorProfiles.AdobeRGB1998);

            Assert.True(result);
        }

        [Fact]
        public void ShouldReturnTrueWhenSourceProfileColorSpaceIsCorrectAndTheImageHasNoProfile()
        {
            using var image = new MagickImage(Files.Builtin.Logo);
            var result = image.TransformColorSpace(ColorProfiles.SRGB, ColorProfiles.AdobeRGB1998);

            Assert.True(result);
        }

        [Fact]
        public void ShouldNotChangeTheColorSpaceWhenSourceColorSpaceIsIncorrect()
        {
            using var image = new MagickImage(Files.MagickNETIconPNG);

            Assert.Equal(ColorSpace.sRGB, image.ColorSpace);

            image.TransformColorSpace(ColorProfiles.USWebCoatedSWOP, ColorProfiles.USWebCoatedSWOP);

            Assert.Equal(ColorSpace.sRGB, image.ColorSpace);
        }

        [Fact]
        public void ShouldChangeTheColorSpace()
        {
            using var image = new MagickImage(Files.MagickNETIconPNG);

            Assert.Equal(ColorSpace.sRGB, image.ColorSpace);

            image.TransformColorSpace(ColorProfiles.SRGB, ColorProfiles.USWebCoatedSWOP);

            Assert.Equal(ColorSpace.CMYK, image.ColorSpace);
        }

        [Fact]
        public void ShouldClampPixels()
        {
            using var image = new MagickImage(MagickColors.White, 1, 1);
            image.TransformColorSpace(ColorProfiles.SRGB, ColorProfiles.AdobeRGB1998);

#if Q8 || Q16
            ColorAssert.Equal(new MagickColor("#ffffff"), image, 1, 1);
#else
            ColorAssert.Equal(new MagickColor(65538f, 65531f, 65542f, 65535f), image, 1, 1);
#endif
        }

        [Fact]
        public void ShouldUseTheSpecifiedMode()
        {
            using var quantumImage = new MagickImage(Files.PictureJPG);
            quantumImage.TransformColorSpace(ColorProfiles.USWebCoatedSWOP);

            using var highResImage = new MagickImage(Files.PictureJPG);
            highResImage.TransformColorSpace(ColorProfiles.USWebCoatedSWOP, ColorTransformMode.HighRes);

            var difference = quantumImage.Compare(highResImage, ErrorMetric.RootMeanSquared);

#if Q16HDRI
            Assert.Equal(0.0, difference);
#else
            Assert.NotEqual(0.0, difference);
#endif
        }
    }
}
