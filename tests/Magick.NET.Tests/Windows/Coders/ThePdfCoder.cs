// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if WINDOWS_BUILD

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class ThePdfCoder
    {
        [Fact]
        public void Test_Format()
        {
            using (var image = new MagickImage(Files.Coders.CartoonNetworkStudiosLogoAI))
            {
                Assert.Equal(765, image.Width);
                Assert.Equal(361, image.Height);
                Assert.Equal(MagickFormat.Ai, image.Format);
            }
        }
    }
}

#endif