// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public class TheJpegXlCoder
    {
        [Fact]
        public void ShouldWriteCorrectOutputImage()
        {
            using (var image = new MagickImage(Files.Builtin.Logo))
            {
                using (var memoryStream = new MemoryStream())
                {
                    image.Write(memoryStream, MagickFormat.Jxl);

                    Assert.InRange(memoryStream.Length, 20000, 40000);
                }
            }
        }
    }
}
