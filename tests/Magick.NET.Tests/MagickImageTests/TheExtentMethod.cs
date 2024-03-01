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
            public void ShouldThrowExceptionWhenWidthIsNegative()
            {
                {
                    using var image = new MagickImage(MagickColors.Black, 1, 1);
                    Assert.Throws<ArgumentException>("width", () => image.Extent(-1, 3));
                }

                {
                    using var image = new MagickImage(MagickColors.Black, 1, 1);
                    Assert.Throws<ArgumentException>("width", () => image.Extent(2, 3, -1, 3));
                }

                {
                    using var image = new MagickImage(MagickColors.Black, 1, 1);
                    Assert.Throws<ArgumentException>("width", () => image.Extent(-1, 3, MagickColors.Purple));
                }

                {
                    using var image = new MagickImage(MagickColors.Black, 1, 1);
                    Assert.Throws<ArgumentException>("width", () => image.Extent(-1, 3, Gravity.Center));
                }

                {
                    using var image = new MagickImage(MagickColors.Black, 1, 1);
                    Assert.Throws<ArgumentException>("width", () => image.Extent(-1, 3, Gravity.Center, MagickColors.Purple));
                }
            }

            [Fact]
            public void ShouldThrowExceptionWhenHeightIsNegative()
            {
                {
                    using var image = new MagickImage(MagickColors.Black, 1, 1);
                    Assert.Throws<ArgumentException>("height", () => image.Extent(2, -1));
                }

                {
                    using var image = new MagickImage(MagickColors.Black, 1, 1);
                    Assert.Throws<ArgumentException>("height", () => image.Extent(2, 3, 2, -1));
                }

                {
                    using var image = new MagickImage(MagickColors.Black, 1, 1);
                    Assert.Throws<ArgumentException>("height", () => image.Extent(2, -1, MagickColors.Purple));
                }

                {
                    using var image = new MagickImage(MagickColors.Black, 1, 1);
                    Assert.Throws<ArgumentException>("height", () => image.Extent(2, -1, Gravity.Center));
                }

                {
                    using var image = new MagickImage(MagickColors.Black, 1, 1);
                    Assert.Throws<ArgumentException>("height", () => image.Extent(2, -1, Gravity.Center, MagickColors.Purple));
                }
            }

            [Fact]
            public void ShouldExtentTheImage()
            {
                using var image = new MagickImage(MagickColors.Black, 1, 1);
                image.Extent(2, 3);

                Assert.Equal(2, image.Width);
                Assert.Equal(3, image.Height);
            }

            [Fact]
            public void ShouldExtentTheImageAtOffset()
            {
                using var image = new MagickImage(MagickColors.Black, 1, 1);
                image.BackgroundColor = MagickColors.Purple;
                image.Extent(-1, -1, 2, 3);

                Assert.Equal(2, image.Width);
                Assert.Equal(3, image.Height);
                ColorAssert.Equal(MagickColors.Purple, image, 0, 0);
            }

            [Fact]
            public void ShouldExtentTheImageWithTheSpecifiedColor()
            {
                using var image = new MagickImage(MagickColors.Black, 1, 1);
                image.Extent(2, 3, MagickColors.Purple);

                Assert.Equal(2, image.Width);
                Assert.Equal(3, image.Height);
                ColorAssert.Equal(MagickColors.Purple, image, 1, 1);
            }

            [Fact]
            public void ShouldExtentTheImageWithTheSpecifiedGravity()
            {
                using var image = new MagickImage(MagickColors.Black, 1, 1);
                image.BackgroundColor = MagickColors.Purple;
                image.Extent(2, 3, Gravity.Southeast);

                Assert.Equal(2, image.Width);
                Assert.Equal(3, image.Height);
                ColorAssert.Equal(MagickColors.Purple, image, 0, 0);
            }

            [Fact]
            public void ShouldExtentTheImageWithTheSpecifiedGravityAndColor()
            {
                using var image = new MagickImage(MagickColors.Black, 1, 1);
                image.Extent(2, 3, Gravity.Northwest, MagickColors.Purple);

                Assert.Equal(2, image.Width);
                Assert.Equal(3, image.Height);
                ColorAssert.Equal(MagickColors.Purple, image, 1, 1);
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

                Assert.Equal(2, image.Width);
                Assert.Equal(3, image.Height);
            }

            [Fact]
            public void ShouldExtentTheImageAtOffset()
            {
                using var image = new MagickImage(MagickColors.Black, 1, 1);
                image.BackgroundColor = MagickColors.Purple;
                image.Extent(new MagickGeometry(-1, -1, 2, 3));

                Assert.Equal(2, image.Width);
                Assert.Equal(3, image.Height);
                ColorAssert.Equal(MagickColors.Purple, image, 0, 0);
            }

            [Fact]
            public void ShouldExtentTheImageWithTheSpecifiedColor()
            {
                using var image = new MagickImage(MagickColors.Black, 1, 1);
                image.Extent(new MagickGeometry(2, 3), MagickColors.Purple);

                Assert.Equal(2, image.Width);
                Assert.Equal(3, image.Height);
                ColorAssert.Equal(MagickColors.Purple, image, 1, 1);
            }

            [Fact]
            public void ShouldExtentTheImageWithTheSpecifiedGravity()
            {
                using var image = new MagickImage(MagickColors.Black, 1, 1);
                image.BackgroundColor = MagickColors.Purple;
                image.Extent(new MagickGeometry(2, 3), Gravity.Southwest);

                Assert.Equal(2, image.Width);
                Assert.Equal(3, image.Height);
                ColorAssert.Equal(MagickColors.Purple, image, 0, 0);
            }

            [Fact]
            public void ShouldExtentTheImageWithTheSpecifiedGravityAndColor()
            {
                using var image = new MagickImage(MagickColors.Black, 1, 1);
                image.Extent(new MagickGeometry(2, 3), Gravity.Southwest, MagickColors.Purple);

                Assert.Equal(2, image.Width);
                Assert.Equal(3, image.Height);
                ColorAssert.Equal(MagickColors.Purple, image, 0, 0);
            }
        }
    }
}
