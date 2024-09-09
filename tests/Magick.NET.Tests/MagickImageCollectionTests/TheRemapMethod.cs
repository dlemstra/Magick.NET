// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageCollectionTests
{
    public class TheRemapMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenCollectionIsEmpty()
        {
            using var remapImage = new MagickImage();
            using var images = new MagickImageCollection();

            Assert.Throws<InvalidOperationException>(() => images.Remap(remapImage));
        }

        [Fact]
        public void ShouldThrowExceptionWhenCollectionIsEmptyAndImageIsNotNull()
        {
            using var colors = new MagickImageCollection
            {
                new MagickImage(MagickColors.Red, 1, 1),
                new MagickImage(MagickColors.Green, 1, 1),
            };

            using var remapImage = colors.AppendHorizontally();
            using var images = new MagickImageCollection();

            Assert.Throws<InvalidOperationException>(() => images.Remap(remapImage));
        }

        [Fact]
        public void ShouldThrowExceptionWhenImageIsNull()
        {
            using var images = new MagickImageCollection();
            images.Read(Files.RoseSparkleGIF);

            Assert.Throws<ArgumentNullException>("image", () => images.Remap(null!));
        }

        [Fact]
        public void ShouldThrowExceptionWhenSettingsIsNull()
        {
            using var images = new MagickImageCollection();
            images.Read(Files.RoseSparkleGIF);

            Assert.Throws<ArgumentNullException>("settings", () => images.Remap(images[0], null!));
        }

        [Fact]
        public void ShouldDitherWhenSpecifiedInSettings()
        {
            using var colors = new MagickImageCollection
            {
                new MagickImage(MagickColors.Red, 1, 1),
                new MagickImage(MagickColors.Green, 1, 1),
            };

            using var remapImage = colors.AppendHorizontally();
            using var images = new MagickImageCollection();
            images.Read(Files.RoseSparkleGIF);

            var settings = new QuantizeSettings
            {
                DitherMethod = DitherMethod.FloydSteinberg,
            };

            images.Remap(remapImage, settings);

            ColorAssert.Equal(MagickColors.Red, images[0], 60, 17);
            ColorAssert.Equal(MagickColors.Green, images[0], 37, 24);

            ColorAssert.Equal(MagickColors.Red, images[1], 27, 45);
            ColorAssert.Equal(MagickColors.Green, images[1], 36, 26);

            ColorAssert.Equal(MagickColors.Red, images[2], 55, 12);
            ColorAssert.Equal(MagickColors.Green, images[2], 17, 21);
        }
    }
}
