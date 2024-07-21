// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
using ImageMagick.Factories;
using Xunit;

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace Magick.NET.Tests;

public partial class MagickColorFactoryTests
{
    public class TheCreateMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenColorIsNull()
        {
            var factory = new MagickColorFactory();

            Assert.Throws<ArgumentNullException>("color", () =>
            {
                factory.Create((string)null);
            });
        }

        [Fact]
        public void ShouldThrowExceptionWhenColorIsEmpty()
        {
            var factory = new MagickColorFactory();

            Assert.Throws<ArgumentException>("color", () =>
            {
                factory.Create(string.Empty);
            });
        }

        [Fact]
        public void ShouldThrowExceptionWhenColorDoesNotStartWithHash()
        {
            var factory = new MagickColorFactory();

            Assert.Throws<ArgumentException>("color", () =>
            {
                factory.Create("FFFFFF");
            });
        }

        [Fact]
        public void ShouldThrowExceptionWhenColorHasInvalidLength()
        {
            var factory = new MagickColorFactory();

            Assert.Throws<ArgumentException>("color", () =>
            {
                factory.Create("#FFFFF");
            });
        }

        [Fact]
        public void ShouldThrowExceptionWhenColorHasInvalidHexValue()
        {
            var factory = new MagickColorFactory();

            Assert.Throws<ArgumentException>("color", () =>
            {
                factory.Create("#FGF");
            });

            Assert.Throws<ArgumentException>("color", () =>
            {
                factory.Create("#GGFFFF");
            });

            Assert.Throws<ArgumentException>("color", () =>
            {
                factory.Create("#FFFG000000000000");
            });
        }

        [Fact]
        public void ShouldInitializeTheInstanceCorrectly()
        {
            TestColor("#FF", Quantum.Max, Quantum.Max, Quantum.Max, false);
            TestColor("#F00", Quantum.Max, 0, 0, false);
            TestColor("#0F00", 0, Quantum.Max, 0, true);
            TestColor("#0000FF", 0, 0, Quantum.Max, false);
            TestColor("#FF00FF00", Quantum.Max, 0, Quantum.Max, true);

            TestColor("#0000FFFF0000", 0, Quantum.Max, 0, false);
            TestColor("#000080000000", 0, (QuantumType)((Quantum.Max / 2.0) + 0.5), 0, false);
            TestColor("#FFFf000000000000", Quantum.Max, 0, 0, true);

            var half = Quantum.Max * 0.5f;
            TestColor("gray(50%) ", half, half, half, false, 1);
            TestColor("rgba(100%, 0%, 0%, 0.0)", Quantum.Max, 0, 0, true);
        }

        private void TestColor(string hexValue, double red, double green, double blue, bool isTransparent)
        {
            TestColor(hexValue, red, green, blue, isTransparent, 0.01);
        }

        private void TestColor(string hexValue, double red, double green, double blue, bool isTransparent, double delta)
        {
            var factory = new MagickColorFactory();
            var color = factory.Create(hexValue);

            Assert.InRange(color.R, red - delta, red + delta);
            Assert.InRange(color.G, green - delta, green + delta);
            Assert.InRange(color.B, blue - delta, blue + delta);

            if (isTransparent)
                ColorAssert.Transparent(color.A);
            else
                ColorAssert.NotTransparent(color.A);
        }
    }
}
