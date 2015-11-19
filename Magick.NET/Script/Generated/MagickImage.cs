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
    [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
    [SuppressMessage("Microsoft.Maintainability", "CA1505:AvoidUnmaintainableCode")]
    private void ExecuteImage(XmlElement element, MagickImage image)
    {
      switch(element.Name[0])
      {
        case 'a':
        {
          switch(element.Name[1])
          {
            case 'd':
            {
              switch(element.Name[2])
              {
                case 'j':
                {
                  ExecuteAdjoin(element, image);
                  return;
                }
                case 'a':
                {
                  switch(element.Name[8])
                  {
                    case 'B':
                    {
                      ExecuteAdaptiveBlur(element, image);
                      return;
                    }
                    case 'R':
                    {
                      ExecuteAdaptiveResize(element, image);
                      return;
                    }
                    case 'S':
                    {
                      ExecuteAdaptiveSharpen(element, image);
                      return;
                    }
                    case 'T':
                    {
                      ExecuteAdaptiveThreshold(element, image);
                      return;
                    }
                  }
                  break;
                }
                case 'd':
                {
                  switch(element.Name[3])
                  {
                    case 'N':
                    {
                      ExecuteAddNoise(element, image);
                      return;
                    }
                    case 'P':
                    {
                      ExecuteAddProfile(element, image);
                      return;
                    }
                  }
                  break;
                }
              }
              break;
            }
            case 'l':
            {
              if (element.Name.Length == 5)
              {
                ExecuteAlpha(element, image);
                return;
              }
              if (element.Name.Length == 10)
              {
                ExecuteAlphaColor(element, image);
                return;
              }
              break;
            }
            case 'n':
            {
              switch(element.Name[2])
              {
                case 'i':
                {
                  switch(element.Name[9])
                  {
                    case 'D':
                    {
                      ExecuteAnimationDelay(element, image);
                      return;
                    }
                    case 'I':
                    {
                      ExecuteAnimationIterations(element, image);
                      return;
                    }
                  }
                  break;
                }
                case 'n':
                {
                  ExecuteAnnotate(element, image);
                  return;
                }
              }
              break;
            }
            case 'u':
            {
              switch(element.Name[4])
              {
                case 'G':
                {
                  ExecuteAutoGamma(element, image);
                  return;
                }
                case 'L':
                {
                  ExecuteAutoLevel(element, image);
                  return;
                }
                case 'O':
                {
                  ExecuteAutoOrient(image);
                  return;
                }
              }
              break;
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
              ExecuteBackgroundColor(element, image);
              return;
            }
            case 'l':
            {
              switch(element.Name[2])
              {
                case 'a':
                {
                  switch(element.Name[5])
                  {
                    case 'P':
                    {
                      ExecuteBlackPointCompensation(element, image);
                      return;
                    }
                    case 'T':
                    {
                      ExecuteBlackThreshold(element, image);
                      return;
                    }
                  }
                  break;
                }
                case 'u':
                {
                  switch(element.Name[3])
                  {
                    case 'e':
                    {
                      ExecuteBlueShift(element, image);
                      return;
                    }
                    case 'r':
                    {
                      ExecuteBlur(element, image);
                      return;
                    }
                  }
                  break;
                }
              }
              break;
            }
            case 'o':
            {
              switch(element.Name[2])
              {
                case 'r':
                {
                  if (element.Name.Length == 6)
                  {
                    ExecuteBorder(element, image);
                    return;
                  }
                  if (element.Name.Length == 11)
                  {
                    ExecuteBorderColor(element, image);
                    return;
                  }
                  break;
                }
                case 'x':
                {
                  ExecuteBoxColor(element, image);
                  return;
                }
              }
              break;
            }
            case 'i':
            {
              ExecuteBitDepth(element, image);
              return;
            }
            case 'r':
            {
              ExecuteBrightnessContrast(element, image);
              return;
            }
          }
          break;
        }
        case 'c':
        {
          switch(element.Name[1])
          {
            case 'l':
            {
              switch(element.Name[2])
              {
                case 'a':
                {
                  switch(element.Name[3])
                  {
                    case 's':
                    {
                      ExecuteClassType(element, image);
                      return;
                    }
                    case 'm':
                    {
                      ExecuteClamp(element, image);
                      return;
                    }
                  }
                  break;
                }
                case 'i':
                {
                  ExecuteClip(element, image);
                  return;
                }
                case 'u':
                {
                  ExecuteClut(element, image);
                  return;
                }
                case 'o':
                {
                  ExecuteClone(element, image);
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
                  switch(element.Name[5])
                  {
                    case 'F':
                    {
                      ExecuteColorFuzz(element, image);
                      return;
                    }
                    case 'M':
                    {
                      if (element.Name.Length == 8)
                      {
                        ExecuteColorMap(element, image);
                        return;
                      }
                      if (element.Name.Length == 12)
                      {
                        ExecuteColorMapSize(element, image);
                        return;
                      }
                      break;
                    }
                    case 'S':
                    {
                      ExecuteColorSpace(element, image);
                      return;
                    }
                    case 'T':
                    {
                      ExecuteColorType(element, image);
                      return;
                    }
                    case 'A':
                    {
                      ExecuteColorAlpha(element, image);
                      return;
                    }
                    case 'i':
                    {
                      ExecuteColorize(element, image);
                      return;
                    }
                  }
                  break;
                }
                case 'm':
                {
                  switch(element.Name[3])
                  {
                    case 'm':
                    {
                      ExecuteComment(element, image);
                      return;
                    }
                    case 'p':
                    {
                      switch(element.Name[4])
                      {
                        case 'o':
                        {
                          switch(element.Name[6])
                          {
                            case 'e':
                            {
                              ExecuteCompose(element, image);
                              return;
                            }
                            case 'i':
                            {
                              ExecuteComposite(element, image);
                              return;
                            }
                          }
                          break;
                        }
                        case 'r':
                        {
                          ExecuteCompressionMethod(element, image);
                          return;
                        }
                      }
                      break;
                    }
                  }
                  break;
                }
                case 'n':
                {
                  switch(element.Name[3])
                  {
                    case 'n':
                    {
                      ExecuteConnectedComponents(element, image);
                      return;
                    }
                    case 't':
                    {
                      if (element.Name.Length == 8)
                      {
                        ExecuteContrast(element, image);
                        return;
                      }
                      if (element.Name.Length == 15)
                      {
                        ExecuteContrastStretch(element, image);
                        return;
                      }
                      break;
                    }
                  }
                  break;
                }
                case 'p':
                {
                  ExecuteCopyPixels(element, image);
                  return;
                }
              }
              break;
            }
            case 'a':
            {
              ExecuteCannyEdge(element, image);
              return;
            }
            case 'd':
            {
              ExecuteCDL(element, image);
              return;
            }
            case 'h':
            {
              switch(element.Name[2])
              {
                case 'a':
                {
                  switch(element.Name[3])
                  {
                    case 'n':
                    {
                      ExecuteChangeColorSpace(element, image);
                      return;
                    }
                    case 'r':
                    {
                      ExecuteCharcoal(element, image);
                      return;
                    }
                  }
                  break;
                }
                case 'o':
                {
                  if (element.Name.Length == 4)
                  {
                    ExecuteChop(element, image);
                    return;
                  }
                  switch(element.Name[4])
                  {
                    case 'H':
                    {
                      ExecuteChopHorizontal(element, image);
                      return;
                    }
                    case 'V':
                    {
                      ExecuteChopVertical(element, image);
                      return;
                    }
                  }
                  break;
                }
                case 'r':
                {
                  switch(element.Name[6])
                  {
                    case 'B':
                    {
                      ExecuteChromaBluePrimary(element, image);
                      return;
                    }
                    case 'G':
                    {
                      ExecuteChromaGreenPrimary(element, image);
                      return;
                    }
                    case 'R':
                    {
                      ExecuteChromaRedPrimary(element, image);
                      return;
                    }
                    case 'W':
                    {
                      ExecuteChromaWhitePoint(element, image);
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
              ExecuteCrop(element, image);
              return;
            }
            case 'y':
            {
              ExecuteCycleColormap(element, image);
              return;
            }
          }
          break;
        }
        case 'd':
        {
          switch(element.Name[1])
          {
            case 'e':
            {
              switch(element.Name[2])
              {
                case 'b':
                {
                  ExecuteDebug(element, image);
                  return;
                }
                case 'n':
                {
                  ExecuteDensity(element, image);
                  return;
                }
                case 'p':
                {
                  ExecuteDepth(element, image);
                  return;
                }
                case 'c':
                {
                  ExecuteDecipher(element, image);
                  return;
                }
                case 's':
                {
                  switch(element.Name[3])
                  {
                    case 'k':
                    {
                      ExecuteDeskew(element, image);
                      return;
                    }
                    case 'p':
                    {
                      ExecuteDespeckle(image);
                      return;
                    }
                  }
                  break;
                }
              }
              break;
            }
            case 'i':
            {
              ExecuteDistort(element, image);
              return;
            }
            case 'r':
            {
              ExecuteDraw(element, image);
              return;
            }
          }
          break;
        }
        case 'e':
        {
          switch(element.Name[1])
          {
            case 'n':
            {
              switch(element.Name[2])
              {
                case 'd':
                {
                  ExecuteEndian(element, image);
                  return;
                }
                case 'c':
                {
                  ExecuteEncipher(element, image);
                  return;
                }
                case 'h':
                {
                  ExecuteEnhance(image);
                  return;
                }
              }
              break;
            }
            case 'd':
            {
              ExecuteEdge(element, image);
              return;
            }
            case 'm':
            {
              ExecuteEmboss(element, image);
              return;
            }
            case 'q':
            {
              ExecuteEqualize(image);
              return;
            }
            case 'v':
            {
              ExecuteEvaluate(element, image);
              return;
            }
            case 'x':
            {
              ExecuteExtent(element, image);
              return;
            }
          }
          break;
        }
        case 'f':
        {
          switch(element.Name[1])
          {
            case 'i':
            {
              switch(element.Name[3])
              {
                case 'l':
                {
                  switch(element.Name[4])
                  {
                    case 'C':
                    {
                      ExecuteFillColor(element, image);
                      return;
                    }
                    case 'P':
                    {
                      ExecuteFillPattern(element, image);
                      return;
                    }
                    case 'R':
                    {
                      ExecuteFillRule(element, image);
                      return;
                    }
                  }
                  break;
                }
                case 't':
                {
                  ExecuteFilterType(element, image);
                  return;
                }
              }
              break;
            }
            case 'l':
            {
              switch(element.Name[2])
              {
                case 'a':
                {
                  ExecuteFlashPixView(element, image);
                  return;
                }
                case 'i':
                {
                  ExecuteFlip(image);
                  return;
                }
                case 'o':
                {
                  switch(element.Name[3])
                  {
                    case 'o':
                    {
                      ExecuteFloodFill(element, image);
                      return;
                    }
                    case 'p':
                    {
                      ExecuteFlop(image);
                      return;
                    }
                  }
                  break;
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
                    ExecuteFont(element, image);
                    return;
                  }
                  switch(element.Name[4])
                  {
                    case 'F':
                    {
                      ExecuteFontFamily(element, image);
                      return;
                    }
                    case 'P':
                    {
                      ExecuteFontPointsize(element, image);
                      return;
                    }
                    case 'S':
                    {
                      ExecuteFontStyle(element, image);
                      return;
                    }
                    case 'W':
                    {
                      ExecuteFontWeight(element, image);
                      return;
                    }
                  }
                  break;
                }
                case 'r':
                {
                  ExecuteFormat(element, image);
                  return;
                }
              }
              break;
            }
            case 'r':
            {
              ExecuteFrame(element, image);
              return;
            }
            case 'x':
            {
              ExecuteFx(element, image);
              return;
            }
          }
          break;
        }
        case 'g':
        {
          switch(element.Name[1])
          {
            case 'i':
            {
              ExecuteGifDisposeMethod(element, image);
              return;
            }
            case 'a':
            {
              switch(element.Name[2])
              {
                case 'm':
                {
                  ExecuteGammaCorrect(element, image);
                  return;
                }
                case 'u':
                {
                  ExecuteGaussianBlur(element, image);
                  return;
                }
              }
              break;
            }
            case 'r':
            {
              ExecuteGrayscale(element, image);
              return;
            }
          }
          break;
        }
        case 'h':
        {
          switch(element.Name[1])
          {
            case 'a':
            {
              switch(element.Name[2])
              {
                case 's':
                {
                  ExecuteHasAlpha(element, image);
                  return;
                }
                case 'l':
                {
                  ExecuteHaldClut(element, image);
                  return;
                }
              }
              break;
            }
            case 'o':
            {
              ExecuteHoughLine(element, image);
              return;
            }
          }
          break;
        }
        case 'i':
        {
          switch(element.Name[1])
          {
            case 'n':
            {
              switch(element.Name[2])
              {
                case 't':
                {
                  switch(element.Name[5])
                  {
                    case 'l':
                    {
                      ExecuteInterlace(element, image);
                      return;
                    }
                    case 'p':
                    {
                      ExecuteInterpolate(element, image);
                      return;
                    }
                  }
                  break;
                }
                case 'v':
                {
                  switch(element.Name[7])
                  {
                    case 'F':
                    {
                      switch(element.Name[8])
                      {
                        case 'l':
                        {
                          ExecuteInverseFloodFill(element, image);
                          return;
                        }
                        case 'o':
                        {
                          ExecuteInverseFourierTransform(element, image);
                          return;
                        }
                      }
                      break;
                    }
                    case 'L':
                    {
                      if (element.Name.Length == 12)
                      {
                        ExecuteInverseLevel(element, image);
                        return;
                      }
                      if (element.Name.Length == 18)
                      {
                        ExecuteInverseLevelColors(element, image);
                        return;
                      }
                      break;
                    }
                    case 'O':
                    {
                      ExecuteInverseOpaque(element, image);
                      return;
                    }
                    case 'T':
                    {
                      ExecuteInverseTransparent(element, image);
                      return;
                    }
                  }
                  break;
                }
              }
              break;
            }
            case 'm':
            {
              ExecuteImplode(element, image);
              return;
            }
          }
          break;
        }
        case 'l':
        {
          switch(element.Name[1])
          {
            case 'a':
            {
              ExecuteLabel(element, image);
              return;
            }
            case 'e':
            {
              if (element.Name.Length == 5)
              {
                ExecuteLevel(element, image);
                return;
              }
              if (element.Name.Length == 11)
              {
                ExecuteLevelColors(element, image);
                return;
              }
              break;
            }
            case 'i':
            {
              switch(element.Name[2])
              {
                case 'n':
                {
                  ExecuteLinearStretch(element, image);
                  return;
                }
                case 'q':
                {
                  ExecuteLiquidRescale(element, image);
                  return;
                }
              }
              break;
            }
            case 'o':
            {
              switch(element.Name[2])
              {
                case 'c':
                {
                  ExecuteLocalContrast(element, image);
                  return;
                }
                case 'w':
                {
                  ExecuteLower(element, image);
                  return;
                }
              }
              break;
            }
          }
          break;
        }
        case 'm':
        {
          switch(element.Name[1])
          {
            case 'a':
            {
              switch(element.Name[2])
              {
                case 's':
                {
                  ExecuteMask(element, image);
                  return;
                }
                case 'g':
                {
                  ExecuteMagnify(image);
                  return;
                }
              }
              break;
            }
            case 'e':
            {
              ExecuteMedianFilter(element, image);
              return;
            }
            case 'i':
            {
              ExecuteMinify(image);
              return;
            }
            case 'o':
            {
              switch(element.Name[2])
              {
                case 'd':
                {
                  ExecuteModulate(element, image);
                  return;
                }
                case 'r':
                {
                  ExecuteMorphology(element, image);
                  return;
                }
                case 't':
                {
                  ExecuteMotionBlur(element, image);
                  return;
                }
              }
              break;
            }
          }
          break;
        }
        case 'o':
        {
          switch(element.Name[1])
          {
            case 'r':
            {
              switch(element.Name[2])
              {
                case 'i':
                {
                  ExecuteOrientation(element, image);
                  return;
                }
                case 'd':
                {
                  ExecuteOrderedDither(element, image);
                  return;
                }
              }
              break;
            }
            case 'i':
            {
              ExecuteOilPaint(element, image);
              return;
            }
            case 'p':
            {
              ExecuteOpaque(element, image);
              return;
            }
          }
          break;
        }
        case 'p':
        {
          switch(element.Name[1])
          {
            case 'a':
            {
              ExecutePage(element, image);
              return;
            }
            case 'e':
            {
              ExecutePerceptible(element, image);
              return;
            }
            case 'o':
            {
              switch(element.Name[2])
              {
                case 'l':
                {
                  ExecutePolaroid(element, image);
                  return;
                }
                case 's':
                {
                  ExecutePosterize(element, image);
                  return;
                }
              }
              break;
            }
            case 'r':
            {
              ExecutePreserveColorType(image);
              return;
            }
          }
          break;
        }
        case 'q':
        {
          switch(element.Name[3])
          {
            case 'l':
            {
              ExecuteQuality(element, image);
              return;
            }
            case 'n':
            {
              ExecuteQuantize(element, image);
              return;
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
              switch(element.Name[2])
              {
                case 'n':
                {
                  ExecuteRenderingIntent(element, image);
                  return;
                }
                case 's':
                {
                  switch(element.Name[3])
                  {
                    case 'o':
                    {
                      ExecuteResolutionUnits(element, image);
                      return;
                    }
                    case 'a':
                    {
                      ExecuteResample(element, image);
                      return;
                    }
                    case 'i':
                    {
                      ExecuteResize(element, image);
                      return;
                    }
                  }
                  break;
                }
                case 'd':
                {
                  ExecuteReduceNoise(element, image);
                  return;
                }
                case 'm':
                {
                  switch(element.Name[6])
                  {
                    case 'D':
                    {
                      ExecuteRemoveDefine(element, image);
                      return;
                    }
                    case 'P':
                    {
                      ExecuteRemoveProfile(element, image);
                      return;
                    }
                  }
                  break;
                }
                case 'P':
                {
                  ExecuteRePage(image);
                  return;
                }
              }
              break;
            }
            case 'a':
            {
              switch(element.Name[2])
              {
                case 'i':
                {
                  ExecuteRaise(element, image);
                  return;
                }
                case 'n':
                {
                  ExecuteRandomThreshold(element, image);
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
                  ExecuteRoll(element, image);
                  return;
                }
                case 't':
                {
                  switch(element.Name[5])
                  {
                    case 'e':
                    {
                      ExecuteRotate(element, image);
                      return;
                    }
                    case 'i':
                    {
                      ExecuteRotationalBlur(element, image);
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
        case 's':
        {
          switch(element.Name[1])
          {
            case 't':
            {
              switch(element.Name[2])
              {
                case 'r':
                {
                  switch(element.Name[3])
                  {
                    case 'o':
                    {
                      switch(element.Name[6])
                      {
                        case 'A':
                        {
                          ExecuteStrokeAntiAlias(element, image);
                          return;
                        }
                        case 'C':
                        {
                          ExecuteStrokeColor(element, image);
                          return;
                        }
                        case 'D':
                        {
                          switch(element.Name[10])
                          {
                            case 'A':
                            {
                              ExecuteStrokeDashArray(element, image);
                              return;
                            }
                            case 'O':
                            {
                              ExecuteStrokeDashOffset(element, image);
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
                              ExecuteStrokeLineCap(element, image);
                              return;
                            }
                            case 'J':
                            {
                              ExecuteStrokeLineJoin(element, image);
                              return;
                            }
                          }
                          break;
                        }
                        case 'M':
                        {
                          ExecuteStrokeMiterLimit(element, image);
                          return;
                        }
                        case 'P':
                        {
                          ExecuteStrokePattern(element, image);
                          return;
                        }
                        case 'W':
                        {
                          ExecuteStrokeWidth(element, image);
                          return;
                        }
                      }
                      break;
                    }
                    case 'i':
                    {
                      ExecuteStrip(image);
                      return;
                    }
                  }
                  break;
                }
                case 'e':
                {
                  switch(element.Name[3])
                  {
                    case 'g':
                    {
                      ExecuteStegano(element, image);
                      return;
                    }
                    case 'r':
                    {
                      ExecuteStereo(element, image);
                      return;
                    }
                  }
                  break;
                }
              }
              break;
            }
            case 'a':
            {
              ExecuteSample(element, image);
              return;
            }
            case 'c':
            {
              ExecuteScale(element, image);
              return;
            }
            case 'e':
            {
              switch(element.Name[2])
              {
                case 'g':
                {
                  ExecuteSegment(element, image);
                  return;
                }
                case 'l':
                {
                  ExecuteSelectiveBlur(element, image);
                  return;
                }
                case 'p':
                {
                  ExecuteSepiaTone(element, image);
                  return;
                }
                case 't':
                {
                  switch(element.Name[3])
                  {
                    case 'A':
                    {
                      switch(element.Name[4])
                      {
                        case 'r':
                        {
                          ExecuteSetArtifact(element, image);
                          return;
                        }
                        case 't':
                        {
                          switch(element.Name[6])
                          {
                            case 'e':
                            {
                              ExecuteSetAttenuate(element, image);
                              return;
                            }
                            case 'r':
                            {
                              ExecuteSetAttribute(element, image);
                              return;
                            }
                          }
                          break;
                        }
                      }
                      break;
                    }
                    case 'D':
                    {
                      if (element.Name.Length == 9)
                      {
                        ExecuteSetDefine(element, image);
                        return;
                      }
                      if (element.Name.Length == 10)
                      {
                        ExecuteSetDefines(element, image);
                        return;
                      }
                      break;
                    }
                    case 'H':
                    {
                      ExecuteSetHighlightColor(element, image);
                      return;
                    }
                    case 'L':
                    {
                      ExecuteSetLowlightColor(element, image);
                      return;
                    }
                  }
                  break;
                }
              }
              break;
            }
            case 'h':
            {
              switch(element.Name[2])
              {
                case 'a':
                {
                  switch(element.Name[3])
                  {
                    case 'd':
                    {
                      switch(element.Name[4])
                      {
                        case 'e':
                        {
                          ExecuteShade(element, image);
                          return;
                        }
                        case 'o':
                        {
                          ExecuteShadow(element, image);
                          return;
                        }
                      }
                      break;
                    }
                    case 'r':
                    {
                      ExecuteSharpen(element, image);
                      return;
                    }
                    case 'v':
                    {
                      ExecuteShave(element, image);
                      return;
                    }
                  }
                  break;
                }
                case 'e':
                {
                  ExecuteShear(element, image);
                  return;
                }
              }
              break;
            }
            case 'i':
            {
              ExecuteSigmoidalContrast(element, image);
              return;
            }
            case 'k':
            {
              ExecuteSketch(element, image);
              return;
            }
            case 'o':
            {
              ExecuteSolarize(element, image);
              return;
            }
            case 'p':
            {
              switch(element.Name[2])
              {
                case 'a':
                {
                  ExecuteSparseColor(element, image);
                  return;
                }
                case 'l':
                {
                  ExecuteSplice(element, image);
                  return;
                }
                case 'r':
                {
                  ExecuteSpread(element, image);
                  return;
                }
              }
              break;
            }
            case 'w':
            {
              ExecuteSwirl(element, image);
              return;
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
              switch(element.Name[4])
              {
                case 'A':
                {
                  ExecuteTextAntiAlias(element, image);
                  return;
                }
                case 'D':
                {
                  ExecuteTextDirection(element, image);
                  return;
                }
                case 'E':
                {
                  ExecuteTextEncoding(element, image);
                  return;
                }
                case 'G':
                {
                  ExecuteTextGravity(element, image);
                  return;
                }
                case 'I':
                {
                  switch(element.Name[9])
                  {
                    case 'l':
                    {
                      ExecuteTextInterlineSpacing(element, image);
                      return;
                    }
                    case 'w':
                    {
                      ExecuteTextInterwordSpacing(element, image);
                      return;
                    }
                  }
                  break;
                }
                case 'K':
                {
                  ExecuteTextKerning(element, image);
                  return;
                }
                case 'U':
                {
                  ExecuteTextUnderColor(element, image);
                  return;
                }
                case 'u':
                {
                  ExecuteTexture(element, image);
                  return;
                }
              }
              break;
            }
            case 'h':
            {
              switch(element.Name[2])
              {
                case 'r':
                {
                  ExecuteThreshold(element, image);
                  return;
                }
                case 'u':
                {
                  ExecuteThumbnail(element, image);
                  return;
                }
              }
              break;
            }
            case 'i':
            {
              switch(element.Name[2])
              {
                case 'l':
                {
                  ExecuteTile(element, image);
                  return;
                }
                case 'n':
                {
                  ExecuteTint(element, image);
                  return;
                }
              }
              break;
            }
            case 'r':
            {
              switch(element.Name[2])
              {
                case 'a':
                {
                  switch(element.Name[5])
                  {
                    case 'f':
                    {
                      if (element.Name.Length == 9)
                      {
                        ExecuteTransform(element, image);
                        return;
                      }
                      switch(element.Name[9])
                      {
                        case 'C':
                        {
                          ExecuteTransformColorSpace(element, image);
                          return;
                        }
                        case 'O':
                        {
                          ExecuteTransformOrigin(element, image);
                          return;
                        }
                        case 'R':
                        {
                          switch(element.Name[10])
                          {
                            case 'e':
                            {
                              ExecuteTransformReset(image);
                              return;
                            }
                            case 'o':
                            {
                              ExecuteTransformRotation(element, image);
                              return;
                            }
                          }
                          break;
                        }
                        case 'S':
                        {
                          switch(element.Name[10])
                          {
                            case 'c':
                            {
                              ExecuteTransformScale(element, image);
                              return;
                            }
                            case 'k':
                            {
                              switch(element.Name[13])
                              {
                                case 'X':
                                {
                                  ExecuteTransformSkewX(element, image);
                                  return;
                                }
                                case 'Y':
                                {
                                  ExecuteTransformSkewY(element, image);
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
                    case 'p':
                    {
                      switch(element.Name[6])
                      {
                        case 'a':
                        {
                          if (element.Name.Length == 11)
                          {
                            ExecuteTransparent(element, image);
                            return;
                          }
                          if (element.Name.Length == 17)
                          {
                            ExecuteTransparentChroma(element, image);
                            return;
                          }
                          break;
                        }
                        case 'o':
                        {
                          ExecuteTranspose(image);
                          return;
                        }
                      }
                      break;
                    }
                    case 'v':
                    {
                      ExecuteTransverse(image);
                      return;
                    }
                  }
                  break;
                }
                case 'i':
                {
                  ExecuteTrim(image);
                  return;
                }
              }
              break;
            }
          }
          break;
        }
        case 'v':
        {
          switch(element.Name[1])
          {
            case 'e':
            {
              ExecuteVerbose(element, image);
              return;
            }
            case 'i':
            {
              switch(element.Name[2])
              {
                case 'r':
                {
                  ExecuteVirtualPixelMethod(element, image);
                  return;
                }
                case 'g':
                {
                  ExecuteVignette(element, image);
                  return;
                }
              }
              break;
            }
          }
          break;
        }
        case 'k':
        {
          ExecuteKuwahara(element, image);
          return;
        }
        case 'n':
        {
          switch(element.Name[1])
          {
            case 'e':
            {
              ExecuteNegate(element, image);
              return;
            }
            case 'o':
            {
              ExecuteNormalize(image);
              return;
            }
          }
          break;
        }
        case 'u':
        {
          ExecuteUnsharpmask(element, image);
          return;
        }
        case 'w':
        {
          switch(element.Name[1])
          {
            case 'a':
            {
              ExecuteWave(element, image);
              return;
            }
            case 'h':
            {
              ExecuteWhiteThreshold(element, image);
              return;
            }
            case 'r':
            {
              ExecuteWrite(element, image);
              return;
            }
          }
          break;
        }
        case 'z':
        {
          ExecuteZoom(element, image);
          return;
        }
      }
      throw new NotImplementedException(element.Name);
    }

    private void ExecuteAdjoin(XmlElement element, MagickImage image)
    {
      image.Adjoin = Variables.GetValue<Boolean>(element, "value");
    }

    private void ExecuteAlphaColor(XmlElement element, MagickImage image)
    {
      image.AlphaColor = Variables.GetValue<MagickColor>(element, "value");
    }

    private void ExecuteAnimationDelay(XmlElement element, MagickImage image)
    {
      image.AnimationDelay = Variables.GetValue<Int32>(element, "value");
    }

    private void ExecuteAnimationIterations(XmlElement element, MagickImage image)
    {
      image.AnimationIterations = Variables.GetValue<Int32>(element, "value");
    }

    private void ExecuteBackgroundColor(XmlElement element, MagickImage image)
    {
      image.BackgroundColor = Variables.GetValue<MagickColor>(element, "value");
    }

    private void ExecuteBlackPointCompensation(XmlElement element, MagickImage image)
    {
      image.BlackPointCompensation = Variables.GetValue<Boolean>(element, "value");
    }

    private void ExecuteBorderColor(XmlElement element, MagickImage image)
    {
      image.BorderColor = Variables.GetValue<MagickColor>(element, "value");
    }

    private void ExecuteBoxColor(XmlElement element, MagickImage image)
    {
      image.BoxColor = Variables.GetValue<MagickColor>(element, "value");
    }

    private void ExecuteClassType(XmlElement element, MagickImage image)
    {
      image.ClassType = Variables.GetValue<ClassType>(element, "value");
    }

    private void ExecuteColorFuzz(XmlElement element, MagickImage image)
    {
      image.ColorFuzz = Variables.GetValue<Percentage>(element, "value");
    }

    private void ExecuteColorMapSize(XmlElement element, MagickImage image)
    {
      image.ColorMapSize = Variables.GetValue<Int32>(element, "value");
    }

    private void ExecuteColorSpace(XmlElement element, MagickImage image)
    {
      image.ColorSpace = Variables.GetValue<ColorSpace>(element, "value");
    }

    private void ExecuteColorType(XmlElement element, MagickImage image)
    {
      image.ColorType = Variables.GetValue<ColorType>(element, "value");
    }

    private void ExecuteComment(XmlElement element, MagickImage image)
    {
      image.Comment = Variables.GetValue<String>(element, "value");
    }

    private void ExecuteCompose(XmlElement element, MagickImage image)
    {
      image.Compose = Variables.GetValue<CompositeOperator>(element, "value");
    }

    private void ExecuteCompressionMethod(XmlElement element, MagickImage image)
    {
      image.CompressionMethod = Variables.GetValue<CompressionMethod>(element, "value");
    }

    private void ExecuteDebug(XmlElement element, MagickImage image)
    {
      image.Debug = Variables.GetValue<Boolean>(element, "value");
    }

    private void ExecuteDensity(XmlElement element, MagickImage image)
    {
      image.Density = Variables.GetValue<PointD>(element, "value");
    }

    private void ExecuteDepth(XmlElement element, MagickImage image)
    {
      image.Depth = Variables.GetValue<Int32>(element, "value");
    }

    private void ExecuteEndian(XmlElement element, MagickImage image)
    {
      image.Endian = Variables.GetValue<Endian>(element, "value");
    }

    private void ExecuteFillColor(XmlElement element, MagickImage image)
    {
      image.FillColor = Variables.GetValue<MagickColor>(element, "value");
    }

    private void ExecuteFillPattern(XmlElement element, MagickImage image)
    {
      image.FillPattern = CreateMagickImage(element);
    }

    private void ExecuteFillRule(XmlElement element, MagickImage image)
    {
      image.FillRule = Variables.GetValue<FillRule>(element, "value");
    }

    private void ExecuteFilterType(XmlElement element, MagickImage image)
    {
      image.FilterType = Variables.GetValue<FilterType>(element, "value");
    }

    private void ExecuteFlashPixView(XmlElement element, MagickImage image)
    {
      image.FlashPixView = Variables.GetValue<String>(element, "value");
    }

    private void ExecuteFont(XmlElement element, MagickImage image)
    {
      image.Font = Variables.GetValue<String>(element, "value");
    }

    private void ExecuteFontFamily(XmlElement element, MagickImage image)
    {
      image.FontFamily = Variables.GetValue<String>(element, "value");
    }

    private void ExecuteFontPointsize(XmlElement element, MagickImage image)
    {
      image.FontPointsize = Variables.GetValue<double>(element, "value");
    }

    private void ExecuteFontStyle(XmlElement element, MagickImage image)
    {
      image.FontStyle = Variables.GetValue<FontStyleType>(element, "value");
    }

    private void ExecuteFontWeight(XmlElement element, MagickImage image)
    {
      image.FontWeight = Variables.GetValue<FontWeight>(element, "value");
    }

    private void ExecuteFormat(XmlElement element, MagickImage image)
    {
      image.Format = Variables.GetValue<MagickFormat>(element, "value");
    }

    private void ExecuteGifDisposeMethod(XmlElement element, MagickImage image)
    {
      image.GifDisposeMethod = Variables.GetValue<GifDisposeMethod>(element, "value");
    }

    private void ExecuteHasAlpha(XmlElement element, MagickImage image)
    {
      image.HasAlpha = Variables.GetValue<Boolean>(element, "value");
    }

    private void ExecuteInterlace(XmlElement element, MagickImage image)
    {
      image.Interlace = Variables.GetValue<Interlace>(element, "value");
    }

    private void ExecuteInterpolate(XmlElement element, MagickImage image)
    {
      image.Interpolate = Variables.GetValue<PixelInterpolateMethod>(element, "value");
    }

    private void ExecuteLabel(XmlElement element, MagickImage image)
    {
      image.Label = Variables.GetValue<String>(element, "value");
    }

    private void ExecuteMask(XmlElement element, MagickImage image)
    {
      image.Mask = CreateMagickImage(element);
    }

    private void ExecuteOrientation(XmlElement element, MagickImage image)
    {
      image.Orientation = Variables.GetValue<OrientationType>(element, "value");
    }

    private void ExecutePage(XmlElement element, MagickImage image)
    {
      image.Page = Variables.GetValue<MagickGeometry>(element, "value");
    }

    private void ExecuteQuality(XmlElement element, MagickImage image)
    {
      image.Quality = Variables.GetValue<Int32>(element, "value");
    }

    private void ExecuteRenderingIntent(XmlElement element, MagickImage image)
    {
      image.RenderingIntent = Variables.GetValue<RenderingIntent>(element, "value");
    }

    private void ExecuteResolutionUnits(XmlElement element, MagickImage image)
    {
      image.ResolutionUnits = Variables.GetValue<Resolution>(element, "value");
    }

    private void ExecuteStrokeAntiAlias(XmlElement element, MagickImage image)
    {
      image.StrokeAntiAlias = Variables.GetValue<Boolean>(element, "value");
    }

    private void ExecuteStrokeColor(XmlElement element, MagickImage image)
    {
      image.StrokeColor = Variables.GetValue<MagickColor>(element, "value");
    }

    private void ExecuteStrokeDashArray(XmlElement element, MagickImage image)
    {
      image.StrokeDashArray = Variables.GetDoubleArray(element);
    }

    private void ExecuteStrokeDashOffset(XmlElement element, MagickImage image)
    {
      image.StrokeDashOffset = Variables.GetValue<double>(element, "value");
    }

    private void ExecuteStrokeLineCap(XmlElement element, MagickImage image)
    {
      image.StrokeLineCap = Variables.GetValue<LineCap>(element, "value");
    }

    private void ExecuteStrokeLineJoin(XmlElement element, MagickImage image)
    {
      image.StrokeLineJoin = Variables.GetValue<LineJoin>(element, "value");
    }

    private void ExecuteStrokeMiterLimit(XmlElement element, MagickImage image)
    {
      image.StrokeMiterLimit = Variables.GetValue<Int32>(element, "value");
    }

    private void ExecuteStrokePattern(XmlElement element, MagickImage image)
    {
      image.StrokePattern = CreateMagickImage(element);
    }

    private void ExecuteStrokeWidth(XmlElement element, MagickImage image)
    {
      image.StrokeWidth = Variables.GetValue<double>(element, "value");
    }

    private void ExecuteTextAntiAlias(XmlElement element, MagickImage image)
    {
      image.TextAntiAlias = Variables.GetValue<Boolean>(element, "value");
    }

    private void ExecuteTextDirection(XmlElement element, MagickImage image)
    {
      image.TextDirection = Variables.GetValue<TextDirection>(element, "value");
    }

    private void ExecuteTextEncoding(XmlElement element, MagickImage image)
    {
      image.TextEncoding = Variables.GetValue<Encoding>(element, "value");
    }

    private void ExecuteTextGravity(XmlElement element, MagickImage image)
    {
      image.TextGravity = Variables.GetValue<Gravity>(element, "value");
    }

    private void ExecuteTextInterlineSpacing(XmlElement element, MagickImage image)
    {
      image.TextInterlineSpacing = Variables.GetValue<double>(element, "value");
    }

    private void ExecuteTextInterwordSpacing(XmlElement element, MagickImage image)
    {
      image.TextInterwordSpacing = Variables.GetValue<double>(element, "value");
    }

    private void ExecuteTextKerning(XmlElement element, MagickImage image)
    {
      image.TextKerning = Variables.GetValue<double>(element, "value");
    }

    private void ExecuteTextUnderColor(XmlElement element, MagickImage image)
    {
      image.TextUnderColor = Variables.GetValue<MagickColor>(element, "value");
    }

    private void ExecuteVerbose(XmlElement element, MagickImage image)
    {
      image.Verbose = Variables.GetValue<Boolean>(element, "value");
    }

    private void ExecuteVirtualPixelMethod(XmlElement element, MagickImage image)
    {
      image.VirtualPixelMethod = Variables.GetValue<VirtualPixelMethod>(element, "value");
    }

    private void ExecuteAdaptiveBlur(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        arguments[attribute.Name] = Variables.GetValue<double>(attribute);
      }
      if (arguments.Count == 0)
        image.AdaptiveBlur();
      else if (OnlyContains(arguments, "radius"))
        image.AdaptiveBlur((double)arguments["radius"]);
      else if (OnlyContains(arguments, "radius", "sigma"))
        image.AdaptiveBlur((double)arguments["radius"], (double)arguments["sigma"]);
      else
        throw new ArgumentException("Invalid argument combination for 'adaptiveBlur', allowed combinations are: [] [radius] [radius, sigma]");
    }

    private void ExecuteAdaptiveResize(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        if (attribute.Name == "geometry")
          arguments["geometry"] = Variables.GetValue<MagickGeometry>(attribute);
        else if (attribute.Name == "height")
          arguments["height"] = Variables.GetValue<Int32>(attribute);
        else if (attribute.Name == "width")
          arguments["width"] = Variables.GetValue<Int32>(attribute);
      }
      if (OnlyContains(arguments, "geometry"))
        image.AdaptiveResize((MagickGeometry)arguments["geometry"]);
      else if (OnlyContains(arguments, "width", "height"))
        image.AdaptiveResize((Int32)arguments["width"], (Int32)arguments["height"]);
      else
        throw new ArgumentException("Invalid argument combination for 'adaptiveResize', allowed combinations are: [geometry] [width, height]");
    }

    private void ExecuteAdaptiveSharpen(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        if (attribute.Name == "channels")
          arguments["channels"] = Variables.GetValue<Channels>(attribute);
        else if (attribute.Name == "radius")
          arguments["radius"] = Variables.GetValue<double>(attribute);
        else if (attribute.Name == "sigma")
          arguments["sigma"] = Variables.GetValue<double>(attribute);
      }
      if (arguments.Count == 0)
        image.AdaptiveSharpen();
      else if (OnlyContains(arguments, "channels"))
        image.AdaptiveSharpen((Channels)arguments["channels"]);
      else if (OnlyContains(arguments, "radius", "sigma"))
        image.AdaptiveSharpen((double)arguments["radius"], (double)arguments["sigma"]);
      else if (OnlyContains(arguments, "radius", "sigma", "channels"))
        image.AdaptiveSharpen((double)arguments["radius"], (double)arguments["sigma"], (Channels)arguments["channels"]);
      else
        throw new ArgumentException("Invalid argument combination for 'adaptiveSharpen', allowed combinations are: [] [channels] [radius, sigma] [radius, sigma, channels]");
    }

    private void ExecuteAdaptiveThreshold(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        if (attribute.Name == "bias")
          arguments["bias"] = Variables.GetValue<double>(attribute);
        else if (attribute.Name == "biasPercentage")
          arguments["biasPercentage"] = Variables.GetValue<Percentage>(attribute);
        else if (attribute.Name == "height")
          arguments["height"] = Variables.GetValue<Int32>(attribute);
        else if (attribute.Name == "width")
          arguments["width"] = Variables.GetValue<Int32>(attribute);
      }
      if (OnlyContains(arguments, "width", "height"))
        image.AdaptiveThreshold((Int32)arguments["width"], (Int32)arguments["height"]);
      else if (OnlyContains(arguments, "width", "height", "bias"))
        image.AdaptiveThreshold((Int32)arguments["width"], (Int32)arguments["height"], (double)arguments["bias"]);
      else if (OnlyContains(arguments, "width", "height", "biasPercentage"))
        image.AdaptiveThreshold((Int32)arguments["width"], (Int32)arguments["height"], (Percentage)arguments["biasPercentage"]);
      else
        throw new ArgumentException("Invalid argument combination for 'adaptiveThreshold', allowed combinations are: [width, height] [width, height, bias] [width, height, biasPercentage]");
    }

    private void ExecuteAddNoise(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        if (attribute.Name == "channels")
          arguments["channels"] = Variables.GetValue<Channels>(attribute);
        else if (attribute.Name == "noiseType")
          arguments["noiseType"] = Variables.GetValue<NoiseType>(attribute);
      }
      if (OnlyContains(arguments, "noiseType"))
        image.AddNoise((NoiseType)arguments["noiseType"]);
      else if (OnlyContains(arguments, "noiseType", "channels"))
        image.AddNoise((NoiseType)arguments["noiseType"], (Channels)arguments["channels"]);
      else
        throw new ArgumentException("Invalid argument combination for 'addNoise', allowed combinations are: [noiseType] [noiseType, channels]");
    }

    private void ExecuteAddProfile(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        arguments[attribute.Name] = Variables.GetValue<Boolean>(attribute);
      }
      foreach (XmlElement elem in element.SelectNodes("*"))
      {
        arguments[elem.Name] = CreateProfile(elem);
      }
      if (OnlyContains(arguments, "profile"))
        image.AddProfile((ImageProfile)arguments["profile"]);
      else if (OnlyContains(arguments, "profile", "overwriteExisting"))
        image.AddProfile((ImageProfile)arguments["profile"], (Boolean)arguments["overwriteExisting"]);
      else
        throw new ArgumentException("Invalid argument combination for 'addProfile', allowed combinations are: [profile] [profile, overwriteExisting]");
    }

    private void ExecuteAlpha(XmlElement element, MagickImage image)
    {
      AlphaOption option_ = Variables.GetValue<AlphaOption>(element, "option");
      image.Alpha(option_);
    }

    private void ExecuteAnnotate(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        if (attribute.Name == "boundingArea")
          arguments["boundingArea"] = Variables.GetValue<MagickGeometry>(attribute);
        else if (attribute.Name == "degrees")
          arguments["degrees"] = Variables.GetValue<double>(attribute);
        else if (attribute.Name == "gravity")
          arguments["gravity"] = Variables.GetValue<Gravity>(attribute);
        else if (attribute.Name == "text")
          arguments["text"] = Variables.GetValue<String>(attribute);
      }
      if (OnlyContains(arguments, "text", "boundingArea"))
        image.Annotate((String)arguments["text"], (MagickGeometry)arguments["boundingArea"]);
      else if (OnlyContains(arguments, "text", "boundingArea", "gravity"))
        image.Annotate((String)arguments["text"], (MagickGeometry)arguments["boundingArea"], (Gravity)arguments["gravity"]);
      else if (OnlyContains(arguments, "text", "boundingArea", "gravity", "degrees"))
        image.Annotate((String)arguments["text"], (MagickGeometry)arguments["boundingArea"], (Gravity)arguments["gravity"], (double)arguments["degrees"]);
      else if (OnlyContains(arguments, "text", "gravity"))
        image.Annotate((String)arguments["text"], (Gravity)arguments["gravity"]);
      else
        throw new ArgumentException("Invalid argument combination for 'annotate', allowed combinations are: [text, boundingArea] [text, boundingArea, gravity] [text, boundingArea, gravity, degrees] [text, gravity]");
    }

    private void ExecuteAutoGamma(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        arguments[attribute.Name] = Variables.GetValue<Channels>(attribute);
      }
      if (arguments.Count == 0)
        image.AutoGamma();
      else if (OnlyContains(arguments, "channels"))
        image.AutoGamma((Channels)arguments["channels"]);
      else
        throw new ArgumentException("Invalid argument combination for 'autoGamma', allowed combinations are: [] [channels]");
    }

    private void ExecuteAutoLevel(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        arguments[attribute.Name] = Variables.GetValue<Channels>(attribute);
      }
      if (arguments.Count == 0)
        image.AutoLevel();
      else if (OnlyContains(arguments, "channels"))
        image.AutoLevel((Channels)arguments["channels"]);
      else
        throw new ArgumentException("Invalid argument combination for 'autoLevel', allowed combinations are: [] [channels]");
    }

    private static void ExecuteAutoOrient(MagickImage image)
    {
      image.AutoOrient();
    }

    private void ExecuteBitDepth(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        if (attribute.Name == "channels")
          arguments["channels"] = Variables.GetValue<Channels>(attribute);
        else if (attribute.Name == "value")
          arguments["value"] = Variables.GetValue<Int32>(attribute);
      }
      if (OnlyContains(arguments, "channels", "value"))
        image.BitDepth((Channels)arguments["channels"], (Int32)arguments["value"]);
      else if (OnlyContains(arguments, "value"))
        image.BitDepth((Int32)arguments["value"]);
      else
        throw new ArgumentException("Invalid argument combination for 'bitDepth', allowed combinations are: [channels, value] [value]");
    }

    private void ExecuteBlackThreshold(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        if (attribute.Name == "channels")
          arguments["channels"] = Variables.GetValue<Channels>(attribute);
        else if (attribute.Name == "threshold")
          arguments["threshold"] = Variables.GetValue<Percentage>(attribute);
      }
      if (OnlyContains(arguments, "threshold"))
        image.BlackThreshold((Percentage)arguments["threshold"]);
      else if (OnlyContains(arguments, "threshold", "channels"))
        image.BlackThreshold((Percentage)arguments["threshold"], (Channels)arguments["channels"]);
      else
        throw new ArgumentException("Invalid argument combination for 'blackThreshold', allowed combinations are: [threshold] [threshold, channels]");
    }

    private void ExecuteBlueShift(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        arguments[attribute.Name] = Variables.GetValue<double>(attribute);
      }
      if (arguments.Count == 0)
        image.BlueShift();
      else if (OnlyContains(arguments, "factor"))
        image.BlueShift((double)arguments["factor"]);
      else
        throw new ArgumentException("Invalid argument combination for 'blueShift', allowed combinations are: [] [factor]");
    }

    private void ExecuteBlur(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        if (attribute.Name == "channels")
          arguments["channels"] = Variables.GetValue<Channels>(attribute);
        else if (attribute.Name == "radius")
          arguments["radius"] = Variables.GetValue<double>(attribute);
        else if (attribute.Name == "sigma")
          arguments["sigma"] = Variables.GetValue<double>(attribute);
      }
      if (arguments.Count == 0)
        image.Blur();
      else if (OnlyContains(arguments, "channels"))
        image.Blur((Channels)arguments["channels"]);
      else if (OnlyContains(arguments, "radius", "sigma"))
        image.Blur((double)arguments["radius"], (double)arguments["sigma"]);
      else if (OnlyContains(arguments, "radius", "sigma", "channels"))
        image.Blur((double)arguments["radius"], (double)arguments["sigma"], (Channels)arguments["channels"]);
      else
        throw new ArgumentException("Invalid argument combination for 'blur', allowed combinations are: [] [channels] [radius, sigma] [radius, sigma, channels]");
    }

    private void ExecuteBorder(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        arguments[attribute.Name] = Variables.GetValue<Int32>(attribute);
      }
      if (OnlyContains(arguments, "size"))
        image.Border((Int32)arguments["size"]);
      else if (OnlyContains(arguments, "width", "height"))
        image.Border((Int32)arguments["width"], (Int32)arguments["height"]);
      else
        throw new ArgumentException("Invalid argument combination for 'border', allowed combinations are: [size] [width, height]");
    }

    private void ExecuteBrightnessContrast(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        if (attribute.Name == "brightness")
          arguments["brightness"] = Variables.GetValue<Percentage>(attribute);
        else if (attribute.Name == "channels")
          arguments["channels"] = Variables.GetValue<Channels>(attribute);
        else if (attribute.Name == "contrast")
          arguments["contrast"] = Variables.GetValue<Percentage>(attribute);
      }
      if (OnlyContains(arguments, "brightness", "contrast"))
        image.BrightnessContrast((Percentage)arguments["brightness"], (Percentage)arguments["contrast"]);
      else if (OnlyContains(arguments, "brightness", "contrast", "channels"))
        image.BrightnessContrast((Percentage)arguments["brightness"], (Percentage)arguments["contrast"], (Channels)arguments["channels"]);
      else
        throw new ArgumentException("Invalid argument combination for 'brightnessContrast', allowed combinations are: [brightness, contrast] [brightness, contrast, channels]");
    }

    private void ExecuteCannyEdge(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        if (attribute.Name == "lower")
          arguments["lower"] = Variables.GetValue<Percentage>(attribute);
        else if (attribute.Name == "radius")
          arguments["radius"] = Variables.GetValue<double>(attribute);
        else if (attribute.Name == "sigma")
          arguments["sigma"] = Variables.GetValue<double>(attribute);
        else if (attribute.Name == "upper")
          arguments["upper"] = Variables.GetValue<Percentage>(attribute);
      }
      if (arguments.Count == 0)
        image.CannyEdge();
      else if (OnlyContains(arguments, "radius", "sigma", "lower", "upper"))
        image.CannyEdge((double)arguments["radius"], (double)arguments["sigma"], (Percentage)arguments["lower"], (Percentage)arguments["upper"]);
      else
        throw new ArgumentException("Invalid argument combination for 'cannyEdge', allowed combinations are: [] [radius, sigma, lower, upper]");
    }

    private void ExecuteCDL(XmlElement element, MagickImage image)
    {
      String fileName_ = Variables.GetValue<String>(element, "fileName");
      image.CDL(fileName_);
    }

    private void ExecuteChangeColorSpace(XmlElement element, MagickImage image)
    {
      ColorSpace value_ = Variables.GetValue<ColorSpace>(element, "value");
      image.ChangeColorSpace(value_);
    }

    private void ExecuteCharcoal(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        arguments[attribute.Name] = Variables.GetValue<double>(attribute);
      }
      if (arguments.Count == 0)
        image.Charcoal();
      else if (OnlyContains(arguments, "radius", "sigma"))
        image.Charcoal((double)arguments["radius"], (double)arguments["sigma"]);
      else
        throw new ArgumentException("Invalid argument combination for 'charcoal', allowed combinations are: [] [radius, sigma]");
    }

    private void ExecuteChop(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        if (attribute.Name == "geometry")
          arguments["geometry"] = Variables.GetValue<MagickGeometry>(attribute);
        else if (attribute.Name == "height")
          arguments["height"] = Variables.GetValue<Int32>(attribute);
        else if (attribute.Name == "width")
          arguments["width"] = Variables.GetValue<Int32>(attribute);
        else if (attribute.Name == "xOffset")
          arguments["xOffset"] = Variables.GetValue<Int32>(attribute);
        else if (attribute.Name == "yOffset")
          arguments["yOffset"] = Variables.GetValue<Int32>(attribute);
      }
      if (OnlyContains(arguments, "geometry"))
        image.Chop((MagickGeometry)arguments["geometry"]);
      else if (OnlyContains(arguments, "xOffset", "width", "yOffset", "height"))
        image.Chop((Int32)arguments["xOffset"], (Int32)arguments["width"], (Int32)arguments["yOffset"], (Int32)arguments["height"]);
      else
        throw new ArgumentException("Invalid argument combination for 'chop', allowed combinations are: [geometry] [xOffset, width, yOffset, height]");
    }

    private void ExecuteChopHorizontal(XmlElement element, MagickImage image)
    {
      Int32 offset_ = Variables.GetValue<Int32>(element, "offset");
      Int32 width_ = Variables.GetValue<Int32>(element, "width");
      image.ChopHorizontal(offset_, width_);
    }

    private void ExecuteChopVertical(XmlElement element, MagickImage image)
    {
      Int32 offset_ = Variables.GetValue<Int32>(element, "offset");
      Int32 height_ = Variables.GetValue<Int32>(element, "height");
      image.ChopVertical(offset_, height_);
    }

    private void ExecuteChromaBluePrimary(XmlElement element, MagickImage image)
    {
      double x_ = Variables.GetValue<double>(element, "x");
      double y_ = Variables.GetValue<double>(element, "y");
      image.ChromaBluePrimary(x_, y_);
    }

    private void ExecuteChromaGreenPrimary(XmlElement element, MagickImage image)
    {
      double x_ = Variables.GetValue<double>(element, "x");
      double y_ = Variables.GetValue<double>(element, "y");
      image.ChromaGreenPrimary(x_, y_);
    }

    private void ExecuteChromaRedPrimary(XmlElement element, MagickImage image)
    {
      double x_ = Variables.GetValue<double>(element, "x");
      double y_ = Variables.GetValue<double>(element, "y");
      image.ChromaRedPrimary(x_, y_);
    }

    private void ExecuteChromaWhitePoint(XmlElement element, MagickImage image)
    {
      double x_ = Variables.GetValue<double>(element, "x");
      double y_ = Variables.GetValue<double>(element, "y");
      image.ChromaWhitePoint(x_, y_);
    }

    private void ExecuteClamp(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        arguments[attribute.Name] = Variables.GetValue<Channels>(attribute);
      }
      if (arguments.Count == 0)
        image.Clamp();
      else if (OnlyContains(arguments, "channels"))
        image.Clamp((Channels)arguments["channels"]);
      else
        throw new ArgumentException("Invalid argument combination for 'clamp', allowed combinations are: [] [channels]");
    }

    private void ExecuteClip(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        if (attribute.Name == "inside")
          arguments["inside"] = Variables.GetValue<Boolean>(attribute);
        else if (attribute.Name == "pathName")
          arguments["pathName"] = Variables.GetValue<String>(attribute);
      }
      if (arguments.Count == 0)
        image.Clip();
      else if (OnlyContains(arguments, "pathName", "inside"))
        image.Clip((String)arguments["pathName"], (Boolean)arguments["inside"]);
      else
        throw new ArgumentException("Invalid argument combination for 'clip', allowed combinations are: [] [pathName, inside]");
    }

    private void ExecuteClut(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        if (attribute.Name == "channels")
          arguments["channels"] = Variables.GetValue<Channels>(attribute);
        else if (attribute.Name == "method")
          arguments["method"] = Variables.GetValue<PixelInterpolateMethod>(attribute);
      }
      foreach (XmlElement elem in element.SelectNodes("*"))
      {
        arguments[elem.Name] = CreateMagickImage(elem);
      }
      if (OnlyContains(arguments, "image", "method"))
        image.Clut((MagickImage)arguments["image"], (PixelInterpolateMethod)arguments["method"]);
      else if (OnlyContains(arguments, "image", "method", "channels"))
        image.Clut((MagickImage)arguments["image"], (PixelInterpolateMethod)arguments["method"], (Channels)arguments["channels"]);
      else
        throw new ArgumentException("Invalid argument combination for 'clut', allowed combinations are: [image, method] [image, method, channels]");
    }

    private void ExecuteColorAlpha(XmlElement element, MagickImage image)
    {
      MagickColor color_ = Variables.GetValue<MagickColor>(element, "color");
      image.ColorAlpha(color_);
    }

    private void ExecuteColorize(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        if (attribute.Name == "alpha")
          arguments["alpha"] = Variables.GetValue<Percentage>(attribute);
        else if (attribute.Name == "alphaBlue")
          arguments["alphaBlue"] = Variables.GetValue<Percentage>(attribute);
        else if (attribute.Name == "alphaGreen")
          arguments["alphaGreen"] = Variables.GetValue<Percentage>(attribute);
        else if (attribute.Name == "alphaRed")
          arguments["alphaRed"] = Variables.GetValue<Percentage>(attribute);
        else if (attribute.Name == "color")
          arguments["color"] = Variables.GetValue<MagickColor>(attribute);
      }
      if (OnlyContains(arguments, "color", "alpha"))
        image.Colorize((MagickColor)arguments["color"], (Percentage)arguments["alpha"]);
      else if (OnlyContains(arguments, "color", "alphaRed", "alphaGreen", "alphaBlue"))
        image.Colorize((MagickColor)arguments["color"], (Percentage)arguments["alphaRed"], (Percentage)arguments["alphaGreen"], (Percentage)arguments["alphaBlue"]);
      else
        throw new ArgumentException("Invalid argument combination for 'colorize', allowed combinations are: [color, alpha] [color, alphaRed, alphaGreen, alphaBlue]");
    }

    private void ExecuteColorMap(XmlElement element, MagickImage image)
    {
      Int32 index_ = Variables.GetValue<Int32>(element, "index");
      MagickColor color_ = Variables.GetValue<MagickColor>(element, "color");
      image.ColorMap(index_, color_);
    }

    private void ExecuteComposite(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        if (attribute.Name == "args")
          arguments["args"] = Variables.GetValue<String>(attribute);
        else if (attribute.Name == "compose")
          arguments["compose"] = Variables.GetValue<CompositeOperator>(attribute);
        else if (attribute.Name == "gravity")
          arguments["gravity"] = Variables.GetValue<Gravity>(attribute);
        else if (attribute.Name == "offset")
          arguments["offset"] = Variables.GetValue<MagickGeometry>(attribute);
        else if (attribute.Name == "x")
          arguments["x"] = Variables.GetValue<Int32>(attribute);
        else if (attribute.Name == "y")
          arguments["y"] = Variables.GetValue<Int32>(attribute);
      }
      foreach (XmlElement elem in element.SelectNodes("*"))
      {
        arguments[elem.Name] = CreateMagickImage(elem);
      }
      if (OnlyContains(arguments, "image", "compose"))
        image.Composite((MagickImage)arguments["image"], (CompositeOperator)arguments["compose"]);
      else if (OnlyContains(arguments, "image", "compose", "args"))
        image.Composite((MagickImage)arguments["image"], (CompositeOperator)arguments["compose"], (String)arguments["args"]);
      else if (OnlyContains(arguments, "image", "gravity"))
        image.Composite((MagickImage)arguments["image"], (Gravity)arguments["gravity"]);
      else if (OnlyContains(arguments, "image", "gravity", "compose"))
        image.Composite((MagickImage)arguments["image"], (Gravity)arguments["gravity"], (CompositeOperator)arguments["compose"]);
      else if (OnlyContains(arguments, "image", "gravity", "compose", "args"))
        image.Composite((MagickImage)arguments["image"], (Gravity)arguments["gravity"], (CompositeOperator)arguments["compose"], (String)arguments["args"]);
      else if (OnlyContains(arguments, "image", "offset"))
        image.Composite((MagickImage)arguments["image"], (MagickGeometry)arguments["offset"]);
      else if (OnlyContains(arguments, "image", "offset", "compose"))
        image.Composite((MagickImage)arguments["image"], (MagickGeometry)arguments["offset"], (CompositeOperator)arguments["compose"]);
      else if (OnlyContains(arguments, "image", "offset", "compose", "args"))
        image.Composite((MagickImage)arguments["image"], (MagickGeometry)arguments["offset"], (CompositeOperator)arguments["compose"], (String)arguments["args"]);
      else if (OnlyContains(arguments, "image", "x", "y"))
        image.Composite((MagickImage)arguments["image"], (Int32)arguments["x"], (Int32)arguments["y"]);
      else if (OnlyContains(arguments, "image", "x", "y", "compose"))
        image.Composite((MagickImage)arguments["image"], (Int32)arguments["x"], (Int32)arguments["y"], (CompositeOperator)arguments["compose"]);
      else if (OnlyContains(arguments, "image", "x", "y", "compose", "args"))
        image.Composite((MagickImage)arguments["image"], (Int32)arguments["x"], (Int32)arguments["y"], (CompositeOperator)arguments["compose"], (String)arguments["args"]);
      else
        throw new ArgumentException("Invalid argument combination for 'composite', allowed combinations are: [image, compose] [image, compose, args] [image, gravity] [image, gravity, compose] [image, gravity, compose, args] [image, offset] [image, offset, compose] [image, offset, compose, args] [image, x, y] [image, x, y, compose] [image, x, y, compose, args]");
    }

    private void ExecuteConnectedComponents(XmlElement element, MagickImage image)
    {
      Int32 connectivity_ = Variables.GetValue<Int32>(element, "connectivity");
      image.ConnectedComponents(connectivity_);
    }

    private void ExecuteContrast(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        arguments[attribute.Name] = Variables.GetValue<Boolean>(attribute);
      }
      if (arguments.Count == 0)
        image.Contrast();
      else if (OnlyContains(arguments, "enhance"))
        image.Contrast((Boolean)arguments["enhance"]);
      else
        throw new ArgumentException("Invalid argument combination for 'contrast', allowed combinations are: [] [enhance]");
    }

    private void ExecuteContrastStretch(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        if (attribute.Name == "blackPoint")
          arguments["blackPoint"] = Variables.GetValue<Percentage>(attribute);
        else if (attribute.Name == "channels")
          arguments["channels"] = Variables.GetValue<Channels>(attribute);
        else if (attribute.Name == "whitePoint")
          arguments["whitePoint"] = Variables.GetValue<Percentage>(attribute);
      }
      if (OnlyContains(arguments, "blackPoint"))
        image.ContrastStretch((Percentage)arguments["blackPoint"]);
      else if (OnlyContains(arguments, "blackPoint", "whitePoint"))
        image.ContrastStretch((Percentage)arguments["blackPoint"], (Percentage)arguments["whitePoint"]);
      else if (OnlyContains(arguments, "blackPoint", "whitePoint", "channels"))
        image.ContrastStretch((Percentage)arguments["blackPoint"], (Percentage)arguments["whitePoint"], (Channels)arguments["channels"]);
      else
        throw new ArgumentException("Invalid argument combination for 'contrastStretch', allowed combinations are: [blackPoint] [blackPoint, whitePoint] [blackPoint, whitePoint, channels]");
    }

    private void ExecuteCopyPixels(XmlElement element, MagickImage image)
    {
      MagickImage source_ = CreateMagickImage(element["source"]);
      MagickGeometry geometry_ = Variables.GetValue<MagickGeometry>(element, "geometry");
      Coordinate offset_ = CreateCoordinate(element["offset"]);
      image.CopyPixels(source_, geometry_, offset_);
    }

    private void ExecuteCrop(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        if (attribute.Name == "geometry")
          arguments["geometry"] = Variables.GetValue<MagickGeometry>(attribute);
        else if (attribute.Name == "gravity")
          arguments["gravity"] = Variables.GetValue<Gravity>(attribute);
        else if (attribute.Name == "height")
          arguments["height"] = Variables.GetValue<Int32>(attribute);
        else if (attribute.Name == "width")
          arguments["width"] = Variables.GetValue<Int32>(attribute);
        else if (attribute.Name == "x")
          arguments["x"] = Variables.GetValue<Int32>(attribute);
        else if (attribute.Name == "y")
          arguments["y"] = Variables.GetValue<Int32>(attribute);
      }
      if (OnlyContains(arguments, "geometry"))
        image.Crop((MagickGeometry)arguments["geometry"]);
      else if (OnlyContains(arguments, "width", "height"))
        image.Crop((Int32)arguments["width"], (Int32)arguments["height"]);
      else if (OnlyContains(arguments, "width", "height", "gravity"))
        image.Crop((Int32)arguments["width"], (Int32)arguments["height"], (Gravity)arguments["gravity"]);
      else if (OnlyContains(arguments, "x", "y", "width", "height"))
        image.Crop((Int32)arguments["x"], (Int32)arguments["y"], (Int32)arguments["width"], (Int32)arguments["height"]);
      else
        throw new ArgumentException("Invalid argument combination for 'crop', allowed combinations are: [geometry] [width, height] [width, height, gravity] [x, y, width, height]");
    }

    private void ExecuteCycleColormap(XmlElement element, MagickImage image)
    {
      Int32 amount_ = Variables.GetValue<Int32>(element, "amount");
      image.CycleColormap(amount_);
    }

    private void ExecuteDecipher(XmlElement element, MagickImage image)
    {
      String passphrase_ = Variables.GetValue<String>(element, "passphrase");
      image.Decipher(passphrase_);
    }

    private void ExecuteDeskew(XmlElement element, MagickImage image)
    {
      Percentage threshold_ = Variables.GetValue<Percentage>(element, "threshold");
      image.Deskew(threshold_);
    }

    private static void ExecuteDespeckle(MagickImage image)
    {
      image.Despeckle();
    }

    private void ExecuteDistort(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        if (attribute.Name == "bestfit")
          arguments["bestfit"] = Variables.GetValue<Boolean>(attribute);
        else if (attribute.Name == "method")
          arguments["method"] = Variables.GetValue<DistortMethod>(attribute);
      }
      foreach (XmlElement elem in element.SelectNodes("*"))
      {
        arguments[elem.Name] = Variables.GetDoubleArray(elem);
      }
      if (OnlyContains(arguments, "method", "arguments"))
        image.Distort((DistortMethod)arguments["method"], (Double[])arguments["arguments"]);
      else if (OnlyContains(arguments, "method", "bestfit", "arguments"))
        image.Distort((DistortMethod)arguments["method"], (Boolean)arguments["bestfit"], (Double[])arguments["arguments"]);
      else
        throw new ArgumentException("Invalid argument combination for 'distort', allowed combinations are: [method, arguments] [method, bestfit, arguments]");
    }

    private void ExecuteEdge(XmlElement element, MagickImage image)
    {
      double radius_ = Variables.GetValue<double>(element, "radius");
      image.Edge(radius_);
    }

    private void ExecuteEmboss(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        arguments[attribute.Name] = Variables.GetValue<double>(attribute);
      }
      if (arguments.Count == 0)
        image.Emboss();
      else if (OnlyContains(arguments, "radius", "sigma"))
        image.Emboss((double)arguments["radius"], (double)arguments["sigma"]);
      else
        throw new ArgumentException("Invalid argument combination for 'emboss', allowed combinations are: [] [radius, sigma]");
    }

    private void ExecuteEncipher(XmlElement element, MagickImage image)
    {
      String passphrase_ = Variables.GetValue<String>(element, "passphrase");
      image.Encipher(passphrase_);
    }

    private static void ExecuteEnhance(MagickImage image)
    {
      image.Enhance();
    }

    private static void ExecuteEqualize(MagickImage image)
    {
      image.Equalize();
    }

    private void ExecuteEvaluate(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        if (attribute.Name == "channels")
          arguments["channels"] = Variables.GetValue<Channels>(attribute);
        else if (attribute.Name == "evaluateFunction")
          arguments["evaluateFunction"] = Variables.GetValue<EvaluateFunction>(attribute);
        else if (attribute.Name == "evaluateOperator")
          arguments["evaluateOperator"] = Variables.GetValue<EvaluateOperator>(attribute);
        else if (attribute.Name == "geometry")
          arguments["geometry"] = Variables.GetValue<MagickGeometry>(attribute);
        else if (attribute.Name == "percentage")
          arguments["percentage"] = Variables.GetValue<Percentage>(attribute);
        else if (attribute.Name == "value")
          arguments["value"] = Variables.GetValue<double>(attribute);
      }
      foreach (XmlElement elem in element.SelectNodes("*"))
      {
        arguments[elem.Name] = Variables.GetDoubleArray(elem);
      }
      if (OnlyContains(arguments, "channels", "evaluateFunction", "arguments"))
        image.Evaluate((Channels)arguments["channels"], (EvaluateFunction)arguments["evaluateFunction"], (Double[])arguments["arguments"]);
      else if (OnlyContains(arguments, "channels", "evaluateOperator", "percentage"))
        image.Evaluate((Channels)arguments["channels"], (EvaluateOperator)arguments["evaluateOperator"], (Percentage)arguments["percentage"]);
      else if (OnlyContains(arguments, "channels", "evaluateOperator", "value"))
        image.Evaluate((Channels)arguments["channels"], (EvaluateOperator)arguments["evaluateOperator"], (double)arguments["value"]);
      else if (OnlyContains(arguments, "channels", "geometry", "evaluateOperator", "percentage"))
        image.Evaluate((Channels)arguments["channels"], (MagickGeometry)arguments["geometry"], (EvaluateOperator)arguments["evaluateOperator"], (Percentage)arguments["percentage"]);
      else if (OnlyContains(arguments, "channels", "geometry", "evaluateOperator", "value"))
        image.Evaluate((Channels)arguments["channels"], (MagickGeometry)arguments["geometry"], (EvaluateOperator)arguments["evaluateOperator"], (double)arguments["value"]);
      else
        throw new ArgumentException("Invalid argument combination for 'evaluate', allowed combinations are: [channels, evaluateFunction, arguments] [channels, evaluateOperator, percentage] [channels, evaluateOperator, value] [channels, geometry, evaluateOperator, percentage] [channels, geometry, evaluateOperator, value]");
    }

    private void ExecuteExtent(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        if (attribute.Name == "backgroundColor")
          arguments["backgroundColor"] = Variables.GetValue<MagickColor>(attribute);
        else if (attribute.Name == "geometry")
          arguments["geometry"] = Variables.GetValue<MagickGeometry>(attribute);
        else if (attribute.Name == "gravity")
          arguments["gravity"] = Variables.GetValue<Gravity>(attribute);
        else if (attribute.Name == "height")
          arguments["height"] = Variables.GetValue<Int32>(attribute);
        else if (attribute.Name == "width")
          arguments["width"] = Variables.GetValue<Int32>(attribute);
        else if (attribute.Name == "x")
          arguments["x"] = Variables.GetValue<Int32>(attribute);
        else if (attribute.Name == "y")
          arguments["y"] = Variables.GetValue<Int32>(attribute);
      }
      if (OnlyContains(arguments, "geometry"))
        image.Extent((MagickGeometry)arguments["geometry"]);
      else if (OnlyContains(arguments, "geometry", "backgroundColor"))
        image.Extent((MagickGeometry)arguments["geometry"], (MagickColor)arguments["backgroundColor"]);
      else if (OnlyContains(arguments, "geometry", "gravity"))
        image.Extent((MagickGeometry)arguments["geometry"], (Gravity)arguments["gravity"]);
      else if (OnlyContains(arguments, "geometry", "gravity", "backgroundColor"))
        image.Extent((MagickGeometry)arguments["geometry"], (Gravity)arguments["gravity"], (MagickColor)arguments["backgroundColor"]);
      else if (OnlyContains(arguments, "width", "height"))
        image.Extent((Int32)arguments["width"], (Int32)arguments["height"]);
      else if (OnlyContains(arguments, "width", "height", "backgroundColor"))
        image.Extent((Int32)arguments["width"], (Int32)arguments["height"], (MagickColor)arguments["backgroundColor"]);
      else if (OnlyContains(arguments, "width", "height", "gravity"))
        image.Extent((Int32)arguments["width"], (Int32)arguments["height"], (Gravity)arguments["gravity"]);
      else if (OnlyContains(arguments, "width", "height", "gravity", "backgroundColor"))
        image.Extent((Int32)arguments["width"], (Int32)arguments["height"], (Gravity)arguments["gravity"], (MagickColor)arguments["backgroundColor"]);
      else if (OnlyContains(arguments, "x", "y", "width", "height"))
        image.Extent((Int32)arguments["x"], (Int32)arguments["y"], (Int32)arguments["width"], (Int32)arguments["height"]);
      else
        throw new ArgumentException("Invalid argument combination for 'extent', allowed combinations are: [geometry] [geometry, backgroundColor] [geometry, gravity] [geometry, gravity, backgroundColor] [width, height] [width, height, backgroundColor] [width, height, gravity] [width, height, gravity, backgroundColor] [x, y, width, height]");
    }

    private static void ExecuteFlip(MagickImage image)
    {
      image.Flip();
    }

    private void ExecuteFloodFill(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        if (attribute.Name == "alpha")
          arguments["alpha"] = Variables.GetValue<Int32>(attribute);
        else if (attribute.Name == "borderColor")
          arguments["borderColor"] = Variables.GetValue<MagickColor>(attribute);
        else if (attribute.Name == "color")
          arguments["color"] = Variables.GetValue<MagickColor>(attribute);
        else if (attribute.Name == "geometry")
          arguments["geometry"] = Variables.GetValue<MagickGeometry>(attribute);
        else if (attribute.Name == "x")
          arguments["x"] = Variables.GetValue<Int32>(attribute);
        else if (attribute.Name == "y")
          arguments["y"] = Variables.GetValue<Int32>(attribute);
      }
      foreach (XmlElement elem in element.SelectNodes("*"))
      {
        arguments[elem.Name] = CreateMagickImage(elem);
      }
      if (OnlyContains(arguments, "alpha", "x", "y"))
        image.FloodFill((Int32)arguments["alpha"], (Int32)arguments["x"], (Int32)arguments["y"]);
      else if (OnlyContains(arguments, "color", "geometry"))
        image.FloodFill((MagickColor)arguments["color"], (MagickGeometry)arguments["geometry"]);
      else if (OnlyContains(arguments, "color", "geometry", "borderColor"))
        image.FloodFill((MagickColor)arguments["color"], (MagickGeometry)arguments["geometry"], (MagickColor)arguments["borderColor"]);
      else if (OnlyContains(arguments, "color", "x", "y"))
        image.FloodFill((MagickColor)arguments["color"], (Int32)arguments["x"], (Int32)arguments["y"]);
      else if (OnlyContains(arguments, "color", "x", "y", "borderColor"))
        image.FloodFill((MagickColor)arguments["color"], (Int32)arguments["x"], (Int32)arguments["y"], (MagickColor)arguments["borderColor"]);
      else if (OnlyContains(arguments, "image", "geometry"))
        image.FloodFill((MagickImage)arguments["image"], (MagickGeometry)arguments["geometry"]);
      else if (OnlyContains(arguments, "image", "geometry", "borderColor"))
        image.FloodFill((MagickImage)arguments["image"], (MagickGeometry)arguments["geometry"], (MagickColor)arguments["borderColor"]);
      else if (OnlyContains(arguments, "image", "x", "y"))
        image.FloodFill((MagickImage)arguments["image"], (Int32)arguments["x"], (Int32)arguments["y"]);
      else if (OnlyContains(arguments, "image", "x", "y", "borderColor"))
        image.FloodFill((MagickImage)arguments["image"], (Int32)arguments["x"], (Int32)arguments["y"], (MagickColor)arguments["borderColor"]);
      else
        throw new ArgumentException("Invalid argument combination for 'floodFill', allowed combinations are: [alpha, x, y] [color, geometry] [color, geometry, borderColor] [color, x, y] [color, x, y, borderColor] [image, geometry] [image, geometry, borderColor] [image, x, y] [image, x, y, borderColor]");
    }

    private static void ExecuteFlop(MagickImage image)
    {
      image.Flop();
    }

    private void ExecuteFrame(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        if (attribute.Name == "geometry")
          arguments["geometry"] = Variables.GetValue<MagickGeometry>(attribute);
        else if (attribute.Name == "height")
          arguments["height"] = Variables.GetValue<Int32>(attribute);
        else if (attribute.Name == "innerBevel")
          arguments["innerBevel"] = Variables.GetValue<Int32>(attribute);
        else if (attribute.Name == "outerBevel")
          arguments["outerBevel"] = Variables.GetValue<Int32>(attribute);
        else if (attribute.Name == "width")
          arguments["width"] = Variables.GetValue<Int32>(attribute);
      }
      if (arguments.Count == 0)
        image.Frame();
      else if (OnlyContains(arguments, "geometry"))
        image.Frame((MagickGeometry)arguments["geometry"]);
      else if (OnlyContains(arguments, "width", "height"))
        image.Frame((Int32)arguments["width"], (Int32)arguments["height"]);
      else if (OnlyContains(arguments, "width", "height", "innerBevel", "outerBevel"))
        image.Frame((Int32)arguments["width"], (Int32)arguments["height"], (Int32)arguments["innerBevel"], (Int32)arguments["outerBevel"]);
      else
        throw new ArgumentException("Invalid argument combination for 'frame', allowed combinations are: [] [geometry] [width, height] [width, height, innerBevel, outerBevel]");
    }

    private void ExecuteFx(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        if (attribute.Name == "channels")
          arguments["channels"] = Variables.GetValue<Channels>(attribute);
        else if (attribute.Name == "expression")
          arguments["expression"] = Variables.GetValue<String>(attribute);
      }
      if (OnlyContains(arguments, "expression"))
        image.Fx((String)arguments["expression"]);
      else if (OnlyContains(arguments, "expression", "channels"))
        image.Fx((String)arguments["expression"], (Channels)arguments["channels"]);
      else
        throw new ArgumentException("Invalid argument combination for 'fx', allowed combinations are: [expression] [expression, channels]");
    }

    private void ExecuteGammaCorrect(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        arguments[attribute.Name] = Variables.GetValue<double>(attribute);
      }
      if (OnlyContains(arguments, "gamma"))
        image.GammaCorrect((double)arguments["gamma"]);
      else if (OnlyContains(arguments, "gammaRed", "gammaGreen", "gammaBlue"))
        image.GammaCorrect((double)arguments["gammaRed"], (double)arguments["gammaGreen"], (double)arguments["gammaBlue"]);
      else
        throw new ArgumentException("Invalid argument combination for 'gammaCorrect', allowed combinations are: [gamma] [gammaRed, gammaGreen, gammaBlue]");
    }

    private void ExecuteGaussianBlur(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        if (attribute.Name == "channels")
          arguments["channels"] = Variables.GetValue<Channels>(attribute);
        else if (attribute.Name == "sigma")
          arguments["sigma"] = Variables.GetValue<double>(attribute);
        else if (attribute.Name == "width")
          arguments["width"] = Variables.GetValue<double>(attribute);
      }
      if (OnlyContains(arguments, "width", "sigma"))
        image.GaussianBlur((double)arguments["width"], (double)arguments["sigma"]);
      else if (OnlyContains(arguments, "width", "sigma", "channels"))
        image.GaussianBlur((double)arguments["width"], (double)arguments["sigma"], (Channels)arguments["channels"]);
      else
        throw new ArgumentException("Invalid argument combination for 'gaussianBlur', allowed combinations are: [width, sigma] [width, sigma, channels]");
    }

    private void ExecuteGrayscale(XmlElement element, MagickImage image)
    {
      PixelIntensityMethod method_ = Variables.GetValue<PixelIntensityMethod>(element, "method");
      image.Grayscale(method_);
    }

    private void ExecuteHaldClut(XmlElement element, MagickImage image)
    {
      MagickImage image_ = CreateMagickImage(element["image"]);
      image.HaldClut(image_);
    }

    private void ExecuteHoughLine(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        arguments[attribute.Name] = Variables.GetValue<Int32>(attribute);
      }
      if (arguments.Count == 0)
        image.HoughLine();
      else if (OnlyContains(arguments, "width", "height", "threshold"))
        image.HoughLine((Int32)arguments["width"], (Int32)arguments["height"], (Int32)arguments["threshold"]);
      else
        throw new ArgumentException("Invalid argument combination for 'houghLine', allowed combinations are: [] [width, height, threshold]");
    }

    private void ExecuteImplode(XmlElement element, MagickImage image)
    {
      double factor_ = Variables.GetValue<double>(element, "factor");
      image.Implode(factor_);
    }

    private void ExecuteInverseFloodFill(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        if (attribute.Name == "alpha")
          arguments["alpha"] = Variables.GetValue<Int32>(attribute);
        else if (attribute.Name == "borderColor")
          arguments["borderColor"] = Variables.GetValue<MagickColor>(attribute);
        else if (attribute.Name == "color")
          arguments["color"] = Variables.GetValue<MagickColor>(attribute);
        else if (attribute.Name == "geometry")
          arguments["geometry"] = Variables.GetValue<MagickGeometry>(attribute);
        else if (attribute.Name == "x")
          arguments["x"] = Variables.GetValue<Int32>(attribute);
        else if (attribute.Name == "y")
          arguments["y"] = Variables.GetValue<Int32>(attribute);
      }
      foreach (XmlElement elem in element.SelectNodes("*"))
      {
        arguments[elem.Name] = CreateMagickImage(elem);
      }
      if (OnlyContains(arguments, "alpha", "x", "y"))
        image.InverseFloodFill((Int32)arguments["alpha"], (Int32)arguments["x"], (Int32)arguments["y"]);
      else if (OnlyContains(arguments, "color", "geometry"))
        image.InverseFloodFill((MagickColor)arguments["color"], (MagickGeometry)arguments["geometry"]);
      else if (OnlyContains(arguments, "color", "geometry", "borderColor"))
        image.InverseFloodFill((MagickColor)arguments["color"], (MagickGeometry)arguments["geometry"], (MagickColor)arguments["borderColor"]);
      else if (OnlyContains(arguments, "color", "x", "y"))
        image.InverseFloodFill((MagickColor)arguments["color"], (Int32)arguments["x"], (Int32)arguments["y"]);
      else if (OnlyContains(arguments, "color", "x", "y", "borderColor"))
        image.InverseFloodFill((MagickColor)arguments["color"], (Int32)arguments["x"], (Int32)arguments["y"], (MagickColor)arguments["borderColor"]);
      else if (OnlyContains(arguments, "image", "geometry"))
        image.InverseFloodFill((MagickImage)arguments["image"], (MagickGeometry)arguments["geometry"]);
      else if (OnlyContains(arguments, "image", "geometry", "borderColor"))
        image.InverseFloodFill((MagickImage)arguments["image"], (MagickGeometry)arguments["geometry"], (MagickColor)arguments["borderColor"]);
      else if (OnlyContains(arguments, "image", "x", "y"))
        image.InverseFloodFill((MagickImage)arguments["image"], (Int32)arguments["x"], (Int32)arguments["y"]);
      else if (OnlyContains(arguments, "image", "x", "y", "borderColor"))
        image.InverseFloodFill((MagickImage)arguments["image"], (Int32)arguments["x"], (Int32)arguments["y"], (MagickColor)arguments["borderColor"]);
      else
        throw new ArgumentException("Invalid argument combination for 'inverseFloodFill', allowed combinations are: [alpha, x, y] [color, geometry] [color, geometry, borderColor] [color, x, y] [color, x, y, borderColor] [image, geometry] [image, geometry, borderColor] [image, x, y] [image, x, y, borderColor]");
    }

    private void ExecuteInverseFourierTransform(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        arguments[attribute.Name] = Variables.GetValue<Boolean>(attribute);
      }
      foreach (XmlElement elem in element.SelectNodes("*"))
      {
        arguments[elem.Name] = CreateMagickImage(elem);
      }
      if (OnlyContains(arguments, "image"))
        image.InverseFourierTransform((MagickImage)arguments["image"]);
      else if (OnlyContains(arguments, "image", "magnitude"))
        image.InverseFourierTransform((MagickImage)arguments["image"], (Boolean)arguments["magnitude"]);
      else
        throw new ArgumentException("Invalid argument combination for 'inverseFourierTransform', allowed combinations are: [image] [image, magnitude]");
    }

    private void ExecuteInverseLevel(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        if (attribute.Name == "blackPoint")
          arguments["blackPoint"] = Variables.GetValue<QuantumType>(attribute);
        else if (attribute.Name == "blackPointPercentage")
          arguments["blackPointPercentage"] = Variables.GetValue<Percentage>(attribute);
        else if (attribute.Name == "channels")
          arguments["channels"] = Variables.GetValue<Channels>(attribute);
        else if (attribute.Name == "midpoint")
          arguments["midpoint"] = Variables.GetValue<double>(attribute);
        else if (attribute.Name == "whitePoint")
          arguments["whitePoint"] = Variables.GetValue<QuantumType>(attribute);
        else if (attribute.Name == "whitePointPercentage")
          arguments["whitePointPercentage"] = Variables.GetValue<Percentage>(attribute);
      }
      if (OnlyContains(arguments, "blackPoint", "whitePoint"))
        image.InverseLevel((QuantumType)arguments["blackPoint"], (QuantumType)arguments["whitePoint"]);
      else if (OnlyContains(arguments, "blackPoint", "whitePoint", "channels"))
        image.InverseLevel((QuantumType)arguments["blackPoint"], (QuantumType)arguments["whitePoint"], (Channels)arguments["channels"]);
      else if (OnlyContains(arguments, "blackPoint", "whitePoint", "midpoint"))
        image.InverseLevel((QuantumType)arguments["blackPoint"], (QuantumType)arguments["whitePoint"], (double)arguments["midpoint"]);
      else if (OnlyContains(arguments, "blackPoint", "whitePoint", "midpoint", "channels"))
        image.InverseLevel((QuantumType)arguments["blackPoint"], (QuantumType)arguments["whitePoint"], (double)arguments["midpoint"], (Channels)arguments["channels"]);
      else if (OnlyContains(arguments, "blackPointPercentage", "whitePointPercentage"))
        image.InverseLevel((Percentage)arguments["blackPointPercentage"], (Percentage)arguments["whitePointPercentage"]);
      else if (OnlyContains(arguments, "blackPointPercentage", "whitePointPercentage", "channels"))
        image.InverseLevel((Percentage)arguments["blackPointPercentage"], (Percentage)arguments["whitePointPercentage"], (Channels)arguments["channels"]);
      else if (OnlyContains(arguments, "blackPointPercentage", "whitePointPercentage", "midpoint"))
        image.InverseLevel((Percentage)arguments["blackPointPercentage"], (Percentage)arguments["whitePointPercentage"], (double)arguments["midpoint"]);
      else if (OnlyContains(arguments, "blackPointPercentage", "whitePointPercentage", "midpoint", "channels"))
        image.InverseLevel((Percentage)arguments["blackPointPercentage"], (Percentage)arguments["whitePointPercentage"], (double)arguments["midpoint"], (Channels)arguments["channels"]);
      else
        throw new ArgumentException("Invalid argument combination for 'inverseLevel', allowed combinations are: [blackPoint, whitePoint] [blackPoint, whitePoint, channels] [blackPoint, whitePoint, midpoint] [blackPoint, whitePoint, midpoint, channels] [blackPointPercentage, whitePointPercentage] [blackPointPercentage, whitePointPercentage, channels] [blackPointPercentage, whitePointPercentage, midpoint] [blackPointPercentage, whitePointPercentage, midpoint, channels]");
    }

    private void ExecuteInverseLevelColors(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        if (attribute.Name == "blackColor")
          arguments["blackColor"] = Variables.GetValue<MagickColor>(attribute);
        else if (attribute.Name == "channels")
          arguments["channels"] = Variables.GetValue<Channels>(attribute);
        else if (attribute.Name == "whiteColor")
          arguments["whiteColor"] = Variables.GetValue<MagickColor>(attribute);
      }
      if (OnlyContains(arguments, "blackColor", "whiteColor"))
        image.InverseLevelColors((MagickColor)arguments["blackColor"], (MagickColor)arguments["whiteColor"]);
      else if (OnlyContains(arguments, "blackColor", "whiteColor", "channels"))
        image.InverseLevelColors((MagickColor)arguments["blackColor"], (MagickColor)arguments["whiteColor"], (Channels)arguments["channels"]);
      else
        throw new ArgumentException("Invalid argument combination for 'inverseLevelColors', allowed combinations are: [blackColor, whiteColor] [blackColor, whiteColor, channels]");
    }

    private void ExecuteInverseOpaque(XmlElement element, MagickImage image)
    {
      MagickColor target_ = Variables.GetValue<MagickColor>(element, "target");
      MagickColor fill_ = Variables.GetValue<MagickColor>(element, "fill");
      image.InverseOpaque(target_, fill_);
    }

    private void ExecuteInverseTransparent(XmlElement element, MagickImage image)
    {
      MagickColor color_ = Variables.GetValue<MagickColor>(element, "color");
      image.InverseTransparent(color_);
    }

    private void ExecuteKuwahara(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        arguments[attribute.Name] = Variables.GetValue<double>(attribute);
      }
      if (arguments.Count == 0)
        image.Kuwahara();
      else if (OnlyContains(arguments, "radius", "sigma"))
        image.Kuwahara((double)arguments["radius"], (double)arguments["sigma"]);
      else
        throw new ArgumentException("Invalid argument combination for 'kuwahara', allowed combinations are: [] [radius, sigma]");
    }

    private void ExecuteLevel(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        if (attribute.Name == "blackPoint")
          arguments["blackPoint"] = Variables.GetValue<QuantumType>(attribute);
        else if (attribute.Name == "blackPointPercentage")
          arguments["blackPointPercentage"] = Variables.GetValue<Percentage>(attribute);
        else if (attribute.Name == "channels")
          arguments["channels"] = Variables.GetValue<Channels>(attribute);
        else if (attribute.Name == "midpoint")
          arguments["midpoint"] = Variables.GetValue<double>(attribute);
        else if (attribute.Name == "whitePoint")
          arguments["whitePoint"] = Variables.GetValue<QuantumType>(attribute);
        else if (attribute.Name == "whitePointPercentage")
          arguments["whitePointPercentage"] = Variables.GetValue<Percentage>(attribute);
      }
      if (OnlyContains(arguments, "blackPoint", "whitePoint"))
        image.Level((QuantumType)arguments["blackPoint"], (QuantumType)arguments["whitePoint"]);
      else if (OnlyContains(arguments, "blackPoint", "whitePoint", "channels"))
        image.Level((QuantumType)arguments["blackPoint"], (QuantumType)arguments["whitePoint"], (Channels)arguments["channels"]);
      else if (OnlyContains(arguments, "blackPoint", "whitePoint", "midpoint"))
        image.Level((QuantumType)arguments["blackPoint"], (QuantumType)arguments["whitePoint"], (double)arguments["midpoint"]);
      else if (OnlyContains(arguments, "blackPoint", "whitePoint", "midpoint", "channels"))
        image.Level((QuantumType)arguments["blackPoint"], (QuantumType)arguments["whitePoint"], (double)arguments["midpoint"], (Channels)arguments["channels"]);
      else if (OnlyContains(arguments, "blackPointPercentage", "whitePointPercentage"))
        image.Level((Percentage)arguments["blackPointPercentage"], (Percentage)arguments["whitePointPercentage"]);
      else if (OnlyContains(arguments, "blackPointPercentage", "whitePointPercentage", "channels"))
        image.Level((Percentage)arguments["blackPointPercentage"], (Percentage)arguments["whitePointPercentage"], (Channels)arguments["channels"]);
      else if (OnlyContains(arguments, "blackPointPercentage", "whitePointPercentage", "midpoint"))
        image.Level((Percentage)arguments["blackPointPercentage"], (Percentage)arguments["whitePointPercentage"], (double)arguments["midpoint"]);
      else if (OnlyContains(arguments, "blackPointPercentage", "whitePointPercentage", "midpoint", "channels"))
        image.Level((Percentage)arguments["blackPointPercentage"], (Percentage)arguments["whitePointPercentage"], (double)arguments["midpoint"], (Channels)arguments["channels"]);
      else
        throw new ArgumentException("Invalid argument combination for 'level', allowed combinations are: [blackPoint, whitePoint] [blackPoint, whitePoint, channels] [blackPoint, whitePoint, midpoint] [blackPoint, whitePoint, midpoint, channels] [blackPointPercentage, whitePointPercentage] [blackPointPercentage, whitePointPercentage, channels] [blackPointPercentage, whitePointPercentage, midpoint] [blackPointPercentage, whitePointPercentage, midpoint, channels]");
    }

    private void ExecuteLevelColors(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        if (attribute.Name == "blackColor")
          arguments["blackColor"] = Variables.GetValue<MagickColor>(attribute);
        else if (attribute.Name == "channels")
          arguments["channels"] = Variables.GetValue<Channels>(attribute);
        else if (attribute.Name == "whiteColor")
          arguments["whiteColor"] = Variables.GetValue<MagickColor>(attribute);
      }
      if (OnlyContains(arguments, "blackColor", "whiteColor"))
        image.LevelColors((MagickColor)arguments["blackColor"], (MagickColor)arguments["whiteColor"]);
      else if (OnlyContains(arguments, "blackColor", "whiteColor", "channels"))
        image.LevelColors((MagickColor)arguments["blackColor"], (MagickColor)arguments["whiteColor"], (Channels)arguments["channels"]);
      else
        throw new ArgumentException("Invalid argument combination for 'levelColors', allowed combinations are: [blackColor, whiteColor] [blackColor, whiteColor, channels]");
    }

    private void ExecuteLinearStretch(XmlElement element, MagickImage image)
    {
      Percentage blackPoint_ = Variables.GetValue<Percentage>(element, "blackPoint");
      Percentage whitePoint_ = Variables.GetValue<Percentage>(element, "whitePoint");
      image.LinearStretch(blackPoint_, whitePoint_);
    }

    private void ExecuteLiquidRescale(XmlElement element, MagickImage image)
    {
      MagickGeometry geometry_ = Variables.GetValue<MagickGeometry>(element, "geometry");
      image.LiquidRescale(geometry_);
    }

    private void ExecuteLocalContrast(XmlElement element, MagickImage image)
    {
      double radius_ = Variables.GetValue<double>(element, "radius");
      Percentage strength_ = Variables.GetValue<Percentage>(element, "strength");
      image.LocalContrast(radius_, strength_);
    }

    private void ExecuteLower(XmlElement element, MagickImage image)
    {
      Int32 size_ = Variables.GetValue<Int32>(element, "size");
      image.Lower(size_);
    }

    private static void ExecuteMagnify(MagickImage image)
    {
      image.Magnify();
    }

    private void ExecuteMedianFilter(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        arguments[attribute.Name] = Variables.GetValue<double>(attribute);
      }
      if (arguments.Count == 0)
        image.MedianFilter();
      else if (OnlyContains(arguments, "radius"))
        image.MedianFilter((double)arguments["radius"]);
      else
        throw new ArgumentException("Invalid argument combination for 'medianFilter', allowed combinations are: [] [radius]");
    }

    private static void ExecuteMinify(MagickImage image)
    {
      image.Minify();
    }

    private void ExecuteModulate(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        arguments[attribute.Name] = Variables.GetValue<Percentage>(attribute);
      }
      if (OnlyContains(arguments, "brightness"))
        image.Modulate((Percentage)arguments["brightness"]);
      else if (OnlyContains(arguments, "brightness", "saturation"))
        image.Modulate((Percentage)arguments["brightness"], (Percentage)arguments["saturation"]);
      else if (OnlyContains(arguments, "brightness", "saturation", "hue"))
        image.Modulate((Percentage)arguments["brightness"], (Percentage)arguments["saturation"], (Percentage)arguments["hue"]);
      else
        throw new ArgumentException("Invalid argument combination for 'modulate', allowed combinations are: [brightness] [brightness, saturation] [brightness, saturation, hue]");
    }

    private void ExecuteMorphology(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        if (attribute.Name == "arguments")
          arguments["arguments"] = Variables.GetValue<String>(attribute);
        else if (attribute.Name == "channels")
          arguments["channels"] = Variables.GetValue<Channels>(attribute);
        else if (attribute.Name == "iterations")
          arguments["iterations"] = Variables.GetValue<Int32>(attribute);
        else if (attribute.Name == "kernel")
          arguments["kernel"] = Variables.GetValue<Kernel>(attribute);
        else if (attribute.Name == "method")
          arguments["method"] = Variables.GetValue<MorphologyMethod>(attribute);
        else if (attribute.Name == "userKernel")
          arguments["userKernel"] = Variables.GetValue<String>(attribute);
      }
      if (OnlyContains(arguments, "method", "kernel"))
        image.Morphology((MorphologyMethod)arguments["method"], (Kernel)arguments["kernel"]);
      else if (OnlyContains(arguments, "method", "kernel", "arguments"))
        image.Morphology((MorphologyMethod)arguments["method"], (Kernel)arguments["kernel"], (String)arguments["arguments"]);
      else if (OnlyContains(arguments, "method", "kernel", "arguments", "channels"))
        image.Morphology((MorphologyMethod)arguments["method"], (Kernel)arguments["kernel"], (String)arguments["arguments"], (Channels)arguments["channels"]);
      else if (OnlyContains(arguments, "method", "kernel", "arguments", "channels", "iterations"))
        image.Morphology((MorphologyMethod)arguments["method"], (Kernel)arguments["kernel"], (String)arguments["arguments"], (Channels)arguments["channels"], (Int32)arguments["iterations"]);
      else if (OnlyContains(arguments, "method", "kernel", "arguments", "iterations"))
        image.Morphology((MorphologyMethod)arguments["method"], (Kernel)arguments["kernel"], (String)arguments["arguments"], (Int32)arguments["iterations"]);
      else if (OnlyContains(arguments, "method", "kernel", "channels"))
        image.Morphology((MorphologyMethod)arguments["method"], (Kernel)arguments["kernel"], (Channels)arguments["channels"]);
      else if (OnlyContains(arguments, "method", "kernel", "channels", "iterations"))
        image.Morphology((MorphologyMethod)arguments["method"], (Kernel)arguments["kernel"], (Channels)arguments["channels"], (Int32)arguments["iterations"]);
      else if (OnlyContains(arguments, "method", "kernel", "iterations"))
        image.Morphology((MorphologyMethod)arguments["method"], (Kernel)arguments["kernel"], (Int32)arguments["iterations"]);
      else if (OnlyContains(arguments, "method", "userKernel"))
        image.Morphology((MorphologyMethod)arguments["method"], (String)arguments["userKernel"]);
      else if (OnlyContains(arguments, "method", "userKernel", "channels"))
        image.Morphology((MorphologyMethod)arguments["method"], (String)arguments["userKernel"], (Channels)arguments["channels"]);
      else if (OnlyContains(arguments, "method", "userKernel", "channels", "iterations"))
        image.Morphology((MorphologyMethod)arguments["method"], (String)arguments["userKernel"], (Channels)arguments["channels"], (Int32)arguments["iterations"]);
      else if (OnlyContains(arguments, "method", "userKernel", "iterations"))
        image.Morphology((MorphologyMethod)arguments["method"], (String)arguments["userKernel"], (Int32)arguments["iterations"]);
      else
        throw new ArgumentException("Invalid argument combination for 'morphology', allowed combinations are: [method, kernel] [method, kernel, arguments] [method, kernel, arguments, channels] [method, kernel, arguments, channels, iterations] [method, kernel, arguments, iterations] [method, kernel, channels] [method, kernel, channels, iterations] [method, kernel, iterations] [method, userKernel] [method, userKernel, channels] [method, userKernel, channels, iterations] [method, userKernel, iterations]");
    }

    private void ExecuteMotionBlur(XmlElement element, MagickImage image)
    {
      double radius_ = Variables.GetValue<double>(element, "radius");
      double sigma_ = Variables.GetValue<double>(element, "sigma");
      double angle_ = Variables.GetValue<double>(element, "angle");
      image.MotionBlur(radius_, sigma_, angle_);
    }

    private void ExecuteNegate(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        if (attribute.Name == "channels")
          arguments["channels"] = Variables.GetValue<Channels>(attribute);
        else if (attribute.Name == "onlyGrayscale")
          arguments["onlyGrayscale"] = Variables.GetValue<Boolean>(attribute);
      }
      if (arguments.Count == 0)
        image.Negate();
      else if (OnlyContains(arguments, "channels"))
        image.Negate((Channels)arguments["channels"]);
      else if (OnlyContains(arguments, "channels", "onlyGrayscale"))
        image.Negate((Channels)arguments["channels"], (Boolean)arguments["onlyGrayscale"]);
      else if (OnlyContains(arguments, "onlyGrayscale"))
        image.Negate((Boolean)arguments["onlyGrayscale"]);
      else
        throw new ArgumentException("Invalid argument combination for 'negate', allowed combinations are: [] [channels] [channels, onlyGrayscale] [onlyGrayscale]");
    }

    private static void ExecuteNormalize(MagickImage image)
    {
      image.Normalize();
    }

    private void ExecuteOilPaint(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        arguments[attribute.Name] = Variables.GetValue<double>(attribute);
      }
      if (arguments.Count == 0)
        image.OilPaint();
      else if (OnlyContains(arguments, "radius"))
        image.OilPaint((double)arguments["radius"]);
      else
        throw new ArgumentException("Invalid argument combination for 'oilPaint', allowed combinations are: [] [radius]");
    }

    private void ExecuteOpaque(XmlElement element, MagickImage image)
    {
      MagickColor target_ = Variables.GetValue<MagickColor>(element, "target");
      MagickColor fill_ = Variables.GetValue<MagickColor>(element, "fill");
      image.Opaque(target_, fill_);
    }

    private void ExecuteOrderedDither(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        if (attribute.Name == "channels")
          arguments["channels"] = Variables.GetValue<Channels>(attribute);
        else if (attribute.Name == "thresholdMap")
          arguments["thresholdMap"] = Variables.GetValue<String>(attribute);
      }
      if (OnlyContains(arguments, "thresholdMap"))
        image.OrderedDither((String)arguments["thresholdMap"]);
      else if (OnlyContains(arguments, "thresholdMap", "channels"))
        image.OrderedDither((String)arguments["thresholdMap"], (Channels)arguments["channels"]);
      else
        throw new ArgumentException("Invalid argument combination for 'orderedDither', allowed combinations are: [thresholdMap] [thresholdMap, channels]");
    }

    private void ExecutePerceptible(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        if (attribute.Name == "channels")
          arguments["channels"] = Variables.GetValue<Channels>(attribute);
        else if (attribute.Name == "epsilon")
          arguments["epsilon"] = Variables.GetValue<double>(attribute);
      }
      if (OnlyContains(arguments, "epsilon"))
        image.Perceptible((double)arguments["epsilon"]);
      else if (OnlyContains(arguments, "epsilon", "channels"))
        image.Perceptible((double)arguments["epsilon"], (Channels)arguments["channels"]);
      else
        throw new ArgumentException("Invalid argument combination for 'perceptible', allowed combinations are: [epsilon] [epsilon, channels]");
    }

    private void ExecutePolaroid(XmlElement element, MagickImage image)
    {
      String caption_ = Variables.GetValue<String>(element, "caption");
      double angle_ = Variables.GetValue<double>(element, "angle");
      PixelInterpolateMethod method_ = Variables.GetValue<PixelInterpolateMethod>(element, "method");
      image.Polaroid(caption_, angle_, method_);
    }

    private void ExecutePosterize(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        if (attribute.Name == "channels")
          arguments["channels"] = Variables.GetValue<Channels>(attribute);
        else if (attribute.Name == "levels")
          arguments["levels"] = Variables.GetValue<Int32>(attribute);
        else if (attribute.Name == "method")
          arguments["method"] = Variables.GetValue<DitherMethod>(attribute);
      }
      if (OnlyContains(arguments, "levels"))
        image.Posterize((Int32)arguments["levels"]);
      else if (OnlyContains(arguments, "levels", "channels"))
        image.Posterize((Int32)arguments["levels"], (Channels)arguments["channels"]);
      else if (OnlyContains(arguments, "levels", "method"))
        image.Posterize((Int32)arguments["levels"], (DitherMethod)arguments["method"]);
      else if (OnlyContains(arguments, "levels", "method", "channels"))
        image.Posterize((Int32)arguments["levels"], (DitherMethod)arguments["method"], (Channels)arguments["channels"]);
      else
        throw new ArgumentException("Invalid argument combination for 'posterize', allowed combinations are: [levels] [levels, channels] [levels, method] [levels, method, channels]");
    }

    private static void ExecutePreserveColorType(MagickImage image)
    {
      image.PreserveColorType();
    }

    private void ExecuteQuantize(XmlElement element, MagickImage image)
    {
      QuantizeSettings settings_ = CreateQuantizeSettings(element["settings"]);
      image.Quantize(settings_);
    }

    private void ExecuteRaise(XmlElement element, MagickImage image)
    {
      Int32 size_ = Variables.GetValue<Int32>(element, "size");
      image.Raise(size_);
    }

    private void ExecuteRandomThreshold(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        if (attribute.Name == "channels")
          arguments["channels"] = Variables.GetValue<Channels>(attribute);
        else if (attribute.Name == "high")
          arguments["high"] = Variables.GetValue<QuantumType>(attribute);
        else if (attribute.Name == "low")
          arguments["low"] = Variables.GetValue<QuantumType>(attribute);
        else if (attribute.Name == "percentageHigh")
          arguments["percentageHigh"] = Variables.GetValue<Percentage>(attribute);
        else if (attribute.Name == "percentageLow")
          arguments["percentageLow"] = Variables.GetValue<Percentage>(attribute);
      }
      if (OnlyContains(arguments, "low", "high"))
        image.RandomThreshold((QuantumType)arguments["low"], (QuantumType)arguments["high"]);
      else if (OnlyContains(arguments, "low", "high", "channels"))
        image.RandomThreshold((QuantumType)arguments["low"], (QuantumType)arguments["high"], (Channels)arguments["channels"]);
      else if (OnlyContains(arguments, "percentageLow", "percentageHigh"))
        image.RandomThreshold((Percentage)arguments["percentageLow"], (Percentage)arguments["percentageHigh"]);
      else if (OnlyContains(arguments, "percentageLow", "percentageHigh", "channels"))
        image.RandomThreshold((Percentage)arguments["percentageLow"], (Percentage)arguments["percentageHigh"], (Channels)arguments["channels"]);
      else
        throw new ArgumentException("Invalid argument combination for 'randomThreshold', allowed combinations are: [low, high] [low, high, channels] [percentageLow, percentageHigh] [percentageLow, percentageHigh, channels]");
    }

    private void ExecuteReduceNoise(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        arguments[attribute.Name] = Variables.GetValue<Int32>(attribute);
      }
      if (arguments.Count == 0)
        image.ReduceNoise();
      else if (OnlyContains(arguments, "order"))
        image.ReduceNoise((Int32)arguments["order"]);
      else
        throw new ArgumentException("Invalid argument combination for 'reduceNoise', allowed combinations are: [] [order]");
    }

    private void ExecuteRemoveDefine(XmlElement element, MagickImage image)
    {
      MagickFormat format_ = Variables.GetValue<MagickFormat>(element, "format");
      String name_ = Variables.GetValue<String>(element, "name");
      image.RemoveDefine(format_, name_);
    }

    private void ExecuteRemoveProfile(XmlElement element, MagickImage image)
    {
      String name_ = Variables.GetValue<String>(element, "name");
      image.RemoveProfile(name_);
    }

    private static void ExecuteRePage(MagickImage image)
    {
      image.RePage();
    }

    private void ExecuteResample(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        if (attribute.Name == "density")
          arguments["density"] = Variables.GetValue<PointD>(attribute);
        else if (attribute.Name == "resolutionX")
          arguments["resolutionX"] = Variables.GetValue<double>(attribute);
        else if (attribute.Name == "resolutionY")
          arguments["resolutionY"] = Variables.GetValue<double>(attribute);
      }
      if (OnlyContains(arguments, "density"))
        image.Resample((PointD)arguments["density"]);
      else if (OnlyContains(arguments, "resolutionX", "resolutionY"))
        image.Resample((double)arguments["resolutionX"], (double)arguments["resolutionY"]);
      else
        throw new ArgumentException("Invalid argument combination for 'resample', allowed combinations are: [density] [resolutionX, resolutionY]");
    }

    private void ExecuteResize(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        if (attribute.Name == "geometry")
          arguments["geometry"] = Variables.GetValue<MagickGeometry>(attribute);
        else if (attribute.Name == "height")
          arguments["height"] = Variables.GetValue<Int32>(attribute);
        else if (attribute.Name == "percentage")
          arguments["percentage"] = Variables.GetValue<Percentage>(attribute);
        else if (attribute.Name == "percentageHeight")
          arguments["percentageHeight"] = Variables.GetValue<Percentage>(attribute);
        else if (attribute.Name == "percentageWidth")
          arguments["percentageWidth"] = Variables.GetValue<Percentage>(attribute);
        else if (attribute.Name == "width")
          arguments["width"] = Variables.GetValue<Int32>(attribute);
      }
      if (OnlyContains(arguments, "geometry"))
        image.Resize((MagickGeometry)arguments["geometry"]);
      else if (OnlyContains(arguments, "percentage"))
        image.Resize((Percentage)arguments["percentage"]);
      else if (OnlyContains(arguments, "percentageWidth", "percentageHeight"))
        image.Resize((Percentage)arguments["percentageWidth"], (Percentage)arguments["percentageHeight"]);
      else if (OnlyContains(arguments, "width", "height"))
        image.Resize((Int32)arguments["width"], (Int32)arguments["height"]);
      else
        throw new ArgumentException("Invalid argument combination for 'resize', allowed combinations are: [geometry] [percentage] [percentageWidth, percentageHeight] [width, height]");
    }

    private void ExecuteRoll(XmlElement element, MagickImage image)
    {
      Int32 xOffset_ = Variables.GetValue<Int32>(element, "xOffset");
      Int32 yOffset_ = Variables.GetValue<Int32>(element, "yOffset");
      image.Roll(xOffset_, yOffset_);
    }

    private void ExecuteRotate(XmlElement element, MagickImage image)
    {
      double degrees_ = Variables.GetValue<double>(element, "degrees");
      image.Rotate(degrees_);
    }

    private void ExecuteRotationalBlur(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        if (attribute.Name == "angle")
          arguments["angle"] = Variables.GetValue<double>(attribute);
        else if (attribute.Name == "channels")
          arguments["channels"] = Variables.GetValue<Channels>(attribute);
      }
      if (OnlyContains(arguments, "angle"))
        image.RotationalBlur((double)arguments["angle"]);
      else if (OnlyContains(arguments, "angle", "channels"))
        image.RotationalBlur((double)arguments["angle"], (Channels)arguments["channels"]);
      else
        throw new ArgumentException("Invalid argument combination for 'rotationalBlur', allowed combinations are: [angle] [angle, channels]");
    }

    private void ExecuteSample(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        if (attribute.Name == "geometry")
          arguments["geometry"] = Variables.GetValue<MagickGeometry>(attribute);
        else if (attribute.Name == "height")
          arguments["height"] = Variables.GetValue<Int32>(attribute);
        else if (attribute.Name == "percentage")
          arguments["percentage"] = Variables.GetValue<Percentage>(attribute);
        else if (attribute.Name == "percentageHeight")
          arguments["percentageHeight"] = Variables.GetValue<Percentage>(attribute);
        else if (attribute.Name == "percentageWidth")
          arguments["percentageWidth"] = Variables.GetValue<Percentage>(attribute);
        else if (attribute.Name == "width")
          arguments["width"] = Variables.GetValue<Int32>(attribute);
      }
      if (OnlyContains(arguments, "geometry"))
        image.Sample((MagickGeometry)arguments["geometry"]);
      else if (OnlyContains(arguments, "percentage"))
        image.Sample((Percentage)arguments["percentage"]);
      else if (OnlyContains(arguments, "percentageWidth", "percentageHeight"))
        image.Sample((Percentage)arguments["percentageWidth"], (Percentage)arguments["percentageHeight"]);
      else if (OnlyContains(arguments, "width", "height"))
        image.Sample((Int32)arguments["width"], (Int32)arguments["height"]);
      else
        throw new ArgumentException("Invalid argument combination for 'sample', allowed combinations are: [geometry] [percentage] [percentageWidth, percentageHeight] [width, height]");
    }

    private void ExecuteScale(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        if (attribute.Name == "geometry")
          arguments["geometry"] = Variables.GetValue<MagickGeometry>(attribute);
        else if (attribute.Name == "height")
          arguments["height"] = Variables.GetValue<Int32>(attribute);
        else if (attribute.Name == "percentage")
          arguments["percentage"] = Variables.GetValue<Percentage>(attribute);
        else if (attribute.Name == "percentageHeight")
          arguments["percentageHeight"] = Variables.GetValue<Percentage>(attribute);
        else if (attribute.Name == "percentageWidth")
          arguments["percentageWidth"] = Variables.GetValue<Percentage>(attribute);
        else if (attribute.Name == "width")
          arguments["width"] = Variables.GetValue<Int32>(attribute);
      }
      if (OnlyContains(arguments, "geometry"))
        image.Scale((MagickGeometry)arguments["geometry"]);
      else if (OnlyContains(arguments, "percentage"))
        image.Scale((Percentage)arguments["percentage"]);
      else if (OnlyContains(arguments, "percentageWidth", "percentageHeight"))
        image.Scale((Percentage)arguments["percentageWidth"], (Percentage)arguments["percentageHeight"]);
      else if (OnlyContains(arguments, "width", "height"))
        image.Scale((Int32)arguments["width"], (Int32)arguments["height"]);
      else
        throw new ArgumentException("Invalid argument combination for 'scale', allowed combinations are: [geometry] [percentage] [percentageWidth, percentageHeight] [width, height]");
    }

    private void ExecuteSegment(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        if (attribute.Name == "clusterThreshold")
          arguments["clusterThreshold"] = Variables.GetValue<double>(attribute);
        else if (attribute.Name == "quantizeColorSpace")
          arguments["quantizeColorSpace"] = Variables.GetValue<ColorSpace>(attribute);
        else if (attribute.Name == "smoothingThreshold")
          arguments["smoothingThreshold"] = Variables.GetValue<double>(attribute);
      }
      if (arguments.Count == 0)
        image.Segment();
      else if (OnlyContains(arguments, "quantizeColorSpace", "clusterThreshold", "smoothingThreshold"))
        image.Segment((ColorSpace)arguments["quantizeColorSpace"], (double)arguments["clusterThreshold"], (double)arguments["smoothingThreshold"]);
      else
        throw new ArgumentException("Invalid argument combination for 'segment', allowed combinations are: [] [quantizeColorSpace, clusterThreshold, smoothingThreshold]");
    }

    private void ExecuteSelectiveBlur(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        if (attribute.Name == "channels")
          arguments["channels"] = Variables.GetValue<Channels>(attribute);
        else if (attribute.Name == "radius")
          arguments["radius"] = Variables.GetValue<double>(attribute);
        else if (attribute.Name == "sigma")
          arguments["sigma"] = Variables.GetValue<double>(attribute);
        else if (attribute.Name == "threshold")
          arguments["threshold"] = Variables.GetValue<double>(attribute);
      }
      if (OnlyContains(arguments, "radius", "sigma", "threshold"))
        image.SelectiveBlur((double)arguments["radius"], (double)arguments["sigma"], (double)arguments["threshold"]);
      else if (OnlyContains(arguments, "radius", "sigma", "threshold", "channels"))
        image.SelectiveBlur((double)arguments["radius"], (double)arguments["sigma"], (double)arguments["threshold"], (Channels)arguments["channels"]);
      else
        throw new ArgumentException("Invalid argument combination for 'selectiveBlur', allowed combinations are: [radius, sigma, threshold] [radius, sigma, threshold, channels]");
    }

    private void ExecuteSepiaTone(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        arguments[attribute.Name] = Variables.GetValue<Percentage>(attribute);
      }
      if (arguments.Count == 0)
        image.SepiaTone();
      else if (OnlyContains(arguments, "threshold"))
        image.SepiaTone((Percentage)arguments["threshold"]);
      else
        throw new ArgumentException("Invalid argument combination for 'sepiaTone', allowed combinations are: [] [threshold]");
    }

    private void ExecuteSetArtifact(XmlElement element, MagickImage image)
    {
      String name_ = Variables.GetValue<String>(element, "name");
      String value_ = Variables.GetValue<String>(element, "value");
      image.SetArtifact(name_, value_);
    }

    private void ExecuteSetAttenuate(XmlElement element, MagickImage image)
    {
      double attenuate_ = Variables.GetValue<double>(element, "attenuate");
      image.SetAttenuate(attenuate_);
    }

    private void ExecuteSetAttribute(XmlElement element, MagickImage image)
    {
      String name_ = Variables.GetValue<String>(element, "name");
      String value_ = Variables.GetValue<String>(element, "value");
      image.SetAttribute(name_, value_);
    }

    private void ExecuteSetDefine(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        if (attribute.Name == "flag")
          arguments["flag"] = Variables.GetValue<Boolean>(attribute);
        else if (attribute.Name == "format")
          arguments["format"] = Variables.GetValue<MagickFormat>(attribute);
        else if (attribute.Name == "name")
          arguments["name"] = Variables.GetValue<String>(attribute);
        else if (attribute.Name == "value")
          arguments["value"] = Variables.GetValue<String>(attribute);
      }
      if (OnlyContains(arguments, "format", "name", "flag"))
        image.SetDefine((MagickFormat)arguments["format"], (String)arguments["name"], (Boolean)arguments["flag"]);
      else if (OnlyContains(arguments, "format", "name", "value"))
        image.SetDefine((MagickFormat)arguments["format"], (String)arguments["name"], (String)arguments["value"]);
      else
        throw new ArgumentException("Invalid argument combination for 'setDefine', allowed combinations are: [format, name, flag] [format, name, value]");
    }

    private void ExecuteSetDefines(XmlElement element, MagickImage image)
    {
      IDefines defines_ = CreateIDefines(element["defines"]);
      image.SetDefines(defines_);
    }

    private void ExecuteSetHighlightColor(XmlElement element, MagickImage image)
    {
      MagickColor color_ = Variables.GetValue<MagickColor>(element, "color");
      image.SetHighlightColor(color_);
    }

    private void ExecuteSetLowlightColor(XmlElement element, MagickImage image)
    {
      MagickColor color_ = Variables.GetValue<MagickColor>(element, "color");
      image.SetLowlightColor(color_);
    }

    private void ExecuteShade(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        if (attribute.Name == "azimuth")
          arguments["azimuth"] = Variables.GetValue<double>(attribute);
        else if (attribute.Name == "colorShading")
          arguments["colorShading"] = Variables.GetValue<Boolean>(attribute);
        else if (attribute.Name == "elevation")
          arguments["elevation"] = Variables.GetValue<double>(attribute);
      }
      if (arguments.Count == 0)
        image.Shade();
      else if (OnlyContains(arguments, "azimuth", "elevation", "colorShading"))
        image.Shade((double)arguments["azimuth"], (double)arguments["elevation"], (Boolean)arguments["colorShading"]);
      else
        throw new ArgumentException("Invalid argument combination for 'shade', allowed combinations are: [] [azimuth, elevation, colorShading]");
    }

    private void ExecuteShadow(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        if (attribute.Name == "alpha")
          arguments["alpha"] = Variables.GetValue<Percentage>(attribute);
        else if (attribute.Name == "color")
          arguments["color"] = Variables.GetValue<MagickColor>(attribute);
        else if (attribute.Name == "sigma")
          arguments["sigma"] = Variables.GetValue<double>(attribute);
        else if (attribute.Name == "x")
          arguments["x"] = Variables.GetValue<Int32>(attribute);
        else if (attribute.Name == "y")
          arguments["y"] = Variables.GetValue<Int32>(attribute);
      }
      if (arguments.Count == 0)
        image.Shadow();
      else if (OnlyContains(arguments, "color"))
        image.Shadow((MagickColor)arguments["color"]);
      else if (OnlyContains(arguments, "x", "y", "sigma", "alpha"))
        image.Shadow((Int32)arguments["x"], (Int32)arguments["y"], (double)arguments["sigma"], (Percentage)arguments["alpha"]);
      else if (OnlyContains(arguments, "x", "y", "sigma", "alpha", "color"))
        image.Shadow((Int32)arguments["x"], (Int32)arguments["y"], (double)arguments["sigma"], (Percentage)arguments["alpha"], (MagickColor)arguments["color"]);
      else
        throw new ArgumentException("Invalid argument combination for 'shadow', allowed combinations are: [] [color] [x, y, sigma, alpha] [x, y, sigma, alpha, color]");
    }

    private void ExecuteSharpen(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        if (attribute.Name == "channels")
          arguments["channels"] = Variables.GetValue<Channels>(attribute);
        else if (attribute.Name == "radius")
          arguments["radius"] = Variables.GetValue<double>(attribute);
        else if (attribute.Name == "sigma")
          arguments["sigma"] = Variables.GetValue<double>(attribute);
      }
      if (arguments.Count == 0)
        image.Sharpen();
      else if (OnlyContains(arguments, "channels"))
        image.Sharpen((Channels)arguments["channels"]);
      else if (OnlyContains(arguments, "radius", "sigma"))
        image.Sharpen((double)arguments["radius"], (double)arguments["sigma"]);
      else if (OnlyContains(arguments, "radius", "sigma", "channels"))
        image.Sharpen((double)arguments["radius"], (double)arguments["sigma"], (Channels)arguments["channels"]);
      else
        throw new ArgumentException("Invalid argument combination for 'sharpen', allowed combinations are: [] [channels] [radius, sigma] [radius, sigma, channels]");
    }

    private void ExecuteShave(XmlElement element, MagickImage image)
    {
      Int32 leftRight_ = Variables.GetValue<Int32>(element, "leftRight");
      Int32 topBottom_ = Variables.GetValue<Int32>(element, "topBottom");
      image.Shave(leftRight_, topBottom_);
    }

    private void ExecuteShear(XmlElement element, MagickImage image)
    {
      double xAngle_ = Variables.GetValue<double>(element, "xAngle");
      double yAngle_ = Variables.GetValue<double>(element, "yAngle");
      image.Shear(xAngle_, yAngle_);
    }

    private void ExecuteSigmoidalContrast(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        if (attribute.Name == "contrast")
          arguments["contrast"] = Variables.GetValue<double>(attribute);
        else if (attribute.Name == "midpoint")
          arguments["midpoint"] = Variables.GetValue<double>(attribute);
        else if (attribute.Name == "sharpen")
          arguments["sharpen"] = Variables.GetValue<Boolean>(attribute);
      }
      if (OnlyContains(arguments, "sharpen", "contrast"))
        image.SigmoidalContrast((Boolean)arguments["sharpen"], (double)arguments["contrast"]);
      else if (OnlyContains(arguments, "sharpen", "contrast", "midpoint"))
        image.SigmoidalContrast((Boolean)arguments["sharpen"], (double)arguments["contrast"], (double)arguments["midpoint"]);
      else
        throw new ArgumentException("Invalid argument combination for 'sigmoidalContrast', allowed combinations are: [sharpen, contrast] [sharpen, contrast, midpoint]");
    }

    private void ExecuteSketch(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        arguments[attribute.Name] = Variables.GetValue<double>(attribute);
      }
      if (arguments.Count == 0)
        image.Sketch();
      else if (OnlyContains(arguments, "radius", "sigma", "angle"))
        image.Sketch((double)arguments["radius"], (double)arguments["sigma"], (double)arguments["angle"]);
      else
        throw new ArgumentException("Invalid argument combination for 'sketch', allowed combinations are: [] [radius, sigma, angle]");
    }

    private void ExecuteSolarize(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        if (attribute.Name == "factor")
          arguments["factor"] = Variables.GetValue<double>(attribute);
        else if (attribute.Name == "factorPercentage")
          arguments["factorPercentage"] = Variables.GetValue<Percentage>(attribute);
      }
      if (arguments.Count == 0)
        image.Solarize();
      else if (OnlyContains(arguments, "factor"))
        image.Solarize((double)arguments["factor"]);
      else if (OnlyContains(arguments, "factorPercentage"))
        image.Solarize((Percentage)arguments["factorPercentage"]);
      else
        throw new ArgumentException("Invalid argument combination for 'solarize', allowed combinations are: [] [factor] [factorPercentage]");
    }

    private void ExecuteSparseColor(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        if (attribute.Name == "channels")
          arguments["channels"] = Variables.GetValue<Channels>(attribute);
        else if (attribute.Name == "method")
          arguments["method"] = Variables.GetValue<SparseColorMethod>(attribute);
      }
      foreach (XmlElement elem in element.SelectNodes("*"))
      {
        arguments[elem.Name] = CreateSparseColorArgs(elem);
      }
      if (OnlyContains(arguments, "channels", "method", "args"))
        image.SparseColor((Channels)arguments["channels"], (SparseColorMethod)arguments["method"], (IEnumerable<SparseColorArg>)arguments["args"]);
      else if (OnlyContains(arguments, "method", "args"))
        image.SparseColor((SparseColorMethod)arguments["method"], (IEnumerable<SparseColorArg>)arguments["args"]);
      else
        throw new ArgumentException("Invalid argument combination for 'sparseColor', allowed combinations are: [channels, method, args] [method, args]");
    }

    private void ExecuteSplice(XmlElement element, MagickImage image)
    {
      MagickGeometry geometry_ = Variables.GetValue<MagickGeometry>(element, "geometry");
      image.Splice(geometry_);
    }

    private void ExecuteSpread(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        arguments[attribute.Name] = Variables.GetValue<Int32>(attribute);
      }
      if (arguments.Count == 0)
        image.Spread();
      else if (OnlyContains(arguments, "amount"))
        image.Spread((Int32)arguments["amount"]);
      else
        throw new ArgumentException("Invalid argument combination for 'spread', allowed combinations are: [] [amount]");
    }

    private void ExecuteStegano(XmlElement element, MagickImage image)
    {
      MagickImage watermark_ = CreateMagickImage(element["watermark"]);
      image.Stegano(watermark_);
    }

    private void ExecuteStereo(XmlElement element, MagickImage image)
    {
      MagickImage rightImage_ = CreateMagickImage(element["rightImage"]);
      image.Stereo(rightImage_);
    }

    private static void ExecuteStrip(MagickImage image)
    {
      image.Strip();
    }

    private void ExecuteSwirl(XmlElement element, MagickImage image)
    {
      double degrees_ = Variables.GetValue<double>(element, "degrees");
      image.Swirl(degrees_);
    }

    private void ExecuteTexture(XmlElement element, MagickImage image)
    {
      MagickImage image_ = CreateMagickImage(element["image"]);
      image.Texture(image_);
    }

    private void ExecuteThreshold(XmlElement element, MagickImage image)
    {
      Percentage percentage_ = Variables.GetValue<Percentage>(element, "percentage");
      image.Threshold(percentage_);
    }

    private void ExecuteThumbnail(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        if (attribute.Name == "geometry")
          arguments["geometry"] = Variables.GetValue<MagickGeometry>(attribute);
        else if (attribute.Name == "height")
          arguments["height"] = Variables.GetValue<Int32>(attribute);
        else if (attribute.Name == "percentage")
          arguments["percentage"] = Variables.GetValue<Percentage>(attribute);
        else if (attribute.Name == "percentageHeight")
          arguments["percentageHeight"] = Variables.GetValue<Percentage>(attribute);
        else if (attribute.Name == "percentageWidth")
          arguments["percentageWidth"] = Variables.GetValue<Percentage>(attribute);
        else if (attribute.Name == "width")
          arguments["width"] = Variables.GetValue<Int32>(attribute);
      }
      if (OnlyContains(arguments, "geometry"))
        image.Thumbnail((MagickGeometry)arguments["geometry"]);
      else if (OnlyContains(arguments, "percentage"))
        image.Thumbnail((Percentage)arguments["percentage"]);
      else if (OnlyContains(arguments, "percentageWidth", "percentageHeight"))
        image.Thumbnail((Percentage)arguments["percentageWidth"], (Percentage)arguments["percentageHeight"]);
      else if (OnlyContains(arguments, "width", "height"))
        image.Thumbnail((Int32)arguments["width"], (Int32)arguments["height"]);
      else
        throw new ArgumentException("Invalid argument combination for 'thumbnail', allowed combinations are: [geometry] [percentage] [percentageWidth, percentageHeight] [width, height]");
    }

    private void ExecuteTile(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        if (attribute.Name == "args")
          arguments["args"] = Variables.GetValue<String>(attribute);
        else if (attribute.Name == "compose")
          arguments["compose"] = Variables.GetValue<CompositeOperator>(attribute);
      }
      foreach (XmlElement elem in element.SelectNodes("*"))
      {
        arguments[elem.Name] = CreateMagickImage(elem);
      }
      if (OnlyContains(arguments, "image", "compose"))
        image.Tile((MagickImage)arguments["image"], (CompositeOperator)arguments["compose"]);
      else if (OnlyContains(arguments, "image", "compose", "args"))
        image.Tile((MagickImage)arguments["image"], (CompositeOperator)arguments["compose"], (String)arguments["args"]);
      else
        throw new ArgumentException("Invalid argument combination for 'tile', allowed combinations are: [image, compose] [image, compose, args]");
    }

    private void ExecuteTint(XmlElement element, MagickImage image)
    {
      String opacity_ = Variables.GetValue<String>(element, "opacity");
      image.Tint(opacity_);
    }

    private void ExecuteTransform(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        arguments[attribute.Name] = Variables.GetValue<MagickGeometry>(attribute);
      }
      if (OnlyContains(arguments, "imageGeometry"))
        image.Transform((MagickGeometry)arguments["imageGeometry"]);
      else if (OnlyContains(arguments, "imageGeometry", "cropGeometry"))
        image.Transform((MagickGeometry)arguments["imageGeometry"], (MagickGeometry)arguments["cropGeometry"]);
      else
        throw new ArgumentException("Invalid argument combination for 'transform', allowed combinations are: [imageGeometry] [imageGeometry, cropGeometry]");
    }

    private static void ExecuteTransformColorSpace(XmlElement element, MagickImage image)
    {
      ColorProfile source_ = CreateColorProfile(element["source"]);
      ColorProfile target_ = CreateColorProfile(element["target"]);
      image.TransformColorSpace(source_, target_);
    }

    private void ExecuteTransformOrigin(XmlElement element, MagickImage image)
    {
      double x_ = Variables.GetValue<double>(element, "x");
      double y_ = Variables.GetValue<double>(element, "y");
      image.TransformOrigin(x_, y_);
    }

    private static void ExecuteTransformReset(MagickImage image)
    {
      image.TransformReset();
    }

    private void ExecuteTransformRotation(XmlElement element, MagickImage image)
    {
      double angle_ = Variables.GetValue<double>(element, "angle");
      image.TransformRotation(angle_);
    }

    private void ExecuteTransformScale(XmlElement element, MagickImage image)
    {
      double scaleX_ = Variables.GetValue<double>(element, "scaleX");
      double scaleY_ = Variables.GetValue<double>(element, "scaleY");
      image.TransformScale(scaleX_, scaleY_);
    }

    private void ExecuteTransformSkewX(XmlElement element, MagickImage image)
    {
      double skewX_ = Variables.GetValue<double>(element, "skewX");
      image.TransformSkewX(skewX_);
    }

    private void ExecuteTransformSkewY(XmlElement element, MagickImage image)
    {
      double skewY_ = Variables.GetValue<double>(element, "skewY");
      image.TransformSkewY(skewY_);
    }

    private void ExecuteTransparent(XmlElement element, MagickImage image)
    {
      MagickColor color_ = Variables.GetValue<MagickColor>(element, "color");
      image.Transparent(color_);
    }

    private void ExecuteTransparentChroma(XmlElement element, MagickImage image)
    {
      MagickColor colorLow_ = Variables.GetValue<MagickColor>(element, "colorLow");
      MagickColor colorHigh_ = Variables.GetValue<MagickColor>(element, "colorHigh");
      image.TransparentChroma(colorLow_, colorHigh_);
    }

    private static void ExecuteTranspose(MagickImage image)
    {
      image.Transpose();
    }

    private static void ExecuteTransverse(MagickImage image)
    {
      image.Transverse();
    }

    private static void ExecuteTrim(MagickImage image)
    {
      image.Trim();
    }

    private void ExecuteUnsharpmask(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        if (attribute.Name == "amount")
          arguments["amount"] = Variables.GetValue<double>(attribute);
        else if (attribute.Name == "channels")
          arguments["channels"] = Variables.GetValue<Channels>(attribute);
        else if (attribute.Name == "radius")
          arguments["radius"] = Variables.GetValue<double>(attribute);
        else if (attribute.Name == "sigma")
          arguments["sigma"] = Variables.GetValue<double>(attribute);
        else if (attribute.Name == "threshold")
          arguments["threshold"] = Variables.GetValue<double>(attribute);
      }
      if (OnlyContains(arguments, "radius", "sigma"))
        image.Unsharpmask((double)arguments["radius"], (double)arguments["sigma"]);
      else if (OnlyContains(arguments, "radius", "sigma", "amount", "threshold"))
        image.Unsharpmask((double)arguments["radius"], (double)arguments["sigma"], (double)arguments["amount"], (double)arguments["threshold"]);
      else if (OnlyContains(arguments, "radius", "sigma", "amount", "threshold", "channels"))
        image.Unsharpmask((double)arguments["radius"], (double)arguments["sigma"], (double)arguments["amount"], (double)arguments["threshold"], (Channels)arguments["channels"]);
      else if (OnlyContains(arguments, "radius", "sigma", "channels"))
        image.Unsharpmask((double)arguments["radius"], (double)arguments["sigma"], (Channels)arguments["channels"]);
      else
        throw new ArgumentException("Invalid argument combination for 'unsharpmask', allowed combinations are: [radius, sigma] [radius, sigma, amount, threshold] [radius, sigma, amount, threshold, channels] [radius, sigma, channels]");
    }

    private void ExecuteVignette(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        if (attribute.Name == "radius")
          arguments["radius"] = Variables.GetValue<double>(attribute);
        else if (attribute.Name == "sigma")
          arguments["sigma"] = Variables.GetValue<double>(attribute);
        else if (attribute.Name == "x")
          arguments["x"] = Variables.GetValue<Int32>(attribute);
        else if (attribute.Name == "y")
          arguments["y"] = Variables.GetValue<Int32>(attribute);
      }
      if (arguments.Count == 0)
        image.Vignette();
      else if (OnlyContains(arguments, "radius", "sigma", "x", "y"))
        image.Vignette((double)arguments["radius"], (double)arguments["sigma"], (Int32)arguments["x"], (Int32)arguments["y"]);
      else
        throw new ArgumentException("Invalid argument combination for 'vignette', allowed combinations are: [] [radius, sigma, x, y]");
    }

    private void ExecuteWave(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        arguments[attribute.Name] = Variables.GetValue<double>(attribute);
      }
      if (arguments.Count == 0)
        image.Wave();
      else if (OnlyContains(arguments, "amplitude", "length"))
        image.Wave((double)arguments["amplitude"], (double)arguments["length"]);
      else
        throw new ArgumentException("Invalid argument combination for 'wave', allowed combinations are: [] [amplitude, length]");
    }

    private void ExecuteWhiteThreshold(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        if (attribute.Name == "channels")
          arguments["channels"] = Variables.GetValue<Channels>(attribute);
        else if (attribute.Name == "threshold")
          arguments["threshold"] = Variables.GetValue<Percentage>(attribute);
      }
      if (OnlyContains(arguments, "threshold"))
        image.WhiteThreshold((Percentage)arguments["threshold"]);
      else if (OnlyContains(arguments, "threshold", "channels"))
        image.WhiteThreshold((Percentage)arguments["threshold"], (Channels)arguments["channels"]);
      else
        throw new ArgumentException("Invalid argument combination for 'whiteThreshold', allowed combinations are: [threshold] [threshold, channels]");
    }

    private void ExecuteZoom(XmlElement element, MagickImage image)
    {
      Hashtable arguments = new Hashtable();
      foreach (XmlAttribute attribute in element.Attributes)
      {
        if (attribute.Name == "geometry")
          arguments["geometry"] = Variables.GetValue<MagickGeometry>(attribute);
        else if (attribute.Name == "height")
          arguments["height"] = Variables.GetValue<Int32>(attribute);
        else if (attribute.Name == "percentage")
          arguments["percentage"] = Variables.GetValue<Percentage>(attribute);
        else if (attribute.Name == "percentageHeight")
          arguments["percentageHeight"] = Variables.GetValue<Percentage>(attribute);
        else if (attribute.Name == "percentageWidth")
          arguments["percentageWidth"] = Variables.GetValue<Percentage>(attribute);
        else if (attribute.Name == "width")
          arguments["width"] = Variables.GetValue<Int32>(attribute);
      }
      if (OnlyContains(arguments, "geometry"))
        image.Zoom((MagickGeometry)arguments["geometry"]);
      else if (OnlyContains(arguments, "percentage"))
        image.Zoom((Percentage)arguments["percentage"]);
      else if (OnlyContains(arguments, "percentageWidth", "percentageHeight"))
        image.Zoom((Percentage)arguments["percentageWidth"], (Percentage)arguments["percentageHeight"]);
      else if (OnlyContains(arguments, "width", "height"))
        image.Zoom((Int32)arguments["width"], (Int32)arguments["height"]);
      else
        throw new ArgumentException("Invalid argument combination for 'zoom', allowed combinations are: [geometry] [percentage] [percentageWidth, percentageHeight] [width, height]");
    }
  }
}
