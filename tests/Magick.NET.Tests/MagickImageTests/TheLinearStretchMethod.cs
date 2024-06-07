// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class MagickImageTests
{
    public class TheLinearStretchMethod
    {
#if !Q16HDRI
        [Fact]
        public void ShouldThrowExceptionWhenBlackPointIsNegative()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentException>("blackPoint", () => image.LinearStretch(new Percentage(-1), new Percentage(0)));
        }

        [Fact]
        public void ShouldThrowExceptionWhenWhitePointIsNegative()
        {
            using var image = new MagickImage();

            Assert.Throws<ArgumentException>("whitePoint", () => image.LinearStretch(new Percentage(0), new Percentage(-1)));
        }
#endif

        [Fact]
        public void ShouldStretchThePixelsWhenValuesAreEqual()
        {
            using var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG);
            image.Scale(100, 100);

            image.LinearStretch((Percentage)1, (Percentage)1);
            using var memStream = new MemoryStream();
            image.Format = MagickFormat.Histogram;
            image.Write(memStream);
            memStream.Position = 0;

            using var histogram = new MagickImage(memStream);

#if Q8
            ColorAssert.Equal(MagickColors.Red, histogram, 65, 38);
            ColorAssert.Equal(MagickColors.Lime, histogram, 135, 0);
            ColorAssert.Equal(MagickColors.Blue, histogram, 209, 81);
#else
            ColorAssert.Equal(MagickColors.Red, histogram, 34, 183);
            ColorAssert.Equal(MagickColors.Lime, histogram, 122, 193);
            ColorAssert.Equal(MagickColors.Blue, histogram, 210, 194);
#endif
        }

        [Fact]
        public void ShouldStretchThePixels()
        {
            using var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG);
            image.Scale(100, 100);

            image.LinearStretch((Percentage)10, (Percentage)90);
            using var memStream = new MemoryStream();
            image.Format = MagickFormat.Histogram;
            image.Write(memStream);
            memStream.Position = 0;

            using var histogram = new MagickImage(memStream);

#if Q8
            ColorAssert.Equal(MagickColors.Red, histogram, 97, 174);
            ColorAssert.Equal(MagickColors.Blue, histogram, 195, 190);
            ColorAssert.Equal(MagickColors.Lime, histogram, 213, 168);
#else
            ColorAssert.Equal(MagickColors.Red, histogram, 136, 178);
            ColorAssert.Equal(MagickColors.Blue, histogram, 56, 191);
            ColorAssert.Equal(MagickColors.Lime, histogram, 41, 173);
#endif
        }
    }
}
