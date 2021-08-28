// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Xml;
using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests
{
    public partial class XmlHelperTests
    {
        public class TheCreateElementMethod
        {
            [Fact]
            public void ShouldAddXmlDocumentAsDocumentElement()
            {
                var doc = new XmlDocument();
                var element = XmlHelper.CreateElement(doc, "test");

                Assert.Equal(doc.DocumentElement, element);
                Assert.Equal("test", element.Name);
            }

            [Fact]
            public void ShouldAddXmlNodeAsChildElement()
            {
                var doc = new XmlDocument();
                var root = XmlHelper.CreateElement(doc, "root");

                var element = XmlHelper.CreateElement(root, "test");

                Assert.Equal(root.FirstChild, element);
                Assert.Equal("test", element.Name);
            }
        }
    }
}
