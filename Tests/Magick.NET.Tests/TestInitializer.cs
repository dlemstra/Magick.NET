// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

using ImageMagick;
using ImageMagick.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System;
using System.Xml;

namespace Magick.NET.Tests
{
    [TestClass]
    public static class TestInitializer
    {
        private static string _path;

        [AssemblyInitialize]
        public static void InitializeWithCustomPolicy(TestContext context)
        {

#if !NETCOREAPP1_1
            MagickNET.SetGhostscriptDirectory(@"C:\Program Files (x86)\gs\gs9.20\bin");
#endif

            ConfigurationFiles configFiles = ConfigurationFiles.Default;
            configFiles.Policy.Data = ModifyPolicy(configFiles.Policy.Data);

            _path = MagickNET.Initialize(configFiles);
        }

        [AssemblyCleanup]
        public static void RemoveCustomPolicyFolder()
        {
            if (_path != null)
                Directory.Delete(_path, true);
        }

        private static string ModifyPolicy(string data)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(data);

            XmlElement policy = doc.CreateElement("policy");
            SetAttribute(policy, "domain", "coder");
            SetAttribute(policy, "rights", "none");
            SetAttribute(policy, "pattern", "{PALM}");

            doc.DocumentElement.AppendChild(policy);

            return doc.OuterXml;
        }

        private static void SetAttribute(XmlElement element, string name, string value)
        {
            XmlAttribute attribute = element.OwnerDocument.CreateAttribute(name);
            attribute.Value = value;

            element.Attributes.Append(attribute);
        }
    }
}
