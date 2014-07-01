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

#include "ExifDataType.h"
#include "ExifTag.h"

namespace ImageMagick
{
	///=============================================================================================
	/// <summary>
	/// A value of the exif profile.
	/// </summary>
	public ref class ExifValue sealed : IEquatable<ExifValue^>
	{
		//===========================================================================================
	private:
		//===========================================================================================
		ExifDataType _DataType;
		bool _IsArray;
		ExifTag _Tag;
		Object^ _Value;
		//===========================================================================================
		void CheckValue(Object^ value);
		//===========================================================================================
		void Initialize(ExifTag tag, ExifDataType dataType, bool isArray);
		//===========================================================================================
		String^ ToString(Object^ value);
		//===========================================================================================
	internal:
		//===========================================================================================
		property bool HasValue
		{
			bool get();
		}
		//===========================================================================================
		property int Length
		{
			int get();
		}
		//===========================================================================================
		property int NumberOfComponents
		{
			int get();
		}
		//===========================================================================================
		ExifValue(ExifTag tag, ExifDataType dataType, bool isArray);
		//===========================================================================================
		ExifValue(ExifTag tag, ExifDataType dataType, Object^ value, bool isArray);
		//===========================================================================================
		static ExifValue^ Create(ExifTag tag, Object^ value);
		//===========================================================================================
		static unsigned int GetSize(ExifDataType dataType);
		//===========================================================================================
	public:
		//===========================================================================================
		/// <summary>
		/// The data type of the exif value.
		/// </summary>
		property ExifDataType DataType
		{
			ExifDataType get();
		}
		//===========================================================================================
		/// <summary>
		/// Returns true if the value is an array.
		/// </summary>
		property bool IsArray
		{
			bool get();
		}
		//===========================================================================================
		/// <summary>
		/// The tag of the exif value.
		/// </summary>
		property ExifTag Tag
		{
			ExifTag get();
		}
		//===========================================================================================
		/// <summary>
		/// The value.
		/// </summary>
		property Object^ Value
		{
			Object^ get();
			void set(Object^ value);
		}
		//===========================================================================================
		static bool operator == (ExifValue^ left, ExifValue^ right);
		//===========================================================================================
		static bool operator != (ExifValue^ left, ExifValue^ right);
		///==========================================================================================
		///<summary>
		/// Determines whether the specified object is equal to the current exif value.
		///</summary>
		///<param name="obj">The object to compare this exif value with.</param>
		virtual bool Equals(Object^ obj) override;
		///==========================================================================================
		///<summary>
		/// Determines whether the specified exif value is equal to the current exif value.
		///</summary>
		///<param name="other">The exif value to compare this exif value with.</param>
		virtual bool Equals(ExifValue^ other);
		///==========================================================================================
		///<summary>
		/// Servers as a hash of this type.
		///</summary>
		virtual int GetHashCode() override;
		///==========================================================================================
		///<summary>
		/// Returns a string that represents the current value.
		///</summary>
		virtual String^ ToString() override;
		//===========================================================================================
	};
	//==============================================================================================
}