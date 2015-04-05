//=================================================================================================
// Copyright 2013-2015 Dirk Lemstra <https://magick.codeplex.com/>
//
// Licensed under the ImageMagick License (the "License"); you may not use this file except in 
// compliance with the License. You may obtain a copy of the License at
//
//   http://www.imagemagick.org/script/license.php
//
// Unless required by applicable law or agreed to in writing, software distributed under the
// License is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either
// express or implied. See the License for the specific language governing permissions and
// limitations under the License.
//=================================================================================================

using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Text;

namespace ImageMagick
{
	///=============================================================================================
	/// <summary>
	/// A value of the exif profile.
	/// </summary>
	public sealed class ExifValue : IEquatable<ExifValue>
	{
		//===========================================================================================
		object _Value;
		//===========================================================================================
		private void CheckValue(object value)
		{
			if (value == null)
				return;

			Type type = value.GetType();

			if (DataType == ExifDataType.Ascii)
			{
				Throw.IfFalse("value", type == typeof(string), "Value should be a string.");
				return;
			}

			if (type.IsArray)
			{
				Throw.IfTrue("value", !IsArray, "Value should not be an array.");
				type = type.GetElementType();
			}
			else
			{
				Throw.IfTrue("value", IsArray, "Value should be an array.");
			}

			switch (DataType)
			{
				case ExifDataType.Byte:
					Throw.IfFalse("value", type == typeof(Byte), "Value should be a byte{0}", IsArray ? " array." : ".");
					break;
				case ExifDataType.DoubleFloat:
				case ExifDataType.Rational:
				case ExifDataType.SignedRational:
					Throw.IfFalse("value", type == typeof(double), "Value should be a double{0}", IsArray ? " array." : ".");
					break;
				case ExifDataType.Long:
					Throw.IfFalse("value", type == typeof(UInt32), "Value should be an unsigned int{0}", IsArray ? " array." : ".");
					break;
				case ExifDataType.Short:
					Throw.IfFalse("value", type == typeof(UInt16), "Value should be an unsigned short{0}", IsArray ? " array." : ".");
					break;
				case ExifDataType.SignedByte:
					Throw.IfFalse("value", type == typeof(SByte), "Value should be a signed byte{0}", IsArray ? " array." : ".");
					break;
				case ExifDataType.SignedLong:
					Throw.IfFalse("value", type == typeof(Int32), "Value should be an int{0}", IsArray ? " array." : ".");
					break;
				case ExifDataType.SignedShort:
					Throw.IfFalse("value", type == typeof(Int16), "Value should be a short{0}", IsArray ? " array." : ".");
					break;
				case ExifDataType.SingleFloat:
					Throw.IfFalse("value", type == typeof(float), "Value should be a float{0}", IsArray ? " array." : ".");
					break;
				case ExifDataType.Undefined:
					Throw.IfFalse("value", type == typeof(Byte), "Value should be a byte array.");
					break;
				default:
					throw new NotImplementedException();
			}
		}
		//===========================================================================================
		private static ExifValue CreateNumber(ExifTag tag, Type type, bool isArray)
		{
			if (type == null || type == typeof(UInt16))
				return new ExifValue(tag, ExifDataType.Short, isArray);
			else if (type == typeof(Int16))
				return new ExifValue(tag, ExifDataType.SignedShort, isArray);
			else if (type == typeof(UInt32))
				return new ExifValue(tag, ExifDataType.Long, isArray);
			else
				return new ExifValue(tag, ExifDataType.SignedLong, isArray);
		}
		//===========================================================================================
		private string ToString(object value)
		{
			switch (DataType)
			{
				case ExifDataType.Ascii:
					return (string)value;
				case ExifDataType.Byte:
					return ((Byte)value).ToString("X2", CultureInfo.InvariantCulture);
				case ExifDataType.DoubleFloat:
					return ((double)value).ToString(CultureInfo.InvariantCulture);
				case ExifDataType.Long:
					return ((uint)value).ToString(CultureInfo.InvariantCulture);
				case ExifDataType.Rational:
					return ((double)value).ToString(CultureInfo.InvariantCulture);
				case ExifDataType.Short:
					return ((ushort)value).ToString(CultureInfo.InvariantCulture);
				case ExifDataType.SignedByte:
					return ((SByte)value).ToString("X2", CultureInfo.InvariantCulture);
				case ExifDataType.SignedLong:
					return ((int)value).ToString(CultureInfo.InvariantCulture);
				case ExifDataType.SignedRational:
					return ((double)value).ToString(CultureInfo.InvariantCulture);
				case ExifDataType.SignedShort:
					return ((short)value).ToString(CultureInfo.InvariantCulture);
				case ExifDataType.SingleFloat:
					return ((float)value).ToString(CultureInfo.InvariantCulture);
				case ExifDataType.Undefined:
					return ((Byte)value).ToString("X2", CultureInfo.InvariantCulture);
				default:
					throw new NotImplementedException();
			}
		}
		//===========================================================================================
		internal bool HasValue
		{
			get
			{
				if (_Value == null)
					return false;

				if (DataType == ExifDataType.Ascii)
					return ((string)_Value).Length > 0;

				return true;
			}
		}
		//===========================================================================================
		internal int Length
		{
			get
			{
				if (_Value == null)
					return 4;

				int size = (int)(GetSize(DataType) * NumberOfComponents);

				return size < 4 ? 4 : size;
			}
		}
		//===========================================================================================
		internal int NumberOfComponents
		{
			get
			{
				if (DataType == ExifDataType.Ascii)
					return Encoding.UTF8.GetBytes((string)_Value).Length;

				if (IsArray)
					return ((Array)_Value).Length;

				return 1;
			}
		}
		//===========================================================================================
		internal ExifValue(ExifTag tag, ExifDataType dataType, bool isArray)
		{
			Tag = tag;
			DataType = dataType;
			IsArray = isArray;

			if (dataType == ExifDataType.Ascii)
				IsArray = false;
		}
		//===========================================================================================
		internal ExifValue(ExifTag tag, ExifDataType dataType, object value, bool isArray)
			: this(tag, dataType, isArray)
		{
			_Value = value;
		}
		//===========================================================================================
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
		internal static ExifValue Create(ExifTag tag, object value)
		{
			Throw.IfTrue("tag", tag == ExifTag.Unknown, "Invalid Tag");

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
				case ExifTag.SpectralSensitivity:
				case ExifTag.DateTimeOriginal:
				case ExifTag.DateTimeDigitized:
				case ExifTag.SubsecTime:
				case ExifTag.SubsecTimeOriginal:
				case ExifTag.SubsecTimeDigitized:
				case ExifTag.RelatedSoundFile:
				case ExifTag.ImageUniqueID:
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
				case ExifTag.TileOffsets:
				case ExifTag.SMinSampleValue:
				case ExifTag.SMaxSampleValue:
				case ExifTag.JPEGQTables:
				case ExifTag.JPEGDCTables:
				case ExifTag.JPEGACTables:
				case ExifTag.StripRowCounts:
					exifValue = new ExifValue(tag, ExifDataType.Long, true);
					break;
				case ExifTag.SubIFDOffset:
				case ExifTag.GPSIFDOffset:
				case ExifTag.T4Options:
				case ExifTag.T6Options:
				case ExifTag.XClipPathUnits:
				case ExifTag.YClipPathUnits:
				case ExifTag.ProfileType:
				case ExifTag.CodingMethods:
				case ExifTag.JPEGInterchangeFormat:
				case ExifTag.JPEGInterchangeFormatLength:
					exifValue = new ExifValue(tag, ExifDataType.Long, false);
					break;

				case ExifTag.WhitePoint:
				case ExifTag.PrimaryChromaticities:
				case ExifTag.YCbCrCoefficients:
				case ExifTag.ReferenceBlackWhite:
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
				case ExifTag.ExposureTime:
				case ExifTag.FNumber:
				case ExifTag.CompressedBitsPerPixel:
				case ExifTag.ApertureValue:
				case ExifTag.MaxApertureValue:
				case ExifTag.SubjectDistance:
				case ExifTag.FocalLength:
				case ExifTag.FlashEnergy:
				case ExifTag.FocalPlaneXResolution:
				case ExifTag.FocalPlaneYResolution:
				case ExifTag.ExposureIndex:
				case ExifTag.DigitalZoomRatio:
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
				case ExifTag.ISOSpeedRatings:
				case ExifTag.SubjectArea:
				case ExifTag.SubjectLocation:
					exifValue = new ExifValue(tag, ExifDataType.Short, true);
					break;
				case ExifTag.Compression:
				case ExifTag.PhotometricInterpretation:
				case ExifTag.Threshholding:
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
				case ExifTag.ExposureProgram:
				case ExifTag.MeteringMode:
				case ExifTag.LightSource:
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
				case ExifTag.CFAPattern:
				case ExifTag.DeviceSettingDescription:
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
					throw new NotImplementedException();
			}

