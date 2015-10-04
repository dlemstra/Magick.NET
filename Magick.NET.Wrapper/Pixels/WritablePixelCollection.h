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

#include "PixelBaseCollection.h"
#include "..\Quantum.h"

using namespace System::Collections::Generic;

namespace ImageMagick
{
  namespace Wrapper
  {
    private ref class WritablePixelCollection sealed : PixelBaseCollection
    {
    private:
      Magick::Quantum* _Pixels;

      template<typename T>
      void SetPixels(array<T>^ values)
      {
        Magick::Quantum *p = _Pixels;

        long i = 0;
        while (i < values->Length)
        {
          *(p++) = Quantum::Convert((T)values[i++]);
        }
      }

    protected private:
      property const Magick::Quantum* Pixels
      {
        virtual const Magick::Quantum* get() override sealed;
      }

    internal:
      WritablePixelCollection(Magick::Image* image, int x, int y, int width, int height);

    public:
      void SetPixel(int x, int y, array<Magick::Quantum>^ value);

#if (MAGICKCORE_QUANTUM_DEPTH > 8)
      void Set(array<Byte>^ values);
#endif

      void Set(array<double>^ values);

      void Set(array<unsigned int>^ values);

      void Set(array<Magick::Quantum>^ values);

#if (MAGICKCORE_QUANTUM_DEPTH != 16 || defined(MAGICKCORE_HDRI_SUPPORT))
      void Set(array<unsigned short>^ values);
#endif

      void Write();
    };
  }
}