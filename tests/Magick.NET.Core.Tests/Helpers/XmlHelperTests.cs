// Copyright 2013-2020 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using System.Xml;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    [TestClass]
    public class XmlHelperTests
    {
        [TestMethod]
        public void CreateElement_NodeIsXmlDocument_AddsNodeAsDocumentElement()
        {
            var doc = new XmlDocument();
            XmlElement element = XmlHelper.CreateElement(doc, "test");

            Assert.AreEqual(doc.DocumentElement, element);
            Assert.AreEqual("test", element.Name);
        }

        [TestMethod]
        public void CreateElement_NodeIsXmlNode_AddsNodeAsChildElement()
        {
            var doc = new XmlDocument();
            XmlElement root = XmlHelper.CreateElement(doc, "root");

            XmlElement element = XmlHelper.CreateElement(root, "test");

            Assert.AreEqual(root.FirstChild, element);
            Assert.AreEqual("test", element.Name);
        }

        [TestMethod]
        public void SetAttribute_DoesNotHaveAttritubte_AttributeIsAdded()
        {
            var doc = new XmlDocument();
            XmlElement element = doc.CreateElement("test");

            XmlHelper.SetAttribute(element, "attr", "val");

            XmlAttribute attribute = element.Attributes["attr"];
            Assert.IsNotNull(attribute);
            Assert.AreEqual("val", attribute.Value);
        }

        [TestMethod]
        public void SetAttribute_HasAttribute_ValueIsSet()
        {
            var doc = new XmlDocument();
            XmlElement element = doc.CreateElement("test");
            XmlAttribute attribute = doc.CreateAttribute("attr");
            element.Attributes.Append(attribute);

            XmlHelper.SetAttribute(element, "attr", 42);

            Assert.AreEqual("42", attribute.Value);
        }
    }
}
