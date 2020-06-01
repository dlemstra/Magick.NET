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

using System.IO;

namespace ImageMagick
{
    /// <summary>
    /// Class that can be used to create <see cref="IMagickImage{TQuantumType}"/> instances.
    /// </summary>
    /// <typeparam name="TQuantumType">The quantum type.</typeparam>
    public interface IMagickImageFactory<TQuantumType>
        where TQuantumType : struct
    {
        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
        /// </summary>
        /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickImage<TQuantumType> Create();

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
        /// </summary>
        /// <param name="data">The byte array to read the image data from.</param>
        /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickImage<TQuantumType> Create(byte[] data);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
        /// </summary>
        /// <param name="data">The byte array to read the image data from.</param>
        /// <param name="offset">The offset at which to begin reading data.</param>
        /// <param name="count">The maximum number of bytes to read.</param>
        /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickImage<TQuantumType> Create(byte[] data, int offset, int count);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
        /// </summary>
        /// <param name="data">The byte array to read the image data from.</param>
        /// <param name="offset">The offset at which to begin reading data.</param>
        /// <param name="count">The maximum number of bytes to read.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickImage<TQuantumType> Create(byte[] data, int offset, int count, IMagickReadSettings<TQuantumType> readSettings);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
        /// </summary>
        /// <param name="data">The byte array to read the image data from.</param>
        /// <param name="offset">The offset at which to begin reading data.</param>
        /// <param name="count">The maximum number of bytes to read.</param>
        /// <param name="settings">The pixel settings to use when reading the image.</param>
        /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickImage<TQuantumType> Create(byte[] data, int offset, int count, IPixelReadSettings<TQuantumType> settings);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
        /// </summary>
        /// <param name="data">The byte array to read the image data from.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickImage<TQuantumType> Create(byte[] data, IMagickReadSettings<TQuantumType> readSettings);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
        /// </summary>
        /// <param name="data">The byte array to read the image data from.</param>
        /// <param name="settings">The pixel settings to use when reading the image.</param>
        /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickImage<TQuantumType> Create(byte[] data, IPixelReadSettings<TQuantumType> settings);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
        /// </summary>
        /// <param name="file">The file to read the image from.</param>
        /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickImage<TQuantumType> Create(FileInfo file);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
        /// </summary>
        /// <param name="file">The file to read the image from.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickImage<TQuantumType> Create(FileInfo file, IMagickReadSettings<TQuantumType> readSettings);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
        /// </summary>
        /// <param name="file">The file to read the image from.</param>
        /// <param name="settings">The pixel settings to use when reading the image.</param>
        /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickImage<TQuantumType> Create(FileInfo file, IPixelReadSettings<TQuantumType> settings);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
        /// </summary>
        /// <param name="color">The color to fill the image with.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
        IMagickImage<TQuantumType> Create(IMagickColor<TQuantumType> color, int width, int height);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
        /// </summary>
        /// <param name="stream">The stream to read the image data from.</param>
        /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickImage<TQuantumType> Create(Stream stream);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
        /// </summary>
        /// <param name="stream">The stream to read the image data from.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickImage<TQuantumType> Create(Stream stream, IMagickReadSettings<TQuantumType> readSettings);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
        /// </summary>
        /// <param name="stream">The stream to read the image data from.</param>
        /// <param name="settings">The pixel settings to use when reading the image.</param>
        /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickImage<TQuantumType> Create(Stream stream, IPixelReadSettings<TQuantumType> settings);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickImage<TQuantumType> Create(string fileName);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickImage<TQuantumType> Create(string fileName, int width, int height);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickImage<TQuantumType> Create(string fileName, IMagickReadSettings<TQuantumType> readSettings);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage{TQuantumType}"/>.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <param name="settings">The pixel settings to use when reading the image.</param>
        /// <returns>A new <see cref="IMagickImage{TQuantumType}"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        IMagickImage<TQuantumType> Create(string fileName, IPixelReadSettings<TQuantumType> settings);
    }
}
