// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests;

public partial class PointDTests
{
    public class TheConstructor
    {
        [Fact]
        public void ShouldThrowExceptionWhenValueIsNull()
        {
            Assert.Throws<ArgumentNullException>("value", () => { new PointD(null!); });
        }

        [Fact]
        public void ShouldThrowExceptionWhenValueIsEmpty()
        {
            Assert.Throws<ArgumentException>("value", () => { new PointD(string.Empty); });
        }

        [Fact]
        public void ShouldThrowExceptionWhenValueIsInvalid()
        {
            Assert.Throws<ArgumentException>("value", () => { new PointD("1.0x"); });

            Assert.Throws<ArgumentException>("value", () => { new PointD("x1.0"); });

            Assert.Throws<ArgumentException>("value", () => { new PointD("ax1.0"); });

            Assert.Throws<ArgumentException>("value", () => { new PointD("1.0xb"); });

            Assert.Throws<ArgumentException>("value", () => { new PointD("1.0x6 magick"); });
        }

        [Fact]
        public void ShouldSetTheXAndYToZeroByDefault()
        {
            PointD point = default;
            Assert.Equal(0.0, point.X);
            Assert.Equal(0.0, point.Y);
        }

        [Fact]
        public void ShouldSetTheXAndYValue()
        {
            var point = new PointD(5, 10);
            Assert.Equal(5.0, point.X);
            Assert.Equal(10.0, point.Y);
        }

        [Fact]
        public void ShouldUseTheXValueWhenTValueIsNotSet()
        {
            var point = new PointD(5);
            Assert.Equal(5.0, point.X);
            Assert.Equal(5.0, point.Y);
        }

        [Fact]
        public void ShouldSetTheValuesFromString()
        {
            var point = new PointD("1.0x2.5");
            Assert.Equal(1.0, point.X);
            Assert.Equal(2.5, point.Y);
            Assert.Equal("1x2.5", point.ToString());
        }
    }
}
