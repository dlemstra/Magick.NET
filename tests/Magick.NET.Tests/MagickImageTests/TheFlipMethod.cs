// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheFlipMethod
        {
            [Fact]
            public void ShouldFlipTheImageVertically()
            {
                using (var images = new MagickImageCollection())
                {
                    images.Add(new MagickImage(MagickColors.DodgerBlue, 10, 10));
                    images.Add(new MagickImage(MagickColors.Firebrick, 10, 10));

                    using (var image = images.AppendVertically())
                    {
                        ColorAssert.Equal(MagickColors.DodgerBlue, image, 5, 0);
                        ColorAssert.Equal(MagickColors.Firebrick, image, 5, 10);

                        image.Flip();

                        ColorAssert.Equal(MagickColors.Firebrick, image, 5, 0);
                        ColorAssert.Equal(MagickColors.DodgerBlue, image, 5, 10);
                    }
                }
            }
        }
    }
}
