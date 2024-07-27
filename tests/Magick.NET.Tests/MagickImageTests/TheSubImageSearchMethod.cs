// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheSubImageSearchMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenImageIsNull()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentNullException>("image", () => image.SubImageSearch(null));
        }

        [Fact]
        public void ShouldFindTheSpecifiedImageInTheImage()
        {
            using var images = new MagickImageCollection();
            images.Add(new MagickImage(MagickColors.Green, 2, 2));
            images.Add(new MagickImage(MagickColors.Red, 2, 2));

            using var combined = images.AppendHorizontally();
            using var searchResult = combined.SubImageSearch(new MagickImage(MagickColors.Red, 1, 1), ErrorMetric.RootMeanSquared);

            Assert.NotNull(searchResult);
            Assert.NotNull(searchResult.SimilarityImage);
            Assert.NotNull(searchResult.BestMatch);
            Assert.Equal(0.0, searchResult.SimilarityMetric);
            Assert.Equal(2, searchResult.BestMatch.X);
            Assert.Equal(0, searchResult.BestMatch.Y);
            Assert.Equal(1U, searchResult.BestMatch.Width);
            Assert.Equal(1U, searchResult.BestMatch.Height);
        }
    }
}
