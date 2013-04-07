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
#include "stdafx.h"

using namespace System::Runtime::InteropServices;

namespace ImageMagick
{
	//==============================================================================================
	std::string& Marshaller::Marshal(String^ value, std::string &unmanagedValue)
	{
		if (value == nullptr)
			return unmanagedValue;

		const char* chars = (const char*)(Marshal::StringToHGlobalAnsi(value)).ToPointer();
		unmanagedValue = chars;
		Marshal::FreeHGlobal(IntPtr((void*)chars));
		return unmanagedValue;
	}
	//==============================================================================================
	String^ Marshaller::Marshal(const std::string& value)
	{
		return gcnew String(value.c_str());
	}
	//==============================================================================================
	Magick::Blob* Marshaller::Marshal(array<Byte>^ bytes)
	{
		if (bytes == nullptr || bytes->Length == 0)
			return new Magick::Blob();

		char* unmanagedValue = new char[bytes->Length];
		Marshal::Copy(bytes, 0, IntPtr(unmanagedValue), bytes->Length);
		return new Magick::Blob(unmanagedValue, bytes->Length);
	}
	//==============================================================================================
	double* Marshaller::Marshal(array<double>^ doubles)
	{
		if (doubles == nullptr || doubles->Length == 0)
			return NULL;

		double* unmanagedValue = new double[doubles->Length];
		for (int i = 0; i < doubles->Length; i++)
		{
			unmanagedValue[i] = doubles[i];
		}

		return unmanagedValue;
	}
	//==============================================================================================
}