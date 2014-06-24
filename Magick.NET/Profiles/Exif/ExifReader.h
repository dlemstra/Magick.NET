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

#include "ExifValue.h"
#include "ExifDataType.h"

using namespace System::Collections::Generic;

namespace ImageMagick
{
	//==============================================================================================
	private ref class ExifReader sealed
	{
		//===========================================================================================
	private:
		//===========================================================================================
		generic<typename TDataType>
		where TDataType : value class
			delegate TDataType ConverterMethod(array<Byte>^ data);
		//===========================================================================================
		array<Byte>^ _Data;
		unsigned int _Index;
		bool _IsLittleEndian;
		unsigned int _GPSIFDoffset;
		unsigned int _StartIndex;
		unsigned int _SubIFDoffset;
		unsigned int _ThumbnailLength;
		unsigned int _ThumbnailOffset;
		//===========================================================================================
		void AddValues(List<ExifValue^>^ values, unsigned int index);
		//===========================================================================================
		Object^ ConvertValue(ExifDataType dataType, array<Byte>^ data, int numberOfComponents);
		//===========================================================================================
		ExifValue^ CreateValue();
		//===========================================================================================
		array<Byte>^ GetBytes(unsigned int length);
		//===========================================================================================
		static unsigned int GetSize(ExifDataType dataType);
		//===========================================================================================
		String^ GetString(unsigned int length);
		//===========================================================================================
		void GetThumbnail();
		//===========================================================================================
		unsigned short GetUInt16();
		//===========================================================================================
		unsigned int GetUInt32();
		//===========================================================================================
		generic<typename TDataType>
		where TDataType : value class
			static array<TDataType>^ ToArray(ExifDataType dataType, array<Byte>^ data,
			ConverterMethod<TDataType>^ converter);
		//===========================================================================================
		static Byte ToByte(array<Byte>^ data);
		//===========================================================================================
		double ToDouble(array<Byte>^ data);
		//===========================================================================================
		short ToInt16(array<Byte>^ data);
		//===========================================================================================
		int ToInt32(array<Byte>^ data);
		//===========================================================================================
		double ToRational(array<Byte>^ data);
		//===========================================================================================
		SByte ToSByte(array<Byte>^ data);
		//===========================================================================================
		float ToSingle(array<Byte>^ data);
		//===========================================================================================
		static String^ ToString(array<Byte>^ data);
		//===========================================================================================
		unsigned short ToUInt16(array<Byte>^ data);
		//===========================================================================================
		unsigned int ToUInt32(array<Byte>^ data);
		//===========================================================================================
		double ToURational(array<Byte>^ data);
		//===========================================================================================
		bool ValidateArray(array<Byte>^ data, int size);
		//===========================================================================================
	public:
		//===========================================================================================
		property unsigned int ThumbnailLength
		{
			unsigned int get();
		}
		//===========================================================================================
		property unsigned int ThumbnailOffset
		{
			unsigned int get();
		}
		//===========================================================================================
		List<ExifValue^>^ Read(array<Byte>^ data);
		//===========================================================================================
	};
	//==============================================================================================
}