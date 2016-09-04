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
    private void ExecuteIDrawable(XmlElement element, Collection<IDrawable> drawables)
    {
      switch(element.Name[0])
      {
        case 'a':
        {
          switch(element.Name[1])
          {
            case 'f':
            {
              ExecuteDrawableAffine(element, drawables);
              return;
            }
            case 'l':
            {
              ExecuteDrawableAlpha(element, drawables);
              return;
            }
            case 'r':
            {
              ExecuteDrawableArc(element, drawables);
              return;
            }
          }
          break;
        }
        case 'b':
        {
          switch(element.Name[1])
          {
            case 'e':
            {
              ExecuteDrawableBezier(element, drawables);
              return;
            }
            case 'o':
            {
              ExecuteDrawableBorderColor(element, drawables);
              return;
            }
          }
          break;
        }
        case 'c':
        {
          switch(element.Name[1])
          {
            case 'i':
            {
              ExecuteDrawableCircle(element, drawables);
              return;
            }
            case 'l':
            {
              switch(element.Name[4])
              {
                case 'P':
                {
                  ExecuteDrawableClipPath(element, drawables);
                  return;
                }
                case 'R':
                {
                  ExecuteDrawableClipRule(element, drawables);
                  return;
                }
                case 'U':
                {
                  ExecuteDrawableClipUnits(element, drawables);
                  return;
                }
              }
              break;
            }
            case 'o':
            {
              switch(element.Name[2])
              {
                case 'l':
                {
                  ExecuteDrawableColor(element, drawables);
                  return;
                }
                case 'm':
                {
                  ExecuteDrawableComposite(element, drawables);
                  return;
                }
              }
              break;
            }
          }
          break;
        }
        case 'd':
        {
          ExecuteDrawableDensity(element, drawables);
          return;
        }
        case 'e':
        {
          ExecuteDrawableEllipse(element, drawables);
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
                  ExecuteDrawableFillColor(element, drawables);
                  return;
                }
                case 'O':
                {
                  ExecuteDrawableFillOpacity(element, drawables);
                  return;
                }
                case 'P':
                {
                  ExecuteDrawableFillPatternUrl(element, drawables);
                  return;
                }
                case 'R':
                {
                  ExecuteDrawableFillRule(element, drawables);
                  return;
                }
              }
              break;
            }
            case 'o':
            {
              if (element.Name.Length == 4)
              {
                ExecuteDrawableFont(element, drawables);
                return;
              }
              if (element.Name.Length == 13)
              {
                ExecuteDrawableFontPointSize(element, drawables);
                return;
              }
              break;
            }
          }
          break;
        }
        case 'g':
        {
          ExecuteDrawableGravity(element, drawables);
          return;
        }
        case 'l':
        {
          ExecuteDrawableLine(element, drawables);
          return;
        }
        case 'p':
        {
          switch(element.Name[1])
          {
            case 'a':
            {
              ExecuteDrawablePath(element, drawables);
              return;
            }
            case 'o':
            {
              switch(element.Name[2])
              {
                case 'i':
                {
                  ExecuteDrawablePoint(element, drawables);
                  return;
                }
                case 'l':
                {
                  switch(element.Name[4])
                  {
                    case 'g':
                    {
                      ExecuteDrawablePolygon(element, drawables);
                      return;
                    }
                    case 'l':
                    {
                      ExecuteDrawablePolyline(element, drawables);
                      return;
                    }
                  }
                  break;
                }
              }
              break;
            }
            case 'u':
            {
              switch(element.Name[4])
              {
                case 'C':
                {
                  ExecuteDrawablePushClipPath(element, drawables);
                  return;
                }
                case 'P':
                {
                  ExecuteDrawablePushPattern(element, drawables);
                  return;
                }
              }
              break;
            }
          }
          break;
        }
        case 'r':
        {
          switch(element.Name[1])
          {
            case 'e':
            {
              ExecuteDrawableRectangle(element, drawables);
              return;
            }
            case 'o':
            {
              switch(element.Name[2])
              {
                case 't':
                {
                  ExecuteDrawableRotation(element, drawables);
                  return;
                }
                case 'u':
                {
                  ExecuteDrawableRoundRectangle(element, drawables);
                  return;
                }
              }
              break;
            }
          }
          break;
        }
        case 's':
        {
          switch(element.Name[1])
          {
            case 'c':
            {
              ExecuteDrawableScaling(element, drawables);
              return;
            }
            case 'k':
            {
              switch(element.Name[4])
              {
                case 'X':
                {
                  ExecuteDrawableSkewX(element, drawables);
                  return;
                }
                case 'Y':
                {
                  ExecuteDrawableSkewY(element, drawables);
                  return;
                }
              }
              break;
            }
            case 't':
            {
              switch(element.Name[6])
              {
                case 'A':
                {
                  ExecuteDrawableStrokeAntialias(element, drawables);
                  return;
                }
                case 'C':
                {
                  ExecuteDrawableStrokeColor(element, drawables);
                  return;
                }
                case 'D':
                {
                  switch(element.Name[10])
                  {
                    case 'A':
                    {
                      ExecuteDrawableStrokeDashArray(element, drawables);
                      return;
                    }
                    case 'O':
                    {
                      ExecuteDrawableStrokeDashOffset(element, drawables);
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
                      ExecuteDrawableStrokeLineCap(element, drawables);
                      return;
                    }
                    case 'J':
                    {
                      ExecuteDrawableStrokeLineJoin(element, drawables);
                      return;
                    }
                  }
                  break;
                }
                case 'M':
                {
                  ExecuteDrawableStrokeMiterLimit(element, drawables);
                  return;
                }
                case 'O':
                {
                  ExecuteDrawableStrokeOpacity(element, drawables);
                  return;
                }
                case 'P':
                {
                  ExecuteDrawableStrokePatternUrl(element, drawables);
                  return;
                }
                case 'W':
                {
                  ExecuteDrawableStrokeWidth(element, drawables);
                  return;
                }
              }
              break;
            }
          }
          break;
        }
        case 't':
        {
          switch(element.Name[1])
          {
            case 'e':
            {
              if (element.Name.Length == 4)
              {
                ExecuteDrawableText(element, drawables);
                return;
              }
              switch(element.Name[4])
              {
                case 'A':
                {
                  switch(element.Name[5])
                  {
                    case 'l':
                    {
                      ExecuteDrawableTextAlignment(element, drawables);
                      return;
                    }
                    case 'n':
                    {
                      ExecuteDrawableTextAntialias(element, drawables);
                      return;
                    }
                  }
                  break;
                }
                case 'D':
                {
                  switch(element.Name[5])
                  {
                    case 'e':
                    {
                      ExecuteDrawableTextDecoration(element, drawables);
                      return;
                    }
                    case 'i':
                    {
                      ExecuteDrawableTextDirection(element, drawables);
                      return;
                    }
                  }
                  break;
                }
                case 'E':
                {
                  ExecuteDrawableTextEncoding(element, drawables);
                  return;
                }
                case 'I':
                {
                  switch(element.Name[9])
                  {
                    case 'l':
                    {
                      ExecuteDrawableTextInterlineSpacing(element, drawables);
                      return;
                    }
                    case 'w':
                    {
                      ExecuteDrawableTextInterwordSpacing(element, drawables);
                      return;
                    }
                  }
                  break;
                }
                case 'K':
                {
                  ExecuteDrawableTextKerning(element, drawables);
                  return;
                }
                case 'U':
                {
                  ExecuteDrawableTextUnderColor(element, drawables);
                  return;
                }
              }
              break;
            }
            case 'r':
            {
              ExecuteDrawableTranslation(element, drawables);
              return;
            }
          }
          break;
        }
        case 'v':
        {
          ExecuteDrawableViewbox(element, drawables);
          return;
        }
      }
      throw new NotSupportedException(element.Name);
    }
    private void ExecuteDrawableAffine(XmlElement element, Collection<IDrawable> drawables)
    {
      double scaleX_ = Variables.GetValue<double>(element, "scaleX");
      double scaleY_ = Variables.GetValue<double>(element, "scaleY");
      double shearX_ = Variables.GetValue<double>(element, "shearX");
      double shearY_ = Variables.GetValue<double>(element, "shearY");
      double translateX_ = Variables.GetValue<double>(element, "translateX");
      double translateY_ = Variables.GetValue<double>(element, "translateY");
      drawables.Add(new DrawableAffine(scaleX_, scaleY_, shearX_, shearY_, translateX_, translateY_));
    }
    private void ExecuteDrawableAlpha(XmlElement element, Collection<IDrawable> drawables)
    {
      double x_ = Variables.GetValue<double>(element, "x");
      double y_ = Variables.GetValue<double>(element, "y");
      PaintMethod paintMethod_ = Variables.GetValue<PaintMethod>(element, "paintMethod");
      drawables.Add(new DrawableAlpha(x_, y_, paintMethod_));
    }
    private void ExecuteDrawableArc(XmlElement element, Collection<IDrawable> drawables)
    {
      double startX_ = Variables.GetValue<double>(element, "startX");
      double startY_ = Variables.GetValue<double>(element, "startY");
      double endX_ = Variables.GetValue<double>(element, "endX");
      double endY_ = Variables.GetValue<double>(element, "endY");
      double startDegrees_ = Variables.GetValue<double>(element, "startDegrees");
      double endDegrees_ = Variables.GetValue<double>(element, "endDegrees");
      drawables.Add(new DrawableArc(startX_, startY_, endX_, endY_, startDegrees_, endDegrees_));
    }
    private void ExecuteDrawableBezier(XmlElement element, Collection<IDrawable> drawables)
    {
      IEnumerable<PointD> coordinates_ = CreatePointDs(element);
      drawables.Add(new DrawableBezier(coordinates_));
    }
    private void ExecuteDrawableBorderColor(XmlElement element, Collection<IDrawable> drawables)
    {
      MagickColor color_ = Variables.GetValue<MagickColor>(element, "color");
      drawables.Add(new DrawableBorderColor(color_));
    }
    private void ExecuteDrawableCircle(XmlElement element, Collection<IDrawable> drawables)
    {
      double originX_ = Variables.GetValue<double>(element, "originX");
      double originY_ = Variables.GetValue<double>(element, "originY");
      double perimeterX_ = Variables.GetValue<double>(element, "perimeterX");
      double perimeterY_ = Variables.GetValue<double>(element, "perimeterY");
      drawables.Add(new DrawableCircle(originX_, originY_, perimeterX_, perimeterY_));
    }
    private void ExecuteDrawableClipPath(XmlElement element, Collection<IDrawable> drawables)
    {
      String clipPath_ = Variables.GetValue<String>(element, "clipPath");
      drawables.Add(new DrawableClipPath(clipPath_));
    }
    private void ExecuteDrawableClipRule(XmlElement element, Collection<IDrawable> drawables)
    {
      FillRule fillRule_ = Variables.GetValue<FillRule>(element, "fillRule");
      drawables.Add(new DrawableClipRule(fillRule_));
    }
    private void ExecuteDrawableClipUnits(XmlElement element, Collection<IDrawable> drawables)
    {
      ClipPathUnit units_ = Variables.GetValue<ClipPathUnit>(element, "units");
      drawables.Add(new DrawableClipUnits(units_));
    }
    private void ExecuteDrawableColor(XmlElement element, Collection<IDrawable> drawables)
    {
      double x_ = Variables.GetValue<double>(element, "x");
      double y_ = Variables.GetValue<double>(element, "y");
      PaintMethod paintMethod_ = Variables.GetValue<PaintMethod>(element, "paintMethod");
      drawables.Add(new DrawableColor(x_, y_, paintMethod_));
    }
    private void ExecuteDrawableComposite(XmlElement element, Collection<IDrawable> drawables)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        if (attribute.Name == "compose")
          arguments["compose"] = Variables.GetValue<CompositeOperator>(attribute);
        else if (attribute.Name == "offset")
          arguments["offset"] = Variables.GetValue<MagickGeometry>(attribute);
        else if (attribute.Name == "x")
          arguments["x"] = Variables.GetValue<double>(attribute);
        else if (attribute.Name == "y")
          arguments["y"] = Variables.GetValue<double>(attribute);
      }
      foreach (XmlElement elem in element.SelectNodes("*"))
      {
        arguments[elem.Name] = CreateMagickImage(elem);
      }
      if (OnlyContains(arguments, "offset", "compose", "image"))
        drawables.Add(new DrawableComposite((MagickGeometry)arguments["offset"], (CompositeOperator)arguments["compose"], (MagickImage)arguments["image"]));
      else if (OnlyContains(arguments, "offset", "image"))
        drawables.Add(new DrawableComposite((MagickGeometry)arguments["offset"], (MagickImage)arguments["image"]));
      else if (OnlyContains(arguments, "x", "y", "compose", "image"))
        drawables.Add(new DrawableComposite((double)arguments["x"], (double)arguments["y"], (CompositeOperator)arguments["compose"], (MagickImage)arguments["image"]));
      else if (OnlyContains(arguments, "x", "y", "image"))
        drawables.Add(new DrawableComposite((double)arguments["x"], (double)arguments["y"], (MagickImage)arguments["image"]));
      else
        throw new ArgumentException("Invalid argument combination for 'composite', allowed combinations are: [offset, compose, image] [offset, image] [x, y, compose, image] [x, y, image]");
    }
    private void ExecuteDrawableDensity(XmlElement element, Collection<IDrawable> drawables)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        if (attribute.Name == "density")
          arguments["density"] = Variables.GetValue<double>(attribute);
        else if (attribute.Name == "pointDensity")
          arguments["pointDensity"] = Variables.GetValue<PointD>(attribute);
      }
      if (OnlyContains(arguments, "density"))
        drawables.Add(new DrawableDensity((double)arguments["density"]));
      else if (OnlyContains(arguments, "pointDensity"))
        drawables.Add(new DrawableDensity((PointD)arguments["pointDensity"]));
      else
        throw new ArgumentException("Invalid argument combination for 'density', allowed combinations are: [density] [pointDensity]");
    }
    private void ExecuteDrawableEllipse(XmlElement element, Collection<IDrawable> drawables)
    {
      double originX_ = Variables.GetValue<double>(element, "originX");
      double originY_ = Variables.GetValue<double>(element, "originY");
      double radiusX_ = Variables.GetValue<double>(element, "radiusX");
      double radiusY_ = Variables.GetValue<double>(element, "radiusY");
      double startDegrees_ = Variables.GetValue<double>(element, "startDegrees");
      double endDegrees_ = Variables.GetValue<double>(element, "endDegrees");
      drawables.Add(new DrawableEllipse(originX_, originY_, radiusX_, radiusY_, startDegrees_, endDegrees_));
    }
    private void ExecuteDrawableFillColor(XmlElement element, Collection<IDrawable> drawables)
    {
      MagickColor color_ = Variables.GetValue<MagickColor>(element, "color");
      drawables.Add(new DrawableFillColor(color_));
    }
    private void ExecuteDrawableFillOpacity(XmlElement element, Collection<IDrawable> drawables)
    {
      Percentage opacity_ = Variables.GetValue<Percentage>(element, "opacity");
      drawables.Add(new DrawableFillOpacity(opacity_));
    }
    private void ExecuteDrawableFillPatternUrl(XmlElement element, Collection<IDrawable> drawables)
    {
      String url_ = Variables.GetValue<String>(element, "url");
      drawables.Add(new DrawableFillPatternUrl(url_));
    }
    private void ExecuteDrawableFillRule(XmlElement element, Collection<IDrawable> drawables)
    {
      FillRule fillRule_ = Variables.GetValue<FillRule>(element, "fillRule");
      drawables.Add(new DrawableFillRule(fillRule_));
    }
    private void ExecuteDrawableFont(XmlElement element, Collection<IDrawable> drawables)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        if (attribute.Name == "family")
          arguments["family"] = Variables.GetValue<String>(attribute);
        else if (attribute.Name == "stretch")
          arguments["stretch"] = Variables.GetValue<FontStretch>(attribute);
        else if (attribute.Name == "style")
          arguments["style"] = Variables.GetValue<FontStyleType>(attribute);
        else if (attribute.Name == "weight")
          arguments["weight"] = Variables.GetValue<FontWeight>(attribute);
      }
      if (OnlyContains(arguments, "family"))
        drawables.Add(new DrawableFont((String)arguments["family"]));
      else if (OnlyContains(arguments, "family", "style", "weight", "stretch"))
        drawables.Add(new DrawableFont((String)arguments["family"], (FontStyleType)arguments["style"], (FontWeight)arguments["weight"], (FontStretch)arguments["stretch"]));
      else
        throw new ArgumentException("Invalid argument combination for 'font', allowed combinations are: [family] [family, style, weight, stretch]");
    }
    private void ExecuteDrawableFontPointSize(XmlElement element, Collection<IDrawable> drawables)
    {
      double pointSize_ = Variables.GetValue<double>(element, "pointSize");
      drawables.Add(new DrawableFontPointSize(pointSize_));
    }
    private void ExecuteDrawableGravity(XmlElement element, Collection<IDrawable> drawables)
    {
      Gravity gravity_ = Variables.GetValue<Gravity>(element, "gravity");
      drawables.Add(new DrawableGravity(gravity_));
    }
    private void ExecuteDrawableLine(XmlElement element, Collection<IDrawable> drawables)
    {
      double startX_ = Variables.GetValue<double>(element, "startX");
      double startY_ = Variables.GetValue<double>(element, "startY");
      double endX_ = Variables.GetValue<double>(element, "endX");
      double endY_ = Variables.GetValue<double>(element, "endY");
      drawables.Add(new DrawableLine(startX_, startY_, endX_, endY_));
    }
    private void ExecuteDrawablePath(XmlElement element, Collection<IDrawable> drawables)
    {
      IEnumerable<IPath> paths_ = CreatePaths(element);
      drawables.Add(new DrawablePath(paths_));
    }
    private void ExecuteDrawablePoint(XmlElement element, Collection<IDrawable> drawables)
    {
      double x_ = Variables.GetValue<double>(element, "x");
      double y_ = Variables.GetValue<double>(element, "y");
      drawables.Add(new DrawablePoint(x_, y_));
    }
    private void ExecuteDrawablePolygon(XmlElement element, Collection<IDrawable> drawables)
    {
      IEnumerable<PointD> coordinates_ = CreatePointDs(element);
      drawables.Add(new DrawablePolygon(coordinates_));
    }
    private void ExecuteDrawablePolyline(XmlElement element, Collection<IDrawable> drawables)
    {
      IEnumerable<PointD> coordinates_ = CreatePointDs(element);
      drawables.Add(new DrawablePolyline(coordinates_));
    }
    private void ExecuteDrawablePushClipPath(XmlElement element, Collection<IDrawable> drawables)
    {
      String clipPath_ = Variables.GetValue<String>(element, "clipPath");
      drawables.Add(new DrawablePushClipPath(clipPath_));
    }
    private void ExecuteDrawablePushPattern(XmlElement element, Collection<IDrawable> drawables)
    {
      String id_ = Variables.GetValue<String>(element, "id");
      double x_ = Variables.GetValue<double>(element, "x");
      double y_ = Variables.GetValue<double>(element, "y");
      double width_ = Variables.GetValue<double>(element, "width");
      double height_ = Variables.GetValue<double>(element, "height");
      drawables.Add(new DrawablePushPattern(id_, x_, y_, width_, height_));
    }
    private void ExecuteDrawableRectangle(XmlElement element, Collection<IDrawable> drawables)
    {
      double upperLeftX_ = Variables.GetValue<double>(element, "upperLeftX");
      double upperLeftY_ = Variables.GetValue<double>(element, "upperLeftY");
      double lowerRightX_ = Variables.GetValue<double>(element, "lowerRightX");
      double lowerRightY_ = Variables.GetValue<double>(element, "lowerRightY");
      drawables.Add(new DrawableRectangle(upperLeftX_, upperLeftY_, lowerRightX_, lowerRightY_));
    }
    private void ExecuteDrawableRotation(XmlElement element, Collection<IDrawable> drawables)
    {
      double angle_ = Variables.GetValue<double>(element, "angle");
      drawables.Add(new DrawableRotation(angle_));
    }
    private void ExecuteDrawableRoundRectangle(XmlElement element, Collection<IDrawable> drawables)
    {
      double centerX_ = Variables.GetValue<double>(element, "centerX");
      double centerY_ = Variables.GetValue<double>(element, "centerY");
      double width_ = Variables.GetValue<double>(element, "width");
      double height_ = Variables.GetValue<double>(element, "height");
      double cornerWidth_ = Variables.GetValue<double>(element, "cornerWidth");
      double cornerHeight_ = Variables.GetValue<double>(element, "cornerHeight");
      drawables.Add(new DrawableRoundRectangle(centerX_, centerY_, width_, height_, cornerWidth_, cornerHeight_));
    }
    private void ExecuteDrawableScaling(XmlElement element, Collection<IDrawable> drawables)
    {
      double x_ = Variables.GetValue<double>(element, "x");
      double y_ = Variables.GetValue<double>(element, "y");
      drawables.Add(new DrawableScaling(x_, y_));
    }
    private void ExecuteDrawableSkewX(XmlElement element, Collection<IDrawable> drawables)
    {
      double angle_ = Variables.GetValue<double>(element, "angle");
      drawables.Add(new DrawableSkewX(angle_));
    }
    private void ExecuteDrawableSkewY(XmlElement element, Collection<IDrawable> drawables)
    {
      double angle_ = Variables.GetValue<double>(element, "angle");
      drawables.Add(new DrawableSkewY(angle_));
    }
    private void ExecuteDrawableStrokeAntialias(XmlElement element, Collection<IDrawable> drawables)
    {
      Boolean isEnabled_ = Variables.GetValue<Boolean>(element, "isEnabled");
      drawables.Add(new DrawableStrokeAntialias(isEnabled_));
    }
    private void ExecuteDrawableStrokeColor(XmlElement element, Collection<IDrawable> drawables)
    {
      MagickColor color_ = Variables.GetValue<MagickColor>(element, "color");
      drawables.Add(new DrawableStrokeColor(color_));
    }
    private void ExecuteDrawableStrokeDashArray(XmlElement element, Collection<IDrawable> drawables)
    {
      Double[] dash_ = Variables.GetDoubleArray(element["dash"]);
      drawables.Add(new DrawableStrokeDashArray(dash_));
    }
    private void ExecuteDrawableStrokeDashOffset(XmlElement element, Collection<IDrawable> drawables)
    {
      double offset_ = Variables.GetValue<double>(element, "offset");
      drawables.Add(new DrawableStrokeDashOffset(offset_));
    }
    private void ExecuteDrawableStrokeLineCap(XmlElement element, Collection<IDrawable> drawables)
    {
      LineCap lineCap_ = Variables.GetValue<LineCap>(element, "lineCap");
      drawables.Add(new DrawableStrokeLineCap(lineCap_));
    }
    private void ExecuteDrawableStrokeLineJoin(XmlElement element, Collection<IDrawable> drawables)
    {
      LineJoin lineJoin_ = Variables.GetValue<LineJoin>(element, "lineJoin");
      drawables.Add(new DrawableStrokeLineJoin(lineJoin_));
    }
    private void ExecuteDrawableStrokeMiterLimit(XmlElement element, Collection<IDrawable> drawables)
    {
      Int32 miterlimit_ = Variables.GetValue<Int32>(element, "miterlimit");
      drawables.Add(new DrawableStrokeMiterLimit(miterlimit_));
    }
    private void ExecuteDrawableStrokeOpacity(XmlElement element, Collection<IDrawable> drawables)
    {
      Percentage opacity_ = Variables.GetValue<Percentage>(element, "opacity");
      drawables.Add(new DrawableStrokeOpacity(opacity_));
    }
    private void ExecuteDrawableStrokePatternUrl(XmlElement element, Collection<IDrawable> drawables)
    {
      String url_ = Variables.GetValue<String>(element, "url");
      drawables.Add(new DrawableStrokePatternUrl(url_));
    }
    private void ExecuteDrawableStrokeWidth(XmlElement element, Collection<IDrawable> drawables)
    {
      double width_ = Variables.GetValue<double>(element, "width");
      drawables.Add(new DrawableStrokeWidth(width_));
    }
    private void ExecuteDrawableText(XmlElement element, Collection<IDrawable> drawables)
    {
      double x_ = Variables.GetValue<double>(element, "x");
      double y_ = Variables.GetValue<double>(element, "y");
      String value_ = Variables.GetValue<String>(element, "value");
      drawables.Add(new DrawableText(x_, y_, value_));
    }
    private void ExecuteDrawableTextAlignment(XmlElement element, Collection<IDrawable> drawables)
    {
      TextAlignment alignment_ = Variables.GetValue<TextAlignment>(element, "alignment");
      drawables.Add(new DrawableTextAlignment(alignment_));
    }
    private void ExecuteDrawableTextAntialias(XmlElement element, Collection<IDrawable> drawables)
    {
      Boolean isEnabled_ = Variables.GetValue<Boolean>(element, "isEnabled");
      drawables.Add(new DrawableTextAntialias(isEnabled_));
    }
    private void ExecuteDrawableTextDecoration(XmlElement element, Collection<IDrawable> drawables)
    {
      TextDecoration decoration_ = Variables.GetValue<TextDecoration>(element, "decoration");
      drawables.Add(new DrawableTextDecoration(decoration_));
    }
    private void ExecuteDrawableTextDirection(XmlElement element, Collection<IDrawable> drawables)
    {
      TextDirection direction_ = Variables.GetValue<TextDirection>(element, "direction");
      drawables.Add(new DrawableTextDirection(direction_));
    }
    private void ExecuteDrawableTextEncoding(XmlElement element, Collection<IDrawable> drawables)
    {
      Encoding encoding_ = Variables.GetValue<Encoding>(element, "encoding");
      drawables.Add(new DrawableTextEncoding(encoding_));
    }
    private void ExecuteDrawableTextInterlineSpacing(XmlElement element, Collection<IDrawable> drawables)
    {
      double spacing_ = Variables.GetValue<double>(element, "spacing");
      drawables.Add(new DrawableTextInterlineSpacing(spacing_));
    }
    private void ExecuteDrawableTextInterwordSpacing(XmlElement element, Collection<IDrawable> drawables)
    {
      double spacing_ = Variables.GetValue<double>(element, "spacing");
      drawables.Add(new DrawableTextInterwordSpacing(spacing_));
    }
    private void ExecuteDrawableTextKerning(XmlElement element, Collection<IDrawable> drawables)
    {
      double kerning_ = Variables.GetValue<double>(element, "kerning");
      drawables.Add(new DrawableTextKerning(kerning_));
    }
    private void ExecuteDrawableTextUnderColor(XmlElement element, Collection<IDrawable> drawables)
    {
      MagickColor color_ = Variables.GetValue<MagickColor>(element, "color");
      drawables.Add(new DrawableTextUnderColor(color_));
    }
    private void ExecuteDrawableTranslation(XmlElement element, Collection<IDrawable> drawables)
    {
      double x_ = Variables.GetValue<double>(element, "x");
      double y_ = Variables.GetValue<double>(element, "y");
      drawables.Add(new DrawableTranslation(x_, y_));
    }
    private void ExecuteDrawableViewbox(XmlElement element, Collection<IDrawable> drawables)
    {
      double upperLeftX_ = Variables.GetValue<double>(element, "upperLeftX");
      double upperLeftY_ = Variables.GetValue<double>(element, "upperLeftY");
      double lowerRightX_ = Variables.GetValue<double>(element, "lowerRightX");
      double lowerRightY_ = Variables.GetValue<double>(element, "lowerRightY");
      drawables.Add(new DrawableViewbox(upperLeftX_, upperLeftY_, lowerRightX_, lowerRightY_));
    }
  }
}
