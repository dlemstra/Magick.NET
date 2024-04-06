// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageCollectionTests
{
    public class TheMontageMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenCollectionIsEmpty()
        {
            var settings = new MontageSettings();
            using var images = new MagickImageCollection();

            Assert.Throws<InvalidOperationException>(() => images.Montage(settings));
        }

        [Fact]
        public void ShouldThrowExceptionWhenSettingsIsNull()
        {
            using var images = new MagickImageCollection
            {
                new MagickImage(MagickColors.Magenta, 1, 1),
            };

            Assert.Throws<ArgumentNullException>("settings", () => images.Montage(null));
        }

        [Fact]
        public void ShouldMontageTheImages()
        {
            using var images = new MagickImageCollection();

            for (var i = 0; i < 9; i++)
                images.Add(Files.Builtin.Logo);

            var settings = new MontageSettings
            {
                Geometry = new MagickGeometry("200x200"),
                TileGeometry = new MagickGeometry("2x"),
            };

            using var montageResult = images.Montage(settings);

            Assert.NotNull(montageResult);
            Assert.Equal(400, montageResult.Width);
            Assert.Equal(1000, montageResult.Height);
        }
    }
}
