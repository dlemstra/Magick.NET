// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests
{
    public partial class ExifValueTests
    {
        public class TheEqualsMethod
        {
            [Fact]
            public void ShouldReturnFalseWhenInstanceIsNull()
            {
                var value = new ExifLong(ExifTag.SubIFDOffset);

                Assert.False(value.Equals(null));
            }

            [Fact]
            public void ShouldReturnTrueWhenInstanceIsTheSame()
            {
                var value = new ExifLong(ExifTag.SubIFDOffset);

                Assert.True(value.Equals(value));
            }

            [Fact]
            public void ShouldReturnTrueWhenObjectIsTheSame()
            {
                var value = new ExifLong(ExifTag.SubIFDOffset);

                Assert.True(value.Equals(value));
            }

            [Fact]
            public void ShouldReturnTrueWhenInstanceIsEqual()
            {
                var first = new ExifLong(ExifTag.SubIFDOffset);
                var second = ExifTag.SubIFDOffset;

                Assert.True(first.Equals(second));
            }

            [Fact]
            public void ShouldReturnTrueWhenObjectIsEqual()
            {
                var first = new ExifLong(ExifTag.SubIFDOffset);
                var second = new ExifLong(ExifTag.SubIFDOffset);

                Assert.True(first.Equals(second));
            }

            [Fact]
            public void ShouldReturnFalseWhenInstanceIsNotEqual()
            {
                var first = new ExifLong(ExifTag.SubIFDOffset);
                var second = ExifTag.CodingMethods;

                Assert.False(first.Equals(second));
            }

            [Fact]
            public void ShouldReturnFalseWhenObjectIsNotEqual()
            {
                var first = new ExifLong(ExifTag.SubIFDOffset);
                var second = new ExifLong(ExifTag.CodingMethods);

                Assert.False(first.Equals(second));
            }
        }
    }
}
