// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests
{
    public partial class NumberTests
    {
        public class TheOperators
        {
            [Fact]
            public void ShouldReturnTheCorrectValueWhenValuesAreEqual()
            {
                var first = new Number(10U);
                var second = new Number(10);

                Assert.True(first == second);
                Assert.False(first != second);
                Assert.False(first < second);
                Assert.True(first <= second);
                Assert.False(first > second);
                Assert.True(first >= second);
            }

            [Fact]
            public void ShouldReturnTheCorrectValueWhenValuesAreNotEqual()
            {
                var first = new Number(24);
                var second = new Number(42);

                Assert.False(first == second);
                Assert.True(first != second);
                Assert.True(first < second);
                Assert.True(first <= second);
                Assert.False(first > second);
                Assert.False(first >= second);
            }

            [Fact]
            public void ShouldBeAbleToImplicitCastFromInt()
            {
                Number number = 10;

                Assert.Equal(10U, (uint)number);
            }

            [Fact]
            public void ShouldBeAbleToImplicitCastFromUInt()
            {
                Number number = 10U;

                Assert.Equal(10, (ushort)number);
            }

            [Fact]
            public void ShouldBeAbleToImplicitCastFromShort()
            {
                Number number = (short)10;

                Assert.Equal(10U, (uint)number);
            }

            [Fact]
            public void ShouldBeAbleToImplicitCastFromUShort()
            {
                Number number = (ushort)10U;

                Assert.Equal(10, (ushort)number);
            }
        }
    }
}
