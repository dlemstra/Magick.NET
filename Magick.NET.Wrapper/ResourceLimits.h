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
    private ref class ResourceLimits abstract sealed
    {
    public:
      [CLSCompliantAttribute(false)]
      static property Magick::MagickSizeType Disk
      {
        Magick::MagickSizeType get();
        void set(Magick::MagickSizeType limit);
      }

      [CLSCompliantAttribute(false)]
      static property Magick::MagickSizeType Height
      {
        Magick::MagickSizeType get();
        void set(Magick::MagickSizeType limit);
      }

      [CLSCompliantAttribute(false)]
      static property Magick::MagickSizeType Memory
      {
        Magick::MagickSizeType get();
        void set(Magick::MagickSizeType limit);
      }

      [CLSCompliantAttribute(false)]
      static property Magick::MagickSizeType Thread
      {
        Magick::MagickSizeType get();
        void set(Magick::MagickSizeType limit);
      }

      [CLSCompliantAttribute(false)]
      static property Magick::MagickSizeType Throttle
      {
        Magick::MagickSizeType get();
        void set(Magick::MagickSizeType limit);
      }

      [CLSCompliantAttribute(false)]
      static property Magick::MagickSizeType Width
      {
        Magick::MagickSizeType get();
        void set(Magick::MagickSizeType limit);
      }
    };
  }
}