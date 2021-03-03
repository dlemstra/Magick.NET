// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageCollectionTests
    {
        public class TheFlattenMethod
        {
            [Fact]
            public void ShouldThrowExceptionWhenCollectionIsEmpty()
            {
                using (var images = new MagickImageCollection())
                {
                    Assert.Throws<InvalidOperationException>(() =>
                    {
                        images.Flatten();
                    });
                }
            }

            [Fact]
            public void ShouldUseImageBackground()
            {
                using (var images = new MagickImageCollection())
                {
                    var image = new MagickImage(MagickColors.Red, 10, 10);
                    image.Extent(110, 110, Gravity.Center, MagickColors.None);
                    image.BackgroundColor = MagickColors.Moccasin;

                    images.Add(image);

                    using (var result = images.Flatten())
                    {
                        ColorAssert.Equal(MagickColors.Moccasin, result, 0, 0);
                    }
                }
            }

            [Fact]
            public void ShouldUseSpecifiedBackground()
            {
                using (var images = new MagickImageCollection())
                {
                    var image = new MagickImage(MagickColors.Red, 10, 10);
                    image.Extent(110, 110, Gravity.Center, MagickColors.None);
                    image.BackgroundColor = MagickColors.Moccasin;

                    images.Add(image);

                    using (var result = images.Flatten(MagickColors.MistyRose))
                    {
                        ColorAssert.Equal(MagickColors.MistyRose, result, 0, 0);
                        Assert.Equal(MagickColors.Moccasin, image.BackgroundColor);
                    }
                }
            }
        }
    }
}
