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
#include "Stdafx.h"
#include "ClipPathReader.h"
#include "EightBimProfile.h"
#include "..\..\MagickImage.h"
#include "..\..\Helpers\ByteConverter.h"
#include "..\..\Helpers\XmlHelper.h"

using namespace System::Globalization;

namespace ImageMagick
{
	//==============================================================================================
	XmlDocument^ EightBimProfile::CreateClipPath(int offset, int length)
	{
		XmlDocument^ clipPath = gcnew XmlDocument();
		clipPath->CreateXmlDeclaration("1.0", "iso-8859-1", nullptr);

		XmlElement^ svg = XmlHelper::CreateElement(clipPath, "svg");
		XmlHelper::SetAttribute(svg, "width", _Width);
		XmlHelper::SetAttribute(svg, "height", _Height);

		XmlElement^ g = XmlHelper::CreateElement(svg, "g");

		XmlElement^ path = XmlHelper::CreateElement(g, "path");
		XmlHelper::SetAttribute(path, "fill", "#00000000");
		XmlHelper::SetAttribute(path, "stroke", "#00000000");
		XmlHelper::SetAttribute(path, "stroke-width", "0");
		XmlHelper::SetAttribute(path, "stroke-antialiasing", "false");
		String^ d = GetClipPath(offset, length);
		XmlHelper::SetAttribute(path, "d", d);

		return clipPath;
	}
	//==============================================================================================
	String^ EightBimProfile::GetClipPath(int offset, int length)
	{
		ClipPathReader^ reader = gcnew ClipPathReader(_Width, _Height);
		return reader->Read(Data, offset, length);
	}
	//==============================================================================================
	void EightBimProfile::Initialize()
	{
		if (_ClipPaths != nullptr)
			return;

		_ClipPaths = gcnew List<IXPathNavigable^>();
		_Values = gcnew List<EightBimValue^>();

		int i = 0;
		while (i < Data->Length)
		{
			if (Data[i++] != '8')
				continue;
			if (Data[i++] != 'B')
				continue;
			if (Data[i++] != 'I')
				continue;
			if (Data[i++] != 'M')
				continue;

			if (i + 7 > Data->Length)
				return;

			short id = ByteConverter::ToShort(Data, i);

			int length = (int)Data[i++];
			if (length != 0)
				i += length;

			if ((length & 0x01) == 0)
				i++;

			length = ByteConverter::ToInt(Data, i);
			if (i + length > Data->Length)
				return;

			if (length != 0)
			{
				if (id > 1999 && id < 2998)
					_ClipPaths->Add(CreateClipPath(i, length));

				array<Byte>^ data = gcnew array<Byte>(length);
				Array::Copy(Data, i, data, 0, length);
				_Values->Add(gcnew EightBimValue(id, data));
			}

			i += length;
		}
	}
	//==============================================================================================
	void EightBimProfile::Initialize(MagickImage^ image)
	{
		_Width = image->Width;
		_Height = image->Height;
	}
	//==============================================================================================
	IEnumerable<IXPathNavigable^>^ EightBimProfile::ClippingPaths::get()
	{
		Initialize();

		return _ClipPaths;
	}
	//==============================================================================================
	IEnumerable<EightBimValue^>^ EightBimProfile::Values::get()
	{
		Initialize();

		return _Values;
	}
	//==============================================================================================
}