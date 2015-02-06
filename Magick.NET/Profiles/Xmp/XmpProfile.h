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

#include "..\ImageProfile.h"

using namespace System::IO;
using namespace System::Xml;
using namespace System::Xml::XPath;

#if !(NET20)
using namespace System::Xml::Linq;
#endif

namespace ImageMagick
{
	///=============================================================================================
	///<summary>
	/// Class that contains an XMP profile.
	///</summary>
	public ref class XmpProfile sealed : ImageProfile 
	{
		//===========================================================================================
	internal:
		//===========================================================================================
		XmpProfile() {};
		//===========================================================================================
	public:
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the XmpProfile class.
		///</summary>
		///<param name="data">A byte array containing the profile.</param>
		XmpProfile(array<Byte>^ data) : ImageProfile("xmp", data) {};
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the XmpProfile class.
		///</summary>
		///<param name="document">A document containing the profile.</param>
		XmpProfile(IXPathNavigable^ document);
#if !(NET20)
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the XmpProfile class.
		///</summary>
		///<param name="document">A document containing the profile.</param>
		XmpProfile(XDocument^ document);
#endif
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the XmpProfile class.
		///</summary>
		///<param name="stream">A stream containing the profile.</param>
		XmpProfile(Stream^ stream) : ImageProfile("xmp", stream) {};
		///==========================================================================================
		///<summary>
		/// Initializes a new instance of the XmpProfile class.
		///</summary>
		///<param name="fileName">The fully qualified name of the profile file, or the relative profile file name.</param>
		XmpProfile(String^ fileName) : ImageProfile("xmp", fileName) {};
		///==========================================================================================
		///<summary>
		/// Creates a XmlReader that can be used to read the data of the profile.
		///</summary>
		XmlReader^ CreateReader();
		///==========================================================================================
		///<summary>
		/// Creates an instance from the specified IXPathNavigable.
		///</summary>
		///<param name="document">A document containing the profile.</param>
		static XmpProfile^ FromIXPathNavigable(IXPathNavigable^ document);
#if !(NET20)
		///==========================================================================================
		///<summary>
		/// Creates an instance from the specified IXPathNavigable.
		///</summary>
		///<param name="document">A document containing the profile.</param>
		static XmpProfile^ FromXDocument(XDocument^ document);
#endif
		///==========================================================================================
		///<summary>
		/// Converts this instance to an IXPathNavigable.
		///</summary>
		IXPathNavigable^ ToIXPathNavigable();
#if !(NET20)
		///==========================================================================================
		///<summary>
		/// Converts this instance to a XDocument.
		///</summary>
		XDocument^ ToXDocument();
#endif
		//===========================================================================================
	};
	//==============================================================================================
}