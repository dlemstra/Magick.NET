// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Linq;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class EightBimValueTests
{
    public class TheToByteArrayMethod
    {
        [Fact]
        public void ShouldConvertTheValueToByteArray()
        {
            using var image = new MagickImage(Files.FujiFilmFinePixS1ProJPG);
            var profile = image.Get8BimProfile();

            Assert.NotNull(profile);

            var value = profile.Values.First();
            var bytes = value.ToByteArray();

            Assert.Equal(273, bytes.Length);
        }
    }
}
