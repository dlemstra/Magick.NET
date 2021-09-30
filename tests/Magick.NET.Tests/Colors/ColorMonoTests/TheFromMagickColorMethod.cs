// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

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
    public partial class ColorMonoTests
    {
        public class TheFromMagickColorMethod
        {
            [Fact]
            public void ShouldReturnNullWhenValueIsNull()
            {
                var result = ColorMono.FromMagickColor(null);

                Assert.Null(result);
            }

            [Fact]
            public void ShouldInitializeTheProperties()
            {
                var color = MagickColors.Black;
                var grayColor = ColorMono.FromMagickColor(color);

                Assert.Equal(grayColor, color);
            }
        }
    }
}
