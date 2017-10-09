// Copyright 2013-2017 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace ImageMagick.ImageOptimizers
{
    /// <summary>
    /// Class that can be used to optimize png files.
    /// </summary>
    public sealed class PngOptimizer : IImageOptimizer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PngOptimizer"/> class.
        /// </summary>
        public PngOptimizer()
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether various compression types will be used to find
        /// the smallest file. This process will take extra time because the file has to be written
        /// multiple times.
        /// </summary>
        public bool OptimalCompression
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the format that the optimizer supports.
        /// </summary>
        public MagickFormatInfo Format
        {
            get
            {
                return MagickNET.GetFormatInformation(MagickFormat.Png);
            }
        }

        /// <summary>
        /// Performs compression on the specified file. With some formats the image will be decoded
        /// and encoded and this will result in a small quality reduction. If the new file size is not
        /// smaller the file won't be overwritten.
        /// </summary>
        /// <param name="file">The png file to compress.</param>
        /// <returns>True when the image could be compressed otherwise false.</returns>
        public bool Compress(FileInfo file)
        {
            return LosslessCompress(file);
        }

        /// <summary>
        /// Performs compression on the specified file. With some formats the image will be decoded
        /// and encoded and this will result in a small quality reduction. If the new file size is not
        /// smaller the file won't be overwritten.
        /// </summary>
        /// <param name="fileName">The file name of the png image to compress.</param>
        /// <returns>True when the image could be compressed otherwise false.</returns>
        public bool Compress(string fileName)
        {
            return LosslessCompress(fileName);
        }

        /// <summary>
        /// Performs lossless compression on the specified file. If the new file size is not smaller
        /// the file won't be overwritten.
        /// </summary>
        /// <param name="file">The png file to optimize.</param>
        /// <returns>True when the image could be compressed otherwise false.</returns>
        public bool LosslessCompress(FileInfo file)
        {
            Throw.IfNull(nameof(file), file);

            return DoLosslessCompress(file);
        }

        /// <summary>
        /// Performs lossless compression on the specified file. If the new file size is not smaller
        /// the file won't be overwritten.
        /// </summary>
        /// <param name="fileName">The png file to optimize.</param>
        /// <returns>True when the image could be compressed otherwise false.</returns>
        public bool LosslessCompress(string fileName)
        {
            string filePath = FileHelper.CheckForBaseDirectory(fileName);
            Throw.IfNullOrEmpty(nameof(fileName), filePath);

            return DoLosslessCompress(new FileInfo(filePath));
        }

        private static void CheckFormat(MagickImage image)
        {
            MagickFormat format = image.FormatInfo.Module;
            if (format != MagickFormat.Png)
                throw new MagickCorruptImageErrorException("Invalid image format: " + format.ToString());
        }

        private static void CheckTransparency(MagickImage image)
        {
            if (!image.HasAlpha)
                return;

            if (image.IsOpaque)
                image.HasAlpha = false;
        }

        private bool DoLosslessCompress(FileInfo file)
        {
            bool isCompressed = false;

            using (MagickImage image = new MagickImage(file))
            {
                CheckFormat(image);

                image.Strip();
                image.Settings.SetDefine(MagickFormat.Png, "exclude-chunks", "all");
                image.Settings.SetDefine(MagickFormat.Png, "include-chunks", "tRNS,gAMA");
                CheckTransparency(image);

                Collection<TemporaryFile> tempFiles = new Collection<TemporaryFile>();

                try
                {
                    TemporaryFile bestFile = null;

                    foreach (int quality in GetQualityList())
                    {
                        TemporaryFile tempFile = new TemporaryFile();
                        tempFiles.Add(tempFile);

                        image.Quality = quality;
                        image.Write(tempFile);

                        if (bestFile == null || bestFile.Length > tempFile.Length)
                            bestFile = tempFile;
                    }

                    if (bestFile.Length < file.Length)
                    {
                        isCompressed = true;
                        bestFile.CopyTo(file);
                        file.Refresh();
                    }
                }
                finally
                {
                    foreach (TemporaryFile tempFile in tempFiles)
                    {
                        tempFile.Dispose();
                    }
                }
            }

            return isCompressed;
        }

        private IEnumerable<int> GetQualityList()
        {
            if (OptimalCompression)
                return new int[] { 91, 94, 95, 97 };
            else
                return new int[] { 90 };
        }
    }
}
