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

#include "Enums\LogEvents.h"
#include "Events\LogEventArgs.h"
#include "MagickFormatInfo.h"

using namespace System::Collections::Generic;
using namespace System::Reflection;
using namespace System::Runtime::InteropServices;

namespace ImageMagick
{
	///=============================================================================================
	[UnmanagedFunctionPointerAttribute(CallingConvention::Cdecl)]
	private delegate void MagickLogFuncDelegate(const Magick::LogEventType type, const char* text);
	///=============================================================================================
	///<summary>
	/// Class that can be used to initialize Magick.NET.
	///</summary>
	public ref class MagickNET abstract sealed
	{
		//===========================================================================================
	private:
		//===========================================================================================
		static initonly array<String^>^ _ImageMagickFiles = gcnew array<String^>
		{
			"coder.xml", "colors.xml", "configure.xml", "delegates.xml", "english.xml", "locale.xml",
				"log.xml", "magic.xml", "policy.xml", "thresholds.xml", "type.xml", "type-ghostscript.xml"
		};
		static MagickLogFuncDelegate^ _LogDelegate;
		static EventHandler<LogEventArgs^>^ _LogEvent;
		//static bool _UseOpenCL = true;
		//===========================================================================================
		static String^ CheckDirectory(String^ path);
		//===========================================================================================
		static void CheckImageMagickFiles(String^ path);
		//===========================================================================================
		static void OnLog(const Magick::LogEventType type, const char* text);
		//===========================================================================================
		static void SetEnv(const char* name, String^ value);
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Returns the features reported by ImageMagick.
		///</summary>
		static property String^ Features
		{
			String^ get();
		}
		///==========================================================================================
		static event EventHandler<LogEventArgs^>^ Log
		{
			void add(EventHandler<LogEventArgs^>^ handler);
			void remove(EventHandler<LogEventArgs^>^ handler);
		}
		///==========================================================================================
		///<summary>
		/// Returns information about the supported formats.
		///</summary>
		static property IEnumerable<MagickFormatInfo^>^ SupportedFormats
		{
			IEnumerable<MagickFormatInfo^>^ get();
		}
		/*///==========================================================================================
		///<summary>
		/// Gets or sets the use of OpenCL.
		///</summary>
		static property bool UseOpenCL
		{
			bool get();
			void set(bool value);
		}*/
		///==========================================================================================
		///<summary>
		/// Returns the version of Magick.NET.
		///</summary>
		static property String^ Version
		{
			String^ get();
		}
		///==========================================================================================
		///<summary>
		/// Returns the format information of the specified format.
		///</summary>
		///<param name="format">The image format.</param>
		static MagickFormatInfo^ GetFormatInformation(MagickFormat format);
		///==========================================================================================
		///<summary>
		/// Adds the specified path to the environment path. You should place the ImageMagick
		/// xml files in that directory.
		///</summary>
		///<param name="path">The path that contains the ImageMagick xml files.</param>
		static void Initialize(String^ path);
		///==========================================================================================
		///<summary>
		/// Set the events that will be written to the log. The log will be written to the Log event
		/// and the debug window in VisualStudio. To change the log settings you must use a custom
		/// log.xml file.
		///</summary>
		///<param name="events">The events that will be logged.</param>
		static void SetLogEvents(LogEvents events);
		///==========================================================================================
		///<summary>
		/// Sets the directory that will be used when ImageMagick does not have enough memory for the
		/// pixel cache.
		///</summary>
		///<param name="path">The path where temp files will be written.</param>
		static void SetTempDirectory(String^ path);
		///==========================================================================================
		///<summary>
#if (_M_X64)
		/// Sets the directory that contains the Ghostscript file gsdll64.dll.
#else
		/// Sets the directory that contains the Ghostscript file gsdll32.dll.
#endif
		///</summary>
		///<param name="path">The path of the Ghostscript directory.</param>
		static void SetGhostscriptDirectory(String^ path);
		///==========================================================================================
		///<summary>
		/// Sets the directory that contains the Ghostscript font files.
		///</summary>
		///<param name="path">The path of the Ghostscript font directory.</param>
		static void SetGhostscriptFontDirectory(String^ path);
		//===========================================================================================
	};
	//==============================================================================================
}