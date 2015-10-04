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

using System.IO;
using System.Web;

namespace ImageMagick.Web.Handlers
{
  /// <summary>
  /// IHttpHandler that can be used to optimize image before they are written to the response.
  /// </summary>
  public class ImageOptimizerHandler : MagickHandler
  {

    private static ImageOptimizer _ImageOptimizer = new ImageOptimizer();

    private void CreateOptimizedFile(string cacheFileName)
    {
      string tempFile = GetTempFileName();
      try
      {
        File.Copy(UrlResolver.FileName, tempFile);

        OptimizeFile(tempFile);

        MoveToCache(tempFile, cacheFileName);
      }
      finally
      {
        if (File.Exists(tempFile))
          File.Delete(tempFile);
      }
    }

    private string GetOptimizedFileName()
    {
      string cacheFileName = GetCacheFileName("Optimized", UrlResolver.Format.ToString());

      if (!CanUseCache(cacheFileName))
        CreateOptimizedFile(cacheFileName);

      return cacheFileName;
    }

    internal ImageOptimizerHandler(IUrlResolver urlResolver, MagickFormatInfo formatInfo)
      : base(urlResolver, formatInfo)
    {
    }

    internal static bool CanOptimize(MagickFormatInfo formatInfo)
    {
      if (!MagickWebSettings.OptimizeImages)
        return false;

      return _ImageOptimizer.IsSupported(formatInfo);
    }

    /// <summary>
    /// Optimizes the specified file.
    /// </summary>
    protected static void OptimizeFile(string fileName)
    {
      _ImageOptimizer.LosslessCompress(fileName);
    }

    /// <summary>
    /// Writes the file to the response.
    /// </summary>
    protected override void WriteFile(HttpContext context)
    {
      string fileName = GetOptimizedFileName();
      WriteFile(context, fileName);
    }
  }
}
