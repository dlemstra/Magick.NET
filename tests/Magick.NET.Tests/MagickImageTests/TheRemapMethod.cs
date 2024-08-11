// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Collections.Generic;
using System.Linq;
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

public partial class MagickImageTests
{
    public class TheRemapMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenImageIsNull()
        {
            using var image = new MagickImage(Files.Builtin.Logo);

            Assert.Throws<ArgumentNullException>("image", () => image.Remap((IMagickImage<QuantumType>)null));
        }

        [Fact]
        public void ShouldThrowExceptionWhenColorsIsNull()
        {
            using var image = new MagickImage(Files.Builtin.Logo);

            Assert.Throws<ArgumentNullException>("colors", () => image.Remap((IEnumerable<MagickColor>)null));
        }

        [Fact]
        public void ShouldThrowExceptionWhenColorsIsEmpty()
        {
            using var image = new MagickImage(Files.Builtin.Logo);

            Assert.Throws<ArgumentException>("colors", () => image.Remap(Enumerable.Empty<MagickColor>()));
        }

        [Fact]
        public void ShouldUseTheColorsOfTheImage()
        {
            using var image = new MagickImage(Files.Builtin.Logo);
            using var colors = CreatePalleteImage();
            image.Remap(colors);

            ColorAssert.Equal(MagickColors.Blue, image, 0, 0);
            ColorAssert.Equal(MagickColors.Green, image, 455, 396);
            ColorAssert.Equal(MagickColors.Red, image, 505, 451);
        }

        [Fact]
        public void ShouldUseTheColors()
        {
            using var image = new MagickImage(Files.Builtin.Logo);
            var colors = new List<MagickColor>
            {
                MagickColors.Gold,
                MagickColors.Lime,
                MagickColors.Fuchsia,
            };
            image.Remap(colors);

            ColorAssert.Equal(MagickColors.Fuchsia, image, 0, 0);
            ColorAssert.Equal(MagickColors.Lime, image, 455, 396);
            ColorAssert.Equal(MagickColors.Gold, image, 512, 449);
        }

        private static IMagickImage<QuantumType> CreatePalleteImage()
        {
            using var images = new MagickImageCollection
            {
                new MagickImage(MagickColors.Red, 1, 1),
                new MagickImage(MagickColors.Blue, 1, 1),
                new MagickImage(MagickColors.Green, 1, 1),
            };

            return images.AppendHorizontally();
        }
    }
}
