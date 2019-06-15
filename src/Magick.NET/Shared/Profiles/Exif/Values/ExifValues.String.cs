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
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTag.ImageDescription"/> tag.
        /// </summary>
        public static ExifString ImageDescription => new ExifString(ExifTag.ImageDescription);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTag.Make"/> tag.
        /// </summary>
        public static ExifString Make => new ExifString(ExifTag.Make);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTag.Model"/> tag.
        /// </summary>
        public static ExifString Model => new ExifString(ExifTag.Model);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTag.Software"/> tag.
        /// </summary>
        public static ExifString Software => new ExifString(ExifTag.Software);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTag.DateTime"/> tag.
        /// </summary>
        public static ExifString DateTime => new ExifString(ExifTag.DateTime);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTag.Artist"/> tag.
        /// </summary>
        public static ExifString Artist => new ExifString(ExifTag.Artist);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTag.HostComputer"/> tag.
        /// </summary>
        public static ExifString HostComputer => new ExifString(ExifTag.HostComputer);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTag.Copyright"/> tag.
        /// </summary>
        public static ExifString Copyright => new ExifString(ExifTag.Copyright);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTag.DocumentName"/> tag.
        /// </summary>
        public static ExifString DocumentName => new ExifString(ExifTag.DocumentName);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTag.PageName"/> tag.
        /// </summary>
        public static ExifString PageName => new ExifString(ExifTag.PageName);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTag.InkNames"/> tag.
        /// </summary>
        public static ExifString InkNames => new ExifString(ExifTag.InkNames);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTag.TargetPrinter"/> tag.
        /// </summary>
        public static ExifString TargetPrinter => new ExifString(ExifTag.TargetPrinter);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTag.ImageID"/> tag.
        /// </summary>
        public static ExifString ImageID => new ExifString(ExifTag.ImageID);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTag.MDLabName"/> tag.
        /// </summary>
        public static ExifString MDLabName => new ExifString(ExifTag.MDLabName);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTag.MDSampleInfo"/> tag.
        /// </summary>
        public static ExifString MDSampleInfo => new ExifString(ExifTag.MDSampleInfo);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTag.MDPrepDate"/> tag.
        /// </summary>
        public static ExifString MDPrepDate => new ExifString(ExifTag.MDPrepDate);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTag.MDPrepTime"/> tag.
        /// </summary>
        public static ExifString MDPrepTime => new ExifString(ExifTag.MDPrepTime);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTag.MDFileUnits"/> tag.
        /// </summary>
        public static ExifString MDFileUnits => new ExifString(ExifTag.MDFileUnits);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTag.SEMInfo"/> tag.
        /// </summary>
        public static ExifString SEMInfo => new ExifString(ExifTag.SEMInfo);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTag.SpectralSensitivity"/> tag.
        /// </summary>
        public static ExifString SpectralSensitivity => new ExifString(ExifTag.SpectralSensitivity);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTag.DateTimeOriginal"/> tag.
        /// </summary>
        public static ExifString DateTimeOriginal => new ExifString(ExifTag.DateTimeOriginal);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTag.DateTimeDigitized"/> tag.
        /// </summary>
        public static ExifString DateTimeDigitized => new ExifString(ExifTag.DateTimeDigitized);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTag.SubsecTime"/> tag.
        /// </summary>
        public static ExifString SubsecTime => new ExifString(ExifTag.SubsecTime);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTag.SubsecTimeOriginal"/> tag.
        /// </summary>
        public static ExifString SubsecTimeOriginal => new ExifString(ExifTag.SubsecTimeOriginal);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTag.SubsecTimeDigitized"/> tag.
        /// </summary>
        public static ExifString SubsecTimeDigitized => new ExifString(ExifTag.SubsecTimeDigitized);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTag.RelatedSoundFile"/> tag.
        /// </summary>
        public static ExifString RelatedSoundFile => new ExifString(ExifTag.RelatedSoundFile);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTag.FaxSubaddress"/> tag.
        /// </summary>
        public static ExifString FaxSubaddress => new ExifString(ExifTag.FaxSubaddress);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTag.OffsetTime"/> tag.
        /// </summary>
        public static ExifString OffsetTime => new ExifString(ExifTag.OffsetTime);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTag.OffsetTimeOriginal"/> tag.
        /// </summary>
        public static ExifString OffsetTimeOriginal => new ExifString(ExifTag.OffsetTimeOriginal);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTag.OffsetTimeDigitized"/> tag.
        /// </summary>
        public static ExifString OffsetTimeDigitized => new ExifString(ExifTag.OffsetTimeDigitized);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTag.SecurityClassification"/> tag.
        /// </summary>
        public static ExifString SecurityClassification => new ExifString(ExifTag.SecurityClassification);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTag.ImageHistory"/> tag.
        /// </summary>
        public static ExifString ImageHistory => new ExifString(ExifTag.ImageHistory);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTag.ImageUniqueID"/> tag.
        /// </summary>
        public static ExifString ImageUniqueID => new ExifString(ExifTag.ImageUniqueID);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTag.OwnerName"/> tag.
        /// </summary>
        public static ExifString OwnerName => new ExifString(ExifTag.OwnerName);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTag.SerialNumber"/> tag.
        /// </summary>
        public static ExifString SerialNumber => new ExifString(ExifTag.SerialNumber);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTag.LensMake"/> tag.
        /// </summary>
        public static ExifString LensMake => new ExifString(ExifTag.LensMake);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTag.LensModel"/> tag.
        /// </summary>
        public static ExifString LensModel => new ExifString(ExifTag.LensModel);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTag.LensSerialNumber"/> tag.
        /// </summary>
        public static ExifString LensSerialNumber => new ExifString(ExifTag.LensSerialNumber);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTag.GDALMetadata"/> tag.
        /// </summary>
        public static ExifString GDALMetadata => new ExifString(ExifTag.GDALMetadata);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTag.GDALNoData"/> tag.
        /// </summary>
        public static ExifString GDALNoData => new ExifString(ExifTag.GDALNoData);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTag.GPSLatitudeRef"/> tag.
        /// </summary>
        public static ExifString GPSLatitudeRef => new ExifString(ExifTag.GPSLatitudeRef);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTag.GPSLongitudeRef"/> tag.
        /// </summary>
        public static ExifString GPSLongitudeRef => new ExifString(ExifTag.GPSLongitudeRef);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTag.GPSSatellites"/> tag.
        /// </summary>
        public static ExifString GPSSatellites => new ExifString(ExifTag.GPSSatellites);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTag.GPSStatus"/> tag.
        /// </summary>
        public static ExifString GPSStatus => new ExifString(ExifTag.GPSStatus);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTag.GPSMeasureMode"/> tag.
        /// </summary>
        public static ExifString GPSMeasureMode => new ExifString(ExifTag.GPSMeasureMode);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTag.GPSSpeedRef"/> tag.
        /// </summary>
        public static ExifString GPSSpeedRef => new ExifString(ExifTag.GPSSpeedRef);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTag.GPSTrackRef"/> tag.
        /// </summary>
        public static ExifString GPSTrackRef => new ExifString(ExifTag.GPSTrackRef);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTag.GPSImgDirectionRef"/> tag.
        /// </summary>
        public static ExifString GPSImgDirectionRef => new ExifString(ExifTag.GPSImgDirectionRef);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTag.GPSMapDatum"/> tag.
        /// </summary>
        public static ExifString GPSMapDatum => new ExifString(ExifTag.GPSMapDatum);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTag.GPSDestLatitudeRef"/> tag.
        /// </summary>
        public static ExifString GPSDestLatitudeRef => new ExifString(ExifTag.GPSDestLatitudeRef);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTag.GPSDestLongitudeRef"/> tag.
        /// </summary>
        public static ExifString GPSDestLongitudeRef => new ExifString(ExifTag.GPSDestLongitudeRef);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTag.GPSDestBearingRef"/> tag.
        /// </summary>
        public static ExifString GPSDestBearingRef => new ExifString(ExifTag.GPSDestBearingRef);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTag.GPSDestDistanceRef"/> tag.
        /// </summary>
        public static ExifString GPSDestDistanceRef => new ExifString(ExifTag.GPSDestDistanceRef);

        /// <summary>
        /// Gets a new <see cref="ExifString"/> instance for the <see cref="ExifTag.GPSDateStamp"/> tag.
        /// </summary>
        public static ExifString GPSDateStamp => new ExifString(ExifTag.GPSDateStamp);
    }
}
