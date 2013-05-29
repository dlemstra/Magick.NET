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

#include "..\Exceptions\Base\MagickException.h"
#include "..\Exceptions\MagickWarningExceptions.h"
#include "..\MagickImage.h"
#include "MagickReadSettings.h"

using namespace System::IO;
using namespace System::Runtime::InteropServices;

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Class that can be used to read images.
	///</summary>
	private ref class MagickReader abstract sealed
	{
		//===========================================================================================
	private:
		//===========================================================================================
		static void ApplySettings(Magick::Image* image, MagickReadSettings^ readSettings);
		//===========================================================================================
		static void ApplySettings(MagickCore::ImageInfo *imageInfo, MagickReadSettings^ readSettings);
		//===========================================================================================
		static MagickWarningException^ Read(Magick::Image* image, Magick::Blob* blob,
			MagickReadSettings^ readSettings);
		//===========================================================================================
		static MagickWarningException^ Read(std::list<Magick::Image>* imageList, Magick::Blob* blob,
			MagickReadSettings^ readSettings);
	internal:
		//===========================================================================================
		static array<Byte>^ Read(Stream^ stream);
		//===========================================================================================
		static void Read(Magick::Blob* blob, Stream^ stream);
		//===========================================================================================
		static void Read(Magick::Blob* blob, String^ fileName);
		//===========================================================================================
		static MagickWarningException^ Read(Magick::Image* image, array<Byte>^ data,
			MagickReadSettings^ readSettings);
		//===========================================================================================
		static MagickWarningException^ Read(Magick::Image* image, Stream^ stream,
			MagickReadSettings^ readSettings);
		//===========================================================================================
		static MagickWarningException^ Read(Magick::Image* image, String^ fileName,
			MagickReadSettings^ readSettings);
		//===========================================================================================
		static MagickWarningException^ Read(std::list<Magick::Image>* imageList, array<Byte>^ data,
			MagickReadSettings^ readSettings);
		//===========================================================================================
		static MagickWarningException^ Read(std::list<Magick::Image>* imageList, Stream^ stream,
			MagickReadSettings^ readSettings);
		//===========================================================================================
		static MagickWarningException^ Read(std::list<Magick::Image>* imageList, String^ fileName,
			MagickReadSettings^ readSettings);
		//===========================================================================================
	};
}