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
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.SessionState;

namespace ImageMagick.Web.Handlers
{
  /// <summary>
  /// Base class for IHttpHandlers that use the IUrlResolver class.
  /// </summary>
  public abstract class MagickHandler : IHttpHandler, IRequiresSessionState
  {
    private static readonly ReaderWriterLockSlim _Lock = new ReaderWriterLockSlim();
    private static volatile string _Version;

    private void AddCacheControlHeader(HttpResponse response)
    {
      if (Settings.ClientCache.CacheControlMode == CacheControlMode.NoControl)
        return;

      response.Cache.SetMaxAge(Settings.ClientCache.CacheControlMaxAge);
      response.Cache.SetCacheability(HttpCacheability.Public);
    }

    private static string CalculateMD5(string value)
    {
      using (MD5 md5 = MD5.Create())
      {
        byte[] data = md5.ComputeHash(Encoding.Default.GetBytes(value));

        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < data.Length; i++)
        {
          sb.Append(data[i].ToString("X2", CultureInfo.InvariantCulture));
        }

        return sb.ToString();
      }
    }

    private void InitializeVersion()
    {
      if (!Settings.ShowVersion || _Version != null)
        return;

      object version = typeof(MagickHandler).Assembly.GetCustomAttributes(typeof(AssemblyFileVersionAttribute), false)[0];
      _Version = ((AssemblyFileVersionAttribute)version).Version;
    }

