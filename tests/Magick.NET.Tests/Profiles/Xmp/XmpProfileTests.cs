// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

using System;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
using ImageMagick;
using Xunit;

namespace Magick.NET.Tests
{
    public class XmpProfileTests
    {
        [Fact]
        public void Test_CreateReader()
        {
            using (var image = new MagickImage(Files.InvitationTIF))
            {
                var profile = image.GetXmpProfile();
                Assert.NotNull(profile);

                using (XmlReader reader = profile.CreateReader())
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(reader);
                    TestIXPathNavigable(doc.CreateNavigator());
                }
            }
        }

        [Fact]
        public void Test_FromIXPathNavigable()
        {
            using (var image = new MagickImage(Files.InvitationTIF))
            {
                var profile = image.GetXmpProfile();
                Assert.NotNull(profile);

                var doc = profile.ToIXPathNavigable();

                Assert.Throws<ArgumentNullException>("document", () =>
                {
                    XmpProfile.FromIXPathNavigable(null);
                });

                var newProfile = XmpProfile.FromIXPathNavigable(doc);
                image.SetProfile(newProfile);

                doc = profile.ToIXPathNavigable();
                TestIXPathNavigable(doc);

                profile = image.GetXmpProfile();
                Assert.NotNull(profile);

                doc = profile.ToIXPathNavigable();
                TestIXPathNavigable(doc);

                Assert.Equal(profile, newProfile);
            }
        }

        [Fact]
        public void Test_FromXDocument()
        {
            using (var image = new MagickImage(Files.InvitationTIF))
            {
                var profile = image.GetXmpProfile();
                Assert.NotNull(profile);

                var doc = profile.ToXDocument();

                Assert.Throws<ArgumentNullException>("document", () =>
                {
                    XmpProfile.FromXDocument(null);
                });

                var newProfile = XmpProfile.FromXDocument(doc);
                image.SetProfile(newProfile);

                doc = profile.ToXDocument();
                TestXDocument(doc);

                profile = image.GetXmpProfile();
                Assert.NotNull(profile);

                doc = profile.ToXDocument();
                TestXDocument(doc);

                Assert.Equal(profile, newProfile);
            }
        }

        [Fact]
        public void Test_ToIXPathNavigable()
        {
            using (var image = new MagickImage(Files.InvitationTIF))
            {
                var profile = image.GetXmpProfile();
                Assert.NotNull(profile);

                IXPathNavigable doc = profile.ToIXPathNavigable();
                TestIXPathNavigable(doc);
            }
        }

        [Fact]
        public void Test_ToXDocument()
        {
            using (var image = new MagickImage(Files.InvitationTIF))
            {
                var profile = image.GetXmpProfile();
                Assert.NotNull(profile);

                XDocument document = profile.ToXDocument();
                TestXDocument(document);
            }
        }

        private static void TestIXPathNavigable(IXPathNavigable document)
        {
            Assert.NotNull(document);

            var navigator = document.CreateNavigator();
            navigator.MoveToRoot();
            Assert.True(navigator.MoveToChild(XPathNodeType.Element));
            Assert.True(navigator.MoveToChild(XPathNodeType.Element));
            Assert.True(navigator.MoveToChild(XPathNodeType.Element));
            int i = 0;
            while (navigator.MoveToNext(XPathNodeType.Element))
                i++;
            Assert.Equal(4, i);
        }

        private static void TestXDocument(XDocument document)
        {
            Assert.NotNull(document);
            Assert.Equal(5, document.Root.Elements().First().Elements().Count());
        }
    }
}
