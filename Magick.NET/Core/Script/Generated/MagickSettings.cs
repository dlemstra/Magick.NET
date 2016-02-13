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
    [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
    [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode")]
    private void ExecuteMagickSettings(XmlElement element, MagickSettings settings)
    {
      switch(element.Name[0])
      {
        case 'a':
        {
          switch(element.Name[1])
          {
            case 'd':
            {
              ExecuteAdjoin(element, settings);
              return;
            }
            case 'l':
            {
              ExecuteAlphaColor(element, settings);
              return;
            }
          }
          break;
        }
        case 'b':
        {
          switch(element.Name[1])
          {
            case 'a':
            {
              ExecuteBackgroundColor(element, settings);
              return;
            }
            case 'o':
            {
              ExecuteBorderColor(element, settings);
              return;
            }
          }
          break;
        }
        case 'c':
        {
          switch(element.Name[2])
          {
            case 'l':
            {
              switch(element.Name[5])
              {
                case 'S':
                {
                  ExecuteColorSpace(element, settings);
                  return;
                }
                case 'T':
                {
                  ExecuteColorType(element, settings);
                  return;
                }
              }
              break;
            }
            case 'm':
            {
              ExecuteCompressionMethod(element, settings);
              return;
            }
          }
          break;
        }
        case 'd':
        {
          switch(element.Name[2])
          {
            case 'b':
            {
              ExecuteDebug(element, settings);
              return;
            }
            case 'n':
            {
              ExecuteDensity(element, settings);
              return;
            }
          }
          break;
        }
        case 'e':
        {
          ExecuteEndian(element, settings);
          return;
        }
        case 'f':
        {
          switch(element.Name[1])
          {
            case 'i':
            {
              switch(element.Name[4])
              {
                case 'C':
                {
                  ExecuteFillColor(element, settings);
                  return;
                }
                case 'P':
                {
                  ExecuteFillPattern(element, settings);
                  return;
                }
                case 'R':
                {
                  ExecuteFillRule(element, settings);
                  return;
                }
              }
              break;
            }
            case 'o':
            {
              switch(element.Name[2])
              {
                case 'n':
                {
                  if (element.Name.Length == 4)
                  {
                    ExecuteFont(element, settings);
                    return;
                  }
                  switch(element.Name[4])
                  {
                    case 'F':
                    {
                      ExecuteFontFamily(element, settings);
                      return;
                    }
                    case 'P':
                    {
                      ExecuteFontPointsize(element, settings);
                      return;
                    }
                    case 'S':
                    {
                      ExecuteFontStyle(element, settings);
                      return;
                    }
                    case 'W':
                    {
                      ExecuteFontWeight(element, settings);
                      return;
                    }
                  }
                  break;
                }
                case 'r':
                {
                  ExecuteFormat(element, settings);
                  return;
                }
              }
              break;
            }
          }
          break;
        }
        case 'p':
        {
          ExecutePage(element, settings);
          return;
        }
        case 's':
        {
          switch(element.Name[1])
          {
            case 't':
            {
              switch(element.Name[6])
              {
                case 'A':
                {
                  ExecuteStrokeAntiAlias(element, settings);
                  return;
                }
                case 'C':
                {
                  ExecuteStrokeColor(element, settings);
                  return;
                }
                case 'D':
                {
                  switch(element.Name[10])
                  {
                    case 'A':
                    {
                      ExecuteStrokeDashArray(element, settings);
                      return;
                    }
                    case 'O':
                    {
                      ExecuteStrokeDashOffset(element, settings);
                      return;
                    }
                  }
                  break;
                }
                case 'L':
                {
                  switch(element.Name[10])
                  {
                    case 'C':
                    {
                      ExecuteStrokeLineCap(element, settings);
                      return;
                    }
                    case 'J':
                    {
                      ExecuteStrokeLineJoin(element, settings);
                      return;
                    }
                  }
                  break;
                }
                case 'M':
                {
                  ExecuteStrokeMiterLimit(element, settings);
                  return;
                }
                case 'P':
                {
                  ExecuteStrokePattern(element, settings);
                  return;
                }
                case 'W':
                {
                  ExecuteStrokeWidth(element, settings);
                  return;
                }
              }
              break;
            }
            case 'e':
            {
              switch(element.Name[12])
              {
                case 'O':
                {
                  ExecuteSetTransformOrigin(element, settings);
                  return;
                }
                case 'R':
                {
                  ExecuteSetTransformRotation(element, settings);
                  return;
                }
                case 'S':
                {
                  switch(element.Name[13])
                  {
                    case 'c':
                    {
                      ExecuteSetTransformScale(element, settings);
                      return;
                    }
                    case 'k':
                    {
                      switch(element.Name[16])
                      {
                        case 'X':
                        {
                          ExecuteSetTransformSkewX(element, settings);
                          return;
                        }
                        case 'Y':
                        {
                          ExecuteSetTransformSkewY(element, settings);
                          return;
                        }
                      }
                      break;
                    }
                  }
                  break;
                }
              }
              break;
            }
          }
          break;
        }
        case 't':
        {
          switch(element.Name[4])
          {
            case 'A':
            {
              ExecuteTextAntiAlias(element, settings);
              return;
            }
            case 'D':
            {
              ExecuteTextDirection(element, settings);
              return;
            }
            case 'E':
            {
              ExecuteTextEncoding(element, settings);
              return;
            }
            case 'G':
            {
              ExecuteTextGravity(element, settings);
              return;
            }
            case 'I':
            {
              switch(element.Name[9])
              {
                case 'l':
                {
                  ExecuteTextInterlineSpacing(element, settings);
                  return;
                }
                case 'w':
                {
                  ExecuteTextInterwordSpacing(element, settings);
                  return;
                }
              }
              break;
            }
            case 'K':
            {
              ExecuteTextKerning(element, settings);
              return;
            }
            case 'U':
            {
              ExecuteTextUnderColor(element, settings);
              return;
            }
          }
          break;
        }
        case 'v':
        {
          ExecuteVerbose(element, settings);
          return;
        }
        case 'r':
        {
          ExecuteResetTransform(settings);
          return;
        }
      }
      throw new NotImplementedException(element.Name);
    }
    private void ExecuteAdjoin(XmlElement element, MagickSettings settings)
    {
      settings.Adjoin = Variables.GetValue<Boolean>(element, "value");
    }
    private void ExecuteAlphaColor(XmlElement element, MagickSettings settings)
    {
      settings.AlphaColor = Variables.GetValue<MagickColor>(element, "value");
    }
    private void ExecuteBackgroundColor(XmlElement element, MagickSettings settings)
    {
      settings.BackgroundColor = Variables.GetValue<MagickColor>(element, "value");
    }
    private void ExecuteBorderColor(XmlElement element, MagickSettings settings)
    {
      settings.BorderColor = Variables.GetValue<MagickColor>(element, "value");
    }
    private void ExecuteColorSpace(XmlElement element, MagickSettings settings)
    {
      settings.ColorSpace = Variables.GetValue<ColorSpace>(element, "value");
    }
    private void ExecuteColorType(XmlElement element, MagickSettings settings)
    {
      settings.ColorType = Variables.GetValue<ColorType>(element, "value");
    }
    private void ExecuteCompressionMethod(XmlElement element, MagickSettings settings)
    {
      settings.CompressionMethod = Variables.GetValue<CompressionMethod>(element, "value");
    }
    private void ExecuteDebug(XmlElement element, MagickSettings settings)
    {
      settings.Debug = Variables.GetValue<Boolean>(element, "value");
    }
    private void ExecuteDensity(XmlElement element, MagickSettings settings)
    {
      settings.Density = Variables.GetValue<PointD>(element, "value");
    }
    private void ExecuteEndian(XmlElement element, MagickSettings settings)
    {
      settings.Endian = Variables.GetValue<Endian>(element, "value");
    }
    private void ExecuteFillColor(XmlElement element, MagickSettings settings)
    {
      settings.FillColor = Variables.GetValue<MagickColor>(element, "value");
    }
    private void ExecuteFillPattern(XmlElement element, MagickSettings settings)
    {
      settings.FillPattern = CreateMagickImage(element);
    }
    private void ExecuteFillRule(XmlElement element, MagickSettings settings)
    {
      settings.FillRule = Variables.GetValue<FillRule>(element, "value");
    }
    private void ExecuteFont(XmlElement element, MagickSettings settings)
    {
      settings.Font = Variables.GetValue<String>(element, "value");
    }
    private void ExecuteFontFamily(XmlElement element, MagickSettings settings)
    {
      settings.FontFamily = Variables.GetValue<String>(element, "value");
    }
    private void ExecuteFontPointsize(XmlElement element, MagickSettings settings)
    {
      settings.FontPointsize = Variables.GetValue<double>(element, "value");
    }
    private void ExecuteFontStyle(XmlElement element, MagickSettings settings)
    {
      settings.FontStyle = Variables.GetValue<FontStyleType>(element, "value");
    }
    private void ExecuteFontWeight(XmlElement element, MagickSettings settings)
    {
      settings.FontWeight = Variables.GetValue<FontWeight>(element, "value");
    }
    private void ExecuteFormat(XmlElement element, MagickSettings settings)
    {
      settings.Format = Variables.GetValue<MagickFormat>(element, "value");
    }
    private void ExecutePage(XmlElement element, MagickSettings settings)
    {
      settings.Page = Variables.GetValue<MagickGeometry>(element, "value");
    }
    private void ExecuteStrokeAntiAlias(XmlElement element, MagickSettings settings)
    {
      settings.StrokeAntiAlias = Variables.GetValue<Boolean>(element, "value");
    }
    private void ExecuteStrokeColor(XmlElement element, MagickSettings settings)
    {
      settings.StrokeColor = Variables.GetValue<MagickColor>(element, "value");
    }
    private void ExecuteStrokeDashArray(XmlElement element, MagickSettings settings)
    {
      settings.StrokeDashArray = Variables.GetDoubleArray(element);
    }
    private void ExecuteStrokeDashOffset(XmlElement element, MagickSettings settings)
    {
      settings.StrokeDashOffset = Variables.GetValue<double>(element, "value");
    }
    private void ExecuteStrokeLineCap(XmlElement element, MagickSettings settings)
    {
      settings.StrokeLineCap = Variables.GetValue<LineCap>(element, "value");
    }
    private void ExecuteStrokeLineJoin(XmlElement element, MagickSettings settings)
    {
      settings.StrokeLineJoin = Variables.GetValue<LineJoin>(element, "value");
    }
    private void ExecuteStrokeMiterLimit(XmlElement element, MagickSettings settings)
    {
      settings.StrokeMiterLimit = Variables.GetValue<Int32>(element, "value");
    }
    private void ExecuteStrokePattern(XmlElement element, MagickSettings settings)
    {
      settings.StrokePattern = CreateMagickImage(element);
    }
    private void ExecuteStrokeWidth(XmlElement element, MagickSettings settings)
    {
      settings.StrokeWidth = Variables.GetValue<double>(element, "value");
    }
    private void ExecuteTextAntiAlias(XmlElement element, MagickSettings settings)
    {
      settings.TextAntiAlias = Variables.GetValue<Boolean>(element, "value");
    }
    private void ExecuteTextDirection(XmlElement element, MagickSettings settings)
    {
      settings.TextDirection = Variables.GetValue<TextDirection>(element, "value");
    }
    private void ExecuteTextEncoding(XmlElement element, MagickSettings settings)
    {
      settings.TextEncoding = Variables.GetValue<Encoding>(element, "value");
    }
    private void ExecuteTextGravity(XmlElement element, MagickSettings settings)
    {
      settings.TextGravity = Variables.GetValue<Gravity>(element, "value");
    }
    private void ExecuteTextInterlineSpacing(XmlElement element, MagickSettings settings)
    {
      settings.TextInterlineSpacing = Variables.GetValue<double>(element, "value");
    }
    private void ExecuteTextInterwordSpacing(XmlElement element, MagickSettings settings)
    {
      settings.TextInterwordSpacing = Variables.GetValue<double>(element, "value");
    }
    private void ExecuteTextKerning(XmlElement element, MagickSettings settings)
    {
      settings.TextKerning = Variables.GetValue<double>(element, "value");
    }
    private void ExecuteTextUnderColor(XmlElement element, MagickSettings settings)
    {
      settings.TextUnderColor = Variables.GetValue<MagickColor>(element, "value");
    }
    private void ExecuteVerbose(XmlElement element, MagickSettings settings)
    {
      settings.Verbose = Variables.GetValue<Boolean>(element, "value");
    }
    private static void ExecuteResetTransform(MagickSettings settings)
    {
      settings.ResetTransform();
    }
    private void ExecuteSetTransformOrigin(XmlElement element, MagickSettings settings)
    {
      double x_ = Variables.GetValue<double>(element, "x");
      double y_ = Variables.GetValue<double>(element, "y");
      settings.SetTransformOrigin(x_, y_);
    }
    private void ExecuteSetTransformRotation(XmlElement element, MagickSettings settings)
    {
      double angle_ = Variables.GetValue<double>(element, "angle");
      settings.SetTransformRotation(angle_);
    }
    private void ExecuteSetTransformScale(XmlElement element, MagickSettings settings)
    {
      double x_ = Variables.GetValue<double>(element, "x");
      double y_ = Variables.GetValue<double>(element, "y");
      settings.SetTransformScale(x_, y_);
    }
    private void ExecuteSetTransformSkewX(XmlElement element, MagickSettings settings)
    {
      double value_ = Variables.GetValue<double>(element, "value");
      settings.SetTransformSkewX(value_);
    }
    private void ExecuteSetTransformSkewY(XmlElement element, MagickSettings settings)
    {
      double value_ = Variables.GetValue<double>(element, "value");
      settings.SetTransformSkewY(value_);
    }
  }
}
