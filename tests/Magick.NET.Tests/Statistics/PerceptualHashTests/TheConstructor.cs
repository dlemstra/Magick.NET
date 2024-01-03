// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class PerceptualHashTests
{
    public class TheConstructor
    {
        [Fact]
        public void ShouldThrowExceptionWhenHashIsNull()
        {
            Assert.Throws<ArgumentNullException>("hash", () => new PerceptualHash(null));
        }

        [Fact]
        public void ShouldThrowExceptionWhenHashIsEmpty()
        {
            Assert.Throws<ArgumentException>("hash", () => new PerceptualHash(string.Empty));
        }

        [Fact]
        public void ShouldThrowExceptionWhenValueIsTooSmall()
        {
            Assert.Throws<ArgumentException>("hash", () => new PerceptualHash("a0df"));
        }

        [Fact]
        public void ShouldThrowExceptionWhenValueIsInvalid()
        {
            Assert.Throws<ArgumentException>("hash", () => new PerceptualHash("H00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000"));
        }

        [Fact]
        public void ShouldConvertStringHashToPerceptualHash()
        {
            var hash = new PerceptualHash("81b4488652898d48a7a9622346206e620f8a646682939835e986ec98c78f887ae8c67f81b1e884c58a0d18af2d622718fd35623ffdeac9a78cbaedaa81d888434e824c683ad781c37895978c8688c426628ed61b216279b81b48887318a1628af43622a2619d162372");

            var red = hash.GetChannel(PixelChannel.Red);
            Assert.Equal(0.698, red.HuPhash(ColorSpace.sRGB, 0), 3);
            Assert.Equal(3.439, red.HuPhash(ColorSpace.sRGB, 1), 3);
            Assert.Equal(3.912, red.HuPhash(ColorSpace.sRGB, 2), 3);
            Assert.Equal(4.292, red.HuPhash(ColorSpace.sRGB, 3), 3);
            Assert.Equal(8.756, red.HuPhash(ColorSpace.sRGB, 4), 3);
            Assert.Equal(8.302, red.HuPhash(ColorSpace.sRGB, 5), 3);
            Assert.Equal(8.440, red.HuPhash(ColorSpace.sRGB, 6), 3);

            var green = hash.GetChannel(PixelChannel.Green);
            Assert.Equal(0.694, green.HuPhash(ColorSpace.sRGB, 0), 3);
            Assert.Equal(3.399, green.HuPhash(ColorSpace.sRGB, 1), 3);
            Assert.Equal(4.117, green.HuPhash(ColorSpace.sRGB, 2), 3);
            Assert.Equal(4.484, green.HuPhash(ColorSpace.sRGB, 3), 3);
            Assert.Equal(8.817, green.HuPhash(ColorSpace.sRGB, 4), 3);
            Assert.Equal(6.482, green.HuPhash(ColorSpace.sRGB, 5), 3);
            Assert.Equal(9.215, green.HuPhash(ColorSpace.sRGB, 6), 3);

            var blue = hash.GetChannel(PixelChannel.Blue);
            Assert.Equal(0.722, blue.HuPhash(ColorSpace.sRGB, 0), 3);
            Assert.Equal(3.830, blue.HuPhash(ColorSpace.sRGB, 1), 3);
            Assert.Equal(5.130, blue.HuPhash(ColorSpace.sRGB, 2), 3);
            Assert.Equal(5.021, blue.HuPhash(ColorSpace.sRGB, 3), 3);
            Assert.Equal(10.477, blue.HuPhash(ColorSpace.sRGB, 4), 3);
            Assert.Equal(6.945, blue.HuPhash(ColorSpace.sRGB, 5), 3);
            Assert.Equal(10.139, blue.HuPhash(ColorSpace.sRGB, 6), 3);
        }
    }
}
