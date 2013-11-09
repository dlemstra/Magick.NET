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
#include "Stdafx.h"
#include "EightBimProfile.h"
#include "..\..\MagickImage.h"
#include "..\..\Helpers\XmlHelper.h"
#include "..\..\Helpers\ByteConverter.h"

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
		String^ test =  GetClipPath(offset, length);
		XmlHelper::SetAttribute(path, "d",test);

		return clipPath;
	}
	//==============================================================================================
	String^ EightBimProfile::GetClipPath(int offset, int length)
	{
		array<Point>^ last = gcnew array<Point>(3);
		array<Point>^ first = gcnew array<Point>(3);
		array<Point>^ point = gcnew array<Point>(3);

		StringBuilder^ path = gcnew StringBuilder();

		int i = offset;
		int knotCount = 0;
		bool inSubpath = false;
		while (i < offset + length)
		{
			short selector = ByteConverter::ToShort(Data, i);
			switch (selector)
			{
			case 0:
			case 3:
				if (knotCount != 0)
				{
					i+=24;
					break;
				}
				knotCount = ByteConverter::ToShort(Data, i);
				i+=22;
				break;
			case 1:
			case 2:
			case 4:
			case 5:
				if (knotCount == 0)
				{
					i+=24;
					break;
				}

				for (int k=0; k < 3; k++)
				{
					int yy = ByteConverter::ToInt(Data, i);
					long y = (long) yy;
					if (yy > 2147483647)
						y = yy-4294967295U-1;

					int xx = ByteConverter::ToInt(Data, i);
					long x = (long) xx;
					if (xx > 2147483647)
						x = (long)xx-4294967295U-1;

					point[k].X = (int)(((double)x*_Width/4096/4096) + 0.5);
					point[k].Y = (int)(((double)y*_Height/4096/4096) + 0.5);
				}

				if (inSubpath == false)
				{
					path->AppendFormat(CultureInfo::InvariantCulture, "M {0},{1}\n", point[1].X, point[1].Y);

					for (int k=0; k < 3; k++)
					{
						first[k]=point[k];
						last[k]=point[k];
					}
				}
				else
				{
					if ((last[1].X == last[2].X) && (last[1].Y == last[2].Y) &&
						(point[0].X == point[1].X) && (point[0].Y == point[1].Y))
						path->AppendFormat(CultureInfo::InvariantCulture, "L {0},{1}\n", point[1].X, point[1].Y);
					else
						path->AppendFormat(CultureInfo::InvariantCulture, "C {0},{1} {2},{3} {4},{5}\n", last[2].X,last[2].Y,point[0].X,point[0].Y,point[1].X,point[1].Y);

					for (int k=0; k < 3; k++)
					{
						last[k]=point[k];
					}
				}

				inSubpath = true;
				knotCount--;
				if (knotCount == 0)
				{
					if ((last[1].X == last[2].X) && (last[1].Y == last[2].Y) &&
						(first[0].X == first[1].X) && (first[0].Y == first[1].Y))
						path->AppendFormat(CultureInfo::InvariantCulture, "L {0},{1} Z\n", first[1].X, first[1].Y);
					else
						path->AppendFormat(CultureInfo::InvariantCulture, "C {0},{1} {2},{3} {4},{5} Z\n",last[2].X,last[2].Y,first[0].X,first[0].Y,first[1].X,first[1].Y);
					inSubpath=false;
				}

				break;
			case 6:
			case 7:
			case 8:
			default:
				i+=24;
				break;
			}
		}

		return path->ToString();
	}
	//==============================================================================================
	void EightBimProfile::Initialize()
	{
		if (_ClipPaths != nullptr)
			return;

		_ClipPaths = gcnew List<IXPathNavigable^>();

		int i = 0;
		while(i + 11 < Data->Length)
		{
			if (Data[i++] != '8')
				continue;
			if (Data[i++] != 'B')
				continue;
			if (Data[i++] != 'I')
				continue;
			if (Data[i++] != 'M')
				continue;

			short id = ByteConverter::ToShort(Data, i);

			int length = (int)Data[i++];
			if (length != 0)
				i += length;

			if ((length & 0x01) == 0)
				i++;

			length = ByteConverter::ToInt(Data, i);

			if (id > 1999 && id < 2998)
				_ClipPaths->Add(CreateClipPath(i, length));

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
}