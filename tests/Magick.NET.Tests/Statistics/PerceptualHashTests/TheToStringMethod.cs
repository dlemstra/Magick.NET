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
            OpenCLValue.Assert("81b4488655898d38a7aa6223562032620f8a2614819b78241685c4b8c1a786f0689c9881b1f884ca8a0d38af2f622728fd3d623fedeacea78bcaedaa81d8884349824c583ad981c37895998c8658c42a628ed61b216279b81b49887348a1608af44622a3619d362371", "81b4488656898d38a7a96223562017620f7a26cd81a1e823ec85b3b8cc3186ec889ad481b1f884cb8a0d58af30622728fd41623fedea8aa78d4aeda481d8f84355824cd83ae281c378959a8c8668c42a628ec61b216279c81b49887348a1608af44622a3619d362370", hash);
#elif Q16
            OpenCLValue.Assert("81b4488652898d48a7a9622346206e620f8a649d8297d835bd86eb58c7be887e78c5f881b1e884c58a0d18af2d622718fd35623ffdeac9a78cbaedaa81d888434e824c683ad781c37895978c8688c426628ed61b216279b81b48887318a1628af43622a2619d162372", "81b4488652898d48a7a9622346206e620f8a646682939835e986ec98c78f887ae8c67f81b1e884c58a0d18af2d622718fd35623ffdeac9a78cbaedaa81d888434e824c683ad781c37895978c8688c426628ed61b216279b81b48887318a1628af43622a2619d162372", hash);
#else
            OpenCLValue.Assert("81b4488652898d48a7a9622346206e620f8a730882e4a83a9e877108d25488fc58dbb781b1e884c58a0d18af2d622718fd35623ffdeac9a78cbaedaa81d888434e824c683ad781c37895978c8688c426628ed61b216279b81b48887318a1628af43622a2619d162372", "81b4488652898d48a7a9622346206e620f8a731182e3a83aa2876d48d19488f438dcb581b1e884c58a0d18af2d622718fd35623ffdeac9a78cbaedaa81d888434e824c683ad781c37895978c8688c426628ed61b216279b81b48887318a1628af43622a2619d162372", hash);
#endif
            var clone = new PerceptualHash(hash);

            Assert.InRange(phash.SumSquaredDistance(clone), 0.0, 0.001);
        }
    }
}
