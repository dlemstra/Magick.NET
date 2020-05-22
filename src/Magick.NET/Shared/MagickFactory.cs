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
    /// Class that can be used to create <see cref="IMagickImage"/>, <see cref="IMagickImageCollection"/> or <see cref="IMagickImageInfo"/> instances.
    /// </summary>
    public sealed partial class MagickFactory : IMagickFactory
    {
        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageCollection"/>.
        /// </summary>
        /// <returns>A new <see cref="IMagickImageCollection"/> instance.</returns>
        public IMagickImageCollection CreateCollection() => new MagickImageCollection();

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageCollection"/>.
        /// </summary>
        /// <param name="data">The byte array to read the image data from.</param>
        /// <returns>A new <see cref="IMagickImageCollection"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImageCollection CreateCollection(byte[] data) => new MagickImageCollection(data);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageCollection"/>.
        /// </summary>
        /// <param name="data">The byte array to read the image data from.</param>
        /// <param name="offset">The offset at which to begin reading data.</param>
        /// <param name="count">The maximum number of bytes to read.</param>
        /// <returns>A new <see cref="IMagickImageCollection"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImageCollection CreateCollection(byte[] data, int offset, int count) => new MagickImageCollection(data, offset, count);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageCollection"/>.
        /// </summary>
        /// <param name="data">The byte array to read the image data from.</param>
        /// <param name="offset">The offset at which to begin reading data.</param>
        /// <param name="count">The maximum number of bytes to read.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <returns>A new <see cref="IMagickImageCollection"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImageCollection CreateCollection(byte[] data, int offset, int count, MagickReadSettings readSettings) => new MagickImageCollection(data, offset, count, readSettings);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageCollection"/>.
        /// </summary>
        /// <param name="data">The byte array to read the image data from.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <returns>A new <see cref="IMagickImageCollection"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImageCollection CreateCollection(byte[] data, MagickReadSettings readSettings) => new MagickImageCollection(data, readSettings);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageCollection"/>.
        /// </summary>
        /// <param name="file">The file to read the image from.</param>
        /// <returns>A new <see cref="IMagickImageCollection"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImageCollection CreateCollection(FileInfo file) => new MagickImageCollection(file);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageCollection"/>.
        /// </summary>
        /// <param name="file">The file to read the image from.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <returns>A new <see cref="IMagickImageCollection"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImageCollection CreateCollection(FileInfo file, MagickReadSettings readSettings) => new MagickImageCollection(file, readSettings);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageCollection"/>.
        /// </summary>
        /// <param name="images">The images to add to the collection.</param>
        /// <returns>A new <see cref="IMagickImageCollection"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImageCollection CreateCollection(IEnumerable<IMagickImage> images) => new MagickImageCollection(images);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageCollection"/>.
        /// </summary>
        /// <param name="stream">The stream to read the image data from.</param>
        /// <returns>A new <see cref="IMagickImageCollection"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImageCollection CreateCollection(Stream stream) => new MagickImageCollection(stream);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageCollection"/>.
        /// </summary>
        /// <param name="stream">The stream to read the image data from.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <returns>A new <see cref="IMagickImageCollection"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImageCollection CreateCollection(Stream stream, MagickReadSettings readSettings) => new MagickImageCollection(stream, readSettings);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageCollection"/>.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <returns>A new <see cref="IMagickImageCollection"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImageCollection CreateCollection(string fileName) => new MagickImageCollection(fileName);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageCollection"/>.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <returns>A new <see cref="IMagickImageCollection"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImageCollection CreateCollection(string fileName, MagickReadSettings readSettings) => new MagickImageCollection(fileName, readSettings);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage"/>.
        /// </summary>
        /// <returns>A new <see cref="IMagickImage"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImage CreateImage() => new MagickImage();

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage"/>.
        /// </summary>
        /// <param name="data">The byte array to read the image data from.</param>
        /// <returns>A new <see cref="IMagickImage"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImage CreateImage(byte[] data) => new MagickImage(data);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage"/>.
        /// </summary>
        /// <param name="data">The byte array to read the image data from.</param>
        /// <param name="offset">The offset at which to begin reading data.</param>
        /// <param name="count">The maximum number of bytes to read.</param>
        /// <returns>A new <see cref="IMagickImage"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImage CreateImage(byte[] data, int offset, int count) => new MagickImage(data, offset, count);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage"/>.
        /// </summary>
        /// <param name="data">The byte array to read the image data from.</param>
        /// <param name="offset">The offset at which to begin reading data.</param>
        /// <param name="count">The maximum number of bytes to read.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <returns>A new <see cref="IMagickImage"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImage CreateImage(byte[] data, int offset, int count, MagickReadSettings readSettings) => new MagickImage(data, offset, count, readSettings);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage"/>.
        /// </summary>
        /// <param name="data">The byte array to read the image data from.</param>
        /// <param name="offset">The offset at which to begin reading data.</param>
        /// <param name="count">The maximum number of bytes to read.</param>
        /// <param name="settings">The pixel settings to use when reading the image.</param>
        /// <returns>A new <see cref="IMagickImage"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImage CreateImage(byte[] data, int offset, int count, PixelReadSettings settings) => new MagickImage(data, offset, count, settings);

        /// <summary>
        /// Initializes a new instance of the <see cref="IMagickImage"/> class.
        /// </summary>
        /// <param name="data">The byte array to read the image data from.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <returns>A new <see cref="IMagickImage"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImage CreateImage(byte[] data, MagickReadSettings readSettings) => new MagickImage(data, readSettings);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage"/>.
        /// </summary>
        /// <param name="data">The byte array to read the image data from.</param>
        /// <param name="settings">The pixel settings to use when reading the image.</param>
        /// <returns>A new <see cref="IMagickImage"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImage CreateImage(byte[] data, PixelReadSettings settings) => new MagickImage(data, settings);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage"/>.
        /// </summary>
        /// <param name="file">The file to read the image from.</param>
        /// <returns>A new <see cref="MagickImage"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImage CreateImage(FileInfo file) => new MagickImage(file);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage"/>.
        /// </summary>
        /// <param name="file">The file to read the image from.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <returns>A new <see cref="IMagickImage"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImage CreateImage(FileInfo file, MagickReadSettings readSettings) => new MagickImage(file, readSettings);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage"/>.
        /// </summary>
        /// <param name="file">The file to read the image from.</param>
        /// <param name="settings">The pixel settings to use when reading the image.</param>
        /// <returns>A new <see cref="IMagickImage"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImage CreateImage(FileInfo file, PixelReadSettings settings) => new MagickImage(file, settings);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage"/>.
        /// </summary>
        /// <param name="color">The color to fill the image with.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns>A new <see cref="IMagickImage"/> instance.</returns>
        public IMagickImage CreateImage(IMagickColor color, int width, int height) => new MagickImage(color, width, height);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage"/>.
        /// </summary>
        /// <param name="stream">The stream to read the image data from.</param>
        /// <returns>A new <see cref="IMagickImage"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImage CreateImage(Stream stream) => new MagickImage(stream);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage"/>.
        /// </summary>
        /// <param name="stream">The stream to read the image data from.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <returns>A new <see cref="IMagickImage"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImage CreateImage(Stream stream, MagickReadSettings readSettings) => new MagickImage(stream, readSettings);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage"/>.
        /// </summary>
        /// <param name="stream">The stream to read the image data from.</param>
        /// <param name="settings">The pixel settings to use when reading the image.</param>
        /// <returns>A new <see cref="IMagickImage"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImage CreateImage(Stream stream, PixelReadSettings settings) => new MagickImage(stream, settings);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage"/>.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <returns>A new <see cref="IMagickImage"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImage CreateImage(string fileName) => new MagickImage(fileName);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage"/>.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns>A new <see cref="IMagickImage"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImage CreateImage(string fileName, int width, int height) => new MagickImage(fileName, width, height);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage"/>.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <returns>A new <see cref="IMagickImage"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImage CreateImage(string fileName, MagickReadSettings readSettings) => new MagickImage(fileName, readSettings);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage"/>.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <param name="settings">The pixel settings to use when reading the image.</param>
        /// <returns>A new <see cref="IMagickImage"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImage CreateImage(string fileName, PixelReadSettings settings) => new MagickImage(fileName, settings);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageInfo"/>.
        /// </summary>
        /// <returns>A new <see cref="IMagickImageInfo"/> instance.</returns>
        public IMagickImageInfo CreateImageInfo() => new MagickImageInfo();

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageInfo"/>.
        /// </summary>
        /// <param name="data">The byte array to read the information from.</param>
        /// <returns>A new <see cref="IMagickImageInfo"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImageInfo CreateImageInfo(byte[] data) => new MagickImageInfo(data);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageInfo"/>.
        /// </summary>
        /// <param name="data">The byte array to read the information from.</param>
        /// <param name="offset">The offset at which to begin reading data.</param>
        /// <param name="count">The maximum number of bytes to read.</param>
        /// <returns>A new <see cref="IMagickImageInfo"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImageInfo CreateImageInfo(byte[] data, int offset, int count) => new MagickImageInfo(data, offset, count);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageInfo"/>.
        /// </summary>
        /// <param name="file">The file to read the image from.</param>
        /// <returns>A new <see cref="IMagickImageInfo"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImageInfo CreateImageInfo(FileInfo file) => new MagickImageInfo(file);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageInfo"/>.
        /// </summary>
        /// <param name="stream">The stream to read the image data from.</param>
        /// <returns>A new <see cref="IMagickImageInfo"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImageInfo CreateImageInfo(Stream stream) => new MagickImageInfo(stream);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageInfo"/>.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <returns>A new <see cref="IMagickImageInfo"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImageInfo CreateImageInfo(string fileName) => new MagickImageInfo(fileName);
    }
}
