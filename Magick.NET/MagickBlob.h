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

#include "Helpers\MagickException.h"
#include "Helpers\MagickWrapper.h"
#include "Helpers\MagickWriter.h"

using namespace System::IO;

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Encapsulation of the ImageMagick Blob object.
	///</summary>
	public ref class MagickBlob sealed : MagickWrapper<Magick::Blob>
	{
		//===========================================================================================
	private:
		//===========================================================================================
		void Initialize(Stream^ stream);
		//===========================================================================================
		MagickBlob()
		{
		}
		//===========================================================================================
	internal:
		//===========================================================================================
		MagickBlob(Magick::Blob& blob);
		//===========================================================================================
		static operator Magick::Blob& (MagickBlob^ blob)
		{
			Throw::IfNull("blob", blob);

			return *(blob->Value);
		}
		//===========================================================================================
		static explicit operator Magick::Blob* (MagickBlob^ blob)
		{
			if (blob == nullptr)
				return NULL;

			return blob->Value;
		}
		//===========================================================================================
		static MagickBlob^ Create();
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the MagickBlob class using the specified image data.
		///</summary>
		///<param name="data">The image data.</param>
		MagickBlob(array<Byte>^ data);
		///==========================================================================================
		///<summary>
		/// Returns the length of the blob.
		///</summary>
		property int Length
		{
			int get()
			{
				return Value->length();
			}
		}
		///==========================================================================================
		///<summary>
		/// Reads a blob from the specified fileName.
		///</summary>
		///<param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
		static MagickBlob^ Read(String^ fileName);
		///==========================================================================================
		///<summary>
		/// Reads a blob from the specified stream.
		///</summary>
		///<param name="stream">The stream to read the image data from.</param>
		static MagickBlob^ Read(Stream^ stream);
		///==========================================================================================
		///<summary>
		/// Writes the blob to the specified file name.
		///</summary>
		///<param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
		///<exception cref="MagickException"/>
		void Write(String^ fileName);
		///==========================================================================================
		///<summary>
		/// Writes the blob to the specified file name.
		///</summary>
		///<param name="stream">The stream to write the image data to.</param>
		///<exception cref="MagickException"/>
		void Write(Stream^ stream);
		//===========================================================================================
	};
	//==============================================================================================
}