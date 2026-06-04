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
            Assert.Equal("a65e687f9488c1088f0261ce08eeb961d40a26ce81a1e823ec85b3b8cc3586ec889ad4a66728738487ed988c6861cda8dea761b93cf8dbaebc98252482bbf861bc838ee8561c819a08793a8915289c2b61eb58e3146222681cd58a01d8c2f18c819627d761c64629d1", hash);
#elif Q16
            Assert.Equal("a658b87fa188c0688eb561ccb8ed8a61d43a646682939835e986ec98c78f887ae8c67fa66538739a87ee088c6061cd18de7661b94cf800aeb958251d82bc2861b7838ef8561e8199e8792d8914a89c2061eb28e2f66222d81cd58a0168c2f68c818627d761c63629d6", hash);
#else
            Assert.Equal("a658a87fa188c0688eb561ccb8ed8961d43a731182e3a83aa2876d48d19488f438dcb5a66538739a87ee088c6061cd18de7661b94cf800aeb958251d82bc2861b7838ef8561e8199e8792d8914a89c2061eb28e2f66222d81cd58a0168c2f68c818627d761c63629d6", hash);
#endif
            var clone = new PerceptualHash(hash);

            Assert.InRange(phash.SumSquaredDistance(clone), 0.0, 0.001);
        }
    }
}
