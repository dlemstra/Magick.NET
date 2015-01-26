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
#include "Stdafx.h"
#include "XmpProfile.h"

using namespace System::Reflection;
using namespace System::Threading;

namespace ImageMagick
{
	//==============================================================================================
	IXPathNavigable^ XmpProfile::ToIXPathNavigable()
	{
		XmlReader^ reader = CreateReader();

		try
		{
			XmlDocument^ result = gcnew XmlDocument();
			result->Load(reader);
			return result;
		}
		finally
		{
			delete reader;
		}
	}
	//==============================================================================================
#if !(NET20)
	XDocument^ XmpProfile::ToXDocument()
	{
		XmlReader^ reader = CreateReader();

		try
		{
			return XDocument::Load(reader);
		}
		finally
		{
			delete reader;
		}
	}
#endif
	//==============================================================================================
	XmlReader^ XmpProfile::CreateReader()
	{
		MemoryStream^ memStream = gcnew MemoryStream(Data, 0, Data->Length);
		XmlReaderSettings^ settings = gcnew XmlReaderSettings();
		settings->CloseInput = true;
		return XmlReader::Create(memStream, settings);
	}
	//==============================================================================================
}