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
    /// <content/>
    public abstract partial class ExifTag
    {
        /// <summary>
        /// Gets the FreeOffsets exif tag.
        /// </summary>
        public static ExifTag<uint[]> FreeOffsets { get; } = new ExifTag<uint[]>(ExifTagValue.FreeOffsets);

        /// <summary>
        /// Gets the FreeByteCounts exif tag.
        /// </summary>
        public static ExifTag<uint[]> FreeByteCounts { get; } = new ExifTag<uint[]>(ExifTagValue.FreeByteCounts);

        /// <summary>
        /// Gets the ColorResponseUnit exif tag.
        /// </summary>
        public static ExifTag<uint[]> ColorResponseUnit { get; } = new ExifTag<uint[]>(ExifTagValue.ColorResponseUnit);

        /// <summary>
        /// Gets the TileOffsets exif tag.
        /// </summary>
        public static ExifTag<uint[]> TileOffsets { get; } = new ExifTag<uint[]>(ExifTagValue.TileOffsets);

        /// <summary>
        /// Gets the SMinSampleValue exif tag.
        /// </summary>
        public static ExifTag<uint[]> SMinSampleValue { get; } = new ExifTag<uint[]>(ExifTagValue.SMinSampleValue);

        /// <summary>
        /// Gets the SMaxSampleValue exif tag.
        /// </summary>
        public static ExifTag<uint[]> SMaxSampleValue { get; } = new ExifTag<uint[]>(ExifTagValue.SMaxSampleValue);

        /// <summary>
        /// Gets the JPEGQTables exif tag.
        /// </summary>
        public static ExifTag<uint[]> JPEGQTables { get; } = new ExifTag<uint[]>(ExifTagValue.JPEGQTables);

        /// <summary>
        /// Gets the JPEGDCTables exif tag.
        /// </summary>
        public static ExifTag<uint[]> JPEGDCTables { get; } = new ExifTag<uint[]>(ExifTagValue.JPEGDCTables);

        /// <summary>
        /// Gets the JPEGACTables exif tag.
        /// </summary>
        public static ExifTag<uint[]> JPEGACTables { get; } = new ExifTag<uint[]>(ExifTagValue.JPEGACTables);

        /// <summary>
        /// Gets the StripRowCounts exif tag.
        /// </summary>
        public static ExifTag<uint[]> StripRowCounts { get; } = new ExifTag<uint[]>(ExifTagValue.StripRowCounts);

        /// <summary>
        /// Gets the IntergraphRegisters exif tag.
        /// </summary>
        public static ExifTag<uint[]> IntergraphRegisters { get; } = new ExifTag<uint[]>(ExifTagValue.IntergraphRegisters);

        /// <summary>
        /// Gets the TimeZoneOffset exif tag.
        /// </summary>
        public static ExifTag<uint[]> TimeZoneOffset { get; } = new ExifTag<uint[]>(ExifTagValue.TimeZoneOffset);
    }
}