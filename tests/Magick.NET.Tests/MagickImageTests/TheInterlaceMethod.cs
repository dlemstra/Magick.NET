// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheInterlaceMethod
        {
            [Fact]
            public void ShouldUseNoInterlaceAsTheDefault()
            {
                using (var image = new MagickImage(MagickColors.Fuchsia, 100, 60))
                {
                    using (var memStream = new MemoryStream())
                    {
                        image.Format = MagickFormat.Jpeg;
                        image.Write(memStream);

                        memStream.Position = 0;
                        image.Read(memStream);

                        Assert.Equal(Interlace.NoInterlace, image.Interlace);
                    }
                }
            }

            [Fact]
            public void ShouldBeUseWhenWritingJpegImage()
            {
                using (var image = new MagickImage(MagickColors.Fuchsia, 100, 60))
                {
                    using (var memStream = new MemoryStream())
                    {
                        image.Interlace = Interlace.Undefined;
                        image.Write(memStream);

                        memStream.Position = 0;
                        image.Read(memStream);

                        Assert.Equal(Interlace.Jpeg, image.Interlace);
                    }
                }
            }
        }
    }
}
