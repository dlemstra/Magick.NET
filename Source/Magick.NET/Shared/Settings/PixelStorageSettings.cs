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

namespace ImageMagick
{
    /// <summary>
    /// Class that contains setting for pixel storage.
    /// </summary>
    public sealed class PixelStorageSettings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PixelStorageSettings"/> class.
        /// </summary>
        public PixelStorageSettings()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PixelStorageSettings"/> class.
        /// </summary>
        /// <param name="storageType">The pixel storage type</param>
        /// <param name="mapping">The mapping of the pixels (e.g. RGB/RGBA/ARGB).</param>
        public PixelStorageSettings(StorageType storageType, string mapping)
        {
            Mapping = mapping;
            StorageType = storageType;
        }

        /// <summary>
        /// Gets or sets the mapping of the pixels (e.g. RGB/RGBA/ARGB).
        /// </summary>
        public string Mapping
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the pixel storage type.
        /// </summary>
        public StorageType StorageType
        {
            get;
            set;
        }

        internal PixelStorageSettings Clone()
        {
            return new PixelStorageSettings()
            {
                Mapping = Mapping,
                StorageType = StorageType,
            };
        }
    }
}