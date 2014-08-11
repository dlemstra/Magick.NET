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
#include "Stdafx.h"

using namespace System::Runtime::InteropServices;

namespace ImageMagick
{
	//==============================================================================================
	template <typename TStorageType>
	TStorageType* Marshaller::MarshalStorageType(array<Byte>^ bytes)
	{
		int length = bytes->Length / sizeof(TStorageType);
		TStorageType* unmanagedValue = new TStorageType[length];
		Marshal::Copy(bytes, 0, IntPtr(unmanagedValue), bytes->Length);
		return unmanagedValue;
	}
	//==============================================================================================
	unsigned char* Marshaller::Marshal(array<Byte>^ bytes)
	{
		if (bytes == nullptr || bytes->Length == 0)
			return NULL;

		unsigned char* unmanagedValue = new unsigned char[bytes->Length];
		Marshal::Copy(bytes, 0, IntPtr(unmanagedValue), bytes->Length);
		return unmanagedValue;
	}
	//==============================================================================================
	void Marshaller::Marshal(array<Byte>^ bytes, Magick::Blob* value)
	{
		if (bytes == nullptr || bytes->Length == 0)
			return;

		char* unmanagedValue = new char[bytes->Length];
		Marshal::Copy(bytes, 0, IntPtr(unmanagedValue), bytes->Length);
		value->update(unmanagedValue, bytes->Length);
		delete[] unmanagedValue;
	}
	//==============================================================================================
	void* Marshaller::Marshal(array<Byte>^ values, StorageType storageType)
	{
		Throw::IfNullOrEmpty("values", values);

		switch(storageType)
		{
		case StorageType::Char:
			return Marshal(values);
		case StorageType::Double:
			return MarshalStorageType<double>(values);
		case StorageType::Float:
			return MarshalStorageType<float>(values);
		case StorageType::Long:
			return MarshalStorageType<int>(values);
		case StorageType::LongLong:
			return MarshalStorageType<long>(values);
		case StorageType::Quantum:
			return  MarshalStorageType<Magick::Quantum>(values);
		case StorageType::Short:
			return MarshalStorageType<short>(values);
		case StorageType::Undefined:
		default:
			throw gcnew NotImplementedException();
		}
	}
	//==============================================================================================
	double* Marshaller::Marshal(array<double>^ values)
	{
		if (values == nullptr || values->Length == 0)
			return NULL;

		double* unmanagedValue = new double[values->Length];
		Marshal::Copy(values, 0, IntPtr(unmanagedValue), values->Length);
		return unmanagedValue;
	}
	//==============================================================================================
	array<Byte>^ Marshaller::Marshal(Magick::Blob* value)
	{
		if (value == NULL || value->length() == 0)
			return nullptr;

		int length = Convert::ToInt32(value->length());
		array<Byte>^ data = gcnew array<Byte>(length);
		IntPtr ptr = IntPtr((void*)value->data());
		Marshal::Copy(ptr, data, 0, length);
		return data;
	}
	//==============================================================================================
	array<double>^ Marshaller::Marshal(const double* values)
	{
		if (values == NULL)
			return nullptr;

		int length = 0;
		const double* v = values;
		while(*v != 0.0)
		{
			length++;
			v++;
		}

		array<double>^ managedValue = gcnew array<double>(length);

		int index = 0;
		v = values;
		while(*v != 0.0)
		{
			managedValue[index++] = *v++;
		}

		return managedValue;
	}
	//==============================================================================================
	String^ Marshaller::Marshal(const std::string& value)
	{
		if (value.empty())
			return nullptr;
		else
			return gcnew String(value.c_str(), 0, (int)value.length(), System::Text::Encoding::UTF8);
	}
	//==============================================================================================
	std::string& Marshaller::Marshal(String^ value, std::string &unmanagedValue)
	{
		if (value == nullptr)
			return unmanagedValue;

		if (value->Length == 0)
		{
			unmanagedValue = "";
			return unmanagedValue;
		}

		array<Byte>^ bytes = System::Text::Encoding::UTF8->GetBytes(value);
		pin_ptr<unsigned char> bytesPtr = &bytes[0];
		unmanagedValue = std::string(reinterpret_cast<char *>(bytesPtr), bytes->Length);
		return unmanagedValue;
	}
	//==============================================================================================
	double* Marshaller::MarshalAndTerminate(array<double>^ values)
	{
		if (values == nullptr || values->Length == 0)
			return NULL;

		int zeroIndex = Array::IndexOf(values, 0.0);
		int length = zeroIndex == -1 ? values->Length + 1 : zeroIndex + 1;

		double* unmanagedValue = new double[length];
		Marshal::Copy(values, 0, IntPtr(unmanagedValue), length - 1);
		unmanagedValue[length - 1] = 0.0;

		return unmanagedValue;
	}
	//==============================================================================================
}