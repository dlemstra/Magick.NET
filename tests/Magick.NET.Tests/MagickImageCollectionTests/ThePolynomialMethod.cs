// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageCollectionTests
{
    public class ThePolynomialMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenCollectionIsEmpty()
        {
            var terms = new double[] { 0.30, 1, 0.59, 1, 0.11, 1 };
            using var images = new MagickImageCollection();

            Assert.Throws<InvalidOperationException>(() => images.Polynomial(terms));
        }

        [Fact]
        public void ShouldThrowExceptionWhenTermsIsNull()
        {
            using var images = new MagickImageCollection(Files.Builtin.Logo);

            Assert.Throws<ArgumentNullException>("terms", () => images.Polynomial(null!));
        }

        [Fact]
        public void ShouldThrowExceptionWhenTermsIsEmpty()
        {
            using var images = new MagickImageCollection(Files.Builtin.Logo);

            Assert.Throws<ArgumentException>("terms", () => images.Polynomial(Array.Empty<double>()));
        }

        [Fact]
        public void ShouldCreateImage()
        {
            using var image = new MagickImage(Files.Builtin.Logo);
            var channels = image.Separate();

            using var images = new MagickImageCollection(channels);
            var terms = new double[] { 0.30, 1, 0.59, 1, 0.11, 1 };

            using var polynomial = images.Polynomial(terms);

            var distortion = polynomial.Compare(image, ErrorMetric.RootMeanSquared);
            Assert.InRange(distortion, 0.086, 0.087);
        }
    }
}
