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

    private void OnScriptRead(object sender, ScriptReadEventArgs arguments)
    {
      arguments.Image = new MagickImage(UrlResolver.FileName, arguments.Settings);
    }

    private void WriteToCache(MagickImage image, string cacheFileName)
    {
      string tempFile = DetermineTempFileName();

      try
      {
        image.Write(tempFile);

        MagickFormatInfo formatInfo = MagickNET.GetFormatInformation(UrlResolver.Format);

        if (CanOptimize(Settings, formatInfo))
          OptimizeFile(tempFile);

        MoveToCache(tempFile, cacheFileName);
      }
      finally
      {
        if (File.Exists(tempFile))
          File.Delete(tempFile);
      }
    }

    internal MagickScriptHandler(MagickWebSettings settings, IUrlResolver urlResolver, MagickFormatInfo formatInfo)
      : base(settings, urlResolver, formatInfo)
    {
    }

    /// <inheritdoc/>
    protected override string GetFileName(HttpContext context)
    {
      IXPathNavigable xml = UrlResolver.Script;

      string cacheFileName = GetCacheFileName(xml);
      if (!CanUseCache(cacheFileName))
        CreateScriptedFile(xml, cacheFileName);

      return cacheFileName;
    }
  }
}
