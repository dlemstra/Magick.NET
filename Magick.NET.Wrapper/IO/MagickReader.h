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

#include "..\Colors\MagickColor.h"
#include "MagickReaderSettings.h"

using namespace System::IO;
using namespace System::Runtime::InteropServices;

namespace ImageMagick
{
	namespace Wrapper
	{
		//===========================================================================================
		private ref class MagickReader abstract sealed
		{
			//========================================================================================
		private:
			//========================================================================================
			static int GetExpectedLength(MagickReaderSettings^ settings);
			//========================================================================================
			static void ReadPixels(Magick::Image* image, MagickReaderSettings^ settings,
				array<Byte>^ pixels);
			//========================================================================================
			static array<Byte>^ ReadUnchecked(String^ filePath);
			//========================================================================================
		internal:
			//========================================================================================
			static MagickException^ Read(Magick::Image* image, array<Byte>^ bytes,
				MagickReaderSettings^ settings);
			//========================================================================================
			static MagickException^ Read(Magick::Image* image, MagickColor^ color,
				int width, int height);
			//========================================================================================
			static MagickException^ Read(Magick::Image* image, Stream^ stream,
				MagickReaderSettings^ readSettings);
			//========================================================================================
			static MagickException^ Read(Magick::Image* image, String^ fileName,
				MagickReaderSettings^ readSettings);
			//========================================================================================
			static MagickException^ Read(Magick::Image* image, String^ fileName,
				int width, int height);
			//========================================================================================
			static MagickException^ Read(std::vector<Magick::Image>* imageList, array<Byte>^ bytes,
				MagickReaderSettings^ readSettings);
			//========================================================================================
			static MagickException^ Read(std::vector<Magick::Image>* imageList, Stream^ stream,
				MagickReaderSettings^ readSettings);
			//========================================================================================
			static MagickException^ Read(std::vector<Magick::Image>* imageList, String^ fileName,
				MagickReaderSettings^ readSettings);
			//========================================================================================
		public:
			//========================================================================================
			static array<Byte>^ Read(Stream^ stream);
			//========================================================================================
			static array<Byte>^ Read(String^ fileName);
			//========================================================================================
		};
		//===========================================================================================
	}
}