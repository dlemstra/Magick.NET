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

using System;
using System.Xml;
using ImageMagick;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    public partial class ScriptVariablesTests
    {
        [TestClass]
        public class TheGetSingleArrayMethod
        {
            [TestMethod]
            public void ShouldReturnNullWhenElementIsNull()
            {
                var document = new XmlDocument();
                var scriptVariables = new ScriptVariables(document);

                var result = scriptVariables.GetSingleArray(null);

                Assert.IsNull(result);
            }

            [TestMethod]
            public void ShouldReturnNullWhenElementHasNoVariableAttribute()
            {
                var document = new XmlDocument();
                var scriptVariables = new ScriptVariables(document);

                var element = document.CreateElement("test");

                var result = scriptVariables.GetSingleArray(element);

                Assert.IsNull(result);
            }

            [TestMethod]
            public void ShouldReturnNullWhenVariableValueIsInvalid()
            {
                var document = new XmlDocument();
                document.LoadXml("<test foo=\"{$foo}\"/>");

                var scriptVariables = new ScriptVariables(document);

                var element = document.CreateElement("test");
                element.SetAttribute("variable", "bar");

                var result = scriptVariables.GetSingleArray(element);

                Assert.IsNull(result);
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenVariableNameIsInvalid()
            {
                var document = new XmlDocument();
                document.LoadXml("<test foo=\"{$foo}\"/>");

                var scriptVariables = new ScriptVariables(document);

                var element = document.CreateElement("test");
                element.SetAttribute("variable", "{$bar}");

                ExceptionAssert.Throws<InvalidOperationException>(() =>
                {
                    scriptVariables.GetSingleArray(element);
                }, "Invalid variable name: bar");
            }

            [TestMethod]
            public void ShouldThrowExceptionWhenVariableTypeIsIncorrect()
            {
                var document = new XmlDocument();
                document.LoadXml("<test foo=\"{$foo}\"/>");

                var scriptVariables = new ScriptVariables(document);
                scriptVariables.Set("foo", new string[] { });

                var element = document.CreateElement("test");
                element.SetAttribute("variable", "{$foo}");

                ExceptionAssert.Throws<InvalidOperationException>(() =>
                {
                    scriptVariables.GetSingleArray(element);
                }, "The value of variable 'foo' is not a float[].");
            }

            [TestMethod]
            public void ShouldReturnValue()
            {
                var document = new XmlDocument();
                document.LoadXml("<test foo=\"{$foo}\"/>");

                var scriptVariables = new ScriptVariables(document);
                scriptVariables.Set("foo", new float[] { 42 });

                var element = document.CreateElement("test");
                element.SetAttribute("variable", "{$foo}");

                var result = scriptVariables.GetSingleArray(element);

                Assert.IsNotNull(result);
                Assert.AreEqual(42.0f, result[0]);
            }
        }
    }
}
