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
#include "MagickColor.h"
#include "..\Quantum.h"

using namespace System::Globalization;

namespace ImageMagick
{
	//==============================================================================================
	void MagickColor::Initialize(Magick::Color color)
	{
		A = color.quantumAlpha();
		B = color.quantumBlue();
		G = color.quantumGreen();
		K = color.quantumBlack();
		R = color.quantumRed();
		_PixelType = color.pixelType();
	}
	//==============================================================================================
	void MagickColor::Initialize(unsigned char red, unsigned char green, unsigned char blue,
		unsigned char alpha)
	{
#if (MAGICKCORE_QUANTUM_DEPTH == 8)
		A = (Magick::Quantum)alpha;
		B = (Magick::Quantum)blue;
		G = (Magick::Quantum)green;
		K = 0;
		R = (Magick::Quantum)red;
#elif (MAGICKCORE_QUANTUM_DEPTH == 16)
		A = Quantum::Convert(alpha);
		B = Quantum::Convert(blue);
		G = Quantum::Convert(green);
		K = 0;
		R = Quantum::Convert(red);
#else
#error Not implemented!
#endif
		_PixelType = Magick::Color::PixelType::RGBAPixel;
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

		Initialize(x11color);
	}
	//==============================================================================================
	Magick::Quantum MagickColor::ParseHex(String^ color, int offset, int length)
	{
		Magick::Quantum result = 0;
		Magick::Quantum k = 1;

		int i = length - 1;
		while (i >= 0)
		{
			wchar_t c = color[offset + i];

			if (c >= '0' && c <= '9')
				result += k * ((char)c - '0');
			else if (c >= 'a' && c <= 'f')
				result += k * ((char)c - 'a' + '\n');
			else if (c >= 'A' && c <= 'F')
				result += k * ((char)c - 'A' + '\n');
			else
				throw gcnew ArgumentException("Invalid character: " + c + ".");

			i--;
			k = k * 16;
		}

		return result;
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
			red = (unsigned char)ParseHex(color, 1, 1);
			red += red * 16;
			green = (unsigned char)ParseHex(color, 2, 1);
			green += green * 16;
			blue = (unsigned char)ParseHex(color, 3, 1);
			blue += blue * 16;

			if (color->Length == 5)
			{
				alpha = (unsigned char)ParseHex(color, 4, 1);
				alpha += alpha * 16;
			}
		}
		else if (color->Length == 7 || color->Length == 9)
		{
			red = (unsigned char)ParseHex(color, 1, 2);
			green = (unsigned char)ParseHex(color, 3, 2);
			blue = (unsigned char)ParseHex(color, 5, 2);

			if (color->Length == 9)
				alpha = (unsigned char)ParseHex(color, 7, 2);
		}
		else
		{
			throw gcnew ArgumentException("Invalid hex value.");
		}

		Initialize(red, green, blue, alpha);
	}
	//==============================================================================================
#if (MAGICKCORE_QUANTUM_DEPTH > 8)
	void MagickColor::ParseQ16HexColor(String^ color)
	{
		if (color->Length < 13)
		{
			ParseQ8HexColor(color);
		}
		else if (color->Length == 13 || color->Length == 17)
		{
			R = ParseHex(color, 1, 4);
			G = ParseHex(color, 5, 4);
			B = ParseHex(color, 9, 4);

			if (color->Length == 17)
				A = ParseHex(color, 13, 4);
			else
				A = Quantum::Max;
		}
		else
		{
			throw gcnew ArgumentException("Invalid hex value.");
		}
	}
