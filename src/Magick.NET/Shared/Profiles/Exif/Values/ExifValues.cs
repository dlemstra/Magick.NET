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
        internal static IExifValue Create(ExifTag tag, object value)
        {
            var result = CreateNumberValue(tag, value);

            if (result == null)
            {
                result = Create(tag);
                result.Value = value;
            }

            return result;
        }

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

                case ExifTag.BitsPerSample:
                case ExifTag.MinSampleValue:
                case ExifTag.MaxSampleValue:
                case ExifTag.GrayResponseCurve:
                case ExifTag.ColorMap:
                case ExifTag.ExtraSamples:
                case ExifTag.PageNumber:
                case ExifTag.TransferFunction:
                case ExifTag.Predictor:
                case ExifTag.HalftoneHints:
                case ExifTag.SampleFormat:
                case ExifTag.TransferRange:
                case ExifTag.DefaultImageColor:
                case ExifTag.JPEGLosslessPredictors:
                case ExifTag.JPEGPointTransforms:
                case ExifTag.YCbCrSubsampling:
                case ExifTag.CFARepeatPatternDim:
                case ExifTag.IntergraphPacketData:
                case ExifTag.ISOSpeedRatings:
                case ExifTag.SubjectArea:
                case ExifTag.SubjectLocation:
                    return new ExifShortArray(tag);

                case ExifTag.OldSubfileType:
                case ExifTag.Compression:
                case ExifTag.PhotometricInterpretation:
                case ExifTag.Thresholding:
                case ExifTag.CellWidth:
                case ExifTag.CellLength:
                case ExifTag.FillOrder:
                case ExifTag.Orientation:
                case ExifTag.SamplesPerPixel:
                case ExifTag.PlanarConfiguration:
                case ExifTag.GrayResponseUnit:
                case ExifTag.ResolutionUnit:
                case ExifTag.CleanFaxData:
                case ExifTag.InkSet:
                case ExifTag.NumberOfInks:
                case ExifTag.DotRange:
                case ExifTag.Indexed:
                case ExifTag.OPIProxy:
                case ExifTag.JPEGProc:
                case ExifTag.JPEGRestartInterval:
                case ExifTag.YCbCrPositioning:
                case ExifTag.Rating:
                case ExifTag.RatingPercent:
                case ExifTag.ExposureProgram:
                case ExifTag.Interlace:
                case ExifTag.SelfTimerMode:
                case ExifTag.SensitivityType:
                case ExifTag.MeteringMode:
                case ExifTag.LightSource:
                case ExifTag.FocalPlaneResolutionUnit2:
                case ExifTag.SensingMethod2:
                case ExifTag.Flash:
                case ExifTag.ColorSpace:
                case ExifTag.FocalPlaneResolutionUnit:
                case ExifTag.SensingMethod:
                case ExifTag.CustomRendered:
                case ExifTag.ExposureMode:
                case ExifTag.WhiteBalance:
                case ExifTag.FocalLengthIn35mmFilm:
                case ExifTag.SceneCaptureType:
                case ExifTag.GainControl:
                case ExifTag.Contrast:
                case ExifTag.Saturation:
                case ExifTag.Sharpness:
                case ExifTag.SubjectDistanceRange:
                case ExifTag.GPSDifferential:
                    return new ExifShort(tag);

                case ExifTag.Decode:
                    return new ExifSignedRationalArray(tag);

                case ExifTag.ShutterSpeedValue:
                case ExifTag.BrightnessValue:
                case ExifTag.ExposureBiasValue:
                case ExifTag.AmbientTemperature:
                case ExifTag.WaterDepth:
                case ExifTag.CameraElevationAngle:
                    return new ExifSignedRational(tag);

                case ExifTag.ImageDescription:
                case ExifTag.Make:
                case ExifTag.Model:
                case ExifTag.Software:
                case ExifTag.DateTime:
                case ExifTag.Artist:
                case ExifTag.HostComputer:
                case ExifTag.Copyright:
                case ExifTag.DocumentName:
                case ExifTag.PageName:
                case ExifTag.InkNames:
                case ExifTag.TargetPrinter:
                case ExifTag.ImageID:
                case ExifTag.MDLabName:
                case ExifTag.MDSampleInfo:
                case ExifTag.MDPrepDate:
                case ExifTag.MDPrepTime:
                case ExifTag.MDFileUnits:
                case ExifTag.SEMInfo:
                case ExifTag.SpectralSensitivity:
                case ExifTag.DateTimeOriginal:
                case ExifTag.DateTimeDigitized:
                case ExifTag.SubsecTime:
                case ExifTag.SubsecTimeOriginal:
                case ExifTag.SubsecTimeDigitized:
                case ExifTag.RelatedSoundFile:
                case ExifTag.FaxSubaddress:
                case ExifTag.OffsetTime:
                case ExifTag.OffsetTimeOriginal:
                case ExifTag.OffsetTimeDigitized:
                case ExifTag.SecurityClassification:
                case ExifTag.ImageHistory:
                case ExifTag.ImageUniqueID:
                case ExifTag.OwnerName:
                case ExifTag.SerialNumber:
                case ExifTag.LensMake:
                case ExifTag.LensModel:
                case ExifTag.LensSerialNumber:
                case ExifTag.GDALMetadata:
                case ExifTag.GDALNoData:
                case ExifTag.GPSLatitudeRef:
                case ExifTag.GPSLongitudeRef:
                case ExifTag.GPSSatellites:
                case ExifTag.GPSStatus:
                case ExifTag.GPSMeasureMode:
                case ExifTag.GPSSpeedRef:
                case ExifTag.GPSTrackRef:
                case ExifTag.GPSImgDirectionRef:
                case ExifTag.GPSMapDatum:
                case ExifTag.GPSDestLatitudeRef:
                case ExifTag.GPSDestLongitudeRef:
                case ExifTag.GPSDestBearingRef:
                case ExifTag.GPSDestDistanceRef:
                case ExifTag.GPSDateStamp:
                    return new ExifString(tag);

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

        private static IExifValue CreateNumberValue(ExifTag tag, object value)
        {
            switch (tag)
            {
                case ExifTag.StripOffsets:
                case ExifTag.TileByteCounts:
                case ExifTag.ImageLayer:
                    switch (value)
                    {
                        case null:
                            return new ExifShortArray(tag);
                        case ushort[] ushortValue:
                            return ExifShortArray.Create(tag, ushortValue);
                        case uint[] ulongValue:
                            return ExifLongArray.Create(tag, ulongValue);
                        default:
                            throw new NotSupportedException();
                    }

                case ExifTag.ImageWidth:
                case ExifTag.ImageLength:
                case ExifTag.TileWidth:
                case ExifTag.TileLength:
                case ExifTag.BadFaxLines:
                case ExifTag.ConsecutiveBadFaxLines:
                case ExifTag.PixelXDimension:
                case ExifTag.PixelYDimension:
                    switch (value)
                    {
                        case null:
                            return new ExifShort(tag);
                        case ushort ushortValue:
                            return ExifShort.Create(tag, ushortValue);
                        case short shortValue:
                            return ExifShort.Create(tag, (ushort)shortValue);
                        case uint ulongValue:
                            return ExifLong.Create(tag, ulongValue);
                        case int longValue:
                            return ExifLong.Create(tag, (uint)longValue);
                        default:
                            throw new NotSupportedException();
                    }

                default:
                    return null;
            }
        }
    }
}
