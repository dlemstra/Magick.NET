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
  internal sealed class TestSectionLoader : ISectionLoader
  {
    private string _tempFile;

    MagickWebSettings ISectionLoader.GetSection(string name)
    {
      ExeConfigurationFileMap map = new ExeConfigurationFileMap();
      map.ExeConfigFilename = _tempFile;
      Configuration config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);

      return config.GetSection(name) as MagickWebSettings;
    }

    public MagickWebSettings Load(string config)
    {
      _tempFile = Path.GetTempFileName();
      try
      {
        File.WriteAllText(_tempFile, config);

        MagickWebSettings settings = MagickWebSettings.CreateInstance(this);
        Assert.IsNotNull(settings);

        return settings;
      }
      finally
      {
        if (File.Exists(_tempFile))
          File.Delete(_tempFile);
      }
    }
  }
}
