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

#include "..\MagickImage.h"
#include "..\Settings\MagickReadSettings.h"

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Class that contains data for the Read event.
	///</summary>
	public ref class ScriptReadEventArgs sealed : EventArgs
	{
		//===========================================================================================
	private:
		//===========================================================================================
		String^ _Id;
		MagickReadSettings^ _Settings;
		//===========================================================================================
	internal:
		//===========================================================================================
		ScriptReadEventArgs(String^ id, MagickReadSettings^ settings);
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// The ID of the image.
		///</summary>
		property String^ Id
		{
			String^ get();
		}
		///==========================================================================================
		///<summary>
		/// The image that was read.
		///</summary>
		property MagickImage^ Image;
		///==========================================================================================
		///<summary>
		/// The read settings for the image.
		///</summary>
		property MagickReadSettings^ Settings
		{
			MagickReadSettings^ get();
		}
		//===========================================================================================
	};
	//==============================================================================================
}