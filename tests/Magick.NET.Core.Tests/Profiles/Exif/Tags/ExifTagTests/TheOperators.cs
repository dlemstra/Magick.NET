// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests;

public partial class ExifTagTests
{
    public class TheOperators
    {
        [Fact]
        public void ShouldReturnTheCorrectValueWhenInstanceIsNull()
        {
            var tag = new ExifTag<uint>(ExifTagValue.SubIFDOffset);

            Assert.False(tag == null!);
            Assert.True(tag != null!);
            Assert.False(null! == tag);
            Assert.True(null! != tag);
        }

        [Fact]
        public void ShouldReturnTheCorrectValueWhenValuesAreEqual()
        {
            var first = new ExifTag<uint>(ExifTagValue.SubIFDOffset);
            var second = new ExifTag<uint>(ExifTagValue.SubIFDOffset);

            Assert.True(first == second);
            Assert.False(first != second);
        }

        [Fact]
        public void ShouldReturnTheCorrectValueWhenValuesAreNotEqual()
        {
            var first = new ExifTag<uint>(ExifTagValue.SubIFDOffset);
            var second = new ExifTag<uint>(ExifTagValue.CodingMethods);

            Assert.False(first == second);
            Assert.True(first != second);
        }
    }
}
