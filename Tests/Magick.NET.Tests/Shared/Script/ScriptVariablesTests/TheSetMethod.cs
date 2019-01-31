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

using System.Xml;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class ScriptVariablesTests
    {
        [TestClass]
        public class TheSetMethod
        {
            [TestMethod]
            public void ShouldThrowExceptionWhenNameIsNull()
            {
                var document = new XmlDocument();
                var scriptVariables = new ScriptVariables(document);

                ExceptionAssert.ThrowsArgumentNullException("name", () =>
                {
                    scriptVariables.Set(null, "42");
                });
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenNameToSetIsInvalid()
            {
                var document = new XmlDocument();
                var scriptVariables = new ScriptVariables(document);

                ExceptionAssert.ThrowsArgumentException("name", () =>
                {
                    scriptVariables.Set("invalid", "42");
                }, "Invalid variable name: invalid");
            }

            [TestMethod]
            public void ShouldSetTheVariable()
            {
                var document = new XmlDocument();
                document.LoadXml("<test foo=\"{$foo}\"/>");

                var scriptVariables = new ScriptVariables(document);
                scriptVariables.Set("foo", "test");

                Assert.AreEqual("test", scriptVariables.Get("foo"));
            }
        }
    }
}
