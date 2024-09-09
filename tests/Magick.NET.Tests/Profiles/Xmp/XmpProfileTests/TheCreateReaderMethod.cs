// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Xml;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class XmpProfileTests
{
    public class TheCreateReaderMethod
    {
        [Fact]
        public void ShouldCreateAnXmlReader()
        {
            using var image = new MagickImage(Files.InvitationTIF);
            var profile = image.GetXmpProfile();

            Assert.NotNull(profile);

            using var reader = profile.CreateReader();
            Assert.NotNull(reader);

            var doc = new XmlDocument();
            doc.Load(reader);
            var xml = doc.CreateNavigator()?.OuterXml;

            Assert.StartsWith(@"<?xpacket begin="""" id=""W5M0MpCehiHzreSzNTczkc9d""?>", xml);
        }
    }
}
