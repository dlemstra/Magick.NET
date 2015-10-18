//=================================================================================================
// Copyright 2013-2015 Dirk Lemstra <https://magick.codeplex.com/>
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

using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Web.Hosting;

namespace ImageMagick.Web
{
  /// <summary>
  /// Class that contains the settings for Magick.NET.Web.
  /// </summary>
  public sealed class MagickWebSettings : ConfigurationSection
  {
    [ConfigurationProperty("cacheDirectory", IsRequired = true)]
    private string _CacheDirectory
    {
      get
      {
        return (string)this["cacheDirectory"];
      }
      set
      {
        this["cacheDirectory"] = value;
      }
    }

    [ConfigurationProperty("clientCache")]
    private HttpClientCache _ClientCache
    {
      get
      {
        return (HttpClientCache)this["clientCache"];
      }
    }

    [ConfigurationProperty("enableGzip", DefaultValue = true)]
    private bool _EnableGzip
    {
      get
      {
        return (bool)this["enableGzip"];
      }
    }

    [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "magick")]
    private static MagickWebSettings _Instance
    {
      get
      {
        MagickWebSettings section = ConfigurationManager.GetSection("magick.net.web") as MagickWebSettings;
        if (section == null)
          throw new ConfigurationErrorsException("Unable to find section magick.net.web");

        return section;
      }
    }

    [ConfigurationProperty("optimizeImages", DefaultValue = true)]
    private bool _OptimizeImages
    {
      get
      {
        return (bool)this["optimizeImages"];
      }
    }

    [ConfigurationProperty("showVersion", DefaultValue = false)]
    private bool _ShowVersion
    {
      get
      {
        return (bool)this["showVersion"];
      }
    }

    [ConfigurationProperty("tempDirectory")]
    private string _TempDirectory
    {
      get
      {
        return (string)this["tempDirectory"];
      }
      set
      {
        this["tempDirectory"] = value;
      }
    }

    [ConfigurationProperty("useOpenCL", DefaultValue = false)]
    private bool _UseOpenCL
    {
      get
      {
        return (bool)this["useOpenCL"];
      }
    }

    [ConfigurationProperty("urlResolvers")]
    private UrlResolverSettingsCollection _UrlResolvers
    {
      get
      {
        return (UrlResolverSettingsCollection)this["urlResolvers"];
      }
    }

    private static string GetDirectory(string directory)
    {
      string result = directory;

      if (result[0] == '~')
        result = Path.GetFullPath(HostingEnvironment.MapPath("~") + result.Substring(1));

      if (result[result.Length - 1] != '\\')
        result += "\\";

      if (!Directory.Exists(result))
        Directory.CreateDirectory(result);

      return result;
    }

    private static string GetTempDirectory(string tempDirectory)
    {
      if (string.IsNullOrEmpty(tempDirectory))
        return Path.GetTempPath();

      return GetDirectory(tempDirectory);
    }

    /// <summary>
    /// Called after deserialization.
    /// </summary>
    protected override void PostDeserialize()
    {
      base.PostDeserialize();

      _CacheDirectory = GetDirectory(_CacheDirectory);
      _TempDirectory = GetTempDirectory(_TempDirectory);
    }

    /// <summary>
    /// Returns the directory that contains scripted images.
    /// </summary>
    public static string CacheDirectory
    {
      get
      {
        return _Instance._CacheDirectory;
      }
    }

    /// <summary>
    /// Returns the client cache settings.
    /// </summary>
    public static HttpClientCache ClientCache
    {
      get
      {
        return _Instance._ClientCache;
      }
    }

    /// <summary>
    /// Returns true if the gzip compression should be enabled.
    /// </summary>
    public static bool EnableGzip
    {
      get
      {
        return _Instance._EnableGzip;
      }
    }

    /// <summary>
    /// Returns true if the images should be optimized.
    /// </summary>
    public static bool OptimizeImages
    {
      get
      {
        return _Instance._OptimizeImages;
      }
    }

    /// <summary>
    /// Returns true if the version can be shown in the http headers.
    /// </summary>
    public static bool ShowVersion
    {
      get
      {
        return _Instance._ShowVersion;
      }
    }

    /// <summary>
    /// Returns the directory that will be used to store temporary files.
    /// </summary>
    public static string TempDirectory
    {
      get
      {
        return _Instance._TempDirectory;
      }
    }

    /// <summary>
    /// Returns true if OpenCL acceleration should be used.
    /// </summary>
    public static bool UseOpenCL
    {
      get
      {
        return _Instance._UseOpenCL;
      }
    }

    /// <summary>
    /// Returns the url resolvers.
    /// </summary>
    public static UrlResolverSettingsCollection UrlResolvers
    {
      get
      {
        return _Instance._UrlResolvers;
      }
    }
  }
}
