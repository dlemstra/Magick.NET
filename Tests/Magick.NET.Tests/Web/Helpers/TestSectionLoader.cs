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
using System.Configuration;
using System.IO;

namespace Magick.NET.Tests
{
  [ExcludeFromCodeCoverage]
  public sealed class TestSectionLoader : ISectionLoader
  {
    private string _TempFile;

    private string CreateConfig(string config)
    {
#if Q8
#if PLATFORM_x64
      string libraryName = "Magick.NET.Web-Q8-x64";
#elif PLATFORM_AnyCPU
      string libraryName = "Magick.NET.Web-Q8-AnyCPU";
#else
      string libraryName = "Magick.NET.Web-Q8-x86";
#endif
#elif Q16
#if PLATFORM_x64
      string libraryName = "Magick.NET.Web-Q16-x64";
#elif PLATFORM_AnyCPU
      string libraryName = "Magick.NET.Web-Q16-AnyCPU";
#else
      string libraryName = "Magick.NET.Web-Q16-x86";
#endif
#elif Q16HDRI
#if PLATFORM_x64
      string libraryName = "Magick.NET.Web-Q16-HDRI-x64";
#elif PLATFORM_AnyCPU
      string libraryName = "Magick.NET.Web-Q16-HDRI-AnyCPU";
#else
      string libraryName = "Magick.NET.Web-Q16-HDRI-x86";
#endif
#else
#error Not implemented!
#endif

      return $@"<?xml version=""1.0"" encoding=""utf-8"" ?>
<configuration>
  <configSections>
    <section name=""magick.net.web"" type=""ImageMagick.Web.MagickWebSettings, {libraryName}""/>
  </configSections>
  {config}
</configuration>
";
    }

    private MagickWebSettings LoadSettings(string config)
    {
      config = CreateConfig(config);

      _TempFile = Path.GetTempFileName();
      try
      {
        File.WriteAllText(_TempFile, config);

        MagickWebSettings settings = MagickWebSettings.CreateInstance(this);
        Assert.IsNotNull(settings);

        return settings;
      }
      finally
      {
        if (File.Exists(_TempFile))
          File.Delete(_TempFile);
      }
    }

    MagickWebSettings ISectionLoader.GetSection(string name)
    {
      ExeConfigurationFileMap map = new ExeConfigurationFileMap();
      map.ExeConfigFilename = _TempFile;
      Configuration config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);

      return config.GetSection(name) as MagickWebSettings;
    }

    public static MagickWebSettings Load(string config)
    {
      TestSectionLoader sectionLoader = new TestSectionLoader();
      return sectionLoader.LoadSettings(config);
    }
  }
}
