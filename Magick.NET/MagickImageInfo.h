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

#include "Enums\ColorSpace.h"
#include "Enums\MagickFormat.h"
#include "Helpers\EnumHelper.h"
#include "Helpers\MagickReader.h"

using namespace System::IO;

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Class that contains basic information about an image.
	///</summary>
	public ref class MagickImageInfo sealed : IEquatable<MagickImageInfo^>, IComparable<MagickImageInfo^>
	{
		//===========================================================================================
	private:
		//===========================================================================================
		ColorSpace _ColorSpace;
		String^ _FileName;
		MagickFormat _Format;
		int _Height;
		int _Width;
		//===========================================================================================
		static MagickReadSettings^ CreateReadSettings();
		//===========================================================================================
		void Initialize(Magick::Image* image);
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the MagickImageInfo class.
		///</summary>
		MagickImageInfo() {};
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the MagickImageInfo class using the specified blob.
		///</summary>
		///<param name="data">The byte array to read the information from.</param>
		///<exception cref="MagickException"/>
		MagickImageInfo(array<Byte>^ data);
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the MagickImageInfo class using the specified filename.
		///</summary>
		///<param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
		///<exception cref="MagickException"/>
		MagickImageInfo(String^ fileName);
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the MagickImageInfo class using the specified stream.
		///</summary>
		///<param name="stream">The stream to read the image data from.</param>
		///<exception cref="MagickException"/>
		MagickImageInfo(Stream^ stream);
		///==========================================================================================
		///<summary>
		/// Color space of the image.
		///</summary>
		property ColorSpace ColorSpace
		{
			ImageMagick::ColorSpace get()
			{
				return _ColorSpace;
			}
		}
		///==========================================================================================
		///<summary>
		/// Original file name of the image (only available if read from disk).
		///</summary>
		property String^ FileName
		{
			String^ get()
			{
				return _FileName;
			}
		}
		///==========================================================================================
		///<summary>
		/// The format of the image.
		///</summary>
		property MagickFormat Format
		{
			MagickFormat get()
			{
				return _Format;
			}
		}
		///==========================================================================================
		///<summary>
		/// Height of the image.
		///</summary>
		property int Height
		{
			int get()
			{
				return _Height;
			}
		}
		///==========================================================================================
		///<summary>
		/// Height of the image.
		///</summary>
		property int Width
		{
			int get()
			{
				return _Width;
			}
		}
		//===========================================================================================
		static bool operator == (MagickImageInfo^ left, MagickImageInfo^ right)
		{
			return Object::Equals(left, right);
		}
		//===========================================================================================
		static bool operator != (MagickImageInfo^ left, MagickImageInfo^ right)
		{
			return !Object::Equals(left, right);
		}
		//===========================================================================================
		static bool operator > (MagickImageInfo^ left, MagickImageInfo^ right)
		{
			if (ReferenceEquals(left, nullptr))
				return ReferenceEquals(right, nullptr);

			return left->CompareTo(right) == 1;
		}
		//===========================================================================================
		static bool operator < (MagickImageInfo^ left, MagickImageInfo^ right)
		{
			if (ReferenceEquals(left, nullptr))
				return !ReferenceEquals(right, nullptr);

			return left->CompareTo(right) == -1;
		}
		//===========================================================================================
		static bool operator >= (MagickImageInfo^ left, MagickImageInfo^ right)
		{
			if (ReferenceEquals(left, nullptr))
				return ReferenceEquals(right, nullptr);

			return left->CompareTo(right) >= 0;
		}
		//===========================================================================================
		static bool operator <= (MagickImageInfo^ left, MagickImageInfo^ right)
		{
			if (ReferenceEquals(left, nullptr))
				return !ReferenceEquals(right, nullptr);

			return left->CompareTo(right) <= 0;
		}
		///==========================================================================================
		///<summary>
		/// Compares the current instance with another object of the same type.
		///</summary>
		///<param name="other">The object to compare this image information with.</param>
		virtual int CompareTo(MagickImageInfo^ other);
		///==========================================================================================
		///<summary>
		/// Determines whether the specified object is equal to the current image information.
		///</summary>
		///<param name="obj">The object to compare this image information with.</param>
		virtual bool Equals(Object^ obj) override;
		///==========================================================================================
		///<summary>
		/// Determines whether the specified geometry is equal to the current image information.
		///</summary>
		///<param name="other">The image to compare this image information with.</param>
		virtual bool Equals(MagickImageInfo^ other);
		///==========================================================================================
		///<summary>
		/// Servers as a hash of this type.
		///</summary>
		virtual int GetHashCode() override;
		///==========================================================================================
		///<summary>
		/// Read basic information about an image.
		///</summary>
		///<param name="data">The blob to read the information from.</param>
		///<exception cref="MagickException"/>
		void Read(array<Byte>^ data);
		///==========================================================================================
		///<summary>
		/// Read basic information about an image.
		///</summary>
		///<param name="fileName">The fully qualified name of the image file, or the relative image file name.</param>
		///<exception cref="MagickException"/>
		void Read(String^ fileName);
		///==========================================================================================
		///<summary>
		/// Read basic information about an image.
		///</summary>
		///<param name="stream">The stream to read the image data from.</param>
		///<exception cref="MagickException"/>
		void Read(Stream^ stream);
		//===========================================================================================
	};
	//==============================================================================================
}