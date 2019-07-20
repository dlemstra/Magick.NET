// Copyright 2013-2019 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
        /// Gets the format that the optimizer supports.
        /// </summary>
        public MagickFormatInfo Format => MagickNET.GetFormatInformation(MagickFormat.Png);

        /// <summary>
        /// Gets or sets a value indicating whether various compression types will be used to find
        /// the smallest file. This process will take extra time because the file has to be written
        /// multiple times.
        /// </summary>
        public bool OptimalCompression { get; set; }

        /// <summary>
        /// Performs compression on the specified file. With some formats the image will be decoded
        /// and encoded and this will result in a small quality reduction. If the new file size is not
        /// smaller the file won't be overwritten.
        /// </summary>
        /// <param name="file">The png file to compress.</param>
        /// <returns>True when the image could be compressed otherwise false.</returns>
        public bool Compress(FileInfo file) => DoCompress(file, false);

        /// <summary>
        /// Performs compression on the specified file. With some formats the image will be decoded
        /// and encoded and this will result in a small quality reduction. If the new file size is not
        /// smaller the file won't be overwritten.
        /// </summary>
        /// <param name="fileName">The file name of the png image to compress.</param>
        /// <returns>True when the image could be compressed otherwise false.</returns>
        public bool Compress(string fileName)
        {
            var filePath = FileHelper.CheckForBaseDirectory(fileName);
            Throw.IfNullOrEmpty(nameof(fileName), filePath);

            return DoCompress(new FileInfo(filePath), false);
        }

        /// <summary>
        /// Performs compression on the specified stream. With some formats the image will be decoded
        /// and encoded and this will result in a small quality reduction. If the new size is not
        /// smaller the stream won't be overwritten.
        /// </summary>
        /// <param name="stream">The stream of the png image to compress.</param>
        /// <returns>True when the image could be compressed otherwise false.</returns>
        public bool Compress(Stream stream) => DoCompress(stream, false);

        /// <summary>
        /// Performs lossless compression on the specified file. If the new file size is not smaller
        /// the file won't be overwritten.
        /// </summary>
        /// <param name="file">The png file to optimize.</param>
        /// <returns>True when the image could be compressed otherwise false.</returns>
        public bool LosslessCompress(FileInfo file)
        {
            Throw.IfNull(nameof(file), file);

            return DoCompress(file, true);
        }

        /// <summary>
        /// Performs lossless compression on the specified file. If the new file size is not smaller
        /// the file won't be overwritten.
        /// </summary>
        /// <param name="fileName">The png file to optimize.</param>
        /// <returns>True when the image could be compressed otherwise false.</returns>
        public bool LosslessCompress(string fileName)
        {
            var filePath = FileHelper.CheckForBaseDirectory(fileName);
            Throw.IfNullOrEmpty(nameof(fileName), filePath);

            return DoCompress(new FileInfo(filePath), true);
        }

        /// <summary>
        /// Performs lossless compression on the specified stream. If the new stream size is not smaller
        /// the stream won't be overwritten.
        /// </summary>
        /// <param name="stream">The stream of the png image to compress.</param>
        /// <returns>True when the image could be compressed otherwise false.</returns>
        public bool LosslessCompress(Stream stream) => DoCompress(stream, true);

        private static void CheckTransparency(MagickImage image)
        {
            if (!image.HasAlpha)
                return;

            if (image.IsOpaque)
                image.HasAlpha = false;
        }

        private static void StartCompression(MagickImage image, bool lossless)
        {
            ImageOptimizerHelper.CheckFormat(image, MagickFormat.Png);

            if (!lossless)
            {
                image.Strip();
                image.Settings.SetDefine(MagickFormat.Png, "exclude-chunks", "all");
                image.Settings.SetDefine(MagickFormat.Png, "include-chunks", "tRNS,gAMA");
            }

            CheckTransparency(image);
        }

        private bool DoCompress(FileInfo file, bool lossless)
        {
            bool isCompressed = false;

            using (var image = new MagickImage(file))
            {
                if (image.GetAttribute("png:acTL") != null)
                {
                    return false;
                }

                StartCompression(image, lossless);

                var tempFiles = new Collection<TemporaryFile>();

                try
                {
                    TemporaryFile bestFile = null;

                    foreach (var quality in GetQualityList())
                    {
                        var tempFile = new TemporaryFile();
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

        private bool DoCompress(Stream stream, bool lossless)
        {
            ImageOptimizerHelper.CheckStream(stream);

            var isCompressed = false;
            var startPosition = stream.Position;

            using (var image = new MagickImage(stream))
            {
                StartCompression(image, lossless);

                MemoryStream bestStream = null;

                try
                {
                    foreach (var quality in GetQualityList())
                    {
                        var memStream = new MemoryStream();

                        try
                        {
                            image.Quality = quality;
                            image.Write(memStream);

                            if (bestStream == null || memStream.Length < bestStream.Length)
                            {
                                if (bestStream != null)
                                    bestStream.Dispose();

                                bestStream = memStream;
                                memStream = null;
                            }
                        }
                        finally
                        {
                            if (memStream != null)
                                memStream.Dispose();
                        }
                    }

                    if (bestStream.Length < (stream.Length - startPosition))
                    {
                        isCompressed = true;
                        stream.Position = startPosition;
                        bestStream.Position = 0;
                        bestStream.CopyTo(stream);
                        stream.SetLength(startPosition + bestStream.Length);
                    }

                    stream.Position = startPosition;
                }
                finally
                {
                    if (bestStream != null)
                        bestStream.Dispose();
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
