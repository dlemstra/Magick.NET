// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

#if NETCOREAPP

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class ImageProfileTests
{
    public class TheToReadOnlySpanMethod
    {
        [Fact]
        public void ShouldReturnReadonlySpanWithCorrectLength()
        {
            using var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG);
            var profile = image.GetIptcProfile();

            Assert.NotNull(profile);

            var bytes = profile.ToReadOnlySpan();
            Assert.Equal(273, bytes.Length);
        }
    }
}

#endif
