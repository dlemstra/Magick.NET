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
        /// Performs compression on the specified the file. With some formats the image will be decoded
        /// and encoded and this will result in a small quality reduction. If the new file size is not
        /// smaller the file won't be overwritten.
        /// </summary>
        /// <param name="file">The gif file to compress.</param>
        public void Compress(FileInfo file)
        {
            LosslessCompress(file);
        }

        /// <summary>
        /// Performs compression on the specified the file. With some formats the image will be decoded
        /// and encoded and this will result in a small quality reduction. If the new file size is not
        /// smaller the file won't be overwritten.
        /// </summary>
        /// <param name="fileName">The file name of the gif image to compress.</param>
        public void Compress(string fileName)
        {
            LosslessCompress(fileName);
        }

        /// <summary>
        /// Performs lossless compression on the specified the file. If the new file size is not smaller
        /// the file won't be overwritten.
        /// </summary>
        /// <param name="file">The gif file to compress.</param>
        public void LosslessCompress(FileInfo file)
        {
            Throw.IfNull(nameof(file), file);

            DoLosslessCompress(file);
            file.Refresh();
        }

        /// <summary>
        /// Performs lossless compression on the specified the file. If the new file size is not smaller
        /// the file won't be overwritten.
        /// </summary>
        /// <param name="fileName">The file name of the gif image to compress.</param>
        public void LosslessCompress(string fileName)
        {
            string filePath = FileHelper.CheckForBaseDirectory(fileName);
            Throw.IfInvalidFileName(filePath);

            DoLosslessCompress(new FileInfo(filePath));
        }

        private static void CheckFormat(IMagickImage image)
        {
            MagickFormat format = image.FormatInfo.Module;
            if (format != MagickFormat.Gif)
                throw new MagickCorruptImageErrorException("Invalid image format: " + format.ToString());
        }

        private static void DoLosslessCompress(FileInfo file)
        {
            using (IMagickImageCollection images = new MagickImageCollection(file))
            {
                if (images.Count == 1)
                {
                    DoLosslessCompress(file, images[0]);
                    return;
                }
            }
        }

        private static void DoLosslessCompress(FileInfo file, IMagickImage image)
        {
            CheckFormat(image);

            image.Strip();

            FileInfo tempFile = new FileInfo(Path.GetTempFileName());
            try
            {
                image.Settings.Interlace = Interlace.NoInterlace;
                image.Write(tempFile);

                if (tempFile.Length < file.Length)
                    tempFile.CopyTo(file.FullName, true);
            }
            finally
            {
                if (tempFile.Exists)
                    tempFile.Delete();
            }
        }
    }
}
