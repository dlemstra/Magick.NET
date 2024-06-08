// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Text;
using System.Xml.Linq;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class XmpProfileTests
{
    public class TheFromXDocumentMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenDocumentIsNull()
        {
            Assert.Throws<ArgumentNullException>("document", () => XmpProfile.FromXDocument(null));
        }

        [Fact]
        public void ShouldCreateProfileFromIXDocument()
        {
            var document = XDocument.Parse("<test />");

            var profile = XmpProfile.FromXDocument(document);

            Assert.NotNull(profile);

            var xml = Encoding.UTF8.GetString(profile.ToByteArray());

            Assert.Equal("<test />", xml);
        }
    }
}
