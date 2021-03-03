// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public class ColorYUVTests : ColorBaseTests<ColorYUV>
    {
        [Fact]
        public void Test_GetHashCode()
        {
            ColorYUV first = new ColorYUV(0.0, 0.0, 0.0);
            int hashCode = first.GetHashCode();

            first.Y = 1.0;
            Assert.NotEqual(hashCode, first.GetHashCode());
        }

        [Fact]
        public void Test_IComparable()
        {
            ColorYUV first = new ColorYUV(0.2, 0.3, 0.4);

            AssertIComparable(first);

            ColorYUV second = new ColorYUV(0.2, 0.4, 0.5);

            Test_IComparable_FirstLower(first, second);

            second = new ColorYUV(0.2, 0.3, 0.4);

            Test_IComparable_Equal(first, second);
        }

        [Fact]
        public void Test_IEquatable()
        {
            ColorYUV first = new ColorYUV(0.1, -0.2, -0.3);

            Test_IEquatable_NullAndSelf(first);

            ColorYUV second = new ColorYUV(0.1, -0.2, -0.3);

            Test_IEquatable_Equal(first, second);

            second = new ColorYUV(0.1, -0.2, -0.4);

            Test_IEquatable_NotEqual(first, second);
        }

        [Fact]
        public void Test_ImplicitOperator()
        {
            ColorYUV expected = new ColorYUV(0.413189, 0.789, 1.015);
            ColorYUV actual = MagickColors.Fuchsia;
            Assert.Equal(expected, actual);

            Assert.Null(ColorYUV.FromMagickColor(null));
        }

        [Fact]
        public void Test_ToString()
        {
            ColorYUV color = new ColorYUV(0.413189, 0.789, 1.0156);
            AssertToString(color, MagickColors.Fuchsia);
        }

        [Fact]
        public void Test_Properties()
        {
            ColorYUV color = new ColorYUV(0, 0, 0);

            color.Y = 1;
            Assert.Equal(1, color.Y);
            Assert.Equal(0, color.U);
            Assert.Equal(0, color.V);

            color.U = 2;
            Assert.Equal(1, color.Y);
            Assert.Equal(2, color.U);
            Assert.Equal(0, color.V);

            color.V = 3;
            Assert.Equal(1, color.Y);
            Assert.Equal(2, color.U);
            Assert.Equal(3, color.V);
        }
    }
}
