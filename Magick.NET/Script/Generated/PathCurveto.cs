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

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Xml;

#if Q8
using QuantumType = System.Byte;
#elif Q16
using QuantumType = System.UInt16;
#elif Q16HDRI
using QuantumType = System.Single;
#else
#error Not implemented!
#endif

namespace ImageMagick
{
  public sealed partial class MagickScript
  {
    private PathCurveto CreatePathCurveto(XmlElement element)
    {
      double x1_ = Variables.GetValue<double>(element, "x1");
      double y1_ = Variables.GetValue<double>(element, "y1");
      double x2_ = Variables.GetValue<double>(element, "x2");
      double y2_ = Variables.GetValue<double>(element, "y2");
      double x_ = Variables.GetValue<double>(element, "x");
      double y_ = Variables.GetValue<double>(element, "y");
      return new PathCurveto(x1_, y1_, x2_, y2_, x_, y_);
    }

    private Collection<PathCurveto> CreatePathCurvetos(XmlElement element)
    {
      Collection<PathCurveto> collection = new Collection<PathCurveto>();
      foreach (XmlElement elem in element.SelectNodes("*"))
      {
        collection.Add(CreatePathCurveto(elem));
      }
      return collection;
    }
  }
}
