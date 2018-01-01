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

#if !NETCOREAPP1_1

using System;
using System.Configuration;
using System.IO;
using System.Linq;
using ImageMagick.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Magick.NET.Tests
{
    [TestClass]
    public class MagickWebSettingsTests
    {
        [TestMethod]
        public void Test_Exceptions()
        {
            ExceptionAssert.Throws<ConfigurationErrorsException>(() =>
            {
                MagickWebSettings settings = MagickWebSettings.Instance;
            });

            string config = @"
<magick.net.web canCreateDirectories=""false"" cacheDirectory=""c:\cache"">
  <urlResolvers>
    <urlResolver type=""Magick.NET.Tests.MagickWebSettingsTests, Magick.NET.Tests""/>
  </urlResolvers>
</magick.net.web>";

            ExceptionAssert.Throws<ConfigurationErrorsException>(() =>
            {
                TestSectionLoader.Load(config);
            });
        }

        [TestMethod]
        public void Test_Defaults()
        {
            using (TemporaryDirectory directory = new TemporaryDirectory())
            {
                string tempDir = directory.FullName + "\\";

                string config = $@"<magick.net.web cacheDirectory=""{tempDir}""/>";

                MagickWebSettings settings = TestSectionLoader.Load(config);
                Assert.AreEqual(tempDir, settings.CacheDirectory);
                Assert.IsTrue(settings.CanCreateDirectories);
                Assert.AreEqual(new TimeSpan(1, 0, 0, 0), settings.ClientCache.CacheControlMaxAge);
                Assert.AreEqual(CacheControlMode.UseMaxAge, settings.ClientCache.CacheControlMode);
                Assert.IsTrue(settings.EnableGzip);
                Assert.IsTrue(settings.Optimization.IsEnabled);
                Assert.IsTrue(settings.Optimization.Lossless);
                Assert.IsFalse(settings.Optimization.OptimalCompression);
                Assert.IsNull(settings.ResourceLimits.Height);
                Assert.IsNull(settings.ResourceLimits.Width);
                Assert.IsFalse(settings.ShowVersion);
                Assert.AreEqual(Path.GetTempPath(), settings.TempDirectory);
                Assert.IsFalse(settings.UseOpenCL);

                var urlResolverSettings = settings.UrlResolvers.Cast<UrlResolverSettings>();
                Assert.AreEqual(0, urlResolverSettings.Count());

                Assert.IsTrue(Directory.Exists(tempDir));

                settings = TestSectionLoader.Load(config);
            }
        }

        [TestMethod]
        public void Test_Properties()
        {
            string config = @"
<magick.net.web canCreateDirectories=""false"" cacheDirectory=""~\cache"" tempDirectory=""c:\temp\"" enableGzip=""false"" showVersion=""true"" useOpenCL=""true"">
  <clientCache cacheControlMaxAge=""4:2:0"" cacheControlMode=""NoControl""/>
  <optimization enabled=""false"" lossless=""false"" optimalCompression=""true""/>
  <resourceLimits width=""1"" height=""2""/>
  <urlResolvers>
    <urlResolver type=""Magick.NET.Tests.TestFileUrlResolver, Magick.NET.Tests""/>
  </urlResolvers>
</magick.net.web>";

            MagickWebSettings settings = TestSectionLoader.Load(config);
            Assert.IsTrue(settings.CacheDirectory.EndsWith(@"\cache\"));
            Assert.IsFalse(settings.CanCreateDirectories);
            Assert.AreEqual(new TimeSpan(4, 2, 0), settings.ClientCache.CacheControlMaxAge);
            Assert.AreEqual(CacheControlMode.NoControl, settings.ClientCache.CacheControlMode);
            Assert.IsFalse(settings.EnableGzip);
            Assert.IsFalse(settings.Optimization.IsEnabled);
            Assert.IsFalse(settings.Optimization.Lossless);
            Assert.IsTrue(settings.Optimization.OptimalCompression);
            Assert.AreEqual(1, settings.ResourceLimits.Width);
            Assert.AreEqual(2, settings.ResourceLimits.Height);
            Assert.IsTrue(settings.ShowVersion);
            Assert.AreEqual(@"c:\temp\", settings.TempDirectory);
            Assert.IsTrue(settings.UseOpenCL);

            var urlResolverSettings = settings.UrlResolvers.Cast<UrlResolverSettings>();
            Assert.AreEqual(1, urlResolverSettings.Count());

            IUrlResolver urlResolver = urlResolverSettings.First().CreateInstance();
            Assert.AreEqual(urlResolver.GetType(), typeof(TestFileUrlResolver));
        }
    }
}

#endif