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
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTagValue.ImageDescription"/> tag.
        /// </summary>
        public static ExifString ImageDescription => new ExifString(ExifTagValue.ImageDescription);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTagValue.Make"/> tag.
        /// </summary>
        public static ExifString Make => new ExifString(ExifTagValue.Make);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTagValue.Model"/> tag.
        /// </summary>
        public static ExifString Model => new ExifString(ExifTagValue.Model);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTagValue.Software"/> tag.
        /// </summary>
        public static ExifString Software => new ExifString(ExifTagValue.Software);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTagValue.DateTime"/> tag.
        /// </summary>
        public static ExifString DateTime => new ExifString(ExifTagValue.DateTime);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTagValue.Artist"/> tag.
        /// </summary>
        public static ExifString Artist => new ExifString(ExifTagValue.Artist);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTagValue.HostComputer"/> tag.
        /// </summary>
        public static ExifString HostComputer => new ExifString(ExifTagValue.HostComputer);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTagValue.Copyright"/> tag.
        /// </summary>
        public static ExifString Copyright => new ExifString(ExifTagValue.Copyright);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTagValue.DocumentName"/> tag.
        /// </summary>
        public static ExifString DocumentName => new ExifString(ExifTagValue.DocumentName);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTagValue.PageName"/> tag.
        /// </summary>
        public static ExifString PageName => new ExifString(ExifTagValue.PageName);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTagValue.InkNames"/> tag.
        /// </summary>
        public static ExifString InkNames => new ExifString(ExifTagValue.InkNames);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTagValue.TargetPrinter"/> tag.
        /// </summary>
        public static ExifString TargetPrinter => new ExifString(ExifTagValue.TargetPrinter);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTagValue.ImageID"/> tag.
        /// </summary>
        public static ExifString ImageID => new ExifString(ExifTagValue.ImageID);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTagValue.MDLabName"/> tag.
        /// </summary>
        public static ExifString MDLabName => new ExifString(ExifTagValue.MDLabName);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTagValue.MDSampleInfo"/> tag.
        /// </summary>
        public static ExifString MDSampleInfo => new ExifString(ExifTagValue.MDSampleInfo);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTagValue.MDPrepDate"/> tag.
        /// </summary>
        public static ExifString MDPrepDate => new ExifString(ExifTagValue.MDPrepDate);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTagValue.MDPrepTime"/> tag.
        /// </summary>
        public static ExifString MDPrepTime => new ExifString(ExifTagValue.MDPrepTime);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTagValue.MDFileUnits"/> tag.
        /// </summary>
        public static ExifString MDFileUnits => new ExifString(ExifTagValue.MDFileUnits);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTagValue.SEMInfo"/> tag.
        /// </summary>
        public static ExifString SEMInfo => new ExifString(ExifTagValue.SEMInfo);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTagValue.SpectralSensitivity"/> tag.
        /// </summary>
        public static ExifString SpectralSensitivity => new ExifString(ExifTagValue.SpectralSensitivity);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTagValue.DateTimeOriginal"/> tag.
        /// </summary>
        public static ExifString DateTimeOriginal => new ExifString(ExifTagValue.DateTimeOriginal);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTagValue.DateTimeDigitized"/> tag.
        /// </summary>
        public static ExifString DateTimeDigitized => new ExifString(ExifTagValue.DateTimeDigitized);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTagValue.SubsecTime"/> tag.
        /// </summary>
        public static ExifString SubsecTime => new ExifString(ExifTagValue.SubsecTime);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTagValue.SubsecTimeOriginal"/> tag.
        /// </summary>
        public static ExifString SubsecTimeOriginal => new ExifString(ExifTagValue.SubsecTimeOriginal);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTagValue.SubsecTimeDigitized"/> tag.
        /// </summary>
        public static ExifString SubsecTimeDigitized => new ExifString(ExifTagValue.SubsecTimeDigitized);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTagValue.RelatedSoundFile"/> tag.
        /// </summary>
        public static ExifString RelatedSoundFile => new ExifString(ExifTagValue.RelatedSoundFile);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTagValue.FaxSubaddress"/> tag.
        /// </summary>
        public static ExifString FaxSubaddress => new ExifString(ExifTagValue.FaxSubaddress);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTagValue.OffsetTime"/> tag.
        /// </summary>
        public static ExifString OffsetTime => new ExifString(ExifTagValue.OffsetTime);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTagValue.OffsetTimeOriginal"/> tag.
        /// </summary>
        public static ExifString OffsetTimeOriginal => new ExifString(ExifTagValue.OffsetTimeOriginal);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTagValue.OffsetTimeDigitized"/> tag.
        /// </summary>
        public static ExifString OffsetTimeDigitized => new ExifString(ExifTagValue.OffsetTimeDigitized);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTagValue.SecurityClassification"/> tag.
        /// </summary>
        public static ExifString SecurityClassification => new ExifString(ExifTagValue.SecurityClassification);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTagValue.ImageHistory"/> tag.
        /// </summary>
        public static ExifString ImageHistory => new ExifString(ExifTagValue.ImageHistory);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTagValue.ImageUniqueID"/> tag.
        /// </summary>
        public static ExifString ImageUniqueID => new ExifString(ExifTagValue.ImageUniqueID);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTagValue.OwnerName"/> tag.
        /// </summary>
        public static ExifString OwnerName => new ExifString(ExifTagValue.OwnerName);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTagValue.SerialNumber"/> tag.
        /// </summary>
        public static ExifString SerialNumber => new ExifString(ExifTagValue.SerialNumber);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTagValue.LensMake"/> tag.
        /// </summary>
        public static ExifString LensMake => new ExifString(ExifTagValue.LensMake);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTagValue.LensModel"/> tag.
        /// </summary>
        public static ExifString LensModel => new ExifString(ExifTagValue.LensModel);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTagValue.LensSerialNumber"/> tag.
        /// </summary>
        public static ExifString LensSerialNumber => new ExifString(ExifTagValue.LensSerialNumber);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTagValue.GDALMetadata"/> tag.
        /// </summary>
        public static ExifString GDALMetadata => new ExifString(ExifTagValue.GDALMetadata);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTagValue.GDALNoData"/> tag.
        /// </summary>
        public static ExifString GDALNoData => new ExifString(ExifTagValue.GDALNoData);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTagValue.GPSLatitudeRef"/> tag.
        /// </summary>
        public static ExifString GPSLatitudeRef => new ExifString(ExifTagValue.GPSLatitudeRef);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTagValue.GPSLongitudeRef"/> tag.
        /// </summary>
        public static ExifString GPSLongitudeRef => new ExifString(ExifTagValue.GPSLongitudeRef);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTagValue.GPSSatellites"/> tag.
        /// </summary>
        public static ExifString GPSSatellites => new ExifString(ExifTagValue.GPSSatellites);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTagValue.GPSStatus"/> tag.
        /// </summary>
        public static ExifString GPSStatus => new ExifString(ExifTagValue.GPSStatus);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTagValue.GPSMeasureMode"/> tag.
        /// </summary>
        public static ExifString GPSMeasureMode => new ExifString(ExifTagValue.GPSMeasureMode);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTagValue.GPSSpeedRef"/> tag.
        /// </summary>
        public static ExifString GPSSpeedRef => new ExifString(ExifTagValue.GPSSpeedRef);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTagValue.GPSTrackRef"/> tag.
        /// </summary>
        public static ExifString GPSTrackRef => new ExifString(ExifTagValue.GPSTrackRef);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTagValue.GPSImgDirectionRef"/> tag.
        /// </summary>
        public static ExifString GPSImgDirectionRef => new ExifString(ExifTagValue.GPSImgDirectionRef);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTagValue.GPSMapDatum"/> tag.
        /// </summary>
        public static ExifString GPSMapDatum => new ExifString(ExifTagValue.GPSMapDatum);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTagValue.GPSDestLatitudeRef"/> tag.
        /// </summary>
        public static ExifString GPSDestLatitudeRef => new ExifString(ExifTagValue.GPSDestLatitudeRef);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTagValue.GPSDestLongitudeRef"/> tag.
        /// </summary>
        public static ExifString GPSDestLongitudeRef => new ExifString(ExifTagValue.GPSDestLongitudeRef);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTagValue.GPSDestBearingRef"/> tag.
        /// </summary>
        public static ExifString GPSDestBearingRef => new ExifString(ExifTagValue.GPSDestBearingRef);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTagValue.GPSDestDistanceRef"/> tag.
        /// </summary>
        public static ExifString GPSDestDistanceRef => new ExifString(ExifTagValue.GPSDestDistanceRef);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTagValue.GPSDateStamp"/> tag.
        /// </summary>
        public static ExifString GPSDateStamp => new ExifString(ExifTagValue.GPSDateStamp);
    }
}
