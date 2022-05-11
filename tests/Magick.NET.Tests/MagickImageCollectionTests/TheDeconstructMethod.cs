// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageCollectionTests
    {
        public class TheDeconstructMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenCollectionIsEmpty()
            {
                using (var images = new MagickImageCollection())
                {
                    Assert.Throws<InvalidOperationException>(() =>
                    {
                        images.Deconstruct();
                    });
                }
            }

            [Fact]
            public void ShouldDeconstructTheImages()
            {
                using (var collection = new MagickImageCollection())
                {
                    collection.Add(new MagickImage(MagickColors.Red, 20, 20));

                    using (var frames = new MagickImageCollection())
                    {
                        frames.Add(new MagickImage(MagickColors.Red, 10, 20));
                        frames.Add(new MagickImage(MagickColors.Purple, 10, 20));

                        collection.Add(frames.AppendHorizontally());
                    }

                    Assert.Equal(20, collection[1].Width);
                    Assert.Equal(20, collection[1].Height);
                    Assert.Equal(new MagickGeometry(0, 0, 10, 20), collection[1].Page);
                    ColorAssert.Equal(MagickColors.Red, collection[1], 3, 3);

                    collection.Deconstruct();

                    Assert.Equal(10, collection[1].Width);
                    Assert.Equal(20, collection[1].Height);
                    Assert.Equal(new MagickGeometry(10, 0, 10, 20), collection[1].Page);
                    ColorAssert.Equal(MagickColors.Purple, collection[1], 3, 3);
                }
            }
        }
    }
}
