//=================================================================================================
// Copyright 2013-2014 Dirk Lemstra <https://magick.codeplex.com/>
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
#pragma once

#include "ExifParts.h"
#include "ExifValue.h"
#include "ExifTag.h"

using namespace System::Collections::Generic;

namespace ImageMagick
{
	//==============================================================================================
	private ref class ExifWriter sealed
	{
		//===========================================================================================
	private:
		//===========================================================================================
		static array<ExifTag>^ _IfdTags = gcnew array<ExifTag>(82)
		{
			ExifTag::Threshholding, ExifTag::CellWidth, ExifTag::CellLength, ExifTag::FillOrder,
				ExifTag::ImageDescription, ExifTag::Make, ExifTag::Model,  ExifTag::Orientation, 
				ExifTag::MinSampleValue, ExifTag::MaxSampleValue, ExifTag::XResolution, ExifTag::YResolution,
				ExifTag::FreeOffsets, ExifTag::FreeByteCounts, ExifTag::GrayResponseUnit, ExifTag::GrayResponseCurve,
				ExifTag::ResolutionUnit, ExifTag::Software, ExifTag::DateTime, ExifTag::Artist,
				ExifTag::HostComputer, ExifTag::ColorMap, ExifTag::ExtraSamples, ExifTag::Copyright,
				ExifTag::DocumentName, ExifTag::PageName,	ExifTag::XPosition, ExifTag::YPosition,
				ExifTag::T4Options, ExifTag::T6Options, ExifTag::PageNumber, ExifTag::TransferFunction,
				ExifTag::Predictor, ExifTag::WhitePoint, ExifTag::PrimaryChromaticities,
				ExifTag::HalftoneHints, ExifTag::TileWidth, ExifTag::TileLength, ExifTag::TileOffsets,
				ExifTag::TileByteCounts, ExifTag::BadFaxLines, ExifTag::CleanFaxData,
				ExifTag::ConsecutiveBadFaxLines, ExifTag::InkSet, ExifTag::InkNames, ExifTag::NumberOfInks,
				ExifTag::DotRange, ExifTag::TargetPrinter, ExifTag::SampleFormat, ExifTag::SMinSampleValue,
				ExifTag::SMaxSampleValue, ExifTag::TransferRange, ExifTag::ClipPath, ExifTag::XClipPathUnits,
				ExifTag::YClipPathUnits, ExifTag::Indexed, ExifTag::JPEGTables, ExifTag::OPIProxy,
				ExifTag::ProfileType, ExifTag::FaxProfile, ExifTag::CodingMethods, ExifTag::VersionYear,
				ExifTag::ModeNumber, ExifTag::Decode, ExifTag::DefaultImageColor, ExifTag::JPEGProc,
				ExifTag::JPEGInterchangeFormat, ExifTag::JPEGInterchangeFormatLength,
				ExifTag::JPEGRestartInterval, ExifTag::JPEGLosslessPredictors, ExifTag::JPEGPointTransforms,
				ExifTag::JPEGQTables, ExifTag::JPEGDCTables, ExifTag::JPEGACTables, ExifTag::YCbCrCoefficients,
				ExifTag::YCbCrSubsampling, ExifTag::YCbCrPositioning, ExifTag::ReferenceBlackWhite,
				ExifTag::StripRowCounts, ExifTag::XMP, ExifTag::ImageID, ExifTag::ImageLayer
		};
		static array<ExifTag>^ _ExifTags = gcnew array<ExifTag>(56)
		{
			ExifTag::ExposureTime, ExifTag::FNumber, ExifTag::ExposureProgram, ExifTag::SpectralSensitivity,
				ExifTag::ISOSpeedRatings, ExifTag::OECF, ExifTag::ExifVersion, ExifTag::DateTimeOriginal,
				ExifTag::DateTimeDigitized, ExifTag::ComponentsConfiguration, ExifTag::CompressedBitsPerPixel,
				ExifTag::ShutterSpeedValue, ExifTag::ApertureValue, ExifTag::BrightnessValue,
				ExifTag::ExposureBiasValue, ExifTag::MaxApertureValue, ExifTag::SubjectDistance,
				ExifTag::MeteringMode, ExifTag::LightSource, ExifTag::Flash, ExifTag::FocalLength,
				ExifTag::SubjectArea, ExifTag::MakerNote, ExifTag::UserComment, ExifTag::SubsecTime,
				ExifTag::SubsecTimeOriginal, ExifTag::SubsecTimeDigitized, ExifTag::FlashpixVersion,
				ExifTag::ColorSpace, ExifTag::PixelXDimension, ExifTag::PixelYDimension, ExifTag::RelatedSoundFile,
				ExifTag::FlashEnergy, ExifTag::SpatialFrequencyResponse, ExifTag::FocalPlaneXResolution,
				ExifTag::FocalPlaneYResolution, ExifTag::FocalPlaneResolutionUnit, ExifTag::SubjectLocation,
				ExifTag::ExposureIndex, ExifTag::SensingMethod, ExifTag::FileSource, ExifTag::SceneType,
				ExifTag::CFAPattern, ExifTag::CustomRendered, ExifTag::ExposureMode, ExifTag::WhiteBalance,
				ExifTag::DigitalZoomRatio, ExifTag::FocalLengthIn35mmFilm, ExifTag::SceneCaptureType,
				ExifTag::GainControl, ExifTag::Contrast, ExifTag::Saturation, ExifTag::Sharpness,
				ExifTag::DeviceSettingDescription, ExifTag::SubjectDistanceRange, ExifTag::ImageUniqueID
		};
		static array<ExifTag>^ _GPSTags = gcnew array<ExifTag>(31)
		{
			ExifTag::GPSVersionID, ExifTag::GPSLatitudeRef, ExifTag::GPSLatitude, ExifTag::GPSLongitudeRef,
				ExifTag::GPSLongitude, ExifTag::GPSAltitudeRef, ExifTag::GPSAltitude, ExifTag::GPSTimestamp,
				ExifTag::GPSSatellites, ExifTag::GPSStatus, ExifTag::GPSMeasureMode, ExifTag::GPSDOP,
				ExifTag::GPSSpeedRef, ExifTag::GPSSpeed, ExifTag::GPSTrackRef, ExifTag::GPSTrack,
				ExifTag::GPSImgDirectionRef, ExifTag::GPSImgDirection, ExifTag::GPSMapDatum,
				ExifTag::GPSDestLatitudeRef, ExifTag::GPSDestLatitude, ExifTag::GPSDestLongitudeRef,
				ExifTag::GPSDestLongitude, ExifTag::GPSDestBearingRef, ExifTag::GPSDestBearing,
				ExifTag::GPSDestDistanceRef, ExifTag::GPSDestDistance, ExifTag::GPSProcessingMethod,
				ExifTag::GPSAreaInformation, ExifTag::GPSDateStamp, ExifTag::GPSDifferential
		};
		//===========================================================================================
		static const int _StartIndex = 6;
		//===========================================================================================
		ExifParts _AllowedParts;
		List<ExifValue^>^ _Values;
		List<int>^ _DataOffsets;
		List<int>^ _IfdIndexes;
		List<int>^ _ExifIndexes;
		List<int>^ _GPSIndexes;
		//===========================================================================================
		int GetIndex(List<int>^ indexes, ExifTag tag);
		//===========================================================================================
		List<int>^ GetIndexes(ExifParts part, array<ExifTag>^ tags);
		//===========================================================================================
		int GetLength(IEnumerable<int>^ indexes);
		//===========================================================================================
		template<typename TDataType>
		static int Write(TDataType value, array<Byte>^ destination, int offset);
		//===========================================================================================
		static int WriteArray(ExifValue^ value, array<Byte>^ destination, int offset);
		//===========================================================================================
		int WriteData(List<int>^ indexes, array<Byte>^ destination, int offset);
		//===========================================================================================
		int WriteHeaders(List<int>^ indexes, array<Byte>^ destination, int offset);
		//===========================================================================================
		static int WriteRational(double value, array<Byte>^ destination, int offset);
		//===========================================================================================
		static int WriteURational(double value, array<Byte>^ destination, int offset);
		//===========================================================================================
		static int WriteValue(ExifDataType dataType, Object^ value, array<Byte>^ destination, int offset);
		//===========================================================================================
		static int WriteValue(ExifValue^ value, array<Byte>^ destination, int offset);
		//===========================================================================================
	public:
		//===========================================================================================
		ExifWriter(List<ExifValue^>^ values, ExifParts allowedParts);
		//===========================================================================================
		array<Byte>^ GetData();
		//===========================================================================================
	};
	//==============================================================================================
}