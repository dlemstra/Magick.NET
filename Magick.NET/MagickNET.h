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

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Class that can be used to initialize Magick.NET.
	///</summary>
	public ref class MagickNET abstract sealed
	{
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Adds the sub directory ImageMagick of the current execution path to the environment path.
		/// You should place the supplied ImageMagick dlls in that directory.
		///</summary>
		static bool Initialize();
		///==========================================================================================
		///<summary>
		/// Adds the specified path to the environment path. You should place the ImageMagick dlls
		/// in that directory.
		///</summary>
		static bool Initialize(String^ path);
		///==========================================================================================
		///<summary>
		/// Pixel cache threshold in megabytes. Once this memory threshold is exceeded, all subsequent
		/// pixels cache operations are to/from disk. This setting is shared by all Image objects.
		///</summary>
		static void SetCacheThreshold(int threshold);
		//===========================================================================================
	};
	//==============================================================================================
}