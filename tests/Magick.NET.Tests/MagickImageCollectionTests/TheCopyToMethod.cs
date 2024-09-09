// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageCollectionTests
{
    public class TheCopyToMethod
    {
        [Fact]
        public void ShouldDoNothingWhenCollectionIsEmpty()
        {
            using var images = new MagickImageCollection();

            images.CopyTo(null!, -1);
        }

        [Fact]
        public void ShouldThrowExceptionWhenArrayIsNull()
        {
            using var images = new MagickImageCollection();
            images.Add(new MagickImage(MagickColors.Red, 1, 1));

            Assert.Throws<ArgumentNullException>("array", () => images.CopyTo(null!, -1));
        }

        [Fact]
        public void ShouldThrowExceptionWhenIndexIsNegative()
        {
            using var images = new MagickImageCollection();
            images.Add(new MagickImage(MagickColors.Red, 1, 1));

            var array = new MagickImage[1];

            Assert.Throws<ArgumentOutOfRangeException>("arrayIndex", () => images.CopyTo(array, -1));
        }

        [Fact]
        public void ShouldThrowExceptionWhenIndexIsTooHigh()
        {
            using var images = new MagickImageCollection();
            images.Add(new MagickImage(MagickColors.Red, 1, 1));

            var array = new MagickImage[2];

            Assert.Throws<ArgumentOutOfRangeException>("arrayIndex", () => images.CopyTo(array, 1));
        }

        [Fact]
        public void ShouldThrowExceptionWhenIndexIsOutOfArrayRange()
        {
            using var images = new MagickImageCollection();
            images.Add(new MagickImage(MagickColors.Red, 1, 1));
            images.Add(new MagickImage(MagickColors.Red, 1, 1));

            var array = new MagickImage[1];

            Assert.Throws<ArgumentOutOfRangeException>("arrayIndex", () => images.CopyTo(array, 1));
        }

        [Fact]
        public void ShouldCopyImagesToTheArray()
        {
            using var images = new MagickImageCollection();
            images.Add(new MagickImage(MagickColors.Red, 1, 1));
            images.Add(new MagickImage(MagickColors.Red, 2, 2));

            var array = new MagickImage[4];

            images.CopyTo(array, 1);

            Assert.Null(array[0]);

            Assert.NotNull(array[1]);
            Assert.Equal(1U, array[1].Width);
            Assert.False(ReferenceEquals(array[1], images[0]));

            Assert.NotNull(array[2]);
            Assert.Equal(2U, array[2].Width);
            Assert.False(ReferenceEquals(array[2], images[0]));

            Assert.Null(array[3]);
        }
    }
}
