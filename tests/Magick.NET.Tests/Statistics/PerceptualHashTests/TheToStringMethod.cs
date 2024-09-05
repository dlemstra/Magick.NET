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
            var hash = phash.ToString();

            Assert.Equal(210, hash.Length);

#if Q8
            OpenCLValue.Assert("a65e687f9488c1288f0461ce08eead61d41a2614819b78241685c4b8c1a986f0689c97a66728738287ede88c6a61cda8dea961b93cf8cfaebbe8252682bba861b5838e8856168199f879388915189c2a61eb48e30e6222981cd58a01b8c2f38c819627d761c63629d4", "a65e687f9388c0e88f0261ce08eeb361d40a26ce81a1e823ec85b3b8cc3586ec889ad4a66738738487edb88c6a61cd98dea261b93cf8dbaebc98252482bbf861bc838ee8561c819a08793b8915289c2b61eb58e3156222681cd58a01d8c2f18c819627d761c64629d1", hash);
#elif Q16
            OpenCLValue.Assert("a658c87fa088c0588eb561ccb8ed8a61d43a649d8297d835bd86eb58c7c0887e88c5f5a66578739b87ee188c6161cd18de7861b94cf800aeb958251d82bc2861b7838ef8561e8199e8792d8914a89c2061eb28e2f76222d81cd58a0168c2f68c818627d761c63629d6", "a658c87fa088c0588eb561ccb8ed8a61d43a646682939835e986ec98c78f887ae8c67fa66578739b87ee188c6161cd18de7861b94cf800aeb958251d82bc2861b7838ef8561e8199e8792d8914a89c2061eb28e2f76222d81cd58a0168c2f68c818627d761c63629d6", hash);
#else
            OpenCLValue.Assert("a658c87fa188c0588eb561ccb8ed8861d43a732982e4c83aa9877068d20288f928dc96a66568739b87ee188c6161cd18de7661b94cf800aeb958251d82bc2861b7838ef8561e8199e8792d8914a89c2061eb28e2f76222d81cd58a0168c2f68c818627d761c63629d6", "a658c87fa188c0588eb561ccb8ed8861d43a731182e3a83aa2876d48d19488f438dcb5a66568739b87ee188c6161cd18de7661b94cf800aeb958251d82bc2861b7838ef8561e8199e8792d8914a89c2061eb28e2f76222d81cd58a0168c2f68c818627d761c63629d6", hash);
#endif
            var clone = new PerceptualHash(hash);

            Assert.InRange(phash.SumSquaredDistance(clone), 0.0, 0.001);
        }
    }
}
