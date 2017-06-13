//=================================================================================================
// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
    internal class ImageOptimizerHandler : MagickHandler
    {
        private readonly ImageOptimizer _ImageOptimizer;

        private void CreateOptimizedFile(string cacheFileName)
        {
            string tempFile = DetermineTempFileName();

            try
            {
                ImageData.SaveImage(tempFile);

                OptimizeFile(tempFile);

                MoveToCache(tempFile, cacheFileName);
            }
            finally
            {
                if (File.Exists(tempFile))
                    File.Delete(tempFile);
            }
        }

        /// <summary>
        /// Optimizes the specified file.
        /// </summary>
        /// <param name="fileName">The file name of the file to optimize.</param>
        protected void OptimizeFile(string fileName)
        {
            if (Settings.Optimization.Lossless)
                _ImageOptimizer.LosslessCompress(fileName);
            else
                _ImageOptimizer.Compress(fileName);
        }

        /// <inheritdoc/>
        protected override string GetFileName(HttpContext context)
        {
            MagickFormat format = ImageData.FormatInfo.Format;
            string cacheFileName = GetCacheFileName("Optimized", format.ToString(), format);

            if (!CanUseCache(cacheFileName))
                CreateOptimizedFile(cacheFileName);

            return cacheFileName;
        }

        public ImageOptimizerHandler(MagickWebSettings settings, IImageData imageData)
          : base(settings, imageData)
        {
            _ImageOptimizer = new ImageOptimizer();
            _ImageOptimizer.OptimalCompression = settings.Optimization.OptimalCompression;
        }
    }
}
