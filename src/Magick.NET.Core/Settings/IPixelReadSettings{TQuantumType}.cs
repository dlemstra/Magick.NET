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

namespace ImageMagick
{
    /// <summary>
    /// Class that contains setting for when pixels are read.
    /// </summary>
    /// <typeparam name="TQuantumType">The quantum type.</typeparam>
    public interface IPixelReadSettings<TQuantumType>
        where TQuantumType : struct
    {
        /// <summary>
        /// Gets or sets the mapping of the pixels (e.g. RGB/RGBA/ARGB).
        /// </summary>
        string? Mapping { get; set; }

        /// <summary>
        /// Gets or sets the pixel storage type.
        /// </summary>
        StorageType StorageType { get; set; }

        /// <summary>
        /// Gets the settings to use when reading the image.
        /// </summary>
        IMagickReadSettings<TQuantumType> ReadSettings { get; }
    }
}