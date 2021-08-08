// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public class TheBmpCoder
    {
        [Fact]
        public void ShouldBeAbleToReadBmp3Format()
        {
            using (var file = new TemporaryFile(Files.MagickNETIconPNG))
            {
                using (var image = new MagickImage(file.FileInfo))
                {
                    image.Write(file.FileInfo, MagickFormat.Bmp3);
                }

                var settings = new MagickReadSettings
                {
                    Format = MagickFormat.Bmp3,
                };

                using (var image = new MagickImage(file.FileInfo, settings))
                {
                    Assert.Equal(MagickFormat.Bmp3, image.Format);
                }
            }
        }
    }
}
