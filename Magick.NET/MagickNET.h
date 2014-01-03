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
#include "MagickFormatInfo.h"

using namespace System::Reflection;
using namespace System::Collections::Generic;

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Class that can be used to initialize Magick.NET.
	///</summary>
	public ref class MagickNET abstract sealed
	{
		//===========================================================================================
	private:
		//===========================================================================================
		static bool _ThrowWarnings = false;
		static initonly array<String^>^ _ImageMagickFiles = gcnew array<String^>
		{
			"coder.xml", "colors.xml", "configure.xml", "delegates.xml",
			"english.xml", "locale.xml", "log.xml", "magic.xml",
			"policy.xml", "thresholds.xml", "type.xml", "type-ghostscript.xml"
		};
		//===========================================================================================
		static void CheckImageMagickFiles(String^ path);
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
		///<summary>
		/// Returns information about the supported formats.
		///</summary>
		static property IEnumerable<MagickFormatInfo^>^ SupportedFormats
		{
			IEnumerable<MagickFormatInfo^>^ get();
		}
		///==========================================================================================
		///<summary>
		/// Gets or sets whether WarningExceptions should be thrown.
		///</summary>
		static property bool ThrowWarnings
		{
			bool get();
			void set(bool value);
		}
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
		/// Pixel cache threshold in bytes. Once this memory threshold is exceeded, all subsequent
		/// pixels cache operations are to/from disk. This setting is shared by all MagickImage objects.
		///</summary>
		///<param name="threshold">The threshold in bytes.</param>
		static void SetCacheThreshold(Magick::MagickSizeType threshold);
		///==========================================================================================
		///<summary>
		/// Set the events that will be written to the log. The log will be written to the console and
		/// the debug window in VisualStudio. To change the log settings you must use a custom log.xml
		/// file.
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
		//===========================================================================================
	};
	//==============================================================================================
}