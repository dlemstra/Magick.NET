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
#include "MagickColor.h"

namespace ImageMagick
{
	//===========================================================================================
	void MagickColor::Initialize(unsigned char red, unsigned char green, unsigned char blue, unsigned char alpha)
	{
#if (MAGICKCORE_QUANTUM_DEPTH == 8)
		R = red;
		G = green;
		B = blue;
		A = alpha;
#elif (MAGICKCORE_QUANTUM_DEPTH == 16)
		R = (red << 8 | red);
		G = (green << 8 | green);
		B = (blue << 8 | blue);
		A = (alpha << 8 | alpha);
#else
#error Not implemented!
#endif
	}
	//==============================================================================================
	char MagickColor::ParseHexChar(wchar_t c)
	{
		if (c >= '0' && c <= '9')
			return ((char)c - '0');
		if (c >= 'a' && c <= 'f')
			return ((char)c - 'a' + '\n');
		if (c >= 'A' && c <= 'F')
			return ((char)c - 'A' + '\n');

		throw gcnew ArgumentException("Invalid character: " + c + ".");
	}
	//==============================================================================================
	void MagickColor::ParseQ8HexColor(String^ color)
	{
		unsigned char red;
		unsigned char green;
		unsigned char blue;
		unsigned char alpha = 0;

		if (color->Length == 4 || color->Length == 5)
		{
			red = ParseHexChar(color[1]);
			red += red * 16;
			green = ParseHexChar(color[2]);
			green += green * 16;
			blue = ParseHexChar(color[3]);
			blue += blue * 16;

			if (color->Length == 5)
			{
				alpha = ParseHexChar(color[4]);
				alpha += alpha * 16;
			}
		}
		else if (color->Length == 7 || color->Length == 9)
		{
			red = (ParseHexChar(color[1]) * 16) + ParseHexChar(color[2]);
			green = (ParseHexChar(color[3]) * 16) + ParseHexChar(color[4]);
			blue = (ParseHexChar(color[5]) * 16) + ParseHexChar(color[6]);

			if (color->Length == 9)
				alpha = (ParseHexChar(color[7]) * 16) + ParseHexChar(color[8]);
		}
		else
		{
			throw gcnew ArgumentException("Invalid hex value.");
		}

		Initialize(red, green, blue, alpha);
	}
	//==============================================================================================
#if (MAGICKCORE_QUANTUM_DEPTH > 8)
	//==============================================================================================
	void MagickColor::ParseQ16HexColor(String^ color)
	{
		if (color->Length < 13)
		{
			ParseQ8HexColor(color);
		}
		else if (color->Length == 13 || color->Length == 17)
		{
			R = (ParseHexChar(color[1]) * 4096) + (ParseHexChar(color[2]) * 256) + (ParseHexChar(color[3]) * 16) + ParseHexChar(color[4]);
			G = (ParseHexChar(color[5]) * 4096) + (ParseHexChar(color[6]) * 256) + (ParseHexChar(color[7]) * 16) + ParseHexChar(color[8]);
			B = (ParseHexChar(color[9]) * 4096) + (ParseHexChar(color[10]) * 256) + (ParseHexChar(color[11]) * 16) + ParseHexChar(color[12]);

			if (color->Length == 17)
				A = (ParseHexChar(color[13]) * 4096) + (ParseHexChar(color[14]) * 256) + (ParseHexChar(color[15]) * 16) + ParseHexChar(color[16]);
		}
		else
		{
			throw gcnew ArgumentException("Invalid hex value.");
		}
	}
	//==============================================================================================
#endif
	//==============================================================================================
	MagickColor::MagickColor(MagickColor^ color)
	{
		R = color->R;
		G = color->G;
		B = color->B;
		A = color->A;
	}
	//==============================================================================================
	MagickColor::MagickColor(Magick::Color color)
	{
		R = color.redQuantum();
		G = color.greenQuantum();
		B = color.blueQuantum();
		A = color.alphaQuantum();
	}
	//==============================================================================================
	void MagickColor::Initialize(Color color)
	{
		Initialize(color.R, color.G, color.B, 255 - color.A);
	}
	//==============================================================================================
	const Magick::Color* MagickColor::CreateColor()
	{
		return new Magick::Color(R, G, B, A);
	}
	//==============================================================================================
	MagickColor::MagickColor()
	{
		A = 0;
	}
	//==============================================================================================
	MagickColor::MagickColor(Color color)
	{
		Initialize(color);
	}
	//==============================================================================================
	MagickColor::MagickColor(Magick::Quantum red, Magick::Quantum green, Magick::Quantum blue)
	{
		R = red;
		G = green;
		B = blue;
		A = 0;
	}
	//==============================================================================================
	MagickColor::MagickColor(Magick::Quantum red, Magick::Quantum green, Magick::Quantum blue, 
		Magick::Quantum alpha)
	{
		R = red;
		G = green;
		B = blue;
		A = alpha;
	}
	//==============================================================================================
	MagickColor::MagickColor(String^ hexValue)
	{
		Throw::IfNullOrEmpty("hexValue", hexValue);
		Throw::IfFalse("hexValue", hexValue[0] == '#', "Value should start with '#'.");

#if (MAGICKCORE_QUANTUM_DEPTH == 8)
		ParseQ8HexColor(hexValue);
#elif (MAGICKCORE_QUANTUM_DEPTH == 16)
		ParseQ16HexColor(hexValue);
#else
#error Not implemented!
#endif
	}
	//==============================================================================================
	bool MagickColor::operator == (MagickColor^ left, MagickColor^ right)
	{
		return Object::Equals(left, right);
	}
	//==============================================================================================
	bool MagickColor::operator != (MagickColor^ left, MagickColor^ right)
	{
		return !Object::Equals(left, right);
	}
	//==============================================================================================
	bool MagickColor::operator > (MagickColor^ left, MagickColor^ right)
	{
		if (ReferenceEquals(left, nullptr))
			return ReferenceEquals(right, nullptr);

		return left->CompareTo(right) == 1;
	}
	//==============================================================================================
	bool MagickColor::operator < (MagickColor^ left, MagickColor^ right)
	{
		if (ReferenceEquals(left, nullptr))
			return !ReferenceEquals(right, nullptr);

		return left->CompareTo(right) == -1;
	}
	//==============================================================================================
	bool MagickColor::operator >= (MagickColor^ left, MagickColor^ right)
	{
		if (ReferenceEquals(left, nullptr))
			return ReferenceEquals(right, nullptr);

		return left->CompareTo(right) >= 0;
	}
	//==============================================================================================
	bool MagickColor::operator <= (MagickColor^ left, MagickColor^ right)
	{
		if (ReferenceEquals(left, nullptr))
			return !ReferenceEquals(right, nullptr);

		return left->CompareTo(right) <= 0;
	}
	//==============================================================================================
	int MagickColor::CompareTo(MagickColor^ other)
	{
		if (ReferenceEquals(other, nullptr))
			return 1;

		if(this->R < other->R)
			return -1;

		if (this->R > other->R)
			return 1;

		if (this->G < other->G)
			return -1;

		if (this->G > other->G)
			return 1;

		if (this->B < other->B)
			return -1;

		if (this->B > other->B)
			return 1;

		if(this->A < other->A)
			return -1;

		if(this->A > other->A)
			return 1;

		return 0;
	}
	//==============================================================================================
	bool MagickColor::Equals(Object^ obj)
	{
		if (ReferenceEquals(this, obj))
			return true;

		return Equals(dynamic_cast<MagickColor^>(obj));
	}
	//==============================================================================================
	bool MagickColor::Equals(MagickColor^ other)
	{
		if (ReferenceEquals(other, nullptr))
			return false;

		if (ReferenceEquals(this, other))
			return true;

		return
			A == other->A &&
			G == other->G &&
			B == other->B &&
			R == other->R;
	}
	//==============================================================================================
	int MagickColor::GetHashCode()
	{
		return
			A.GetHashCode() ^
			G.GetHashCode() ^
			B.GetHashCode() ^
			R.GetHashCode();
	}
	//==============================================================================================
	Color MagickColor::ToColor()
	{
#if (MAGICKCORE_QUANTUM_DEPTH == 8)
		int alpha = MaxMap - A;
		int red = R;
		int green = G;
		int blue = B;
#elif (MAGICKCORE_QUANTUM_DEPTH == 16)
		int alpha = (MaxMap - A) >> 8;
		int red = R >> 8;
		int green = G>> 8;
		int blue = B >> 8;
#else
#error Not implemented!
#endif

		return Color::FromArgb(alpha, red, green, blue);
	}
	//==============================================================================================
}