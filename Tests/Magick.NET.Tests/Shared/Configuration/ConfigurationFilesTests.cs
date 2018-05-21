// Copyright 2013-2018 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

using ImageMagick.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests.Configuration
{
    [TestClass]
    public class ConfigurationFilesTests
    {
        private ConfigurationFiles _default = ConfigurationFiles.Default;

        [TestMethod]
        public void Default_Coder_IsInitialized()
        {
            Assert.IsNotNull(_default.Coder);
            Assert.AreEqual("coder.xml", _default.Coder.FileName);
            Assert.IsNotNull(_default.Coder.Data);
            Assert.IsTrue(_default.Coder.Data.Contains("<codermap>"));
        }

        [TestMethod]
        public void Default_Colors_IsInitialized()
        {
            Assert.IsNotNull(_default.Colors);
            Assert.AreEqual("colors.xml", _default.Colors.FileName);
            Assert.IsNotNull(_default.Colors.Data);
            Assert.IsTrue(_default.Colors.Data.Contains("<colormap>"));
        }

        [TestMethod]
        public void Default_Configure_IsInitialized()
        {
            Assert.IsNotNull(_default.Configure);
            Assert.AreEqual("configure.xml", _default.Configure.FileName);
            Assert.IsNotNull(_default.Configure.Data);
            Assert.IsTrue(_default.Configure.Data.Contains("<configuremap>"));
        }

        [TestMethod]
        public void Default_Delegates_IsInitialized()
        {
            Assert.IsNotNull(_default.Delegates);
            Assert.AreEqual("delegates.xml", _default.Delegates.FileName);
            Assert.IsNotNull(_default.Delegates.Data);
            Assert.IsTrue(_default.Delegates.Data.Contains("<delegatemap>"));
        }

        [TestMethod]
        public void Default_English_IsInitialized()
        {
            Assert.IsNotNull(_default.English);
            Assert.AreEqual("english.xml", _default.English.FileName);
            Assert.IsNotNull(_default.English.Data);
            Assert.IsTrue(_default.English.Data.Contains(@"<locale name=""english"">"));
        }

        [TestMethod]
        public void Default_Locale_IsInitialized()
        {
            Assert.IsNotNull(_default.Locale);
            Assert.AreEqual("locale.xml", _default.Locale.FileName);
            Assert.IsNotNull(_default.Locale.Data);
            Assert.IsTrue(_default.Locale.Data.Contains(@"<localemap>"));
        }

        [TestMethod]
        public void Default_Log_IsInitialized()
        {
            Assert.IsNotNull(_default.Log);
            Assert.AreEqual("log.xml", _default.Log.FileName);
            Assert.IsNotNull(_default.Log.Data);
            Assert.IsTrue(_default.Log.Data.Contains(@"<logmap>"));
        }

        [TestMethod]
        public void Default_Magic_IsInitialized()
        {
            Assert.IsNotNull(_default.Magic);
            Assert.AreEqual("magic.xml", _default.Magic.FileName);
            Assert.IsNotNull(_default.Magic.Data);
            Assert.IsTrue(_default.Magic.Data.Contains(@"<magicmap>"));
        }

        [TestMethod]
        public void Default_Policy_IsInitialized()
        {
            Assert.IsNotNull(_default.Policy);
            Assert.AreEqual("policy.xml", _default.Policy.FileName);
            Assert.IsNotNull(_default.Policy.Data);
            Assert.IsTrue(_default.Policy.Data.Contains(@"<policymap>"));
        }

        [TestMethod]
        public void Default_Thresholds_IsInitialized()
        {
            Assert.IsNotNull(_default.Thresholds);
            Assert.AreEqual("thresholds.xml", _default.Thresholds.FileName);
            Assert.IsNotNull(_default.Thresholds.Data);
            Assert.IsTrue(_default.Thresholds.Data.Contains(@"<thresholds>"));
        }

        [TestMethod]
        public void Default_Type_IsInitialized()
        {
            Assert.IsNotNull(_default.Type);
            Assert.AreEqual("type.xml", _default.Type.FileName);
            Assert.IsNotNull(_default.Type.Data);
            Assert.IsTrue(_default.Type.Data.Contains(@"<typemap>"));
            Assert.IsTrue(_default.Type.Data.Contains(@"<include file="""));
        }

        [TestMethod]
        public void Default_TypeGhostscript_IsInitialized()
        {
            Assert.IsNotNull(_default.TypeGhostscript);
            Assert.AreEqual("type-ghostscript.xml", _default.TypeGhostscript.FileName);
            Assert.IsNotNull(_default.TypeGhostscript.Data);
            Assert.IsTrue(_default.TypeGhostscript.Data.Contains(@"<typemap>"));
            Assert.IsTrue(_default.TypeGhostscript.Data.Contains(@"<type name="""));
        }
    }
}