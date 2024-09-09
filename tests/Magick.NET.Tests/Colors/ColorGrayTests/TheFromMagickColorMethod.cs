// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using ImageMagick.Colors;
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

public partial class ColorGrayTests
{
    public class TheFromMagickColorMethod
    {
        [Fact]
        public void ShouldReturnNullWhenValueIsNull()
        {
            var result = ColorGray.FromMagickColor(null!);

            Assert.Null(result);
        }

        [Fact]
        public void ShouldInitializeTheProperties()
        {
            var color = new MagickColor(Quantum.Max, (QuantumType)(Quantum.Max * 0.25), (QuantumType)(Quantum.Max * 0.5));
            var grayColor = ColorGray.FromMagickColor(color);

            Assert.NotNull(grayColor);
            Assert.InRange(grayColor.Shade, 0.41, 0.43);
        }
    }
}
