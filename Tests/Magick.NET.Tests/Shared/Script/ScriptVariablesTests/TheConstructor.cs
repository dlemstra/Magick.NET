// Copyright 2013-2019 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

using System.Linq;
using System.Xml;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class ScriptVariablesTests
    {
        [TestClass]
        public class TheConstructor
        {
            [TestMethod]
            public void ShouldSupportEmptyDocument()
            {
                var document = new XmlDocument();
                var scriptVariables = new ScriptVariables(document);
            }

            [TestMethod]
            public void ShouldLoadTheVariablesFromTheScript()
            {
                var document = new XmlDocument();
                document.Load(Files.Scripts.Variables);

                var scriptVariables = new ScriptVariables(document);

                string[] names = scriptVariables.Names.ToArray();
                Assert.AreEqual(4, names.Length);
                Assert.AreEqual("width", names[0]);
                Assert.AreEqual("height", names[1]);
                Assert.AreEqual("color", names[2]);
                Assert.AreEqual("fillColor", names[3]);
            }
        }
    }
}
