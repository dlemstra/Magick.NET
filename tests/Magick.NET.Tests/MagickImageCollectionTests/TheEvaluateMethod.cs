// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageCollectionTests
{
    public class TheEvaluateMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenCollectionIsEmpty()
        {
            using var images = new MagickImageCollection();

            Assert.Throws<InvalidOperationException>(() => images.Evaluate(EvaluateOperator.Exponential));
        }

        [Fact]
        public void ShouldEvaluateTheImages()
        {
            using var images = new MagickImageCollection();

            images.Add(new MagickImage(MagickColors.Yellow, 40, 10));

            using var frames = new MagickImageCollection();
            frames.Add(new MagickImage(MagickColors.Green, 10, 10));
            frames.Add(new MagickImage(MagickColors.White, 10, 10));
            frames.Add(new MagickImage(MagickColors.Black, 10, 10));
            frames.Add(new MagickImage(MagickColors.Yellow, 10, 10));

            images.Add(frames.AppendHorizontally());

            using var image = images.Evaluate(EvaluateOperator.Min);
            ColorAssert.Equal(MagickColors.Green, image, 0, 0);
            ColorAssert.Equal(MagickColors.Yellow, image, 10, 0);
            ColorAssert.Equal(MagickColors.Black, image, 20, 0);
            ColorAssert.Equal(MagickColors.Yellow, image, 30, 0);
        }
    }
}
