// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public class ThePnmCoder
    {
        [Fact]
        public void ShouldCreateWhiteImage()
        {
            using (var input = new MagickImage("xc:white", 1, 1))
            {
                using (var memoryStream = new MemoryStream())
                {
                    input.Write(memoryStream, MagickFormat.Pnm);

                    memoryStream.Position = 0;
                    using (var output = new MagickImage(memoryStream))
                    {
                        ColorAssert.Equal(MagickColors.White, output, 0, 0);
                    }
                }
            }
        }

        [Fact]
        public void ShouldCreateBlackImage()
        {
            using (var input = new MagickImage("xc:black", 1, 1))
            {
                using (var memoryStream = new MemoryStream())
                {
                    input.Write(memoryStream, MagickFormat.Pnm);

                    memoryStream.Position = 0;
                    using (var output = new MagickImage(memoryStream))
                    {
                        ColorAssert.Equal(MagickColors.Black, output, 0, 0);
                    }
                }
            }
        }
    }
}
