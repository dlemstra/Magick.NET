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

#include "IO\MagickReaderSettings.h"

using namespace System::IO;
using namespace System::Collections::Generic;

namespace ImageMagick
{
  namespace Wrapper
  {
    private ref class MagickImageInfo sealed
    {
    private:
      ColorSpace _ColorSpace;
      CompressionMethod _CompressionMethod;
      String^ _FileName;
      MagickFormat _Format;
      int _Height;
      Resolution _ResolutionUnits;
      double _ResolutionX;
      double _ResolutionY;
      int _Width;

      static MagickReaderSettings^ CreateReadSettings();

      static IEnumerable<MagickImageInfo^>^ Enumerate(std::vector<Magick::Image>* images);

      static void HandleException(MagickException^ exception);

      MagickException^ Initialize(Magick::Image* image);

    public:
      MagickImageInfo() {};

      property ColorSpace ColorSpace
      {
        ImageMagick::ColorSpace get();
      }

      property CompressionMethod CompressionMethod
      {
        ImageMagick::CompressionMethod get();
      }

      property String^ FileName
      {
        String^ get();
      }

      property MagickFormat Format
      {
        MagickFormat get();
      }

      property int Height
      {
        int get();
      }

      property Resolution ResolutionUnits
      {
        Resolution get();
      }

      property double ResolutionX
      {
        double get();
      }

      property double ResolutionY
      {
        double get();
      }

      property int Width
      {
        int get();
      }

      void Read(array<Byte>^ data);

      void Read(String^ fileName);

      void Read(Stream^ stream);

      static IEnumerable<MagickImageInfo^>^ ReadCollection(array<Byte>^ data);

      static IEnumerable<MagickImageInfo^>^ ReadCollection(Stream^ stream);

      static IEnumerable<MagickImageInfo^>^ ReadCollection(String^ fileName);
    };
  }
}