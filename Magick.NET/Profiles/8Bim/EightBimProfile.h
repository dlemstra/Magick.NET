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

#include "..\ImageProfile.h"

using namespace System::Collections::Generic;
using namespace System::IO;
using namespace System::Xml;
using namespace System::Xml::XPath;

namespace ImageMagick
{
	///=============================================================================================
	/// <summary>
	/// Class that can be used to access an 8bim profile.
	/// </summary>
	public ref class EightBimProfile sealed : ImageProfile
	{
		//===========================================================================================
	private:
		//===========================================================================================
		List<IXPathNavigable^>^ _ClipPaths;
		int _Height;
		int _Width;
		//===========================================================================================
		XmlDocument^ CreateClipPath(int offset, int length);
		//===========================================================================================
		String^ GetClipPath(int offset, int length);
		//===========================================================================================
		void Initialize();
		//===========================================================================================
	internal:
		//===========================================================================================
		EightBimProfile() {};
		//===========================================================================================
		virtual void Initialize(MagickImage^ image) override;
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the EightBimProfile class.
		///</summary>
		///<param name="data">The byte array to read the 8bim profile from.</param>
		EightBimProfile(array<Byte>^ data) : ImageProfile("8bim", data) {};
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the EightBimProfile class.
		///</summary>
		///<param name="fileName">The fully qualified name of the 8bim profile file, or the relative
		/// 8bim profile file name.</param>
		EightBimProfile(String^ fileName) : ImageProfile("8bim", fileName) {};
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the EightBimProfile class.
		///</summary>
		///<param name="stream">The stream to read the 8bim profile from.</param>
		EightBimProfile(Stream^ stream) : ImageProfile("8bim", stream) {};
		///==========================================================================================
		///<summary>
		/// Returns the clipping paths this image contains.
		///</summary>
		property IEnumerable<IXPathNavigable^>^ ClippingPaths
		{
			IEnumerable<IXPathNavigable^>^ get();
		}
		//===========================================================================================
	};
	//==============================================================================================
}