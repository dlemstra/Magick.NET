// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class XmpProfileTests
{
    public class TheToIXPathNavigableMethod
    {
        [Fact]
        public void ShouldCreateIXPathNavigableFromProfile()
        {
            using var image = new MagickImage(Files.InvitationTIF);
            var profile = image.GetXmpProfile();

            Assert.NotNull(profile);

            var doc = profile.ToIXPathNavigable();

            Assert.NotNull(doc);

            var xml = doc.CreateNavigator()?.OuterXml;

            Assert.StartsWith(@"<?xpacket begin="""" id=""W5M0MpCehiHzreSzNTczkc9d""?>", xml);
        }

        [Fact]
        public void ShouldReturnNullWhenTheProfileContainsNoData()
        {
        }
    }
}
