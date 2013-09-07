//=================================================================================================
// Copyright 2013 Dirk Lemstra <http://magick.codeplex.com/>
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

#include "..\Enums\StorageType.h"

namespace ImageMagick
{
	//==============================================================================================
	private ref class Marshaller abstract sealed
	{
	private:
		//===========================================================================================
		template <typename TStorageType>
		static TStorageType* MarshalStorageType(array<Byte>^ bytes);
		//===========================================================================================
	public:
		//===========================================================================================
		static unsigned char* Marshal(array<Byte>^ bytes);
		//===========================================================================================
		static void Marshal(array<Byte>^ bytes, Magick::Blob* value);
		//===========================================================================================
		static void* Marshal(array<Byte>^ bytes, StorageType storageType);
		//===========================================================================================
		static double* Marshal(array<double>^ values);
		//===========================================================================================
		static array<Byte>^ Marshal(Magick::Blob* value);
		//===========================================================================================
		static array<double>^ Marshal(const double* values);
		//===========================================================================================
		static String^ Marshal(const std::string& value);
		//===========================================================================================
		static std::string& Marshal(String^, std::string& value);
		//===========================================================================================
		static double* MarshalAndTerminate(array<double>^ values);
		//===========================================================================================
	};
	//==============================================================================================
}