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

namespace ImageMagick
{
    /// <summary>
    /// Interface that contains basic information about an image.
    /// </summary>
    public interface IMagickImageInfo
    {
        /// <summary>
        /// Gets the color space of the image.
        /// </summary>
        ColorSpace ColorSpace { get; }

        /// <summary>
        /// Gets the compression method of the image.
        /// </summary>
        CompressionMethod Compression { get; }

        /// <summary>
        /// Gets the density of the image.
        /// </summary>
        Density Density { get; }

        /// <summary>
        /// Gets the original file name of the image (only available if read from disk).
        /// </summary>
        string FileName { get; }

        /// <summary>
        /// Gets the format of the image.
        /// </summary>
        MagickFormat Format { get; }

        /// <summary>
        /// Gets the height of the image.
        /// </summary>
        int Height { get; }

        /// <summary>
        /// Gets the type of interlacing.
        /// </summary>
        Interlace Interlace { get; }

        /// <summary>
        /// Gets the JPEG/MIFF/PNG compression level.
        /// </summary>
        int Quality { get; }

        /// <summary>
        /// Gets the width of the image.
        /// </summary>
        int Width { get; }

        /// <summary>
        /// Read basic information about an image.
        /// </summary>
        /// <param name="data">The byte array to read the information from.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void Read(byte[] data);

        /// <summary>
        /// Read basic information about an image.
        /// </summary>
        /// <param name="data">The byte array to read the information from.</param>
        /// <param name="offset">The offset at which to begin reading data.</param>
        /// <param name="count">The maximum number of bytes to read.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void Read(byte[] data, int offset, int count);

        /// <summary>
        /// Read basic information about an image.
        /// </summary>
        /// <param name="file">The file to read the image from.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void Read(FileInfo file);

        /// <summary>
        /// Read basic information about an image.
        /// </summary>
        /// <param name="stream">The stream to read the image data from.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void Read(Stream stream);

        /// <summary>
        /// Read basic information about an image.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        void Read(string fileName);
    }
}