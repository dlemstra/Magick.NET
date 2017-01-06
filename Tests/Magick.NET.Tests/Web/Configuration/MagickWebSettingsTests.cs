//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://magick.codeplex.com/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   http://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
// express or implied. See the License for the specific language governing permissions and
// limitations under the License.
//=================================================================================================

using ImageMagick.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Configuration;
using System.IO;
using System.Linq;

namespace Magick.NET.Tests
{
  [TestClass]
  public class MagickWebSettingsTests
  {
    private MagickWebSettings LoadSettings(string config)
    {
      string tempFile = Path.GetTempFileName();
      try
      {
        File.WriteAllText(tempFile, config);

        ExeConfigurationFileMap map = new ExeConfigurationFileMap();
        map.ExeConfigFilename = tempFile;
        Configuration exeConfig = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);

        MagickWebSettings settings = exeConfig.GetSection("magick.net.web") as MagickWebSettings;
        Assert.IsNotNull(settings);

        return settings;
      }
      finally
      {
        if (File.Exists(tempFile))
          File.Delete(tempFile);
      }
    }

    [TestMethod]
    public void Test_Exceptions()
    {
      ExceptionAssert.Throws<ConfigurationErrorsException>(() =>
      {
        MagickWebSettings settings = MagickWebSettings.Instance;
      });


      string config = $@"<?xml version=""1.0"" encoding=""utf-8"" ?>
<configuration>
  <configSections>
    <section name=""magick.net.web"" type=""ImageMagick.Web.MagickWebSettings, Magick.NET.Web-Q8-x86""/>
  </configSections>
  <magick.net.web canCreateDirectories=""false"" cacheDirectory=""c:\cache"">
    <urlResolvers>
      <urlResolver type=""Magick.NET.Tests.MagickWebSettingsTests, Magick.NET.Tests""/>
    </urlResolvers>
  </magick.net.web>
</configuration>";

      ExceptionAssert.Throws<ConfigurationErrorsException>(() =>
      {
        LoadSettings(config);
      });
    }

    [TestMethod]
    public void Test_Defaults()
    {
#if !Q8 || WIN64 || ANYCPU
      Assert.Inconclusive("Only testing this with the Q8-x86 build.");
#endif

      string tempDir = Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString()) + "\\";

      try
      {
        string config = $@"<?xml version=""1.0"" encoding=""utf-8"" ?>
<configuration>
  <configSections>
    <section name=""magick.net.web"" type=""ImageMagick.Web.MagickWebSettings, Magick.NET.Web-Q8-x86""/>
  </configSections>
  <magick.net.web cacheDirectory=""{tempDir}""/>
</configuration>";

        MagickWebSettings settings = LoadSettings(config);
        Assert.AreEqual(tempDir, settings.CacheDirectory);
        Assert.IsTrue(settings.CanCreateDirectories);
        Assert.AreEqual(new TimeSpan(1, 0, 0, 0), settings.ClientCache.CacheControlMaxAge);
        Assert.AreEqual(CacheControlMode.UseMaxAge, settings.ClientCache.CacheControlMode);
        Assert.IsTrue(settings.EnableGzip);
        Assert.IsTrue(settings.OptimizeImages);
        Assert.IsNull(settings.ResourceLimits.Height);
        Assert.IsNull(settings.ResourceLimits.Width);
        Assert.IsFalse(settings.ShowVersion);
        Assert.AreEqual(Path.GetTempPath(), settings.TempDirectory);
        Assert.IsFalse(settings.UseOpenCL);

        var urlResolverSettings = settings.UrlResolvers.Cast<UrlResolverSettings>();
        Assert.AreEqual(0, urlResolverSettings.Count());

        Assert.IsTrue(Directory.Exists(tempDir));
      }
      finally
      {
        if (Directory.Exists(tempDir))
          Directory.Delete(tempDir);
      }
    }

    [TestMethod]
    public void Test_Properties()
    {
#if !Q8 || WIN64 || ANYCPU
      Assert.Inconclusive("Only testing this with the Q8-x86 build.");
#endif

      string config = @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<configuration>
  <configSections>
    <section name=""magick.net.web"" type=""ImageMagick.Web.MagickWebSettings, Magick.NET.Web-Q8-x86""/>
  </configSections>
  <magick.net.web canCreateDirectories=""false"" cacheDirectory=""~\cache"" tempDirectory=""c:\temp\"" enableGzip=""false"" optimizeImages=""false"" showVersion=""true"" useOpenCL=""true"">
    <clientCache cacheControlMaxAge=""4:2:0"" cacheControlMode=""NoControl""/>
    <resourceLimits width=""1"" height=""2""/>
    <urlResolvers>
      <urlResolver type=""Magick.NET.Tests.TestUrlResolver, Magick.NET.Tests""/>
    </urlResolvers>
  </magick.net.web>
</configuration>";

      MagickWebSettings settings = LoadSettings(config);
      Assert.IsTrue(settings.CacheDirectory.EndsWith(@"\cache\"));
      Assert.IsFalse(settings.CanCreateDirectories);
      Assert.AreEqual(new TimeSpan(4, 2, 0), settings.ClientCache.CacheControlMaxAge);
      Assert.AreEqual(CacheControlMode.NoControl, settings.ClientCache.CacheControlMode);
      Assert.IsFalse(settings.EnableGzip);
      Assert.IsFalse(settings.OptimizeImages);
      Assert.AreEqual(1, settings.ResourceLimits.Width);
      Assert.AreEqual(2, settings.ResourceLimits.Height);
      Assert.IsTrue(settings.ShowVersion);
      Assert.AreEqual(@"c:\temp\", settings.TempDirectory);
      Assert.IsTrue(settings.UseOpenCL);

      var urlResolverSettings = settings.UrlResolvers.Cast<UrlResolverSettings>();
      Assert.AreEqual(1, urlResolverSettings.Count());

      IUrlResolver urlResolver = urlResolverSettings.First().CreateInstance();
      Assert.AreEqual(urlResolver.GetType(), typeof(TestUrlResolver));
    }
  }
}
