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
    private string BaseCacheDirectory
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
    private HttpClientCache BaseClientCache
    {
      get
      {
        return (HttpClientCache)this["clientCache"];
      }
    }

    [ConfigurationProperty("enableGzip", DefaultValue = true)]
    private bool BaseEnableGzip
    {
      get
      {
        return (bool)this["enableGzip"];
      }
    }

    [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "magick", Justification = "This is the correct spelling.")]
    private static MagickWebSettings Instance
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
    private bool BaseOptimizeImages
    {
      get
      {
        return (bool)this["optimizeImages"];
      }
    }

    [ConfigurationProperty("resourcelimits")]
    private ResourceLimitsSettings BaseResourceLimits
    {
      get
      {
        return (ResourceLimitsSettings)this["resourcelimits"];
      }
    }

    [ConfigurationProperty("showVersion", DefaultValue = false)]
    private bool BaseShowVersion
    {
      get
      {
        return (bool)this["showVersion"];
      }
    }

    [ConfigurationProperty("tempDirectory")]
    private string BaseTempDirectory
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
    private bool BaseUseOpenCL
    {
      get
      {
        return (bool)this["useOpenCL"];
      }
    }

    [ConfigurationProperty("urlResolvers")]
    private UrlResolverSettingsCollection BaseUrlResolvers
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

      BaseCacheDirectory = GetDirectory(BaseCacheDirectory);
      BaseTempDirectory = GetTempDirectory(BaseTempDirectory);
    }

    /// <summary>
    /// Gets the directory that contains scripted images.
    /// </summary>
    public static string CacheDirectory
    {
      get
      {
        return Instance.BaseCacheDirectory;
      }
    }

    /// <summary>
    /// Gets the client cache settings.
    /// </summary>
    public static HttpClientCache ClientCache
    {
      get
      {
        return Instance.BaseClientCache;
      }
    }

    /// <summary>
    /// Gets a value indicating whether the gzip compression should be enabled.
    /// </summary>
    public static bool EnableGzip
    {
      get
      {
        return Instance.BaseEnableGzip;
      }
    }

    /// <summary>
    /// Gets a value indicating whether the images should be optimized.
    /// </summary>
    public static bool OptimizeImages
    {
      get
      {
        return Instance.BaseOptimizeImages;
      }
    }

    /// <summary>
    /// Gets the settings for the resource limits
    /// </summary>
    public static ResourceLimitsSettings ResourceLimits
    {
      get
      {
        return Instance.BaseResourceLimits;
      }
    }

    /// <summary>
    /// Gets a value indicating whether the version can be shown in the http headers.
    /// </summary>
    public static bool ShowVersion
    {
      get
      {
        return Instance.BaseShowVersion;
      }
    }

    /// <summary>
    /// Gets the directory that will be used to store temporary files.
    /// </summary>
    public static string TempDirectory
    {
      get
      {
        return Instance.BaseTempDirectory;
      }
    }

    /// <summary>
    /// Gets a value indicating whether OpenCL acceleration should be used.
    /// </summary>
    public static bool UseOpenCL
    {
      get
      {
        return Instance.BaseUseOpenCL;
      }
    }

    /// <summary>
    /// Gets the url resolvers.
    /// </summary>
    public static UrlResolverSettingsCollection UrlResolvers
    {
      get
      {
        return Instance.BaseUrlResolvers;
      }
    }
  }
}
