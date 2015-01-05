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

#include "Enums\MagickFormat.h"

using namespace System::Collections::Generic;
using namespace System::Collections::ObjectModel;

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Class that contains information about an image format.
	///</summary>
	public ref class MagickFormatInfo sealed
	{
		//===========================================================================================
	private:
		//===========================================================================================
		String^ _Description;
		MagickFormat _Format;
		String^ _MimeType;
		Magick::CoderInfo* _CoderInfo;
		//===========================================================================================
		MagickFormatInfo() {};
		//===========================================================================================
		static void AddStealthCoder(std::vector<Magick::CoderInfo>* coderList, std::string name);
		//===========================================================================================
		static Collection<MagickFormatInfo^>^ LoadFormats();
		//===========================================================================================
	internal:
		//===========================================================================================
		static initonly Collection<MagickFormatInfo^>^ All = LoadFormats();
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// The description of the format.
		///</summary>
		property String^ Description
		{
			String^ get();
		}
		///==========================================================================================
		///<summary>
		/// The format.
		///</summary>
		property MagickFormat Format
		{
			MagickFormat get();
		}
		///==========================================================================================
		///<summary>
		/// Format supports multiple frames.
		///</summary>
		property bool IsMultiFrame
		{
			bool get();
		}
		///==========================================================================================
		///<summary>
		/// Format is readable.
		///</summary>
		property bool IsReadable
		{
			bool get();
		}
		///==========================================================================================
		///<summary>
		/// Format is writable.
		///</summary>
		property bool IsWritable
		{
			bool get();
		}
		///==========================================================================================
		///<summary>
		/// The mime type.
		///</summary>
		property String^ MimeType
		{
			String^ get();
		}
		///==========================================================================================
		///<summary>
		/// Returns a string that represents the current format.
		///</summary>
		virtual String^ ToString() override;
		///==========================================================================================
		///<summary>
		/// Unregisters this format.
		///</summary>
		bool Unregister(void);
		//===========================================================================================
	};
	//==============================================================================================
}