			exifValue.Value = value;
			return exifValue;
		}
		//===========================================================================================
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
					throw new NotImplementedException(dataType.ToString());
			}
		}
		///==========================================================================================
		/// <summary>
		/// The data type of the exif value.
		/// </summary>
		public ExifDataType DataType
		{
			get;
			private set;
		}
		///==========================================================================================
		/// <summary>
		/// Returns true if the value is an array.
		/// </summary>
		public bool IsArray
		{
			get;
			private set;
		}
		///==========================================================================================
		/// <summary>
		/// The tag of the exif value.
		/// </summary>
		public ExifTag Tag
		{
			get;
			private set;
		}
		///==========================================================================================
		/// <summary>
		/// The value.
		/// </summary>
		public object Value
		{
			get
			{
				return _Value;
			}
			set
			{
				CheckValue(value);
				_Value = value;
			}
		}
		///==========================================================================================
		/// <summary>
		/// Determines whether the specified ExifValue instances are considered equal.
		/// </summary>
		/// <param name="left">The first ExifValue to compare.</param>
		/// <param name="right"> The second ExifValue to compare.</param>
		/// <returns></returns>
		public static bool operator ==(ExifValue left, ExifValue right)
		{
			return object.Equals(left, right);
		}
		///==========================================================================================
		/// <summary>
		/// Determines whether the specified ExifValue instances are not considered equal.
		/// </summary>
		/// <param name="left">The first ExifValue to compare.</param>
		/// <param name="right"> The second ExifValue to compare.</param>
		/// <returns></returns>
		public static bool operator !=(ExifValue left, ExifValue right)
		{
			return !object.Equals(left, right);
		}
		///==========================================================================================
		///<summary>
		/// Determines whether the specified object is equal to the current exif value.
		///</summary>
		///<param name="obj">The object to compare this exif value with.</param>
		public override bool Equals(object obj)
		{
			if (ReferenceEquals(this, obj))
				return true;

			return Equals(obj as ExifValue);
		}
		///==========================================================================================
		///<summary>
		/// Determines whether the specified exif value is equal to the current exif value.
		///</summary>
		///<param name="other">The exif value to compare this exif value with.</param>
		public bool Equals(ExifValue other)
		{
			if (ReferenceEquals(other, null))
				return false;

			if (ReferenceEquals(this, other))
				return true;

			return
				Tag == other.Tag &&
				DataType == other.DataType &&
				object.Equals(_Value, other._Value);
		}
		///==========================================================================================
		///<summary>
		/// Servers as a hash of this type.
		///</summary>
		public override int GetHashCode()
		{
			int hashCode = Tag.GetHashCode() ^ DataType.GetHashCode();
			return _Value != null ? hashCode ^ _Value.GetHashCode() : hashCode;
		}
		///==========================================================================================
		///<summary>
		/// Returns a string that represents the current value.
		///</summary>
		public override string ToString()
		{

			if (_Value == null)
				return null;

			if (DataType == ExifDataType.Ascii)
				return (string)_Value;

			if (!IsArray)
				return ToString(_Value);

			StringBuilder sb = new StringBuilder();
			foreach (Object value in (Array)_Value)
			{
				sb.Append(ToString(value));
				sb.Append(" ");
			}

			return sb.ToString();
		}
		//===========================================================================================
	}
	//==============================================================================================
}