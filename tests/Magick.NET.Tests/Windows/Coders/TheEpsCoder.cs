// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if WINDOWS_BUILD

using System.Linq;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class TheEpsCoder
    {
        [Fact]
        public void ShouldReadTwoImages()
        {
            using (var images = new MagickImageCollection(Files.Coders.SwedenHeartEPS))
            {
                Assert.Equal(2, images.Count);

                Assert.Equal(447, images[0].Width);
                Assert.Equal(420, images[0].Height);
                Assert.Equal(MagickFormat.Ept, images[0].Format);

                Assert.Equal(447, images[1].Width);
                Assert.Equal(420, images[1].Height);
                Assert.Equal(MagickFormat.Tiff, images[1].Format);
            }
        }

        [Fact]
        public void ShouldReadClipPathsInTiffPreview()
        {
            using (var images = new MagickImageCollection(Files.Coders.SwedenHeartEPS))
            {
                var profile = images[1].Get8BimProfile();

                Assert.Single(profile.ClipPaths);
            }
        }
    }
}

#endif