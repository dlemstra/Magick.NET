// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public class ColorMonoTests : ColorBaseTests<ColorMono>
    {
        [Fact]
        public void Test_GetHashCode()
        {
            ColorMono first = new ColorMono(true);
            int hashCode = first.GetHashCode();

            first.IsBlack = false;
            Assert.NotEqual(hashCode, first.GetHashCode());
        }

        [Fact]
        public void Test_IComparable()
        {
            ColorMono first = new ColorMono(true);

            AssertIComparable(first);

            ColorMono second = new ColorMono(false);

            Test_IComparable_FirstLower(first, second);

            second = new ColorMono(true);

            Test_IComparable_Equal(first, second);
        }

        [Fact]
        public void Test_IEquatable()
        {
            ColorMono first = new ColorMono(true);

            Test_IEquatable_NullAndSelf(first);

            ColorMono second = new ColorMono(true);

            Test_IEquatable_Equal(first, second);

            second = new ColorMono(false);

            Test_IEquatable_NotEqual(first, second);
        }

        [Fact]
        public void Test_ImplicitOperator()
        {
            ColorMono expected = new ColorMono(true);
            ColorMono actual = MagickColors.Black;
            Assert.Equal(expected, actual);

            var magickColor = actual.ToMagickColor();
            Assert.Equal(magickColor, MagickColors.Black);

            Assert.Null(ColorMono.FromMagickColor(null));
        }

        [Fact]
        public void Test_ToString()
        {
            ColorMono color = new ColorMono(true);
            AssertToString(color, MagickColors.Black);
        }

        [Fact]
        public void Test_Properties()
        {
            ColorMono color = new ColorMono(true);

            color.IsBlack = false;
            Assert.False(color.IsBlack);
        }

        [Fact]
        public void Test_ColorMono()
        {
            var mono = new ColorMono(false);

            var white = new MagickColor("#fff");
            Assert.Equal(white, mono.ToMagickColor());
            ColorAssert.Equal(MagickColors.White, mono.ToMagickColor());

            mono = new ColorMono(true);

            var black = new MagickColor("#000");
            Assert.Equal(black, mono.ToMagickColor());
            ColorAssert.Equal(MagickColors.Black, mono.ToMagickColor());

            mono = ColorMono.FromMagickColor(MagickColors.Black);
            Assert.True(mono.IsBlack);

            mono = ColorMono.FromMagickColor(MagickColors.White);
            Assert.False(mono.IsBlack);

            var exception = Assert.Throws<ArgumentException>("color", () =>
            {
                ColorMono.FromMagickColor(MagickColors.Gray);
            });

            Assert.Contains("Invalid", exception.Message);
        }
    }
}
