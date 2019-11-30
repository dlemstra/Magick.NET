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
        /// Gets the WhitePoint exif tag.
        /// </summary>
        public static ExifTag<Rational[]> WhitePoint { get; } = new ExifTag<Rational[]>(ExifTagValue.WhitePoint);

        /// <summary>
        /// Gets the PrimaryChromaticities exif tag.
        /// </summary>
        public static ExifTag<Rational[]> PrimaryChromaticities { get; } = new ExifTag<Rational[]>(ExifTagValue.PrimaryChromaticities);

        /// <summary>
        /// Gets the YCbCrCoefficients exif tag.
        /// </summary>
        public static ExifTag<Rational[]> YCbCrCoefficients { get; } = new ExifTag<Rational[]>(ExifTagValue.YCbCrCoefficients);

        /// <summary>
        /// Gets the ReferenceBlackWhite exif tag.
        /// </summary>
        public static ExifTag<Rational[]> ReferenceBlackWhite { get; } = new ExifTag<Rational[]>(ExifTagValue.ReferenceBlackWhite);

        /// <summary>
        /// Gets the GPSLatitude exif tag.
        /// </summary>
        public static ExifTag<Rational[]> GPSLatitude { get; } = new ExifTag<Rational[]>(ExifTagValue.GPSLatitude);

        /// <summary>
        /// Gets the GPSLongitude exif tag.
        /// </summary>
        public static ExifTag<Rational[]> GPSLongitude { get; } = new ExifTag<Rational[]>(ExifTagValue.GPSLongitude);

        /// <summary>
        /// Gets the GPSTimestamp exif tag.
        /// </summary>
        public static ExifTag<Rational[]> GPSTimestamp { get; } = new ExifTag<Rational[]>(ExifTagValue.GPSTimestamp);

        /// <summary>
        /// Gets the GPSDestLatitude exif tag.
        /// </summary>
        public static ExifTag<Rational[]> GPSDestLatitude { get; } = new ExifTag<Rational[]>(ExifTagValue.GPSDestLatitude);

        /// <summary>
        /// Gets the GPSDestLongitude exif tag.
        /// </summary>
        public static ExifTag<Rational[]> GPSDestLongitude { get; } = new ExifTag<Rational[]>(ExifTagValue.GPSDestLongitude);
    }
}