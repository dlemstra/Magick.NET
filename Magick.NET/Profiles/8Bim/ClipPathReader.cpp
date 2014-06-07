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
#include "..\..\Helpers\ByteConverter.h"

using namespace System::Globalization;

namespace ImageMagick
{
	//==============================================================================================
	void ClipPathReader::AddPath(array<Byte>^ data)
	{
		if (_KnotCount == 0)
		{
			_Index += 24;
			return;
		}

		array<PointD>^ point = CreatePoint(data);

		if (_InSubpath == false)
		{
			_Path->AppendFormat(CultureInfo::InvariantCulture, "M {0:0.###} {1:0.###}\n", point[1].X, point[1].Y);

			for (int k=0; k < 3; k++)
			{
				_First[k]=point[k];
				_Last[k]=point[k];
			}
		}
		else
		{
			if ((_Last[1].X == _Last[2].X) && (_Last[1].Y == _Last[2].Y) && (point[0].X == point[1].X) && (point[0].Y == point[1].Y))
				_Path->AppendFormat(CultureInfo::InvariantCulture, "L {0:0.###} {1:0.###}\n", point[1].X, point[1].Y);
			else if ((_Last[1].X == _Last[2].X) && (_Last[1].Y == _Last[2].Y))
				_Path->AppendFormat(CultureInfo::InvariantCulture, "Q {0:0.###} {1:0.###} {2:0.###} {3:0.###}\n", point[0].X, point[0].Y, point[1].X, point[1].Y);
			else if ((point[0].X == point[1].X) && (point[0].Y == point[1].Y))
				_Path->AppendFormat(CultureInfo::InvariantCulture, "S {0:0.###} {1:0.###} {2:0.###} {3:0.###}\n", _Last[2].X, _Last[2].Y, point[1].X, point[1].Y);
			else
				_Path->AppendFormat(CultureInfo::InvariantCulture, "C {0:0.###} {1:0.###} {2:0.###} {3:0.###} {4:0.###} {5:0.###}\n", _Last[2].X, _Last[2].Y, point[0].X, point[0].Y, point[1].X, point[1].Y);

			for (int k=0; k < 3; k++)
				_Last[k]=point[k];
		}

		_InSubpath = true;
		_KnotCount--;
		if (_KnotCount == 0)
		{
			ClosePath();
			_InSubpath=false;
		}
	}
	//==============================================================================================
	void ClipPathReader::ClosePath()
	{
		if ((_Last[1].X == _Last[2].X) && (_Last[1].Y == _Last[2].Y) && (_First[0].X == _First[1].X) && (_First[0].Y == _First[1].Y))
			_Path->AppendFormat(CultureInfo::InvariantCulture, "L {0:0.###} {1:0.###} Z\n", _First[1].X, _First[1].Y);
		else if ((_Last[1].X == _Last[2].X) && (_Last[1].Y == _Last[2].Y))
			_Path->AppendFormat(CultureInfo::InvariantCulture, "Q {0:0.###} {1:0.###} {2:0.###} {3:0.###} Z\n", _First[0].X, _First[0].Y, _First[1].X, _First[1].Y);
		else if ((_First[0].X == _First[1].X) && (_First[0].Y == _First[1].Y))
			_Path->AppendFormat(CultureInfo::InvariantCulture, "S {0:0.###} {1:0.###} {2:0.###} {3:0.###} Z\n", _Last[2].X, _Last[2].Y, _First[1].X, _First[1].Y);
		else
			_Path->AppendFormat(CultureInfo::InvariantCulture, "C {0:0.###} {1:0.###} {2:0.###} {3:0.###} {4:0.###} {5:0.###} Z\n", _Last[2].X, _Last[2].Y, _First[0].X, _First[0].Y, _First[1].X, _First[1].Y);
	}
	//==============================================================================================
	array<PointD>^ ClipPathReader::CreatePoint(array<Byte>^ data)
	{
		array<PointD>^ result = gcnew array<PointD>(3);

		for (int i=0; i < 3; i++)
		{
			int yy = ByteConverter::ToInt(data, _Index);
			long y = (long) yy;
			if (yy > 2147483647)
				y = yy-4294967295U-1;

			int xx = ByteConverter::ToInt(data, _Index);
			long x = (long) xx;
			if (xx > 2147483647)
				x = (long)xx-4294967295U-1;

			result[i].X = ((double)x*_Width/4096/4096);
			result[i].Y = ((double)y*_Height/4096/4096);
		}

		return result;
	}
	//==============================================================================================
	void ClipPathReader::Reset(int offset)
	{
		_Index = offset;
		_KnotCount = 0;
		_InSubpath = false;
		_Path = gcnew StringBuilder();
		_First = gcnew array<PointD>(3);
		_Last = gcnew array<PointD>(3);
	}
	//==============================================================================================
	void ClipPathReader::SetKnotCount(array<Byte>^ data)
	{
		if (_KnotCount != 0)
		{
			_Index += 24;
			return;
		}

		_KnotCount = ByteConverter::ToShort(data, _Index);
		_Index += 22;
	}
	//==============================================================================================
	ClipPathReader::ClipPathReader(int width, int height)
	{
		_Width = width;
		_Height = height;
	}
	//==============================================================================================
	String^ ClipPathReader::Read(array<Byte>^ data, int offset, int length)
	{
		Reset(offset);

		while (_Index < offset + length)
		{
			short selector = ByteConverter::ToShort(data, _Index);
			switch (selector)
			{
			case 0:
			case 3:
				SetKnotCount(data);
				break;
			case 1:
			case 2:
			case 4:
			case 5:
				AddPath(data);
				break;
			case 6:
			case 7:
			case 8:
			default:
				_Index += 24;
				break;
			}
		}

		return _Path->ToString();
	}
	//==============================================================================================
}