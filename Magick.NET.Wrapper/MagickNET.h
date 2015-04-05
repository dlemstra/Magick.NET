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
#pragma once

#include "MagickFormatInfo.h"

using namespace System::Collections::Generic;
using namespace System::Reflection;
using namespace System::Runtime::InteropServices;

namespace ImageMagick
{
	namespace Wrapper
	{
		//===========================================================================================
		[UnmanagedFunctionPointerAttribute(CallingConvention::Cdecl)]
		private delegate void MagickLogFuncDelegate(const Magick::LogEventType type, const char* text);
		//===========================================================================================
		private delegate void MagickLogDelegate(LogEvents type, String^ text);
		//===========================================================================================
		private ref class MagickNET abstract sealed
		{
			//========================================================================================
		private:
			//========================================================================================
			static MagickLogFuncDelegate^ _InternalLogDelegate;
			static MagickLogDelegate^ _ExternalLogDelegate;
			static Nullable<bool> _UseOpenCL;
			//========================================================================================
			static void OnLog(const Magick::LogEventType type, const char* text);
			//========================================================================================
			static bool SetUseOpenCL(bool value);
			//========================================================================================
		public:
			//========================================================================================
			static property String^ Features
			{
				String^ get();
			}
			//========================================================================================
			static property IEnumerable<MagickFormatInfo^>^ SupportedFormats
			{
				IEnumerable<MagickFormatInfo^>^ get();
			}
			//========================================================================================
			static property bool UseOpenCL
			{
				bool get();
				void set(bool value);
			}
			//========================================================================================
			static MagickFormatInfo^ GetFormatInformation(MagickFormat format);
			//========================================================================================
			static void SetEnv(String^ name, String^ value);
			//========================================================================================
			static void SetLogDelegate(MagickLogDelegate^ logDelegate);
			//========================================================================================
			static void SetLogEvents(String^ events);
			//========================================================================================
		};
		//===========================================================================================
	}
}