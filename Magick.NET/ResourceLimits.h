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

#include "Stdafx.h"

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Class that can be used to set the limits to the resources that are being used.
	///</summary>
	public ref class ResourceLimits abstract sealed
	{
	public:
		///==========================================================================================
		///<summary>
		/// Pixel cache limit in bytes. Requests for memory above this limit will fail.
		///</summary>
		[CLSCompliantAttribute(false)]
		static property Magick::MagickSizeType Disk
		{
			Magick::MagickSizeType get();
			void set(Magick::MagickSizeType limit);
		}
		///==========================================================================================
		///<summary>
		/// The maximum width of an image.
		///</summary>
		[CLSCompliantAttribute(false)]
		static property Magick::MagickSizeType Height
		{
			Magick::MagickSizeType get();
			void set(Magick::MagickSizeType limit);
		}
		///==========================================================================================
		///<summary>
		/// Pixel cache limit in bytes. Once this memory limit is exceeded, all subsequent pixels cache
		/// operations are to/from disk.
		///</summary>
		[CLSCompliantAttribute(false)]
		static property Magick::MagickSizeType Memory
		{
			Magick::MagickSizeType get();
			void set(Magick::MagickSizeType limit);
		}
		///==========================================================================================
		///<summary>
		/// Limits the number of threads used in multithreaded operations.
		///</summary>
		[CLSCompliantAttribute(false)]
		static property Magick::MagickSizeType Thread
		{
			Magick::MagickSizeType get();
			void set(Magick::MagickSizeType limit);
		}
		///==========================================================================================
		///<summary>
		/// Periodically yield the CPU for at least the time specified in milliseconds.
		///</summary>
		[CLSCompliantAttribute(false)]
		static property Magick::MagickSizeType Throttle
		{
			Magick::MagickSizeType get();
			void set(Magick::MagickSizeType limit);
		}
		///==========================================================================================
		///<summary>
		/// The maximum width of an image.
		///</summary>
		[CLSCompliantAttribute(false)]
		static property Magick::MagickSizeType Width
		{
			Magick::MagickSizeType get();
			void set(Magick::MagickSizeType limit);
		}
		//===========================================================================================
	};
	//==============================================================================================
}