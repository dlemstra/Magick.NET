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

        private static object CreateValue(ExifTag tag)
        {
            var tagValue = (ExifTagValue)(uint)tag;

            return tagValue switch
            {
                ExifTagValue.FaxProfile => new ExifByte(ExifTag.FaxProfile, ExifDataType.Byte),
                ExifTagValue.ModeNumber => new ExifByte(ExifTag.ModeNumber, ExifDataType.Byte),
                ExifTagValue.GPSAltitudeRef => new ExifByte(ExifTag.GPSAltitudeRef, ExifDataType.Byte),

                ExifTagValue.ClipPath => new ExifByteArray(ExifTag.ClipPath, ExifDataType.Byte),
                ExifTagValue.VersionYear => new ExifByteArray(ExifTag.VersionYear, ExifDataType.Byte),
                ExifTagValue.XMP => new ExifByteArray(ExifTag.XMP, ExifDataType.Byte),
                ExifTagValue.CFAPattern2 => new ExifByteArray(ExifTag.CFAPattern2, ExifDataType.Byte),
                ExifTagValue.TIFFEPStandardID => new ExifByteArray(ExifTag.TIFFEPStandardID, ExifDataType.Byte),
                ExifTagValue.XPTitle => new ExifByteArray(ExifTag.XPTitle, ExifDataType.Byte),
                ExifTagValue.XPComment => new ExifByteArray(ExifTag.XPComment, ExifDataType.Byte),
                ExifTagValue.XPAuthor => new ExifByteArray(ExifTag.XPAuthor, ExifDataType.Byte),
                ExifTagValue.XPKeywords => new ExifByteArray(ExifTag.XPKeywords, ExifDataType.Byte),
                ExifTagValue.XPSubject => new ExifByteArray(ExifTag.XPSubject, ExifDataType.Byte),
                ExifTagValue.GPSVersionID => new ExifByteArray(ExifTag.GPSVersionID, ExifDataType.Byte),

                ExifTagValue.PixelScale => new ExifDoubleArray(ExifTag.PixelScale),
                ExifTagValue.IntergraphMatrix => new ExifDoubleArray(ExifTag.IntergraphMatrix),
                ExifTagValue.ModelTiePoint => new ExifDoubleArray(ExifTag.ModelTiePoint),
                ExifTagValue.ModelTransform => new ExifDoubleArray(ExifTag.ModelTransform),

                ExifTagValue.SubfileType => new ExifLong(ExifTag.SubfileType),
                ExifTagValue.SubIFDOffset => new ExifLong(ExifTag.SubIFDOffset),
                ExifTagValue.GPSIFDOffset => new ExifLong(ExifTag.GPSIFDOffset),
                ExifTagValue.T4Options => new ExifLong(ExifTag.T4Options),
                ExifTagValue.T6Options => new ExifLong(ExifTag.T6Options),
                ExifTagValue.XClipPathUnits => new ExifLong(ExifTag.XClipPathUnits),
                ExifTagValue.YClipPathUnits => new ExifLong(ExifTag.YClipPathUnits),
                ExifTagValue.ProfileType => new ExifLong(ExifTag.ProfileType),
                ExifTagValue.CodingMethods => new ExifLong(ExifTag.CodingMethods),
                ExifTagValue.T82ptions => new ExifLong(ExifTag.T82ptions),
                ExifTagValue.JPEGInterchangeFormat => new ExifLong(ExifTag.JPEGInterchangeFormat),
                ExifTagValue.JPEGInterchangeFormatLength => new ExifLong(ExifTag.JPEGInterchangeFormatLength),
                ExifTagValue.MDFileTag => new ExifLong(ExifTag.MDFileTag),
                ExifTagValue.StandardOutputSensitivity => new ExifLong(ExifTag.StandardOutputSensitivity),
                ExifTagValue.RecommendedExposureIndex => new ExifLong(ExifTag.RecommendedExposureIndex),
                ExifTagValue.ISOSpeed => new ExifLong(ExifTag.ISOSpeed),
                ExifTagValue.ISOSpeedLatitudeyyy => new ExifLong(ExifTag.ISOSpeedLatitudeyyy),
                ExifTagValue.ISOSpeedLatitudezzz => new ExifLong(ExifTag.ISOSpeedLatitudezzz),
                ExifTagValue.FaxRecvParams => new ExifLong(ExifTag.FaxRecvParams),
                ExifTagValue.FaxRecvTime => new ExifLong(ExifTag.FaxRecvTime),

                ExifTagValue.FreeOffsets => new ExifLongArray(ExifTag.FreeOffsets),
                ExifTagValue.FreeByteCounts => new ExifLongArray(ExifTag.FreeByteCounts),
                ExifTagValue.ColorResponseUnit => new ExifLongArray(ExifTag.TileOffsets),
                ExifTagValue.TileOffsets => new ExifLongArray(ExifTag.TileOffsets),
                ExifTagValue.SMinSampleValue => new ExifLongArray(ExifTag.SMinSampleValue),
                ExifTagValue.SMaxSampleValue => new ExifLongArray(ExifTag.SMaxSampleValue),
                ExifTagValue.JPEGQTables => new ExifLongArray(ExifTag.JPEGQTables),
                ExifTagValue.JPEGDCTables => new ExifLongArray(ExifTag.JPEGDCTables),
                ExifTagValue.JPEGACTables => new ExifLongArray(ExifTag.JPEGACTables),
                ExifTagValue.StripRowCounts => new ExifLongArray(ExifTag.StripRowCounts),
                ExifTagValue.IntergraphRegisters => new ExifLongArray(ExifTag.IntergraphRegisters),
                ExifTagValue.TimeZoneOffset => new ExifLongArray(ExifTag.TimeZoneOffset),

                ExifTagValue.ImageWidth => new ExifNumber(ExifTag.ImageWidth),
                ExifTagValue.ImageLength => new ExifNumber(ExifTag.ImageLength),
                ExifTagValue.TileWidth => new ExifNumber(ExifTag.TileWidth),
                ExifTagValue.TileLength => new ExifNumber(ExifTag.TileLength),
                ExifTagValue.BadFaxLines => new ExifNumber(ExifTag.BadFaxLines),
                ExifTagValue.ConsecutiveBadFaxLines => new ExifNumber(ExifTag.ConsecutiveBadFaxLines),
                ExifTagValue.PixelXDimension => new ExifNumber(ExifTag.PixelXDimension),
                ExifTagValue.PixelYDimension => new ExifNumber(ExifTag.PixelYDimension),

                ExifTagValue.StripOffsets => new ExifNumberArray(ExifTag.StripOffsets),
                ExifTagValue.TileByteCounts => new ExifNumberArray(ExifTag.TileByteCounts),
                ExifTagValue.ImageLayer => new ExifNumberArray(ExifTag.ImageLayer),

                ExifTagValue.XPosition => new ExifRational(ExifTag.XPosition),
                ExifTagValue.YPosition => new ExifRational(ExifTag.YPosition),
                ExifTagValue.XResolution => new ExifRational(ExifTag.XResolution),
                ExifTagValue.YResolution => new ExifRational(ExifTag.YResolution),
                ExifTagValue.BatteryLevel => new ExifRational(ExifTag.BatteryLevel),
                ExifTagValue.ExposureTime => new ExifRational(ExifTag.ExposureTime),
                ExifTagValue.FNumber => new ExifRational(ExifTag.FNumber),
                ExifTagValue.MDScalePixel => new ExifRational(ExifTag.MDScalePixel),
                ExifTagValue.CompressedBitsPerPixel => new ExifRational(ExifTag.CompressedBitsPerPixel),
                ExifTagValue.ApertureValue => new ExifRational(ExifTag.ApertureValue),
                ExifTagValue.MaxApertureValue => new ExifRational(ExifTag.MaxApertureValue),
                ExifTagValue.SubjectDistance => new ExifRational(ExifTag.SubjectDistance),
                ExifTagValue.FocalLength => new ExifRational(ExifTag.FocalLength),
                ExifTagValue.FlashEnergy2 => new ExifRational(ExifTag.FlashEnergy2),
                ExifTagValue.FocalPlaneXResolution2 => new ExifRational(ExifTag.FocalPlaneXResolution2),
                ExifTagValue.FocalPlaneYResolution2 => new ExifRational(ExifTag.FocalPlaneYResolution2),
                ExifTagValue.ExposureIndex2 => new ExifRational(ExifTag.ExposureIndex2),
                ExifTagValue.Humidity => new ExifRational(ExifTag.Humidity),
                ExifTagValue.Pressure => new ExifRational(ExifTag.Pressure),
                ExifTagValue.Acceleration => new ExifRational(ExifTag.Acceleration),
                ExifTagValue.FlashEnergy => new ExifRational(ExifTag.FlashEnergy),
                ExifTagValue.FocalPlaneXResolution => new ExifRational(ExifTag.FocalPlaneXResolution),
                ExifTagValue.FocalPlaneYResolution => new ExifRational(ExifTag.FocalPlaneYResolution),
                ExifTagValue.ExposureIndex => new ExifRational(ExifTag.ExposureIndex),
                ExifTagValue.DigitalZoomRatio => new ExifRational(ExifTag.DigitalZoomRatio),
                ExifTagValue.LensInfo => new ExifRational(ExifTag.LensInfo),
                ExifTagValue.GPSAltitude => new ExifRational(ExifTag.GPSAltitude),
                ExifTagValue.GPSDOP => new ExifRational(ExifTag.GPSDOP),
                ExifTagValue.GPSSpeed => new ExifRational(ExifTag.GPSSpeed),
                ExifTagValue.GPSTrack => new ExifRational(ExifTag.GPSTrack),
                ExifTagValue.GPSImgDirection => new ExifRational(ExifTag.GPSImgDirection),
                ExifTagValue.GPSDestBearing => new ExifRational(ExifTag.GPSDestBearing),
                ExifTagValue.GPSDestDistance => new ExifRational(ExifTag.GPSDestDistance),

                ExifTagValue.WhitePoint => new ExifRationalArray(ExifTag.WhitePoint),
                ExifTagValue.PrimaryChromaticities => new ExifRationalArray(ExifTag.PrimaryChromaticities),
                ExifTagValue.YCbCrCoefficients => new ExifRationalArray(ExifTag.YCbCrCoefficients),
                ExifTagValue.ReferenceBlackWhite => new ExifRationalArray(ExifTag.ReferenceBlackWhite),
                ExifTagValue.GPSLatitude => new ExifRationalArray(ExifTag.GPSLatitude),
                ExifTagValue.GPSLongitude => new ExifRationalArray(ExifTag.GPSLongitude),
                ExifTagValue.GPSTimestamp => new ExifRationalArray(ExifTag.GPSTimestamp),
                ExifTagValue.GPSDestLatitude => new ExifRationalArray(ExifTag.GPSDestLatitude),
                ExifTagValue.GPSDestLongitude => new ExifRationalArray(ExifTag.GPSDestLongitude),

                ExifTagValue.OldSubfileType => new ExifShort(ExifTag.OldSubfileType),
                ExifTagValue.Compression => new ExifShort(ExifTag.Compression),
                ExifTagValue.PhotometricInterpretation => new ExifShort(ExifTag.PhotometricInterpretation),
                ExifTagValue.Thresholding => new ExifShort(ExifTag.Thresholding),
                ExifTagValue.CellWidth => new ExifShort(ExifTag.CellWidth),
                ExifTagValue.CellLength => new ExifShort(ExifTag.CellLength),
                ExifTagValue.FillOrder => new ExifShort(ExifTag.FillOrder),
                ExifTagValue.Orientation => new ExifShort(ExifTag.Orientation),
                ExifTagValue.SamplesPerPixel => new ExifShort(ExifTag.SamplesPerPixel),
                ExifTagValue.PlanarConfiguration => new ExifShort(ExifTag.PlanarConfiguration),
                ExifTagValue.GrayResponseUnit => new ExifShort(ExifTag.GrayResponseUnit),
                ExifTagValue.ResolutionUnit => new ExifShort(ExifTag.ResolutionUnit),
                ExifTagValue.CleanFaxData => new ExifShort(ExifTag.CleanFaxData),
                ExifTagValue.InkSet => new ExifShort(ExifTag.InkSet),
                ExifTagValue.NumberOfInks => new ExifShort(ExifTag.NumberOfInks),
                ExifTagValue.DotRange => new ExifShort(ExifTag.DotRange),
                ExifTagValue.Indexed => new ExifShort(ExifTag.Indexed),
                ExifTagValue.OPIProxy => new ExifShort(ExifTag.OPIProxy),
                ExifTagValue.JPEGProc => new ExifShort(ExifTag.JPEGProc),
                ExifTagValue.JPEGRestartInterval => new ExifShort(ExifTag.JPEGRestartInterval),
                ExifTagValue.YCbCrPositioning => new ExifShort(ExifTag.YCbCrPositioning),
                ExifTagValue.Rating => new ExifShort(ExifTag.Rating),
                ExifTagValue.RatingPercent => new ExifShort(ExifTag.RatingPercent),
                ExifTagValue.ExposureProgram => new ExifShort(ExifTag.ExposureProgram),
                ExifTagValue.Interlace => new ExifShort(ExifTag.Interlace),
                ExifTagValue.SelfTimerMode => new ExifShort(ExifTag.SelfTimerMode),
                ExifTagValue.SensitivityType => new ExifShort(ExifTag.SensitivityType),
                ExifTagValue.MeteringMode => new ExifShort(ExifTag.MeteringMode),
                ExifTagValue.LightSource => new ExifShort(ExifTag.LightSource),
                ExifTagValue.FocalPlaneResolutionUnit2 => new ExifShort(ExifTag.FocalPlaneResolutionUnit2),
                ExifTagValue.SensingMethod2 => new ExifShort(ExifTag.SensingMethod2),
                ExifTagValue.Flash => new ExifShort(ExifTag.Flash),
                ExifTagValue.ColorSpace => new ExifShort(ExifTag.ColorSpace),
                ExifTagValue.FocalPlaneResolutionUnit => new ExifShort(ExifTag.FocalPlaneResolutionUnit),
                ExifTagValue.SensingMethod => new ExifShort(ExifTag.SensingMethod),
                ExifTagValue.CustomRendered => new ExifShort(ExifTag.CustomRendered),
                ExifTagValue.ExposureMode => new ExifShort(ExifTag.ExposureMode),
                ExifTagValue.WhiteBalance => new ExifShort(ExifTag.WhiteBalance),
                ExifTagValue.FocalLengthIn35mmFilm => new ExifShort(ExifTag.FocalLengthIn35mmFilm),
                ExifTagValue.SceneCaptureType => new ExifShort(ExifTag.SceneCaptureType),
                ExifTagValue.GainControl => new ExifShort(ExifTag.GainControl),
                ExifTagValue.Contrast => new ExifShort(ExifTag.Contrast),
                ExifTagValue.Saturation => new ExifShort(ExifTag.Saturation),
                ExifTagValue.Sharpness => new ExifShort(ExifTag.Sharpness),
                ExifTagValue.SubjectDistanceRange => new ExifShort(ExifTag.SubjectDistanceRange),
                ExifTagValue.GPSDifferential => new ExifShort(ExifTag.GPSDifferential),

                ExifTagValue.BitsPerSample => new ExifShortArray(ExifTag.BitsPerSample),
                ExifTagValue.MinSampleValue => new ExifShortArray(ExifTag.MinSampleValue),
                ExifTagValue.MaxSampleValue => new ExifShortArray(ExifTag.MaxSampleValue),
                ExifTagValue.GrayResponseCurve => new ExifShortArray(ExifTag.GrayResponseCurve),
                ExifTagValue.ColorMap => new ExifShortArray(ExifTag.ColorMap),
                ExifTagValue.ExtraSamples => new ExifShortArray(ExifTag.ExtraSamples),
                ExifTagValue.PageNumber => new ExifShortArray(ExifTag.PageNumber),
                ExifTagValue.TransferFunction => new ExifShortArray(ExifTag.TransferFunction),
                ExifTagValue.Predictor => new ExifShortArray(ExifTag.Predictor),
                ExifTagValue.HalftoneHints => new ExifShortArray(ExifTag.HalftoneHints),
                ExifTagValue.SampleFormat => new ExifShortArray(ExifTag.SampleFormat),
                ExifTagValue.TransferRange => new ExifShortArray(ExifTag.TransferRange),
                ExifTagValue.DefaultImageColor => new ExifShortArray(ExifTag.DefaultImageColor),
                ExifTagValue.JPEGLosslessPredictors => new ExifShortArray(ExifTag.JPEGLosslessPredictors),
                ExifTagValue.JPEGPointTransforms => new ExifShortArray(ExifTag.JPEGPointTransforms),
                ExifTagValue.YCbCrSubsampling => new ExifShortArray(ExifTag.YCbCrSubsampling),
                ExifTagValue.CFARepeatPatternDim => new ExifShortArray(ExifTag.CFARepeatPatternDim),
                ExifTagValue.IntergraphPacketData => new ExifShortArray(ExifTag.IntergraphPacketData),
                ExifTagValue.ISOSpeedRatings => new ExifShortArray(ExifTag.ISOSpeedRatings),
                ExifTagValue.SubjectArea => new ExifShortArray(ExifTag.SubjectArea),
                ExifTagValue.SubjectLocation => new ExifShortArray(ExifTag.SubjectLocation),

                ExifTagValue.ShutterSpeedValue => new ExifSignedRational(ExifTag.ShutterSpeedValue),
                ExifTagValue.BrightnessValue => new ExifSignedRational(ExifTag.BrightnessValue),
                ExifTagValue.ExposureBiasValue => new ExifSignedRational(ExifTag.ExposureBiasValue),
                ExifTagValue.AmbientTemperature => new ExifSignedRational(ExifTag.AmbientTemperature),
                ExifTagValue.WaterDepth => new ExifSignedRational(ExifTag.WaterDepth),
                ExifTagValue.CameraElevationAngle => new ExifSignedRational(ExifTag.CameraElevationAngle),

                ExifTagValue.Decode => new ExifSignedRationalArray(ExifTag.Decode),

                ExifTagValue.ImageDescription => new ExifString(ExifTag.ImageDescription),
                ExifTagValue.Make => new ExifString(ExifTag.Make),
                ExifTagValue.Model => new ExifString(ExifTag.Model),
                ExifTagValue.Software => new ExifString(ExifTag.Software),
                ExifTagValue.DateTime => new ExifString(ExifTag.DateTime),
                ExifTagValue.Artist => new ExifString(ExifTag.Artist),
                ExifTagValue.HostComputer => new ExifString(ExifTag.HostComputer),
                ExifTagValue.Copyright => new ExifString(ExifTag.Copyright),
                ExifTagValue.DocumentName => new ExifString(ExifTag.DocumentName),
                ExifTagValue.PageName => new ExifString(ExifTag.PageName),
                ExifTagValue.InkNames => new ExifString(ExifTag.InkNames),
                ExifTagValue.TargetPrinter => new ExifString(ExifTag.TargetPrinter),
                ExifTagValue.ImageID => new ExifString(ExifTag.ImageID),
                ExifTagValue.MDLabName => new ExifString(ExifTag.MDLabName),
                ExifTagValue.MDSampleInfo => new ExifString(ExifTag.MDSampleInfo),
                ExifTagValue.MDPrepDate => new ExifString(ExifTag.MDPrepDate),
                ExifTagValue.MDPrepTime => new ExifString(ExifTag.MDPrepTime),
                ExifTagValue.MDFileUnits => new ExifString(ExifTag.MDFileUnits),
                ExifTagValue.SEMInfo => new ExifString(ExifTag.SEMInfo),
                ExifTagValue.SpectralSensitivity => new ExifString(ExifTag.SpectralSensitivity),
                ExifTagValue.DateTimeOriginal => new ExifString(ExifTag.DateTimeOriginal),
                ExifTagValue.DateTimeDigitized => new ExifString(ExifTag.DateTimeDigitized),
                ExifTagValue.SubsecTime => new ExifString(ExifTag.SubsecTime),
                ExifTagValue.SubsecTimeOriginal => new ExifString(ExifTag.SubsecTimeOriginal),
                ExifTagValue.SubsecTimeDigitized => new ExifString(ExifTag.SubsecTimeDigitized),
                ExifTagValue.RelatedSoundFile => new ExifString(ExifTag.RelatedSoundFile),
                ExifTagValue.FaxSubaddress => new ExifString(ExifTag.FaxSubaddress),
                ExifTagValue.OffsetTime => new ExifString(ExifTag.OffsetTime),
                ExifTagValue.OffsetTimeOriginal => new ExifString(ExifTag.OffsetTimeOriginal),
                ExifTagValue.OffsetTimeDigitized => new ExifString(ExifTag.OffsetTimeDigitized),
                ExifTagValue.SecurityClassification => new ExifString(ExifTag.SecurityClassification),
                ExifTagValue.ImageHistory => new ExifString(ExifTag.ImageHistory),
                ExifTagValue.ImageUniqueID => new ExifString(ExifTag.ImageUniqueID),
                ExifTagValue.OwnerName => new ExifString(ExifTag.OwnerName),
                ExifTagValue.SerialNumber => new ExifString(ExifTag.SerialNumber),
                ExifTagValue.LensMake => new ExifString(ExifTag.LensMake),
                ExifTagValue.LensModel => new ExifString(ExifTag.LensModel),
                ExifTagValue.LensSerialNumber => new ExifString(ExifTag.LensSerialNumber),
                ExifTagValue.GDALMetadata => new ExifString(ExifTag.GDALMetadata),
                ExifTagValue.GDALNoData => new ExifString(ExifTag.GDALNoData),
                ExifTagValue.GPSLatitudeRef => new ExifString(ExifTag.GPSLatitudeRef),
                ExifTagValue.GPSLongitudeRef => new ExifString(ExifTag.GPSLongitudeRef),
                ExifTagValue.GPSSatellites => new ExifString(ExifTag.GPSSatellites),
                ExifTagValue.GPSStatus => new ExifString(ExifTag.GPSStatus),
                ExifTagValue.GPSMeasureMode => new ExifString(ExifTag.GPSMeasureMode),
                ExifTagValue.GPSSpeedRef => new ExifString(ExifTag.GPSSpeedRef),
                ExifTagValue.GPSTrackRef => new ExifString(ExifTag.GPSTrackRef),
                ExifTagValue.GPSImgDirectionRef => new ExifString(ExifTag.GPSImgDirectionRef),
                ExifTagValue.GPSMapDatum => new ExifString(ExifTag.GPSMapDatum),
                ExifTagValue.GPSDestLatitudeRef => new ExifString(ExifTag.GPSDestLatitudeRef),
                ExifTagValue.GPSDestLongitudeRef => new ExifString(ExifTag.GPSDestLongitudeRef),
                ExifTagValue.GPSDestBearingRef => new ExifString(ExifTag.GPSDestBearingRef),
                ExifTagValue.GPSDestDistanceRef => new ExifString(ExifTag.GPSDestDistanceRef),
                ExifTagValue.GPSDateStamp => new ExifString(ExifTag.GPSDateStamp),

                ExifTagValue.FileSource => new ExifByte(ExifTag.FileSource, ExifDataType.Undefined),
                ExifTagValue.SceneType => new ExifByte(ExifTag.SceneType, ExifDataType.Undefined),

                ExifTagValue.JPEGTables => new ExifByteArray(ExifTag.JPEGTables, ExifDataType.Undefined),
                ExifTagValue.OECF => new ExifByteArray(ExifTag.OECF, ExifDataType.Undefined),
                ExifTagValue.ExifVersion => new ExifByteArray(ExifTag.ExifVersion, ExifDataType.Undefined),
                ExifTagValue.ComponentsConfiguration => new ExifByteArray(ExifTag.ComponentsConfiguration, ExifDataType.Undefined),
                ExifTagValue.MakerNote => new ExifByteArray(ExifTag.MakerNote, ExifDataType.Undefined),
                ExifTagValue.UserComment => new ExifByteArray(ExifTag.UserComment, ExifDataType.Undefined),
                ExifTagValue.FlashpixVersion => new ExifByteArray(ExifTag.FlashpixVersion, ExifDataType.Undefined),
                ExifTagValue.SpatialFrequencyResponse => new ExifByteArray(ExifTag.SpatialFrequencyResponse, ExifDataType.Undefined),
                ExifTagValue.SpatialFrequencyResponse2 => new ExifByteArray(ExifTag.SpatialFrequencyResponse2, ExifDataType.Undefined),
                ExifTagValue.Noise => new ExifByteArray(ExifTag.Noise, ExifDataType.Undefined),
                ExifTagValue.CFAPattern => new ExifByteArray(ExifTag.CFAPattern, ExifDataType.Undefined),
                ExifTagValue.DeviceSettingDescription => new ExifByteArray(ExifTag.DeviceSettingDescription, ExifDataType.Undefined),
                ExifTagValue.ImageSourceData => new ExifByteArray(ExifTag.ImageSourceData, ExifDataType.Undefined),
                ExifTagValue.GPSProcessingMethod => new ExifByteArray(ExifTag.GPSProcessingMethod, ExifDataType.Undefined),
                ExifTagValue.GPSAreaInformation => new ExifByteArray(ExifTag.GPSAreaInformation, ExifDataType.Undefined),

                _ => null,
            };
        }
    }
}
