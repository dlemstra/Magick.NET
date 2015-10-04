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
#include "PixelBaseCollection.h"

namespace ImageMagick
{
  namespace Wrapper
  {
    PixelBaseCollection::!PixelBaseCollection()
    {
      if (_View == NULL)
        return;

      delete _View;
      _View = NULL;
    }

    PixelBaseCollection::PixelBaseCollection(Magick::Image* image, int width, int height)
    {
      Throw::IfTrue("width", width > (int)image->size().width(), "Invalid width specified: {0}.", width);
      Throw::IfTrue("height", height > (int)image->size().height(), "Invalid height specified: {0}.", height);

      _View = new Magick::Pixels(*image);
      _Width = width;
      _Height = height;
      _Channels = (int)image->channels();
    }

    Magick::Pixels* PixelBaseCollection::View::get()
    {
      return _View;
    }

    void PixelBaseCollection::CheckPixels()
    {
      if (Pixels == NULL)
        throw gcnew InvalidOperationException("Image contains no pixel data.");
    }

    int PixelBaseCollection::GetIndex(int y)
    {
      return y * _Width * _Channels;
    }

    int PixelBaseCollection::GetIndex(int x, int y)
    {
      return ((y * _Width) + x) * _Channels;
    }

    int PixelBaseCollection::Channels::get()
    {
      return _Channels;
    }

    int PixelBaseCollection::Height::get()
    {
      return _Height;
    }

    int PixelBaseCollection::Width::get()
    {
      return _Width;
    }

    int PixelBaseCollection::GetIndex(PixelChannel channel)
    {
      return (int)View->offset((Magick::PixelChannel)channel);
    }

    array<Magick::Quantum>^ PixelBaseCollection::GetValue(int x, int y)
    {
      int index = GetIndex(x, y);

      array<Magick::Quantum>^ value = gcnew array<Magick::Quantum>(_Channels);
      for (int i = 0; i < _Channels; i++)
      {
        value[i] = *(Pixels + index + i);
      }

      return value;
    }

    array<Magick::Quantum>^ PixelBaseCollection::GetValues()
    {
      long length = _Width * _Height * _Channels;
      array<Magick::Quantum>^ value = gcnew array<Magick::Quantum>(length);

      for (long i = 0; i < length; i++)
      {
        value[i] = *(Pixels + i);
      }

      return value;
    }

    array<Magick::Quantum>^ PixelBaseCollection::GetValues(int y)
    {
      int index = GetIndex(y);

      long length = _Width * _Channels;
      array<Magick::Quantum>^ value = gcnew array<Magick::Quantum>(length);
      for (long i = 0; i < length; i++)
      {
        value[i] = *(Pixels + index + i);
      }

      return value;
    }
  }
}