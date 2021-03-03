// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public abstract class ColorBaseTests<TColor>
      where TColor : ColorBase
    {
        protected static void AssertIComparable(TColor first)
        {
            Assert.Equal(0, first.CompareTo(first));
            Assert.Equal(1, first.CompareTo(null));
            Assert.False(first < null);
            Assert.False(first <= null);
            Assert.True(first > null);
            Assert.True(first >= null);
            Assert.True(null < first);
            Assert.True(null <= first);
            Assert.False(null > first);
            Assert.False(null >= first);
        }

        protected static void Test_IComparable_Equal(TColor first, TColor second)
        {
            Assert.Equal(0, first.CompareTo(second));
            Assert.False(first < second);
            Assert.True(first <= second);
            Assert.False(first > second);
            Assert.True(first >= second);
        }

        protected static void Test_IComparable_FirstLower(TColor first, TColor second)
        {
            Assert.Equal(-1, first.CompareTo(second));
            Assert.True(first < second);
            Assert.True(first <= second);
            Assert.False(first > second);
            Assert.False(first >= second);
        }

        protected static void Test_IEquatable_NotEqual(TColor first, TColor second)
        {
            Assert.True(first != second);
            Assert.False(first.Equals(second));
        }

        protected static void Test_IEquatable_Equal(TColor first, TColor second)
        {
            Assert.True(first == second);
            Assert.True(first.Equals(second));
        }

        protected static void Test_IEquatable_NullAndSelf(TColor first)
        {
            Assert.False(first == null);
            Assert.False(first.Equals(null));
            Assert.True(first.Equals(first));
            Assert.True(first.Equals((object)first));
        }

        protected static void AssertToString(TColor color, MagickColor expected)
        {
            Assert.Equal(color.ToString(), expected.ToString());
        }
    }
}
