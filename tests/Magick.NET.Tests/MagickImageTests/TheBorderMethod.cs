// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheBorderMethod
        {
            [Fact]
            public void ShouldAddBorderOnAllSides()
            {
                using (var image = new MagickImage("xc:red", 1, 1))
                {
                    image.BorderColor = MagickColors.Green;
                    image.Border(3);

                    Assert.Equal(7, image.Width);
                    Assert.Equal(7, image.Height);
                    ColorAssert.Equal(MagickColors.Green, image, 1, 1);
                }
            }

            [Fact]
            public void ShouldOnlyAddVerticalBorderWhenOnlyWidthIsSpecified()
            {
                using (var image = new MagickImage("xc:red", 1, 1))
                {
                    image.Border(3, 0);

                    Assert.Equal(7, image.Width);
                    Assert.Equal(1, image.Height);
                }
            }

            [Fact]
            public void ShouldOnlyAddHorizontalBorderWhenOnlyHeightIsSpecified()
            {
                using (var image = new MagickImage("xc:red", 1, 1))
                {
                    image.Border(0, 3);

                    Assert.Equal(1, image.Width);
                    Assert.Equal(7, image.Height);
                }
            }
        }
    }
}
