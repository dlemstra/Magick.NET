// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

namespace ImageMagick.Formats.Pdf
{
    /// <summary>
    /// The info of a <see cref="MagickFormat.Pdf"/> file.
    /// </summary>
    public sealed partial class PdfInfo
    {
        private PdfInfo(int pageCount)
        {
            PageCount = pageCount;
        }

        /// <summary>
        /// Gets the page count of the file.
        /// </summary>
        public int PageCount { get; }

        /// <summary>
        /// Creates info from a <see cref="MagickFormat.Pdf"/> file.
        /// </summary>
        /// <param name="file">The pdf file to create the info from.</param>
        /// <returns>The info of a <see cref="MagickFormat.Pdf"/> file.</returns>
        public static PdfInfo Create(FileInfo file)
        {
            Throw.IfNull(nameof(file), file);
            return Create(file.FullName);
        }

        /// <summary>
        /// Creates info from a <see cref="MagickFormat.Pdf"/> file.
        /// </summary>
        /// <param name="fileName">The pdf file to create the info from.</param>
        /// <returns>The info of a <see cref="MagickFormat.Pdf"/> file.</returns>
        public static PdfInfo Create(string fileName)
        {
            var filePath = FileHelper.CheckForBaseDirectory(fileName);
            Throw.IfNullOrEmpty(nameof(fileName), filePath);
            filePath = filePath.Replace('\\', '/');

            var nativePdfInfo = new NativePdfInfo();
            var pageCount = nativePdfInfo.PageCount(filePath);

            return new PdfInfo(pageCount);
        }
    }
}
