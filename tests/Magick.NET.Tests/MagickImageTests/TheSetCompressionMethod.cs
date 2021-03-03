// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickImageTests
    {
        public class TheSetCompressionMethod
        {
            [Fact]
            public void ShouldChangeTheCompression()
            {
                using (var image = new MagickImage())
                {
                    image.SetCompression(CompressionMethod.JBIG2);

                    Assert.Equal(CompressionMethod.JBIG2, image.Compression);
                }
            }
        }
    }
}
