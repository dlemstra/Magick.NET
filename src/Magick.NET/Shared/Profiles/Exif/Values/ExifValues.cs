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
        internal static IExifValue Create(ExifTagValue tag, object value)
        {
            var result = CreateNumberValue(tag, value);

            if (result == null)
            {
                result = Create(tag);
                result.Value = value;
            }

            return result;
        }

        internal static IExifValue Create(ExifTagValue tag)
        {
            Throw.IfTrue(nameof(tag), tag == ExifTagValue.Unknown, "Invalid Tag");

            switch (tag)
            {
                case ExifTagValue.ClipPath:
                case ExifTagValue.VersionYear:
                case ExifTagValue.XMP:
                case ExifTagValue.CFAPattern2:
                case ExifTagValue.TIFFEPStandardID:
                case ExifTagValue.XPTitle:
                case ExifTagValue.XPComment:
                case ExifTagValue.XPAuthor:
                case ExifTagValue.XPKeywords:
                case ExifTagValue.XPSubject:
                case ExifTagValue.GPSVersionID:
                    return new ExifByteArray(tag, ExifDataType.Byte);

                case ExifTagValue.FaxProfile:
                case ExifTagValue.ModeNumber:
                case ExifTagValue.GPSAltitudeRef:
                    return new ExifByte(tag, ExifDataType.Byte);

                case ExifTagValue.FreeOffsets:
                case ExifTagValue.FreeByteCounts:
                case ExifTagValue.ColorResponseUnit:
                case ExifTagValue.TileOffsets:
                case ExifTagValue.SMinSampleValue:
                case ExifTagValue.SMaxSampleValue:
                case ExifTagValue.JPEGQTables:
                case ExifTagValue.JPEGDCTables:
                case ExifTagValue.JPEGACTables:
                case ExifTagValue.StripRowCounts:
                case ExifTagValue.IntergraphRegisters:
                case ExifTagValue.TimeZoneOffset:
                    return new ExifLongArray(tag);

                case ExifTagValue.SubfileType:
                case ExifTagValue.SubIFDOffset:
                case ExifTagValue.GPSIFDOffset:
                case ExifTagValue.T4Options:
                case ExifTagValue.T6Options:
                case ExifTagValue.XClipPathUnits:
                case ExifTagValue.YClipPathUnits:
                case ExifTagValue.ProfileType:
                case ExifTagValue.CodingMethods:
                case ExifTagValue.T82ptions:
                case ExifTagValue.JPEGInterchangeFormat:
                case ExifTagValue.JPEGInterchangeFormatLength:
                case ExifTagValue.MDFileTag:
                case ExifTagValue.StandardOutputSensitivity:
                case ExifTagValue.RecommendedExposureIndex:
                case ExifTagValue.ISOSpeed:
                case ExifTagValue.ISOSpeedLatitudeyyy:
                case ExifTagValue.ISOSpeedLatitudezzz:
                case ExifTagValue.FaxRecvParams:
                case ExifTagValue.FaxRecvTime:
                case ExifTagValue.ImageNumber:
                    return new ExifLong(tag);

                case ExifTagValue.WhitePoint:
                case ExifTagValue.PrimaryChromaticities:
                case ExifTagValue.YCbCrCoefficients:
                case ExifTagValue.ReferenceBlackWhite:
                case ExifTagValue.PixelScale:
                case ExifTagValue.IntergraphMatrix:
                case ExifTagValue.ModelTiePoint:
                case ExifTagValue.ModelTransform:
                case ExifTagValue.GPSLatitude:
                case ExifTagValue.GPSLongitude:
                case ExifTagValue.GPSTimestamp:
                case ExifTagValue.GPSDestLatitude:
                case ExifTagValue.GPSDestLongitude:
                    return new ExifRationalArray(tag);

                case ExifTagValue.XPosition:
                case ExifTagValue.YPosition:
                case ExifTagValue.XResolution:
                case ExifTagValue.YResolution:
                case ExifTagValue.BatteryLevel:
                case ExifTagValue.ExposureTime:
                case ExifTagValue.FNumber:
                case ExifTagValue.MDScalePixel:
                case ExifTagValue.CompressedBitsPerPixel:
                case ExifTagValue.ApertureValue:
                case ExifTagValue.MaxApertureValue:
                case ExifTagValue.SubjectDistance:
                case ExifTagValue.FocalLength:
                case ExifTagValue.FlashEnergy2:
                case ExifTagValue.FocalPlaneXResolution2:
                case ExifTagValue.FocalPlaneYResolution2:
                case ExifTagValue.ExposureIndex2:
                case ExifTagValue.Humidity:
                case ExifTagValue.Pressure:
                case ExifTagValue.Acceleration:
                case ExifTagValue.FlashEnergy:
                case ExifTagValue.FocalPlaneXResolution:
                case ExifTagValue.FocalPlaneYResolution:
                case ExifTagValue.ExposureIndex:
                case ExifTagValue.DigitalZoomRatio:
                case ExifTagValue.LensInfo:
                case ExifTagValue.GPSAltitude:
                case ExifTagValue.GPSDOP:
                case ExifTagValue.GPSSpeed:
                case ExifTagValue.GPSTrack:
                case ExifTagValue.GPSImgDirection:
                case ExifTagValue.GPSDestBearing:
                case ExifTagValue.GPSDestDistance:
                    return new ExifRational(tag);

                case ExifTagValue.BitsPerSample:
                case ExifTagValue.MinSampleValue:
                case ExifTagValue.MaxSampleValue:
                case ExifTagValue.GrayResponseCurve:
                case ExifTagValue.ColorMap:
                case ExifTagValue.ExtraSamples:
                case ExifTagValue.PageNumber:
                case ExifTagValue.TransferFunction:
                case ExifTagValue.Predictor:
                case ExifTagValue.HalftoneHints:
                case ExifTagValue.SampleFormat:
                case ExifTagValue.TransferRange:
                case ExifTagValue.DefaultImageColor:
                case ExifTagValue.JPEGLosslessPredictors:
                case ExifTagValue.JPEGPointTransforms:
                case ExifTagValue.YCbCrSubsampling:
                case ExifTagValue.CFARepeatPatternDim:
                case ExifTagValue.IntergraphPacketData:
                case ExifTagValue.ISOSpeedRatings:
                case ExifTagValue.SubjectArea:
                case ExifTagValue.SubjectLocation:
                    return new ExifShortArray(tag);

                case ExifTagValue.OldSubfileType:
                case ExifTagValue.Compression:
                case ExifTagValue.PhotometricInterpretation:
                case ExifTagValue.Thresholding:
                case ExifTagValue.CellWidth:
                case ExifTagValue.CellLength:
                case ExifTagValue.FillOrder:
                case ExifTagValue.Orientation:
                case ExifTagValue.SamplesPerPixel:
                case ExifTagValue.PlanarConfiguration:
                case ExifTagValue.GrayResponseUnit:
                case ExifTagValue.ResolutionUnit:
                case ExifTagValue.CleanFaxData:
                case ExifTagValue.InkSet:
                case ExifTagValue.NumberOfInks:
                case ExifTagValue.DotRange:
                case ExifTagValue.Indexed:
                case ExifTagValue.OPIProxy:
                case ExifTagValue.JPEGProc:
                case ExifTagValue.JPEGRestartInterval:
                case ExifTagValue.YCbCrPositioning:
                case ExifTagValue.Rating:
                case ExifTagValue.RatingPercent:
                case ExifTagValue.ExposureProgram:
                case ExifTagValue.Interlace:
                case ExifTagValue.SelfTimerMode:
                case ExifTagValue.SensitivityType:
                case ExifTagValue.MeteringMode:
                case ExifTagValue.LightSource:
                case ExifTagValue.FocalPlaneResolutionUnit2:
                case ExifTagValue.SensingMethod2:
                case ExifTagValue.Flash:
                case ExifTagValue.ColorSpace:
                case ExifTagValue.FocalPlaneResolutionUnit:
                case ExifTagValue.SensingMethod:
                case ExifTagValue.CustomRendered:
                case ExifTagValue.ExposureMode:
                case ExifTagValue.WhiteBalance:
                case ExifTagValue.FocalLengthIn35mmFilm:
                case ExifTagValue.SceneCaptureType:
                case ExifTagValue.GainControl:
                case ExifTagValue.Contrast:
                case ExifTagValue.Saturation:
                case ExifTagValue.Sharpness:
                case ExifTagValue.SubjectDistanceRange:
                case ExifTagValue.GPSDifferential:
                    return new ExifShort(tag);

                case ExifTagValue.Decode:
                    return new ExifSignedRationalArray(tag);

                case ExifTagValue.ShutterSpeedValue:
                case ExifTagValue.BrightnessValue:
                case ExifTagValue.ExposureBiasValue:
                case ExifTagValue.AmbientTemperature:
                case ExifTagValue.WaterDepth:
                case ExifTagValue.CameraElevationAngle:
                    return new ExifSignedRational(tag);

                case ExifTagValue.ImageDescription:
                case ExifTagValue.Make:
                case ExifTagValue.Model:
                case ExifTagValue.Software:
                case ExifTagValue.DateTime:
                case ExifTagValue.Artist:
                case ExifTagValue.HostComputer:
                case ExifTagValue.Copyright:
                case ExifTagValue.DocumentName:
                case ExifTagValue.PageName:
                case ExifTagValue.InkNames:
                case ExifTagValue.TargetPrinter:
                case ExifTagValue.ImageID:
                case ExifTagValue.MDLabName:
                case ExifTagValue.MDSampleInfo:
                case ExifTagValue.MDPrepDate:
                case ExifTagValue.MDPrepTime:
                case ExifTagValue.MDFileUnits:
                case ExifTagValue.SEMInfo:
                case ExifTagValue.SpectralSensitivity:
                case ExifTagValue.DateTimeOriginal:
                case ExifTagValue.DateTimeDigitized:
                case ExifTagValue.SubsecTime:
                case ExifTagValue.SubsecTimeOriginal:
                case ExifTagValue.SubsecTimeDigitized:
                case ExifTagValue.RelatedSoundFile:
                case ExifTagValue.FaxSubaddress:
                case ExifTagValue.OffsetTime:
                case ExifTagValue.OffsetTimeOriginal:
                case ExifTagValue.OffsetTimeDigitized:
                case ExifTagValue.SecurityClassification:
                case ExifTagValue.ImageHistory:
                case ExifTagValue.ImageUniqueID:
                case ExifTagValue.OwnerName:
                case ExifTagValue.SerialNumber:
                case ExifTagValue.LensMake:
                case ExifTagValue.LensModel:
                case ExifTagValue.LensSerialNumber:
                case ExifTagValue.GDALMetadata:
                case ExifTagValue.GDALNoData:
                case ExifTagValue.GPSLatitudeRef:
                case ExifTagValue.GPSLongitudeRef:
                case ExifTagValue.GPSSatellites:
                case ExifTagValue.GPSStatus:
                case ExifTagValue.GPSMeasureMode:
                case ExifTagValue.GPSSpeedRef:
                case ExifTagValue.GPSTrackRef:
                case ExifTagValue.GPSImgDirectionRef:
                case ExifTagValue.GPSMapDatum:
                case ExifTagValue.GPSDestLatitudeRef:
                case ExifTagValue.GPSDestLongitudeRef:
                case ExifTagValue.GPSDestBearingRef:
                case ExifTagValue.GPSDestDistanceRef:
                case ExifTagValue.GPSDateStamp:
                    return new ExifString(tag);

                case ExifTagValue.JPEGTables:
                case ExifTagValue.OECF:
                case ExifTagValue.ExifVersion:
                case ExifTagValue.ComponentsConfiguration:
                case ExifTagValue.MakerNote:
                case ExifTagValue.UserComment:
                case ExifTagValue.FlashpixVersion:
                case ExifTagValue.SpatialFrequencyResponse:
                case ExifTagValue.SpatialFrequencyResponse2:
                case ExifTagValue.Noise:
                case ExifTagValue.CFAPattern:
                case ExifTagValue.DeviceSettingDescription:
                case ExifTagValue.ImageSourceData:
                case ExifTagValue.GPSProcessingMethod:
                case ExifTagValue.GPSAreaInformation:
                    return new ExifByteArray(tag, ExifDataType.Undefined);

                case ExifTagValue.FileSource:
                case ExifTagValue.SceneType:
                    return new ExifByte(tag, ExifDataType.Undefined);

                default:
                    throw new NotSupportedException();
            }
        }

        private static IExifValue CreateNumberValue(ExifTagValue tag, object value)
        {
            switch (tag)
            {
                case ExifTagValue.StripOffsets:
                case ExifTagValue.TileByteCounts:
                case ExifTagValue.ImageLayer:
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

                case ExifTagValue.ImageWidth:
                case ExifTagValue.ImageLength:
                case ExifTagValue.TileWidth:
                case ExifTagValue.TileLength:
                case ExifTagValue.BadFaxLines:
                case ExifTagValue.ConsecutiveBadFaxLines:
                case ExifTagValue.PixelXDimension:
                case ExifTagValue.PixelYDimension:
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
