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

using namespace System::IO;

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Class that contains a color profile.
	///</summary>
	public ref class ColorProfile sealed 
	{
		//===========================================================================================
	private:
		//===========================================================================================
		array<Byte>^ _Data;
		String^ _Name;
		//===========================================================================================
		void Initialize(String^ name, Stream^ stream);
		//===========================================================================================
	internal:
		//===========================================================================================
		ColorProfile(String^ name, Stream^ stream);
		//===========================================================================================
		property array<Byte>^ Data
		{
			array<Byte>^ get()
			{
				return _Data;
			}
		}
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the ColorProfile class.
		///</summary>
		///<param name="name">The name of the profile (e.g. "ICM", "IPTC", or a generic profile name).</param>
		///<param name="fileName">The fully qualified name of the profile file, or the relative profile file name.</param>
		ColorProfile(String^ name, String^ fileName);
		///==========================================================================================
		///<summary>
		/// The name of the profile.
		///</summary>
		property String^ Name
		{
			String^ get()
			{
				return _Name;
			}
		}
		//===========================================================================================
	};
	//==============================================================================================
}