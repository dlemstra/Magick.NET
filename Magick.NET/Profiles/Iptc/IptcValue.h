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

#include "IptcTag.h"

namespace ImageMagick
{
	///=============================================================================================
	/// <summary>
	/// A value of the iptc profile.
	/// </summary>
	public ref class IptcValue sealed : IEquatable<IptcValue^>
	{
		//===========================================================================================
	private:
		//===========================================================================================
		IptcTag _Tag;
		array<Byte>^ _Data;
		//===========================================================================================
	internal:
		//===========================================================================================
		IptcValue(IptcTag tag, array<Byte>^ value);
		//===========================================================================================
	public:
		//===========================================================================================
		/// <summary>
		/// The tag of the iptc value.
		/// </summary>
		property IptcTag Tag
		{
			IptcTag get();
		}
		//===========================================================================================
		/// <summary>
		/// The value.
		/// </summary>
		property String^ Value
		{
			String^ get();
		}
		//===========================================================================================
		static bool operator == (IptcValue^ left, IptcValue^ right);
		//===========================================================================================
		static bool operator != (IptcValue^ left, IptcValue^ right);
		///==========================================================================================
		///<summary>
		/// Determines whether the specified object is equal to the current iptc value.
		///</summary>
		///<param name="obj">The object to compare this iptc value with.</param>
		virtual bool Equals(Object^ obj) override;
		///==========================================================================================
		///<summary>
		/// Determines whether the specified iptc value is equal to the current iptc value.
		///</summary>
		///<param name="other">The iptc value to compare this iptc value with.</param>
		virtual bool Equals(IptcValue^ other);
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
		//===========================================================================================
	};
	//==============================================================================================
}