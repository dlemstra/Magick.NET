// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System;
using System.Text;
using System.Xml;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests;

public partial class XmpProfileTests
{
    public class TheFromIXPathNavigableMethod
    {
        [Fact]
        public void ShouldThrowExceptionWhenDocumentIsNull()
        {
            Assert.Throws<ArgumentNullException>("document", () => XmpProfile.FromIXPathNavigable(null));
        }

        [Fact]
        public void ShouldCreateProfileFromIXPathNavigable()
        {
            var document = new XmlDocument();
            document.LoadXml("<test />");

            var profile = XmpProfile.FromIXPathNavigable(document);

            Assert.NotNull(profile);

            var xml = Encoding.UTF8.GetString(profile.ToByteArray());

            Assert.Equal(@"﻿<test />", xml);
        }
    }
}
