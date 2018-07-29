// Copyright 2013-2018 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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
        public IMagickImageCollection CreateCollection()
        {
            return new MagickImageCollection();
        }

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageCollection"/>.
        /// </summary>
        /// <param name="data">The byte array to read the image data from.</param>
        /// <returns>A new <see cref="IMagickImageCollection"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImageCollection CreateCollection(byte[] data)
        {
            return new MagickImageCollection(data);
        }

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageCollection"/>.
        /// </summary>
        /// <param name="data">The byte array to read the image data from.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <returns>A new <see cref="IMagickImageCollection"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImageCollection CreateCollection(byte[] data, MagickReadSettings readSettings)
        {
            return new MagickImageCollection(data, readSettings);
        }

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageCollection"/>.
        /// </summary>
        /// <param name="file">The file to read the image from.</param>
        /// <returns>A new <see cref="IMagickImageCollection"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImageCollection CreateCollection(FileInfo file)
        {
            return new MagickImageCollection(file);
        }

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageCollection"/>.
        /// </summary>
        /// <param name="file">The file to read the image from.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <returns>A new <see cref="IMagickImageCollection"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImageCollection CreateCollection(FileInfo file, MagickReadSettings readSettings)
        {
            return new MagickImageCollection(file, readSettings);
        }

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageCollection"/>.
        /// </summary>
        /// <param name="images">The images to add to the collection.</param>
        /// <returns>A new <see cref="IMagickImageCollection"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImageCollection CreateCollection(IEnumerable<IMagickImage> images)
        {
            return new MagickImageCollection(images);
        }

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageCollection"/>.
        /// </summary>
        /// <param name="stream">The stream to read the image data from.</param>
        /// <returns>A new <see cref="IMagickImageCollection"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImageCollection CreateCollection(Stream stream)
        {
            return new MagickImageCollection(stream);
        }

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageCollection"/>.
        /// </summary>
        /// <param name="stream">The stream to read the image data from.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <returns>A new <see cref="IMagickImageCollection"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImageCollection CreateCollection(Stream stream, MagickReadSettings readSettings)
        {
            return new MagickImageCollection(stream, readSettings);
        }

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageCollection"/>.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <returns>A new <see cref="IMagickImageCollection"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImageCollection CreateCollection(string fileName)
        {
            return new MagickImageCollection(fileName);
        }

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageCollection"/>.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <returns>A new <see cref="IMagickImageCollection"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImageCollection CreateCollection(string fileName, MagickReadSettings readSettings)
        {
            return new MagickImageCollection(fileName, readSettings);
        }

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage"/>.
        /// </summary>
        /// <returns>A new <see cref="IMagickImage"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImage CreateImage()
        {
            return new MagickImage();
        }

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage"/>.
        /// </summary>
        /// <param name="data">The byte array to read the image data from.</param>
        /// <returns>A new <see cref="IMagickImage"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImage CreateImage(byte[] data)
        {
            return new MagickImage(data);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IMagickImage"/> class.
        /// </summary>
        /// <param name="data">The byte array to read the image data from.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <returns>A new <see cref="IMagickImage"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImage CreateImage(byte[] data, MagickReadSettings readSettings)
        {
            return new MagickImage(data, readSettings);
        }

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage"/>.
        /// </summary>
        /// <param name="data">The byte array to read the image data from.</param>
        /// <param name="pixelStorageSettings">The pixel storage settings to use when reading the image.</param>
        /// <returns>A new <see cref="IMagickImage"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImage CreateImage(byte[] data, PixelStorageSettings pixelStorageSettings)
        {
            return new MagickImage(data, pixelStorageSettings);
        }

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage"/>.
        /// </summary>
        /// <param name="file">The file to read the image from.</param>
        /// <returns>A new <see cref="MagickImage"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImage CreateImage(FileInfo file)
        {
            return new MagickImage(file);
        }

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage"/>.
        /// </summary>
        /// <param name="file">The file to read the image from.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <returns>A new <see cref="IMagickImage"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImage CreateImage(FileInfo file, MagickReadSettings readSettings)
        {
            return new MagickImage(file, readSettings);
        }

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage"/>.
        /// </summary>
        /// <param name="file">The file to read the image from.</param>
        /// <param name="pixelStorageSettings">The pixel storage settings to use when reading the image.</param>
        /// <returns>A new <see cref="IMagickImage"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImage CreateImage(FileInfo file, PixelStorageSettings pixelStorageSettings)
        {
            return new MagickImage(file, pixelStorageSettings);
        }

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage"/>.
        /// </summary>
        /// <param name="color">The color to fill the image with.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns>A new <see cref="IMagickImage"/> instance.</returns>
        public IMagickImage CreateImage(MagickColor color, int width, int height)
        {
            return new MagickImage(color, width, height);
        }

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage"/>.
        /// </summary>
        /// <param name="stream">The stream to read the image data from.</param>
        /// <returns>A new <see cref="IMagickImage"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImage CreateImage(Stream stream)
        {
            return new MagickImage(stream);
        }

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage"/>.
        /// </summary>
        /// <param name="stream">The stream to read the image data from.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <returns>A new <see cref="IMagickImage"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImage CreateImage(Stream stream, MagickReadSettings readSettings)
        {
            return new MagickImage(stream, readSettings);
        }

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage"/>.
        /// </summary>
        /// <param name="stream">The stream to read the image data from.</param>
        /// <param name="pixelStorageSettings">The pixel storage settings to use when reading the image.</param>
        /// <returns>A new <see cref="IMagickImage"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImage CreateImage(Stream stream, PixelStorageSettings pixelStorageSettings)
        {
            return new MagickImage(stream, pixelStorageSettings);
        }

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage"/>.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <returns>A new <see cref="IMagickImage"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImage CreateImage(string fileName)
        {
            return new MagickImage(fileName);
        }

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage"/>.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns>A new <see cref="IMagickImage"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImage CreateImage(string fileName, int width, int height)
        {
            return new MagickImage(fileName, width, height);
        }

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage"/>.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <param name="readSettings">The settings to use when reading the image.</param>
        /// <returns>A new <see cref="IMagickImage"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImage CreateImage(string fileName, MagickReadSettings readSettings)
        {
            return new MagickImage(fileName, readSettings);
        }

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImage"/>.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <param name="pixelStorageSettings">The pixel storage settings to use when reading the image.</param>
        /// <returns>A new <see cref="IMagickImage"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImage CreateImage(string fileName, PixelStorageSettings pixelStorageSettings)
        {
            return new MagickImage(fileName, pixelStorageSettings);
        }

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageInfo"/>.
        /// </summary>
        /// <returns>A new <see cref="IMagickImageInfo"/> instance.</returns>
        public IMagickImageInfo CreateImageInfo()
        {
            return new MagickImageInfo();
        }

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageInfo"/>.
        /// </summary>
        /// <param name="data">The byte array to read the information from.</param>
        /// <returns>A new <see cref="IMagickImageInfo"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImageInfo CreateImageInfo(byte[] data)
        {
            return new MagickImageInfo(data);
        }

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageInfo"/>.
        /// </summary>
        /// <param name="file">The file to read the image from.</param>
        /// <returns>A new <see cref="IMagickImageInfo"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImageInfo CreateImageInfo(FileInfo file)
        {
            return new MagickImageInfo(file);
        }

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageInfo"/>.
        /// </summary>
        /// <param name="stream">The stream to read the image data from.</param>
        /// <returns>A new <see cref="IMagickImageInfo"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImageInfo CreateImageInfo(Stream stream)
        {
            return new MagickImageInfo(stream);
        }

        /// <summary>
        /// Initializes a new instance that implements <see cref="IMagickImageInfo"/>.
        /// </summary>
        /// <param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
        /// <returns>A new <see cref="IMagickImageInfo"/> instance.</returns>
        /// <exception cref="MagickException">Thrown when an error is raised by ImageMagick.</exception>
        public IMagickImageInfo CreateImageInfo(string fileName)
        {
            return new MagickImageInfo(fileName);
        }
    }
}
