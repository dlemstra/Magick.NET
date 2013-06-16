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

#include "..\ImageProfile.h"

using namespace System::IO;
using namespace System::Collections::Generic;

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Class that contains an ICM/ICC color profile.
	///</summary>
	public ref class ColorProfile sealed : ImageProfile 
	{
		//===========================================================================================
	private:
		//===========================================================================================
		static initonly Object^ _SyncRoot = gcnew Object();
		static Dictionary<String^, ColorProfile^>^ _Profiles = gcnew Dictionary<String^, ColorProfile^>();
		//===========================================================================================
		static ColorProfile^ Load(String^ resourceName);
		//===========================================================================================
	internal:
		//===========================================================================================
		ColorProfile() {};
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the ColorProfile class.
		///</summary>
		///<param name="data">A byte array containing the profile.</param>
		ColorProfile(array<Byte>^ data) : ImageProfile("icm", data) {};
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the ColorProfile class.
		///</summary>
		///<param name="stream">A stream containing the profile.</param>
		ColorProfile(Stream^ stream) : ImageProfile("icm", stream) {};
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the ColorProfile class.
		///</summary>
		///<param name="fileName">The fully qualified name of the profile file, or the relative profile file name.</param>
		ColorProfile(String^ fileName) : ImageProfile("icm", fileName) {};
		///==========================================================================================
		///<summary>
		/// The sRGB icm profile.
		///</summary>
		static property ColorProfile^ SRGB
		{
			ColorProfile^ get();
		}
		//===========================================================================================
	};
	//==============================================================================================
}