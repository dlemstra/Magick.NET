//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
// express or implied. See the License for the specific language governing permissions and
// limitations under the License.
//=================================================================================================

using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;
using ImageMagick.Configuration;

namespace Magick.NET.Tests
{
    [TestClass]
    public class ConfigurationFilesTests
    {
        private ConfigurationFiles Default = ConfigurationFiles.Default;

        [TestMethod]
        public void Default_Coder_IsInitialized()
        {
            Assert.IsNotNull(Default.Coder);
            Assert.AreEqual("coder.xml", Default.Coder.FileName);
            Assert.IsNotNull(Default.Coder.Data);
            Assert.IsTrue(Default.Coder.Data.Contains("<codermap>"));
        }

        [TestMethod]
        public void Default_Colors_IsInitialized()
        {
            Assert.IsNotNull(Default.Colors);
            Assert.AreEqual("colors.xml", Default.Colors.FileName);
            Assert.IsNotNull(Default.Colors.Data);
            Assert.IsTrue(Default.Colors.Data.Contains("<colormap>"));
        }

        [TestMethod]
        public void Default_Configure_IsInitialized()
        {
            Assert.IsNotNull(Default.Configure);
            Assert.AreEqual("configure.xml", Default.Configure.FileName);
            Assert.IsNotNull(Default.Configure.Data);
            Assert.IsTrue(Default.Configure.Data.Contains("<configuremap>"));
        }

        [TestMethod]
        public void Default_Delegates_IsInitialized()
        {
            Assert.IsNotNull(Default.Delegates);
            Assert.AreEqual("delegates.xml", Default.Delegates.FileName);
            Assert.IsNotNull(Default.Delegates.Data);
            Assert.IsTrue(Default.Delegates.Data.Contains("<delegatemap>"));
        }

        [TestMethod]
        public void Default_English_IsInitialized()
        {
            Assert.IsNotNull(Default.English);
            Assert.AreEqual("english.xml", Default.English.FileName);
            Assert.IsNotNull(Default.English.Data);
            Assert.IsTrue(Default.English.Data.Contains(@"<locale name=""english"">"));
        }

        [TestMethod]
        public void Default_Locale_IsInitialized()
        {
            Assert.IsNotNull(Default.Locale);
            Assert.AreEqual("locale.xml", Default.Locale.FileName);
            Assert.IsNotNull(Default.Locale.Data);
            Assert.IsTrue(Default.Locale.Data.Contains(@"<localemap>"));
        }

        [TestMethod]
        public void Default_Log_IsInitialized()
        {
            Assert.IsNotNull(Default.Log);
            Assert.AreEqual("log.xml", Default.Log.FileName);
            Assert.IsNotNull(Default.Log.Data);
            Assert.IsTrue(Default.Log.Data.Contains(@"<logmap>"));
        }

        [TestMethod]
        public void Default_Magic_IsInitialized()
        {
            Assert.IsNotNull(Default.Magic);
            Assert.AreEqual("magic.xml", Default.Magic.FileName);
            Assert.IsNotNull(Default.Magic.Data);
            Assert.IsTrue(Default.Magic.Data.Contains(@"<magicmap>"));
        }

        [TestMethod]
        public void Default_Policy_IsInitialized()
        {
            Assert.IsNotNull(Default.Policy);
            Assert.AreEqual("policy.xml", Default.Policy.FileName);
            Assert.IsNotNull(Default.Policy.Data);
            Assert.IsTrue(Default.Policy.Data.Contains(@"<policymap>"));
        }

        [TestMethod]
        public void Default_Thresholds_IsInitialized()
        {
            Assert.IsNotNull(Default.Thresholds);
            Assert.AreEqual("thresholds.xml", Default.Thresholds.FileName);
            Assert.IsNotNull(Default.Thresholds.Data);
            Assert.IsTrue(Default.Thresholds.Data.Contains(@"<thresholds>"));
        }

        [TestMethod]
        public void Default_Type_IsInitialized()
        {
            Assert.IsNotNull(Default.Type);
            Assert.AreEqual("type.xml", Default.Type.FileName);
            Assert.IsNotNull(Default.Type.Data);
            Assert.IsTrue(Default.Type.Data.Contains(@"<typemap>"));
            Assert.IsTrue(Default.Type.Data.Contains(@"<include file="""));
        }

        [TestMethod]
        public void Default_TypeGhostscript_IsInitialized()
        {
            Assert.IsNotNull(Default.TypeGhostscript);
            Assert.AreEqual("type-ghostscript.xml", Default.TypeGhostscript.FileName);
            Assert.IsNotNull(Default.TypeGhostscript.Data);
            Assert.IsTrue(Default.TypeGhostscript.Data.Contains(@"<typemap>"));
            Assert.IsTrue(Default.TypeGhostscript.Data.Contains(@"<type name="""));
        }
    }
}