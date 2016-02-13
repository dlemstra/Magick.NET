//=================================================================================================
// Copyright 2013-2016 Dirk Lemstra <https://magick.codeplex.com/>
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
    private MagickReadSettings CreateMagickReadSettings(XmlElement element)
    {
      if (element == null)
        return null;
      MagickReadSettings result = new MagickReadSettings();
      result.ColorSpace = Variables.GetValue<Nullable<ColorSpace>>(element, "colorSpace");
      result.Defines = CreateIReadDefines(element["defines"]);
      result.Density = Variables.GetValue<Nullable<PointD>>(element, "density");
      result.Format = Variables.GetValue<Nullable<MagickFormat>>(element, "format");
      result.FrameCount = Variables.GetValue<Nullable<Int32>>(element, "frameCount");
      result.FrameIndex = Variables.GetValue<Nullable<Int32>>(element, "frameIndex");
      result.Height = Variables.GetValue<Nullable<Int32>>(element, "height");
      result.PixelStorage = CreatePixelStorageSettings(element["pixelStorage"]);
      result.UseMonochrome = Variables.GetValue<Nullable<Boolean>>(element, "useMonochrome");
      result.Width = Variables.GetValue<Nullable<Int32>>(element, "width");
      XmlElement setDefine = (XmlElement)element.SelectSingleNode("setDefine");
      if (setDefine != null)
      {
        MagickFormat format_ = XmlHelper.GetAttribute<MagickFormat>(setDefine, "format");
        String name_ = XmlHelper.GetAttribute<String>(setDefine, "name");
        String value_ = XmlHelper.GetAttribute<String>(setDefine, "value");
        result.SetDefine(format_,name_,value_);
      }
      return result;
    }
  }
}
