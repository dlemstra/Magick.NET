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
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;

namespace ImageMagick
{
    /// <summary>
    /// A value of the exif profile.
    /// </summary>
    public sealed class ExifValue : IExifValue
    {
        private object _value;

        internal ExifValue(ExifTag tag, ExifDataType dataType, bool isArray)
        {
            Tag = tag;
            DataType = dataType;
            IsArray = isArray;

            if (dataType == ExifDataType.Ascii)
                IsArray = false;
        }

        internal ExifValue(ExifTag tag, ExifDataType dataType, object value, bool isArray)
          : this(tag, dataType, isArray)
        {
            _value = value;
        }

        /// <summary>
        /// Gets the data type of the exif value.
        /// </summary>
        public ExifDataType DataType { get; }

        /// <summary>
        /// Gets a value indicating whether the value is an array.
        /// </summary>
        public bool IsArray { get; }

        /// <summary>
        /// Gets the tag of the exif value.
        /// </summary>
        public ExifTag Tag { get; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public object Value
        {
            get => _value;
            set
            {
                CheckValue(value);
                _value = value;
            }
        }

        internal bool HasValue
        {
            get
            {
                if (_value == null)
                    return false;

                if (DataType == ExifDataType.Ascii)
                    return ((string)_value).Length > 0;

                return true;
            }
        }

        internal int Length
        {
            get
            {
                if (_value == null)
                    return 4;

                int size = (int)(GetSize(DataType) * NumberOfComponents);

                return size < 4 ? 4 : size;
            }
        }

        internal int NumberOfComponents
        {
            get
            {
                if (DataType == ExifDataType.Ascii)
                    return Encoding.UTF8.GetBytes((string)_value).Length;

                if (IsArray)
                    return ((Array)_value).Length;

                return 1;
            }
        }

        /// <summary>
        /// Returns a string that represents the current value.
        /// </summary>
        /// <returns>A string that represents the current value.</returns>
        public override string ToString()
        {
            if (_value == null)
                return null;

            if (DataType == ExifDataType.Ascii)
                return (string)_value;

            if (!IsArray)
                return ToString(_value);

            StringBuilder sb = new StringBuilder();
            foreach (object value in (Array)_value)
            {
                sb.Append(ToString(value));
                sb.Append(" ");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Tries to set the value and returns a value indicating whether the value could be set.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>A value indicating whether the value could be set.</returns>
        public bool TrySetValue(object value)
        {
            return false;
        }

        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Cannot avoid it here.")]
        internal static ExifValue Create(ExifTag tag, object value)
        {
            Throw.IfTrue(nameof(tag), tag == ExifTag.Unknown, "Invalid Tag");

            ExifValue exifValue = null;
            Type type = value != null ? value.GetType() : null;
            if (type != null && type.IsArray)
                type = type.GetElementType();

            switch (tag)
            {
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
                    exifValue = new ExifValue(tag, ExifDataType.Ascii, true);
                    break;

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
                    exifValue = new ExifValue(tag, ExifDataType.Byte, true);
                    break;
                case ExifTag.FaxProfile:
                case ExifTag.ModeNumber:
                case ExifTag.GPSAltitudeRef:
                    exifValue = new ExifValue(tag, ExifDataType.Byte, false);
                    break;

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
                    exifValue = new ExifValue(tag, ExifDataType.Long, true);
                    break;
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
                    exifValue = new ExifValue(tag, ExifDataType.Long, false);
                    break;

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
                    exifValue = new ExifValue(tag, ExifDataType.Rational, true);
                    break;
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
                    exifValue = new ExifValue(tag, ExifDataType.Rational, false);
                    break;

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
                    exifValue = new ExifValue(tag, ExifDataType.Short, true);
                    break;
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
                    exifValue = new ExifValue(tag, ExifDataType.Short, false);
                    break;

                case ExifTag.Decode:
                    exifValue = new ExifValue(tag, ExifDataType.SignedRational, true);
                    break;
                case ExifTag.ShutterSpeedValue:
                case ExifTag.BrightnessValue:
                case ExifTag.ExposureBiasValue:
                case ExifTag.AmbientTemperature:
                case ExifTag.WaterDepth:
                case ExifTag.CameraElevationAngle:
                    exifValue = new ExifValue(tag, ExifDataType.SignedRational, false);
                    break;

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
                    exifValue = new ExifValue(tag, ExifDataType.Undefined, true);
                    break;
                case ExifTag.FileSource:
                case ExifTag.SceneType:
                    exifValue = new ExifValue(tag, ExifDataType.Undefined, false);
                    break;

                case ExifTag.StripOffsets:
                case ExifTag.TileByteCounts:
                case ExifTag.ImageLayer:
                    exifValue = CreateNumber(tag, type, true);
                    break;
                case ExifTag.ImageWidth:
                case ExifTag.ImageLength:
                case ExifTag.TileWidth:
                case ExifTag.TileLength:
                case ExifTag.BadFaxLines:
                case ExifTag.ConsecutiveBadFaxLines:
                case ExifTag.PixelXDimension:
                case ExifTag.PixelYDimension:
                    exifValue = CreateNumber(tag, type, false);
                    break;

                default:
                    throw new NotSupportedException();
            }

            exifValue.Value = value;
            return exifValue;
        }

        internal static uint GetSize(ExifDataType dataType)
        {
            switch (dataType)
            {
                case ExifDataType.Ascii:
                case ExifDataType.Byte:
                case ExifDataType.SignedByte:
                case ExifDataType.Undefined:
                    return 1;
                case ExifDataType.Short:
                case ExifDataType.SignedShort:
                    return 2;
                case ExifDataType.Long:
                case ExifDataType.SignedLong:
                case ExifDataType.SingleFloat:
                    return 4;
                case ExifDataType.DoubleFloat:
                case ExifDataType.Rational:
                case ExifDataType.SignedRational:
                    return 8;
                default:
                    throw new NotSupportedException(dataType.ToString());
            }
        }

        private static ExifValue CreateNumber(ExifTag tag, Type type, bool isArray)
        {
            if (type == null || type == typeof(ushort))
                return new ExifValue(tag, ExifDataType.Short, isArray);
            else if (type == typeof(short))
                return new ExifValue(tag, ExifDataType.SignedShort, isArray);
            else if (type == typeof(uint))
                return new ExifValue(tag, ExifDataType.Long, isArray);
            else
                return new ExifValue(tag, ExifDataType.SignedLong, isArray);
        }

        [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Cannot avoid it here.")]
        private void CheckValue(object value)
        {
            if (value == null)
                return;

            Type type = value.GetType();

            if (DataType == ExifDataType.Ascii)
            {
                Throw.IfFalse(nameof(value), type == typeof(string), "Value should be a string.");
                return;
            }

            if (type.IsArray)
            {
                Throw.IfTrue(nameof(value), !IsArray, "Value should not be an array.");
                type = type.GetElementType();
            }
            else
            {
                Throw.IfTrue(nameof(value), IsArray, "Value should be an array.");
            }

            switch (DataType)
            {
                case ExifDataType.Byte:
                    Throw.IfFalse(nameof(value), type == typeof(byte), "Value should be a byte{0}", IsArray ? " array." : ".");
                    break;
                case ExifDataType.DoubleFloat:
                    Throw.IfFalse(nameof(value), type == typeof(double), "Value should be a double{0}", IsArray ? " array." : ".");
                    break;
                case ExifDataType.Long:
                    Throw.IfFalse(nameof(value), type == typeof(uint), "Value should be an unsigned int{0}", IsArray ? " array." : ".");
                    break;
                case ExifDataType.Rational:
                    Throw.IfFalse(nameof(value), type == typeof(Rational), "Value should be a rational{0}", IsArray ? " array." : ".");
                    break;
                case ExifDataType.Short:
                    Throw.IfFalse(nameof(value), type == typeof(ushort), "Value should be an unsigned short{0}", IsArray ? " array." : ".");
                    break;
                case ExifDataType.SignedByte:
                    Throw.IfFalse(nameof(value), type == typeof(sbyte), "Value should be a signed byte{0}", IsArray ? " array." : ".");
                    break;
                case ExifDataType.SignedLong:
                    Throw.IfFalse(nameof(value), type == typeof(int), "Value should be an int{0}", IsArray ? " array." : ".");
                    break;
                case ExifDataType.SignedRational:
                    Throw.IfFalse(nameof(value), type == typeof(SignedRational), "Value should be a signed rational{0}", IsArray ? " array." : ".");
                    break;
                case ExifDataType.SignedShort:
                    Throw.IfFalse(nameof(value), type == typeof(short), "Value should be a short{0}", IsArray ? " array." : ".");
                    break;
                case ExifDataType.SingleFloat:
                    Throw.IfFalse(nameof(value), type == typeof(float), "Value should be a float{0}", IsArray ? " array." : ".");
                    break;
                case ExifDataType.Undefined:
                    Throw.IfFalse(nameof(value), type == typeof(byte), "Value should be a byte array.");
                    break;
                default:
                    throw new NotSupportedException();
            }
        }

        private string ToString(object value)
        {
            string description = ExifTagDescriptionAttribute.GetDescription(Tag, value);
            if (description != null)
                return description;

            switch (DataType)
            {
                case ExifDataType.Ascii:
                    return (string)value;
                case ExifDataType.Byte:
                    return ((byte)value).ToString("X2", CultureInfo.InvariantCulture);
                case ExifDataType.DoubleFloat:
                    return ((double)value).ToString(CultureInfo.InvariantCulture);
                case ExifDataType.Long:
                    return ((uint)value).ToString(CultureInfo.InvariantCulture);
                case ExifDataType.Rational:
                    return ((Rational)value).ToString(CultureInfo.InvariantCulture);
                case ExifDataType.Short:
                    return ((ushort)value).ToString(CultureInfo.InvariantCulture);
                case ExifDataType.SignedByte:
                    return ((sbyte)value).ToString("X2", CultureInfo.InvariantCulture);
                case ExifDataType.SignedLong:
                    return ((int)value).ToString(CultureInfo.InvariantCulture);
                case ExifDataType.SignedRational:
                    return ((SignedRational)value).ToString(CultureInfo.InvariantCulture);
                case ExifDataType.SignedShort:
                    return ((short)value).ToString(CultureInfo.InvariantCulture);
                case ExifDataType.SingleFloat:
                    return ((float)value).ToString(CultureInfo.InvariantCulture);
                case ExifDataType.Undefined:
                    return ((byte)value).ToString("X2", CultureInfo.InvariantCulture);
                default:
                    throw new NotSupportedException();
            }
        }
    }
}
