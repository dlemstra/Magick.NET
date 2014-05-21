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
#include "MagickColor.h"
#include "..\Quantum.h"

using namespace System::Globalization;

namespace ImageMagick
{
	//==============================================================================================
	void MagickColor::Initialize(unsigned char red, unsigned char green, unsigned char blue,
		unsigned char alpha)
	{
#if (MAGICKCORE_QUANTUM_DEPTH == 8)
		R = (Magick::Quantum)red;
		G = (Magick::Quantum)green;
		B = (Magick::Quantum)blue;
		A = (Magick::Quantum)alpha;
#elif (MAGICKCORE_QUANTUM_DEPTH == 16)
		R = Quantum::Convert(red);
		G = Quantum::Convert(green);
		B = Quantum::Convert(blue);
		A = Quantum::Convert(alpha);
#else
#error Not implemented!
#endif
	}
	//==============================================================================================
	void MagickColor::ParseColor(String^ color)
	{
		std::string x11colorString;
		Marshaller::Marshal(color, x11colorString);

		Magick::Color x11color;
		try
		{
			x11color = x11colorString.c_str();
		}
		catch(Magick::Exception&)
		{
		}

		if (!x11color.isValid())
			throw gcnew ArgumentException("Invalid color specified", "color");

		R = x11color.redQuantum();
		G = x11color.greenQuantum();
		B = x11color.blueQuantum();
		A = Quantum::Max - x11color.alphaQuantum();
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
		unsigned char alpha = 255;

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
			R = (Magick::Quantum)((ParseHexChar(color[1]) * 4096) + (ParseHexChar(color[2]) * 256) + (ParseHexChar(color[3]) * 16) + ParseHexChar(color[4]));
			G = (Magick::Quantum)((ParseHexChar(color[5]) * 4096) + (ParseHexChar(color[6]) * 256) + (ParseHexChar(color[7]) * 16) + ParseHexChar(color[8]));
			B = (Magick::Quantum)((ParseHexChar(color[9]) * 4096) + (ParseHexChar(color[10]) * 256) + (ParseHexChar(color[11]) * 16) + ParseHexChar(color[12]));

			if (color->Length == 17)
				A = (Magick::Quantum)((ParseHexChar(color[13]) * 4096) + (ParseHexChar(color[14]) * 256) + (ParseHexChar(color[15]) * 16) + ParseHexChar(color[16]));
			else
				A = Quantum::Max;
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
		A = Quantum::Max - color.alphaQuantum();
	}
	//==============================================================================================
	void MagickColor::Initialize(Color color)
	{
		Initialize(color.R, color.G, color.B, color.A);
	}
	//==============================================================================================
	const Magick::Color* MagickColor::CreateColor()
	{
		return new Magick::Color(R, G, B, Quantum::Max - A);
	}
	//==============================================================================================
	MagickColor::MagickColor()
	{
		A = Quantum::Max;
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
		A = Quantum::Max;
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
	MagickColor::MagickColor(String^ color)
	{
		Throw::IfNullOrEmpty("color", color);

		if (color->Equals("transparent", StringComparison::OrdinalIgnoreCase))
		{
			R = Quantum::Max;
			G = Quantum::Max;
			B = Quantum::Max;
			A = 0;
			return;
		}

		if (color[0] == '#')
		{
#if (MAGICKCORE_QUANTUM_DEPTH == 8)
			ParseQ8HexColor(color);
#elif (MAGICKCORE_QUANTUM_DEPTH == 16)
			ParseQ16HexColor(color);
#else
#error Not implemented!
#endif
			return;
		}

		ParseColor(color);
	}
	//==============================================================================================
	MagickColor^ MagickColor::Transparent::get()
	{
		return gcnew MagickColor(Quantum::Max, Quantum::Max, Quantum::Max, 0);
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
	MagickColor::operator Color (MagickColor^ color)
	{
		if (ReferenceEquals(color, nullptr))
			return Color::Empty;

		return color->ToColor();
	}
	//==============================================================================================
	MagickColor::operator MagickColor^ (Color color)
	{
		return gcnew MagickColor(color);
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
		int alpha = MagickCore::ScaleQuantumToChar(A);
		int red = MagickCore::ScaleQuantumToChar(R);
		int green = MagickCore::ScaleQuantumToChar(G);
		int blue = MagickCore::ScaleQuantumToChar(B);

		return Color::FromArgb(alpha, red, green, blue);
	}
	//==============================================================================================
	String^ MagickColor::ToString()
	{
#if (MAGICKCORE_QUANTUM_DEPTH == 8)
		return String::Format(CultureInfo::InvariantCulture, "#{0:X2}{1:X2}{2:X2}{3:X2}",
			(char)R, (char)G, (char)B, (char)A);
#elif (MAGICKCORE_QUANTUM_DEPTH == 16)
		return String::Format(CultureInfo::InvariantCulture, "#{0:X4}{1:X4}{2:X4}{3:X4}",
			(unsigned short)R, (unsigned short)G, (unsigned short)B, (unsigned short)A);
#else
#error Not implemented!
#endif
	}
	//==============================================================================================
}