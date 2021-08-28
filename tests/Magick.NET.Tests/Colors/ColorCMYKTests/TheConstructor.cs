// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using ImageMagick;
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

namespace Magick.NET.Tests
{
    public partial class ColorCMYKTests : ColorBaseTests<ColorCMYK>
    {
        public class TheConstructor
        {
            [Fact]
            public void ShouldThrowExceptionWhenColorIsNull()
            {
                Assert.Throws<ArgumentNullException>("color", () =>
                {
                    new ColorCMYK(null);
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenColorIsEmpty()
            {
                Assert.Throws<ArgumentException>("color", () =>
                {
                    new ColorCMYK(string.Empty);
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenColorDoesNotStartWithHash()
            {
                Assert.Throws<ArgumentException>("color", () =>
                {
                    new ColorCMYK("FFFFFF");
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenColorHasInvalidLength()
            {
                Assert.Throws<ArgumentException>("color", () =>
                {
                    new ColorCMYK("#FFFFF");
                });
            }

            [Fact]
            public void ShouldThrowExceptionWhenColorHasInvalidHexValue()
            {
                Assert.Throws<ArgumentException>("color", () =>
                {
                    new ColorCMYK("#FGF");
                });

                Assert.Throws<ArgumentException>("color", () =>
                {
                    new ColorCMYK("#GGFFFF");
                });

                Assert.Throws<ArgumentException>("color", () =>
                {
                    new ColorCMYK("#FFFG000000000000");
                });
            }
        }
    }
}
