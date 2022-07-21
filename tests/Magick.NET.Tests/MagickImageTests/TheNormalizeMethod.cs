// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheNormalizeMethod
        {
            [Fact]
            public void ShouldNormalizeTheImage()
            {
                using (var images = new MagickImageCollection())
                {
                    images.Add(new MagickImage("gradient:gray70-gray30", 100, 100));
                    images.Add(new MagickImage("gradient:blue-navy", 50, 100));

                    using (var colorRange = images.AppendHorizontally())
                    {
                        ColorAssert.Equal(new MagickColor("gray70"), colorRange, 0, 0);
                        ColorAssert.Equal(new MagickColor("blue"), colorRange, 101, 0);

                        ColorAssert.Equal(new MagickColor("gray30"), colorRange, 0, 99);
                        ColorAssert.Equal(new MagickColor("navy"), colorRange, 101, 99);

                        colorRange.Normalize();

                        ColorAssert.Equal(new MagickColor("white"), colorRange, 0, 0);
                        ColorAssert.Equal(new MagickColor("blue"), colorRange, 101, 0);

#if Q8
                        ColorAssert.Equal(new MagickColor("gray40"), colorRange, 0, 99);
                        ColorAssert.Equal(new MagickColor("#0000b3"), colorRange, 101, 99);
#else
                        ColorAssert.Equal(new MagickColor("#662e662e662e"), colorRange, 0, 99);
                        ColorAssert.Equal(new MagickColor("#00000000b317"), colorRange, 101, 99);
#endif
                    }
                }
            }
        }
    }
}
