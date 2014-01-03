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

#include "..\ImageProfile.h"
#include "ExifValue.h"

using namespace System::Collections::Generic;
using namespace System::IO;

namespace ImageMagick
{
	//==============================================================================================
	ref class MagickImage;
	///=============================================================================================
	/// <summary>
	/// Class that can be used to access an Exif profile.
	/// </summary>
	public ref class ExifProfile sealed : ImageProfile
	{
		//===========================================================================================
	private:
		//===========================================================================================
		List<ExifValue^>^ _Values;
		unsigned int _ThumbnailOffset;
		unsigned int _ThumbnailLength;
		//===========================================================================================
		void Initialize();
		//===========================================================================================
	internal:
		//===========================================================================================
		ExifProfile() {};
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the ExifProfile class.
		///</summary>
		///<param name="data">The byte array to read the exif profile from.</param>
		ExifProfile(array<Byte>^ data) : ImageProfile("exif", data) {};
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the ExifProfile class.
		///</summary>
		///<param name="fileName">The fully qualified name of the exif profile file, or the relative
		/// exif profile file name.</param>
		ExifProfile(String^ fileName) : ImageProfile("exif", fileName) {};
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the ExifProfile class.
		///</summary>
		///<param name="stream">The stream to read the exif profile from.</param>
		ExifProfile(Stream^ stream) : ImageProfile("exif", stream) {};
		///==========================================================================================
		///<summary>
		/// Returns the values of this exif profile.
		///</summary>
		property IEnumerable<ExifValue^>^ Values
		{
			IEnumerable<ExifValue^>^ get();
		}
		///==========================================================================================
		///<summary>
		/// Returns the thumbnail in the exif profile when available.
		///</summary>
		MagickImage^ CreateThumbnail();
		//===========================================================================================
	};
	//==============================================================================================
}