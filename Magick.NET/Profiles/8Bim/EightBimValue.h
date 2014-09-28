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

namespace ImageMagick
{
	///=============================================================================================
	/// <summary>
	/// A value of the 8bim profile.
	/// </summary>
	public ref class EightBimValue sealed : IEquatable<EightBimValue^>
	{
		//===========================================================================================
	private:
		//===========================================================================================
		short _ID;
		array<Byte>^ _Data;
		//===========================================================================================
	internal:
		//===========================================================================================
		EightBimValue(short ID, array<Byte>^ data);
		//===========================================================================================
	public:
		//===========================================================================================
		/// <summary>
		/// The ID of the 8bim value
		/// </summary>
		property short ID
		{
			short get();
		}
		//===========================================================================================
		static bool operator == (EightBimValue^ left, EightBimValue^ right);
		//===========================================================================================
		static bool operator != (EightBimValue^ left, EightBimValue^ right);
		///==========================================================================================
		///<summary>
		/// Determines whether the specified object is equal to the current 8bim value.
		///</summary>
		///<param name="obj">The object to compare this 8bim value with.</param>
		virtual bool Equals(Object^ obj) override;
		///==========================================================================================
		///<summary>
		/// Determines whether the specified exif value is equal to the current 8bim value.
		///</summary>
		///<param name="other">The exif value to compare this 8bim value with.</param>
		virtual bool Equals(EightBimValue^ other);
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
		///==========================================================================================
		///<summary>
		/// Returns a string that represents the current value.
		///</summary>
		virtual String^ ToString() override;
		///==========================================================================================
		///<summary>
		/// Returns a string that represents the current value with the specified encoding.
		///</summary>
		String^ ToString(System::Text::Encoding^ encoding);
		//===========================================================================================
	};
	//==============================================================================================
}