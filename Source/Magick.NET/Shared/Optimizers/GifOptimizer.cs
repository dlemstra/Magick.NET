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

using System.IO;

namespace ImageMagick.ImageOptimizers
{
    /// <summary>
    /// Class that can be used to optimize gif files.
    /// </summary>
    public sealed class GifOptimizer : IImageOptimizer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GifOptimizer"/> class.
        /// </summary>
        public GifOptimizer()
        {
        }

        /// <summary>
        /// Gets the format that the optimizer supports.
        /// </summary>
        public MagickFormatInfo Format
        {
            get
            {
                return MagickNET.GetFormatInformation(MagickFormat.Gif);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether various compression types will be used to find
        /// the smallest file. This process will take extra time because the file has to be written
        /// multiple times.
        /// </summary>
        bool IImageOptimizer.OptimalCompression { get; set; }

        /// <summary>
        /// Performs compression on the specified file. With some formats the image will be decoded
        /// and encoded and this will result in a small quality reduction. If the new file size is not
        /// smaller the file won't be overwritten.
        /// </summary>
        /// <param name="file">The gif file to compress.</param>
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
        /// <param name="fileName">The file name of the gif image to compress.</param>
        /// <returns>True when the image could be compressed otherwise false.</returns>
        public bool Compress(string fileName)
        {
            return LosslessCompress(fileName);
        }

        /// <summary>
        /// Performs compression on the specified stream. With some formats the image will be decoded
        /// and encoded and this will result in a small quality reduction. If the new size is not
        /// smaller the stream won't be overwritten.
        /// </summary>
        /// <param name="stream">The stream of the gif image to compress.</param>
        /// <returns>True when the image could be compressed otherwise false.</returns>
        public bool Compress(Stream stream)
        {
            return LosslessCompress(stream);
        }

        /// <summary>
        /// Performs lossless compression on the specified file. If the new file size is not smaller
        /// the file won't be overwritten.
        /// </summary>
        /// <param name="file">The gif file to compress.</param>
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
        /// <param name="fileName">The file name of the gif image to compress.</param>
        /// <returns>True when the image could be compressed otherwise false.</returns>
        public bool LosslessCompress(string fileName)
        {
            string filePath = FileHelper.CheckForBaseDirectory(fileName);
            Throw.IfNullOrEmpty(nameof(fileName), filePath);

            return DoLosslessCompress(new FileInfo(filePath));
        }

        /// <summary>
        /// Performs lossless compression on the specified stream. If the new stream size is not smaller
        /// the stream won't be overwritten.
        /// </summary>
        /// <param name="stream">The stream of the gif image to compress.</param>
        /// <returns>True when the image could be compressed otherwise false.</returns>
        public bool LosslessCompress(Stream stream)
        {
            Throw.IfNull(nameof(stream), stream);
            ImageOptimizerHelper.CheckStream(stream);

            bool isCompressed = false;
            long startPosition = stream.Position;

            using (IMagickImageCollection images = new MagickImageCollection(stream))
            {
                if (images.Count == 1)
                    isCompressed = DoLosslessCompress(images[0], stream, startPosition);

                stream.Position = startPosition;
            }

            return isCompressed;
        }

        private static bool DoLosslessCompress(FileInfo file)
        {
            using (IMagickImageCollection images = new MagickImageCollection(file))
            {
                if (images.Count == 1)
                    return DoLosslessCompress(file, images[0]);
            }

            return false;
        }

        private static bool DoLosslessCompress(FileInfo file, IMagickImage image)
        {
            ImageOptimizerHelper.CheckFormat(image, MagickFormat.Gif);

            bool isCompressed = false;

            using (TemporaryFile tempFile = new TemporaryFile())
            {
                LosslessCompress(image);
                image.Write(tempFile);

                if (tempFile.Length < file.Length)
                {
                    isCompressed = true;
                    tempFile.CopyTo(file);
                    file.Refresh();
                }
            }

            return isCompressed;
        }

        private static void LosslessCompress(IMagickImage image)
        {
            image.Strip();
            image.Settings.Interlace = Interlace.NoInterlace;
        }

        private static bool DoLosslessCompress(IMagickImage image, Stream stream, long startPosition)
        {
            ImageOptimizerHelper.CheckFormat(image, MagickFormat.Gif);

            bool isCompressed = false;

            using (MemoryStream memStream = new MemoryStream())
            {
                LosslessCompress(image);
                image.Write(memStream);

                if (memStream.Length < (stream.Length - startPosition))
                {
                    isCompressed = true;
                    memStream.CopyTo(stream);
                    stream.SetLength(startPosition + memStream.Length);
                }
            }

            return isCompressed;
        }
    }
}