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

namespace ImageMagick
{
    /// <content/>
    public abstract partial class ExifTag
    {
        /// <summary>
        /// Gets the StripOffsets exif tag.
        /// </summary>
        public static ExifTag<Number[]> StripOffsets { get; } = new ExifTag<Number[]>(ExifTagValue.StripOffsets);

        /// <summary>
        /// Gets the TileByteCounts exif tag.
        /// </summary>
        public static ExifTag<Number[]> TileByteCounts { get; } = new ExifTag<Number[]>(ExifTagValue.TileByteCounts);

        /// <summary>
        /// Gets the ImageLayer exif tag.
        /// </summary>
        public static ExifTag<Number[]> ImageLayer { get; } = new ExifTag<Number[]>(ExifTagValue.ImageLayer);
    }
}