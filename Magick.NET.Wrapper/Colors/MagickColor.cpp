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
  namespace Wrapper
  {
    void MagickColor::Initialize(Magick::Color color)
    {
      A = color.quantumAlpha();
      B = color.quantumBlue();
      G = color.quantumGreen();
      K = color.quantumBlack();
      R = color.quantumRed();
      _PixelType = color.pixelType();
    }

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

    MagickColor::MagickColor(Magick::Color::PixelType pixelType)
    {
      A = Quantum::Max;
      B = 0;
      G = 0;
      K = 0;
      R = 0;
      _PixelType = pixelType;
    }

    MagickColor::MagickColor(Magick::Color color)
    {
      Initialize(color);
    }

    void MagickColor::Initialize(Color color)
    {
      Initialize(color.R, color.G, color.B, color.A);
    }

    const Magick::Color* MagickColor::CreateColor()
    {
      if (_PixelType == Magick::Color::CMYKPixel || _PixelType == Magick::Color::CMYKAPixel)
        return new Magick::Color(R, G, B, K, A);
      else
        return new Magick::Color(R, G, B, A);
    }

    MagickColor::MagickColor()
    {
      A = Quantum::Max;
      B = 0;
      G = 0;
      K = 0;
      R = 0;
      _PixelType = Magick::Color::PixelType::RGBPixel;
    }

    MagickColor::MagickColor(Color color)
    {
      Initialize(color);
    }

    MagickColor::MagickColor(MagickColor^ color)
    {
      A = color->A;
      B = color->B;
      G = color->G;
      K = color->K;
      R = color->R;
      _PixelType = color->_PixelType;
    }

    MagickColor::MagickColor(Magick::Quantum red, Magick::Quantum green, Magick::Quantum blue)
    {
      A = Quantum::Max;
      B = blue;
      G = green;
      K = 0;
      R = red;
      _PixelType = Magick::Color::PixelType::RGBPixel;
    }

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

    MagickColor::MagickColor(String^ color)
    {
      Throw::IfNullOrEmpty("color", color);

      std::string x11colorString;
      Marshaller::Marshal(color, x11colorString);

      Magick::Color x11color;
      try
      {
        x11color = x11colorString.c_str();
      }
      catch (Magick::Exception&)
      {
      }

      Throw::IfFalse("color", x11color.isValid(), "Invalid color specified");

      Initialize(x11color);
    }

    MagickColor^ MagickColor::ConvertHSLToRGB(Tuple<double, double, double>^ value)
    {
      double red, green, blue;
      MagickCore::ConvertHSLToRGB(value->Item1, value->Item2, value->Item3, &red, &green, &blue);

      return gcnew MagickColor(Quantum::Convert(red), Quantum::Convert(green), Quantum::Convert(blue));
    }

    Tuple<double, double, double>^ MagickColor::ConvertRGBToHSL(MagickColor^ value)
    {
      double hue, luminosity, saturation;
      MagickCore::ConvertRGBToHSL(value->R, value->G, value->B, &hue, &luminosity, &saturation);

      return gcnew Tuple<double, double, double>(hue, luminosity, saturation);
    }

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

    Color MagickColor::ToColor()
    {
      int alpha = MagickCore::ScaleQuantumToChar(A);
      int blue = MagickCore::ScaleQuantumToChar(B);
      int green = MagickCore::ScaleQuantumToChar(G);
      int red = MagickCore::ScaleQuantumToChar(R);

      return Color::FromArgb(alpha, red, green, blue);
    }

    String^ MagickColor::ToString()
    {
      if (_PixelType == Magick::Color::CMYKPixel || _PixelType == Magick::Color::CMYKAPixel)
        return String::Format(CultureInfo::InvariantCulture, "cmyka({0},{1},{2},{3},{4:0.0###})",
          R, G, B, K, (double)A / Quantum::Max);
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
  }
}