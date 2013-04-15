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

#include "..\Enums\ColorSpace.h"
#include "..\Colors\ColorProfile.h"
#include "..\MagickImage.h"
#include "MagickException.h"

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
		static String^ GetReadWarning(Magick::Warning warning);
		//===========================================================================================
		static String^ Read(Magick::Image* image, bool ping, MagickBlob^ blob, Nullable<int> width,
			Nullable<int> height, Nullable<ColorSpace> colorSpace);
		//===========================================================================================
		static String^ Read(Magick::Image* image, bool ping, String^ fileName, Nullable<int> width,
			Nullable<int> height, Nullable<ColorSpace> colorSpace);
		//===========================================================================================
		static String^ Read(Magick::Image* image, bool ping, Stream^ stream, Nullable<int> width,
			Nullable<int> height, Nullable<ColorSpace> colorSpace);
		//===========================================================================================
		static String^ Read(std::list<Magick::Image>* imageList, String^ fileName,
			Nullable<ColorSpace> colorSpace);
		//===========================================================================================
		static void SetColorSpace(Magick::Image* image, ColorSpace colorSpace);
		//===========================================================================================
		static void SetColorSpace(std::list<Magick::Image>* image, ColorSpace colorSpace);
		//===========================================================================================
		static void SetSize(Magick::Image* image, Nullable<int> width, Nullable<int> height);
		//===========================================================================================
	internal:
		//===========================================================================================
		static String^ Read(Magick::Image* image, bool ping, MagickBlob^ blob);
		//===========================================================================================
		static String^ Read(Magick::Image* image, bool ping, MagickBlob^ blob, ColorSpace colorSpace);
		//===========================================================================================
		static String^ Read(Magick::Image* image, bool ping, String^ fileName);
		//===========================================================================================
		static String^ Read(Magick::Image* image, bool ping, String^ fileName, ColorSpace colorSpace);
		//===========================================================================================
		static String^ Read(Magick::Image* image, bool ping, Stream^ stream);
		//===========================================================================================
		static String^ Read(Magick::Image* image, bool ping, Stream^ stream, ColorSpace colorSpace);
		//===========================================================================================
		static String^ Read(Magick::Image* image, MagickBlob^ blob, int width, int height);
		//===========================================================================================
		static String^ Read(Magick::Image* image, String^ fileName, int width, int height);
		//===========================================================================================
		static String^ Read(Magick::Image* image, Stream^ stream, int width, int height);
		//===========================================================================================
		static String^ Read(std::list<Magick::Image>* imageList, String^ fileName);
		//===========================================================================================
		static String^ Read(std::list<Magick::Image>* imageList, String^ fileName, ColorSpace colorSpace);
		//===========================================================================================
	};
}