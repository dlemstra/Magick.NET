// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheSepiaToneMethod
        {
            [Fact]
            public void ShouldApplySpecialEffect()
            {
                using (var image = new MagickImage(Files.Builtin.Logo))
                {
                    image.SepiaTone();

#if Q8
                    ColorAssert.Equal(new MagickColor("#472400"), image, 243, 45);
                    ColorAssert.Equal(new MagickColor("#522e00"), image, 394, 394);
                    ColorAssert.Equal(new MagickColor("#e4bb7c"), image, 477, 373);
#elif Q16
                    ColorAssert.Equal(new MagickColor(OpenCLValue.Get("#45be23e80000", "#475f24bf0000")), image, 243, 45);
                    ColorAssert.Equal(new MagickColor(OpenCLValue.Get("#50852d680000", "#52672e770000")), image, 394, 394);
                    ColorAssert.Equal(new MagickColor(OpenCLValue.Get("#e273b8c17a35", "#e5adbb627bf2")), image, 477, 373);
#else
                    ColorAssert.Equal(new MagickColor(OpenCLValue.Get("#45be23e90001", "#475f24bf0000")), image, 243, 45);
                    ColorAssert.Equal(new MagickColor(OpenCLValue.Get("#50862d690001", "#52672e770000")), image, 394, 394);
                    ColorAssert.Equal(new MagickColor(OpenCLValue.Get("#e274b8c17a35", "#e5adbb627bf2")), image, 477, 373);
#endif
                }
            }
        }
    }
}
