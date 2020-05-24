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

using System;

namespace ImageMagick
{
    /// <summary>
    /// Interface that contains information about an image format.
    /// </summary>
    public interface IMagickFormatInfo : IEquatable<IMagickFormatInfo>
    {
        /// <summary>
        /// Gets a value indicating whether the format can be read multithreaded.
        /// </summary>
        bool CanReadMultithreaded { get; }

        /// <summary>
        /// Gets a value indicating whether the format can be written multithreaded.
        /// </summary>
        bool CanWriteMultithreaded { get; }

        /// <summary>
        /// Gets the description of the format.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Gets the format.
        /// </summary>
        MagickFormat Format { get; }

        /// <summary>
        /// Gets a value indicating whether the format supports multiple frames.
        /// </summary>
        bool IsMultiFrame { get; }

        /// <summary>
        /// Gets a value indicating whether the format is readable.
        /// </summary>
        bool IsReadable { get; }

        /// <summary>
        /// Gets a value indicating whether the format is writable.
        /// </summary>
        bool IsWritable { get; }

        /// <summary>
        /// Gets the mime type.
        /// </summary>
        string MimeType { get; }

        /// <summary>
        /// Gets the module.
        /// </summary>
        MagickFormat ModuleFormat { get; }

        /// <summary>
        /// Returns a string that represents the current format.
        /// </summary>
        /// <returns>A string that represents the current format.</returns>
        string ToString();

        /// <summary>
        /// Unregisters this format.
        /// </summary>
        /// <returns>True when the format was found and unregistered.</returns>
        bool Unregister();
    }
}