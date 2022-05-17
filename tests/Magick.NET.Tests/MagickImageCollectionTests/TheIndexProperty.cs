// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageCollectionTests
    {
        public class TheIndexProperty
        {
            [Fact]
            public void ShouldThrowExceptionWhenValueIsNull()
            {
                using (var collection = new MagickImageCollection())
                {
                    var exception = Assert.Throws<InvalidOperationException>(() =>
                    {
                        collection[0] = null;
                    });

                    Assert.Equal("Not allowed to set null value.", exception.Message);
                }
            }

            [Fact]
            public void ShouldThrowExceptionWhenAddingTheSameImage()
            {
                using (var collection = new MagickImageCollection())
                {
                    collection.Add(new MagickImage(MagickColors.Red, 1, 1));
                    collection.Add(new MagickImage(MagickColors.Red, 1, 1));

                    var exception = Assert.Throws<InvalidOperationException>(() =>
                    {
                        collection[0] = collection[1];
                    });

                    Assert.Equal("Not allowed to add the same image to the collection.", exception.Message);
                }
            }

            [Fact]
            public void ShouldBeAbleToOverwriteImageWithSameImage()
            {
                using (var collection = new MagickImageCollection())
                {
                    var image = new MagickImage(MagickColors.Red, 1, 1);
                    collection.Add(image);

                    collection[0] = collection[0];

                    Assert.Same(image, collection[0]);
                }
            }
        }
    }
}
