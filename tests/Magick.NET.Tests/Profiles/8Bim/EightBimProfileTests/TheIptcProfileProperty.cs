// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.IO;
using System.Linq;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class EightBimProfileTests
{
    public class TheIptcProfileProperty
    {
        [Fact]
        public void ShouldReturnNullWhenProfileHasNoIptcProfile()
        {
            using var image = new MagickImage(Files.EightBimTIF);

            var profile = image.Get8BimProfile()!;

            Assert.Null(profile.IptcProfile);
        }

        [Fact]
        public void ShouldReturnValueWhenProfileHasIptcProfile()
        {
            using var image = new MagickImage(Files.EightBimJPG);

            var profile = image.Get8BimProfile();

            Assert.NotNull(profile.IptcProfile);
            Assert.Equal(7, profile.IptcProfile.GetData().Length);
            Assert.Single(profile.IptcProfile.Values);
        }

        [Fact]
        public void ShouldRemoveProfileWhenSetToNull()
        {
            using var image = new MagickImage(Files.EightBimJPG);

            var iptcProfile = image.GetIptcProfile();
            image.RemoveProfile(iptcProfile);

            var profile = image.Get8BimProfile();
            profile.IptcProfile = null;

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
            var bytes = new byte[] { 0x1c, 0x02, 0, 0, 0, 0, 0, 0 };
            profile.IptcProfile = new IptcProfile(bytes);

            image.SetProfile(profile);

            using var stream = new MemoryStream();
            image.Write(stream);

            stream.Position = 0;
            image.Read(stream);

            profile = image.Get8BimProfile();
            var iptcProfile = image.GetIptcProfile();

            Assert.NotNull(profile.IptcProfile);
            Assert.NotNull(iptcProfile);
            Assert.Equal(bytes, iptcProfile.GetData().Skip(8));
        }
    }
}
