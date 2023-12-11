// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickGeometryTests
{
    public class TheFromPageSizeMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenPageSizeIsNull()
        {
            Assert.Throws<ArgumentNullException>("pageSize", () => MagickGeometry.FromPageSize(null));
        }

        [Fact]
        public void ShouldThrowExceptionWhenPageSizeIsEmpty()
        {
            Assert.Throws<ArgumentException>("pageSize", () => MagickGeometry.FromPageSize(string.Empty));
        }

        [Fact]
        public void ShouldThrowExceptionWhenPageSizeIsInvalid()
        {
            var exception = Assert.Throws<InvalidOperationException>(() => MagickGeometry.FromPageSize("invalid"));

            Assert.Equal("Invalid page size specified.", exception.Message);
        }

        [Theory]
        [InlineData("4x6", 288, 432)]
        [InlineData("5x7", 360, 504)]
        [InlineData("7x9", 504, 648)]
        [InlineData("8x10", 576, 720)]
        [InlineData("9x11", 648, 792)]
        [InlineData("9x12", 648, 864)]
        [InlineData("10x13", 720, 936)]
        [InlineData("10x14", 720, 1008)]
        [InlineData("11x17", 792, 1224)]
        [InlineData("4A0", 4768, 6741)]
        [InlineData("2A0", 3370, 4768)]
        [InlineData("a0", 2384, 3370)]
        [InlineData("a10", 74, 105)]
        [InlineData("a1", 1684, 2384)]
        [InlineData("a2", 1191, 1684)]
        [InlineData("a3", 842, 1191)]
        [InlineData("a4small", 595, 842)]
        [InlineData("a4", 595, 842)]
        [InlineData("a5", 420, 595)]
        [InlineData("a6", 298, 420)]
        [InlineData("a7", 210, 298)]
        [InlineData("a8", 147, 210)]
        [InlineData("a9", 105, 147)]
        [InlineData("archa", 648, 864)]
        [InlineData("archb", 864, 1296)]
        [InlineData("archC", 1296, 1728)]
        [InlineData("archd", 1728, 2592)]
        [InlineData("arche", 2592, 3456)]
        [InlineData("b10", 91, 127)]
        [InlineData("b0", 2920, 4127)]
        [InlineData("b1", 2064, 2920)]
        [InlineData("b2", 1460, 2064)]
        [InlineData("b3", 1032, 1460)]
        [InlineData("b4", 729, 1032)]
        [InlineData("b5", 516, 729)]
        [InlineData("b6", 363, 516)]
        [InlineData("b7", 258, 363)]
        [InlineData("b8", 181, 258)]
        [InlineData("b9", 127, 181)]
        [InlineData("c0", 2599, 3676)]
        [InlineData("c1", 1837, 2599)]
        [InlineData("c2", 1298, 1837)]
        [InlineData("c3", 918, 1296)]
        [InlineData("c4", 649, 918)]
        [InlineData("c5", 459, 649)]
        [InlineData("c6", 323, 459)]
        [InlineData("c7", 230, 323)]
        [InlineData("csheet", 1224, 1584)]
        [InlineData("dsheet", 1584, 2448)]
        [InlineData("esheet", 2448, 3168)]
        [InlineData("executive", 540, 720)]
        [InlineData("flsa", 612, 936)]
        [InlineData("flse", 612, 936)]
        [InlineData("folio", 612, 936)]
        [InlineData("halfletter", 396, 612)]
        [InlineData("isob0", 2835, 4008)]
        [InlineData("isob10", 88, 125)]
        [InlineData("isob1", 2004, 2835)]
        [InlineData("isob2", 1417, 2004)]
        [InlineData("isob3", 1001, 1417)]
        [InlineData("isob4", 709, 1001)]
        [InlineData("isob5", 499, 709)]
        [InlineData("isob6", 354, 499)]
        [InlineData("isob7", 249, 354)]
        [InlineData("isob8", 176, 249)]
        [InlineData("isob9", 125, 176)]
        [InlineData("jisb0", 1030, 1456)]
        [InlineData("jisb1", 728, 1030)]
        [InlineData("jisb2", 515, 728)]
        [InlineData("jisb3", 364, 515)]
        [InlineData("jisb4", 257, 364)]
        [InlineData("jisb5", 182, 257)]
        [InlineData("jisb6", 128, 182)]
        [InlineData("ledger", 1224, 792)]
        [InlineData("legal", 612, 1008)]
        [InlineData("lettersmall", 612, 792)]
        [InlineData("letter", 612, 792)]
        [InlineData("monarch", 279, 540)]
        [InlineData("quarto", 610, 780)]
        [InlineData("statement", 396, 612)]
        [InlineData("tabloid", 792, 1224)]
        public void ShouldReturnTheCorrectValues(string pageSize, int expectedWidth, int expectedHeight)
        {
            var geometry = MagickGeometry.FromPageSize(pageSize);

            Assert.Equal(expectedWidth, geometry.Width);
            Assert.Equal(expectedHeight, geometry.Height);
            Assert.Equal(0, geometry.X);
            Assert.Equal(0, geometry.Y);
        }

        [Fact]
        public void ShouldSetTheXAndYPosition()
        {
            var geometry = MagickGeometry.FromPageSize("a4+3+2");

            Assert.Equal(595, geometry.Width);
            Assert.Equal(842, geometry.Height);
            Assert.Equal(3, geometry.X);
            Assert.Equal(2, geometry.Y);
        }
    }
}
