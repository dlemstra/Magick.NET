// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageCollectionTests
{
    public class TheCombineMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenCollectionIsEmpty()
        {
            using var images = new MagickImageCollection();

            Assert.Throws<InvalidOperationException>(() => images.Combine());
        }

        [Fact]
        public void ShouldCombineRgbImage()
        {
            using var rose = new MagickImage(Files.Builtin.Rose);
            using var images = new MagickImageCollection();
            images.AddRange(rose.Separate(Channels.RGB));

            Assert.Equal(3, images.Count);

            using var image = images.Combine();

            Assert.Equal(rose.TotalColors, image.TotalColors);
            Assert.Equal(3U, image.ChannelCount);
            Assert.Equal(0U, image.MetaChannelCount);
            Assert.False(image.HasAlpha);
        }

        [Fact]
        public void ShouldCombineRgbaImage()
        {
            using var rose = new MagickImage(Files.MagickNETIconPNG);
            using var images = new MagickImageCollection();
            images.AddRange(rose.Separate(Channels.RGBA));

            Assert.Equal(4, images.Count);

            using var image = images.Combine();

            Assert.Equal(rose.TotalColors, image.TotalColors);
            Assert.Equal(4U, image.ChannelCount);
            Assert.Equal(0U, image.MetaChannelCount);
            Assert.True(image.HasAlpha);
        }

        [Fact]
        public void ShouldCombineRgbaImageWithMetaChannels()
        {
            using var icon = new MagickImage(Files.MagickNETIconPNG);
            using var images = new MagickImageCollection();
            images.AddRange(icon.Separate(Channels.RGBA));
            images.Add(new MagickImage(MagickColors.Black, icon.Width, icon.Height));

            Assert.Equal(5, images.Count);

            using var image = images.Combine();

            Assert.Equal(0.0, icon.Compare(image, ErrorMetric.RootMeanSquared));
            Assert.Equal(icon.TotalColors, image.TotalColors);
            Assert.Equal(5U, image.ChannelCount);
            Assert.Equal(1U, image.MetaChannelCount);
            Assert.True(image.HasAlpha);
        }

        [Fact]
        public void ShouldCombineCmykImage()
        {
            using var cmyk = new MagickImage(Files.CMYKJPG);
            using var images = new MagickImageCollection();
            images.AddRange(cmyk.Separate(Channels.CMYK));

            Assert.Equal(4, images.Count);

            using var image = images.Combine(ColorSpace.CMYK);

            Assert.Equal(0.0, cmyk.Compare(image, ErrorMetric.RootMeanSquared));
            Assert.Equal(cmyk.TotalColors, image.TotalColors);
            Assert.Equal(4U, image.ChannelCount);
            Assert.Equal(0U, image.MetaChannelCount);
            Assert.False(image.HasAlpha);
        }

        [Fact]
        public void ShouldCombineCmykaImage()
        {
            using var cmyk = new MagickImage(Files.CMYKJPG);
            using var images = new MagickImageCollection();
            images.AddRange(cmyk.Separate(Channels.CMYKA));
            images.Add(new MagickImage(MagickColors.Black, cmyk.Width, cmyk.Height));

            Assert.Equal(5, images.Count);

            using var image = images.Combine(ColorSpace.CMYK);

            Assert.Equal(cmyk.TotalColors, image.TotalColors);
            Assert.Equal(5U, image.ChannelCount);
            Assert.Equal(0U, image.MetaChannelCount);
            Assert.True(image.HasAlpha);
        }

        [Fact]
        public void ShouldCombineCmykaImageWithMetaChannels()
        {
            using var cmyk = new MagickImage(Files.CMYKJPG);
            using var images = new MagickImageCollection();
            images.AddRange(cmyk.Separate(Channels.CMYKA));
            images.Add(new MagickImage(MagickColors.Black, cmyk.Width, cmyk.Height));
            images.Add(new MagickImage(MagickColors.Purple, cmyk.Width, cmyk.Height));

            Assert.Equal(6, images.Count);

            using var image = images.Combine(ColorSpace.CMYK);

            Assert.Equal(cmyk.TotalColors, image.TotalColors);
            Assert.Equal(6U, image.ChannelCount);
            Assert.Equal(1U, image.MetaChannelCount);
            Assert.True(image.HasAlpha);
        }
    }
}
