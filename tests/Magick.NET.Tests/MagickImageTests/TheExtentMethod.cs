// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheExtentMethod
    {
        public class WithWidthAndHeight
        {
            [Fact]
            public void ShouldExtentTheImage()
            {
                using var image = new MagickImage(MagickColors.Black, 1, 1);
                image.Extent(2, 3);

                Assert.Equal(2U, image.Width);
                Assert.Equal(3U, image.Height);
            }

            [Fact]
            public void ShouldExtentTheImageWithTheSpecifiedColor()
            {
                using var image = new MagickImage(MagickColors.Black, 1, 1);
                image.Extent(2, 3, MagickColors.Purple);

                Assert.Equal(2U, image.Width);
                Assert.Equal(3U, image.Height);
                ColorAssert.Equal(MagickColors.Purple, image, 1, 1);
            }

            [Fact]
            public void ShouldExtentTheImageWithTheSpecifiedGravity()
            {
                using var image = new MagickImage(MagickColors.Black, 1, 1);
                image.BackgroundColor = MagickColors.Purple;
                image.Extent(2, 3, Gravity.Southeast);

                Assert.Equal(2U, image.Width);
                Assert.Equal(3U, image.Height);
                ColorAssert.Equal(MagickColors.Purple, image, 0, 0);
            }

            [Fact]
            public void ShouldExtentTheImageWithTheSpecifiedGravityAndColor()
            {
                using var image = new MagickImage(MagickColors.Black, 1, 1);
                image.Extent(2, 3, Gravity.Northwest, MagickColors.Purple);

                Assert.Equal(2U, image.Width);
                Assert.Equal(3U, image.Height);
                ColorAssert.Equal(MagickColors.Purple, image, 1, 1);
            }
        }

        public class WithWidthAndHeightAndOffset
        {
            [Fact]
            public void ShouldExtentTheImageAtOffset()
            {
                using var image = new MagickImage(MagickColors.Black, 1, 1);
                image.BackgroundColor = MagickColors.Purple;
                image.Extent(-1, -1, 2, 3);

                Assert.Equal(2U, image.Width);
                Assert.Equal(3U, image.Height);
                ColorAssert.Equal(MagickColors.Purple, image, 0, 0);
            }
        }

        public class WithGeometry
        {
            [Fact]
            public void ShouldThrowExceptionWhenGeometryIsNull()
            {
                using var image = new MagickImage(MagickColors.Black, 1, 1);

                Assert.Throws<ArgumentNullException>("geometry", () => image.Extent(null));
            }

            [Fact]
            public void ShouldExtentTheImage()
            {
                using var image = new MagickImage(MagickColors.Black, 1, 1);
                image.Extent(new MagickGeometry(2, 3));

                Assert.Equal(2U, image.Width);
                Assert.Equal(3U, image.Height);
            }

            [Fact]
            public void ShouldExtentTheImageAtOffset()
            {
                using var image = new MagickImage(MagickColors.Black, 1, 1);
                image.BackgroundColor = MagickColors.Purple;
                image.Extent(new MagickGeometry(-1, -1, 2, 3));

                Assert.Equal(2U, image.Width);
                Assert.Equal(3U, image.Height);
                ColorAssert.Equal(MagickColors.Purple, image, 0, 0);
            }

            [Fact]
            public void ShouldExtentTheImageWithTheSpecifiedColor()
            {
                using var image = new MagickImage(MagickColors.Black, 1, 1);
                image.Extent(new MagickGeometry(2, 3), MagickColors.Purple);

                Assert.Equal(2U, image.Width);
                Assert.Equal(3U, image.Height);
                ColorAssert.Equal(MagickColors.Purple, image, 1, 1);
            }

            [Fact]
            public void ShouldExtentTheImageWithTheSpecifiedGravity()
            {
                using var image = new MagickImage(MagickColors.Black, 1, 1);
                image.BackgroundColor = MagickColors.Purple;
                image.Extent(new MagickGeometry(2, 3), Gravity.Southwest);

                Assert.Equal(2U, image.Width);
                Assert.Equal(3U, image.Height);
                ColorAssert.Equal(MagickColors.Purple, image, 0, 0);
            }

            [Fact]
            public void ShouldExtentTheImageWithTheSpecifiedGravityAndColor()
            {
                using var image = new MagickImage(MagickColors.Black, 1, 1);
                image.Extent(new MagickGeometry(2, 3), Gravity.Southwest, MagickColors.Purple);

                Assert.Equal(2U, image.Width);
                Assert.Equal(3U, image.Height);
                ColorAssert.Equal(MagickColors.Purple, image, 0, 0);
            }
        }
    }
}
