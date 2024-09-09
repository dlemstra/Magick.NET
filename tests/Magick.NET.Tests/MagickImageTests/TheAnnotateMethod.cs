// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheAnnotateMethod
    {
        public class WithGravity
        {
            [Fact]
            public void ShouldThrowExceptionWhenTextIsNull()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("text", () => image.Annotate(null!, Gravity.Center));
            }

            [Fact]
            public void ShouldThrowExceptionWhenTextIsEmpty()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentException>("text", () => image.Annotate(string.Empty, Gravity.Center));
            }

            [Fact]
            public void ShouldUseTheSpecifiedGravity()
            {
                using var image = new MagickImage(MagickColors.Thistle, 200, 50);
                image.Settings.FontPointsize = 20;
                image.Settings.FillColor = MagickColors.Purple;
                image.Settings.StrokeColor = MagickColors.Purple;
                image.Annotate("Magick.NET", Gravity.East);

                ColorAssert.Equal(MagickColors.Purple, image, 196, 17);
                ColorAssert.Equal(MagickColors.Thistle, image, 173, 17);
            }
        }

        public class WithGeometry
        {
            [Fact]
            public void ShouldThrowExceptionWhenTextIsNull()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("text", () => image.Annotate(null!, new MagickGeometry(1, 2, 3, 4)));
            }

            [Fact]
            public void ShouldThrowExceptionWhenTextIsEmpty()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentException>("text", () => image.Annotate(string.Empty, new MagickGeometry(1, 2, 3, 4)));
            }

            [Fact]
            public void ShouldThrowExceptionWhenGeometryIsNull()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("boundingArea", () => image.Annotate("test", null!));
            }
        }

        public class WithGeometryAndGravity
        {
            [Fact]
            public void ShouldThrowExceptionWhenTextIsNull()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("text", () => image.Annotate(null!, new MagickGeometry(1, 2, 3, 4), Gravity.Center));
            }

            [Fact]
            public void ShouldThrowExceptionWhenTextIsEmpty()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentException>("text", () => image.Annotate(string.Empty, new MagickGeometry(1, 2, 3, 4), Gravity.Center));
            }

            [Fact]
            public void ShouldThrowExceptionWhenGeometryIsNull()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("boundingArea", () => image.Annotate("test", null!, Gravity.Center));
            }
        }

        public class WithGeometryAndGravityAndAngle
        {
            [Fact]
            public void ShouldThrowExceptionWhenTextIsNull()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("text", () => image.Annotate(null!, new MagickGeometry(1, 2, 3, 4), Gravity.Center, 2.0));
            }

            [Fact]
            public void ShouldThrowExceptionWhenTextIsEmpty()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentException>("text", () => image.Annotate(string.Empty, new MagickGeometry(1, 2, 3, 4), Gravity.Center, 2.0));
            }

            [Fact]
            public void ShouldThrowExceptionWhenGeometryIsNull()
            {
                using var image = new MagickImage();

                Assert.Throws<ArgumentNullException>("boundingArea", () => image.Annotate("test", null!, Gravity.Center, 2.0));
            }

            [Fact]
            public void ShouldUseTheSpecifiedSettings()
            {
                using var image = new MagickImage(MagickColors.GhostWhite, 200, 200);
                image.Settings.FontPointsize = 30;
                image.Settings.FillColor = MagickColors.Orange;
                image.Settings.StrokeColor = MagickColors.Orange;
                image.Annotate("Magick.NET", new MagickGeometry(75, 125, 0, 0), Gravity.Undefined, 45);

                ColorAssert.Equal(MagickColors.GhostWhite, image, 104, 83);
                ColorAssert.Equal(MagickColors.Orange, image, 117, 70);
            }
        }
    }
}
