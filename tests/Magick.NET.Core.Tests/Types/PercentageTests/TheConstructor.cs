// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests;

public partial class PercentageTests
{
    public class TheConstructor
    {
        [Fact]
        public void ShouldThrowExceptionWhenValueIsNegative()
        {
            Assert.Throws<ArgumentException>("value", () => new Percentage(-1.0));
            Assert.Throws<ArgumentException>("value", () => (Percentage)(-1.0));
            Assert.Throws<ArgumentException>("value", () => new Percentage(-1));
            Assert.Throws<ArgumentException>("value", () => (Percentage)(-1));
        }

        [Fact]
        public void ShouldDefaultToZero()
        {
            Percentage percentage = default;
            Assert.Equal("0%", percentage.ToString());
        }

        [Fact]
        public void ShouldSetValue()
        {
            var percentage = new Percentage(50);
            Assert.Equal("50%", percentage.ToString());
        }

        [Fact]
        public void ShouldHandleValueAbove100()
        {
            var percentage = new Percentage(200.0);
            Assert.Equal("200%", percentage.ToString());
        }
    }
}
