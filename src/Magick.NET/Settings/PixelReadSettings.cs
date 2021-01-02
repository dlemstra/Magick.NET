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
    /// Class that contains setting for when pixels are read.
    /// </summary>
    public sealed class PixelReadSettings : IPixelReadSettings<QuantumType>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PixelReadSettings"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="storageType">The pixel storage type.</param>
        /// <param name="mapping">The mapping of the pixels.</param>
        public PixelReadSettings(int width, int height, StorageType storageType, PixelMapping mapping)
            : this(width, height, storageType, mapping.ToString())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PixelReadSettings"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="storageType">The pixel storage type.</param>
        /// <param name="mapping">The mapping of the pixels (e.g. RGB/RGBA/ARGB).</param>
        public PixelReadSettings(int width, int height, StorageType storageType, string mapping)
        {
            ReadSettings = new MagickReadSettings
            {
                Width = width,
                Height = height,
            };
            StorageType = storageType;
            Mapping = mapping;
        }

        internal PixelReadSettings()
        {
            ReadSettings = new MagickReadSettings();
        }

        /// <summary>
        /// Gets or sets the mapping of the pixels (e.g. RGB/RGBA/ARGB).
        /// </summary>
        public string Mapping { get; set; }

        /// <summary>
        /// Gets or sets the pixel storage type.
        /// </summary>
        public StorageType StorageType { get; set; }

        /// <summary>
        /// Gets the settings to use when reading the image.
        /// </summary>
        public IMagickReadSettings<QuantumType> ReadSettings { get; internal set; }
    }
}