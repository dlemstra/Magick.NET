// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;
using System.Linq;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class EightBimProfileTests
{
    public class TheSetIptcProfileMethod
    {
        [Fact]
        public void ShouldRemoveProfileWhenSetToNull()
        {
            using var image = new MagickImage(Files.EightBimJPG);

            var iptcProfile = image.GetIptcProfile();
            Assert.NotNull(iptcProfile);

            image.RemoveProfile(iptcProfile);

            var profile = image.Get8BimProfile();
            Assert.NotNull(profile);

            profile.SetIptcProfile(null);

            image.SetProfile(profile);

            using var stream = new MemoryStream();
            image.Write(stream);

            stream.Position = 0;
            image.Read(stream);

            iptcProfile = image.GetIptcProfile();
            Assert.Null(iptcProfile);
        }

        [Fact]
        public void ShouldAddTheProfile()
        {
            using var image = new MagickImage(Files.EightBimTIF);

            var profile = image.Get8BimProfile();
            Assert.NotNull(profile);

            var bytes = new byte[] { 0x1c, 0x02, 0, 0, 0 };
            profile.SetIptcProfile(new IptcProfile(bytes));

            image.SetProfile(profile);

            using var stream = new MemoryStream();
            image.Write(stream);

            stream.Position = 0;
            image.Read(stream);

            profile = image.Get8BimProfile();
            Assert.NotNull(profile);

            var iptcProfile = image.GetIptcProfile();

            Assert.NotNull(profile.GetIptcProfile());
            Assert.NotNull(iptcProfile);
            Assert.Equal(bytes, iptcProfile.ToByteArray().Skip(8));
        }
    }
}