#endif
	//==============================================================================================
	MagickColor::MagickColor(Magick::Color::PixelType pixelType)
	{
		A = Quantum::Max;
		B = 0;
		G = 0;
		K = 0;
		R = 0;
		_PixelType = pixelType;
	}
	//==============================================================================================
	MagickColor::MagickColor(MagickColor^ color)
	{
		A = color->A;
		B = color->B;
		G = color->G;
		K = color->K;
		R = color->R;
		_PixelType = color->_PixelType;
	}
	//==============================================================================================
	MagickColor::MagickColor(Magick::Color color)
	{
		Initialize(color);
	}
	//==============================================================================================
	void MagickColor::Initialize(Color color)
	{
		Initialize(color.R, color.G, color.B, color.A);
	}
	//==============================================================================================
	const Magick::Color* MagickColor::CreateColor()
	{
		if (_PixelType == Magick::Color::CMYKPixel || _PixelType == Magick::Color::CMYKAPixel)
			return new Magick::Color(R, G, B, K, A);
		else
			return new Magick::Color(R, G, B, A);
	}
	//==============================================================================================
	MagickColor::MagickColor()
	{
		A = Quantum::Max;
		B = 0;
		G = 0;
		K = 0;
		R = 0;
		_PixelType = Magick::Color::PixelType::RGBPixel;
	}
	//==============================================================================================
	MagickColor::MagickColor(Color color)
	{
		Initialize(color);
	}
	//==============================================================================================
	MagickColor::MagickColor(Magick::Quantum red, Magick::Quantum green, Magick::Quantum blue)
	{
		A = Quantum::Max;
		B = blue;
		G = green;
		K = 0;
		R = red;
		_PixelType = Magick::Color::PixelType::RGBPixel;
	}
	//==============================================================================================
	MagickColor::MagickColor(Magick::Quantum red, Magick::Quantum green, Magick::Quantum blue,
		Magick::Quantum alpha)
	{
		A = alpha;
		B = blue;
		G = green;
		K = 0;
		R = red;
		_PixelType = Magick::Color::PixelType::RGBAPixel;
	}
	//==============================================================================================
	MagickColor::MagickColor(Magick::Quantum cyan, Magick::Quantum magenta, Magick::Quantum yellow,
		Magick::Quantum key, Magick::Quantum alpha)
	{
		A = alpha;
		B = yellow;
		G = magenta;
		K = key;
		R = cyan;
		_PixelType = Magick::Color::PixelType::CMYKAPixel;
	}
	//==============================================================================================
	MagickColor::MagickColor(String^ color)
	{
		Throw::IfNullOrEmpty("color", color);

		if (color->Equals("transparent", StringComparison::OrdinalIgnoreCase))
		{
			A = 0;
			B = Quantum::Max;
			G = Quantum::Max;
			K = Quantum::Max;
			R = Quantum::Max;
			_PixelType = Magick::Color::PixelType::RGBAPixel;
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
			_PixelType = Magick::Color::PixelType::RGBAPixel;
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

		if(this->K < other->K)
			return -1;

		if(this->K > other->K)
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

		if ((_PixelType == Magick::Color::PixelType::CMYKPixel || _PixelType == Magick::Color::PixelType::CMYKAPixel))
		{
			if (other->_PixelType != Magick::Color::PixelType::CMYKPixel && other->_PixelType != Magick::Color::PixelType::CMYKAPixel)
				return false;
		}

		return
			A == other->A &&
			B == other->B &&
			G == other->G &&
			K == other->K &&
			R == other->R;
	}
	//==============================================================================================
	int MagickColor::GetHashCode()
	{
		return
			((int)_PixelType).GetHashCode() ^
			A.GetHashCode() ^
			B.GetHashCode() ^
			G.GetHashCode() ^
			K.GetHashCode() ^
			R.GetHashCode();
	}
	//==============================================================================================
	Color MagickColor::ToColor()
	{
		int alpha = MagickCore::ScaleQuantumToChar(A);
		int blue = MagickCore::ScaleQuantumToChar(B);
		int green = MagickCore::ScaleQuantumToChar(G);
		int red = MagickCore::ScaleQuantumToChar(R);

		return Color::FromArgb(alpha, red, green, blue);
	}
	//==============================================================================================
	String^ MagickColor::ToString()
	{
		if (_PixelType == Magick::Color::CMYKPixel || _PixelType == Magick::Color::CMYKAPixel)
			return String::Format(CultureInfo::InvariantCulture, "cmyka({0},{1},{2},{3},{4:0.0###})",
			R, G, B, K, (double)A/Quantum::Max);
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