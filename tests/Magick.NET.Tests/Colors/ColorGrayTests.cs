// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public class ColorGrayTests : ColorBaseTests<ColorGray>
    {
        [Fact]
        public void Test_GetHashCode()
        {
            ColorGray first = new ColorGray(0);
            int hashCode = first.GetHashCode();

            first.Shade = 1;
            Assert.NotEqual(hashCode, first.GetHashCode());
        }

        [Fact]
        public void Test_IComparable()
        {
            ColorGray first = new ColorGray(0.5);

            AssertIComparable(first);

            ColorGray second = new ColorGray(0.6);

            Test_IComparable_FirstLower(first, second);

            second = new ColorGray(0.5);

            Test_IComparable_Equal(first, second);
        }

        [Fact]
        public void Test_IEquatable()
        {
            ColorGray first = new ColorGray(0.2);

            Test_IEquatable_NullAndSelf(first);

            ColorGray second = new ColorGray(0.2);

            Test_IEquatable_Equal(first, second);

            second = new ColorGray(0.4);

            Test_IEquatable_NotEqual(first, second);
        }

        [Fact]
        public void Test_ImplicitOperator()
        {
            ColorGray expected = new ColorGray(1.0);
            ColorGray actual = MagickColors.White;
            Assert.Equal(expected, actual);

            var magickColor = actual.ToMagickColor();
            Assert.Equal(magickColor, MagickColors.White);

            Assert.Null(ColorGray.FromMagickColor(null));
        }

        [Fact]
        public void Test_ToString()
        {
            ColorGray color = new ColorGray(1.0);
            AssertToString(color, MagickColors.White);
        }

        [Fact]
        public void Test_Properties()
        {
            ColorGray color = new ColorGray(0);

            color.Shade = 1;
            Assert.Equal(1, color.Shade);

            color.Shade = -0.99;
            Assert.Equal(1, color.Shade);

            color.Shade = 1.01;
            Assert.Equal(1, color.Shade);
        }

        [Fact]
        public void Test_ColorGray()
        {
            ColorGray gray = new ColorGray(1);

            var white = new MagickColor("#fff");
            Assert.Equal(white, gray.ToMagickColor());
            ColorAssert.Equal(MagickColors.White, gray.ToMagickColor());

            gray = new ColorGray(0);

            var black = new MagickColor("#000");
            Assert.Equal(black, gray.ToMagickColor());
            ColorAssert.Equal(MagickColors.Black, gray.ToMagickColor());
        }

        [Fact]
        public void Test_Constructor()
        {
            Assert.Throws<ArgumentException>("shade", () =>
            {
                new ColorGray(1.01);
            });

            Assert.Throws<ArgumentException>("shade", () =>
            {
                new ColorGray(-0.01);
            });
        }
    }
}
