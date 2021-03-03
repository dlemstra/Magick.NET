// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests
{
    public partial class ExifValueTests
    {
        public class TheOperators
        {
            [Fact]
            public void ShouldReturnTheCorrectValueWhenInstanceIsNull()
            {
                var value = new ExifLong(ExifTag.SubIFDOffset);

                Assert.False(value == null);
                Assert.True(value != null);
                Assert.False(null == value);
                Assert.True(null != value);
            }

            [Fact]
            public void ShouldReturnTheCorrectValueWhenValuesAreEqual()
            {
                var first = new ExifLong(ExifTag.SubIFDOffset);
                var second = ExifTag.SubIFDOffset;

                Assert.True(first == second);
                Assert.False(first != second);
            }

            [Fact]
            public void ShouldReturnTheCorrectValueWhenValuesAreNotEqual()
            {
                var first = new ExifLong(ExifTag.SubIFDOffset);
                var second = ExifTag.CodingMethods;

                Assert.False(first == second);
                Assert.True(first != second);
            }
        }
    }
}
