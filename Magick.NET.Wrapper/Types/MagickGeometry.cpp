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
#include "MagickGeometry.h"

namespace ImageMagick
{
  namespace Wrapper
  {
    void MagickGeometry::Initialize(Magick::Geometry geometry)
    {
      X = (int)geometry.xOff();
      Y = (int)geometry.yOff();
      Width = (int)geometry.width();
      Height = (int)geometry.height();
      IsPercentage = geometry.percent();
      IgnoreAspectRatio = geometry.aspect();
      Less = geometry.less();
      Greater = geometry.greater();
      FillArea = geometry.fillArea();
      LimitPixels = geometry.limitPixels();
    }

    MagickGeometry::MagickGeometry(Magick::Geometry geometry)
    {
      Initialize(geometry);
    }

    const Magick::Geometry* MagickGeometry::CreateGeometry()
    {
      Magick::Geometry* result = new Magick::Geometry(Width, Height, X, Y);
      result->percent(IsPercentage);
      result->aspect(IgnoreAspectRatio);
      result->less(Less);
      result->greater(Greater);
      result->fillArea(FillArea);
      result->limitPixels(LimitPixels);

      return result;
    }

    MagickGeometry::MagickGeometry(int x, int y, int width, int height, bool isPercentage)
    {
      X = x;
      Y = y;
      Width = width;
      Height = height;
      IsPercentage = isPercentage;
    }

    MagickGeometry::MagickGeometry(String^ value)
    {
      std::string geometrySpec;
      Marshaller::Marshal(value, geometrySpec);

      Magick::Geometry geometry = Magick::Geometry(geometrySpec);
      Throw::IfFalse("value", geometry.isValid(), "Invalid geometry specified.");

      Initialize(geometry);
    }

    String^ MagickGeometry::ToString()
    {
      const Magick::Geometry* geometry = MagickGeometry::CreateGeometry();
      try
      {
        std::string str = *geometry;
        return Marshaller::Marshal(str);
      }
      finally
      {
        delete geometry;
      }
    }
  }
}