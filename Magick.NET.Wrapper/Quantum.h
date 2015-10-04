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

#include "Stdafx.h"

namespace ImageMagick
{
  namespace Wrapper
  {
    private ref class Quantum abstract sealed
    {
    internal:
      static Magick::Quantum Convert(double value);

      static Magick::Quantum Convert(unsigned int value);

#if (MAGICKCORE_QUANTUM_DEPTH != 16 || defined(MAGICKCORE_HDRI_SUPPORT))
      static Magick::Quantum Convert(unsigned short value);
#endif

    public:
      static property int Depth
      {
        int get();
      }

      QUANTUM_CLS_COMPLIANT static property Magick::Quantum Max
      {
        Magick::Quantum get();
      }

#if (MAGICKCORE_QUANTUM_DEPTH > 8)
      static Magick::Quantum Convert(Byte value);
#endif

      static Magick::Quantum Convert(Magick::Quantum value);

      static double Scale(Magick::Quantum value);
    };
  }
}