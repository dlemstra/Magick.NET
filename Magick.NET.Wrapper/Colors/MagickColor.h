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

using namespace System::Drawing;

namespace ImageMagick
{
  namespace Wrapper
  {
    private ref class MagickColor sealed : IEquatable<MagickColor^>, Internal::IMagickColor
    {
    private:
      Magick::Color::PixelType _PixelType;

      void Initialize(Magick::Color color);

      void Initialize(unsigned char red, unsigned char green, unsigned char blue, unsigned char alpha);

    internal:
      MagickColor(Magick::Color::PixelType pixelType);

      MagickColor(Magick::Color color);

      const Magick::Color* CreateColor();

      void Initialize(Color color);

    public:
      MagickColor();

      MagickColor(Color color);

      MagickColor(MagickColor^ color);

      QUANTUM_CLS_COMPLIANT MagickColor(Magick::Quantum red, Magick::Quantum green, Magick::Quantum blue);

      QUANTUM_CLS_COMPLIANT MagickColor(Magick::Quantum red, Magick::Quantum green, Magick::Quantum blue,
        Magick::Quantum alpha);

      QUANTUM_CLS_COMPLIANT MagickColor(Magick::Quantum cyan, Magick::Quantum magenta, Magick::Quantum yellow,
        Magick::Quantum black, Magick::Quantum alpha);

      MagickColor(String^ color);

      QUANTUM_CLS_COMPLIANT property Magick::Quantum A;

      QUANTUM_CLS_COMPLIANT property Magick::Quantum B;

      QUANTUM_CLS_COMPLIANT property Magick::Quantum G;

      QUANTUM_CLS_COMPLIANT property Magick::Quantum K;

      QUANTUM_CLS_COMPLIANT property Magick::Quantum R;

      static MagickColor^ ConvertHSLToRGB(Tuple<double, double, double>^ value);

      static Tuple<double, double, double>^ ConvertRGBToHSL(MagickColor^ color);

      virtual bool Equals(MagickColor^ other);

      virtual int GetHashCode() override;

      Color ToColor();

      virtual String^ ToString() override;
    };
  }
}