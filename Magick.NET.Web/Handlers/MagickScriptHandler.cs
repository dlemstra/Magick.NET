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
using System.Xml.XPath;

namespace ImageMagick.Web.Handlers
{
  /// <summary>
  /// IHttpHandler that can be used to send a scripted image to the response.
  /// </summary>
  public class MagickScriptHandler : ImageOptimizerHandler
  {

    private void CreateScriptedFile(IXPathNavigable xml, string cacheFileName)
    {
      MagickScript script = new MagickScript(xml);
      script.Read += OnScriptRead;

      using (MagickImage image = script.Execute())
      {
        image.Format = UrlResolver.Format;
        WriteToCache(image, cacheFileName);
      }
    }

    private string GetCacheFileName(IXPathNavigable xml)
    {
      return GetCacheFileName("MagickScript", xml.CreateNavigator().OuterXml);
    }

    private string GetScriptedFileName()
    {
      IXPathNavigable xml = UrlResolver.Script;

      string cacheFileName = GetCacheFileName(xml);
      if (!CanUseCache(cacheFileName))
        CreateScriptedFile(xml, cacheFileName);

      return cacheFileName;
    }

    private void OnScriptRead(object sender, ScriptReadEventArgs arguments)
    {
      arguments.Image = new MagickImage(UrlResolver.FileName, arguments.Settings);
    }

    private void WriteToCache(MagickImage image, string cacheFileName)
    {
      string tempFile = GetTempFileName();

      try
      {
        image.Write(tempFile);

        if (CanOptimize(FormatInfo))
          OptimizeFile(tempFile);

        MoveToCache(tempFile, cacheFileName);
      }
      finally
      {
        if (File.Exists(tempFile))
          File.Delete(tempFile);
      }
    }

    internal MagickScriptHandler(IUrlResolver urlResolver, MagickFormatInfo formatInfo)
      : base(urlResolver, formatInfo)
    {
    }

    /// <summary>
    /// Writes the file to the response.
    /// </summary>
    protected override void WriteFile(HttpContext context)
    {
      string fileName = GetScriptedFileName();
      WriteFile(context, fileName);
    }
  }
}