    private static bool Write304(HttpContext content, DateTime fileDate)
    {
      DateTime modificationDate = new DateTime(fileDate.Year, fileDate.Month, fileDate.Day, fileDate.Hour, fileDate.Minute, fileDate.Second);
      if (modificationDate > DateTime.Now)
        modificationDate = DateTime.Now;

      modificationDate = modificationDate.ToUniversalTime();

      content.Response.Cache.SetLastModified(modificationDate);
      string modifiedSince = content.Request.Headers["If-Modified-Since"];

      if (string.IsNullOrEmpty(modifiedSince))
        return false;

      string since;
      int index = modifiedSince.IndexOf(";", StringComparison.OrdinalIgnoreCase);

      if (index >= 0)
        since = modifiedSince.Substring(0, index);
      else
        since = modifiedSince;

      DateTime modifiedDate;
      bool success = DateTime.TryParseExact(since, "r", CultureInfo.InvariantCulture, DateTimeStyles.None, out modifiedDate);

      if (success && modifiedDate == modificationDate)
      {
        content.Response.StatusCode = 304;
        return true;
      }

      return false;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="MagickHandler"/> class.
    /// </summary>
    /// <param name="settings">The settings to use.</param>
    /// <param name="urlResolver">The url resolver that was used.</param>
    /// <param name="formatInfo">The format information.</param>
    protected MagickHandler(MagickWebSettings settings, IUrlResolver urlResolver, MagickFormatInfo formatInfo)
    {
      Settings = settings;
      UrlResolver = urlResolver;
      FormatInfo = formatInfo;

      InitializeVersion();
    }

    /// <summary>
    /// Writes the file to the response.
    /// </summary>
    /// <param name="context">An HttpContext object that provides references to the intrinsic
    /// server objects (for example, Request, Response, Session, and Server) used to service
    /// HTTP requests.</param>
    protected abstract void WriteFile(HttpContext context);

    /// <summary>
    /// Gets the format information for the file that was resolved with the IUrlResolver.
    /// </summary>
    protected MagickFormatInfo FormatInfo
    {
      get;
    }

    /// <summary>
    /// Gets the IUrlResolver that was used.
    /// </summary>
    protected IUrlResolver UrlResolver
    {
      get;
    }

    /// <summary>
    /// Gets the settings that should be used.
    /// </summary>
    protected MagickWebSettings Settings
    {
      get;
    }

    /// <summary>
    /// Returns true if the cache file is newer then the file name that was resolved by the
    /// IUrlResolver.
    /// </summary>
    /// <param name="cacheFileName">The file name of the cache file.</param>
    /// <returns>True if the cache file is newer</returns>
    protected bool CanUseCache(string cacheFileName)
    {
      _Lock.EnterReadLock();

      try
      {
        if (!File.Exists(cacheFileName))
          return false;

        DateTime fileDate = File.GetLastWriteTime(UrlResolver.FileName);
        DateTime cacheDate = File.GetLastWriteTime(cacheFileName);
        return fileDate <= cacheDate;
      }
      finally
      {
        _Lock.ExitReadLock();
      }
    }

    /// <summary>
    /// Returns the file name that can be used to cache the result.
    /// </summary>
    /// <param name="directoryName">The name of the subdirectory to store the files in.</param>
    /// <param name="subdirectoryKey">The key that will be used to create MD5 hash and that
    /// will be used as a sub directory.</param>
    /// <returns>The file name that can be used to cache the result.</returns>
    protected string GetCacheFileName(string directoryName, string subdirectoryKey)
    {
      string cacheDirectory = Settings.CacheDirectory + directoryName + "\\" + CalculateMD5(subdirectoryKey) + "\\";

      if (!Directory.Exists(cacheDirectory))
        Directory.CreateDirectory(cacheDirectory);

      return cacheDirectory + CalculateMD5(UrlResolver.FileName) + "." + UrlResolver.Format;
    }

    /// <summary>
    /// Returns the file name for a temporary file.
    /// </summary>
    /// <returns>The file name for a temporary file.</returns>
    protected string DetermineTempFileName()
    {
      return Settings.TempDirectory + Guid.NewGuid();
    }

    /// <summary>
    /// Moves to the specified source file name to the destination file name. This is happening
    /// in a lock to avoid problems when an other request is reading the file.
    /// </summary>
    /// <param name="fileName">The name of the file to move to the cache.</param>
    /// <param name="cacheFileName">The file name of the cache file.</param>
    protected static void MoveToCache(string fileName, string cacheFileName)
    {
      try
      {
        _Lock.EnterWriteLock();

        if (File.Exists(cacheFileName))
          File.Delete(cacheFileName);

        File.Move(fileName, cacheFileName);
      }
      finally
      {
        _Lock.ExitWriteLock();
      }
    }

    /// <summary>
    /// Writes the specified file to the response.
    /// </summary>
    /// <param name="context">An HttpContext object that provides references to the intrinsic
    /// server objects (for example, Request, Response, Session, and Server) used to service
    /// HTTP requests.</param>
    /// <param name="fileName">The file name of the file to write to the response.</param>
    protected void WriteFile(HttpContext context, string fileName)
    {
      if (context == null || string.IsNullOrEmpty(fileName))
        return;

      _Lock.EnterReadLock();

      try
      {
        AddCacheControlHeader(context.Response);

        if (Write304(context, File.GetLastWriteTime(fileName)))
          return;

        context.Response.TransmitFile(fileName);
      }
      finally
      {
        _Lock.ExitReadLock();
      }
    }

    /// <summary>
    /// Gets a value indicating whether another request can use the IHttpHandler instance.
    /// </summary>
    public bool IsReusable
    {
      get
      {
        return false;
      }
    }

    /// <summary>
    /// Enables processing of HTTP Web requests by a custom HttpHandler that implements the
    /// IHttpHandler interface.
    /// </summary>
    /// <param name="context">An HttpContext object that provides references to the intrinsic
    /// server objects (for example, Request, Response, Session, and Server) used to service
    /// HTTP requests.</param>
    public void ProcessRequest(HttpContext context)
    {
      if (context == null)
        return;

      context.Response.ContentType = FormatInfo.MimeType;

      if (!string.IsNullOrEmpty(_Version))
        context.Response.AddHeader("X-Magick", _Version);

      WriteFile(context);
    }
  }
}
