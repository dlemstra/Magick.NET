// Copyright 2013-2019 Dirk Lemstra <https://github.com/dlemstra/Magick.NET/>
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

namespace ImageMagick
{
    /// <summary>
    /// Contains the possible exif values.
    /// </summary>
    public static partial class ExifValues
    {
        /// <summary>
        /// Gets a new <see cref="ExifLongArray"/> instance for the <see cref="ExifTagValue.FreeOffsets"/> tag.
        /// </summary>
        public static ExifLongArray FreeOffsets => new ExifLongArray(ExifTagValue.FreeOffsets);

        /// <summary>
        /// Gets a new <see cref="ExifLongArray"/> instance for the <see cref="ExifTagValue.FreeByteCounts"/> tag.
        /// </summary>
        public static ExifLongArray FreeByteCounts => new ExifLongArray(ExifTagValue.FreeByteCounts);

        /// <summary>
        /// Gets a new <see cref="ExifLongArray"/> instance for the <see cref="ExifTagValue.ColorResponseUnit"/> tag.
        /// </summary>
        public static ExifLongArray ColorResponseUnit => new ExifLongArray(ExifTagValue.ColorResponseUnit);

        /// <summary>
        /// Gets a new <see cref="ExifLongArray"/> instance for the <see cref="ExifTagValue.TileOffsets"/> tag.
        /// </summary>
        public static ExifLongArray TileOffsets => new ExifLongArray(ExifTagValue.TileOffsets);

        /// <summary>
        /// Gets a new <see cref="ExifLongArray"/> instance for the <see cref="ExifTagValue.SMinSampleValue"/> tag.
        /// </summary>
        public static ExifLongArray SMinSampleValue => new ExifLongArray(ExifTagValue.SMinSampleValue);

        /// <summary>
        /// Gets a new <see cref="ExifLongArray"/> instance for the <see cref="ExifTagValue.SMaxSampleValue"/> tag.
        /// </summary>
        public static ExifLongArray SMaxSampleValue => new ExifLongArray(ExifTagValue.SMaxSampleValue);

        /// <summary>
        /// Gets a new <see cref="ExifLongArray"/> instance for the <see cref="ExifTagValue.JPEGQTables"/> tag.
        /// </summary>
        public static ExifLongArray JPEGQTables => new ExifLongArray(ExifTagValue.JPEGQTables);

        /// <summary>
        /// Gets a new <see cref="ExifLongArray"/> instance for the <see cref="ExifTagValue.JPEGDCTables"/> tag.
        /// </summary>
        public static ExifLongArray JPEGDCTables => new ExifLongArray(ExifTagValue.JPEGDCTables);

        /// <summary>
        /// Gets a new <see cref="ExifLongArray"/> instance for the <see cref="ExifTagValue.JPEGACTables"/> tag.
        /// </summary>
        public static ExifLongArray JPEGACTables => new ExifLongArray(ExifTagValue.JPEGACTables);

        /// <summary>
        /// Gets a new <see cref="ExifLongArray"/> instance for the <see cref="ExifTagValue.StripRowCounts"/> tag.
        /// </summary>
        public static ExifLongArray StripRowCounts => new ExifLongArray(ExifTagValue.StripRowCounts);

        /// <summary>
        /// Gets a new <see cref="ExifLongArray"/> instance for the <see cref="ExifTagValue.IntergraphRegisters"/> tag.
        /// </summary>
        public static ExifLongArray IntergraphRegisters => new ExifLongArray(ExifTagValue.IntergraphRegisters);

        /// <summary>
        /// Gets a new <see cref="ExifLongArray"/> instance for the <see cref="ExifTagValue.TimeZoneOffset"/> tag.
        /// </summary>
        public static ExifLongArray TimeZoneOffset => new ExifLongArray(ExifTagValue.TimeZoneOffset);

        /// <summary>
        /// Gets a new <see cref="ExifLongArray"/> instance for the <see cref="ExifTagValue.StripOffsets"/> tag.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A new <see cref="ExifLongArray"/> instance.</returns>
        public static ExifLongArray StripOffsets(uint[] value) => ExifLongArray.Create(ExifTagValue.StripOffsets, value);

        /// <summary>
        /// Gets a new <see cref="ExifLongArray"/> instance for the <see cref="ExifTagValue.TileByteCounts"/> tag.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A new <see cref="ExifLongArray"/> instance.</returns>
        public static ExifLongArray TileByteCounts(uint[] value) => ExifLongArray.Create(ExifTagValue.TileByteCounts, value);

        /// <summary>
        /// Gets a new <see cref="ExifLongArray"/> instance for the <see cref="ExifTagValue.ImageLayer"/> tag.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A new <see cref="ExifLongArray"/> instance.</returns>
        public static ExifLongArray ImageLayer(uint[] value) => ExifLongArray.Create(ExifTagValue.ImageLayer, value);
    }
}