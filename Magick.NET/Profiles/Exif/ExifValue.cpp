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
#include "Stdafx.h"
#include "ExifValue.h"

using namespace System::Globalization;
using namespace System::Text;

namespace ImageMagick
{
	//==============================================================================================
	void ExifValue::CheckValue(Object^ value)
	{
		if (value == nullptr)
			return;

		Type^ type = value->GetType();

		if (_DataType == ExifDataType::Ascii)
		{
			Throw::IfFalse("value", type == String::typeid, "Value should be a string.");
			return;
		}

		if (type->IsArray)
		{
			Throw::IfTrue("value", !_IsArray, "Value should not be an array.");
			type = type->GetElementType();
		}
		else
		{
			Throw::IfTrue("value", _IsArray, "Value should be an array.");
		}

		switch(_DataType)
		{
		case ExifDataType::Byte:
			Throw::IfFalse("value", type == Byte::typeid, "Value should be a byte{0}", _IsArray ? " array." : ".");
			break;
		case ExifDataType::DoubleFloat:
		case ExifDataType::Rational:
		case ExifDataType::SignedRational:
			Throw::IfFalse("value", type == double::typeid, "Value should be a double{0}", _IsArray ? " array." : ".");
			break;
		case ExifDataType::Long:
			Throw::IfFalse("value", type == UInt32::typeid, "Value should be an unsigned int{0}", _IsArray ? " array." : ".");
			break;
		case ExifDataType::Short:
			Throw::IfFalse("value", type == UInt16::typeid, "Value should be an unsigned short{0}", _IsArray ? " array." : ".");
			break;
		case ExifDataType::SignedByte:
			Throw::IfFalse("value", type == SByte::typeid, "Value should be a signed byte{0}", _IsArray ? " array." : ".");
			break;
		case ExifDataType::SignedLong:
			Throw::IfFalse("value", type == Int32::typeid, "Value should be an int{0}", _IsArray ? " array." : ".");
			break;
		case ExifDataType::SignedShort:
			Throw::IfFalse("value", type == Int16::typeid, "Value should be a short{0}", _IsArray ? " array." : ".");
			break;
		case ExifDataType::SingleFloat:
			Throw::IfFalse("value", type == float::typeid, "Value should be a float{0}", _IsArray ? " array." : ".");
			break;
		case ExifDataType::Undefined:
			Throw::IfFalse("value", type == Byte::typeid, "Value should be a byte array.");
			break;
		default:
			throw gcnew NotImplementedException();
		}
	}
	//==============================================================================================
	void ExifValue::Initialize(ExifTag tag, ExifDataType dataType, bool isArray)
	{
		_Tag = tag;
		_DataType = dataType;
		_IsArray = isArray;

		if (dataType == ExifDataType::Ascii)
			_IsArray = false;
	}
	//==============================================================================================
	String^ ExifValue::ToString(Object^ value)
	{
		switch(_DataType)
		{
		case ExifDataType::Ascii:
			return (String^)value;
		case ExifDataType::Byte:
			return ((Byte)value).ToString("X2", CultureInfo::InvariantCulture);
		case ExifDataType::DoubleFloat:
			return ((double)value).ToString(CultureInfo::InvariantCulture);
		case ExifDataType::Long:
			return ((unsigned int)value).ToString(CultureInfo::InvariantCulture);
		case ExifDataType::Rational:
			return ((double)value).ToString(CultureInfo::InvariantCulture);
		case ExifDataType::Short:
			return ((unsigned short)value).ToString(CultureInfo::InvariantCulture);
		case ExifDataType::SignedByte:
			return ((SByte)value).ToString("X2", CultureInfo::InvariantCulture);
		case ExifDataType::SignedLong:
			return ((int)value).ToString(CultureInfo::InvariantCulture);
		case ExifDataType::SignedRational:
			return ((double)value).ToString(CultureInfo::InvariantCulture);
		case ExifDataType::SignedShort:
			return ((short)value).ToString(CultureInfo::InvariantCulture);
		case ExifDataType::SingleFloat:
			return ((float)value).ToString(CultureInfo::InvariantCulture);
		case ExifDataType::Undefined:
			return ((Byte)value).ToString("X2", CultureInfo::InvariantCulture);
		default:
			throw gcnew NotImplementedException();
		}
	}
	//==============================================================================================
	bool ExifValue::HasValue::get()
	{
		if (_Value == nullptr)
			return false;

		if (_DataType == ExifDataType::Ascii)
			return ((String^)_Value)->Length > 0;

		return true;
	}
	//==============================================================================================
	int ExifValue::Length::get()
	{
		if (_Value == nullptr)
			return 4;

		int size = GetSize(_DataType) * NumberOfComponents;

		return size < 4 ? 4 : size;
	}
	//==============================================================================================
	int ExifValue::NumberOfComponents::get()
	{
		if (_DataType == ExifDataType::Ascii)
			return Encoding::UTF8->GetBytes((String^)_Value)->Length;

		if (_IsArray)
			return ((Array^)_Value)->Length;

		return 1;
	}
	//==============================================================================================
	ExifValue::ExifValue(ExifTag tag, ExifDataType dataType, bool isArray)
	{
		Initialize(tag, dataType, isArray);
	}
	//==============================================================================================
	ExifValue::ExifValue(ExifTag tag, ExifDataType dataType, Object^ value, bool isArray)
	{
		Initialize(tag, dataType, isArray);
		_Value = value;
	}
	//==============================================================================================
	ExifValue^ ExifValue::Create(ExifTag tag, Object^ value)
	{
		Throw::IfTrue("tag", tag == ExifTag::Unknown, "Invalid Tag");

		ExifValue^ exifValue = nullptr;
		bool isArray = false;
		Type^ type = value != nullptr ? value->GetType() : nullptr;
		if (type != nullptr && type->IsArray)
			type = type->GetElementType();

		switch(tag)
		{
		case ExifTag::ImageDescription:
		case ExifTag::Make:
		case ExifTag::Model:
		case ExifTag::Software:
		case ExifTag::DateTime:
		case ExifTag::Artist:
		case ExifTag::HostComputer:
		case ExifTag::Copyright:
		case ExifTag::DocumentName:
		case ExifTag::PageName:
		case ExifTag::InkNames:
		case ExifTag::TargetPrinter:
		case ExifTag::ImageID:
		case ExifTag::SpectralSensitivity:
		case ExifTag::DateTimeOriginal:
		case ExifTag::DateTimeDigitized:
		case ExifTag::SubsecTime:
		case ExifTag::SubsecTimeOriginal:
		case ExifTag::SubsecTimeDigitized:
		case ExifTag::RelatedSoundFile:
		case ExifTag::ImageUniqueID:
		case ExifTag::GPSLatitudeRef:
		case ExifTag::GPSLongitudeRef:
		case ExifTag::GPSSatellites:
		case ExifTag::GPSStatus:
		case ExifTag::GPSMeasureMode:
		case ExifTag::GPSSpeedRef:
		case ExifTag::GPSTrackRef:
		case ExifTag::GPSImgDirectionRef:
		case ExifTag::GPSMapDatum:
		case ExifTag::GPSDestLatitudeRef:
		case ExifTag::GPSDestLongitudeRef:
		case ExifTag::GPSDestBearingRef:
		case ExifTag::GPSDestDistanceRef:
		case ExifTag::GPSDateStamp:
			exifValue = gcnew ExifValue(tag, ExifDataType::Ascii, true);
			break;

		case ExifTag::ClipPath:
		case ExifTag::VersionYear:
		case ExifTag::XMP:
		case ExifTag::GPSVersionID:
			isArray = true;
		case ExifTag::FaxProfile:
		case ExifTag::ModeNumber:
		case ExifTag::GPSAltitudeRef:
			exifValue = gcnew ExifValue(tag, ExifDataType::Byte, isArray);
			break;

		case ExifTag::FreeOffsets:
		case ExifTag::FreeByteCounts:
		case ExifTag::TileOffsets:
		case ExifTag::SMinSampleValue:
		case ExifTag::SMaxSampleValue:
		case ExifTag::JPEGQTables:
		case ExifTag::JPEGDCTables:
		case ExifTag::JPEGACTables:
		case ExifTag::StripRowCounts:
			isArray = true;
		case ExifTag::SubIFDOffset:
		case ExifTag::GPSIFDOffset:
		case ExifTag::T4Options:
		case ExifTag::T6Options:
		case ExifTag::XClipPathUnits:
		case ExifTag::YClipPathUnits:
		case ExifTag::ProfileType:
		case ExifTag::CodingMethods:
		case ExifTag::JPEGInterchangeFormat:
		case ExifTag::JPEGInterchangeFormatLength:
			exifValue = gcnew ExifValue(tag, ExifDataType::Long, isArray);
			break;

		case ExifTag::WhitePoint:
		case ExifTag::PrimaryChromaticities:
		case ExifTag::YCbCrCoefficients:
		case ExifTag::ReferenceBlackWhite:
		case ExifTag::GPSLatitude:
		case ExifTag::GPSLongitude:
		case ExifTag::GPSTimestamp:
		case ExifTag::GPSDestLatitude:
		case ExifTag::GPSDestLongitude:
			isArray = true;
		case ExifTag::XPosition:
		case ExifTag::YPosition:
		case ExifTag::XResolution:
		case ExifTag::YResolution:
		case ExifTag::ExposureTime:
		case ExifTag::FNumber:
		case ExifTag::CompressedBitsPerPixel:
		case ExifTag::ApertureValue:
		case ExifTag::MaxApertureValue:
		case ExifTag::SubjectDistance:
		case ExifTag::FocalLength:
		case ExifTag::FlashEnergy:
		case ExifTag::FocalPlaneXResolution:
		case ExifTag::FocalPlaneYResolution:
		case ExifTag::ExposureIndex:
		case ExifTag::DigitalZoomRatio:
		case ExifTag::GPSAltitude:
		case ExifTag::GPSDOP:
		case ExifTag::GPSSpeed:
		case ExifTag::GPSTrack:
		case ExifTag::GPSImgDirection:
		case ExifTag::GPSDestBearing:
		case ExifTag::GPSDestDistance:
			exifValue = gcnew ExifValue(tag, ExifDataType::Rational, isArray);
			break;

		case ExifTag::BitsPerSample:
		case ExifTag::MinSampleValue:
		case ExifTag::MaxSampleValue:
		case ExifTag::GrayResponseCurve:
		case ExifTag::ColorMap:
		case ExifTag::ExtraSamples:
		case ExifTag::PageNumber:
		case ExifTag::TransferFunction:
		case ExifTag::Predictor:
		case ExifTag::HalftoneHints:
		case ExifTag::SampleFormat:
		case ExifTag::TransferRange:
		case ExifTag::DefaultImageColor:
		case ExifTag::JPEGLosslessPredictors:
		case ExifTag::JPEGPointTransforms:
		case ExifTag::YCbCrSubsampling:
		case ExifTag::ISOSpeedRatings:
		case ExifTag::SubjectArea:
		case ExifTag::SubjectLocation:
			isArray = true;
		case ExifTag::Compression:
		case ExifTag::PhotometricInterpretation:
		case ExifTag::Threshholding:
		case ExifTag::CellWidth:
		case ExifTag::CellLength:
		case ExifTag::FillOrder:
		case ExifTag::Orientation:
		case ExifTag::SamplesPerPixel:
		case ExifTag::PlanarConfiguration:
		case ExifTag::GrayResponseUnit:
		case ExifTag::ResolutionUnit:
		case ExifTag::CleanFaxData:
		case ExifTag::InkSet:
		case ExifTag::NumberOfInks:
		case ExifTag::DotRange:
		case ExifTag::Indexed:
		case ExifTag::OPIProxy:
		case ExifTag::JPEGProc:
		case ExifTag::JPEGRestartInterval:
		case ExifTag::YCbCrPositioning:
		case ExifTag::ExposureProgram:
		case ExifTag::MeteringMode:
		case ExifTag::LightSource:
		case ExifTag::Flash:
		case ExifTag::ColorSpace:
		case ExifTag::FocalPlaneResolutionUnit:
		case ExifTag::SensingMethod:
		case ExifTag::CustomRendered:
		case ExifTag::ExposureMode:
		case ExifTag::WhiteBalance:
		case ExifTag::FocalLengthIn35mmFilm:
		case ExifTag::SceneCaptureType:
		case ExifTag::GainControl:
		case ExifTag::Contrast:
		case ExifTag::Saturation:
		case ExifTag::Sharpness:
		case ExifTag::SubjectDistanceRange:
		case ExifTag::GPSDifferential:
			exifValue = gcnew ExifValue(tag, ExifDataType::Short, isArray);
			break;

		case ExifTag::Decode:
			isArray = true;
		case ExifTag::ShutterSpeedValue:
		case ExifTag::BrightnessValue:
		case ExifTag::ExposureBiasValue:
			exifValue = gcnew ExifValue(tag, ExifDataType::SignedRational, isArray);
			break;

		case ExifTag::JPEGTables:
		case ExifTag::OECF:
		case ExifTag::ExifVersion:
		case ExifTag::ComponentsConfiguration:
		case ExifTag::MakerNote:
		case ExifTag::UserComment:
		case ExifTag::FlashpixVersion:
		case ExifTag::SpatialFrequencyResponse:
		case ExifTag::CFAPattern:
		case ExifTag::DeviceSettingDescription:
		case ExifTag::GPSProcessingMethod:
		case ExifTag::GPSAreaInformation:
			isArray = true;
		case ExifTag::FileSource:
		case ExifTag::SceneType:
			exifValue = gcnew ExifValue(tag, ExifDataType::Undefined, isArray);
			break;

		case ExifTag::StripOffsets:
		case ExifTag::TileByteCounts:
		case ExifTag::ImageLayer:
			isArray = true;
		case ExifTag::ImageWidth:
		case ExifTag::ImageLength:
		case ExifTag::TileWidth:
		case ExifTag::TileLength:
		case ExifTag::BadFaxLines:
		case ExifTag::ConsecutiveBadFaxLines:
		case ExifTag::PixelXDimension:
		case ExifTag::PixelYDimension:
			if (type == nullptr || type == UInt16::typeid)
				exifValue = gcnew ExifValue(tag, ExifDataType::Short, isArray);
			else if (type == Int16::typeid)
				exifValue = gcnew ExifValue(tag, ExifDataType::SignedShort, isArray);
			else if (type == UInt32::typeid)
				exifValue = gcnew ExifValue(tag, ExifDataType::Long, isArray);
			else
				exifValue = gcnew ExifValue(tag, ExifDataType::SignedLong, isArray);
			break;

		default:
			throw gcnew NotImplementedException();
		}

		exifValue->Value = value;
		return exifValue;
	}
	//==============================================================================================
	unsigned int ExifValue::GetSize(ExifDataType dataType)
	{
		switch (dataType)
		{
		case ExifDataType::Ascii:
		case ExifDataType::Byte:
		case ExifDataType::SignedByte:
		case ExifDataType::Undefined:
			return 1;
		case ExifDataType::Short:
		case ExifDataType::SignedShort:
			return 2;
		case ExifDataType::Long:
		case ExifDataType::SignedLong:
		case ExifDataType::SingleFloat:
			return 4;
		case ExifDataType::DoubleFloat:
		case ExifDataType::Rational:
		case ExifDataType::SignedRational:
			return 8;
		default:
			throw gcnew NotImplementedException(dataType.ToString());
		}
	}
	//==============================================================================================
	ExifDataType ExifValue::DataType::get()
	{
		return _DataType;
	}
	//==============================================================================================
	bool ExifValue::IsArray::get()
	{
		return _IsArray;
	}
	//==============================================================================================
	ExifTag ExifValue::Tag::get()
	{
		return _Tag;
	}
	//==============================================================================================
	Object^ ExifValue::Value::get()
	{
		return _Value;
	}
	//==============================================================================================
	void ExifValue::Value::set(Object^ value)
	{
		CheckValue(value);
		_Value = value;
	}
	//==============================================================================================
	bool ExifValue::operator == (ExifValue^ left, ExifValue^ right)
	{
		return Object::Equals(left, right);
	}
	//==============================================================================================
	bool ExifValue::operator != (ExifValue^ left, ExifValue^ right)
	{
		return !Object::Equals(left, right);
	}
	//==============================================================================================
	bool ExifValue::Equals(Object^ obj)
	{
		if (ReferenceEquals(this, obj))
			return true;

		return Equals(dynamic_cast<ExifValue^>(obj));
	}
	//==============================================================================================
	bool ExifValue::Equals(ExifValue^ other)
	{
		if (ReferenceEquals(other, nullptr))
			return false;

		if (ReferenceEquals(this, other))
			return true;

		return
			_Tag == other->_Tag &&
			_DataType == other->_DataType &&
			Object::Equals(_Value, other->_Value);
	}
	//==============================================================================================
	int ExifValue::GetHashCode()
	{
		int hashCode = _Tag.GetHashCode() ^ _DataType.GetHashCode();
		return _Value != nullptr ? hashCode ^ _Value->GetHashCode() : hashCode;
	}
	//==============================================================================================
	String^ ExifValue::ToString()
	{
		if (_Value == nullptr)
			return nullptr;

		if (_DataType == ExifDataType::Ascii)
			return (String^)_Value;

		if (!_IsArray)
			return ToString(_Value);

		StringBuilder^ sb = gcnew StringBuilder();
		for each(Object^ value in (Array^)_Value)
		{
			sb->Append(ToString(value));
			sb->Append(" ");
		}

		return sb->ToString();
	}
	//==============================================================================================
}