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

using System.IO;
using System.Xml;
using Xunit;

namespace Magick.NET.Tests
{
    public partial class MagickNETTests
    {
        private static void AssertConfigFiles(string path)
        {
            Assert.True(File.Exists(Path.Combine(path, "colors.xml")));
            Assert.True(File.Exists(Path.Combine(path, "configure.xml")));
            Assert.True(File.Exists(Path.Combine(path, "delegates.xml")));
            Assert.True(File.Exists(Path.Combine(path, "english.xml")));
            Assert.True(File.Exists(Path.Combine(path, "locale.xml")));
            Assert.True(File.Exists(Path.Combine(path, "log.xml")));
            Assert.True(File.Exists(Path.Combine(path, "policy.xml")));
            Assert.True(File.Exists(Path.Combine(path, "thresholds.xml")));
            Assert.True(File.Exists(Path.Combine(path, "type.xml")));
            Assert.True(File.Exists(Path.Combine(path, "type-ghostscript.xml")));
        }

        private static string ModifyPolicy(string data)
        {
            var settings = new XmlReaderSettings()
            {
                DtdProcessing = DtdProcessing.Ignore,
            };

            var doc = new XmlDocument();
            using (StringReader sr = new StringReader(data))
            {
                using (XmlReader reader = XmlReader.Create(sr, settings))
                {
                    doc.Load(reader);
                }
            }

            var policy = doc.CreateElement("policy");
            SetAttribute(policy, "domain", "coder");
            SetAttribute(policy, "rights", "none");
            SetAttribute(policy, "pattern", "{PALM}");

            doc.DocumentElement.AppendChild(policy);

            return doc.OuterXml;
        }

        private static string CreateTypeData() => $@"
<?xml version=""1.0""?>
<typemap>
<type format=""ttf"" name=""Arial"" fullname=""Arial"" family=""Arial"" glyphs=""{Files.Fonts.Arial}""/>
<type format=""ttf"" name=""CourierNew"" fullname=""Courier New"" family=""Courier New"" glyphs=""{Files.Fonts.CourierNew}""/>
</typemap>
";

        private static void SetAttribute(XmlElement element, string name, string value)
        {
            var attribute = element.OwnerDocument.CreateAttribute(name);
            attribute.Value = value;

            element.Attributes.Append(attribute);
        }
    }
}
