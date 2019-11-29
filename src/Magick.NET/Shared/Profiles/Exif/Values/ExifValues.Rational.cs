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
        /// Gets a new <see cref="ExifRational"/> instance for the <see cref="ExifTagValue.XPosition"/> tag.
        /// </summary>
        public static ExifRational XPosition => new ExifRational(ExifTagValue.XPosition);

        /// <summary>
        /// Gets a new <see cref="ExifRational"/> instance for the <see cref="ExifTagValue.YPosition"/> tag.
        /// </summary>
        public static ExifRational YPosition => new ExifRational(ExifTagValue.YPosition);

        /// <summary>
        /// Gets a new <see cref="ExifRational"/> instance for the <see cref="ExifTagValue.XResolution"/> tag.
        /// </summary>
        public static ExifRational XResolution => new ExifRational(ExifTagValue.XResolution);

        /// <summary>
        /// Gets a new <see cref="ExifRational"/> instance for the <see cref="ExifTagValue.YResolution"/> tag.
        /// </summary>
        public static ExifRational YResolution => new ExifRational(ExifTagValue.YResolution);

        /// <summary>
        /// Gets a new <see cref="ExifRational"/> instance for the <see cref="ExifTagValue.BatteryLevel"/> tag.
        /// </summary>
        public static ExifRational BatteryLevel => new ExifRational(ExifTagValue.BatteryLevel);

        /// <summary>
        /// Gets a new <see cref="ExifRational"/> instance for the <see cref="ExifTagValue.ExposureTime"/> tag.
        /// </summary>
        public static ExifRational ExposureTime => new ExifRational(ExifTagValue.ExposureTime);

        /// <summary>
        /// Gets a new <see cref="ExifRational"/> instance for the <see cref="ExifTagValue.FNumber"/> tag.
        /// </summary>
        public static ExifRational FNumber => new ExifRational(ExifTagValue.FNumber);

        /// <summary>
        /// Gets a new <see cref="ExifRational"/> instance for the <see cref="ExifTagValue.MDScalePixel"/> tag.
        /// </summary>
        public static ExifRational MDScalePixel => new ExifRational(ExifTagValue.MDScalePixel);

        /// <summary>
        /// Gets a new <see cref="ExifRational"/> instance for the <see cref="ExifTagValue.CompressedBitsPerPixel"/> tag.
        /// </summary>
        public static ExifRational CompressedBitsPerPixel => new ExifRational(ExifTagValue.CompressedBitsPerPixel);

        /// <summary>
        /// Gets a new <see cref="ExifRational"/> instance for the <see cref="ExifTagValue.ApertureValue"/> tag.
        /// </summary>
        public static ExifRational ApertureValue => new ExifRational(ExifTagValue.ApertureValue);

        /// <summary>
        /// Gets a new <see cref="ExifRational"/> instance for the <see cref="ExifTagValue.MaxApertureValue"/> tag.
        /// </summary>
        public static ExifRational MaxApertureValue => new ExifRational(ExifTagValue.MaxApertureValue);

        /// <summary>
        /// Gets a new <see cref="ExifRational"/> instance for the <see cref="ExifTagValue.SubjectDistance"/> tag.
        /// </summary>
        public static ExifRational SubjectDistance => new ExifRational(ExifTagValue.SubjectDistance);

        /// <summary>
        /// Gets a new <see cref="ExifRational"/> instance for the <see cref="ExifTagValue.FocalLength"/> tag.
        /// </summary>
        public static ExifRational FocalLength => new ExifRational(ExifTagValue.FocalLength);

        /// <summary>
        /// Gets a new <see cref="ExifRational"/> instance for the <see cref="ExifTagValue.FlashEnergy2"/> tag.
        /// </summary>
        public static ExifRational FlashEnergy2 => new ExifRational(ExifTagValue.FlashEnergy2);

        /// <summary>
        /// Gets a new <see cref="ExifRational"/> instance for the <see cref="ExifTagValue.FocalPlaneXResolution2"/> tag.
        /// </summary>
        public static ExifRational FocalPlaneXResolution2 => new ExifRational(ExifTagValue.FocalPlaneXResolution2);

        /// <summary>
        /// Gets a new <see cref="ExifRational"/> instance for the <see cref="ExifTagValue.FocalPlaneYResolution2"/> tag.
        /// </summary>
        public static ExifRational FocalPlaneYResolution2 => new ExifRational(ExifTagValue.FocalPlaneYResolution2);

        /// <summary>
        /// Gets a new <see cref="ExifRational"/> instance for the <see cref="ExifTagValue.ExposureIndex2"/> tag.
        /// </summary>
        public static ExifRational ExposureIndex2 => new ExifRational(ExifTagValue.ExposureIndex2);

        /// <summary>
        /// Gets a new <see cref="ExifRational"/> instance for the <see cref="ExifTagValue.Humidity"/> tag.
        /// </summary>
        public static ExifRational Humidity => new ExifRational(ExifTagValue.Humidity);

        /// <summary>
        /// Gets a new <see cref="ExifRational"/> instance for the <see cref="ExifTagValue.Pressure"/> tag.
        /// </summary>
        public static ExifRational Pressure => new ExifRational(ExifTagValue.Pressure);

        /// <summary>
        /// Gets a new <see cref="ExifRational"/> instance for the <see cref="ExifTagValue.Acceleration"/> tag.
        /// </summary>
        public static ExifRational Acceleration => new ExifRational(ExifTagValue.Acceleration);

        /// <summary>
        /// Gets a new <see cref="ExifRational"/> instance for the <see cref="ExifTagValue.FlashEnergy"/> tag.
        /// </summary>
        public static ExifRational FlashEnergy => new ExifRational(ExifTagValue.FlashEnergy);

        /// <summary>
        /// Gets a new <see cref="ExifRational"/> instance for the <see cref="ExifTagValue.FocalPlaneXResolution"/> tag.
        /// </summary>
        public static ExifRational FocalPlaneXResolution => new ExifRational(ExifTagValue.FocalPlaneXResolution);

        /// <summary>
        /// Gets a new <see cref="ExifRational"/> instance for the <see cref="ExifTagValue.FocalPlaneYResolution"/> tag.
        /// </summary>
        public static ExifRational FocalPlaneYResolution => new ExifRational(ExifTagValue.FocalPlaneYResolution);

        /// <summary>
        /// Gets a new <see cref="ExifRational"/> instance for the <see cref="ExifTagValue.ExposureIndex"/> tag.
        /// </summary>
        public static ExifRational ExposureIndex => new ExifRational(ExifTagValue.ExposureIndex);

        /// <summary>
        /// Gets a new <see cref="ExifRational"/> instance for the <see cref="ExifTagValue.DigitalZoomRatio"/> tag.
        /// </summary>
        public static ExifRational DigitalZoomRatio => new ExifRational(ExifTagValue.DigitalZoomRatio);

        /// <summary>
        /// Gets a new <see cref="ExifRational"/> instance for the <see cref="ExifTagValue.LensInfo"/> tag.
        /// </summary>
        public static ExifRational LensInfo => new ExifRational(ExifTagValue.LensInfo);

        /// <summary>
        /// Gets a new <see cref="ExifRational"/> instance for the <see cref="ExifTagValue.GPSAltitude"/> tag.
        /// </summary>
        public static ExifRational GPSAltitude => new ExifRational(ExifTagValue.GPSAltitude);

        /// <summary>
        /// Gets a new <see cref="ExifRational"/> instance for the <see cref="ExifTagValue.GPSDOP"/> tag.
        /// </summary>
        public static ExifRational GPSDOP => new ExifRational(ExifTagValue.GPSDOP);

        /// <summary>
        /// Gets a new <see cref="ExifRational"/> instance for the <see cref="ExifTagValue.GPSSpeed"/> tag.
        /// </summary>
        public static ExifRational GPSSpeed => new ExifRational(ExifTagValue.GPSSpeed);

        /// <summary>
        /// Gets a new <see cref="ExifRational"/> instance for the <see cref="ExifTagValue.GPSTrack"/> tag.
        /// </summary>
        public static ExifRational GPSTrack => new ExifRational(ExifTagValue.GPSTrack);

        /// <summary>
        /// Gets a new <see cref="ExifRational"/> instance for the <see cref="ExifTagValue.GPSImgDirection"/> tag.
        /// </summary>
        public static ExifRational GPSImgDirection => new ExifRational(ExifTagValue.GPSImgDirection);

        /// <summary>
        /// Gets a new <see cref="ExifRational"/> instance for the <see cref="ExifTagValue.GPSDestBearing"/> tag.
        /// </summary>
        public static ExifRational GPSDestBearing => new ExifRational(ExifTagValue.GPSDestBearing);

        /// <summary>
        /// Gets a new <see cref="ExifRational"/> instance for the <see cref="ExifTagValue.GPSDestDistance"/> tag.
        /// </summary>
        public static ExifRational GPSDestDistance => new ExifRational(ExifTagValue.GPSDestDistance);
    }
}