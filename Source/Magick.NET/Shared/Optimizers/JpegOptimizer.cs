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

using System.Collections.ObjectModel;
using System.IO;

namespace ImageMagick.ImageOptimizers
{
    /// <summary>
    /// Class that can be used to optimize jpeg files.
    /// </summary>
    public sealed partial class JpegOptimizer : IImageOptimizer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="JpegOptimizer"/> class.
        /// </summary>
        public JpegOptimizer()
        {
            Progressive = true;
        }

        /// <summary>
        /// Gets the format that the optimizer supports.
        /// </summary>
        public MagickFormatInfo Format
        {
            get
            {
                return MagickNET.GetFormatInformation(MagickFormat.Jpeg);
            }
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
        /// Gets or sets a value indicating whether a progressive jpeg file will be created.
        /// </summary>
        public bool Progressive
        {
            get;
            set;
        }

        /// <summary>
        /// Performs compression on the specified the file. With some formats the image will be decoded
        /// and encoded and this will result in a small quality reduction. If the new file size is not
        /// smaller the file won't be overwritten.
        /// </summary>
        /// <param name="file">The image file to compress.</param>
        public void Compress(FileInfo file)
        {
            Compress(file, 0);
        }

        /// <summary>
        /// Performs compression on the specified the file. With some formats the image will be decoded
        /// and encoded and this will result in a small quality reduction. If the new file size is not
        /// smaller the file won't be overwritten.
        /// </summary>
        /// <param name="file">The image file to compress.</param>
        /// <param name="quality">The jpeg quality.</param>
        public void Compress(FileInfo file, int quality)
        {
            Throw.IfNull(nameof(file), file);

            DoCompress(file, false, quality);
        }

        /// <summary>
        /// Performs compression on the specified the file. With some formats the image will be decoded
        /// and encoded and this will result in a small quality reduction. If the new file size is not
        /// smaller the file won't be overwritten.
        /// </summary>
        /// <param name="fileName">The file name of the image to compress.</param>
        public void Compress(string fileName)
        {
            Compress(fileName, 0);
        }

        /// <summary>
        /// Performs compression on the specified the file. With some formats the image will be decoded
        /// and encoded and this will result in a small quality reduction. If the new file size is not
        /// smaller the file won't be overwritten.
        /// </summary>
        /// <param name="fileName">The file name of the image to compress.</param>
        /// <param name="quality">The jpeg quality.</param>
        public void Compress(string fileName, int quality)
        {
            string filePath = FileHelper.CheckForBaseDirectory(fileName);
            Throw.IfInvalidFileName(filePath);

            DoCompress(new FileInfo(fileName), false, quality);
        }

        /// <summary>
        /// Performs lossless compression on the specified the file. If the new file size is not smaller
        /// the file won't be overwritten.
        /// </summary>
        /// <param name="file">The jpeg file to compress.</param>
        public void LosslessCompress(FileInfo file)
        {
            Throw.IfNull(nameof(file), file);

            DoCompress(file, true, 0);
        }

        /// <summary>
        /// Performs lossless compression on the specified file. If the new file size is not smaller
        /// the file won't be overwritten.
        /// </summary>
        /// <param name="fileName">The file name of the jpg image to compress.</param>
        public void LosslessCompress(string fileName)
        {
            string filePath = FileHelper.CheckForBaseDirectory(fileName);
            Throw.IfInvalidFileName(filePath);

            DoCompress(new FileInfo(fileName), true, 0);
        }

        private static bool DoCompress(FileInfo file, FileInfo output, bool progressive, bool lossless, int quality)
        {
            int result = NativeJpegOptimizer.Compress(file.FullName, output.FullName, progressive, lossless, quality);

            if (result == 1)
                throw new MagickCorruptImageErrorException("Unable to decompress the jpeg file.");

            if (result == 2)
                throw new MagickCorruptImageErrorException("Unable to compress the jpeg file.");

            if (result != 0)
                return false;

            output.Refresh();
            return true;
        }

        private void DoCompress(FileInfo file, bool lossless, int quality)
        {
            Collection<FileInfo> tempFiles = new Collection<FileInfo>();

            try
            {
                FileInfo bestFile = null;

                FileInfo tempFile = new FileInfo(Path.GetTempFileName());
                tempFiles.Add(tempFile);

                if (!DoCompress(file, tempFile, Progressive, lossless, quality))
                    return;

                bestFile = tempFile;

                if (OptimalCompression)
                {
                    tempFile = new FileInfo(Path.GetTempFileName());
                    tempFiles.Add(tempFile);

                    if (!DoCompress(file, tempFile, !Progressive, lossless, quality))
                        return;

                    if (bestFile.Length > tempFile.Length)
                        bestFile = tempFile;
                }

                if (bestFile.Length < file.Length)
                {
                    bestFile.CopyTo(file.FullName, true);
                    file.Refresh();
                }
            }
            finally
            {
                foreach (FileInfo tempFile in tempFiles)
                {
                    if (tempFile.Exists)
                        tempFile.Delete();
                }
            }
        }
    }
}
