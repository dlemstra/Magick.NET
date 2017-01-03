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
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.IO.Compression;
using System.Threading;
using System.Web;

namespace ImageMagick.Web.Handlers
{
  /// <summary>
  /// IHttpHandler that can be used to compress files before they are written to the response.
  /// </summary>
  public class GzipHandler : MagickHandler
  {
    private string GetCompressedFileName(HttpContext context)
    {
      string encoding = GetEncoding(context.Request);
      if (string.IsNullOrEmpty(encoding))
        return UrlResolver.FileName;

      string cacheFileName = GetCacheFileName("Compressed", encoding);
      if (!CanUseCache(cacheFileName))
        CreateCompressedFile(encoding, cacheFileName);

      context.Response.AppendHeader("Content-Encoding", encoding);
      context.Response.AppendHeader("Vary", "Accept-Encoding");

      return cacheFileName;
    }

    [SuppressMessage("Microsoft.Usage", "CA2202:Do not dispose objects multiple times", Justification = "Code is much cleaner this way.")]
    private void CreateCompressedFile(string encoding, string cacheFileName)
    {
      string tempFile = DetermineTempFileName();

      try
      {
        using (FileStream fs = File.Create(tempFile))
        {
          using (Stream output = CreateCompressStream(fs, encoding))
          {
            using (FileStream input = File.OpenRead(UrlResolver.FileName))
            {
              byte[] buffer = new byte[81920];
              int len;

              while ((len = input.Read(buffer, 0, buffer.Length)) > 0)
                output.Write(buffer, 0, len);
            }
          }
        }

        MoveToCache(tempFile, cacheFileName);
      }
      finally
      {
        if (File.Exists(tempFile))
          File.Delete(tempFile);
      }
    }

    private static Stream CreateCompressStream(FileStream stream, string encoding)
    {
      if (encoding == "gzip")
        return new GZipStream(stream, CompressionMode.Compress);

      if (encoding == "deflate")
        return new DeflateStream(stream, CompressionMode.Compress);

      throw new NotSupportedException(encoding);
    }

    private static string GetEncoding(HttpRequest request)
    {
      try
      {
        string encoding = request.Headers["Accept-Encoding"];
        if (string.IsNullOrEmpty(encoding))
          return null;

        if (encoding.Contains("gzip"))
          return "gzip";

        if (encoding.Contains("deflate"))
          return "deflate";

        return null;
      }
      catch (ThreadAbortException)
      {
        return null;
      }
    }

    internal GzipHandler(MagickWebSettings settings, IUrlResolver urlResolver, MagickFormatInfo formatInfo)
      : base(settings, urlResolver, formatInfo)
    {
    }

    internal static bool CanCompress(MagickWebSettings settings, MagickFormatInfo formatInfo)
    {
      if (!settings.EnableGzip)
        return false;

      return formatInfo.Format == MagickFormat.Svg;
    }

    /// <summary>
    /// Writes the file to the response.
    /// </summary>
    /// <param name="context">An HttpContext object that provides references to the intrinsic
    /// server objects (for example, Request, Response, Session, and Server) used to service
    /// HTTP requests.</param>
    protected override void WriteFile(HttpContext context)
    {
      string fileName = GetCompressedFileName(context);
      WriteFile(context, fileName);
    }
  }
}
