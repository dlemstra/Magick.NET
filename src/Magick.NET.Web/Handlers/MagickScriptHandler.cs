// Copyright 2013-2020 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using System.IO;
using System.Web;

namespace ImageMagick.Web.Handlers
{
    /// <summary>
    /// IHttpHandler that can be used to send a scripted image to the response.
    /// </summary>
    internal class MagickScriptHandler : ImageOptimizerHandler
    {
        private readonly IScriptData _scriptResolver;

        internal MagickScriptHandler(MagickWebSettings settings, IImageData imageData, IScriptData scriptResolver)
          : base(settings, imageData)
        {
            _scriptResolver = scriptResolver;
        }

        /// <inheritdoc/>
        protected override string GetFileName(HttpContext context)
        {
            string cacheFileName = GetCacheFileName();
            if (!CanUseCache(cacheFileName))
                CreateScriptedFile(cacheFileName);

            return cacheFileName;
        }

        protected override string GetMimeType()
        {
            MagickFormatInfo formatInfo = MagickNET.GetFormatInformation(_scriptResolver.OutputFormat);
            return formatInfo.MimeType;
        }

        private void CreateScriptedFile(string cacheFileName)
        {
            MagickScript script = new MagickScript(_scriptResolver.Script);
            script.Read += OnScriptRead;

            using (IMagickImage image = script.Execute())
            {
                image.Format = _scriptResolver.OutputFormat;
                WriteToCache(image, cacheFileName);
            }
        }

        private string GetCacheFileName()
        {
            string outerXml = _scriptResolver.Script.CreateNavigator().OuterXml;
            return GetCacheFileName("MagickScript", outerXml, _scriptResolver.OutputFormat);
        }

        private void OnScriptRead(object sender, ScriptReadEventArgs arguments)
        {
            arguments.Image = ImageData.ReadImage(arguments.ReadSettings);
        }

        private void WriteToCache(IMagickImage image, string cacheFileName)
        {
            string tempFile = DetermineTempFileName();

            try
            {
                image.Write(tempFile);

                MagickFormatInfo formatInfo = MagickNET.GetFormatInformation(_scriptResolver.OutputFormat);

                if (HandlerHelper.CanOptimize(Settings, formatInfo))
                    OptimizeFile(tempFile);

                MoveToCache(tempFile, cacheFileName);
            }
            finally
            {
                if (File.Exists(tempFile))
                    File.Delete(tempFile);
            }
        }
    }
}
