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
#include "../Helpers/MagickException.h"
#include "PixelBaseCollection.h"

using namespace System::Collections::Generic;

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Class that can be used to access the individual pixels of an image and modify them.
	///</summary>
	//==============================================================================================
	public ref class WritablePixelCollection sealed : PixelBaseCollection
	{
		//===========================================================================================
	private:
		//===========================================================================================
		Magick::PixelPacket* _Pixels;
		//===========================================================================================
		void SetPixel(Pixel^ pixel);
		//===========================================================================================
	protected private:
		//===========================================================================================
		property const Magick::PixelPacket* Pixels
		{
			virtual const Magick::PixelPacket* get() override sealed
			{
				return _Pixels;
			}
		}
		//===========================================================================================
	internal:
		//===========================================================================================
		WritablePixelCollection(Magick::Image* image, int x, int y, int width, int height);
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Changes the value of the specified pixel.
		///</summary>
		///<param name="pixel">The pixel to set.</param>
		void Set(Pixel^ pixel);
		///==========================================================================================
		///<summary>
		/// Changes the value of the specified pixels.
		///</summary>
		///<param name="pixels">The pixels to set.</param>
		void Set(IEnumerable<Pixel^>^ pixels);
		///==========================================================================================
		///<summary>
		/// Transfers the image cache pixels to the image. If the current instance is read-only an
		/// InvalidOperationException() will be thrown.
		///</summary>
		void Write();
		//===========================================================================================
	};
	//==============================================================================================
}