// Copyright 2013-2021 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in
// compliance with the License. You may obtain a copy of the License at
//
//   https://imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND,
// either express or implied. See the License for the specific language governing permissions
// and limitations under the License.

using System.Collections.Generic;
using System.IO;

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace ImageMagick
{
    /// <summary>
    /// Class that can be used to create <see cref="IMagickImageCollection{QuantumType}"/> instances.
    /// </summary>
    public sealed class MagickImageCollectionFactory : IMagickImageCollectionFactory<QuantumType>
    {
        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageCollection{TQuantumType}"/>.
        /// </summary>
        /// <returns>A new <see cref="IMagickImageCollection{TQuantumType}"/> instance.</returns>
        public IMagickImageCollection<QuantumType> Create()
            => new MagickImageCollection();

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageCollection{TQuantumType}"/>.
        /// </summary>
        /// <param name="data">The byte array to read the image data from.</param>
        /// <returns>A new <see cref="IMagickImageCollection{TQuantumType}"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImageCollection<QuantumType> Create(byte[] data)
            => new MagickImageCollection(data);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageCollection{TQuantumType}"/>.
        /// </summary>
        /// <param name="data">The byte array to read the image data from.</param>
        /// <param name="offset">The offset at which to begin reading data.</param>
        /// <param name="count">The maximum number of bytes to read.</param>
        /// <returns>A new <see cref="IMagickImageCollection{TQuantumType}"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImageCollection<QuantumType> Create(byte[] data, int offset, int count)
            => new MagickImageCollection(data, offset, count);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageCollection{TQuantumType}"/>.
        /// </summary>
        /// <param name="data">The byte array to read the image data from.</param>
        /// <param name="offset">The offset at which to begin reading data.</param>
        /// <param name="count">The maximum number of bytes to read.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <returns>A new <see cref="IMagickImageCollection{TQuantumType}"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImageCollection<QuantumType> Create(byte[] data, int offset, int count, IMagickReadSettings<QuantumType> readSettings)
            => new MagickImageCollection(data, offset, count, readSettings);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageCollection{TQuantumType}"/>.
        /// </summary>
        /// <param name="data">The byte array to read the image data from.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <returns>A new <see cref="IMagickImageCollection{TQuantumType}"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImageCollection<QuantumType> Create(byte[] data, IMagickReadSettings<QuantumType> readSettings)
            => new MagickImageCollection(data, readSettings);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageCollection{TQuantumType}"/>.
        /// </summary>
        /// <param name="file">The file to read the image from.</param>
        /// <returns>A new <see cref="IMagickImageCollection{TQuantumType}"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImageCollection<QuantumType> Create(FileInfo file)
            => new MagickImageCollection(file);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageCollection{TQuantumType}"/>.
        /// </summary>
        /// <param name="file">The file to read the image from.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <returns>A new <see cref="IMagickImageCollection{TQuantumType}"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImageCollection<QuantumType> Create(FileInfo file, IMagickReadSettings<QuantumType> readSettings)
            => new MagickImageCollection(file, readSettings);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageCollection{TQuantumType}"/>.
        /// </summary>
        /// <param name="images">The images to add to the collection.</param>
        /// <returns>A new <see cref="IMagickImageCollection{TQuantumType}"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImageCollection<QuantumType> Create(IEnumerable<IMagickImage<QuantumType>> images)
            => new MagickImageCollection(images);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageCollection{TQuantumType}"/>.
        /// </summary>
        /// <param name="stream">The stream to read the image data from.</param>
        /// <returns>A new <see cref="IMagickImageCollection{TQuantumType}"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImageCollection<QuantumType> Create(Stream stream)
            => new MagickImageCollection(stream);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageCollection{TQuantumType}"/>.
        /// </summary>
        /// <param name="stream">The stream to read the image data from.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <returns>A new <see cref="IMagickImageCollection{TQuantumType}"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImageCollection<QuantumType> Create(Stream stream, IMagickReadSettings<QuantumType> readSettings)
            => new MagickImageCollection(stream, readSettings);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageCollection{TQuantumType}"/>.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <returns>A new <see cref="IMagickImageCollection{TQuantumType}"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImageCollection<QuantumType> Create(string fileName)
            => new MagickImageCollection(fileName);

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageCollection{TQuantumType}"/>.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <returns>A new <see cref="IMagickImageCollection{TQuantumType}"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImageCollection<QuantumType> Create(string fileName, IMagickReadSettings<QuantumType> readSettings)
            => new MagickImageCollection(fileName, readSettings);
    }
}
