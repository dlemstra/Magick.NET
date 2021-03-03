// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests
{
    public partial class ExifTagTests
    {
        public class TheEqualsMethod
        {
            [Fact]
            public void ShouldReturnFalseWhenInstanceIsNull()
            {
                var tag = new ExifTag<uint>(ExifTagValue.SubIFDOffset);

                Assert.False(tag.Equals(null));
            }

            [Fact]
            public void ShouldReturnTrueWhenInstanceIsTheSame()
            {
                var tag = new ExifTag<uint>(ExifTagValue.SubIFDOffset);

                Assert.True(tag.Equals(tag));
            }

            [Fact]
            public void ShouldReturnTrueWhenObjectIsTheSame()
            {
                var tag = new ExifTag<uint>(ExifTagValue.SubIFDOffset);

                Assert.True(tag.Equals((object)tag));
            }

            [Fact]
            public void ShouldReturnTrueWhenInstanceIsEqual()
            {
                var first = new ExifTag<uint>(ExifTagValue.SubIFDOffset);
                var second = new ExifTag<uint>(ExifTagValue.SubIFDOffset);

                Assert.True(first.Equals(second));
            }

            [Fact]
            public void ShouldReturnTrueWhenObjectIsEqual()
            {
                var first = new ExifTag<uint>(ExifTagValue.SubIFDOffset);
                var second = new ExifTag<uint>(ExifTagValue.SubIFDOffset);

                Assert.True(first.Equals((object)second));
            }

            [Fact]
            public void ShouldReturnFalseWhenInstanceIsNotEqual()
            {
                var first = new ExifTag<uint>(ExifTagValue.SubIFDOffset);
                var second = new ExifTag<uint>(ExifTagValue.CodingMethods);

                Assert.False(first.Equals(second));
            }

            [Fact]
            public void ShouldReturnFalseWhenObjectIsNotEqual()
            {
                var first = new ExifTag<uint>(ExifTagValue.SubIFDOffset);
                var second = new ExifTag<uint>(ExifTagValue.CodingMethods);

                Assert.False(first.Equals((object)second));
            }
        }
    }
}
