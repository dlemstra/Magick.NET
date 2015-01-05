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

using namespace System::IO;

namespace ImageMagick
{
	//==============================================================================================
	ref class MagickImage;
	///=============================================================================================
	///<summary>
	/// Class that contains an image profile.
	///</summary>
	public ref class ImageProfile : IEquatable<ImageProfile^>
	{
		//===========================================================================================
	private:
		//===========================================================================================
		array<Byte>^ _Data;
		String^ _Name;
		//===========================================================================================
		static array<Byte>^ Copy(array<Byte>^ data);
		//===========================================================================================
	protected:
		//===========================================================================================
		property array<Byte>^ Data
		{
			array<Byte>^ get();
		}
		//===========================================================================================
	internal:
		//===========================================================================================
		ImageProfile() {};
		//===========================================================================================
		ImageProfile(String^ name);
		//===========================================================================================
		virtual array<Byte>^ GetData();
		//===========================================================================================
		void Initialize(String^ name, array<Byte>^ data);
		//===========================================================================================
		virtual void Initialize(MagickImage^ image);
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the ImageProfile class.
		///</summary>
		///<param name="name">The name of the profile.</param>
		///<param name="data">A byte array containing the profile.</param>
		ImageProfile(String^ name, array<Byte>^ data);
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the ImageProfile class.
		///</summary>
		///<param name="name">The name of the profile.</param>
		///<param name="stream">A stream containing the profile.</param>
		ImageProfile(String^ name, Stream^ stream);
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the ImageProfile class.
		///</summary>
		///<param name="name">The name of the profile.</param>
		///<param name="fileName">The fully qualified name of the profile file, or the relative profile file name.</param>
		ImageProfile(String^ name, String^ fileName);
		///==========================================================================================
		///<summary>
		/// The name of the profile.
		///</summary>
		property String^ Name
		{
			String^ get();
		}
		//===========================================================================================
		static bool operator == (ImageProfile^ left, ImageProfile^ right);
		//===========================================================================================
		static bool operator != (ImageProfile^ left, ImageProfile^ right);
		///==========================================================================================
		///<summary>
		/// Determines whether the specified object is equal to the current image profile.
		///</summary>
		///<param name="obj">The object to compare this image profile with.</param>
		virtual bool Equals(Object^ obj) override;
		///==========================================================================================
		///<summary>
		/// Determines whether the specified image is equal to the current image profile.
		///</summary>
		///<param name="other">The image profile to compare this image profile with.</param>
		virtual bool Equals(ImageProfile^ other);
		///==========================================================================================
		///<summary>
		/// Servers as a hash of this type.
		///</summary>
		virtual int GetHashCode() override;
		///==========================================================================================
		///<summary>
		/// Converts this instance to a byte array.
		///</summary>
		array<Byte>^ ToByteArray();
		//===========================================================================================
	};
	//==============================================================================================
}