// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheSortPixelsMethod
        {
            [Fact]
            public void ShouldSortThePixels()
            {
                using (var image = new MagickImage(MagickColors.Blue, 1, 1))
                {
                    image.Extent(1, 2, MagickColors.Green);
                    image.Extent(2, 2, MagickColors.Red);

                    image.SortPixels();

                    ColorAssert.Equal(MagickColors.Blue, image, 0, 0);
                    ColorAssert.Equal(MagickColors.Red, image, 1, 0);
                    ColorAssert.Equal(MagickColors.Red, image, 0, 1);
                    ColorAssert.Equal(MagickColors.Green, image, 1, 1);
                }
            }
        }
    }
}
