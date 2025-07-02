// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class PerceptualHashTests
{
    public class TheToStringMethod
    {
        [Fact]
        public void ShouldReturnTheValueAsString()
        {
            using var image = new MagickImage(Files.ImageMagickJPG);
            var phash = image.PerceptualHash();

            Assert.NotNull(phash);

            var hash = phash.ToString();

            Assert.Equal(210, hash.Length);

#if Q8
            Assert.Equal("a65e687f9488c1088f0262ee062ee062ee0a26ce81a1e823ec85b3b62ee062ee089ad4a66728738487ed988c6862ee062ee062ee0cf8dbaebc98252482bbf861bc838ee62ee0819a08793a8915289c2b61eb58e31462ee081cd58a01d8c2f18c819627d761c6462ee0", hash);
#elif Q16
            Assert.Equal("a658b87fa188c0688eb562ee062ee062ee0a646682939835e986ec962ee062ee062ee0a66538739a87ee088c6062ee062ee062ee0cf800aeb958251d82bc2861b7838ef62ee08199e8792d8914a89c2061eb28e2f662ee081cd58a0168c2f68c818627d761c6362ee0", hash);
#else
            Assert.Equal("a658a87fa188c0688eb562ee062ee062ee0a731182e3a83aa2876d462ee062ee062ee0a66538739a87ee088c6062ee062ee062ee0cf800aeb958251d82bc2861b7838ef62ee08199e8792d8914a89c2061eb28e2f662ee081cd58a0168c2f68c818627d761c6362ee0", hash);
#endif
            var clone = new PerceptualHash(hash);

            Assert.InRange(phash.SumSquaredDistance(clone), 0.0, 0.001);
        }
    }
}
