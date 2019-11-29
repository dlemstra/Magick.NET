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
        /// Gets a new <see cref="ExifLong"/> instance for the <see cref="ExifTagValue.SubfileType"/> tag.
        /// </summary>
        public static ExifLong SubfileType => new ExifLong(ExifTagValue.SubfileType);

        /// <summary>
        /// Gets a new <see cref="ExifLong"/> instance for the <see cref="ExifTagValue.SubIFDOffset"/> tag.
        /// </summary>
        public static ExifLong SubIFDOffset => new ExifLong(ExifTagValue.SubIFDOffset);

        /// <summary>
        /// Gets a new <see cref="ExifLong"/> instance for the <see cref="ExifTagValue.GPSIFDOffset"/> tag.
        /// </summary>
        public static ExifLong GPSIFDOffset => new ExifLong(ExifTagValue.GPSIFDOffset);

        /// <summary>
        /// Gets a new <see cref="ExifLong"/> instance for the <see cref="ExifTagValue.T4Options"/> tag.
        /// </summary>
        public static ExifLong T4Options => new ExifLong(ExifTagValue.T4Options);

        /// <summary>
        /// Gets a new <see cref="ExifLong"/> instance for the <see cref="ExifTagValue.T6Options"/> tag.
        /// </summary>
        public static ExifLong T6Options => new ExifLong(ExifTagValue.T6Options);

        /// <summary>
        /// Gets a new <see cref="ExifLong"/> instance for the <see cref="ExifTagValue.XClipPathUnits"/> tag.
        /// </summary>
        public static ExifLong XClipPathUnits => new ExifLong(ExifTagValue.XClipPathUnits);

        /// <summary>
        /// Gets a new <see cref="ExifLong"/> instance for the <see cref="ExifTagValue.YClipPathUnits"/> tag.
        /// </summary>
        public static ExifLong YClipPathUnits => new ExifLong(ExifTagValue.YClipPathUnits);

        /// <summary>
        /// Gets a new <see cref="ExifLong"/> instance for the <see cref="ExifTagValue.ProfileType"/> tag.
        /// </summary>
        public static ExifLong ProfileType => new ExifLong(ExifTagValue.ProfileType);

        /// <summary>
        /// Gets a new <see cref="ExifLong"/> instance for the <see cref="ExifTagValue.CodingMethods"/> tag.
        /// </summary>
        public static ExifLong CodingMethods => new ExifLong(ExifTagValue.CodingMethods);

        /// <summary>
        /// Gets a new <see cref="ExifLong"/> instance for the <see cref="ExifTagValue.T82ptions"/> tag.
        /// </summary>
        public static ExifLong T82ptions => new ExifLong(ExifTagValue.T82ptions);

        /// <summary>
        /// Gets a new <see cref="ExifLong"/> instance for the <see cref="ExifTagValue.JPEGInterchangeFormat"/> tag.
        /// </summary>
        public static ExifLong JPEGInterchangeFormat => new ExifLong(ExifTagValue.JPEGInterchangeFormat);

        /// <summary>
        /// Gets a new <see cref="ExifLong"/> instance for the <see cref="ExifTagValue.JPEGInterchangeFormatLength"/> tag.
        /// </summary>
        public static ExifLong JPEGInterchangeFormatLength => new ExifLong(ExifTagValue.JPEGInterchangeFormatLength);

        /// <summary>
        /// Gets a new <see cref="ExifLong"/> instance for the <see cref="ExifTagValue.MDFileTag"/> tag.
        /// </summary>
        public static ExifLong MDFileTag => new ExifLong(ExifTagValue.MDFileTag);

        /// <summary>
        /// Gets a new <see cref="ExifLong"/> instance for the <see cref="ExifTagValue.StandardOutputSensitivity"/> tag.
        /// </summary>
        public static ExifLong StandardOutputSensitivity => new ExifLong(ExifTagValue.StandardOutputSensitivity);

        /// <summary>
        /// Gets a new <see cref="ExifLong"/> instance for the <see cref="ExifTagValue.RecommendedExposureIndex"/> tag.
        /// </summary>
        public static ExifLong RecommendedExposureIndex => new ExifLong(ExifTagValue.RecommendedExposureIndex);

        /// <summary>
        /// Gets a new <see cref="ExifLong"/> instance for the <see cref="ExifTagValue.ISOSpeed"/> tag.
        /// </summary>
        public static ExifLong ISOSpeed => new ExifLong(ExifTagValue.ISOSpeed);

        /// <summary>
        /// Gets a new <see cref="ExifLong"/> instance for the <see cref="ExifTagValue.ISOSpeedLatitudeyyy"/> tag.
        /// </summary>
        public static ExifLong ISOSpeedLatitudeyyy => new ExifLong(ExifTagValue.ISOSpeedLatitudeyyy);

        /// <summary>
        /// Gets a new <see cref="ExifLong"/> instance for the <see cref="ExifTagValue.ISOSpeedLatitudezzz"/> tag.
        /// </summary>
        public static ExifLong ISOSpeedLatitudezzz => new ExifLong(ExifTagValue.ISOSpeedLatitudezzz);

        /// <summary>
        /// Gets a new <see cref="ExifLong"/> instance for the <see cref="ExifTagValue.FaxRecvParams"/> tag.
        /// </summary>
        public static ExifLong FaxRecvParams => new ExifLong(ExifTagValue.FaxRecvParams);

        /// <summary>
        /// Gets a new <see cref="ExifLong"/> instance for the <see cref="ExifTagValue.FaxRecvTime"/> tag.
        /// </summary>
        public static ExifLong FaxRecvTime => new ExifLong(ExifTagValue.FaxRecvTime);

        /// <summary>
        /// Gets a new <see cref="ExifLong"/> instance for the <see cref="ExifTagValue.ImageNumber"/> tag.
        /// </summary>
        public static ExifLong ImageNumber => new ExifLong(ExifTagValue.ImageNumber);

        /// <summary>
        /// Gets a new <see cref="ExifLong"/> instance for the <see cref="ExifTagValue.ImageWidth"/> tag.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A new <see cref="ExifLong"/> instance.</returns>
        public static ExifLong ImageWidth(uint value) => ExifLong.Create(ExifTagValue.ImageWidth, value);

        /// <summary>
        /// Gets a new <see cref="ExifLong"/> instance for the <see cref="ExifTagValue.ImageLength"/> tag.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A new <see cref="ExifLong"/> instance.</returns>
        public static ExifLong ImageLength(uint value) => ExifLong.Create(ExifTagValue.ImageLength, value);

        /// <summary>
        /// Gets a new <see cref="ExifLong"/> instance for the <see cref="ExifTagValue.TileWidth"/> tag.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A new <see cref="ExifLong"/> instance.</returns>
        public static ExifLong TileWidth(uint value) => ExifLong.Create(ExifTagValue.TileWidth, value);

        /// <summary>
        /// Gets a new <see cref="ExifLong"/> instance for the <see cref="ExifTagValue.TileLength"/> tag.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A new <see cref="ExifLong"/> instance.</returns>
        public static ExifLong TileLength(uint value) => ExifLong.Create(ExifTagValue.TileLength, value);

        /// <summary>
        /// Gets a new <see cref="ExifLong"/> instance for the <see cref="ExifTagValue.BadFaxLines"/> tag.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A new <see cref="ExifLong"/> instance.</returns>
        public static ExifLong BadFaxLines(uint value) => ExifLong.Create(ExifTagValue.BadFaxLines, value);

        /// <summary>
        /// Gets a new <see cref="ExifLong"/> instance for the <see cref="ExifTagValue.ConsecutiveBadFaxLines"/> tag.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A new <see cref="ExifLong"/> instance.</returns>
        public static ExifLong ConsecutiveBadFaxLines(uint value) => ExifLong.Create(ExifTagValue.ConsecutiveBadFaxLines, value);

        /// <summary>
        /// Gets a new <see cref="ExifLong"/> instance for the <see cref="ExifTagValue.PixelXDimension"/> tag.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A new <see cref="ExifLong"/> instance.</returns>
        public static ExifLong PixelXDimension(uint value) => ExifLong.Create(ExifTagValue.PixelXDimension, value);

        /// <summary>
        /// Gets a new <see cref="ExifLong"/> instance for the <see cref="ExifTagValue.PixelYDimension"/> tag.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A new <see cref="ExifLong"/> instance.</returns>
        public static ExifLong PixelYDimension(uint value) => ExifLong.Create(ExifTagValue.PixelYDimension, value);
    }
}