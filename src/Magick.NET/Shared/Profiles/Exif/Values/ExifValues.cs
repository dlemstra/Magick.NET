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

using System;

namespace ImageMagick
{
    /// <summary>
    /// Contains the possible exif values.
    /// </summary>
    public static partial class ExifValues
    {
        internal static IExifValue Create(ExifTag tag)
        {
            Throw.IfTrue(nameof(tag), tag == ExifTag.Unknown, "Invalid Tag");

            switch (tag)
            {
                case ExifTag.ClipPath:
                case ExifTag.VersionYear:
                case ExifTag.XMP:
                case ExifTag.CFAPattern2:
                case ExifTag.TIFFEPStandardID:
                case ExifTag.XPTitle:
                case ExifTag.XPComment:
                case ExifTag.XPAuthor:
                case ExifTag.XPKeywords:
                case ExifTag.XPSubject:
                case ExifTag.GPSVersionID:
                    return new ExifByteArray(tag, ExifDataType.Byte);

                case ExifTag.FaxProfile:
                case ExifTag.ModeNumber:
                case ExifTag.GPSAltitudeRef:
                    return new ExifByte(tag, ExifDataType.Byte);

                case ExifTag.FreeOffsets:
                case ExifTag.FreeByteCounts:
                case ExifTag.ColorResponseUnit:
                case ExifTag.TileOffsets:
                case ExifTag.SMinSampleValue:
                case ExifTag.SMaxSampleValue:
                case ExifTag.JPEGQTables:
                case ExifTag.JPEGDCTables:
                case ExifTag.JPEGACTables:
                case ExifTag.StripRowCounts:
                case ExifTag.IntergraphRegisters:
                case ExifTag.TimeZoneOffset:
                    return new ExifLongArray(tag);

                case ExifTag.SubfileType:
                case ExifTag.SubIFDOffset:
                case ExifTag.GPSIFDOffset:
                case ExifTag.T4Options:
                case ExifTag.T6Options:
                case ExifTag.XClipPathUnits:
                case ExifTag.YClipPathUnits:
                case ExifTag.ProfileType:
                case ExifTag.CodingMethods:
                case ExifTag.T82ptions:
                case ExifTag.JPEGInterchangeFormat:
                case ExifTag.JPEGInterchangeFormatLength:
                case ExifTag.MDFileTag:
                case ExifTag.StandardOutputSensitivity:
                case ExifTag.RecommendedExposureIndex:
                case ExifTag.ISOSpeed:
                case ExifTag.ISOSpeedLatitudeyyy:
                case ExifTag.ISOSpeedLatitudezzz:
                case ExifTag.FaxRecvParams:
                case ExifTag.FaxRecvTime:
                case ExifTag.ImageNumber:
                    return new ExifLong(tag);

                case ExifTag.WhitePoint:
                case ExifTag.PrimaryChromaticities:
                case ExifTag.YCbCrCoefficients:
                case ExifTag.ReferenceBlackWhite:
                case ExifTag.PixelScale:
                case ExifTag.IntergraphMatrix:
                case ExifTag.ModelTiePoint:
                case ExifTag.ModelTransform:
                case ExifTag.GPSLatitude:
                case ExifTag.GPSLongitude:
                case ExifTag.GPSTimestamp:
                case ExifTag.GPSDestLatitude:
                case ExifTag.GPSDestLongitude:
                    return new ExifRationalArray(tag);

                case ExifTag.XPosition:
                case ExifTag.YPosition:
                case ExifTag.XResolution:
                case ExifTag.YResolution:
                case ExifTag.BatteryLevel:
                case ExifTag.ExposureTime:
                case ExifTag.FNumber:
                case ExifTag.MDScalePixel:
                case ExifTag.CompressedBitsPerPixel:
                case ExifTag.ApertureValue:
                case ExifTag.MaxApertureValue:
                case ExifTag.SubjectDistance:
                case ExifTag.FocalLength:
                case ExifTag.FlashEnergy2:
                case ExifTag.FocalPlaneXResolution2:
                case ExifTag.FocalPlaneYResolution2:
                case ExifTag.ExposureIndex2:
                case ExifTag.Humidity:
                case ExifTag.Pressure:
                case ExifTag.Acceleration:
                case ExifTag.FlashEnergy:
                case ExifTag.FocalPlaneXResolution:
                case ExifTag.FocalPlaneYResolution:
                case ExifTag.ExposureIndex:
                case ExifTag.DigitalZoomRatio:
                case ExifTag.LensInfo:
                case ExifTag.GPSAltitude:
                case ExifTag.GPSDOP:
                case ExifTag.GPSSpeed:
                case ExifTag.GPSTrack:
                case ExifTag.GPSImgDirection:
                case ExifTag.GPSDestBearing:
                case ExifTag.GPSDestDistance:
                    return new ExifRational(tag);

                case ExifTag.JPEGTables:
                case ExifTag.OECF:
                case ExifTag.ExifVersion:
                case ExifTag.ComponentsConfiguration:
                case ExifTag.MakerNote:
                case ExifTag.UserComment:
                case ExifTag.FlashpixVersion:
                case ExifTag.SpatialFrequencyResponse:
                case ExifTag.SpatialFrequencyResponse2:
                case ExifTag.Noise:
                case ExifTag.CFAPattern:
                case ExifTag.DeviceSettingDescription:
                case ExifTag.ImageSourceData:
                case ExifTag.GPSProcessingMethod:
                case ExifTag.GPSAreaInformation:
                    return new ExifByteArray(tag, ExifDataType.Undefined);

                case ExifTag.FileSource:
                case ExifTag.SceneType:
                    return new ExifByte(tag, ExifDataType.Undefined);

                default:
                    throw new NotSupportedException();
            }
        }
    }
}
