// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickGeometryTests
{
    public class TheOperators
    {
        [Fact]
        public void ShouldReturnTheCorrectValueWhenInstanceIsNull()
        {
            var geometry = new MagickGeometry(10, 5);

            Assert.False(geometry == null!);
            Assert.True(geometry != null!);
            Assert.False(geometry < null!);
            Assert.False(geometry <= null!);
            Assert.True(geometry > null!);
            Assert.True(geometry >= null!);
            Assert.False(null! == geometry);
            Assert.True(null! != geometry);
            Assert.True(null! < geometry);
            Assert.True(null! <= geometry);
            Assert.False(null! > geometry);
            Assert.False(null! >= geometry);
        }

        [Fact]
        public void ShouldReturnTheCorrectValueWhenInstanceIsSpecified()
        {
            var first = new MagickGeometry(10, 5);
            var second = new MagickGeometry(5, 5);

            Assert.False(first == second);
            Assert.True(first != second);
            Assert.False(first < second);
            Assert.False(first <= second);
            Assert.True(first > second);
            Assert.True(first >= second);
        }

        [Fact]
        public void ShouldReturnTheCorrectValueWhenInstanceHasSameSize()
        {
            var first = new MagickGeometry(10, 5);
            var second = new MagickGeometry(5, 10);

            Assert.False(first == second);
            Assert.True(first != second);
            Assert.False(first < second);
            Assert.True(first <= second);
            Assert.False(first > second);
            Assert.True(first >= second);
        }

        [Fact]
        public void ShouldReturnTheCorrectValueWhenInstanceAreEqual()
        {
            var first = new MagickGeometry(10, 5);
            var second = new MagickGeometry(10, 5);

            Assert.True(first == second);
            Assert.False(first != second);
            Assert.False(first < second);
            Assert.True(first <= second);
            Assert.False(first > second);
            Assert.True(first >= second);
        }
    }
}
