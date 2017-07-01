// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   http://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using System.Diagnostics;
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
                        using (Stream input = ImageData.ReadImage())
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

            Debug.Assert(encoding == "deflate");
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

        internal GzipHandler(MagickWebSettings settings, IImageData imageData)
          : base(settings, imageData)
        {
        }

        /// <inheritdoc/>
        [SuppressMessage("Microsoft.Design", "CA1062:Validate arguments of public methods", MessageId = "0", Justification = "Context will not be null.")]
        protected override string GetFileName(HttpContext context)
        {
            Debug.Assert(context != null);

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
    }
}
