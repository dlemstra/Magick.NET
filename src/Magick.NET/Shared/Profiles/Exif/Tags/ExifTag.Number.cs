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
    /// <content/>
    public abstract partial class ExifTag
    {
        /// <summary>
        /// Gets the ImageWidth exif tag.
        /// </summary>
        public static ExifTag<ExifNumber> ImageWidth { get; } = new ExifTag<ExifNumber>(ExifTagValue.ImageWidth);

        /// <summary>
        /// Gets the ImageLength exif tag.
        /// </summary>
        public static ExifTag<ExifNumber> ImageLength { get; } = new ExifTag<ExifNumber>(ExifTagValue.ImageLength);

        /// <summary>
        /// Gets the TileWidth exif tag.
        /// </summary>
        public static ExifTag<ExifNumber> TileWidth { get; } = new ExifTag<ExifNumber>(ExifTagValue.TileWidth);

        /// <summary>
        /// Gets the TileLength exif tag.
        /// </summary>
        public static ExifTag<ExifNumber> TileLength { get; } = new ExifTag<ExifNumber>(ExifTagValue.TileLength);

        /// <summary>
        /// Gets the BadFaxLines exif tag.
        /// </summary>
        public static ExifTag<ExifNumber> BadFaxLines { get; } = new ExifTag<ExifNumber>(ExifTagValue.BadFaxLines);

        /// <summary>
        /// Gets the ConsecutiveBadFaxLines exif tag.
        /// </summary>
        public static ExifTag<ExifNumber> ConsecutiveBadFaxLines { get; } = new ExifTag<ExifNumber>(ExifTagValue.ConsecutiveBadFaxLines);

        /// <summary>
        /// Gets the PixelXDimension exif tag.
        /// </summary>
        public static ExifTag<ExifNumber> PixelXDimension { get; } = new ExifTag<ExifNumber>(ExifTagValue.PixelXDimension);

        /// <summary>
        /// Gets the PixelYDimension exif tag.
        /// </summary>
        public static ExifTag<ExifNumber> PixelYDimension { get; } = new ExifTag<ExifNumber>(ExifTagValue.PixelYDimension);
    }
}