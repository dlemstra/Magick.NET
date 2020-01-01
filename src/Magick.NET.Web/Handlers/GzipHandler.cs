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

using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.IO.Compression;
using System.Web;

namespace ImageMagick.Web.Handlers
{
    /// <summary>
    /// IHttpHandler that can be used to compress files before they are written to the response.
    /// </summary>
    internal sealed class GzipHandler : MagickHandler
    {
        internal GzipHandler(MagickWebSettings settings, IImageData imageData)
          : base(settings, imageData)
        {
        }

        /// <inheritdoc/>
        protected override string GetFileName(HttpContext context)
        {
            DebugThrow.IfNull(nameof(context), context);

            string encoding = GetEncoding(context.Request);
            if (string.IsNullOrEmpty(encoding))
                return null;

            string cacheFileName = GetCacheFileName("Compressed", encoding, ImageData.FormatInfo.Format);
            if (!CanUseCache(cacheFileName))
                CreateCompressedFile(encoding, cacheFileName);

            context.Response.AppendHeader("Content-Encoding", encoding);
            context.Response.AppendHeader("Vary", "Accept-Encoding");

            return cacheFileName;
        }

        private static Stream CreateCompressStream(FileStream stream, string encoding)
        {
            if (encoding == "gzip")
                return new GZipStream(stream, CompressionMode.Compress);

            DebugThrow.IfNotEqual("deflate", encoding);
            return new DeflateStream(stream, CompressionMode.Compress);
        }

        private static string GetEncoding(HttpRequest request)
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

        private void CreateCompressedFile(string encoding, string cacheFileName)
        {
            string tempFile = DetermineTempFileName();

            try
            {
                using (FileStream fs = File.Create(tempFile))
                {
                    using (Stream output = CreateCompressStream(fs, encoding))
                    {
                        using (Stream input = ImageData.ReadImage())
                        {
                            input.CopyTo(output);
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
    }
}
