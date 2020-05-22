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

using System.Collections.Generic;
using System.IO;

namespace ImageMagick
{
    /// <summary>
    /// Interface for a class that can be used to create <see cref="IMagickImage"/>, <see cref="IMagickImageCollection"/> or <see cref="IMagickImageInfo"/> instances.
    /// </summary>
    public partial interface IMagickFactory
    {
        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageCollection"/>.
        /// </summary>
        /// <returns>A new <see cref="IMagickImageCollection"/> instance.</returns>
        IMagickImageCollection CreateCollection();

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageCollection"/>.
        /// </summary>
        /// <param name="data">The byte array to read the image data from.</param>
        /// <returns>A new <see cref="IMagickImageCollection"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickImageCollection CreateCollection(byte[] data);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageCollection"/>.
        /// </summary>
        /// <param name="data">The byte array to read the image data from.</param>
        /// <param name="offset">The offset at which to begin reading data.</param>
        /// <param name="count">The maximum number of bytes to read.</param>
        /// <returns>A new <see cref="IMagickImageCollection"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickImageCollection CreateCollection(byte[] data, int offset, int count);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageCollection"/>.
        /// </summary>
        /// <param name="data">The byte array to read the image data from.</param>
        /// <param name="offset">The offset at which to begin reading data.</param>
        /// <param name="count">The maximum number of bytes to read.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <returns>A new <see cref="IMagickImageCollection"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickImageCollection CreateCollection(byte[] data, int offset, int count, MagickReadSettings readSettings);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageCollection"/>.
        /// </summary>
        /// <param name="data">The byte array to read the image data from.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <returns>A new <see cref="IMagickImageCollection"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickImageCollection CreateCollection(byte[] data, MagickReadSettings readSettings);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageCollection"/>.
        /// </summary>
        /// <param name="file">The file to read the image from.</param>
        /// <returns>A new <see cref="IMagickImageCollection"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickImageCollection CreateCollection(FileInfo file);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageCollection"/>.
        /// </summary>
        /// <param name="file">The file to read the image from.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <returns>A new <see cref="IMagickImageCollection"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickImageCollection CreateCollection(FileInfo file, MagickReadSettings readSettings);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageCollection"/>.
        /// </summary>
        /// <param name="images">The images to add to the collection.</param>
        /// <returns>A new <see cref="IMagickImageCollection"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickImageCollection CreateCollection(IEnumerable<IMagickImage> images);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageCollection"/>.
        /// </summary>
        /// <param name="stream">The stream to read the image data from.</param>
        /// <returns>A new <see cref="IMagickImageCollection"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickImageCollection CreateCollection(Stream stream);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageCollection"/>.
        /// </summary>
        /// <param name="stream">The stream to read the image data from.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <returns>A new <see cref="IMagickImageCollection"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickImageCollection CreateCollection(Stream stream, MagickReadSettings readSettings);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageCollection"/>.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <returns>A new <see cref="IMagickImageCollection"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickImageCollection CreateCollection(string fileName);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageCollection"/>.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <returns>A new <see cref="IMagickImageCollection"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickImageCollection CreateCollection(string fileName, MagickReadSettings readSettings);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage"/>.
        /// </summary>
        /// <returns>A new <see cref="IMagickImage"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickImage CreateImage();

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage"/>.
        /// </summary>
        /// <param name="data">The byte array to read the image data from.</param>
        /// <returns>A new <see cref="IMagickImage"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickImage CreateImage(byte[] data);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage"/>.
        /// </summary>
        /// <param name="data">The byte array to read the image data from.</param>
        /// <param name="offset">The offset at which to begin reading data.</param>
        /// <param name="count">The maximum number of bytes to read.</param>
        /// <returns>A new <see cref="IMagickImage"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickImage CreateImage(byte[] data, int offset, int count);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage"/>.
        /// </summary>
        /// <param name="data">The byte array to read the image data from.</param>
        /// <param name="offset">The offset at which to begin reading data.</param>
        /// <param name="count">The maximum number of bytes to read.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <returns>A new <see cref="IMagickImage"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickImage CreateImage(byte[] data, int offset, int count, MagickReadSettings readSettings);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage"/>.
        /// </summary>
        /// <param name="data">The byte array to read the image data from.</param>
        /// <param name="offset">The offset at which to begin reading data.</param>
        /// <param name="count">The maximum number of bytes to read.</param>
        /// <param name="settings">The pixel settings to use when reading the image.</param>
        /// <returns>A new <see cref="IMagickImage"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickImage CreateImage(byte[] data, int offset, int count, PixelReadSettings settings);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage"/>.
        /// </summary>
        /// <param name="data">The byte array to read the image data from.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <returns>A new <see cref="IMagickImage"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickImage CreateImage(byte[] data, MagickReadSettings readSettings);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage"/>.
        /// </summary>
        /// <param name="data">The byte array to read the image data from.</param>
        /// <param name="settings">The pixel settings to use when reading the image.</param>
        /// <returns>A new <see cref="IMagickImage"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickImage CreateImage(byte[] data, PixelReadSettings settings);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage"/>.
        /// </summary>
        /// <param name="file">The file to read the image from.</param>
        /// <returns>A new <see cref="MagickImage"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickImage CreateImage(FileInfo file);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage"/>.
        /// </summary>
        /// <param name="file">The file to read the image from.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <returns>A new <see cref="IMagickImage"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickImage CreateImage(FileInfo file, MagickReadSettings readSettings);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage"/>.
        /// </summary>
        /// <param name="file">The file to read the image from.</param>
        /// <param name="settings">The pixel settings to use when reading the image.</param>
        /// <returns>A new <see cref="IMagickImage"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickImage CreateImage(FileInfo file, PixelReadSettings settings);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage"/>.
        /// </summary>
        /// <param name="color">The color to fill the image with.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns>A new <see cref="IMagickImage"/> instance.</returns>
        IMagickImage CreateImage(IMagickColor color, int width, int height);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage"/>.
        /// </summary>
        /// <param name="stream">The stream to read the image data from.</param>
        /// <returns>A new <see cref="IMagickImage"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickImage CreateImage(Stream stream);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage"/>.
        /// </summary>
        /// <param name="stream">The stream to read the image data from.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <returns>A new <see cref="IMagickImage"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickImage CreateImage(Stream stream, MagickReadSettings readSettings);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage"/>.
        /// </summary>
        /// <param name="stream">The stream to read the image data from.</param>
        /// <param name="settings">The pixel settings to use when reading the image.</param>
        /// <returns>A new <see cref="IMagickImage"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickImage CreateImage(Stream stream, PixelReadSettings settings);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage"/>.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <returns>A new <see cref="IMagickImage"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickImage CreateImage(string fileName);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage"/>.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns>A new <see cref="IMagickImage"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickImage CreateImage(string fileName, int width, int height);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage"/>.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <returns>A new <see cref="IMagickImage"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickImage CreateImage(string fileName, MagickReadSettings readSettings);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage"/>.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <param name="settings">The pixel settings to use when reading the image.</param>
        /// <returns>A new <see cref="IMagickImage"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickImage CreateImage(string fileName, PixelReadSettings settings);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageInfo"/>.
        /// </summary>
        /// <returns>A new <see cref="IMagickImageInfo"/> instance.</returns>
        IMagickImageInfo CreateImageInfo();

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageInfo"/>.
        /// </summary>
        /// <param name="data">The byte array to read the information from.</param>
        /// <returns>A new <see cref="IMagickImageInfo"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickImageInfo CreateImageInfo(byte[] data);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageInfo"/>.
        /// </summary>
        /// <param name="data">The byte array to read the information from.</param>
        /// <param name="offset">The offset at which to begin reading data.</param>
        /// <param name="count">The maximum number of bytes to read.</param>
        /// <returns>A new <see cref="IMagickImageInfo"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickImageInfo CreateImageInfo(byte[] data, int offset, int count);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageInfo"/>.
        /// </summary>
        /// <param name="file">The file to read the image from.</param>
        /// <returns>A new <see cref="IMagickImageInfo"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickImageInfo CreateImageInfo(FileInfo file);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageInfo"/>.
        /// </summary>
        /// <param name="stream">The stream to read the image data from.</param>
        /// <returns>A new <see cref="IMagickImageInfo"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickImageInfo CreateImageInfo(Stream stream);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageInfo"/>.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <returns>A new <see cref="IMagickImageInfo"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickImageInfo CreateImageInfo(string fileName);
    }
}
