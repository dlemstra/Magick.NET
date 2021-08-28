// Copyright Dirk Lemstra https://github.com/dlemstra/Magick.NET.
// Licensed under the Apache License, Version 2.0.

using System.Xml;
using ImageMagick;
using Xunit;

namespace Magick.NET.Core.Tests
{
    public partial class XmlHelperTests
    {
        public class TheSetAttributeMethod
        {
            [Fact]
            public void ShouldAddAttribute()
            {
                var doc = new XmlDocument();
                var element = doc.CreateElement("test");

                XmlHelper.SetAttribute(element, "attr", "val");

                var attribute = element.Attributes["attr"];
                Assert.NotNull(attribute);
                Assert.Equal("val", attribute.Value);
            }

            [Fact]
            public void ShouldChangeValueOfAttribute()
            {
                var doc = new XmlDocument();
                var element = doc.CreateElement("test");

                var attribute = doc.CreateAttribute("attr");
                element.Attributes.Append(attribute);

                XmlHelper.SetAttribute(element, "attr", 42);

                Assert.Equal("42", attribute.Value);
            }
        }
    }
}
