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

using System;
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
    private static Lazy<MagickWebSettings> _instance = new Lazy<MagickWebSettings>(CreateInstance);

    private static MagickWebSettings CreateInstance()
    {
      return CreateInstance(new SectionLoader());
    }

    private string GetDirectory(string directory)
    {
      string result = directory;

      if (result[0] == '~')
        result = Path.GetFullPath(HostingEnvironment.MapPath("~") + result.Substring(1));

      if (result[result.Length - 1] != '\\')
        result += "\\";

      if (CanCreateDirectories)
      {
        if (!Directory.Exists(result))
          Directory.CreateDirectory(result);
      }

      return result;
    }

    private string GetTempDirectory(string tempDirectory)
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

      CacheDirectory = GetDirectory(CacheDirectory);
      TempDirectory = GetTempDirectory(TempDirectory);
    }

    [SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "magick", Justification = "This is the correct spelling.")]
    internal static MagickWebSettings CreateInstance(ISectionLoader sectionLoader)
    {
      MagickWebSettings section = sectionLoader.GetSection("magick.net.web");

      if (section == null)
        throw new ConfigurationErrorsException("Unable to find section magick.net.web");

      return section;
    }

    internal static MagickWebSettings Instance
    {
      get
      {
        return _instance.Value;
      }
    }

    /// <summary>
    /// Gets the directory that contains scripted images.
    /// </summary>
    [ConfigurationProperty("cacheDirectory", IsRequired = true)]
    public string CacheDirectory
    {
      get
      {
        return (string)this["cacheDirectory"];
      }

      private set
      {
        this["cacheDirectory"] = value;
      }
    }

    /// <summary>
    /// Gets a value indicating whether the directories should be created when they do not exist.
    /// </summary>
    [ConfigurationProperty("canCreateDirectories", DefaultValue = true)]
    public bool CanCreateDirectories
    {
      get
      {
        return (bool)this["canCreateDirectories"];
      }
    }

    /// <summary>
    /// Gets the client cache settings.
    /// </summary>
    [ConfigurationProperty("clientCache")]
    public HttpClientCache ClientCache
    {
      get
      {
        return (HttpClientCache)this["clientCache"];
      }
    }

    /// <summary>
    /// Gets a value indicating whether the gzip compression should be enabled.
    /// </summary>
    [ConfigurationProperty("enableGzip", DefaultValue = true)]
    public bool EnableGzip
    {
      get
      {
        return (bool)this["enableGzip"];
      }
    }

    /// <summary>
    /// Gets a value indicating whether the images should be optimized.
    /// </summary>
    [ConfigurationProperty("optimizeImages", DefaultValue = true)]
    public bool OptimizeImages
    {
      get
      {
        return (bool)this["optimizeImages"];
      }
    }

    /// <summary>
    /// Gets the settings for the resource limits
    /// </summary>
    [ConfigurationProperty("resourceLimits")]
    public ResourceLimitsSettings ResourceLimits
    {
      get
      {
        return (ResourceLimitsSettings)this["resourceLimits"];
      }
    }

    /// <summary>
    /// Gets a value indicating whether the version can be shown in the http headers.
    /// </summary>
    [ConfigurationProperty("showVersion", DefaultValue = false)]
    public bool ShowVersion
    {
      get
      {
        return (bool)this["showVersion"];
      }
    }

    /// <summary>
    /// Gets the directory that will be used to store temporary files.
    /// </summary>
    [ConfigurationProperty("tempDirectory")]
    public string TempDirectory
    {
      get
      {
        return (string)this["tempDirectory"];
      }

      private set
      {
        this["tempDirectory"] = value;
      }
    }

    /// <summary>
    /// Gets a value indicating whether OpenCL acceleration should be used.
    /// </summary>
    [ConfigurationProperty("useOpenCL", DefaultValue = false)]
    public bool UseOpenCL
    {
      get
      {
        return (bool)this["useOpenCL"];
      }
    }

    /// <summary>
    /// Gets the url resolvers.
    /// </summary>
    [ConfigurationProperty("urlResolvers")]
    public UrlResolverSettingsCollection UrlResolvers
    {
      get
      {
        return (UrlResolverSettingsCollection)this["urlResolvers"];
      }
    }
  }
}
