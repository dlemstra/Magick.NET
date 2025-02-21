// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheWhiteBalanceMethod
    {
        [Fact]
        public void ShouldWhiteBalanceTheImage()
        {
            using var image = new MagickImage(Files.Builtin.Rose);
            image.WhiteBalance();
#if Q8
            ColorAssert.Equal(new MagickColor("#dd4946"), image, 45, 25);
#elif Q16
            ColorAssert.Equal(new MagickColor("#de4c4a664691"), image, 45, 25);
#else
            ColorAssert.Equal(new MagickColor("#de4c4a654692"), image, 45, 25);
#endif
        }

        [Fact]
        public void ShouldUseTheVibrance()
        {
            using var image = new MagickImage(Files.Builtin.Rose);
            image.WhiteBalance(new Percentage(70));

#if Q8
            ColorAssert.Equal(new MagickColor("#00cb91"), image, 45, 25);
#elif Q16
            ColorAssert.Equal(new MagickColor("#0000cc33926f"), image, 45, 25);
#else
            image.Clamp();
            ColorAssert.Equal(new MagickColor("#0000cc32926f"), image, 45, 25);
#endif
        }
    }
}
