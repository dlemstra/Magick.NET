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

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Class that contains data for the Write event.
	///</summary>
	public ref class ScriptWriteEventArgs sealed : EventArgs
	{
		//===========================================================================================
	private:
		//===========================================================================================
		String^ _Id;
		MagickImage^ _Image;
		//===========================================================================================
	internal:
		//===========================================================================================
		ScriptWriteEventArgs(String^ id, MagickImage^ image);
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
		/// The image that needs to be written.
		///</summary>
		property MagickImage^ Image
		{
			MagickImage^ get();
		}
		//===========================================================================================
	};
	//==============================================================================================
}