// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests
{
    public partial class NumberTests
    {
        public class TheCompareToMethod
        {
            [Fact]
            public void ShouldReturnZeroWhenInstancesAreTheSame()
            {
                var first = new Number(10);

                Assert.Equal(0, first.CompareTo(first));
            }

            [Fact]
            public void ShouldReturnZeroWhenInstancesAreEqual()
            {
                var first = new Number(10);
                var second = new Number(10);

                Assert.Equal(0, first.CompareTo(second));
            }

            [Fact]
            public void ShouldReturnOneWhenInstancesAreNotEqual()
            {
                var first = new Number(10);
                var second = new Number(5);

                Assert.Equal(1, first.CompareTo(second));
            }
        }
    }
}
