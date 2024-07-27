// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using ImageMagick.Drawing;
using Xunit;

namespace Magick.NET.Tests;

public partial class DrawablesTests
{
    public class TheCompositeMethod
    {
        [Obsolete]
        public class WithOffsetAndImage
        {
            [Fact]
            public void ShouldThrowExceptionWhenImageIsNull()
            {
                Assert.Throws<ArgumentNullException>("image", () =>
                {
                    new Drawables().Composite(0, 0, CompositeOperator.In, null);
                });
            }

            [Fact]
            public void ShouldCopyPixelsOfTheImage()
            {
                using var image = new MagickImage(MagickColors.Green, 3, 1);
                using var inner = new MagickImage(MagickColors.Purple, 2, 2);
                new Drawables()
                    .Composite(1, 0, 1, 1, CompositeOperator.In, inner)
                    .Draw(image);

                ColorAssert.Equal(MagickColors.Green, image, 0, 0);
                ColorAssert.Equal(MagickColors.Purple, image, 1, 0);
                ColorAssert.Equal(MagickColors.Green, image, 2, 0);
            }
        }

        [Obsolete]
        public class WithGeometryOffsetAndCompositeOperatorAndImage
        {
            [Fact]
            public void ShouldThrowExceptionWhenImageIsNull()
            {
                Assert.Throws<ArgumentNullException>("image", () =>
                {
                    new Drawables().Composite(0, 0, CompositeOperator.Over, null);
                });
            }

            [Fact]
            public void ShouldUseTheCompositeOperator()
            {
                using var image = new MagickImage(MagickColors.Green, 3, 1);
                using var inner = new MagickImage(MagickColors.Purple, 2, 2);
                new Drawables()
                    .Composite(1, 0, 1, 1, CompositeOperator.Plus, inner)
                    .Draw(image);

                ColorAssert.Equal(MagickColors.Green, image, 0, 0);
                ColorAssert.Equal(MagickColors.Gray, image, 1, 0);
                ColorAssert.Equal(MagickColors.Green, image, 2, 0);
            }
        }

        public class WithOffsetAndCompositeOperatorAndImage
        {
            [Fact]
            public void ShouldThrowExceptionWhenImageIsNull()
            {
                Assert.Throws<ArgumentNullException>("image", () =>
                {
                    new Drawables().Composite(1, 0, 1, 1, CompositeOperator.Over, null);
                });
            }

            [Fact]
            public void ShouldUseTheCompositeOperator()
            {
                using var image = new MagickImage(MagickColors.Green, 3, 1);
                using var inner = new MagickImage(MagickColors.Purple, 2, 2);
                new Drawables()
                    .Composite(1, 0, 1, 1, CompositeOperator.Plus, inner)
                    .Draw(image);

                ColorAssert.Equal(MagickColors.Green, image, 0, 0);
                ColorAssert.Equal(MagickColors.Gray, image, 1, 0);
                ColorAssert.Equal(MagickColors.Green, image, 2, 0);
            }
        }

        [Obsolete]
        public class WithXYAndImage
        {
            [Fact]
            public void ShouldThrowExceptionWhenImageIsNull()
            {
                Assert.Throws<ArgumentNullException>("image", () =>
                {
                    new Drawables().Composite(0, 0, CompositeOperator.In, null);
                });
            }

            [Fact]
            public void ShouldCopyPixelsOfTheImage()
            {
                using var image = new MagickImage(MagickColors.Green, 3, 1);
                using var inner = new MagickImage(MagickColors.Purple, 2, 2);
                new Drawables()
                    .Composite(1, 0, CompositeOperator.In, inner)
                    .Draw(image);

                ColorAssert.Equal(MagickColors.Green, image, 0, 0);
                ColorAssert.Equal(MagickColors.Purple, image, 1, 0);
                ColorAssert.Equal(MagickColors.Purple, image, 2, 0);
            }
        }

        public class WithXYAndCompositeOperatorAndImage
        {
            [Fact]
            public void ShouldThrowExceptionWhenImageIsNull()
            {
                Assert.Throws<ArgumentNullException>("image", () =>
                {
                    new Drawables().Composite(0, 0, CompositeOperator.Over, null);
                });
            }

            [Fact]
            public void ShouldUseTheCompositeOperator()
            {
                using var image = new MagickImage(MagickColors.Green, 3, 1);
                using var inner = new MagickImage(MagickColors.Purple, 2, 2);
                new Drawables()
                    .Composite(1, 0, CompositeOperator.Plus, inner)
                    .Draw(image);

                ColorAssert.Equal(MagickColors.Green, image, 0, 0);
                ColorAssert.Equal(MagickColors.Gray, image, 1, 0);
                ColorAssert.Equal(MagickColors.Gray, image, 2, 0);
            }
        }
    }
}
