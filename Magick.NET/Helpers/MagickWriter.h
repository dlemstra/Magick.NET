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

#include "..\MagickBlob.h"
#include "..\Exceptions\Base\MagickException.h"

using namespace System::IO;

namespace ImageMagick
{
	//==============================================================================================
	private ref class MagickWriter abstract sealed
	{
		//===========================================================================================
	private:
		//===========================================================================================
		static void MagickWriter::WriteUnchecked(Magick::Blob* blob, Stream^ stream);
		//===========================================================================================
	public:
		//===========================================================================================
		static void Write(Magick::Blob* blob, array<Byte>^ data);
		//===========================================================================================
		static void Write(Magick::Blob* blob, Stream^ stream);
		//===========================================================================================
		static void Write(Magick::Blob* blob, String^ fileName);
		//===========================================================================================
		static void Write(Magick::Image* image, Magick::Blob* blob);
		//===========================================================================================
		static void Write(Magick::Image* image, Stream^ stream);
		//===========================================================================================
		static void Write(Magick::Image* image, String^ fileName);
		//===========================================================================================
		static void Write(std::list<Magick::Image>* imageList, Magick::Blob* blob);
		//===========================================================================================
		static void Write(std::list<Magick::Image>* imageList, Stream^ stream);
		//===========================================================================================
		static void Write(std::list<Magick::Image>* imageList, String^ fileName);
		//===========================================================================================
	};
	//==============================================================================================
}