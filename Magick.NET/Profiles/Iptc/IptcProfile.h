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

#include "IptcValue.h"
#include "..\ImageProfile.h"

using namespace System::Collections::Generic;
using namespace System::IO;

namespace ImageMagick
{
	///=============================================================================================
	/// <summary>
	/// Class that can be used to access an Iptc profile.
	/// </summary>
	public ref class IptcProfile sealed : ImageProfile
	{
		//===========================================================================================
	private:
		//===========================================================================================
		List<IptcValue^>^ _Values;
		//===========================================================================================
		void Initialize();
		//===========================================================================================
	internal:
		//===========================================================================================
		virtual array<Byte>^ GetData() override;
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the IptcProfile class.
		///</summary>
		IptcProfile() : ImageProfile("iptc") {};
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the IptcProfile class.
		///</summary>
		///<param name="data">The byte array to read the iptc profile from.</param>
		IptcProfile(array<Byte>^ data) : ImageProfile("iptc", data) {};
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the IptcProfile class.
		///</summary>
		///<param name="fileName">The fully qualified name of the iptc profile file, or the relative
		/// iptc profile file name.</param>
		IptcProfile(String^ fileName) : ImageProfile("iptc", fileName) {};
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the IptcProfile class.
		///</summary>
		///<param name="stream">The stream to read the iptc profile from.</param>
		IptcProfile(Stream^ stream) : ImageProfile("iptc", stream) {};
		///==========================================================================================
		///<summary>
		/// Returns the values of this iptc profile.
		///</summary>
		property IEnumerable<IptcValue^>^ Values
		{
			IEnumerable<IptcValue^>^ get();
		}
		///==========================================================================================
		///<summary>
		/// Changes the encoding for all the values,
		///</summary>
		///<param name="encoding">The encoding to use when storing the bytes.</param>
		void SetEncoding(Encoding^ encoding);
		///==========================================================================================
		///<summary>
		/// Sets the value of the specified tag.
		///</summary>
		///<param name="tag">The tag of the iptc value.</param>
		///<param name="encoding">The encoding to use when storing the bytes.</param>
		///<param name="value">The value.</param>
		void SetValue(IptcTag tag, Encoding^ encoding, String^ value);
		///==========================================================================================
		///<summary>
		/// Sets the value of the specified tag.
		///</summary>
		///<param name="tag">The tag of the iptc value.</param>
		///<param name="value">The value.</param>
		void SetValue(IptcTag tag, String^ value);
		//===========================================================================================
	};
	//==============================================================================================
}