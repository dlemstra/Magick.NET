// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickColorTests
    {
        public class TheToStringMethod
        {
            [Fact]
            public void ShouldReturnTheCorrectString()
            {
                var color = new MagickColor(MagickColors.Red);
#if Q8
                Assert.Equal("#FF0000FF", color.ToString());
#else
                Assert.Equal("#FFFF00000000FFFF", color.ToString());
#endif
            }

            [Fact]
            public void ShouldReturnTheCorrectStringForCmykColor()
            {
#if Q8
                var color = new MagickColor(0, Quantum.Max, 0, 0, (System.Byte)(Quantum.Max / 3));
#elif Q16
                var color = new MagickColor(0, Quantum.Max, 0, 0, (System.UInt16)(Quantum.Max / 3));
#else
                var color = new MagickColor(0, Quantum.Max, 0, 0, (System.Single)(Quantum.Max / 3));
#endif
                Assert.Equal("cmyka(0," + Quantum.Max + ",0,0,0.3333)", color.ToString());

                color = new MagickColor(0, Quantum.Max, 0, 0, Quantum.Max);
                Assert.Equal("cmyka(0," + Quantum.Max + ",0,0,1.0)", color.ToString());
            }
        }
    }
}
