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
	double* Marshaller::Marshal(array<double>^ values)
	{
		if (values == nullptr || values->Length == 0)
			return NULL;

		double* unmanagedValue = new double[values->Length];
		for(int i = 0; i < values->Length; i++)
		{
			unmanagedValue[i] = values[i];
		}

		return unmanagedValue;
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
	double* Marshaller::MarshalAndTerminate(array<double>^ values)
	{
		if (values == nullptr || values->Length == 0)
			return NULL;

		int zeroIndex = Array::IndexOf(values, 0.0);
		int length = zeroIndex == -1 ? values->Length + 1 : zeroIndex + 1;

		double* unmanagedValue = new double[length];
		for(int i = 0; i < length - 1; i++)
		{
			unmanagedValue[i] = values[i];
		}
		unmanagedValue[length - 1] = 0.0;

		return unmanagedValue;
	}
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
		if (value.empty())
			return nullptr;
		else
			return gcnew String(value.c_str());
	}
	//==============================================================================================
}