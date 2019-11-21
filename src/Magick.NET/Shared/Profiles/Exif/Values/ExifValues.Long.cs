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
        /// Gets a new <see cref="ExifLong"/> instance for the <see cref="ExifTag.SubfileType"/> tag.
        /// </summary>
        public static ExifLong SubfileType => new ExifLong(ExifTag.SubfileType);

        /// <summary>
        /// Gets a new <see cref="ExifLong"/> instance for the <see cref="ExifTag.SubIFDOffset"/> tag.
        /// </summary>
        public static ExifLong SubIFDOffset => new ExifLong(ExifTag.SubIFDOffset);

        /// <summary>
        /// Gets a new <see cref="ExifLong"/> instance for the <see cref="ExifTag.GPSIFDOffset"/> tag.
        /// </summary>
        public static ExifLong GPSIFDOffset => new ExifLong(ExifTag.GPSIFDOffset);

        /// <summary>
        /// Gets a new <see cref="ExifLong"/> instance for the <see cref="ExifTag.T4Options"/> tag.
        /// </summary>
        public static ExifLong T4Options => new ExifLong(ExifTag.T4Options);

        /// <summary>
        /// Gets a new <see cref="ExifLong"/> instance for the <see cref="ExifTag.T6Options"/> tag.
        /// </summary>
        public static ExifLong T6Options => new ExifLong(ExifTag.T6Options);

        /// <summary>
        /// Gets a new <see cref="ExifLong"/> instance for the <see cref="ExifTag.XClipPathUnits"/> tag.
        /// </summary>
        public static ExifLong XClipPathUnits => new ExifLong(ExifTag.XClipPathUnits);

        /// <summary>
        /// Gets a new <see cref="ExifLong"/> instance for the <see cref="ExifTag.YClipPathUnits"/> tag.
        /// </summary>
        public static ExifLong YClipPathUnits => new ExifLong(ExifTag.YClipPathUnits);

        /// <summary>
        /// Gets a new <see cref="ExifLong"/> instance for the <see cref="ExifTag.ProfileType"/> tag.
        /// </summary>
        public static ExifLong ProfileType => new ExifLong(ExifTag.ProfileType);

        /// <summary>
        /// Gets a new <see cref="ExifLong"/> instance for the <see cref="ExifTag.CodingMethods"/> tag.
        /// </summary>
        public static ExifLong CodingMethods => new ExifLong(ExifTag.CodingMethods);

        /// <summary>
        /// Gets a new <see cref="ExifLong"/> instance for the <see cref="ExifTag.T82ptions"/> tag.
        /// </summary>
        public static ExifLong T82ptions => new ExifLong(ExifTag.T82ptions);

        /// <summary>
        /// Gets a new <see cref="ExifLong"/> instance for the <see cref="ExifTag.JPEGInterchangeFormat"/> tag.
        /// </summary>
        public static ExifLong JPEGInterchangeFormat => new ExifLong(ExifTag.JPEGInterchangeFormat);

        /// <summary>
        /// Gets a new <see cref="ExifLong"/> instance for the <see cref="ExifTag.JPEGInterchangeFormatLength"/> tag.
        /// </summary>
        public static ExifLong JPEGInterchangeFormatLength => new ExifLong(ExifTag.JPEGInterchangeFormatLength);

        /// <summary>
        /// Gets a new <see cref="ExifLong"/> instance for the <see cref="ExifTag.MDFileTag"/> tag.
        /// </summary>
        public static ExifLong MDFileTag => new ExifLong(ExifTag.MDFileTag);

        /// <summary>
        /// Gets a new <see cref="ExifLong"/> instance for the <see cref="ExifTag.StandardOutputSensitivity"/> tag.
        /// </summary>
        public static ExifLong StandardOutputSensitivity => new ExifLong(ExifTag.StandardOutputSensitivity);

        /// <summary>
        /// Gets a new <see cref="ExifLong"/> instance for the <see cref="ExifTag.RecommendedExposureIndex"/> tag.
        /// </summary>
        public static ExifLong RecommendedExposureIndex => new ExifLong(ExifTag.RecommendedExposureIndex);

        /// <summary>
        /// Gets a new <see cref="ExifLong"/> instance for the <see cref="ExifTag.ISOSpeed"/> tag.
        /// </summary>
        public static ExifLong ISOSpeed => new ExifLong(ExifTag.ISOSpeed);

        /// <summary>
        /// Gets a new <see cref="ExifLong"/> instance for the <see cref="ExifTag.ISOSpeedLatitudeyyy"/> tag.
        /// </summary>
        public static ExifLong ISOSpeedLatitudeyyy => new ExifLong(ExifTag.ISOSpeedLatitudeyyy);

        /// <summary>
        /// Gets a new <see cref="ExifLong"/> instance for the <see cref="ExifTag.ISOSpeedLatitudezzz"/> tag.
        /// </summary>
        public static ExifLong ISOSpeedLatitudezzz => new ExifLong(ExifTag.ISOSpeedLatitudezzz);

        /// <summary>
        /// Gets a new <see cref="ExifLong"/> instance for the <see cref="ExifTag.FaxRecvParams"/> tag.
        /// </summary>
        public static ExifLong FaxRecvParams => new ExifLong(ExifTag.FaxRecvParams);

        /// <summary>
        /// Gets a new <see cref="ExifLong"/> instance for the <see cref="ExifTag.FaxRecvTime"/> tag.
        /// </summary>
        public static ExifLong FaxRecvTime => new ExifLong(ExifTag.FaxRecvTime);

        /// <summary>
        /// Gets a new <see cref="ExifLong"/> instance for the <see cref="ExifTag.ImageNumber"/> tag.
        /// </summary>
        public static ExifLong ImageNumber => new ExifLong(ExifTag.ImageNumber);

        /// <summary>
        /// Gets a new <see cref="ExifLong"/> instance for the <see cref="ExifTag.ImageWidth"/> tag.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A new <see cref="ExifLong"/> instance.</returns>
        public static ExifLong ImageWidth(uint value) => ExifLong.Create(ExifTag.ImageWidth, value);

        /// <summary>
        /// Gets a new <see cref="ExifLong"/> instance for the <see cref="ExifTag.ImageLength"/> tag.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A new <see cref="ExifLong"/> instance.</returns>
        public static ExifLong ImageLength(uint value) => ExifLong.Create(ExifTag.ImageLength, value);

        /// <summary>
        /// Gets a new <see cref="ExifLong"/> instance for the <see cref="ExifTag.TileWidth"/> tag.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A new <see cref="ExifLong"/> instance.</returns>
        public static ExifLong TileWidth(uint value) => ExifLong.Create(ExifTag.TileWidth, value);

        /// <summary>
        /// Gets a new <see cref="ExifLong"/> instance for the <see cref="ExifTag.TileLength"/> tag.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A new <see cref="ExifLong"/> instance.</returns>
        public static ExifLong TileLength(uint value) => ExifLong.Create(ExifTag.TileLength, value);

        /// <summary>
        /// Gets a new <see cref="ExifLong"/> instance for the <see cref="ExifTag.BadFaxLines"/> tag.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A new <see cref="ExifLong"/> instance.</returns>
        public static ExifLong BadFaxLines(uint value) => ExifLong.Create(ExifTag.BadFaxLines, value);

        /// <summary>
        /// Gets a new <see cref="ExifLong"/> instance for the <see cref="ExifTag.ConsecutiveBadFaxLines"/> tag.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A new <see cref="ExifLong"/> instance.</returns>
        public static ExifLong ConsecutiveBadFaxLines(uint value) => ExifLong.Create(ExifTag.ConsecutiveBadFaxLines, value);

        /// <summary>
        /// Gets a new <see cref="ExifLong"/> instance for the <see cref="ExifTag.PixelXDimension"/> tag.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A new <see cref="ExifLong"/> instance.</returns>
        public static ExifLong PixelXDimension(uint value) => ExifLong.Create(ExifTag.PixelXDimension, value);

        /// <summary>
        /// Gets a new <see cref="ExifLong"/> instance for the <see cref="ExifTag.PixelYDimension"/> tag.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A new <see cref="ExifLong"/> instance.</returns>
        public static ExifLong PixelYDimension(uint value) => ExifLong.Create(ExifTag.PixelYDimension, value);
    }
}